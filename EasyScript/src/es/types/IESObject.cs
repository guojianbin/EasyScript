using System;
using System.Collections;

namespace Easily.ES {

/// <summary>
/// @author Easily
/// </summary>
public interface IESObject : IDisposable {

	bool IsTrue();
	object ToObject();
	IESObject Clone();
	IESObject GetProperty(string name);

}

/// <summary>
/// @author Easily
/// </summary>
public interface IESInteger : IESObject {

	int Value { get; set; }

}

/// <summary>
/// @author Easily
/// </summary>
public interface IESNumber : IESObject {

	float Value { get; set; }

}

/// <summary>
/// @author Easily
/// </summary>
public interface IESString : IESObject {

	string Value { get; set; }

}

/// <summary>
/// @author Easily
/// </summary>
public interface IESEnumerable : IESObject {

	IEnumerator GetEnumerator();

}

/// <summary>
/// @author Easily
/// </summary>
public interface IESCollection : IESObject {

	int Count { get; }

}

/// <summary>
/// @author Easily
/// </summary>
public interface IESMember : IESObject {

	object Target { get; }

}

/// <summary>
/// @author Easily
/// </summary>
public interface IESFunction : IESMember {

	IESObject Invoke(IESObject[] args);

}

/// <summary>
/// @author Easily
/// </summary>
public interface IESIndex : IESMember {

	IESObject this[IESObject obj] { get; set; }

}

/// <summary>
/// @author Easily
/// </summary>
public interface IESProperty : IESMember {

	IESObject GetValue();
	void SetValue(IESObject value);
	void SetValue(object value);

}

}