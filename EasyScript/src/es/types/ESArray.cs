using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Easily.ES {

	/// <summary>
	/// @author Easily
	/// </summary>
	public sealed class ESArray : ESObject, IESCollection, IESIndex, IList<IESObject> {

		private readonly List<IESObject> _list;

		public IList<IESObject> Source {
			get { return _list; }
		}

		public int Count {
			get { return _list.Count; }
		}

		public object Target {
			get { return _list; }
		}

		public bool IsReadOnly {
			get { return false; }
		}

		IESObject IESIndex.this[IESObject index] {
			get { return this[index.GetInteger()]; }
			set { this[index.GetInteger()] = value; }
		}

		public IESObject this[int index] {
			get { return _list[index]; }
			set { _list[index] = value; }
		}

		public ESArray() {
			_list = new List<IESObject>();
		}

		public ESArray(IEnumerable<IESObject> list) {
			_list = new List<IESObject>(list);
		}

		public ESArray(params IESObject[] array) {
			_list = new List<IESObject>(array);
		}

		public ESArray(IEnumerable list) {
			_list = new List<IESObject>(ToVirtuals(list).ToList());
		}

		public object GetObject(int index) {
			return _list[index].ToObject();
		}

		public T GetObject<T>(int index) {
			return (T)GetObject(index);
		}

		public uint GetUInt32(int index) {
			return ToUInt32(GetFloat(index));
		}

		public int GetInt32(int index) {
			return ToInt32(GetObject(index));
		}

		public float GetFloat(int index) {
			return _list[index].GetNumber();
		}

		public byte GetByte(int index) {
			return ToByte(GetObject(index));
		}

		public string GetString(int index) {
			return _list[index].GetString();
		}

		public bool GetBoolean(int index) {
			return _list[index].ToBoolean();
		}

		public ESTable GetTable(int index) {
			return _list[index].Cast<ESTable>();
		}

		public ESArray GetArray(int index) {
			return _list[index].Cast<ESArray>();
		}

		public override bool ToBoolean() {
			return Count != 0;
		}

		IEnumerator IESEnumerable.GetEnumerator() {
			return GetEnumerator();
		}

		public override IESObject GetProperty(string name) {
			return ESUtility.GetProperty(this, name);
		}

		public void Add(IESObject item) {
			_list.Add(item);
		}

		public void Clear() {
			_list.Clear();
		}

		public bool Contains(IESObject item) {
			return _list.Contains(item);
		}

		public int IndexOf(IESObject item) {
			return _list.IndexOf(item);
		}

		public void Insert(int index, IESObject item) {
			_list.Insert(index, item);
		}

		public bool Remove(IESObject item) {
			return _list.Remove(item);
		}

		public void RemoveAt(int index) {
			_list.RemoveAt(index);
		}

		public IEnumerator<IESObject> GetEnumerator() {
			return _list.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator() {
			return GetEnumerator();
		}

		public void CopyTo(IESObject[] array, int arrayIndex) {
			throw new InvalidOperationException(ToString());
		}

		protected override void OnDispose() {
			base.OnDispose();
			_list.Clear();
		}

		public override string ToString() {
			return string.Format("ESArray Count: {0}", Count);
		}

	}

}