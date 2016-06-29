using System;
using Engine.DS;

namespace Engine.Bases {

public class EasyObject : Disposable, IEasyObject {

	private readonly EasyMap<String, IEasyElement> _elements = new EasyMap<String, IEasyElement>();

	public T AddElement<T>() where T : IEasyElement, new() {
		var element = new T();
		_elements.Add(typeof(T).Name, element);
		return element;
	}

	public T GetElement<T>() where T : class, IEasyElement {
		return (T)_elements[typeof(T).Name];
	}

	public T TryGetElement<T>() where T : IEasyElement, new() {
		IEasyElement element;
		if (_elements.TryGetValue(typeof(T).Name, out element)) {
			return (T)element;
		} else {
			return AddElement<T>();
		}
	}

	protected override void OnDispose() {
		base.OnDispose();
		_elements.Dispose();
	}

}

}