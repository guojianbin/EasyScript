using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Easily.ES {

/// <summary>
/// @author Easily
/// </summary>
static public class ESUtility {

	private static readonly HashSet<Type> _numTypes = new HashSet<Type>(new[] { typeof(float), typeof(double), typeof(int), typeof(uint), typeof(long), typeof(ulong), typeof(byte), typeof(sbyte), typeof(char), typeof(short), typeof(ushort) });
	private static readonly IESObject[] _0Vtrs = new IESObject[0];
	private static readonly object[] _0Objs = new object[0];

	private const BindingFlags _instance = BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance;
	private const BindingFlags _static = BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static;

	public static IESObject GetProperty(object obj, string name) {
		var type = obj.GetType();
		var field = type.GetField(name, _instance);
		if (field != null) {
			return new ESField(obj, field);
		}
		var prop = type.GetProperty(name, _instance);
		if (prop != null) {
			return new ESProperty(obj, prop);
		}
		var func = type.GetMethod(name, _instance);
		if (func != null) {
			return new ESMethod(obj, func);
		} else {
			throw new InvalidOperationException(string.Format("Target: {0}, Name: {1}", obj, name));
		}
	}

	public static IESObject GetProperty(Type type, string name) {
		var field = type.GetField(name, _static);
		if (field != null) {
			return new ESField(field);
		}
		var prop = type.GetProperty(name, _static);
		if (prop != null) {
			return new ESProperty(prop);
		}
		var func = type.GetMethod(name, _static);
		if (func != null) {
			return new ESMethod(func);
		} else {
			throw new InvalidOperationException(string.Format("Type: {0}, Name: {1}", type, name));
		}
	}

	public static Func<object, object> GetConvert(Type type) {
		if (type == typeof(int)) {
			return v => ToInt32(v);
		} else if (type == typeof(uint)) {
			return v => ToUInt32(v);
		} else if (type == typeof(long)) {
			return v => ToInt64(v);
		} else if (type == typeof(ulong)) {
			return v => ToUInt64(v);
		} else if (type == typeof(short)) {
			return v => ToInt16(v);
		} else if (type == typeof(ushort)) {
			return v => ToUInt16(v);
		} else {
			throw new InvalidOperationException(type.ToString());
		}
	}

	public static Type GetType(object target, string name) {
		var type = target.GetType();
		var field = type.GetField(name, _instance);
		if (field != null) {
			return field.FieldType;
		}
		var prop = type.GetProperty(name, _instance);
		if (prop != null) {
			return prop.PropertyType;
		} else {
			throw new InvalidOperationException(string.Format("Target: {0}, Name: {1}", target, name));
		}
	}

	public static object GetValue(object obj, string name) {
		var type = obj.GetType();
		var field = type.GetField(name, _instance);
		if (field != null) {
			return field.GetValue(obj);
		}
		var prop = type.GetProperty(name, _instance);
		if (prop != null) {
			return prop.GetValue(obj, null);
		} else {
			throw new InvalidOperationException(string.Format("Target: {0}, Name: {1}", obj, name));
		}
	}

	public static void SetValue(object obj, string name, object value) {
		var type = obj.GetType();
		var field = type.GetField(name, _instance);
		if (field != null) {
			field.SetValue(obj, value);
			return;
		}
		var prop = type.GetProperty(name, _instance);
		if (prop != null) {
			prop.SetValue(obj, value, null);
		} else {
			throw new InvalidOperationException(string.Format("Target: {0}, Name: {1}", obj, name));
		}
	}

	public static IESObject ToVirtual(object obj) {
		if (obj == null) {
			return ESDefault.Value;
		} else if (obj is IESObject) {
			return (IESObject)obj;
		} else if (obj is bool) {
			return new ESBoolean((bool)obj);
		} else if (obj is string) {
			return new ESString((string)obj);
		} else if (obj is Delegate) {
			return new ESDelegate((Delegate)obj);
		} else if (obj is Type) {
			return new ESType((Type)obj);
		} else if (_numTypes.Contains(obj.GetType())) {
			return new ESNumber(obj);
		} else {
			return new ESSprite(obj);
		}
	}

	public static IESObject[] ToVirtuals(object[] objs, int count) {
		if (count == 0) {
			return _0Vtrs;
		}
		var arr = new IESObject[count];
		if (objs == null || objs.Length == 0) {
			return arr;
		}
		var len = Math.Min(objs.Length, count);
		for (var i = 0; i < len; i++) {
			arr[i] = ToVirtual(objs[i]);
		}
		return arr;
	}

	public static IESObject[] ToVirtuals(object[] objs) {
		if (objs != null) {
			return ToVirtuals(objs, objs.Length);
		} else {
			return _0Vtrs;
		}
	}

	public static IESObject[] ToVirtuals<T>(IEnumerable<T> list) {
		if (list != null) {
			return ToVirtuals(list.Cast<object>().ToArray());
		} else {
			return _0Vtrs;
		}
	}

	public static IESObject[] ToVirtuals(IEnumerable list) {
		if (list != null) {
			return ToVirtuals(list.Cast<object>());
		} else {
			return _0Vtrs;
		}
	}

	public static object[] ToObjects(IESObject[] objs, int count) {
		if (count == 0) {
			return _0Objs;
		}
		var arr = new object[count];
		if (objs == null || objs.Length == 0) {
			return arr;
		}
		var len = Math.Min(objs.Length, count);
		for (var i = 0; i < len; i++) {
			arr[i] = objs[i].ToObject();
		}
		return arr;
	}

	public static IESObject GetProperty(ESArray array, string name) {
		if (name == "add") {
			return ToVirtual(new Action<object>(v => array.Add(ToVirtual(v))));
		} else if (name == "remove") {
			return ToVirtual(new Action<float>(i => array.RemoveAt((int)i)));
		} else if (name == "insert") {
			return ToVirtual(new Action<float, object>((i, o) => array.Insert((int)i, ToVirtual(o))));
		} else if (name == "clear") {
			return ToVirtual(new Action(array.Clear));
		} else if (name == "count") {
			return new ESSprite(array.Count);
		} else {
			return GetProperty(array.Source, name);
		}
	}

	public static IESObject GetProperty(ESTable table, string name) {
		if (name == "add") {
			return ToVirtual(new Action<string, object>((k, v) => table.Add(k, ToVirtual(v))));
		} else if (name == "remove") {
			return ToVirtual(new Action<string>(k => table.Remove(k)));
		} else if (name == "clear") {
			return ToVirtual(new Action(table.Clear));
		} else if (name == "keys") {
			return ToVirtual(table.Keys);
		} else if (name == "values") {
			return ToVirtual(table.Values);
		} else if (name == "count") {
			return new ESNumber(table.Count);
		} else {
			return new ESKey(table, name);
		}
	}

	public static ESArray ToList(object obj) {
		if (obj is ESArray) {
			return (ESArray)obj;
		} else if (obj is IEnumerable) {
			return new ESArray((IEnumerable)obj);
		} else if (obj is ESSprite) {
			return new ESArray(((ESSprite)obj).GetValue<IEnumerable>());
		} else {
			throw new InvalidCastException(obj.ToString());
		}
	}

	public static ESTable ToTable(object obj) {
		if (obj is ESTable) {
			return (ESTable)obj;
		} else if (obj is IEnumerable) {
			return new ESTable((IEnumerable)obj);
		} else if (obj is ESSprite) {
			return new ESTable(((ESSprite)obj).GetValue<IEnumerable>());
		} else {
			throw new InvalidCastException(obj.ToString());
		}
	}

	public static byte ToByte(object value) {
		return Convert.ToByte(value);
	}

	public static byte ToByte(string value) {
		return Convert.ToByte(value);
	}

	public static float ToFloat(object value) {
		return Convert.ToSingle(value);
	}

	public static float ToFloat(string value) {
		return Convert.ToSingle(value);
	}

	public static float ToFloat(int value) {
		return Convert.ToSingle(value);
	}

	public static short ToInt16(object value) {
		return Convert.ToInt16(value);
	}

	public static ushort ToUInt16(object value) {
		return Convert.ToUInt16(value);
	}

	public static int ToInt32(object value) {
		return Convert.ToInt32(value);
	}

	public static int ToInt32(float value) {
		return Convert.ToInt32(value);
	}

	public static int ToInt32(string value) {
		return Convert.ToInt32(value);
	}

	public static uint ToUInt32(object value) {
		return Convert.ToUInt32(value);
	}

	public static uint ToUInt32(float value) {
		return Convert.ToUInt32(value);
	}

	public static uint ToUInt32(string value) {
		return Convert.ToUInt32(value);
	}

	public static long ToInt64(object value) {
		return Convert.ToInt64(value);
	}

	public static ulong ToUInt64(object value) {
		return Convert.ToUInt64(value);
	}

	public static string ToString(object value) {
		return Convert.ToString(value);
	}

}

}