using System.Collections.Generic;

namespace Easily.ES {

	/// <summary>
	/// @author Easily
	/// </summary>
	internal class SyntaxMethod : Syntax {

		public SyntaxMethod() {
			Add(new SyntaxMatchBody(new[] { typeof(IExpressionRight), typeof(CharterDOT), typeof(IExpressionName), typeof(CharterRDO), typeof(IExpressionNumber) }));
		}

		public override void Parse(Parser parser, List<IExpression> list, ref int pos) {
			var item1 = list[pos].Cast<IExpressionRight>();
			var item2 = list[pos + 2].Cast<IExpressionName>();
			var item3 = list[pos + 4].Cast<IExpressionNumber>();
			list.RemoveRange(pos, 5);
			list.Insert(pos, new ExpressionMethod(item1, item2, item3));
			pos -= 1;
		}

	}

}