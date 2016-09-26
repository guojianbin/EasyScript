using System.Collections.Generic;

namespace Easily.ES {

	/// <summary>
	/// @author Easily
	/// </summary>
	internal class SyntaxFor : Syntax {

		public SyntaxFor() {
			Add(new SyntaxMatchBody(new[] { typeof(CharterFor), typeof(ExpressionParens), typeof(ExpressionBraces) }));
		}

		public override void Parse(Parser parser, List<IExpression> list, ref int pos) {
			var item1 = list[pos + 1].Cast<ExpressionParens>();
			var item2 = list[pos + 2].Cast<ExpressionBraces>();
			list.RemoveRange(pos, 3);
			parser.Parse(item1);
			parser.Parse(ParseLevel.FOR_ARGS, item1);
			parser.Parse(item2);
			list.Insert(pos, new ExpressionFor(item1[0].Cast<ExpressionForArgs>(), item2.Unbound()));
		}

	}

}