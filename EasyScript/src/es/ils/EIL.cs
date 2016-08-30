using Easily.Bases;

namespace Easily.ES {

	/// <summary>
	/// @author Easily
	/// </summary>
	public enum ILType {

		PUSH, // push x
		POP, // pop
		CLR, // clr
		BIND, // bind x
		LOAD, // load x
		JUMP, // jump x
		JUMP_IF, // jump_if x
		PROP, // prop x
		CALL, // call n
		RET, // ret
		ADD,
		SUB,
		MUL,
		DIV,
		NOT,
		AND,
		OR,
		EQ,
		NEQ,
		GT,
		GE,
		LT,
		LE

	}

	/// <summary>
	/// @author Easily
	/// </summary>
	public class EIL : Disposable {

		public string name { get; private set; }
		public ILType type { get; private set; }
		public IESObject target { get; private set; }

		public EIL(string name, ILType type) {
			this.name = name;
			this.type = type;
		}

		public EIL(string name, ILType type, IESObject target) {
			this.name = name;
			this.type = type;
			this.target = target;
		}

	}

}