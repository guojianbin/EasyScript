using System.Collections.Generic;

namespace Easily.ES {

	/// <summary>
	/// @author Easily
	/// </summary>
	internal class SyntaxGT : Syntax {

		public SyntaxGT() {
			Add(new SyntaxMatchBody(new[] {typeof(IExpressionRight), typeof(MarkerGt), typeof(IExpressionRight)}));
		}

		public override void Parse(Parser parser, List<IExpression> list, ref int pos) {
			var item1 = list[pos].Cast<IExpressionRight>();
			var item2 = list[pos + 2].Cast<IExpressionRight>();
			list.RemoveRange(pos, 3);
			list.Insert(pos, new ExpressionGT(item1, item2));
			pos -= 1;
		}

	}

}