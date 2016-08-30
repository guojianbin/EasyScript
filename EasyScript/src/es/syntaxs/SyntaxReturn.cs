using System.Collections.Generic;

namespace Easily.ES {

	/// <summary>
	/// @author Easily
	/// </summary>
	internal class SyntaxReturn : Syntax {

		public SyntaxReturn() {
			Add(new SyntaxMatchBody(new[] {typeof(MarkerRet)}));
		}

		public override void Parse(Parser parser, List<IExpression> list, ref int pos) {
			list.RemoveRange(pos, 1);
			list.Insert(pos, new ExpressionReturn());
			pos -= 1;
		}

	}

}