using System.Collections.Generic;

namespace Easily.ES {

	/// <summary>
	/// @author Easily
	/// </summary>
	internal class SyntaxWhile : Syntax {

		public SyntaxWhile() {
			Add(new SyntaxMatchBody(new[] {typeof(MarkerWhile), typeof(ExpressionSS), typeof(ExpressionBB)}));
		}

		public override void Parse(Parser parser, List<IExpression> list, ref int pos) {
			var item1 = list[pos + 1].Cast<ExpressionSS>();
			var item2 = list[pos + 2].Cast<ExpressionBB>();
			list.RemoveRange(pos, 3);
			parser.Parse(item1);
			parser.Parse(item2);
			var cond = item1.Unbound().Cast<IExpressionLogic>();
			list.Insert(pos, new ExpressionWhile(cond, item2.Unbound()));
		}

	}

}