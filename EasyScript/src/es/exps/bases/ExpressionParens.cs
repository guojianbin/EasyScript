using System;
using System.Collections.Generic;
using Easily.Bases;

namespace Easily.ES {

	/// <summary>
	/// @author Easily
	/// </summary>
	public class ExpressionParens : ExpressionList, IExpressionRight {

		public ExpressionParens(List<IExpression> list) : base(list) {
			// ignored
		}

		public IESObject GetValue(ESContext context) {
			if (Count == 1) {
				return this[0].Execute(context);
			} else {
				throw new InvalidOperationException("Count != 1");
			}
		}

		public override string ToString() {
			return string.Format("ExpressionParens Count: {0}", Count);
		}

	}

}