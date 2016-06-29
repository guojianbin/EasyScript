using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Engine.DS;

namespace Easily.ES {

/// <summary>
/// @author Easily
/// </summary>
public class ESTable : ESObject, IESCollection, IESEnumerable, IESIndex, IDictionary<string, IESObject> {

	private readonly EasyMap<string, IESObject> _dict;

	public object Target {
		get { return _dict; }
	}

	public ICollection<string> Keys {
		get { return _dict.Keys; }
	}

	public ICollection<IESObject> Values {
		get { return _dict.Values; }
	}

	public int Count {
		get { return _dict.Count; }
	}

	public bool IsReadOnly {
		get { return false; }
	}

	public IESObject this[int key] {
		get { return _dict[key.ToString()]; }
		set { _dict[key.ToString()] = value; }
	}

	public IESObject this[string key] {
		get { return _dict[key]; }
		set { _dict[key] = value; }
	}

	IESObject IESIndex.this[IESObject key] {
		get { return this[key.GetString()]; }
		set { this[key.GetString()] = value; }
	}

	public ESTable() {
		_dict = new EasyMap<string, IESObject>();
	}

	public ESTable(Dictionary<string, IESObject> dict) {
		_dict = new EasyMap<string, IESObject>(dict);
	}

	public ESTable(IEnumerable dict) {
		_dict = new EasyMap<string, IESObject>();
		var list = dict.Cast<object>();
		list.ForEach(t => _dict.Add(ToString(GetValue(t, "Key")), ToVirtual(GetValue(t, "Value"))));
	}
	
	public bool TryGetValue(string key, out IESObject value) {
		return _dict.TryGetValue(key, out value);
	}

	public bool Remove(KeyValuePair<string, IESObject> item) {
		return _dict.Remove(item.Key);
	}

	public bool Remove(string key) {
		return _dict.Remove(key);
	}

	public bool ContainsKey(string key) {
		return _dict.ContainsKey(key);
	}

	public void Add(string key, object obj) {
		Add(key, ToVirtual(obj));
	}

	public void Add(string key, IESObject value) {
		_dict.Add(key, value);
	}

	public void Add(KeyValuePair<string, IESObject> item) {
		_dict.Add(item.Key, item.Value);
	}

	public void Clear() {
		_dict.Clear();
	}

	public bool Contains(KeyValuePair<string, IESObject> item) {
		return _dict.ContainsKey(item.Key);
	}

	public void CopyTo(KeyValuePair<string, IESObject>[] array, int arrayIndex) {
		throw new InvalidOperationException(ToString());
	}

	public override bool IsTrue() {
		return Count > 0;
	}

	public IEnumerator<KeyValuePair<string, IESObject>> GetEnumerator() {
		return _dict.GetEnumerator();
	}

	IEnumerator IEnumerable.GetEnumerator() {
		return GetEnumerator();
	}

	IEnumerator IESEnumerable.GetEnumerator() {
		return GetEnumerator();
	}

	public object GetObject(string key) {
		return _dict[key].ToObject();
	}

	public T GetObject<T>(string key) {
		return (T)GetObject(key);
	}

	public uint GetUInt32(string key) {
		return ToUInt32(GetFloat(key));
	}

	public int GetInt32(string key) {
		return ToInt32(GetFloat(key));
	}

	public float GetFloat(string key) {
		return _dict[key].GetNumber();
	}

	public byte GetByte(string key) {
		return ToByte(GetFloat(key));
	}

	public string GetString(string key) {
		return _dict[key].GetString();
	}

	public bool GetBoolean(string key) {
		return _dict[key].IsTrue();
	}

	public ESTable GetTable(string key) {
		return _dict[key].Cast<ESTable>();
	}

	public ESArray GetArray(string key) {
		return _dict[key].Cast<ESArray>();
	}

	public override IESObject GetProperty(string name) {
		return ESUtility.GetProperty(this, name);
	}

	protected override void OnDispose() {
		base.OnDispose();
		_dict.Dispose();
	}

	public override string ToString() {
		return string.Format("ESTable Values: {0}", _dict);
	}

}

}