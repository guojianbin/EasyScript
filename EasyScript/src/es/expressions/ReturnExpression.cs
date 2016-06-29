using Engine.Bases;

namespace Engine.ES {

/// <summary>
/// @author Easily
/// </summary>
public class ReturnExpression : Expression {

	private static readonly IRightExpression _default = new NumberExpression(0);
	private readonly IRightExpression _value;

	public ReturnExpression() : this(_default) {
		// ignored
	}

	public ReturnExpression(IRightExpression value) {
		_value = value;
	}

	public override IESObject Execute(ESDomain domain) {
		domain.IsReturn = true;
		return _value.GetValue(domain);	
	}

	public override void Checking() {
		_value.Checking();	
	}

	protected override void OnDispose() {
		base.OnDispose();
		_value.Dispose();
	}

	public override string ToString() {
		return string.Format("ReturnExpression Value: {0}", _value);
	}

}

}