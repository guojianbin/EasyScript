using Engine.Bases;

namespace Easily.ES {

/// <summary>
/// @author Easily
/// </summary>
public class ESFunction : ESObject, IESFunction {

	private readonly ESDomain _domain;
	private readonly IFuncExpression _value;
	private readonly int _count;

	public object Target {
		get { return null; }
	}

	public ESFunction(ESDomain domain, IFuncExpression value) : this(domain, value, value.Count) {
		// ignored
	}

	public ESFunction(ESDomain domain, IFuncExpression value, int count) {
		_domain = domain;
		_value = value;
		_count = count;
	}

	public IESObject Invoke() {
		return Invoke(ToVirtuals(null, 0));
	}

	public IESObject Invoke(params object[] args) {
		return Invoke(ToVirtuals(args, _count));
	}

	public IESObject Invoke(IESObject[] args) {
		return _value.Invoke(_domain, args);
	}

	public override IESObject GetProperty(string name) {
		return GetProperty(_value, name);
	}

	public override string ToString() {
		return string.Format("ESFunction Domain: {0}, Value: {1}", _domain, _value);
	}

}

}