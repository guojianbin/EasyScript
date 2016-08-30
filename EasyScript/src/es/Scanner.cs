using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Easily.Bases;

namespace Easily.ES {

	/// <summary>
	/// @author Easily
	/// </summary>
	internal class Scanner : Disposable {

		private static readonly Regex _cmment = new Regex(@"((\/\*(.|\n)*?\*\/)|(\-\-|\/\/).*?(\n|$))");
		private static readonly Regex _str = new Regex(@"""(\\""|.|\n)*?""");
		private static readonly Regex _word = new Regex(@"\b([a-zA-Z][\w_]*)\b");
		private static readonly Regex _number = new Regex(@"(\d+\.\d+|\b\d+\b)");
		private static readonly Regex _func = new Regex(@"\bfunction\b");
		private static readonly Regex _class = new Regex(@"\bclass\b");
		private static readonly Regex _new = new Regex(@"\bnew\b");
		private static readonly Regex _for = new Regex(@"\bfor\b");
		private static readonly Regex _foreach = new Regex(@"\bforeach\b");
		private static readonly Regex _in = new Regex(@"\bin\b");
		private static readonly Regex _if = new Regex(@"\bif\b");
		private static readonly Regex _else = new Regex(@"\belse\b");
		private static readonly Regex _break = new Regex(@"\bbreak\b");
		private static readonly Regex _ret = new Regex(@"\breturn\b");
		private static readonly Regex _while = new Regex(@"\bwhile\b");
		private static readonly Regex _true = new Regex(@"\btrue\b");
		private static readonly Regex _false = new Regex(@"\bfalse\b");
		private static readonly List<Struct<Regex, Func<Match, Token>>> _matches = new List<Struct<Regex, Func<Match, Token>>>();
		private static readonly Dictionary<char, TokenType> _m2t = new Dictionary<char, TokenType>();
		private static readonly HashSet<char> _markers;
		private static readonly Dictionary<TokenType, Struct<TokenType, TokenType>> _ranges = new Dictionary<TokenType, Struct<TokenType, TokenType>>();
		private static readonly HashSet<TokenType> _opens;
		private readonly BitArray _flags;
		private readonly string _src;
		private readonly SortedList<int, Token> _tokens = new SortedList<int, Token>();
		private readonly List<Token> _tokens2 = new List<Token>();

		private Scanner(string src) {
			_src = src;
			_flags = new BitArray(_src.Length);
		}

