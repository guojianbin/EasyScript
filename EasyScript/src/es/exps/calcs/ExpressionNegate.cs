using Easily.Bases;

namespace Easily.ES {

	/// <summary>
	/// @author Easily
	/// </summary>
	public class ExpressionNegate : Expression, IExpressionRight, IExpressionLogic {

		private readonly IExpressionRight _value;

		public ExpressionNegate(IExpressionRight value) {
			_value = value;
		}

		protected override void OnDispose() {
			base.OnDispose();
			_value.Dispose();
		}

		public override string ToString() {
			return string.Format("ExpressionNegate Value: {0}", _value);
		}

		public bool ToBoolean(ESContext context) {
			return Execute(context).ToBoolean();
		}

		public override IESObject Execute(ESContext context) {
			return GetValue(context);
		}

		public IESObject GetValue(ESContext context) {
			return new ESNumber(-_value.GetValue<IESNumber>(context).Value);
		}

		public override void Checking() {
			_value.Checking();
		}

	}

}