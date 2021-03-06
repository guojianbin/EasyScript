using System.Collections.Generic;

namespace Easily.ES {

	/// <summary>
	/// @author Easily
	/// </summary>
	internal class SyntaxForEach : Syntax {

		public SyntaxForEach() {
			Add(new SyntaxMatchBody(new[] { typeof(CharterForEach), typeof(ExpressionParens), typeof(ExpressionBraces) }));
		}

		public override void Parse(Parser parser, List<IExpression> list, ref int pos) {
			var item1 = list[pos + 1].Cast<ExpressionParens>();
			var item2 = list[pos + 2].Cast<ExpressionBraces>();
			list.RemoveRange(pos, 3);
			parser.Parse(item1);
			parser.Parse(item2);
			var index = item1[0].Cast<IExpressionName>();
			var value = item1[2].Cast<IExpressionRight>();
			list.Insert(pos, new ExpressionForEach(index, value, item2));
		}

	}

}