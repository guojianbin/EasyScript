using System;
using System.Collections;
using System.Reflection;

namespace Easily.ES {

	/// <summary>
	/// @author Easily
	/// </summary>
	public sealed class ESProperty : ESObject, IESProperty, IESString, IESNumber, IESInteger, IESEnumerable {

		private readonly object _target;
		private readonly PropertyInfo _value;

		public object Target {
			get { return _target; }
		}

		int IESInteger.Value {
			set { SetValue(value); }
			get { return ToInt32(ToObject()); }
		}

		float IESNumber.Value {
			set { SetValue(value); }
			get { return ToFloat(ToObject()); }
		}

		string IESString.Value {
			set { SetValue(value); }
			get { return ToString(ToObject()); }
		}

		public ESProperty(PropertyInfo value) {
			_value = value;
		}

		public ESProperty(object target, PropertyInfo value) {
			_target = target;
			_value = value;
		}

		public IEnumerator GetEnumerator() {
			return ToObject<IEnumerable>().GetEnumerator();
		}

		public void SetValue(IESObject value) {
			SetValue(value.ToObject());
		}

		public IESObject GetValue() {
			return ToVirtual(ToObject());
		}

		public void SetValue(object value) {
			try {
				_value.SetValue(_target, value, null);
			} catch (Exception e) {
				throw new ArgumentException(value.ToString(), e);
			}
		}

		public override object ToObject() {
			try {
				return _value.GetValue(_target, null);
			} catch (Exception e) {
				throw new InvalidOperationException(ToString(), e);
			}
		}

		public override IESObject GetProperty(string name) {
			return GetProperty(ToObject(), name);
		}

		public override bool ToBoolean() {
			return ToObject() != null;
		}

		public override string ToString() {
			return string.Format("ESProperty Target: {0}, Value: {1}", _target, _value);
		}

	}

}