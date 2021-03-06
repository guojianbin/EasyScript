﻿using System.Collections.Generic;
using Easily.Bases;

namespace Easily.ES {

	/// <summary>
	/// @author Easily
	/// </summary>
	public class ExpressionModule : ExpressionList {

		public ExpressionModule(List<IExpression> list) : base(list) {
			// ignored
		}

		public override IESObject Execute(ESContext context) {
			var ret = ESDefault.Value;
			var index = 0;
			while (true) {
				if (index < Count) {
					ret = this[index].Execute(context);
					index += 1;
				} else {
					return ret;
				}
			}
		}

		public override string ToString() {
			return string.Format("ExpressionModule List: {0}", Count);
		}

	}

}