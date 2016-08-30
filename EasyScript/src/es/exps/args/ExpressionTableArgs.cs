using System.Collections;
using System.Collections.Generic;

namespace Easily.ES {

	public class ExpressionTableArgs : Expression, IEnumerable<KeyValuePair<string, IExpressionRight>> {

		private readonly Dictionary<string, IExpressionRight> _args;

		public int Count {
			get { return _args.Count; }
		}

		public ExpressionTableArgs() {
			_args = new Dictionary<string, IExpressionRight>();
		}

		public ExpressionTableArgs(Dictionary<string, IExpressionRight> args) {
			_args = args;
		}

		public static implicit operator Dictionary<string, IExpressionRight>(ExpressionTableArgs obj) {
			return obj._args;
		}

		public override string ToString() {
			return string.Format("ExpressionTableArgs Args: {0}", Count);
		}

		public IEnumerator<KeyValuePair<string, IExpressionRight>> GetEnumerator() {
			return _args.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator() {
			return GetEnumerator();
		}

	}

}