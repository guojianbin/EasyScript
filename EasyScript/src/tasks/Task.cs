using Engine.Bases;

namespace Engine.Tasks {

/// <summary>
/// @author Easily
/// </summary>
public class Task : Disposable, ITask {

	private bool _isStart;
	private bool _isError;
	private bool _isComplete;

	private Observer _onStart = new Observer();
	private Observer _onError = new Observer();
	private Observer _onComplete = new Observer();
	private Observer _onDispose = new Observer();

	public bool IsStart { get { return _isStart; } }
	public bool IsError { get { return _isError; } }
	public bool IsComplete { get { return _isComplete; } }

	public Observer onStart { get { return _onStart; } set { _onStart = value; } }
	public Observer onError { get { return _onError; } set { _onError = value; } }
	public Observer onComplete { get { return _onComplete; } set { _onComplete = value; } }
	public Observer onDispose { get { return _onDispose; } set { _onDispose = value; } }

	public void Start() {
		if (IsDisposed) {
			ThrowException(ExceptionType.OBJECT_DISPOSED, ToString());
		} else if (IsError) {
			ThrowException(ExceptionType.INVALID_OPERATION, "Errored");
		} else if (IsComplete) {
			ThrowException(ExceptionType.INVALID_OPERATION, "Completed");
		} else if (IsStart) {
			ThrowException(ExceptionType.INVALID_OPERATION, "Started");
		} else {
			_isStart = true;
			OnStart();
		}
	}

	public void Error() {
		if (IsDisposed) {
			ThrowException(ExceptionType.OBJECT_DISPOSED, ToString());
		} else if (IsComplete) {
			ThrowException(ExceptionType.INVALID_OPERATION, "Completed");
		} else if (IsError) {
			ThrowException(ExceptionType.INVALID_OPERATION, "Errored");
		} else {
			_isError = true;
			OnError();
			Dispose();
		}
	}

	public void Complete() {
		if (IsDisposed) {
			ThrowException(ExceptionType.OBJECT_DISPOSED, ToString());
		} else if (IsError) {
			ThrowException(ExceptionType.INVALID_OPERATION, "Errored");
		} else if (IsComplete) {
			ThrowException(ExceptionType.INVALID_OPERATION, "Completed");
		} else {
			_isComplete = true;
			OnComplete();
			Dispose();
		}
	}

	protected virtual void OnStart() {
		_onStart.Invoke();
	}

	protected virtual void OnError() {
		_onError.Invoke();
	}

	protected virtual void OnComplete() {
		_onComplete.Invoke();
	}

	protected override void OnDispose() {
		base.OnDispose();
		_onDispose.Invoke();
		_onStart.Clear();
		_onError.Clear();
		_onComplete.Clear();
		_onDispose.Clear();
	}

}

}