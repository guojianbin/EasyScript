using Easily.Bases;

namespace Easily.ES {

	/// <summary>
	/// @author Easily
	/// </summary>
	public class ExpressionIf : Expression {

		private readonly IExpression _branch;
		private readonly IExpressionLogic _cond;

		public ExpressionIf(IExpressionLogic cond, IExpression branch) {
			_cond = cond;
			_branch = branch;
		}

		public override IESObject Execute(ESContext context) {
			return _cond.ToBoolean(context) ? _branch.Execute(context) : ESDefault.Value;
		}

		public override void Checking() {
			_cond.Checking();
			_branch.Checking();
		}

		protected override void OnDispose() {
			base.OnDispose();
			_cond.Dispose();
			_branch.Dispose();
		}

		public override string ToString() {
			return string.Format("ExpressionIf Logic: {0}, Branch: {1}", _cond, _branch);
		}

	}

}