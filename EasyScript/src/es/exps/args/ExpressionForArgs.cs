namespace Easily.ES {

	/// <summary>
	/// @author Easily
	/// </summary>
	public class ExpressionForArgs : Expression {

		private readonly IExpressionRight _begin;
		private readonly IExpressionRight _end;
		private readonly IExpressionName _index;
		private readonly IExpressionRight _step;

		public IExpressionName index {
			get { return _index; }
		}

		public IExpressionRight begin {
			get { return _begin; }
		}

		public IExpressionRight end {
			get { return _end; }
		}

		public IExpressionRight step {
			get { return _step; }
		}

		public ExpressionForArgs(IExpressionName index, IExpressionRight begin, IExpressionRight end, IExpressionRight step) {
			_index = index;
			_begin = begin;
			_end = end;
			_step = step;
		}

		public override void Checking() {
			_index.Checking();
			_begin.Checking();
			_end.Checking();
			_step.Checking();
		}

		protected override void OnDispose() {
			base.OnDispose();
			_index.Dispose();
			_begin.Dispose();
			_end.Dispose();
			_step.Dispose();
		}

		public override string ToString() {
			return string.Format("ExpressionForArgs Index: {0}, Begin: {1}, End: {2}, Step: {3}", _index, _begin, _end, _step);
		}

	}

}