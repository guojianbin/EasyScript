using Easily.Bases;
using Easily.Utility;

namespace Easily.ES {

	/// <summary>
	/// @author Easily
	/// </summary>
	public class ExpressionNew : Expression, IExpressionRight, IExpressionLogic {

		private readonly ExpressionArrayArgs _args;
		private readonly string _name;

		public ExpressionNew(string name, ExpressionArrayArgs args) {
			_name = name;
			_args = args;
		}

		public bool ToBoolean(ESContext context) {
			return Execute(context).ToBoolean();
		}

		public override IESObject Execute(ESContext context) {
			return GetValue(context);
		}

		public IESObject GetValue(ESContext context) {
			return context.GetValue<IESClass>(_name).New(context, ESUtility.ToArray(context, _args));
		}

		public override void Checking() {
			_args.ForEach(Checking);
		}

		public override string ToString() {
			return string.Format("ExpressionNew Name: {0}, Args: {1}", _name, _args.Count);
		}

	}

}