using System.Collections.Generic;

namespace Easily.ES {

	/// <summary>
	/// @author Easily
	/// </summary>
	internal class SyntaxReturn : SyntaxMany {

		public SyntaxReturn() {
			var item = new SyntaxNotifier(OnParse);
			item.Add(new SyntaxMatchBody(new[] { typeof(CharterRet), typeof(IExpressionRight) }));
			Add(item);

			var item2 = new SyntaxNotifier(OnParse2);
			item2.Add(new SyntaxMatchBody(new[] { typeof(CharterRet) }));
			Add(item2);
		}

		public static void OnParse(Parser parser, List<IExpression> list, ref int pos) {
			var item1 = list[pos + 1].Cast<IExpressionRight>();
			list.RemoveRange(pos, 2);
			list.Insert(pos, new ExpressionReturn(item1));
			pos -= 1;
		}

		public static void OnParse2(Parser parser, List<IExpression> list, ref int pos) {
			list.RemoveRange(pos, 1);
			list.Insert(pos, new ExpressionReturn());
			pos -= 1;
		}

	}

}