using Easily.Bases;

namespace Easily.ES {

	/// <summary>
	/// @author Easily
	/// </summary>
	public class ExpressionNot : ExpressionLogic {

		private readonly IExpressionLogic _target;

		public ExpressionNot(IExpressionLogic target) {
			_target = target;
		}

		public override bool ToBoolean(ESContext context) {
			return !_target.ToBoolean(context);
		}

		public override void Checking() {
			_target.Checking();
		}

		protected override void OnDispose() {
			base.OnDispose();
			_target.Dispose();
		}

		public override string ToString() {
			return string.Format("Not Target: {0}", _target);
		}

	}

}