using System;
using System.Collections.Generic;
using System.Linq;
using Easily.Bases;

namespace Easily.ES {

	/// <summary>
	/// @author Easily
	/// </summary>
	public class SyntaxMatchPredicate : Disposable, ISyntaxMatch {

		private readonly Func<IExpression, bool> _predicate;
		private readonly int _count;

		public SyntaxMatchPredicate(Func<IExpression, bool> predicate, int count) {
			_predicate = predicate;
			_count = count;
		}

		public bool IsMatch(List<IExpression> list, int pos) {
			if (pos < 0) {
				return false;
			} else if (pos + _count > list.Count) {
				return false;
			} else {
				return list.Skip(pos).Take(_count).All(t => _predicate(t));
			}
		}

	}

}