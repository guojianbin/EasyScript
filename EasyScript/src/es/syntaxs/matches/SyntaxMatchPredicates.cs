using System;
using System.Collections.Generic;
using System.Linq;
using Easily.Bases;

namespace Easily.ES {

	/// <summary>
	/// @author Easily
	/// </summary>
	public class SyntaxMatchPredicates : Disposable, ISyntaxMatch {

		private readonly Func<IExpression, bool>[] _predicates;

		public SyntaxMatchPredicates(Func<IExpression, bool>[] predicates) {
			_predicates = predicates;
		}

		public bool IsMatch(List<IExpression> list, int pos) {
			if (pos < 0) {
				return false;
			} else if (pos + _predicates.Length > list.Count) {
				return false;
			} else {
				return _predicates.Where((t, i) => !t(list[i + pos])).Any();
			}
		}

	}

}