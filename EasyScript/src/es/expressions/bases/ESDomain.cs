using System.Collections;
using System.Collections.Generic;
using Engine.DS;
using Engine.ES;

namespace Engine.Bases {

/// <summary>
/// @author Easily
/// </summary>
public class ESDomain : Disposable, IEnumerable<KeyValuePair<string ,IESObject>> {

	private readonly EasyMap<string, IESObject> _objects = new EasyMap<string, IESObject>();
	private readonly ESDomain _parent;

	public bool IsBreak { set; get; }
	public bool IsReturn { set; get; }

	public ESDomain() {
		// ignored
	}

	public ESDomain(ESDomain parent) {
		_parent = parent;
	}

	public bool Contains(string key) {
		return _objects.ContainsKey(key);
	}

	public void AddValue(string key, IESObject value) {
		_objects[key] = value;
	}

	public void SetValue(string key, IESObject value) {
		var target = GetTarget(key);
		if (target != null) {
			target.AddValue(key, value);
		} else {
			AddValue(key, value);
		}
	}

	private ESDomain GetTarget(string key) {
		if (_objects.ContainsKey(key)) {
			return this;
		} else if (_parent != default(ESDomain)) {
			return _parent.GetTarget(key);
		} else {
			return default(ESDomain);
		}
	}

	public IESObject GetValue(string key) {
		IESObject value;
		if (_objects.TryGetValue(key, out value)) {
			return value;
		} else if (_parent != null) {
			return _parent.GetValue(key);
		} else {
			return ESDefault.Value;
		}
	}

	public T GetValue<T>(string key) {
		return (T)GetValue(key);
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
		_objects.Dispose();
	}

	public override string ToString() {
		return string.Format("ESDomain IsBreak: {0}, IsReturn: {1}, Parent: {2}, Objects: {3}", IsBreak, IsReturn, _parent, _objects.Format());
	}

}

}