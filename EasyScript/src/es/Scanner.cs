using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Engine.Bases;

namespace Engine.ES {

/// <summary>
/// @author Easily
/// </summary>
public class Scanner : Disposable {

	private static readonly Regex _cmmReg = new Regex(@"((\/\*(.|\n)*?\*\/)|(\-\-|\/\/).*?(\n|$))");
	private static readonly Regex _strReg = new Regex(@"""(\\""|.|\n)*?""");
	private static readonly Regex _wordReg = new Regex(@"\b([a-zA-Z][\w_]*)\b");
	private static readonly Regex _numReg = new Regex(@"(\d+\.\d+|\b\d+\b)");

	private static readonly Regex _funcReg = new Regex(@"\bfunc\b");
	private static readonly Regex _forReg = new Regex(@"\bfor\b");
	private static readonly Regex _foreachReg = new Regex(@"\bforeach\b");
	private static readonly Regex _inReg = new Regex(@"\bin\b");
	private static readonly Regex _ifReg = new Regex(@"\bif\b");
	private static readonly Regex _elseReg = new Regex(@"\belse\b");
	private static readonly Regex _breakReg = new Regex(@"\bbreak\b");
	private static readonly Regex _retReg = new Regex(@"\breturn\b");
	private static readonly Regex _whileReg = new Regex(@"\bwhile\b");
	private static readonly Regex _trueReg = new Regex(@"\btrue\b");
	private static readonly Regex _falseReg = new Regex(@"\bfalse\b");
	private static readonly Regex _catchReg = new Regex(@"\bcatch\b");

	private static readonly List<Struct<Regex, Func<Match, Token>>> _matchObjs = new List<Struct<Regex, Func<Match, Token>>>();
	private static readonly Dictionary<char, TokenType> _markerMap = new Dictionary<char, TokenType>();
	private static readonly char[] _markerList;

	private static readonly Dictionary<TokenType, Struct<TokenType, TokenType>> _rangeMap = new Dictionary<TokenType, Struct<TokenType, TokenType>>();
	private static readonly HashSet<TokenType> _rangeOpens;

	static Scanner() {
		_matchObjs.Add(Struct.Create(_cmmReg, new Func<Match, Token>(m => new Token(TokenType.COMM, m.Value))));
		_matchObjs.Add(Struct.Create(_strReg, new Func<Match, Token>(m => new Token(TokenType.STR, m.Value))));
		_matchObjs.Add(Struct.Create(_funcReg, new Func<Match, Token>(m => new Token(TokenType.FUNC, m.Value))));
		_matchObjs.Add(Struct.Create(_forReg, new Func<Match, Token>(m => new Token(TokenType.FOR, m.Value))));
		_matchObjs.Add(Struct.Create(_foreachReg, new Func<Match, Token>(m => new Token(TokenType.FOREACH, m.Value))));
		_matchObjs.Add(Struct.Create(_inReg, new Func<Match, Token>(m => new Token(TokenType.IN, m.Value))));
		_matchObjs.Add(Struct.Create(_ifReg, new Func<Match, Token>(m => new Token(TokenType.IF, m.Value))));
		_matchObjs.Add(Struct.Create(_elseReg, new Func<Match, Token>(m => new Token(TokenType.ELSE, m.Value))));
		_matchObjs.Add(Struct.Create(_breakReg, new Func<Match, Token>(m => new Token(TokenType.BREAK, m.Value))));
		_matchObjs.Add(Struct.Create(_retReg, new Func<Match, Token>(m => new Token(TokenType.RET, m.Value))));
		_matchObjs.Add(Struct.Create(_whileReg, new Func<Match, Token>(m => new Token(TokenType.WHILE, m.Value))));
		_matchObjs.Add(Struct.Create(_trueReg, new Func<Match, Token>(m => new Token(TokenType.TRUE, m.Value))));
		_matchObjs.Add(Struct.Create(_falseReg, new Func<Match, Token>(m => new Token(TokenType.FALSE, m.Value))));
		_matchObjs.Add(Struct.Create(_catchReg, new Func<Match, Token>(m => new Token(TokenType.CATCH, m.Value))));
		_matchObjs.Add(Struct.Create(_wordReg, new Func<Match, Token>(m => new Token(TokenType.WORD, m.Value))));
		_matchObjs.Add(Struct.Create(_numReg, new Func<Match, Token>(m => new Token(TokenType.NUM, m.Value))));

		_markerMap.Add('(', TokenType.LS);
		_markerMap.Add(')', TokenType.RS);
		_markerMap.Add('[', TokenType.LM);
		_markerMap.Add(']', TokenType.RM);
		_markerMap.Add('{', TokenType.LB);
		_markerMap.Add('}', TokenType.RB);
		_markerMap.Add(',', TokenType.CMA);
		_markerMap.Add('=', TokenType.EQ);
		_markerMap.Add('!', TokenType.EX);
		_markerMap.Add('+', TokenType.PLUS);
		_markerMap.Add('-', TokenType.SUB);
		_markerMap.Add('*', TokenType.MUL);
		_markerMap.Add('/', TokenType.DIV);
		_markerMap.Add('.', TokenType.DOT);
		_markerMap.Add(':', TokenType.COL);
		_markerMap.Add('>', TokenType.GT);
		_markerMap.Add('<', TokenType.LT);
		_markerList = _markerMap.Keys.ToArray();

		_rangeMap.Add(TokenType.LS, Struct.Create(TokenType.RS, TokenType.SS));
		_rangeMap.Add(TokenType.LM, Struct.Create(TokenType.RM, TokenType.MM));
		_rangeMap.Add(TokenType.LB, Struct.Create(TokenType.RB, TokenType.BB));
		_rangeOpens = new HashSet<TokenType>(_rangeMap.Keys);
	}

