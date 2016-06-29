using Engine.Bases;

namespace Engine.ES {

/// <summary>
/// @author Easily
/// </summary>
public class PropertyExpression : Expression, IRightExpression, ILogicExpression, ILeftExpression {

	private readonly IRightExpression _target;
	private readonly INameExpression _property;

	public PropertyExpression(IRightExpression target, INameExpression property) {
		_target = target;
		_property = property;
	}

	private IESProperty GetProperty(ESDomain domain) {
		return Execute(domain).Cast<IESProperty>();
	}

	public override IESObject Execute(ESDomain domain) {
		return GetValue(domain);
	}

	public IESObject GetValue(ESDomain domain) {
		return _target.GetValue(domain).GetProperty(_property.Name);
	}

	public bool IsTrue(ESDomain domain) {
		return GetProperty(domain).GetValue().IsTrue();
	}

	public void SetValue(ESDomain domain, IESObject value) {
		GetProperty(domain).SetValue(value);
	}

	public override void Checking() {
		_target.Checking();
		_property.Checking();
	}

	protected override void OnDispose() {
		base.OnDispose();
		_target.Dispose();
		_property.Dispose();
	}

	public override string ToString() {
		return string.Format("PropertyExpression Target: {0}, Name: {1}", _target, _property);
	}

}

}