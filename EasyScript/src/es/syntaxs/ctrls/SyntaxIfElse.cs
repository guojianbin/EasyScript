using System.Collections.Generic;

namespace Easily.ES {

	/// <summary>
	/// @author Easily
	/// </summary>
	internal class SyntaxIfElse : Syntax {

		public SyntaxIfElse() {
			Add(new SyntaxMatchBody(new[] { typeof(CharterIf), typeof(ExpressionParens), typeof(ExpressionBraces), typeof(CharterElse), typeof(ExpressionBraces) }));
		}

		public override void Parse(Parser parser, List<IExpression> list, ref int pos) {
			var item1 = list[pos + 1].Cast<ExpressionParens>();
			var item2 = list[pos + 2].Cast<ExpressionBraces>();
			var item3 = list[pos + 4].Cast<ExpressionBraces>();
			list.RemoveRange(pos, 5);
			parser.Parse(item1);
			parser.Parse(item2);
			parser.Parse(item3);
			var cond = item1.Unbound().Cast<IExpressionLogic>();
			list.Insert(pos, new ExpressionIfElse(cond, item2.Unbound(), item3.Unbound()));
		}

	}

}