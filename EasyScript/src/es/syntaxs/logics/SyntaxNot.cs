using System.Collections.Generic;

namespace Easily.ES {

	/// <summary>
	/// @author Easily
	/// </summary>
	internal class SyntaxNot : Syntax {

		public SyntaxNot() {
			Add(new SyntaxMatchBody(new[] { typeof(CharterEX), typeof(IExpressionRight) }));
		}

		public override void Parse(Parser parser, List<IExpression> list, ref int pos) {
			var item1 = list[pos + 1].Cast<IExpressionLogic>();
			list.RemoveRange(pos, 2);
			list.Insert(pos, new ExpressionNot(item1));
			pos -= 1;
		}

	}

}