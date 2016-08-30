using System;
using Easily.Bases;

namespace Engine.Events {

/// <summary>
/// @author Easily
/// </summary>
public class EventProxy : Disposable {

	private readonly EventDispatcher _dispatcher;
	private readonly string _eventType;
	private readonly Action _callback;

	public EventProxy(EventDispatcher dispatcher, string eventType, Action callback) {
		_dispatcher = dispatcher;
		_eventType = eventType;
		_callback = callback;
		_dispatcher.AddEventListener(_eventType, _callback);
	}

	protected override void OnDispose() {
		base.OnDispose();
		_dispatcher.RemoveEventListener(_eventType, _callback);
	}

}

/// <summary>
/// @author Easily
/// </summary>
public class EventProxy<T> : Disposable {

	private readonly EventDispatcher _dispatcher;
	private readonly string _eventType;
	private readonly Action<T> _callback;

	public EventProxy(EventDispatcher dispatcher, string eventType, Action<T> callback) {
		_dispatcher = dispatcher;
		_eventType = eventType;
		_callback = callback;
		_dispatcher.AddEventListener(_eventType, _callback);
	}

	protected override void OnDispose() {
		base.OnDispose();
		_dispatcher.RemoveEventListener(_eventType, _callback);
	}

}

/// <summary>
/// @author Easily
/// </summary>
public class EventProxy<T1, T2> : Disposable {

	private readonly EventDispatcher _dispatcher;
	private readonly string _eventType;
	private readonly Action<T1, T2> _callback;

	public EventProxy(EventDispatcher dispatcher, string eventType, Action<T1, T2> callback) {
		_dispatcher = dispatcher;
		_eventType = eventType;
		_callback = callback;
		_dispatcher.AddEventListener(_eventType, _callback);
	}

	protected override void OnDispose() {
		base.OnDispose();
		_dispatcher.RemoveEventListener(_eventType, _callback);
	}

}

/// <summary>
/// @author Easily
/// </summary>
public class EventProxy<T1, T2, T3> : Disposable {

	private readonly EventDispatcher _dispatcher;
	private readonly string _eventType;
	private readonly Action<T1, T2, T3> _callback;

	public EventProxy(EventDispatcher dispatcher, string eventType, Action<T1, T2, T3> callback) {
		_dispatcher = dispatcher;
		_eventType = eventType;
		_callback = callback;
		_dispatcher.AddEventListener(_eventType, _callback);
	}

	protected override void OnDispose() {
		base.OnDispose();
		_dispatcher.RemoveEventListener(_eventType, _callback);
	}

}

}