using System.Collections.Generic;
using System.Text.RegularExpressions;
using Easily.Bases;

namespace Easily.ES {

	/// <summary>
	/// @author Easily
	/// </summary>
	internal class Scanner : Disposable {

		private static readonly Dictionary<char, TokenType> _specChars = new Dictionary<char, TokenType>();
		private static readonly Dictionary<TokenType, RangeToken> _rangeMap = new Dictionary<TokenType, RangeToken>();

		private static readonly Regex _ignored = new Regex(@"[\r\n\t\s ]+|\/\*(.|\n)*?\*\/|(\-\-|\/\/).*?(\n|$)", RegexOptions.Compiled);
		private static readonly Regex _str = new Regex(@"""(\\""|.|\n)*?""", RegexOptions.Compiled);
		private static readonly Regex _func = new Regex(@"\bfunction\b", RegexOptions.Compiled);
		private static readonly Regex _class = new Regex(@"\bclass\b", RegexOptions.Compiled);
		private static readonly Regex _new = new Regex(@"\bnew\b", RegexOptions.Compiled);
		private static readonly Regex _for = new Regex(@"\bfor\b", RegexOptions.Compiled);
		private static readonly Regex _foreach = new Regex(@"\bforeach\b", RegexOptions.Compiled);
		private static readonly Regex _in = new Regex(@"\bin\b", RegexOptions.Compiled);
		private static readonly Regex _if = new Regex(@"\bif\b", RegexOptions.Compiled);
		private static readonly Regex _else = new Regex(@"\belse\b", RegexOptions.Compiled);
		private static readonly Regex _break = new Regex(@"\bbreak\b", RegexOptions.Compiled);
		private static readonly Regex _ret = new Regex(@"\breturn\b", RegexOptions.Compiled);
		private static readonly Regex _while = new Regex(@"\bwhile\b", RegexOptions.Compiled);
		private static readonly Regex _true = new Regex(@"\btrue\b", RegexOptions.Compiled);
		private static readonly Regex _false = new Regex(@"\bfalse\b", RegexOptions.Compiled);
		private static readonly Regex _word = new Regex(@"\b[_a-zA-Z]\w*\b", RegexOptions.Compiled);
		private static readonly Regex _number = new Regex(@"\b(\d+\.\d+|\d+)\b", RegexOptions.Compiled);

		private readonly string _src;
		private readonly Dictionary<Regex, Match> _matches = new Dictionary<Regex, Match>();
		private readonly LinkedList<Token> _tokens = new LinkedList<Token>();

		static Scanner() {
			_specChars.Add('(', TokenType.LS);
			_specChars.Add(')', TokenType.RS);
			_specChars.Add('[', TokenType.LM);
			_specChars.Add(']', TokenType.RM);
			_specChars.Add('{', TokenType.LB);
			_specChars.Add('}', TokenType.RB);
			_specChars.Add('=', TokenType.EQ);
			_specChars.Add('!', TokenType.EX);
			_specChars.Add('>', TokenType.GT);
			_specChars.Add('<', TokenType.LT);
			_specChars.Add(',', TokenType.CMA);
			_specChars.Add('+', TokenType.ADD);
			_specChars.Add('-', TokenType.SUB);
			_specChars.Add('*', TokenType.MUL);
			_specChars.Add('/', TokenType.DIV);
			_specChars.Add('.', TokenType.DOT);
			_specChars.Add(':', TokenType.COL);
			_specChars.Add('`', TokenType.RDO);

			_rangeMap.Add(TokenType.LS, new RangeToken { End = TokenType.RS, Target = TokenType.SS });
			_rangeMap.Add(TokenType.LM, new RangeToken { End = TokenType.RM, Target = TokenType.MM });
			_rangeMap.Add(TokenType.LB, new RangeToken { End = TokenType.RB, Target = TokenType.BB });
		}

		internal static IEnumerable<Token> Parse(string src) {
			return new Scanner(src)._tokens;
		}

		private Scanner(string src) {
			_src = src;
			Start();
		}

		private void Start() {
			Parse();
			Merge();
		}

