using System.Linq;
using Easily.Bases;

namespace Easily.ES {

	/// <summary>
	/// @author Easily
	/// </summary>
	public class ExpressionArray : Expression, IExpressionRight {

		private readonly ExpressionArrayArgs _args;

		public ExpressionArray(ExpressionArrayArgs args) {
			_args = args;
		}

		public override string ToString() {
			return string.Format("ExpressionArray List: {0}", _args.Count);
		}

		public override IESObject Execute(ESContext context) {
			return GetValue(context);
		}

		public IESObject GetValue(ESContext context) {
			if (_args.Count == 0) {
				return new ESArray();
			} else {
				return new ESArray(_args.Select(t => t.Execute(context)));
			}
		}

		public override void Checking() {
			_args.ForEach(Checking);
		}

	}

}