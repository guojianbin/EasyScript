using System;

namespace Engine.ES {

/// <summary>
/// @author Easily
/// </summary>
public class ESPuppet : ESObject, IESString, IESNumber, IESInteger, IESProperty {

	private readonly IESProperty _property;
	private readonly Func<object, object> _convert;

	private object _value {
		get { return _property.ToObject(); } 
		set { _property.SetValue(_convert(value)); }
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

	public ESPuppet(object target, string name) {
		_property = GetProperty(target, name).Cast<IESProperty>();
		_convert = GetConvert(GetType(target, name));
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
		return string.Format("ESPuppet Value: {0}", _value);
	}

}

}