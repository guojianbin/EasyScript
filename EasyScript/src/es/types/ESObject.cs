using System;
using System.Collections;
using System.Collections.Generic;
using Engine.Bases;

namespace Engine.ES {

/// <summary>
/// @author Easily
/// </summary>
public class ESObject : Disposable, IESObject {

	public virtual bool IsTrue() {
		return true;
	}

	public virtual IESObject Clone() {
		return this;
	}

	public T ToObject<T>() {
		return (T)ToObject();
	}

	public virtual object ToObject() {
		return this;
	}

	public virtual IESObject GetProperty(string name) {
		return GetProperty(this, name);
	}

	public static IESObject GetProperty(object obj, string name) {
		return ESUtility.GetProperty(obj, name);
	}

	public static IESObject GetProperty(Type type, string name) {
		return ESUtility.GetProperty(type, name);
	}

	public static Func<object, object> GetConvert(Type type) {
		return ESUtility.GetConvert(type);
	}

	public static Type GetType(object target, string name) {
		return ESUtility.GetType(target, name);
	}

	public static object GetValue(object obj, string name) {
		return ESUtility.GetValue(obj, name);
	}

	public static IESObject ToVirtual(object obj) {
		return ESUtility.ToVirtual(obj);
	}

	public static IESObject[] ToVirtuals(object[] objs, int count) {
		return ESUtility.ToVirtuals(objs, count);
	}

	public static IESObject[] ToVirtuals<T>(IEnumerable<T> list) {
		return ESUtility.ToVirtuals(list);
	}

	public static IESObject[] ToVirtuals(IEnumerable list) {
		return ESUtility.ToVirtuals(list);
	}

	public static object[] ToObjects(IESObject[] objs, int count) {
		return ESUtility.ToObjects(objs, count);
	}

	public static byte ToByte(object value) {
		return ESUtility.ToByte(value);
	}

	public static byte ToByte(string value) {
		return ESUtility.ToByte(value);
	}

	public static float ToFloat(object value) {
		return ESUtility.ToFloat(value);
	}

	public static float ToFloat(string value) {
		return ESUtility.ToFloat(value);
	}

	public static float ToFloat(int value) {
		return ESUtility.ToFloat(value);
	}

	public static int ToInt32(object value) {
		return ESUtility.ToInt32(value);
	}

	public static int ToInt32(float value) {
		return ESUtility.ToInt32(value);
	}

	public static int ToInt32(string value) {
		return ESUtility.ToInt32(value);
	}

	public static uint ToUInt32(object value) {
		return ESUtility.ToUInt32(value);
	}

	public static uint ToUInt32(float value) {
		return ESUtility.ToUInt32(value);
	}

	public static uint ToUInt32(string value) {
		return ESUtility.ToUInt32(value);
	}

	public static string ToString(object value) {
		return ESUtility.ToString(value);
	}

}

}