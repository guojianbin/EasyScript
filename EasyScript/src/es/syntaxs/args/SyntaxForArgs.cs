using System.Collections.Generic;

namespace Easily.ES {

	/// <summary>
	/// @author Easily
	/// </summary>
	internal class SyntaxForArgs : SyntaxMany {

		public SyntaxForArgs() {
			var item = new SyntaxNotifier(OnParse);
			item.Add(new SyntaxMatchCount(5));
			item.Add(new SyntaxMatchBody(new[] {typeof(ExpressionBind), typeof(MarkerCma), typeof(IExpressionRight), typeof(MarkerCma), typeof(IExpressionRight)}));
			Add(item);

			var item2 = new SyntaxNotifier(OnParse2);
			item2.Add(new SyntaxMatchCount(3));
			item2.Add(new SyntaxMatchBody(new[] {typeof(ExpressionBind), typeof(MarkerCma), typeof(IExpressionRight)}));
			Add(item2);
		}

		public static void OnParse(Parser parser, List<IExpression> list, ref int pos) {
			var item1 = list[pos].Cast<ExpressionBind>();
			var item2 = list[pos + 2].Cast<IExpressionRight>();
			var item3 = list[pos + 4].Cast<IExpressionRight>();
			list.RemoveRange(pos, 5);
			list.Insert(pos, new ExpressionForArgs(item1.LValue.Cast<IExpressionName>(), item1.RValue, item2, item3));
		}

		public static void OnParse2(Parser parser, List<IExpression> list, ref int pos) {
			var item1 = list[pos].Cast<ExpressionBind>();
			var item2 = list[pos + 2].Cast<IExpressionRight>();
			list.RemoveRange(pos, 3);
			list.Insert(pos, new ExpressionForArgs(item1.LValue.Cast<IExpressionName>(), item1.RValue, item2, new ExpressionNumber(1)));
		}

	}

}