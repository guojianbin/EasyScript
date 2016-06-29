using Engine.Bases;

namespace Easily.ES {

/// <summary>
/// @author Easily
/// </summary>
public class NumberExpression : Expression, IRightExpression, ILogicExpression, INameExpression {

	private readonly ESNumber _value;

	public string Name {
		get { return _value.GetString(); }
	}

	public NumberExpression(string str) {
		_value = new ESNumber(str);
	}

	public NumberExpression(float value) {
		_value = new ESNumber(value);
	}

	public override IESObject Execute(ESDomain domain) {
		return _value;
	}

	public IESObject GetValue(ESDomain domain) {
		return _value;
	}

	public bool IsTrue(ESDomain domain) {
		return _value.IsTrue();
	}

	protected override void OnDispose() {
		base.OnDispose();
		_value.Dispose();
	}

	public override string ToString() {
		return string.Format("NumberExpression Value: {0}", _value);
	}

}

}