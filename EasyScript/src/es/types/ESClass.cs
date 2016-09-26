using Easily.Bases;

namespace Easily.ES {

	/// <summary>
	/// @author Easily
	/// </summary>
	public sealed class ESClass : ESObject, IESClass {

		private readonly IExpressionClass _value;

		public ESClass(IExpressionClass value) {
			_value = value;
		}

		public IESObject New(ESContext context) {
			return New(context, ToVirtuals(null, 0));
		}

		public IESObject New(ESContext context, params object[] args) {
			return New(context, ToVirtuals(args, args.Length));
		}

		public IESObject New(ESContext context, IESObject[] args) {
			return _value.New(context, args);
		}

		public override string ToString() {
			return string.Format("ESClass Value: {0}", _value);
		}

	}

}