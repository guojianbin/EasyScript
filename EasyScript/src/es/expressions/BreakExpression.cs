using Engine.Bases;

namespace Engine.ES {

/// <summary>
/// @author Easily
/// </summary>
public class BreakExpression : Expression {

	public override IESObject Execute(ESDomain domain) {
		domain.IsBreak = true;
		return ESDefault.Value;
	}

	public override string ToString() {
		return "BREAK";
	}

}

}