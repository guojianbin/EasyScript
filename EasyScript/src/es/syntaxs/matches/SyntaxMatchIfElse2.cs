using System.Collections.Generic;
using Easily.Bases;

namespace Easily.ES {

	/// <summary>
	/// @author Easily
	/// </summary>
	public class SyntaxMatchIfElse2 : Disposable, ISyntaxMatch {

		private readonly ISyntaxMatch _if;
		private readonly ISyntaxMatch _ifElse;
		private readonly ISyntaxMatch _ifElse2;

		public SyntaxMatchIfElse2() {
			_if = new SyntaxMatchBody(new[] { typeof(CharterIf), typeof(ExpressionParens), typeof(ExpressionBraces) });
			_ifElse = new SyntaxMatchBody(new[] { typeof(CharterIf), typeof(ExpressionParens), typeof(ExpressionBraces), typeof(CharterElse), typeof(ExpressionBraces) });
			_ifElse2 = new SyntaxMatchBody(new[] { typeof(CharterIf), typeof(ExpressionParens), typeof(ExpressionBraces), typeof(CharterElse) });
		}

		public bool IsMatch(List<IExpression> list, int pos) {
			return IsMatch(list, pos, 0);
		}

		public bool IsMatch(List<IExpression> list, int pos, int deep) {
			if (deep == 0) {
				if (_ifElse.IsMatch(list, pos)) {
					return false;
				} else if (_ifElse2.IsMatch(list, pos)) {
					return IsMatch(list, pos + 4, deep + 1);
				} else {
					return false;
				}
			} else {
				if (_ifElse.IsMatch(list, pos)) {
					return false;
				} else if (_ifElse2.IsMatch(list, pos)) {
					return IsMatch(list, pos + 4, deep + 1);
				} else if (_if.IsMatch(list, pos)) {
					return true;
				} else {
					return false;
				}
			}
		}

	}

}