using System.Collections.Generic;

namespace Easily.ES {

	/// <summary>
	/// @author Easily
	/// </summary>
	internal class SyntaxSS : Syntax {

		public SyntaxSS() {
			Add(new SyntaxMatchBody(new[] {typeof(ExpressionSS)}));
		}

		public override void Parse(Parser parser, List<IExpression> list, ref int pos) {
			var item1 = list[pos].Cast<ExpressionSS>();
			parser.Parse(item1);
			list[pos] = item1.Unbound();
		}

	}

}