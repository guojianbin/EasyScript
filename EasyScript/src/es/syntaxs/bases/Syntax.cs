using System.Collections.Generic;
using System.Linq;
using Easily.Bases;

namespace Easily.ES {

	/// <summary>
	/// @author Easily
	/// </summary>
	internal abstract class Syntax : Disposable, ISyntax {

		private readonly List<ISyntaxMatch> _matches = new List<ISyntaxMatch>();

		public void Add(ISyntaxMatch match) {
			_matches.Add(match);
		}

		public bool IsMatch(List<IExpression> list, int pos) {
			return _matches.All(t => t.IsMatch(list, pos));
		}

		public virtual void Parse(Parser parser, List<IExpression> list, ref int pos) {
			// ignored
		}

	}

}