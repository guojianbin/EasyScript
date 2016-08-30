using Easily.Bases;

namespace Easily.ES {

	/// <summary>
	/// @author Easily
	/// </summary>
	public class ExpressionProperty : Expression, IExpressionRight, IExpressionLogic, IExpressionLeft {

		private readonly IExpressionName _property;
		private readonly IExpressionRight _target;

		public ExpressionProperty(IExpressionRight target, IExpressionName property) {
			_target = target;
			_property = property;
		}

		private IESProperty GetProperty(ESContext context) {
			return Execute(context).Cast<IESProperty>();
		}

		public void SetValue(ESContext context, IESObject value) {
			GetProperty(context).SetValue(value);
		}

		public bool ToBoolean(ESContext context) {
			return GetProperty(context).GetValue().ToBoolean();
		}

		public override IESObject Execute(ESContext context) {
			return GetValue(context);
		}

		public IESObject GetValue(ESContext context) {
			return _target.GetValue(context).GetProperty(_property.Name);
		}

		public override void Checking() {
			_target.Checking();
			_property.Checking();
		}

		protected override void OnDispose() {
			base.OnDispose();
			_target.Dispose();
			_property.Dispose();
		}

		public override string ToString() {
			return string.Format("ExpressionProperty Target: {0}, Name: {1}", _target, _property);
		}

	}

}