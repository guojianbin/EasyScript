namespace Easily.ES {

	/// <summary>
	/// @author Easily
	/// </summary>
	public sealed class ESBoolean : ESObject {

		private readonly bool _value;

		public bool Value {
			get { return _value; }
		}

		public ESBoolean(bool value) {
			_value = value;
		}

		public override object ToObject() {
			return _value;
		}

		public override bool ToBoolean() {
			return _value;
		}

		public override IESObject GetProperty(string name) {
			return GetProperty(_value, name);
		}

		public override IESObject GetMethod(string name, int count) {
			return GetMethod(_value, name, count);
		}

		public override IESObject Clone() {
			return new ESBoolean(_value);
		}

		public override string ToString() {
			return _value.ToString();
		}

	}

}