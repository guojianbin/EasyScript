using System;

namespace Engine.ES {

/// <summary>
/// @author Easily
/// </summary>
public class ESString : ESObject, IESCollection, IESString, IESNumber, IESInteger {

	private readonly string _value;

	public int Count {
		get { return _value.Length; }
	}

	public string Value {
		set { throw new InvalidOperationException(ToString()); }
		get { return _value; }
	}

	float IESNumber.Value {
		set { throw new InvalidOperationException(ToString()); }
		get { return ToFloat(_value); }
	}

	int IESInteger.Value {
		set { throw new InvalidOperationException(ToString()); }
		get { return ToInt32(_value); }
	}

	public ESString(string value) {
		_value = value;
	}

	public override bool IsTrue() {
		return !string.IsNullOrEmpty(_value);
	}

	public override object ToObject() {
		return _value;
	}

	public override IESObject GetProperty(string name) {
		return GetProperty(_value, name);
	}

	public override string ToString() {
		return string.Format("ESString Value: {0}", _value);
	}

}

}