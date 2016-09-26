using Easily.Bases;

namespace Easily.ES {

	/// <summary>
	/// @author Easily
	/// </summary>
	public class ExpressionMethod : Expression, IExpressionRight {

		private readonly IExpressionName _property;
		private readonly IExpressionRight _target;
		private readonly IExpressionNumber _count;

		public ExpressionMethod(IExpressionRight target, IExpressionName property, IExpressionNumber count) {
			_target = target;
			_property = property;
			_count = count;
		}

		public override IESObject Execute(ESContext context) {
			return GetValue(context);
		}

		public IESObject GetValue(ESContext context) {
			return _target.GetValue(context).GetMethod(_property.Name, ESUtility.ToInt32(_count.Value));
		}

		public override void Checking() {
			_target.Checking();
			_property.Checking();
			_count.Checking();
		}

		protected override void OnDispose() {
			base.OnDispose();
			_target.Dispose();
			_property.Dispose();
			_count.Dispose();
		}

		public override string ToString() {
			return string.Format("ExpressionMethod Property: {0}, Target: {1}, Count: {2}", _property, _target, _count);
		}

	}

}