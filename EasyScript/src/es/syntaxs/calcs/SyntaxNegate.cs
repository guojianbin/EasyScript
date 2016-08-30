using System.Collections.Generic;

namespace Easily.ES {

	/// <summary>
	/// @author Easily
	/// </summary>
	internal class SyntaxNegate : Syntax {

		public SyntaxNegate() {
			Add(new SyntaxMatchBound(typeof(IExpressionRight)));
			Add(new SyntaxMatchBody(new[] {typeof(MarkerSub), typeof(IExpressionRight)}));
		}

		public override void Parse(Parser parser, List<IExpression> list, ref int pos) {
			var item1 = list[pos + 1].Cast<IExpressionRight>();
			list.RemoveRange(pos, 2);
			list.Insert(pos, new ExpressionNegate(item1));
			pos -= 1;
		}

	}

}