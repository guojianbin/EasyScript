using System;
using System.Collections;

namespace Easily.ES {

	/// <summary>
	/// @author Easily
	/// </summary>
	public sealed class ESSprite : ESObject, IESCollection, IESString, IESNumber, IESInteger {

		private object _value;

		public object Value {
			get { return _value; }
		}

		int IESCollection.Count {
			get { return GetValue<ICollection>().Count; }
		}

		int IESInteger.Value {
			set { _value = value; }
			get { return ToInt32(_value); }
		}

		float IESNumber.Value {
			set { _value = value; }
			get { return ToFloat(_value); }
		}

		string IESString.Value {
			set { _value = value; }
			get { return ToString(_value); }
		}

		public ESSprite(object value) {
			_value = value;
		}

		public T GetValue<T>() {
			try {
				return (T)_value;
			} catch (Exception e) {
				throw new InvalidOperationException(ToString(), e);
			}
		}

		public override bool ToBoolean() {
			return _value != null;
		}

		public override object ToObject() {
			return _value;
		}

		public override IESObject GetProperty(string name) {
			return GetProperty(_value, name);
		}

		public IEnumerator GetEnumerator() {
			return GetValue<IEnumerable>().GetEnumerator();
		}

		public override string ToString() {
			return string.Format("ESSprite Value: {0}", _value);
		}

	}

}