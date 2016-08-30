using System;
using System.Collections;
using System.Collections.Generic;
using Easily.ES;

namespace Easily.Bases {

	/// <summary>
	/// @author Easily
	/// </summary>
	public class ESContext : ESObject, IEnumerable<KeyValuePair<string, IESObject>> {

		private readonly Dictionary<string, IESObject> _objects = new Dictionary<string, IESObject>();
		private readonly ESContext _parent;

		public bool IsBreak { set; get; }
		public bool IsReturn { set; get; }

		public ESContext() {
			_parent = NIO<ESContext>();
		}

		public ESContext(ESContext parent) {
			_parent = parent;
		}

		public bool Contains(string key) {
			return _objects.ContainsKey(key);
		}

		public void UpdateValue(string key, IESObject value) {
			_objects[key] = value;
		}

		public void SetValue(string key, IESObject value) {
			var target = GetTarget(key);
			if (target.IsEnabled) {
				target.UpdateValue(key, value);
			} else {
				UpdateValue(key, value);
			}
		}

		private ESContext GetTarget(string key) {
			if (_objects.ContainsKey(key)) {
				return this;
			} else if (_parent.IsEnabled) {
				return _parent.GetTarget(key);
			} else {
				return _parent;
			}
		}

		public IESObject GetValue(string key) {
			IESObject value;
			if (_objects.TryGetValue(key, out value)) {
				return value;
			} else if (_parent.IsEnabled) {
				return _parent.GetValue(key);
			} else {
				return ESDefault.Value;
			}
		}

		public T GetValue<T>(string key) {
			try {
				return (T)GetValue(key);
			} catch (Exception e) {
				throw new InvalidOperationException(key, e);
			}
		}

		public void Remove(string key) {
			_objects.Remove(key);
		}

		public IEnumerator<KeyValuePair<string, IESObject>> GetEnumerator() {
			return _objects.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator() {
			return GetEnumerator();
		}

		protected override void OnDispose() {
			base.OnDispose();
			_objects.Clear();
		}

		public override string ToString() {
			return string.Format("ESContext Count: {0}", _objects.Count);
		}

	}

}