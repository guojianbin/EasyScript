using System.Collections.Generic;

namespace Easily.ES {

	/// <summary>
	/// @author Easily
	/// </summary>
	internal class SyntaxLambda : SyntaxMany {

		public SyntaxLambda() {
			var item = new SyntaxNotifier(OnParse);
			item.Add(new SyntaxMatchBody(new[] { typeof(IExpressionName), typeof(CharterEQ), typeof(CharterGT), typeof(IExpressionRight) }));
			Add(item);

			var item2 = new SyntaxNotifier(OnParse2);
			item2.Add(new SyntaxMatchBody(new[] { typeof(ExpressionParens), typeof(CharterEQ), typeof(CharterGT), typeof(IExpressionRight) }));
			Add(item2);
		}

		public static void OnParse(Parser parser, List<IExpression> list, ref int pos) {
			var item1 = list[pos].Cast<IExpressionName>();
			var item2 = list[pos + 3].Cast<IExpressionRight>();
			list.RemoveRange(pos, 4);
			list.Insert(pos, new ExpressionFunc(new ExpressionStringArgs(item1.Name), new ExpressionReturn(item2)));
			pos -= 1;
		}

		public static void OnParse2(Parser parser, List<IExpression> list, ref int pos) {
			var item1 = list[pos].Cast<ExpressionParens>();
			var item2 = list[pos + 3].Cast<IExpressionRight>();
			list.RemoveRange(pos, 4);
			parser.Parse(ParseLevel.STRING_ARGS, item1);
			list.Insert(pos, new ExpressionFunc(ESUtility.ToStringArgs(item1), new ExpressionReturn(item2)));
			pos -= 1;
		}

	}

}