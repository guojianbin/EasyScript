using Easily.Bases;

namespace Easily.ES {

	/// <summary>
	/// @author Easily
	/// </summary>
	public class ExpressionWord : Expression, IExpressionRight, IExpressionLeft, IExpressionLogic, IExpressionName {

		private readonly string _value;

		public ExpressionWord(string value) {
			_value = value;
		}

		public override string ToString() {
			return string.Format("ExpressionWord Value: {0}", _value);
		}

		public void SetValue(ESContext context, IESObject value) {
			context.SetValue(_value, value);
		}

		public bool ToBoolean(ESContext context) {
			return Execute(context).ToBoolean();
		}

		public string Name {
			get { return _value; }
		}

		public override IESObject Execute(ESContext context) {
			return GetValue(context);
		}

		public IESObject GetValue(ESContext context) {
			return context.GetValue(_value);
		}

	}

}