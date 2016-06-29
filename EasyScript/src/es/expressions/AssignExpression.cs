using Engine.Bases;

namespace Easily.ES {

/// <summary>
/// @author Easily
/// </summary>
public class AssignExpression : Expression, IRightExpression {

	private readonly ILeftExpression _lvalue;
	private readonly IRightExpression _rvalue;

	public ILeftExpression LValue {
		get { return _lvalue; }
	}

	public IRightExpression RValue {
		get { return _rvalue; }
	}

	public AssignExpression(ILeftExpression lvalue, IRightExpression rvalue) {
		_lvalue = lvalue;
		_rvalue = rvalue;
	}

	public override IESObject Execute(ESDomain domain) {
		return GetValue(domain);
	}

	public IESObject GetValue(ESDomain domain) {
		var value = _rvalue.GetValue(domain);
		_lvalue.SetValue(domain, value.Clone());
		return value;
	}

	public override void Checking() {
		_lvalue.Checking();
		_rvalue.Checking();
	}

	protected override void OnDispose() {
		base.OnDispose();
		_lvalue.Dispose();
		_rvalue.Dispose();
	}

	public override string ToString() {
		return string.Format("AssignExpression Left: {0}, Right: {1}", _lvalue, _rvalue);
	}

}

}