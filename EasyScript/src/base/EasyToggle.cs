using System;

namespace Easily.Bases {

/// <summary>
/// @author Easily
/// </summary>
public class EasyToggle : Disposable {

	private readonly Action<bool> _onActive;
	private int _count;

	public EasyToggle(Action<bool> onActive) {
		_onActive = onActive;
	}

	public EasyToggle(int count, Action<bool> onActive) {
		_count = count;
		_onActive = onActive;
	}

	public void SetActive(bool value) {
		if (value) {
			if (_count == 0) {
				_count += 1;
				_onActive(true);
			} else if (_count > 0) {
				_count += 1;
			} else {
				ThrowException(ExceptionType.INVALID_OPERATION, string.Format("on toggle error count={0}", _count));
			}
		} else {
			if (_count == 1) {
				_count -= 1;
				_onActive(false);
			} else if (_count > 1) {
				_count -= 1;
			} else {
				ThrowException(ExceptionType.INVALID_OPERATION, string.Format("off toggle error count={0}", _count));
			}
		}
	}

}

}