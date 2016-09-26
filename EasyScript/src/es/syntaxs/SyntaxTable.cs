using System.Collections.Generic;

namespace Easily.ES {

	/// <summary>
	/// @author Easily
	/// </summary>
	internal class SyntaxTable : Syntax {

		public SyntaxTable() {
			Add(new SyntaxMatchBody(new[] { typeof(ExpressionBraces) }));
		}

		public override void Parse(Parser parser, List<IExpression> list, ref int pos) {
			var item1 = list[pos].Cast<ExpressionBraces>();
			list.RemoveRange(pos, 1);
			parser.Parse(item1);
			parser.Parse(ParseLevel.TABLE_ARGS, item1);
			list.Insert(pos, new ExpressionTable(ESUtility.ToTableArgs(item1)));
			pos -= 1;
		}

	}

}