using System.Collections.Generic;

namespace Easily.ES {

	/// <summary>
	/// @author Easily
	/// </summary>
	internal class SyntaxStringArgs : Syntax {

		public SyntaxStringArgs() {
			Add(new SyntaxMatchSplit(typeof(CharterCMA), new[] { typeof(IExpressionName) }));
		}

		public override void Parse(Parser parser, List<IExpression> list, ref int pos) {
			var args = new List<string>();
			Parse(list, 0, args);
			list.Clear();
			list.Add(new ExpressionStringArgs(args));
		}

		public static void Parse(List<IExpression> list, int pos, List<string> result) {
			if (list.Count - pos == 1) {
				result.Add(list[pos].Cast<IExpressionName>().Name);
			} else if (list.Count - pos > 1) {
				result.Add(list[pos].Cast<IExpressionName>().Name);
				Parse(list, pos + 2, result);
			}
		}

	}

}