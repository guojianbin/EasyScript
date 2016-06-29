using System;

namespace Easily.ES {

/// <summary>
/// @author Easily
/// </summary>
public class ESProxy : ESObject, IESString, IESNumber, IESInteger, IESProperty {

	private readonly Func<object> _getValue;
	private readonly Action<object> _setValue;

	private object _value {
		get { return _getValue(); } 
		set { _setValue(value); }
	}

	public object Target {
		get { return _value; }
	}

	int IESInteger.Value {
		set { _value = value; }
		get { return ToInt32(_value); }
	}

	string IESString.Value {
		set { _value = value; }
		get { return ToString(_value); }
	}

	float IESNumber.Value {
		set { _value = value; }
		get { return ToFloat(_value); }
	}

	public ESProxy(Func<object> getValue, Action<object> setValue) {
		_getValue = getValue;
		_setValue = setValue;
	}

	public void SetValue(IESObject value) {
		_value = value.ToObject();
	}

	public void SetValue(object value) {
		_value = value;
	}

	public IESObject GetValue() {
		return ToVirtual(_value);
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
		return string.Format("ESProxy Value: {0}", _value);
	}

}

}