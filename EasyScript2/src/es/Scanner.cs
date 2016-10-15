using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Easily.ES {

	/// <summary>
	/// @author Easily
	/// </summary>
	public class Scanner {

	    private static readonly HashSet<char> _operators = new HashSet<char>(new[] { '(', ')', '[', ']', '{', '}', '=', '!', '>', '<', ',', '+', '-', '*', '/', '.', ':', '&', '$', ';' });
        private static readonly Regex _ignored = new Regex(@"[ \s\r\n\t]+", RegexOptions.Compiled);
        private static readonly Regex _comments = new Regex(@"\/\*(.|\n)*?\*\/|\/\/.*?(\n|$)", RegexOptions.Compiled);
        private static readonly Regex _str = new Regex(@"""(\\""|.|\n)*?""", RegexOptions.Compiled);
        private static readonly Regex _keywords = new Regex(@"\b(function|class|new|for|foreach|in|if|else|break|return|while|true|false|default)\b", RegexOptions.Compiled);
        private static readonly Regex _word = new Regex(@"\b[_a-zA-Z]\w*\b", RegexOptions.Compiled);
        private static readonly Regex _number = new Regex(@"\b(\d+\.\d+|\d+)\b", RegexOptions.Compiled);

        private readonly Dictionary<Regex, Match> _matches = new Dictionary<Regex, Match>();
        private readonly List<Token> _tokens = new List<Token>();
        private readonly string _src;

        public Scanner(string src) {
	        _src = src;
            Parse();
        }

	    internal List<Token> Tokens {
	        get { return _tokens; }
	    }

	    private bool IsMatch(Regex regex, int pos, out Match match) {
	        if (_matches.TryGetValue(regex, out match)) {
	            return TryMatch(regex, pos, match);
	        } else {
	            match = regex.Match(_src, pos);
	            _matches.Add(regex, match);
	            return TryMatch(regex, pos, match);
	        }
	    }

	    private bool TryMatch(Regex regex, int pos, Match match) {
	        while (true) {
	            if (!match.Success) {
	                return false;
	            } else if (match.Index == pos) {
	                _matches[regex] = match.NextMatch();
	                return true;
	            } else if (match.Index < pos) {
	                match = regex.Match(_src, pos);
	            } else {
	                return false;
	            }
	        }
	    }

	    private void Parse() {
	        var pos = 0;
	        while (pos < _src.Length) {
	            Match match;
	            if (IsMatch(_ignored, pos, out match)) {
	                pos += match.Length;
	            } else if (IsMatch(_comments, pos, out match)) {
	                _tokens.Add(new Token { Type = TokenType.Comment, Value = _src.Substring(pos, match.Length) });
	                pos += match.Length;
	            } else if (_operators.Contains(_src[pos])) {
	                _tokens.Add(new Token { Type = TokenType.Operator, Value = _src[pos].ToString() });
	                pos += 1;
	            } else if (IsMatch(_str, pos, out match)) {
	                _tokens.Add(new Token { Type = TokenType.String, Value = _src.Substring(pos, match.Length) });
	                pos += match.Length;
	            } else if (IsMatch(_keywords, pos, out match)) {
	                _tokens.Add(new Token { Type = TokenType.Keyword, Value = _src.Substring(pos, match.Length) });
	                pos += match.Length;
	            } else if (IsMatch(_word, pos, out match)) {
	                _tokens.Add(new Token { Type = TokenType.Word, Value = _src.Substring(pos, match.Length) });
	                pos += match.Length;
	            } else if (IsMatch(_number, pos, out match)) {
	                _tokens.Add(new Token { Type = TokenType.Number, Value = _src.Substring(pos, match.Length) });
	                pos += match.Length;
	            } else {
	                throw new Exception(_src[pos].ToString());
	            }
	        }
	    }

	}

}