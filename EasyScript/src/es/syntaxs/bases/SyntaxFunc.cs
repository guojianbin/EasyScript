using System.Collections.Generic;

namespace Easily.ES {

	/// <summary>
	/// @author Easily
	/// </summary>
	internal class SyntaxFunc : SyntaxMany {

		public SyntaxFunc() {
			var item = new SyntaxNotifier(OnParse);
			item.Add(new SyntaxMatchBody(new[] {typeof(MarkerFunc), typeof(IExpressionName), typeof(ExpressionSS), typeof(ExpressionBB)}));
			Add(item);

			var item2 = new SyntaxNotifier(OnParse2);
			item2.Add(new SyntaxMatchBody(new[] {typeof(MarkerFunc), typeof(ExpressionSS), typeof(ExpressionBB)}));
			Add(item2);
		}

		public static void OnParse(Parser parser, List<IExpression> list, ref int pos) {
			var item1 = list[pos + 1].Cast<IExpressionName>();
			var item2 = list[pos + 2].Cast<ExpressionSS>();
			var item3 = list[pos + 3].Cast<ExpressionBB>();
			list.RemoveRange(pos, 4);
			parser.Parse(ParseLevel.STRING_ARGS, item2);
			parser.Parse(item3);
			list.Insert(pos, new ExpressionFunc(item1.Name, ESUtility.ToStringArgs(item2), item3.Unbound()));
			pos -= 1;
		}

		public static void OnParse2(Parser parser, List<IExpression> list, ref int pos) {
			var item1 = list[pos + 1].Cast<ExpressionSS>();
			var item2 = list[pos + 2].Cast<ExpressionBB>();
			list.RemoveRange(pos, 3);
			parser.Parse(ParseLevel.STRING_ARGS, item1);
			parser.Parse(item2);
			list.Insert(pos, new ExpressionFunc(ESUtility.ToStringArgs(item1), item2.Unbound()));
			pos -= 1;
		}

	}

}