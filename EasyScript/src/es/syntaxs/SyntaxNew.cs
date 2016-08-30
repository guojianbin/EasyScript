using System.Collections.Generic;

namespace Easily.ES {

	/// <summary>
	/// @author Easily
	/// </summary>
	internal class SyntaxNew : Syntax {

		public SyntaxNew() {
			Add(new SyntaxMatchBody(new[] {typeof(MarkerNew), typeof(IExpressionName), typeof(ExpressionSS)}));
		}

		public override void Parse(Parser parser, List<IExpression> list, ref int pos) {
			var item1 = list[pos + 1].Cast<IExpressionName>();
			var item2 = list[pos + 2].Cast<ExpressionSS>();
			list.RemoveRange(pos, 3);
			parser.Parse(item2);
			parser.Parse(ParseLevel.ARRAY_ARGS, item2);
			list.Insert(pos, new ExpressionNew(item1.Name, ESUtility.ToArrayArgs(item2)));
			pos -= 1;
		}

	}

}