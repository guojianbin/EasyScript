using System;
using System.Collections.Generic;

namespace Engine.DS {

/// <summary>
/// @author Easily
/// </summary>
public class BinaryHeap<TKey, TValue> where TKey : IComparable<TKey> {

	private const int defaultCapacity = 64;
	private int _count;
	private KeyValuePair<TKey, TValue>[] _items;
	private readonly KeyValuePair<TKey, TValue> EmptyItem = new KeyValuePair<TKey, TValue>();

	public int Count {
		get { return _count; }
	}

	public bool IsEmpty {
		get { return _count == 0; }
	}

	public BinaryHeap() : this(defaultCapacity) {
		// ignored
	}

	public BinaryHeap(int capacity) {
		_items = new KeyValuePair<TKey, TValue>[capacity];
	}

	public void Clear() {
		_count = 0;
	}

	public bool Enqueue(TKey key, TValue item) {
		if (_count == _items.Length) {
			Resize(_items.Length * 2);
		}

		_items[_count++] = new KeyValuePair<TKey, TValue>(key, item);
		var position = BubbleUp(_count - 1);

		return (position == 0);
	}

	public KeyValuePair<TKey, TValue> Dequeue() {
		return Dequeue(true);
	}

	private KeyValuePair<TKey, TValue> Dequeue(bool shrink) {
		var result = _items[0];

		if (_count == 1) {
			_count = 0;
			_items[0] = EmptyItem;
		} else {
			--_count;
			_items[0] = _items[_count];
			_items[_count] = EmptyItem;
			BubbleDown(0);
		}

		if (shrink) {
			Shrink();
		}

		return result;
	}

	public KeyValuePair<TKey, TValue> Peek() {
		return _items[0];
	}

	public ICollection<KeyValuePair<TKey, TValue>> RemoveAll(Predicate<KeyValuePair<TKey, TValue>> func) {
		ICollection<KeyValuePair<TKey, TValue>> result = new List<KeyValuePair<TKey, TValue>>();
		for (var i = 0; i < _count; i++) {
			while (func(_items[i]) && i < _count) {
				result.Add(_items[i]);
				var last = _count - 1;
				while (func(_items[last]) && i < last) {
					result.Add(_items[last]);
					_items[last] = EmptyItem;
					--last;
				}

				_items[i] = _items[last];
				_items[last] = EmptyItem;
				_count = last;
				if (i < last) {
					BubbleDown(BubbleUp(i));
				}
			}
		}
		Shrink();
		return result;
	}

	private void Shrink() {
		if (_items.Length > defaultCapacity && _count < (_items.Length >> 1)) {
			var newSize = Math.Max(defaultCapacity, (((_count / defaultCapacity) + 1) * defaultCapacity));
			Resize(newSize);
		}
	}

	public ICollection<KeyValuePair<TKey, TValue>> TakeWhile(Predicate<TKey> func) {
		ICollection<KeyValuePair<TKey, TValue>> result = new List<KeyValuePair<TKey, TValue>>();

		while (!IsEmpty && func(Peek().Key)) {
			result.Add(Dequeue(false));
		}

		Shrink();

		return result;
	}

	private void Resize(int newSize) {
		var temp = new KeyValuePair<TKey, TValue>[newSize];
		Array.Copy(_items, 0, temp, 0, _count);
		_items = temp;
	}

	private void BubbleDown(int startIndex) {
		var currentPos = startIndex;
		var swapPos = startIndex;

		while (true) {
			var leftChild = (currentPos << 1) + 1;
			var rightChild = leftChild + 1;

			if (leftChild < _count) {
				if (_items[currentPos].Key.CompareTo(_items[leftChild].Key) > 0) {
					swapPos = leftChild;
				}
			} else {
				break;
			}

			if (rightChild < _count) {
				if (_items[swapPos].Key.CompareTo(_items[rightChild].Key) > 0) {
					swapPos = rightChild;
				}
			}

			if (currentPos != swapPos) {
				var temp = _items[currentPos];
				_items[currentPos] = _items[swapPos];
				_items[swapPos] = temp;
			} else {
				break;
			}

			currentPos = swapPos;
		}
	}

	private int BubbleUp(int startIndex) {
		while (startIndex > 0) {
			var parent = (startIndex - 1) >> 1;
			if (_items[parent].Key.CompareTo(_items[startIndex].Key) > 0) {
				var temp = _items[startIndex];
				_items[startIndex] = _items[parent];
				_items[parent] = temp;
			} else {
				break;
			}
			startIndex = parent;
		}
		return startIndex;
	}

}

}