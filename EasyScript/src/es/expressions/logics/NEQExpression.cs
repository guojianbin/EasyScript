using Engine.Bases;

namespace Engine.ES {

/// <summary>
/// @author Easily
/// </summary>
public class NEQExpression : LogicExpression {

	private readonly IRightExpression _value1;
	private readonly IRightExpression _value2;

	public NEQExpression(IRightExpression value1, IRightExpression value2) {
		_value1 = value1;
		_value2 = value2;
	}

	public override bool IsTrue(ESDomain domain) {
		var v1 = _value1.GetValue(domain).ToObject();
		var v2 = _value2.GetValue(domain).ToObject();
		return !v1.Equals(v2);
	}

	public override void Checking() {
		_value1.Checking();
		_value2.Checking();
	}

	protected override void OnDispose() {
		base.OnDispose();
		_value1.Dispose();
		_value2.Dispose();
	}

	public override string ToString() {
		return string.Format("NEQ Value1: {0}, Value2: {1}", _value1, _value2);
	}

}

}