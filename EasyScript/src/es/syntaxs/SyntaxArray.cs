using System.Collections.Generic;

namespace Easily.ES {

	/// <summary>
	/// @author Easily
	/// </summary>
	internal class SyntaxArray : Syntax {

		public SyntaxArray() {
			Add(new SyntaxMatchBody(new[] { typeof(ExpressionBrackets) }));
		}

		public override void Parse(Parser parser, List<IExpression> list, ref int pos) {
			var item1 = list[pos].Cast<ExpressionBrackets>();
			list.RemoveRange(pos, 1);
			parser.Parse(item1);
			parser.Parse(ParseLevel.ARRAY_ARGS, item1);
			list.Insert(pos, new ExpressionArray(ESUtility.ToArrayArgs(item1)));
			pos -= 1;
		}

	}

}