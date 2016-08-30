using System;
using System.Collections;
using System.Collections.Generic;
using Easily.Bases;

namespace Engine.DS {

/// <summary>
/// @author Easily
/// </summary>
public class EasyQueue<T> : Disposable, IEnumerable<T> {

	private readonly LinkedList<T> _queue = new LinkedList<T>();

	public int Count {
		get { return _queue.Count; }
	}

	public EasyQueue() {
		// ignored
	}

	public EasyQueue(IEnumerable<T> list) {
		EnqueueRange(list);
	}

	public EasyQueue(params T[] array) {
		EnqueueRange(array);
	}

	public void Enqueue(T item) {
		_queue.AddLast(item);
	}

	public void EnqueueRange(IEnumerable<T> list) {
		list.ForEach(Enqueue);
	}

	public T Peek() {
		return _queue.First.Value;
	}

	public T Dequeue() {
		if (Count > 0) {
			var value = _queue.First.Value;
			_queue.RemoveFirst();
			return value;	
		} else {
			throw new IndexOutOfRangeException("count is 0");
		}
	}

	public T[] DequeueRange(int count) {
		var array = new T[count];
		for (var i = 0; i < count; i++) {
			array[i] = Dequeue();
		}
		return array;
	}

	public bool Contains(T item) {
		return _queue.Contains(item);
	}

	public void Remove(T item) {
		_queue.Remove(item);
	}

	public void Clear() {
		_queue.Clear();
	}

	public IEnumerator<T> GetEnumerator() {
		return _queue.GetEnumerator();
	}

	IEnumerator IEnumerable.GetEnumerator() {
		return GetEnumerator();
	}

	protected override void OnDispose() {
		base.OnDispose();
		_queue.Dispose();
		Clear();
	}

}

}