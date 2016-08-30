using System.Collections.Generic;

namespace Easily.ES {

	/// <summary>
	/// @author Easily
	/// </summary>
	internal class SyntaxReturn2 : Syntax {

		public SyntaxReturn2() {
			Add(new SyntaxMatchBody(new[] {typeof(MarkerRet), typeof(IExpressionRight)}));
		}

		public override void Parse(Parser parser, List<IExpression> list, ref int pos) {
			var item1 = list[pos + 1].Cast<IExpressionRight>();
			list.RemoveRange(pos, 2);
			list.Insert(pos, new ExpressionReturn(item1));
			pos -= 1;
		}

	}

}