using System.Collections.Generic;
using Easily.Bases;

namespace Easily.ES {

	/// <summary>
	/// @author Easily
	/// </summary>
	public class ExpressionBraces : ExpressionList {

		public ExpressionBraces(List<IExpression> list) : base(list) {
			// ignored
		}

		public override IESObject Execute(ESContext context) {
			var ret = ESDefault.Value;
			var index = 0;
			while (true) {
				if (index < Count) {
					ret = this[index].Execute(context);
				} else {
					return ret;
				}
				if (context.IsReturn) {
					return ret;
				} else {
					index += 1;
				}
			}
		}

		public override string ToString() {
			return string.Format("ExpressionBraces List: {0}", Count);
		}

	}

}