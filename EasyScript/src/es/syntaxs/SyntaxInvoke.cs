using System.Collections.Generic;

namespace Easily.ES {

	/// <summary>
	/// @author Easily
	/// </summary>
	internal class SyntaxInvoke : Syntax {

		public SyntaxInvoke() {
			Add(new SyntaxMatchBody(new[] {typeof(IExpressionRight), typeof(ExpressionSS)}));
		}

		public override void Parse(Parser parser, List<IExpression> list, ref int pos) {
			var item1 = list[pos].Cast<IExpressionRight>();
			var item2 = list[pos + 1].Cast<ExpressionSS>();
			list.RemoveRange(pos, 2);
			parser.Parse(item2);
			parser.Parse(ParseLevel.ARRAY_ARGS, item2);
			list.Insert(pos, new ExpressionInvoke(item1, ESUtility.ToArrayArgs(item2)));
			pos -= 1;
		}

	}

}