using System;
using System.Collections.Generic;

namespace Easily.Bases {

/// <summary>
/// @author Easily
/// </summary>
public enum ExceptionType : byte {

	EXCEPTION,
	ARGUMENT,
	ARGUMENT_OUT_OF_RANGE,
	INVALID_OPERATION,
	OBJECT_DISPOSED,
	KEY_NOT_FOUND,
	INDEX_OUT_OF_RANGE,

}

/// <summary>
/// @author Easily
/// </summary>
public static class ThrowHelper {

	public static void ThrowException(ExceptionType type, string msg) {
		switch (type) {
			case ExceptionType.ARGUMENT:
				throw new ArgumentException(msg);
			case ExceptionType.ARGUMENT_OUT_OF_RANGE:
				throw new ArgumentOutOfRangeException(msg);
			case ExceptionType.INVALID_OPERATION:
				throw new InvalidOperationException(msg);
			case ExceptionType.OBJECT_DISPOSED:
				throw new ObjectDisposedException(msg);
			case ExceptionType.KEY_NOT_FOUND:
				throw new KeyNotFoundException(msg);
			case ExceptionType.INDEX_OUT_OF_RANGE:
				throw new IndexOutOfRangeException(msg);
			default:
				throw new Exception(msg);
		}
	}

}

}