using System.Linq;
using Easily.Bases;

namespace Easily.ES {

	/// <summary>
	/// @author Easily
	/// </summary>
	public class ExpressionTable : Expression, IExpressionRight {

		private readonly ExpressionTableArgs _args;

		public ExpressionTable(ExpressionTableArgs args) {
			_args = args;
		}

		public override string ToString() {
			return string.Format("ExpressionTable Map: {0}", _args.Count);
		}

		public override IESObject Execute(ESContext context) {
			return GetValue(context);
		}

		public IESObject GetValue(ESContext context) {
			if (_args.Count == 0) {
				return new ESTable();
			} else {
				return new ESTable(_args.ToDictionary(t => t.Key, t => t.Value.GetValue(context)));
			}
		}

	}

}