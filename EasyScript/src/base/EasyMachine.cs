using System;

namespace Engine.Bases {

/// <summary>
/// @author Easily
/// </summary>
public interface IEasyState : IDisposable {

	void Start();

}

/// <summary>
/// @author Easily
/// </summary>
public class EasyState : Disposable, IEasyState {

	virtual public void Start() {
		// ignored
	}

}

/// <summary>
/// @author Easily
/// </summary>
public class EasyMachine : Disposable {

	private IEasyState _current = NIO<EasyState>();

	public void Change(IEasyState state) {
		_current.Dispose();
		_current = state;
		_current.Start();
	}

	protected override void OnDispose() {
		base.OnDispose();
		_current.Dispose();
	}

}

}