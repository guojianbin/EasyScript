using Easily.Bases;

namespace Easily.ES {

	/// <summary>
	/// @author Easily
	/// </summary>
	public class ExpressionIndex : Expression, IExpressionRight, IExpressionLogic, IExpressionLeft {

		private readonly IExpressionRight _target;
		private readonly IExpressionRight _value;

		public ExpressionIndex(IExpressionRight target, IExpressionRight value) {
			_target = target;
			_value = value;
		}

		protected override void OnDispose() {
			base.OnDispose();
			_target.Dispose();
			_value.Dispose();
		}

		public override string ToString() {
			return string.Format("ExpressionIndex Target: {0}, Name: {1}", _target, _value);
		}

		public void SetValue(ESContext context, IESObject value) {
			_target.GetValue<IESIndex>(context)[_value.GetValue(context)] = value;
		}

		public bool ToBoolean(ESContext context) {
			return Execute(context).ToBoolean();
		}

		public override IESObject Execute(ESContext context) {
			return GetValue(context);
		}

		public IESObject GetValue(ESContext context) {
			return _target.GetValue<IESIndex>(context)[_value.GetValue(context)];
		}

		public override void Checking() {
			_target.Checking();
			_value.Checking();
		}

	}

}