using System;
using System.Collections;
using System.Collections.Generic;
using Easily.Bases;

namespace Easily.ES {

	/// <summary>
	/// @author Easily
	/// </summary>
	internal enum TokenType {

		WORD = 1, // \w
		NUM, // \d
		STR, // string
		COMM, // comment
		LS,
		RS,
		LM,
		RM,
		LB,
		RB, // (),[],{}
		SS,
		MM,
		BB, // (),[],{}
		EQ,
		LT,
		GT,
		EX, // =,<>,!
		CMA,
		DOT,
		COL, // ,.:
		ADD,
		SUB,
		MUL,
		DIV, // +-*/
		FUNC,
		CLASS,
		NEW, // func,class,new
		FOR,
		FOREACH,
		IN, // for,foreach,in
		WHILE, // while
		TRUE,
		FALSE, // true,false
		IF,
		ELSE, // if else
		BREAK, // break
		RET // return

	}

	/// <summary>
	/// @author Easily
	/// </summary>
	internal class Token : Disposable, IEnumerable<Token> {

		private readonly int _length;
		private readonly byte _tag;
		private readonly List<Token> _tokens;
		private readonly TokenType _type;
		private readonly string _value;

		public TokenType Type {
			get { return _type; }
		}

		public string Value {
			get { return _value; }
		}

		public int Length {
			get { return _length; }
		}

		public Token(TokenType type) {
			_type = type;
			_length = 1;
			_tag = 1;
		}

		public Token(TokenType type, string value) {
			_type = type;
			_value = value;
			_length = _value.Length;
			_tag = 2;
		}

		public Token(TokenType type, List<Token> tokens) {
			_type = type;
			_tokens = tokens;
			_tag = 3;
		}

		public override string ToString() {
			if (_tag == 1) {
				return string.Format("Type: {0}", _type);
			} else if (_tag == 2) {
				return string.Format("Type: {0}, Value: {1}", _type, _value);
			} else if (_tag == 3) {
				return string.Format("Type: {0}, Tokens: {1}", _type, _tokens.Count);
			} else {
				throw new ArgumentOutOfRangeException(_tag.ToString());
			}
		}

		public IEnumerator<Token> GetEnumerator() {
			return _tokens.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator() {
			return GetEnumerator();
		}

	}

}