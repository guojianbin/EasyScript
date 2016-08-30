using Easily.Bases;

namespace Easily.ES {

	/// <summary>
	/// @author Easily
	/// </summary>
	public class ExpressionForEach : Expression {

		private readonly IExpression _entry;
		private readonly IExpressionName _index;
		private readonly IExpressionRight _list;

		public ExpressionForEach(IExpressionName index, IExpressionRight list, IExpression entry) {
			_index = index;
			_list = list;
			_entry = entry;
		}

		public override IESObject Execute(ESContext context) {
			var ret = ESDefault.Value;
			var items = _list.GetValue<IESEnumerable>(context);
			var iter = items.GetEnumerator();
			while (true) {
				if (iter.MoveNext()) {
					context.UpdateValue(_index.Name, ToVirtual(iter.Current));
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
				}
			}
		}

		public override void Checking() {
			_index.Checking();
			_list.Checking();
			_entry.Checking();
		}

		protected override void OnDispose() {
			base.OnDispose();
			_index.Dispose();
			_list.Dispose();
			_entry.Dispose();
		}

		public override string ToString() {
			return string.Format("ExpressionForEach Index: {0}, List: {1}, Body: {2}", _index, _list, _entry);
		}

	}

}