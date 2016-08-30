using System;
using System.Threading;

namespace Engine.Tasks {

	/// <summary>
	/// @author Easily
	/// </summary>
	public class AsyncTask : Task {

		private readonly Action _callback;

		public AsyncTask(Action callback) {
			_callback = callback;
		}

		protected override void OnStart() {
			base.OnStart();
			ThreadPool.QueueUserWorkItem(t => Execute());
		}

		private void Execute() {
			_callback();
			Abort();
		}

		public void Abort() {
			lock (this) {
				Dispose();
			}
		}

	}

	/// <summary>
	/// @author Easily
	/// </summary>
	public class AsyncTask<T> : Task {

		private readonly Func<T> _callback;
		private T _result;

		public AsyncTask(Func<T> callback) {
			_callback = callback;
		}

		public T Result {
			get { return _result; }
		}

		protected override void OnStart() {
			base.OnStart();
			ThreadPool.QueueUserWorkItem(t => Execute());
		}

		private void Execute() {
			_result = _callback();
			Abort();
		}

		public void Abort() {
			lock (this) {
				Dispose();
			}
		}

	}

}