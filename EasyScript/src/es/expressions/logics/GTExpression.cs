using Engine.Bases;

namespace Engine.ES {

/// <summary>
/// @author Easily
/// </summary>
public class GTExpression : LogicExpression {

	private readonly IRightExpression _value1;
	private readonly IRightExpression _value2;

	public GTExpression(IRightExpression value1, IRightExpression value2) {
		_value1 = value1;
		_value2 = value2;
	}

	public override bool IsTrue(ESDomain domain) {
		var v1 = _value1.GetValue<IESNumber>(domain);
		var v2 = _value2.GetValue<IESNumber>(domain);
		return v1.Value > v2.Value;
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
		return string.Format("GT Value1: {0}, Value2: {1}", _value1, _value2);
	}

}

}