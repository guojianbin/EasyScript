using System;

namespace Easily.ES {

/// <summary>
/// @author Easily
/// </summary>
public class ESType : ESObject {

	private readonly Type _value;

	public Type Value {
		get { return _value; }
	}

	public ESType(Type value) {
		_value = value;
	}

	public override bool IsTrue() {
		return _value != null;
	}

	public override object ToObject() {
		return _value;
	}

	public override IESObject GetProperty(string name) {
		return GetProperty(_value, name);
	}

	public override string ToString() {
		return string.Format("ESType Value: {0}", _value);
	}

}

}