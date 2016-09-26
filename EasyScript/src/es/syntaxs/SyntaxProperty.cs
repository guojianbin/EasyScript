using System.Collections.Generic;

namespace Easily.ES {

	/// <summary>
	/// @author Easily
	/// </summary>
	internal class SyntaxProperty : Syntax {

		public SyntaxProperty() {
			Add(new SyntaxMatchBody(new[] { typeof(IExpressionRight), typeof(CharterDOT), typeof(IExpressionName) }));
		}

		public override void Parse(Parser parser, List<IExpression> list, ref int pos) {
			var item1 = list[pos].Cast<IExpressionRight>();
			var item2 = list[pos + 2].Cast<IExpressionName>();
			list.RemoveRange(pos, 3);
			list.Insert(pos, new ExpressionProperty(item1, item2));
			pos -= 1;
		}

	}

}