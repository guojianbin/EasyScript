using System;
using System.Collections.Generic;
using Easily.Bases;

namespace Easily.ES {

	/// <summary>
	/// @author Easily
	/// </summary>
	public class SyntaxMatchBound : Disposable, ISyntaxMatch {

		private readonly Type _type;

		public SyntaxMatchBound(Type type) {
			_type = type;
		}

		public bool IsMatch(List<IExpression> list, int pos) {
			if (pos - 1 < 0) {
				return true;
			} else {
				return !_type.IsInstanceOfType(list[pos - 1]);
			}
		}

	}

}