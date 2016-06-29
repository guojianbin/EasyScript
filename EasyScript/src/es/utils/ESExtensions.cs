using Engine.Bases;

namespace Easily.ES {

/// <summary>
/// @author Easily
/// </summary>
static public class ESExtensions {

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

	public static T GetValue<T>(this IRightExpression exp, ESDomain domain) {
		return (T)exp.GetValue(domain);
	}

	public static T Cast<T>(this IESObject obj) {
		return (T)obj;
	}

	public static T Cast<T>(this IExpression obj) {
		return (T)obj;
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