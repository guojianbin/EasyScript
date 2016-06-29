using Engine.Bases;

namespace Engine.ES {

/// <summary>
/// @author Easily
/// </summary>
public class WordExpression : Expression, IRightExpression, ILeftExpression, ILogicExpression, INameExpression {

	private readonly string _value;

	public string Name {
		get { return _value; }
	}

	public WordExpression(string value) {
		_value = value;
	}

	public override IESObject Execute(ESDomain domain) {
		return GetValue(domain);
	}

	public IESObject GetValue(ESDomain domain) {
		return domain.GetValue(_value);
	}

	public void SetValue(ESDomain domain, IESObject value) {
		domain.SetValue(_value, value);
	}

	public bool IsTrue(ESDomain domain) {
		return Execute(domain).IsTrue();
	}

	public override string ToString() {
		return string.Format("WordExpression Value: {0}", _value);
	}

}

}