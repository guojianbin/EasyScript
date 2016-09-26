using System.Reflection;
using System.Runtime.Serialization;

namespace Easily.Bases {

	/// <summary>
	/// @author Easily
	/// </summary>
	public static class EasyDefault {

		private static readonly FieldInfo _disposed = typeof(Disposable).GetField("_disposed", BindingFlags.NonPublic | BindingFlags.Instance);

		public static void Dispose<T>(T value) where T :Disposable {
			_disposed.SetValue(value, true);
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
					EasyDefault.Dispose(_value);
					return _value;
				} else {
					return _value;
				}
			}
		}

	}

}