		static Scanner() {
			_matches.Add(Struct.Create(_cmment, new Func<Match, Token>(m => new Token(TokenType.COMM, m.Value))));
			_matches.Add(Struct.Create(_str, new Func<Match, Token>(m => new Token(TokenType.STR, m.Value))));
			_matches.Add(Struct.Create(_func, new Func<Match, Token>(m => new Token(TokenType.FUNC, m.Value))));
			_matches.Add(Struct.Create(_class, new Func<Match, Token>(m => new Token(TokenType.CLASS, m.Value))));
			_matches.Add(Struct.Create(_new, new Func<Match, Token>(m => new Token(TokenType.NEW, m.Value))));
			_matches.Add(Struct.Create(_for, new Func<Match, Token>(m => new Token(TokenType.FOR, m.Value))));
			_matches.Add(Struct.Create(_foreach, new Func<Match, Token>(m => new Token(TokenType.FOREACH, m.Value))));
			_matches.Add(Struct.Create(_in, new Func<Match, Token>(m => new Token(TokenType.IN, m.Value))));
			_matches.Add(Struct.Create(_if, new Func<Match, Token>(m => new Token(TokenType.IF, m.Value))));
			_matches.Add(Struct.Create(_else, new Func<Match, Token>(m => new Token(TokenType.ELSE, m.Value))));
			_matches.Add(Struct.Create(_break, new Func<Match, Token>(m => new Token(TokenType.BREAK, m.Value))));
			_matches.Add(Struct.Create(_ret, new Func<Match, Token>(m => new Token(TokenType.RET, m.Value))));
			_matches.Add(Struct.Create(_while, new Func<Match, Token>(m => new Token(TokenType.WHILE, m.Value))));
			_matches.Add(Struct.Create(_true, new Func<Match, Token>(m => new Token(TokenType.TRUE, m.Value))));
			_matches.Add(Struct.Create(_false, new Func<Match, Token>(m => new Token(TokenType.FALSE, m.Value))));
			_matches.Add(Struct.Create(_word, new Func<Match, Token>(m => new Token(TokenType.WORD, m.Value))));
			_matches.Add(Struct.Create(_number, new Func<Match, Token>(m => new Token(TokenType.NUM, m.Value))));

			_m2t.Add('(', TokenType.LS);
			_m2t.Add(')', TokenType.RS);
			_m2t.Add('[', TokenType.LM);
			_m2t.Add(']', TokenType.RM);
			_m2t.Add('{', TokenType.LB);
			_m2t.Add('}', TokenType.RB);
			_m2t.Add(',', TokenType.CMA);
			_m2t.Add('=', TokenType.EQ);
			_m2t.Add('!', TokenType.EX);
			_m2t.Add('+', TokenType.ADD);
			_m2t.Add('-', TokenType.SUB);
			_m2t.Add('*', TokenType.MUL);
			_m2t.Add('/', TokenType.DIV);
			_m2t.Add('.', TokenType.DOT);
			_m2t.Add(':', TokenType.COL);
			_m2t.Add('>', TokenType.GT);
			_m2t.Add('<', TokenType.LT);
			_markers = new HashSet<char>(_m2t.Keys.ToArray());

			_ranges.Add(TokenType.LS, Struct.Create(TokenType.RS, TokenType.SS));
			_ranges.Add(TokenType.LM, Struct.Create(TokenType.RM, TokenType.MM));
			_ranges.Add(TokenType.LB, Struct.Create(TokenType.RB, TokenType.BB));
			_opens = new HashSet<TokenType>(_ranges.Keys);
		}

		public static List<Token> Parse(string src) {
			var scanner = new Scanner(src);
			return scanner.Start();
		}

		private List<Token> Start() {
			Parse();
			Parse2();

			_tokens2.AddRange(_tokens.Values.Where(t => t.Type != TokenType.COMM));
			_tokens.Clear();

			Parse3();
			return _tokens2;
		}

		private void Parse() {
			_matches.ForEach(t => Parse(t.Item1, t.Item2));
		}

		private void Parse(Regex reg, Func<Match, Token> func) {
			var matches = reg.Matches(_src).Cast<Match>();
			matches.ForEach(t => Add(t.Index, func(t)));
		}

		private void Parse2() {
			var pos = 0;
			while (true) {
				if (pos >= _src.Length) {
					break;
				}
				if (_flags[pos]) {
					pos += 1;
					continue;
				}
				var index = FindMarker(pos);
				if (index == -1) {
					break;
				} else {
					Add(index, new Token(_m2t[_src[index]]));
					pos = index + 1;
				}
			}
		}

		public int FindMarker(int pos) {
			for (var i = pos; i < _src.Length; i++) {
				if (_markers.Contains(_src[i])) {
					return i;
				}
			}
			return -1;
		}

		private void Add(int index, Token token) {
			if (_flags[index]) {
				return;
			}
			_tokens.Add(index, token);
			var begin = index;
			var end = index + token.Length;
			for (var i = begin; i < end; i++) {
				_flags[i] = true;
			}
		}

		private void Parse3() {
			for (var i = 0; i < _tokens2.Count; i++) {
				if (_opens.Contains(_tokens2[i].Type)) {
					var obj = _ranges[_tokens2[i].Type];
					Parse3(i, obj.Item1, obj.Item2);
				}
			}
		}

		private void Parse3(int pos, TokenType right, TokenType comb) {
			for (var i = pos + 1; i < _tokens2.Count; i++) {
				if (_opens.Contains(_tokens2[i].Type)) {
					var obj = _ranges[_tokens2[i].Type];
					Parse3(i, obj.Item1, obj.Item2);
				} else if (_tokens2[i].Type == right) {
					var len = i - pos + 1;
					var list = _tokens2.GetRange(pos + 1, len - 2);
					_tokens2.RemoveRange(pos, len);
					_tokens2.Insert(pos, new Token(comb, list));
					break;
				}
			}
		}

	}

}