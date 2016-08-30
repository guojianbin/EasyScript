using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Easily.Bases;

/// <summary>
/// @author Easily
/// </summary>
static public class Extensions {

	public static void Swap<T>(this IList<T> list, int i1, int i2) {
		var temp = list[i1];
		list[i1] = list[i2];
		list[i2] = temp;
	}

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

	public static void Dispose<T>(this IEnumerable<T> list) {
		if (typeof(IDisposable).IsAssignableFrom(typeof(T))) {
			list.Cast<IDisposable>().ForEach(t => t.Dispose());
		}
	}

	public static IEnumerator Join(this Disposable obj) {
		while (obj.IsEnabled) {
			yield return null;
		}
	}

	static public R Reduce<T, R>(this IEnumerable<T> list, R current, Func<R, T, R> next, Predicate<R> predicate) {
		var iter = list.GetEnumerator();
		while (true) {
			if (!iter.MoveNext()) {
				return current;
			} 
			var temp = current;
			current = next(current, iter.Current);
			if (!predicate(current)) {
				return temp;
			}
		}
	}

}

