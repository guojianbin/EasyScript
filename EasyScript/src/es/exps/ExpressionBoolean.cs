using Easily.Bases;

namespace Easily.ES {

	/// <summary>
	/// @author Easily
	/// </summary>
	public class ExpressionBoolean : Expression, IExpressionRight, IExpressionLogic {

		private readonly ESBoolean _value;

		public ExpressionBoolean(bool value) {
			_value = new ESBoolean(value);
		}

		protected override void OnDispose() {
			base.OnDispose();
			_value.Dispose();
		}

		public override string ToString() {
			return string.Format("ExpressionBoolean Value: {0}", _value);
		}

		public bool ToBoolean(ESContext context) {
			return _value.Value;
		}

		public override IESObject Execute(ESContext context) {
			return GetValue(context);
		}

		public IESObject GetValue(ESContext context) {
			return _value;
		}

	}

}