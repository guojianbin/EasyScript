using System;

namespace Easily.ES {

	/// <summary>
	/// @author Easily
	/// </summary>
	public sealed class ESNumber : ESObject, IESString, IESNumber, IESInteger {

		private float _value;

		int IESInteger.Value {
			set { _value = value; }
			get { return ToInt32(_value); }
		}

		public float Value {
			set { _value = value; }
			get { return _value; }
		}

		string IESString.Value {
			set { _value = ToFloat(value); }
			get { return ToString(_value); }
		}

		public ESNumber(object value) : this(ToFloat(value)) {
			// ignored
		}

		public ESNumber(string value) : this(ToFloat(value)) {
			// ignored
		}

		public ESNumber(float value) {
			_value = value;
		}

		public override object ToObject() {
			return _value;
		}

		public override bool ToBoolean() {
			return Math.Abs(_value) > 0;
		}

		public override IESObject GetProperty(string name) {
			return GetProperty(_value, name);
		}

		public override IESObject GetMethod(string name, int count) {
			return GetMethod(_value, name, count);
		}

		public override IESObject Clone() {
			return new ESNumber(_value);
		}

		public override string ToString() {
			return string.Format("ESNumber Value: {0}", _value);
		}

	}

}