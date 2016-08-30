using Easily.Bases;
using Easily.Utility;

namespace Easily.ES {

	/// <summary>
	/// @author Easily
	/// </summary>
	public class ExpressionInvoke : Expression, IExpressionRight, IExpressionLogic {

		private readonly ExpressionArrayArgs _args;
		private readonly IExpressionRight _func;

		public ExpressionInvoke(IExpressionRight func, ExpressionArrayArgs args) {
			_func = func;
			_args = args;
		}

		public bool ToBoolean(ESContext context) {
			return Execute(context).ToBoolean();
		}

		public override IESObject Execute(ESContext context) {
			return _func.GetValue<IESFunction>(context).Invoke(ESUtility.ToArray(context, _args));
		}

		public IESObject GetValue(ESContext context) {
			return Execute(context);
		}

		public override void Checking() {
			_func.Checking();
			_args.ForEach(Checking);
		}

		protected override void OnDispose() {
			base.OnDispose();
			_func.Dispose();
		}

		public override string ToString() {
			return string.Format("ExpressionInvoke Func: {0}, Args: {1}", _func, _args.Count);
		}

	}

}