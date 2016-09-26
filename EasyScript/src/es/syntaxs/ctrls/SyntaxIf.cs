using System.Collections.Generic;

namespace Easily.ES {

	/// <summary>
	/// @author Easily
	/// </summary>
	internal class SyntaxIf : Syntax {

		public SyntaxIf() {
			Add(new SyntaxMatchBody(new[] { typeof(CharterIf), typeof(ExpressionParens), typeof(ExpressionBraces) }));
		}

		public override void Parse(Parser parser, List<IExpression> list, ref int pos) {
			var item1 = list[pos + 1].Cast<ExpressionParens>();
			var item2 = list[pos + 2].Cast<ExpressionBraces>();
			list.RemoveRange(pos, 3);
			parser.Parse(item1);
			parser.Parse(item2);
			var cond = item1.Unbound().Cast<IExpressionLogic>();
			list.Insert(pos, new ExpressionIf(cond, item2.Unbound()));
		}

	}

}