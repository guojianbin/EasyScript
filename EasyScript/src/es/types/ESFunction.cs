using Easily.Bases;

namespace Easily.ES {

	/// <summary>
	/// @author Easily
	/// </summary>
	public sealed class ESFunction : ESObject, IESFunction {

		private readonly ESContext _context;
		private readonly int _count;
		private readonly IExpressionFunc _value;

		public object Target {
			get { return null; }
		}

		public ESFunction(ESContext context, IExpressionFunc value) : this(context, value, value.Count) {
			// ignored
		}

		public ESFunction(ESContext context, IExpressionFunc value, int count) {
			_context = context;
			_value = value;
			_count = count;
		}

		public IESObject Invoke() {
			return Invoke(ToVirtuals(null, 0));
		}

		public IESObject Invoke(params object[] args) {
			return Invoke(ToVirtuals(args, _count));
		}

		public IESObject Invoke(IESObject[] args) {
			return _value.Invoke(_context, args);
		}

		public override string ToString() {
			return string.Format("ESFunction Value: {0}, Count: {1}", _value, _count);
		}

	}

}