using Easily.Bases;

namespace Easily.ES {

	/// <summary>
	/// @author Easily
	/// </summary>
	public class ExpressionWhile : Expression {

		private readonly IExpressionLogic _cond;
		private readonly IExpression _entry;

		public ExpressionWhile(IExpressionLogic cond, IExpression entry) {
			_cond = cond;
			_entry = entry;
		}

		public override IESObject Execute(ESContext context) {
			var ret = ESDefault.Value;
			while (true) {
				if (_cond.ToBoolean(context)) {
					ret = _entry.Execute(context);
				} else {
					return ret;
				}
				if (context.IsBreak) {
					context.IsBreak = false;
					return ret;
				}
				if (context.IsReturn) {
					return ret;
				}
			}
		}

		public override void Checking() {
			_cond.Checking();
			_entry.Checking();
		}

		protected override void OnDispose() {
			base.OnDispose();
			_cond.Dispose();
			_entry.Dispose();
		}

		public override string ToString() {
			return string.Format("ExpressionWhile Condition: {0}, Body: {1}", _cond, _entry);
		}

	}

}