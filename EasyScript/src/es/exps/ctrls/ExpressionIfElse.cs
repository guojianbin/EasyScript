using Easily.Bases;

namespace Easily.ES {

	/// <summary>
	/// @author Easily
	/// </summary>
	public class ExpressionIfElse : Expression {

		private readonly IExpression _branch1;
		private readonly IExpression _branch2;
		private readonly IExpressionLogic _cond;

		public ExpressionIfElse(IExpressionLogic cond, IExpression branch1, IExpression branch2) {
			_cond = cond;
			_branch1 = branch1;
			_branch2 = branch2;
		}

		public override IESObject Execute(ESContext context) {
			if (_cond.ToBoolean(context)) {
				return _branch1.Execute(context);
			} else {
				return _branch2.Execute(context);
			}
		}

		public override void Checking() {
			_cond.Checking();
			_branch1.Checking();
			_branch2.Checking();
		}

		protected override void OnDispose() {
			base.OnDispose();
			_cond.Dispose();
			_branch1.Dispose();
			_branch2.Dispose();
		}

		public override string ToString() {
			return string.Format("ExpressionIfElse Logic: {0}, Branch1: {1}, Branch2: {2}", _cond, _branch1, _branch2);
		}

	}

}