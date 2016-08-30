using System.Collections;

namespace Easily.ES {

	/// <summary>
	/// @author Easily
	/// </summary>
	public sealed class ESKey : ESObject, IESIndex, IESProperty, IESString, IESNumber, IESInteger, IESFunction, IESEnumerable {

		private readonly string _key;
		private readonly IESTable _table;

		public object Target {
			get { return _table; }
		}

		int IESInteger.Value {
			set { SetValue(new ESNumber(value)); }
			get { return ToInt32(ToObject()); }
		}

		float IESNumber.Value {
			set { SetValue(new ESNumber(value)); }
			get { return ToFloat(ToObject()); }
		}

		string IESString.Value {
			set { SetValue(new ESString(value)); }
			get { return ToString(ToObject()); }
		}

		public ESKey(IESTable table, string key) {
			_table = table;
			_key = key;
		}

		public T GetValue<T>() {
			return (T)GetValue();
		}

		public IEnumerator GetEnumerator() {
			return ToObject<IEnumerable>().GetEnumerator();
		}

		public IESObject Invoke(IESObject[] args) {
			return GetValue<IESFunction>().Invoke(args);
		}

		IESObject IESIndex.this[IESObject index] {
			get { return GetValue<IESIndex>()[index]; }
			set { GetValue<IESIndex>()[index] = value; }
		}

		public override object ToObject() {
			return GetValue().ToObject();
		}

		public override IESObject GetProperty(string name) {
			return GetValue().GetProperty(name);
		}

		public override bool ToBoolean() {
			return GetValue().ToBoolean();
		}

		public void SetValue(IESObject value) {
			_table[_key] = value;
		}

		public void SetValue(object value) {
			SetValue(ToVirtual(value));
		}

		public IESObject GetValue() {
			return _table[_key];
		}

		public override string ToString() {
			return string.Format("ESKey Key: {0}, Table: {1}", _key, _table);
		}

	}

}