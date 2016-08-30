using System.Collections.Generic;

namespace Easily.Bases {

/// <summary>
/// @author Easily
/// </summary>
public class EasyRedis : Disposable {

	private readonly Dictionary<string, int> _intDatas = new Dictionary<string, int>();
	private readonly Dictionary<string, uint> _uintDatas = new Dictionary<string, uint>();
	private readonly Dictionary<string, float> _floatDatas = new Dictionary<string, float>();
	private readonly Dictionary<string, string> _strDatas = new Dictionary<string, string>();
	private readonly Dictionary<string, object> _objDatas = new Dictionary<string, object>();

	public void Write(string key, int value) {
		_intDatas[key] = value;
	}

	public void Write(string key, uint value) {
		_uintDatas[key] = value;
	}

	public void Write(string key, float value) {
		_floatDatas[key] = value;
	}

	public void Write(string key, string value) {
		_strDatas[key] = value;
	}

	public void Write(string key, object value) {
		_objDatas[key] = value;
	}

	public int ReadInt32(string key) {
		return _intDatas[key];
	}

	public uint ReadUInt32(string key) {
		return _uintDatas[key];
	}

	public float ReadFloat(string key) {
		return _floatDatas[key];
	}

	public string ReadString(string key) {
		return _strDatas[key];
	}

	public object ReadObject(string key) {
		return _objDatas[key];
	}

	public T ReadObject<T>(string key) {
		return (T)_objDatas[key];
	}

	protected override void OnDispose() {
		base.OnDispose();
		_intDatas.Clear();
		_uintDatas.Clear();
		_floatDatas.Clear();
		_strDatas.Clear();
	}

}

}