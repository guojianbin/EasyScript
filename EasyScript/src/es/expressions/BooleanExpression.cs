using Engine.Bases;

namespace Engine.ES {

/// <summary>
/// @author Easily
/// </summary>
public class BooleanExpression : Expression, IRightExpression, ILogicExpression {

	private readonly ESBoolean _value;

	public BooleanExpression(bool value) {
		_value = new ESBoolean(value);
	}

	public override IESObject Execute(ESDomain domain) {
		return GetValue(domain);
	}

	public IESObject GetValue(ESDomain domain) {
		return _value;
	}

	public bool IsTrue(ESDomain domain) {
		return _value.Value;
	}

	protected override void OnDispose() {
		base.OnDispose();
		_value.Dispose();
	}

	public override string ToString() {
		return string.Format("BooleanExpression Value: {0}", _value);
	}

}

}