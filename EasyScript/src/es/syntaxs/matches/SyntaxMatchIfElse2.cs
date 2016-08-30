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
			_if = new SyntaxMatchBody(new[] {typeof(MarkerIf), typeof(ExpressionSS), typeof(ExpressionBB)});
			_ifElse = new SyntaxMatchBody(new[] {typeof(MarkerIf), typeof(ExpressionSS), typeof(ExpressionBB), typeof(MarkerElse), typeof(ExpressionBB)});
			_ifElse2 = new SyntaxMatchBody(new[] {typeof(MarkerIf), typeof(ExpressionSS), typeof(ExpressionBB), typeof(MarkerElse)});
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

		public bool IsMatch(List<IExpression> list, int pos) {
			return IsMatch(list, pos, 0);
		}

	}

}