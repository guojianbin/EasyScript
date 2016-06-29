using Engine.Bases;

namespace Easily.ES {

/// <summary>
/// @author Easily
/// </summary>
public class StringExpression : Expression, IRightExpression, ILogicExpression, INameExpression {

	private readonly ESString _value;

	public string Name {
		get { return _value.Value; }
	}

	public StringExpression(string str) : this(new ESString(str)) {
		// ignored
	}

	public StringExpression(ESString value) {
		_value = value;
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
		return string.Format("StringExpression Value: {0}", _value);
	}

}

}