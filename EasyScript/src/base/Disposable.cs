using System;
using Easily.Utility;

namespace Easily.Bases {

	/// <summary>
	/// @author Easily
	/// </summary>
	public class Disposable : IDisposable {

		private bool _disposed;

		public bool IsDisposed {
			get { return _disposed; }
		}

		public bool IsEnabled {
			get { return !_disposed; }
		}

		public static string NewUID() {
			return StringUtils.NewUID();
		}

		public static T NIO<T>() where T : Disposable {
			return EasyDefault<T>.Value;
		}

		public static void ThrowException(string msg) {
			ThrowException(ExceptionType.EXCEPTION, msg);
		}

		public static void ThrowException(ExceptionType type, string msg) {
			ThrowHelper.ThrowException(type, msg);
		}

		protected virtual void OnDispose() {
			// ignored
		}

		public void Dispose() {
			if (!_disposed) {
				GC.SuppressFinalize(this);
				_disposed = true;
				OnDispose();
			}
		}

	}

}