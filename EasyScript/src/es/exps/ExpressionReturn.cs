using Easily.Bases;

namespace Easily.ES {

	/// <summary>
	/// @author Easily
	/// </summary>
	public class ExpressionReturn : Expression {

		private readonly IExpressionRight _value;

		public ExpressionReturn() {
			// ignored
		}

		public ExpressionReturn(IExpressionRight value) {
			_value = value;
		}

		public override IESObject Execute(ESContext context) {
			context.IsReturn = true;
			if (_value != null) {
				return _value.GetValue(context);
			} else {
				return ESDefault.Value;
			}
		}

		public override void Checking() {
			_value.Checking();
		}

		protected override void OnDispose() {
			base.OnDispose();
			_value.Dispose();
		}

		public override string ToString() {
			return string.Format("ExpressionReturn Value: {0}", _value);
		}

	}

}