using System;
using System.Collections.Generic;
using Easily.Bases;

namespace Easily.ES {

	/// <summary>
	/// @author Easily
	/// </summary>
	public class ExpressionSS : ExpressionList, IExpressionRight {

		public ExpressionSS(List<IExpression> list) : base(list) {
			// ignored
		}

		public override string ToString() {
			return string.Format("ExpressionSS List: {0}", Count);
		}

		public IESObject GetValue(ESContext context) {
			if (Count == 1) {
				return this[0].Execute(context);
			} else {
				throw new InvalidOperationException("Count not eq 1");
			}
		}

	}

}