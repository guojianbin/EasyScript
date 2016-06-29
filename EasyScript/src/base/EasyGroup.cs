using System;
using System.Collections;
using System.Collections.Generic;

namespace Engine.Bases {

/// <summary>
/// @author Easily
/// </summary>
public class EasyGroup<T> : Disposable, IEnumerable<T> where T : class {

	private readonly List<T> _items = new List<T>();
	private Action<T, bool> _onFocus;
	private T _current;

	public T this[int index] {
		get { return _items[index]; }
	}

	public T Current {
		get { return _current; }
		set { SetFocus(value); }
	}

	public EasyGroup(Action<T, bool> onFocus) {
		SetFocus(onFocus);
	}

	public void SetFocus(Action<T, bool> onFocus) {
		_onFocus = onFocus;
	}

	public void SetFocus(int index) {
		Current = _items[index];
	}

	public void SetFocus(T obj) {
		_current = obj;
		_items.ForEach(t => _onFocus(t, t == _current));
	}

	public void LostFocus() {
		Current = default(T);
	}

	public void Add(T obj) {
		_items.Add(obj);
	}

	public void AddRange(params T[] items) {
		_items.AddRange(items);
	}

	public void AddRange(IEnumerable<T> items) {
		_items.AddRange(items);
	}

	public void Clear() {
		_items.Clear();
	}

	public IEnumerator<T> GetEnumerator() {
		return _items.GetEnumerator();
	}

	IEnumerator IEnumerable.GetEnumerator() {
		return GetEnumerator();
	}

	protected override void OnDispose() {
		base.OnDispose();
		_items.Clear();
	}

}

}