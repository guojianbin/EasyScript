using System;

namespace Easily.Utility {

	/// <summary>
	/// @author Easily
	/// </summary>
	public class Stopwatch : IDisposable {

		private readonly System.Diagnostics.Stopwatch _stopwatch;
		private readonly Action<long> _callback;

		public Stopwatch(Action<long> callback) {
			_callback = callback;
			_stopwatch = System.Diagnostics.Stopwatch.StartNew();
		}

		public void Dispose() {
			_callback(_stopwatch.ElapsedMilliseconds);
			_stopwatch.Stop();
		}

	}

}