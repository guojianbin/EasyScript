using System;
using System.Collections.Generic;
using System.Linq;
using Easily.Bases;

namespace Easily.ES {

	/// <summary>
	/// @author Easily
	/// </summary>
	public class SyntaxMatchSplit : Disposable, ISyntaxMatch {

		private readonly Type[] _body;
		private readonly Type _sep;

		public SyntaxMatchSplit(Type sep, Type[] body) {
			_sep = sep;
			_body = body;
		}

		private bool MatchBody(IList<IExpression> list, int pos) {
			return _body.Where((t, i) => t.IsInstanceOfType(list[i + pos])).Any();
		}

		private bool MatchSep(IList<IExpression> list, int pos) {
			return _sep.IsInstanceOfType(list[pos + _body.Length]);
		}

		public bool IsMatch(List<IExpression> list, int pos) {
			if (list.Count == 0) {
				return true;
			} else if (list.Count - pos < _body.Length) {
				return false;
			} else if (list.Count - pos == _body.Length) {
				return MatchBody(list, pos);
			} else if (list.Count - pos == _body.Length + 1) {
				return MatchBody(list, pos) && MatchSep(list, pos);
			} else {
				return MatchBody(list, pos) && MatchSep(list, pos) && IsMatch(list, pos + _body.Length + 1);
			}
		}

	}

}