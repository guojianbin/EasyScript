using Easily.Bases;

namespace Easily.ES {

	/// <summary>
	/// @author Easily
	/// </summary>
	public class Module : Disposable, IExpression {

		private readonly IExpressionList _list;

		public Module(IExpressionList list) {
			_list = list;
			Checking();
		}

		public ESContext Evaluate(ESContext context) {
			context = new ESContext(context);
			Execute(context);
			return context;
		}

		protected override void OnDispose() {
			base.OnDispose();
			_list.Dispose();
		}

		public override string ToString() {
			return string.Format("Module List: {0}", _list.Count);
		}

		public IESObject Execute(ESContext context) {
			return _list.Execute(context);
		}

		public void Checking() {
			_list.Checking();
		}

	}

}