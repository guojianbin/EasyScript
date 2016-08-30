using System.Collections;
using System.Collections.Generic;

namespace Easily.ES {

	public class ExpressionArrayArgs : Expression, IEnumerable<IExpressionRight> {

		private readonly List<IExpressionRight> _args;

		public int Count {
			get { return _args.Count; }
		}

		public ExpressionArrayArgs() {
			_args = new List<IExpressionRight>();
		}

		public ExpressionArrayArgs(List<IExpressionRight> args) {
			_args = args;
		}

		public static implicit operator List<IExpressionRight>(ExpressionArrayArgs obj) {
			return obj._args;
		}

		public override string ToString() {
			return string.Format("ExpressionArrayArgs Args: {0}", Count);
		}

		public IEnumerator<IExpressionRight> GetEnumerator() {
			return _args.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator() {
			return GetEnumerator();
		}

	}

}