using System;
using System.Collections.Generic;
using Easily.Bases;

namespace Easily.ES {

	/// <summary>
	/// @author Easily
	/// </summary>
	public class ExpressionMM : ExpressionList {

		public ExpressionMM(List<IExpression> list) : base(list) {
			// ignored
		}

		public override IESObject Execute(ESContext context) {
			throw new InvalidOperationException(ToString());
		}

		public override string ToString() {
			return string.Format("ExpressionMM List: {0}", Count);
		}

	}

}