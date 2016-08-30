using System;

namespace Easily.Bases {

/// <summary>
/// @author Easily
/// </summary>
public interface IEasyElement : IDisposable {

	string ToString();

}

/// <summary>
/// @author Easily
/// </summary>
public interface IEasyObject : IDisposable {

	T AddElement<T>() where T : IEasyElement, new();
	T GetElement<T>() where T : class, IEasyElement;
	T TryGetElement<T>() where T : IEasyElement, new();

}

}