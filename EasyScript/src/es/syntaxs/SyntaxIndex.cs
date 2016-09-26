using System.Collections.Generic;

namespace Easily.ES {

	/// <summary>
	/// @author Easily
	/// </summary>
	internal class SyntaxIndex : Syntax {

		public SyntaxIndex() {
			Add(new SyntaxMatchBody(new[] { typeof(IExpressionRight), typeof(ExpressionBrackets) }));
		}

		public override void Parse(Parser parser, List<IExpression> list, ref int pos) {
			var item1 = list[pos].Cast<IExpressionRight>();
			var item2 = list[pos + 1].Cast<ExpressionBrackets>();
			list.RemoveRange(pos, 2);
			parser.Parse(item2);
			list.Insert(pos, new ExpressionIndex(item1, item2.Unbound().Cast<IExpressionRight>()));
			pos -= 1;
		}

	}

}