using System.Collections.Generic;

namespace Easily.ES {

	/// <summary>
	/// @author Easily
	/// </summary>
	internal class SyntaxParens : Syntax {

		public SyntaxParens() {
			Add(new SyntaxMatchBody(new[] { typeof(ExpressionParens) }));
		}

		public override void Parse(Parser parser, List<IExpression> list, ref int pos) {
			var item1 = list[pos].Cast<ExpressionParens>();
			parser.Parse(item1);
			list[pos] = item1.Unbound();
		}

	}

}