using System;

namespace Engine.Bases {

/// <summary>
/// @author Easily
/// </summary>
public class Disposable : IDisposable {

	private bool _isDisposed;

	public bool IsDisposed {
		get { return _isDisposed; }
	}

	public bool IsEnabled {
		get { return !_isDisposed; }
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

	public void Dispose(bool value) {
		_isDisposed = value;
	}

	protected virtual void OnDispose() {
		// ignored
	}

	public void Dispose() {
		if (!_isDisposed) {
			GC.SuppressFinalize(this);
			Dispose(true);
			OnDispose();
		}
	}

}

}
