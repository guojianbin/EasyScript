using System;
using Easily.Bases;

namespace Easily.ES {

	/// <summary>
	/// @author Easily
	/// </summary>
	public class ExpressionLogic : Expression, IExpressionLogic, IExpressionRight {

		public override IESObject Execute(ESContext context) {
			return GetValue(context);
		}

		public virtual bool ToBoolean(ESContext context) {
			throw new InvalidOperationException(ToString());
		}

		public IESObject GetValue(ESContext context) {
			return new ESBoolean(ToBoolean(context));
		}

	}

}