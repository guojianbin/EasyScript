using System.Collections.Generic;

namespace Easily.ES {

	/// <summary>
	/// @author Easily
	/// </summary>
	internal class SyntaxBind : Syntax {

		public SyntaxBind() {
			Add(new SyntaxMatchBody(new[] { typeof(IExpressionLeft), typeof(CharterEQ), typeof(IExpressionRight) }));
		}

		public override void Parse(Parser parser, List<IExpression> list, ref int pos) {
			var item1 = list[pos].Cast<IExpressionLeft>();
			var item2 = list[pos + 2].Cast<IExpressionRight>();
			list.RemoveRange(pos, 3);
			list.Insert(pos, new ExpressionBind(item1, item2));
			pos -= 1;
		}

	}

}