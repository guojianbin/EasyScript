using System;
using System.Collections;
using System.Collections.Generic;

namespace Easily.ES {

	/// <summary>
	/// @author Easily
	/// </summary>
	public sealed class ESInstance : ESObject, IESTable, IDictionary<string, IESObject> {

		private readonly Dictionary<string, IESObject> _dict = new Dictionary<string, IESObject>();

		public ICollection<string> Keys {
			get { return _dict.Keys; }
		}

		public ICollection<IESObject> Values {
			get { return _dict.Values; }
		}

		public bool IsReadOnly {
			get { return false; }
		}

		public int Count {
			get { return _dict.Count; }
		}

		public IESObject this[string key] {
			get { return _dict[key]; }
			set { _dict[key] = value; }
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
			return new ESKey(this, name);
		}

		protected override void OnDispose() {
			base.OnDispose();
			_dict.Clear();
		}

		public override string ToString() {
			return string.Format("ESInstance Count: {0}", Count);
		}

	}

}