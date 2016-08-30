using System.Collections.Generic;

namespace Easily.ES {

	/// <summary>
	/// @author Easily
	/// </summary>
	internal class SyntaxIfElseIf : Syntax {

		private readonly SyntaxIfElse _ifElse = new SyntaxIfElse();

		public SyntaxIfElseIf() {
			Add(new SyntaxMatchIfElse());
		}

		public override void Parse(Parser parser, List<IExpression> list, ref int pos) {
			var pos2 = pos;
			Parse2(parser, list, ref pos2);
		}

		public void Parse2(Parser parser, List<IExpression> list, ref int pos) {
			if (list.IsMatch<MarkerIf>(pos + 4)) {
				pos += 4;
				Parse2(parser, list, ref pos);
				list[pos] = new ExpressionBB(new List<IExpression> {list[pos]});
				pos -= 4;
				_ifElse.Parse(parser, list, ref pos);
			} else {
				_ifElse.Parse(parser, list, ref pos);
			}
		}

	}

}