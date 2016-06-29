using Engine.Bases;

namespace Easily.ES {

/// <summary>
/// @author Easily
/// </summary>
public class IndexExpression : Expression, IRightExpression, ILogicExpression, ILeftExpression {

	private readonly IRightExpression _target;
	private readonly IRightExpression _value;

	public IndexExpression(IRightExpression target, IRightExpression value) {
		_target = target;
		_value = value;
	}

	public override IESObject Execute(ESDomain domain) {
		return GetValue(domain);
	}

	public IESObject GetValue(ESDomain domain) {
		return _target.GetValue<IESIndex>(domain)[_value.GetValue(domain)];	
	}

	public void SetValue(ESDomain domain, IESObject value) {
		_target.GetValue<IESIndex>(domain)[_value.GetValue(domain)] = value;
	}

	public bool IsTrue(ESDomain domain) {
		return Execute(domain).IsTrue();
	}

	public override void Checking() {
		_target.Checking();
		_value.Checking();
	}

	protected override void OnDispose() {
		base.OnDispose();
		_target.Dispose();
		_value.Dispose();
	}

	public override string ToString() {
		return string.Format("IndexExpression Target: {0}, Name: {1}", _target, _value);
	}

}

}