	private readonly SortedList<int, Token> _sortedList = new SortedList<int, Token>();
	private readonly List<Token> _tokenList = new List<Token>();
	private readonly BitArray _flags;
	private readonly string _src;

	private Scanner(string src) {
		_src = src;
		_flags = new BitArray(_src.Length);
		Start();
	}

	public static List<Token> Parse(string src) {
		return new Scanner(src)._tokenList;
	}

	private void Start() {
		ParseRegs();
		ParseMarks();

		_tokenList.AddRange(_sortedList.Values.Where(t => t.Type != TokenType.COMM));
		_sortedList.Clear();
		ParseRange();
	}

	private void ParseRegs() {
		_matchObjs.ForEach(t => ParseReg(t.Item1, t.Item2));
	}

	private void ParseReg(Regex reg, Func<Match, Token> func) {
		var matches = reg.Matches(_src).Cast<Match>();
		matches.ForEach(t => AddToken(t.Index, func(t)));
	}

	private void ParseMarks() {
		var pos = 0;
		while (true) {
			var index = _src.IndexOfAny(_markerList, pos);
			if (index == -1) {
				break;
			} else {
				AddToken(index, new Token(_markerMap[_src[index]]));
				pos = index + 1;	
			}
		}
	}

	private void AddToken(int index, Token token) {
		if (_flags[index]) {
			return;
		}
		_sortedList.Add(index, token);
		var begin = index;
		var end = index + token.Length;
		for (var i = begin; i < end; i++) {
			_flags[i] = true;
		}
	}

	private void ParseRange() {
		for (var i = 0; i < _tokenList.Count; i++) {
			if (_rangeOpens.Contains(_tokenList[i].Type)) {
				var obj = _rangeMap[_tokenList[i].Type];
				ParseRange(i, obj.Item1, obj.Item2);
			}
		}
	}

	private void ParseRange(int pos, TokenType endType, TokenType comType) {
		for (var i = pos + 1; i < _tokenList.Count; i++) {
			if (_rangeOpens.Contains(_tokenList[i].Type)) {
				var obj = _rangeMap[_tokenList[i].Type];
				ParseRange(i, obj.Item1, obj.Item2);
			} else if (_tokenList[i].Type == endType) {
				var len = i - pos + 1;
				var list = _tokenList.GetRange(pos + 1, len - 2);
				_tokenList.RemoveRange(pos, len);
				_tokenList.Insert(pos, new Token(comType, list));
				break;
			}
		}
	}

}

}
