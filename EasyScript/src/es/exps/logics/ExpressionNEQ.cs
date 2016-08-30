using Easily.Bases;

namespace Easily.ES {

	/// <summary>
	/// @author Easily
	/// </summary>
	public class ExpressionNEQ : ExpressionLogic {

		private readonly IExpressionRight _value1;
		private readonly IExpressionRight _value2;

		public ExpressionNEQ(IExpressionRight value1, IExpressionRight value2) {
			_value1 = value1;
			_value2 = value2;
		}

		public override bool ToBoolean(ESContext context) {
			var v1 = _value1.GetValue(context).ToObject();
			var v2 = _value2.GetValue(context).ToObject();
			return !v1.Equals(v2);
		}

		public override void Checking() {
			_value1.Checking();
			_value2.Checking();
		}

		protected override void OnDispose() {
			base.OnDispose();
			_value1.Dispose();
			_value2.Dispose();
		}

		public override string ToString() {
			return string.Format("NEQ Value1: {0}, Value2: {1}", _value1, _value2);
		}

	}

}