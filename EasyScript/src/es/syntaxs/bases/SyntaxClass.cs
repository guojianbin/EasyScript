using System.Collections.Generic;

namespace Easily.ES {

	/// <summary>
	/// @author Easily
	/// </summary>
	internal class SyntaxClass : Syntax {

		public SyntaxClass() {
			Add(new SyntaxMatchBody(new[] {typeof(MarkerClass), typeof(IExpressionName), typeof(ExpressionBB)}));
		}

		public override void Parse(Parser parser, List<IExpression> list, ref int pos) {
			var item1 = list[pos + 1].Cast<IExpressionName>();
			var item2 = list[pos + 2].Cast<ExpressionBB>();
			list.RemoveRange(pos, 3);
			parser.Parse(item2);
			list.Insert(pos, new ExpressionClass(item1.Name, item2));
		}

	}

}