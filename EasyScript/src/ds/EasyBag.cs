using System.Collections;
using System.Collections.Generic;
using Engine.Bases;

namespace Engine.DS {

/// <summary>
/// @author Easily
/// </summary>
public class EasyBag<TKey, TValue> : Disposable, IDictionary<TKey, TValue> {

	private readonly EasyMap<TKey, TValue> _objects = new EasyMap<TKey, TValue>();
	private readonly EasyList<TValue> _list = new EasyList<TValue>();

	public int Count {
		get { return _list.Count; }
	}

	public bool IsReadOnly {
		get { return false; }
	}

	public TValue this[TKey key] {
		get { return _objects[key]; }
		set { _objects[key] = value; }
	}

	public ICollection<TKey> Keys {
		get { return _objects.Keys; }
	}

	public ICollection<TValue> Values {
		get { return _list; }
	}

	public void Add(KeyValuePair<TKey, TValue> item) {
		_list.Add(item.Value);
		_objects.Add(item);
	}

	public void Clear() {
		_list.Clear();
		_objects.Clear();
	}

	public bool Contains(KeyValuePair<TKey, TValue> item) {
		return _objects.Contains(item);
	}

	public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex) {
		_objects.CopyTo(array, arrayIndex);
	}

	public bool Remove(KeyValuePair<TKey, TValue> item) {
		_list.Remove(item.Value);
		return _objects.Remove(item);
	}

	public void Add(TKey key, TValue value) {
		_list.Add(value);
		_objects.Add(key, value);
	}

	public bool ContainsKey(TKey key) {
		return _objects.ContainsKey(key);
	}

	public bool TryGetValue(TKey key, out TValue value) {
		return _objects.TryGetValue(key, out value);
	}

	public bool Remove(TKey key) {
		TValue value;
		if (_objects.TryGetValue(key, out value)) {
			_list.Remove(value);
			return _objects.Remove(key);
		} else {
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
		_list.Dispose();
		_objects.Dispose();
	}

}

}