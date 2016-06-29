using System;
using System.Collections;
using System.Reflection;

namespace Engine.ES {

/// <summary>
/// @author Easily
/// </summary>
public class ESField : ESObject, IESProperty, IESString, IESNumber, IESInteger, IESEnumerable {

	private readonly object _target;
	private readonly FieldInfo _value;

	float IESNumber.Value {
		set { SetValue(value); }
		get { return ToFloat(ToObject()); }
	}

	int IESInteger.Value {
		set { SetValue(value); }
		get { return ToInt32(ToObject()); }
	}

	string IESString.Value {
		set { SetValue(value); }
		get { return ToString(ToObject()); }
	}

	public object Target {
		get { return _target; }
	}

	public ESField(FieldInfo value) {
		_value = value;
	}

	public ESField(object target, FieldInfo value) {
		_target = target;
		_value = value;
	}

	public void SetValue(IESObject value) {
		SetValue(value.ToObject());
	}

	public IESObject GetValue() {
		return ToVirtual(ToObject());
	}

	public IEnumerator GetEnumerator() {
		return ToObject<IEnumerable>().GetEnumerator();
	}

	public void SetValue(object value) {
		try {
			_value.SetValue(_target, value);
		} catch (Exception e) {
			throw new ArgumentException(value.ToString(), e);
		}
	}

	public override object ToObject() {
		try {
			return _value.GetValue(_target);
		} catch (Exception e) {
			throw new InvalidOperationException(ToString(), e);
		}
	}

	public override IESObject GetProperty(string name) {
		return GetProperty(ToObject(), name);
	}

	public override bool IsTrue() {
		return ToObject() != null;
	}

	public override string ToString() {
		return string.Format("ESField Target: {0}, Value: {1}", _target, _value);
	}

}

}