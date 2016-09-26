using System;
using System.Collections;

namespace Easily.ES {

	/// <summary>
	/// @author Easily
	/// </summary>
	public sealed class ESString : ESObject, IESCollection, IESString, IESNumber, IESInteger {

		private readonly string _value;

		public int Count {
			get { return _value.Length; }
		}

		int IESInteger.Value {
			set { throw new InvalidOperationException(ToString()); }
			get { return ToInt32(_value); }
		}

		float IESNumber.Value {
			set { throw new InvalidOperationException(ToString()); }
			get { return ToFloat(_value); }
		}

		public string Value {
			set { throw new InvalidOperationException(ToString()); }
			get { return _value; }
		}

		public ESString(string value) {
			_value = value;
		}

		public override bool ToBoolean() {
			return !string.IsNullOrEmpty(_value);
		}

		public override object ToObject() {
			return _value;
		}

		public override IESObject GetProperty(string name) {
			return GetProperty(_value, name);
		}

		public override IESObject GetMethod(string name, int count) {
			return GetMethod(_value, name, count);
		}

		public IEnumerator GetEnumerator() {
			throw new InvalidOperationException(ToString());
		}

		public override string ToString() {
			return string.Format("ESString Value: {0}", _value);
		}

	}

}