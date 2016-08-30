using Easily.Bases;

namespace Easily.ES {

	/// <summary>
	/// @author Easily
	/// </summary>
	public class ExpressionString : Expression, IExpressionRight, IExpressionLogic, IExpressionName {

		private readonly ESString _value;

		public string Name {
			get { return _value.Value; }
		}

		public ExpressionString(string str) : this(new ESString(str)) {
			// ignored
		}

		public ExpressionString(ESString value) {
			_value = value;
		}

		public bool ToBoolean(ESContext context) {
			return _value.ToBoolean();
		}

		public override IESObject Execute(ESContext context) {
			return _value;
		}

		public IESObject GetValue(ESContext context) {
			return _value;
		}

		protected override void OnDispose() {
			base.OnDispose();
			_value.Dispose();
		}

		public override string ToString() {
			return string.Format("ExpressionString Value: {0}", _value);
		}

	}

}