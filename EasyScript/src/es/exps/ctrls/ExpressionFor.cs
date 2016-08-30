using Easily.Bases;

namespace Easily.ES {

	/// <summary>
	/// @author Easily
	/// </summary>
	public class ExpressionFor : Expression {

		private readonly ExpressionForArgs _args;
		private readonly IExpression _entry;

		public ExpressionFor(ExpressionForArgs args, IExpression entry) {
			_args = args;
			_entry = entry;
		}

		public override IESObject Execute(ESContext context) {
			var ret = ESDefault.Value;
			var index = _args.begin.GetValue(context).Clone().Cast<IESNumber>();
			var end = _args.end.GetValue<IESNumber>(context).Value;
			var step = _args.step.GetValue<IESNumber>(context).Value;
			while (true) {
				if (index.Value < end) {
					context.UpdateValue(_args.index.Name, index);
					ret = _entry.Execute(context);
				} else {
					return ret;
				}
				if (context.IsBreak) {
					context.IsBreak = false;
					return ret;
				}
				if (context.IsReturn) {
					return ret;
				} else {
					index.Value += step;
				}
			}
		}

		public override void Checking() {
			_args.Checking();
			_entry.Checking();
		}

		protected override void OnDispose() {
			base.OnDispose();
			_args.Dispose();
			_entry.Dispose();
		}

		public override string ToString() {
			return string.Format("ExpressionFor Args: {0}, Entry: {1}", _args, _entry);
		}

	}

}