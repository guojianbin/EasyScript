using System.Collections;
using System.Collections.Generic;

namespace Easily.ES {

	public class ExpressionStringArgs : Expression, IEnumerable<string> {

		private readonly List<string> _args;

		public int Count {
			get { return _args.Count; }
		}

		public ExpressionStringArgs() {
			_args = new List<string>();
		}

		public ExpressionStringArgs(string arg) {
			_args = new List<string>();
			_args.Add(arg);
		}

		public ExpressionStringArgs(List<string> args) {
			_args = args;
		}

		public override string ToString() {
			return string.Format("ExpressionStringArgs Args: {0}", _args.Format());
		}

		public IEnumerator<string> GetEnumerator() {
			return _args.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator() {
			return GetEnumerator();
		}

	}

}