using System.Runtime.Serialization;

namespace Engine.Bases {


/// <summary>
/// @author Easily
/// </summary>
public static class EasyDefault {

	public static void Empty() {
		// ignored
	}

}

/// <summary>
/// @author Easily
/// </summary>
public static class EasyDefault<T> where T : Disposable {

	private static T _value;

	public static T Value {
		get {
			if (_value == null) {
				_value = (T)FormatterServices.GetUninitializedObject(typeof(T));
				_value.Dispose(true);
				return _value;
			} else {
				return _value;
			}
		}
	}

}

}