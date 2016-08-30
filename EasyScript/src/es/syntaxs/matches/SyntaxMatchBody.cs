using System;
using System.Collections.Generic;
using System.Linq;
using Easily.Bases;

namespace Easily.ES {

	/// <summary>
	/// @author Easily
	/// </summary>
	public class SyntaxMatchBody : Disposable, ISyntaxMatch {

		private readonly Type[] _body;

		public SyntaxMatchBody(Type[] body) {
			_body = body;
		}

		public bool IsMatch(List<IExpression> list, int pos) {
			if (pos < 0) {
				return false;
			} else if (pos + _body.Length > list.Count) {
				return false;
			} else {
				return !_body.Where((t, i) => !t.IsInstanceOfType(list[i + pos])).Any();
			}
		}

	}

}