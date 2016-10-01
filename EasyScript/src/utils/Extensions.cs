using System;
using System.Collections.Generic;

namespace Easily.Utility {

	/// <summary>
	/// @author Easily
	/// </summary>
	public static class Extensions {

		public static void ForEach<T>(this IEnumerable<T> list, Action<T> callback) {
		    foreach (var t in list) {
		        callback(t);
		    }
		}

		public static void ForEach<T>(this IEnumerable<T> list, Action<int, T> callback) {
			var i = 0;
            foreach (var t in list) {
                callback(i++, t);
            }
        }

	}

}