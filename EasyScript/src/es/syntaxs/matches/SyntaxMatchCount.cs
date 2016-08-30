using System.Collections.Generic;
using Easily.Bases;

namespace Easily.ES {

	/// <summary>
	/// @author Easily
	/// </summary>
	public class SyntaxMatchCount : Disposable, ISyntaxMatch {

		private readonly int _count;

		public SyntaxMatchCount(int count) {
			_count = count;
		}

		public bool IsMatch(List<IExpression> list, int pos) {
			return list.Count == _count;
		}

	}

}