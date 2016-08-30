using System;
using System.Collections.Generic;

namespace Easily.Utility {

	/// <summary>
	/// @author Easily
	/// </summary>
	public static class Extensions {

		public static void ForEach<T>(this IEnumerable<T> list, Action<T> callback) {
			var iter = list.GetEnumerator();
			while (iter.MoveNext()) {
				callback(iter.Current);
			}
		}

		public static void ForEach<T>(this IEnumerable<T> list, Action<int, T> callback) {
			var index = 0;
			var iter = list.GetEnumerator();
			while (iter.MoveNext()) {
				callback(index++, iter.Current);
			}
		}

	}

}