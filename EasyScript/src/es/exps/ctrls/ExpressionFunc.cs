using Easily.Bases;

namespace Easily.ES {

	/// <summary>
	/// @author Easily
	/// </summary>
	public class ExpressionFunc : Expression, IExpressionFunc, IExpressionRight {

		private readonly ExpressionStringArgs _args;
		private readonly IExpression _entry;
		private readonly string _name;

		public ExpressionFunc(ExpressionStringArgs args, IExpression entry) : this("anonymous_" + NewUID(), args, entry) {
			// ignored
		}

		public ExpressionFunc(string name, ExpressionStringArgs args, IExpression entry) {
			_name = name;
			_args = args;
			_entry = entry;
		}

		protected override void OnDispose() {
			base.OnDispose();
			_entry.Dispose();
		}

		public override string ToString() {
			return string.Format("ExpressionFunc Name: {0}, Args: {1}, Entry: {2}", _name, _args.Count, _entry);
		}

		public int Count {
			get { return _args.Count; }
		}

		public override IESObject Execute(ESContext context) {
			return GetValue(context);
		}

		public IESObject Invoke(ESContext context, IESObject[] args) {
			context = new ESContext(context);
			_args.ForEach((i, t) => context.UpdateValue(t, args[i].Clone()));
			return _entry.Execute(context);
		}

		public override void Checking() {
			_entry.Checking();
		}

		public IESObject GetValue(ESContext context) {
			var func = new ESFunction(context, this);
			context.UpdateValue(_name, func);
			return func;
		}

	}

}