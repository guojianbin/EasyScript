using Easily.Bases;

namespace Easily.ES {

	/// <summary>
	/// @author Easily
	/// </summary>
	public class ExpressionBind : Expression, IExpressionRight {

		private readonly IExpressionLeft _lvalue;
		private readonly IExpressionRight _rvalue;

		public IExpressionLeft LValue {
			get { return _lvalue; }
		}

		public IExpressionRight RValue {
			get { return _rvalue; }
		}

		public ExpressionBind(IExpressionLeft lvalue, IExpressionRight rvalue) {
			_lvalue = lvalue;
			_rvalue = rvalue;
		}

		public override IESObject Execute(ESContext context) {
			return GetValue(context);
		}

		public IESObject GetValue(ESContext context) {
			var value = _rvalue.GetValue(context);
			_lvalue.SetValue(context, value.Clone());
			return value;
		}

		public override void Checking() {
			_lvalue.Checking();
			_rvalue.Checking();
		}

		protected override void OnDispose() {
			base.OnDispose();
			_lvalue.Dispose();
			_rvalue.Dispose();
		}

		public override string ToString() {
			return string.Format("ExpressionBind Left: {0}, Right: {1}", _lvalue, _rvalue);
		}

	}

}