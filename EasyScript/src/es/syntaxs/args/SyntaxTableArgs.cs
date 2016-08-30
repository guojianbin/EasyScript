using System.Collections.Generic;

namespace Easily.ES {

	/// <summary>
	/// @author Easily
	/// </summary>
	internal class SyntaxTableArgs : Syntax {

		public SyntaxTableArgs() {
			Add(new SyntaxMatchSplit(typeof(MarkerCma), new[] {typeof(IExpressionName), typeof(MarkerCol), typeof(IExpressionRight)}));
		}

		public override void Parse(Parser parser, List<IExpression> list, ref int pos) {
			var args = new Dictionary<string, IExpressionRight>();
			Parse(list, 0, args);
			list.Clear();
			list.Add(new ExpressionTableArgs(args));
		}

		public static void Parse(List<IExpression> list, int pos, Dictionary<string, IExpressionRight> result) {
			if (list.Count - pos == 3) {
				result.Add(list[pos].Cast<IExpressionName>().Name, list[pos + 2].Cast<IExpressionRight>());
			} else if (list.Count - pos > 3) {
				result.Add(list[pos].Cast<IExpressionName>().Name, list[pos + 2].Cast<IExpressionRight>());
				Parse(list, pos + 4, result);
			}
		}

	}

}