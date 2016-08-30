using System;

namespace Easily.Bases {

public class Observer : Disposable {

	private Action _func;

	public Action Target {
		get { return _func; }
	}

	public Observer() {
		// ignored
	}

	public Observer(Action func) {
		_func = func;
	}

	public static implicit operator Action(Observer obj) {
		return obj.Invoke;
	}

	public static implicit operator Observer(Action func) {
		return new Observer(func);
	}

	public static Observer operator +(Observer obj, Action func) {
		obj.Add(func);
		return obj;
	}

	public static Observer operator -(Observer obj, Action func) {
		obj.Remove(func);
		return obj;
	}

	public void Add(Action func) {
		if (IsDisposed) {
			ThrowException(ExceptionType.OBJECT_DISPOSED, ToString());
		} else if (func == default(Action)) {
			// ignored
		} else if (_func != default(Action)) {
			_func += func;
		} else {
			_func = func;
		}
	}

	public void Remove(Action func) {
		if (IsDisposed) {
			ThrowException(ExceptionType.OBJECT_DISPOSED, ToString());
		} else if (func == default(Action)) {
			// ignored
		} else if (_func != default(Action)) {
			_func -= func;
		}
	}

	public void Invoke() {
		if (IsDisposed) {
			ThrowException(ExceptionType.OBJECT_DISPOSED, ToString());
		} else if (_func != default(Action)) {
			_func();
		}
	}

	public void Clear() {
		_func = default(Action);
	}

	protected override void OnDispose() {
		base.OnDispose();
		_func = default(Action);
	}

}

}