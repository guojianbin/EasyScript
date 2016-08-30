using System;
using System.Collections;
using System.Collections.Generic;
using Easily.Bases;

namespace Engine.DS {

/// <summary>
/// @author Easily
/// </summary>
public class EasyMap<TKey, TValue> : Disposable, IDictionary<TKey, TValue> {

	private readonly Dictionary<TKey, TValue> _objects;

	public bool IsReadOnly {
		get { return false; }
	}

	public int Count {
		get { return _objects.Count; }
	}

	public ICollection<TValue> Values {
		get { return _objects.Values; }
	}

	public ICollection<TKey> Keys {
		get { return _objects.Keys; }
	}

	public TValue this[TKey key] {
		get { return GetValue(key); }
		set { _objects[key] = value; }
	}

	public EasyMap() {
		_objects = new Dictionary<TKey, TValue>();
	}

	public EasyMap(Dictionary<TKey, TValue> objects) {
		_objects = objects;
	}

	public void Add(TKey key, TValue value) {
		if (!ContainsKey(key)) {
			_objects.Add(key, value);
		} else {
			ThrowException(ExceptionType.ARGUMENT, string.Format("key={0} already exist.", key));
		}
	}

	public void Add(KeyValuePair<TKey, TValue> item) {
		Add(item.Key, item.Value);
	}

	public bool Contains(KeyValuePair<TKey, TValue> item) {
		return ContainsKey(item.Key);
	}

	public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex) {
		throw new InvalidOperationException(ToString());
	}

	public bool ContainsKey(TKey key) {
		return _objects.ContainsKey(key);
	}

	public bool Remove(TKey key) {
		return _objects.Remove(key);
	}

	public void Remove(TKey key, Action<TValue> callback) {
		var value = _objects[key];
		Remove(key);
		callback(value);
	}

	public bool Remove(KeyValuePair<TKey, TValue> item) {
		return Remove(item.Key);
	}

	public void Clear() {
		_objects.Clear();
	}

	public bool TryGetValue(TKey key, out TValue value) {
		return _objects.TryGetValue(key, out value);
	}

	public TValue GetValue(TKey key) {
		TValue value;
		if (TryGetValue(key, out value)) {
			return value;
		} else {
			ThrowException(ExceptionType.KEY_NOT_FOUND, key.ToString());
			return default(TValue);
		}
	}

	public bool TryGetValue<TResult>(TKey key, out TResult result) where TResult : class {
		TValue value;
		if (TryGetValue(key, out value)) {
			result = value as TResult;
			return true;
		} else {
			result = default(TResult);
			return false;
		}
	}

	public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator() {
		return _objects.GetEnumerator();
	}

	IEnumerator IEnumerable.GetEnumerator() {
		return GetEnumerator();
	}

	protected override void OnDispose() {
		base.OnDispose();
		Values.Dispose();
		Clear();
	}

	public override string ToString() {
		return string.Format("EasyMap Objects: {0}", _objects.Format());
	}

}

}