		private bool IsMatch(Regex regex, int pos, out Match match) {
			if (!_matches.TryGetValue(regex, out match)) {
				match = regex.Match(_src, pos);
				_matches.Add(regex, match);
			}
			if (!match.Success) {
				return false;
			} else if (match.Index == pos) {
				_matches[regex] = match.NextMatch();
				return true;
			} else if (match.Index < pos) {
				_matches[regex] = match.NextMatch();
				return IsMatch(regex, pos, out match);
			} else {
				return false;
			}
		}

		private void Parse() {
			var pos = 0;
			Match match;
			TokenType type;
			while (pos < _src.Length) {
				if (IsMatch(_ignored, pos, out match)) {
					pos += match.Length;
				} else if (_specChars.TryGetValue(_src[pos], out type)) {
					_tokens.AddLast(new Token { Type = type });
					pos += 1;
				} else if (IsMatch(_str, pos, out match)) {
					_tokens.AddLast(new Token { Type = TokenType.STR, Value = _src.Substring(pos + 1, match.Length - 2) });
					pos += match.Length;
				} else if (IsMatch(_func, pos, out match)) {
					_tokens.AddLast(new Token { Type = TokenType.FUNC });
					pos += match.Length;
				} else if (IsMatch(_class, pos, out match)) {
					_tokens.AddLast(new Token { Type = TokenType.CLASS });
					pos += match.Length;
				} else if (IsMatch(_new, pos, out match)) {
					_tokens.AddLast(new Token { Type = TokenType.NEW });
					pos += match.Length;
				} else if (IsMatch(_for, pos, out match)) {
					_tokens.AddLast(new Token { Type = TokenType.FOR });
					pos += match.Length;
				} else if (IsMatch(_foreach, pos, out match)) {
					_tokens.AddLast(new Token { Type = TokenType.FOREACH });
					pos += match.Length;
				} else if (IsMatch(_in, pos, out match)) {
					_tokens.AddLast(new Token { Type = TokenType.IN });
					pos += match.Length;
				} else if (IsMatch(_if, pos, out match)) {
					_tokens.AddLast(new Token { Type = TokenType.IF });
					pos += match.Length;
				} else if (IsMatch(_else, pos, out match)) {
					_tokens.AddLast(new Token { Type = TokenType.ELSE });
					pos += match.Length;
				} else if (IsMatch(_break, pos, out match)) {
					_tokens.AddLast(new Token { Type = TokenType.BREAK });
					pos += match.Length;
				} else if (IsMatch(_ret, pos, out match)) {
					_tokens.AddLast(new Token { Type = TokenType.RET });
					pos += match.Length;
				} else if (IsMatch(_while, pos, out match)) {
					_tokens.AddLast(new Token { Type = TokenType.WHILE });
					pos += match.Length;
				} else if (IsMatch(_true, pos, out match)) {
					_tokens.AddLast(new Token { Type = TokenType.TRUE });
					pos += match.Length;
				} else if (IsMatch(_false, pos, out match)) {
					_tokens.AddLast(new Token { Type = TokenType.FALSE });
					pos += match.Length;
				} else if (IsMatch(_word, pos, out match)) {
					_tokens.AddLast(new Token { Type = TokenType.WORD, Value = _src.Substring(pos, match.Length) });
					pos += match.Length;
				} else if (IsMatch(_number, pos, out match)) {
					_tokens.AddLast(new Token { Type = TokenType.NUM, Value = _src.Substring(pos, match.Length) });
					pos += match.Length;
				} else {
					ThrowException(ExceptionType.ARGUMENT, _src[pos].ToString());
				}
			}
		}

		private void Merge() {
			var node = _tokens.First;
			RangeToken temp;
			while (node != null) {
				if (_rangeMap.TryGetValue(node.Value.Type, out temp)) {
					Merge(ref node, temp);
				} else {
					node = node.Next;
				}
			}
		}

		private void Merge(ref LinkedListNode<Token> begin, RangeToken range) {
			var node = begin.Next;
			RangeToken temp;
			while (node != null) {
				if (_rangeMap.TryGetValue(node.Value.Type, out temp)) {
					Merge(ref node, temp);
				} else if (node.Value.Type == range.End) {
					var last = begin.Previous;
					var list = new List<Token>(ESUtility.Range(begin.Next, node));
					ESUtility.Remove(_tokens, begin, node.Next);
					begin = _tokens.AddAfter(last, (new Token { Type = range.Target, Value = list })).Next;
					break;
				} else {
					node = node.Next;
				}
			}
		}

	}

}