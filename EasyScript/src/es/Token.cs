using Easily.Bases;

namespace Easily.ES {

	/// <summary>
	/// @author Easily
	/// </summary>
	internal enum TokenType {

		WORD = 1,						// \w
		NUM,							// \d
		STR,							// string
		LS, RS, LM, RM, LB, RB,		// (),[],{}
		SS, MM, BB,					// (),[],{}
		EQ, LT, GT, EX,				// =,<>,!
		CMA, DOT, RDO, COL,			// ,.`:
		ADD	, SUB, MUL, DIV,			// +-*/
		FUNC, CLASS, NEW,			// func,class,new
		FOR, FOREACH, IN,			// for,foreach,in
		WHILE,							// while
		TRUE, FALSE,					// true,false
		IF, ELSE,						// if else
		BREAK,							// break
		RET								// return

	}

	/// <summary>
	/// @author Easily
	/// </summary>
	internal class Token : Disposable {

		internal TokenType Type;
		internal object Value;

		public override string ToString() {
			if (Value == null) {
				return string.Format("Type: {0}", Type);
			} else {
				return string.Format("Type: {0}, Value: {1}", Type, Value);
			}
		}

	}

	/// <summary>
	/// @author Easily
	/// </summary>
	internal class RangeToken {

		internal TokenType End;
		internal TokenType Target;

		public override string ToString() {
			return string.Format("End: {0}, Target: {1}", End, Target);
		}

	}

}