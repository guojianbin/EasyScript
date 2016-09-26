using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Easily.Utility;

namespace Easily.ES {

	/// <summary>
	/// @author Easily
	/// </summary>
	public sealed class ESTable : ESObject, IESTable, IESIndex, IDictionary<string, IESObject> {

		private readonly Dictionary<string, IESObject> _dict;

		public object Target {
			get { return _dict; }
		}

		public bool IsReadOnly {
			get { return false; }
		}

		public int Count {
			get { return _dict.Count; }
		}

		public ICollection<string> Keys {
			get { return _dict.Keys; }
		}

		public ICollection<IESObject> Values {
			get { return _dict.Values; }
		}

		public IESObject this[int key] {
			get { return _dict[key.ToString()]; }
			set { _dict[key.ToString()] = value; }
		}

		public IESObject this[IESObject key] {
			get { return this[key.GetString()]; }
			set { this[key.GetString()] = value; }
		}

		public IESObject this[string key] {
			get { return _dict[key]; }
			set { _dict[key] = value; }
		}

		public ESTable() {
			_dict = new Dictionary<string, IESObject>();
		}

		public ESTable(IDictionary<string, IESObject> dict) {
			_dict = new Dictionary<string, IESObject>(dict);
		}

		public ESTable(IEnumerable dict) {
			_dict = new Dictionary<string, IESObject>();
			dict.Cast<object>().ForEach(t => _dict.Add(ToString(GetValue(t, "Key")), ToVirtual(GetValue(t, "Value"))));
		}

		public void Add(string key, object obj) {
			Add(key, ToVirtual(obj));
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
			return _dict[key].ToBoolean();
		}

		public ESTable GetTable(string key) {
			return _dict[key].Cast<ESTable>();
		}

		public ESArray GetArray(string key) {
			return _dict[key].Cast<ESArray>();
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

		public IEnumerator<KeyValuePair<string, IESObject>> GetEnumerator() {
			return _dict.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator() {
			return GetEnumerator();
		}

		public override bool ToBoolean() {
			return Count > 0;
		}

		IEnumerator IESEnumerable.GetEnumerator() {
			return GetEnumerator();
		}

		public override IESObject GetProperty(string name) {
			return ESUtility.GetProperty(this, name);
		}

		protected override void OnDispose() {
			base.OnDispose();
			_dict.Clear();
		}

		public override string ToString() {
			return string.Format("ESTable Count: {0}", Count);
		}

	}

}