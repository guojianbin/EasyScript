using System;
using Easily.Bases;

namespace Easily.ES {

	/// <summary>
	/// @author Easily
	/// </summary>
	public class ExpressionMath : Expression, IExpressionRight, IExpressionLogic {

		protected readonly IExpressionRight _value1;
		protected readonly IExpressionRight _value2;

		public ExpressionMath(IExpressionRight value1, IExpressionRight value2) {
			_value1 = value1;
			_value2 = value2;
		}

		public override IESObject Execute(ESContext context) {
			return GetValue(context);
		}

		public virtual IESObject GetValue(ESContext context) {
			throw new InvalidOperationException(ToString());
		}

		public override void Checking() {
			_value1.Checking();
			_value2.Checking();
		}

		public bool ToBoolean(ESContext context) {
			return Execute(context).ToBoolean();
		}

		protected override void OnDispose() {
			base.OnDispose();
			_value1.Dispose();
			_value2.Dispose();
		}

		public override string ToString() {
			return string.Format("ExpressionMath Value1: {0}, Value2: {1}", _value1, _value2);
		}

	}

}