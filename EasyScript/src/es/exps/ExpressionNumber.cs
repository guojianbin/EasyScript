using Easily.Bases;

namespace Easily.ES {

	/// <summary>
	/// @author Easily
	/// </summary>
	public class ExpressionNumber : Expression, IExpressionNumber, IExpressionRight, IExpressionLogic, IExpressionName {

		private readonly ESNumber _value;

		public string Name {
			get { return _value.GetString(); }
		}

		public float Value {
			get { return _value.GetNumber(); }
		}

		public ExpressionNumber(string str) {
			_value = new ESNumber(str);
		}

		public ExpressionNumber(float value) {
			_value = new ESNumber(value);
		}

		public bool ToBoolean(ESContext context) {
			return _value.ToBoolean();
		}

		public override IESObject Execute(ESContext context) {
			return _value;
		}

		public IESObject GetValue(ESContext context) {
			return _value;
		}

		protected override void OnDispose() {
			base.OnDispose();
			_value.Dispose();
		}

		public override string ToString() {
			return string.Format("ExpressionNumber Value: {0}", _value);
		}

	}

}