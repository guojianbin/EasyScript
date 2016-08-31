using System.Linq;
using Easily.Bases;
using Easily.Utility;

namespace Easily.ES {

	/// <summary>
	/// @author Easily
	/// </summary>
	public class ExpressionArray : Expression, IExpressionRight {

		private readonly ExpressionArrayArgs _args;

		public ExpressionArray(ExpressionArrayArgs args) {
			_args = args;
		}

		public override IESObject Execute(ESContext context) {
			return GetValue(context);
		}

		public IESObject GetValue(ESContext context) {
			return _args.Count == 0 ? new ESArray() : new ESArray(_args.Select(t => t.Execute(context)));
		}

		public override void Checking() {
			_args.ForEach(Checking);
		}

		public override string ToString() {
			return string.Format("ExpressionArray List: {0}", _args.Count);
		}

		protected override void OnDispose() {
			base.OnDispose();
			_args.Dispose();
		}

	}

}