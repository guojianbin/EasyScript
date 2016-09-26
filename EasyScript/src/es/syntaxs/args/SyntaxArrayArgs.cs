using System.Collections.Generic;

namespace Easily.ES {

	/// <summary>
	/// @author Easily
	/// </summary>
	internal class SyntaxArrayArgs : Syntax {

		public SyntaxArrayArgs() {
			Add(new SyntaxMatchSplit(typeof(CharterCMA), new[] { typeof(IExpressionRight) }));
		}

		public override void Parse(Parser parser, List<IExpression> list, ref int pos) {
			var args = new List<IExpressionRight>();
			Parse(list, 0, args);
			list.Clear();
			list.Add(new ExpressionArrayArgs(args));
		}

		public static void Parse(List<IExpression> list, int pos, List<IExpressionRight> result) {
			if (list.Count - pos == 1) {
				result.Add(list[pos].Cast<IExpressionRight>());
			} else if (list.Count - pos > 1) {
				result.Add(list[pos].Cast<IExpressionRight>());
				Parse(list, pos + 2, result);
			}
		}

	}

}