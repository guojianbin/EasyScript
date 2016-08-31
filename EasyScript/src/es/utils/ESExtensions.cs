using System;
using System.Collections.Generic;
using Easily.Bases;
using Easily.Utility;

namespace Easily.ES {

	/// <summary>
	/// @author Easily
	/// </summary>
	public static class ESExtensions {

		public static void Invoke(this IESFunction func) {
			if (func != null) {
				func.Invoke(null);
			}
		}

		public static void Invoke(this IESFunction func, params IESObject[] args) {
			if (func != null) {
				func.Invoke(args);
			}
		}

		public static T GetValue<T>(this IExpressionRight exp, ESContext context) {
			try {
				return (T)exp.GetValue(context);
			} catch (Exception e) {
				throw new InvalidOperationException(exp.ToString(), e);
			}
		}

		public static T Cast<T>(this IExpression obj) {
			try {
				return (T)obj;
			} catch (Exception e) {
				throw new InvalidOperationException(obj.ToString(), e);
			}
		}

		public static T Cast<T>(this IESObject obj) {
			try {
				return (T)obj;
			} catch (Exception e) {
				throw new InvalidOperationException(obj.ToString(), e);
			}
		}

		public static bool IsMatch<T>(this IList<IExpression> list, int pos) where T : IExpression {
			if (pos < list.Count) {
				return list[pos] is T;
			} else {
				return false;
			}
		}

		public static void Dispose<T>(this List<T> list) where T : IDisposable {
			list.ForEach(t => t.Dispose());
			list.Clear();
		}

		public static void Dispose<T>(this Dictionary<string, T> dict) where T : IDisposable {
			dict.Values.ForEach(t => t.Dispose());
			dict.Clear();
		}

		public static string GetString(this IESObject obj) {
			return obj.Cast<IESString>().Value;
		}

		public static int GetInteger(this IESObject obj) {
			return obj.Cast<IESInteger>().Value;
		}

		public static float GetNumber(this IESObject obj) {
			return obj.Cast<IESNumber>().Value;
		}

	}

}