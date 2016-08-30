using System.Collections.Generic;

namespace Easily.ES {

	/// <summary>
	/// @author Easily
	/// </summary>
	internal class SyntaxNEQ : Syntax {

		public SyntaxNEQ() {
			Add(new SyntaxMatchBody(new[] {typeof(IExpressionRight), typeof(MarkerEx), typeof(MarkerEq), typeof(IExpressionRight)}));
		}

		public override void Parse(Parser parser, List<IExpression> list, ref int pos) {
			var item1 = list[pos].Cast<IExpressionRight>();
			var item2 = list[pos + 3].Cast<IExpressionRight>();
			list.RemoveRange(pos, 4);
			list.Insert(pos, new ExpressionNEQ(item1, item2));
			pos -= 1;
		}

	}

}