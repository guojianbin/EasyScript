using System;
using Engine.Bases;

namespace Engine.Tasks {

/// <summary>
/// @author Easily
/// </summary>
public interface ITask : IDisposable {

	bool IsStart { get; }
	bool IsError { get; }
	bool IsComplete { get; }
	bool IsDisposed { get; }

	Observer onStart { get; set; }
	Observer onError { get; set; }
	Observer onComplete { get; set; }
	Observer onDispose { get; set; }

	void Start();
	void Error();
	void Complete();

}

}