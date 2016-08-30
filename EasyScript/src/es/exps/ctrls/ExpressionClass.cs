using Easily.Bases;

namespace Easily.ES {

	/// <summary>
	/// @author Easily
	/// </summary>
	public class ExpressionClass : Expression, IExpressionClass {

		private readonly IExpression _entry;
		private readonly string _name;

		public ExpressionClass(string name, IExpression entry) {
			_name = name;
			_entry = entry;
		}

		protected override void OnDispose() {
			base.OnDispose();
			_entry.Dispose();
		}

		public override IESObject Execute(ESContext context) {
			var value = new ESClass(this);
			context.UpdateValue(_name, value);
			return value;
		}

		public IESObject New(ESContext context, IESObject[] args) {
			context = new ESContext(context);
			var obj = new ESInstance();
			context.UpdateValue("this", obj);
			_entry.Execute(context);
			context.ForEach(obj.Add);
			context.GetValue<IESFunction>(_name).Invoke(args);
			return obj;
		}

		public override void Checking() {
			_entry.Checking();
		}

	}

}