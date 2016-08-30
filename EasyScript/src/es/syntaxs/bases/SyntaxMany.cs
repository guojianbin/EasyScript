using System.Collections.Generic;
using System.Linq;
using Easily.Bases;

namespace Easily.ES {

	/// <summary>
	/// @author Easily
	/// </summary>
	internal abstract class SyntaxMany : Disposable, ISyntax {

		private ISyntax _target;
		private readonly List<Syntax> _syntaxs = new List<Syntax>();

		public void Add(Syntax syntax) {
			_syntaxs.Add(syntax);
		}

		public bool IsMatch(List<IExpression> list, int pos) {
			return (_target = _syntaxs.FirstOrDefault(t => t.IsMatch(list, pos))) != null;
		}

		public virtual void Parse(Parser parser, List<IExpression> list, ref int pos) {
			_target.Parse(parser, list, ref pos);
		}

	}

}