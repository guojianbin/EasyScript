using System;
using System.Collections.Generic;
using Engine.Bases;

namespace Easily.ES {

/// <summary>
/// @author Easily
/// </summary>
public enum TokenType {
	WORD = 1,						// \w
	NUM,							// \d
	STR,							// string
	COMM,							// comment
	LS, RS, LM, RM, LB, RB,		// (),[],{}
	SS, MM, BB,					// (),[],{}
	EQ,								// =
	LT,GT,							// <>
	EX,								// !
	CMA,							// ,
	DOT,							// .
	COL,							// :
	PLUS, SUB, MUL, DIV,		// +-*/
	FUNC,							// func
	FOR,FOREACH,IN,				// for,foreach,in
	WHILE,							// while
	TRUE,FALSE,					// true,false
	IF,ELSE,						// if else
	BREAK,							// break
	CATCH,							// catch
	RET,							// return
}

/// <summary>
/// @author Easily
/// </summary>
public class Token : Disposable {

	private readonly TokenType _type;
	private readonly string _value;
	private readonly List<Token> _tokens;
	private readonly int _length;
	private readonly byte _tag;

	public TokenType Type {
		get { return _type; }
	}

	public string Value {
		get { return _value; }
	}

	public int Length {
		get { return _length; }
	}

	public List<Token> Tokens {
		get { return _tokens; }
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

	public Token(TokenType type, IEnumerable<Token> tokens) {
		_type = type;
		_tokens = new List<Token>(tokens);
		_tag = 3;
	}

	public override string ToString() {
		switch (_tag) {
			case 1:
				return string.Format("Type: {0}", _type);
			case 2:
				return string.Format("Type: {0}, Value: {1}", _type, _value);
			case 3:
				return string.Format("Type: {0}, Tokens: {1}", _type, _tokens.Count);
			default:
				throw new ArgumentOutOfRangeException(_tag.ToString());
		}
	}

}

}