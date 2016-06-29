using Engine.Bases;

namespace Easily.ES {

/// <summary>
/// @author Easily
/// </summary>
public enum ILType {
	PUSH, POP, CLR,
	ADD, SUB, MUL, DIV,
	NOT, AND, OR, EQ, NEQ, GT, GE, LT, LE,
	CALL,
	RET,
}

/// <summary>
/// @author Easily
/// </summary>
public class EIL : Disposable {

	public ILType type { get; private set; }
	public IESObject target { get; private set; }

	public EIL(ILType type) {
		this.type = type;
	}

	public EIL(ILType type, IESObject target) {
		this.type = type;
		this.target = target;
	}

}

}