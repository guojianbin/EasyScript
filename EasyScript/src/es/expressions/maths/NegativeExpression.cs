using Engine.Bases;

namespace Engine.ES {

/// <summary>
/// @author Easily
/// </summary>
public class NegativeExpression : Expression, IRightExpression, ILogicExpression {

	private readonly IRightExpression _value;

	public NegativeExpression(IRightExpression value) {
		_value = value;
	}

	public override IESObject Execute(ESDomain domain) {
		return GetValue(domain);
	}

	public IESObject GetValue(ESDomain domain) {
		return new ESNumber(-_value.GetValue<IESNumber>(domain).Value);
	}

	public bool IsTrue(ESDomain domain) {
		return Execute(domain).IsTrue();
	}

	public override void Checking() {
		_value.Checking();
	}

	protected override void OnDispose() {
		base.OnDispose();
		_value.Dispose();
	}

	public override string ToString() {
		return string.Format("NegativeExpression Value: {0}", _value);
	}

}

}