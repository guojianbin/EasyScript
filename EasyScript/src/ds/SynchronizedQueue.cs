using System.Collections.Generic;

namespace Engine.Bases {

/// <summary>
/// @author Easily
/// </summary>
public class SynchronizedQueue<T> : Disposable {

	private readonly Queue<T> _queue = new Queue<T>();
	private readonly object _syncRoot = new object();

	public int Count {
		get {
			lock (_syncRoot) {
				return _queue.Count;
			}
		}
	}

	public void Enqueue(T item) {
		lock (_syncRoot) {
			_queue.Enqueue(item);
		}
	}

	public T Dequeue() {
		lock (_syncRoot) {
			return _queue.Dequeue();
		}
	}

	public T Peek() {
		lock (_syncRoot) {
			return _queue.Peek();
		}
	}

	public void Clear() {
		lock (_syncRoot) {
			_queue.Clear();
		}
	}

}

}