using System;
using System.Collections;
using System.Collections.Generic;
using Engine.Bases;

namespace Engine.DS {

/// <summary>
/// @author Easily
/// </summary>
public class EasyList<T> : Disposable, IList<T> {

	private readonly List<T> _list = new List<T>();

	public bool IsReadOnly {
		get { return false; }
	}

	public int Count {
		get { return _list.Count; }
	}

	public T this[int index] {
		get { return _list[index]; }
		set { _list[index] = value; }
	}

	public EasyList() {
		// ignored
	}

	public EasyList(IEnumerable<T> list) {
		AddRange(list);
	}

	public EasyList(params T[] array) {
		AddRange(array);
	}

	public void Add(T item) {
		_list.Add(item);
	}

	public void AddRange(params T[] arr) {
		_list.AddRange(arr);
	}

	public void AddRange(IEnumerable<T> list) {
		_list.AddRange(list);
	}

	public void Clear() {
		_list.Clear();
	}

	public bool Contains(T item) {
		return _list.Contains(item);	
	}

	public void CopyTo(T[] array, int arrayIndex) {
		_list.CopyTo(array, arrayIndex);
	}

	public int IndexOf(T item) {
		return _list.IndexOf(item);	
	}

	public int FindIndex(Predicate<T> match) {
		return _list.FindIndex(match);
	}

	public void Insert(int index, T item) {
		_list.Insert(index, item);
	}

	public bool Remove(T item) {
		return _list.Remove(item);	
	}

	public void RemoveAt(int index) {
		_list.RemoveAt(index);	
	}

	public void Remove(Predicate<T> match) {
		Remove(Count - 1, match);	
	}

	public void Remove(Predicate<T> match, Action<T> callback) {
		Remove(Count - 1, match, callback);
	}

	private void Remove(int startIndex, Predicate<T> match) {
		if (startIndex == -1) {
			return;
		} 
		var lastIndex = _list.FindLastIndex(startIndex, match);
		if (lastIndex == -1) {
			return;
		}
		RemoveAt(lastIndex);
		Remove(lastIndex - 1, match);
	}

	private void Remove(int startIndex, Predicate<T> match, Action<T> callback) {
		if (startIndex == -1) {
			return;
		} 
		var lastIndex = _list.FindLastIndex(startIndex, match);
		if (lastIndex == -1) {
			return;
		}
		callback(_list[lastIndex]);
		RemoveAt(lastIndex);
		Remove(lastIndex - 1, match, callback);
	}

	public IEnumerator<T> GetEnumerator() {
		return _list.GetEnumerator();	
	}

	IEnumerator IEnumerable.GetEnumerator() {
		return GetEnumerator();
	}

	protected override void OnDispose() {
		base.OnDispose();
		_list.Dispose();
		Clear();
	}

	public override string ToString() {
		return string.Format("EasyList Objects: {0}", _list.Format());
	}

}

}