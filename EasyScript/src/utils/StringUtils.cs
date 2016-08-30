using System;
using System.Collections.Generic;
using System.Linq;

namespace Easily.Utility {

	/// <summary>
	/// @author Easily
	/// </summary>
	static public class StringUtils {

		private static ulong _index;

		public static string NewUID() {
			return (_index++).ToString();
		}

		static public string Format<T>(this IEnumerable<T> list) {
			return string.Format("{{ {0} }}", list.Select((t, i) => string.Format("[ {0}: {1} ]", i, t)).Join(", "));
		}

		static public string Format<T>(this IEnumerable<T> list, Func<T, string> func) {
			return string.Format("{{ {0} }}", list.Select((t, i) => string.Format("[ {0}: {1} ]", i, func(t))).Join(", "));
		}

		static public string Join(this string[] list, string sep) {
			return string.Join(sep, list);
		}

		static public string Join(this IEnumerable<string> list, string sep) {
			return list.ToArray().Join(sep);
		}

	}

}