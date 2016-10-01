using System;
using System.Collections.Generic;
using System.Linq;

namespace Easily.Utility {

	/// <summary>
	/// @author Easily
	/// </summary>
	public static class StringUtils {

		private static ulong _index;

		public static string NewUID() {
			return (_index++).ToString();
		}

		public static string Format<T>(this IEnumerable<T> list) {
			return string.Format("{{ {0} }}", list.Select((t, i) => string.Format("[ {0}: {1} ]", i, t)).Join(", "));
		}

		public static string Format<T>(this IEnumerable<T> list, Func<T, string> func) {
			return string.Format("{{ {0} }}", list.Select((t, i) => string.Format("[ {0}: {1} ]", i, func(t))).Join(", "));
		}

		public static string Join(this string[] list, string sep) {
			return string.Join(sep, list);
		}

		public static string Join(this IEnumerable<string> list, string sep) {
			return list.ToArray().Join(sep);
		}

	}

}