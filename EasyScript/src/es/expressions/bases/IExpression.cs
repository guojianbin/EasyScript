using System;
using System.Collections.Generic;
using Engine.Bases;

namespace Engine.ES {

/// <summary>
/// @author Easily
/// </summary>]
public interface IExpression : IDisposable {

	void Checking();
	IESObject Execute(ESDomain domain);
	string GetCode();

}

/// <summary>
/// @author Easily
/// </summary>
public interface ILeftExpression : IExpression {

	void SetValue(ESDomain domain, IESObject value);

}

/// <summary>
/// @author Easily
/// </summary>
public interface IRightExpression : IExpression {

	IESObject GetValue(ESDomain domain);

}

/// <summary>
/// @author Easily
/// </summary>
public interface INameExpression : IExpression {

	string Name { get; }

}

/// <summary>
/// @author Easily
/// </summary>
public interface ILogicExpression : IExpression {

	bool IsTrue(ESDomain domain);

}

/// <summary>
/// @author Easily
/// </summary>
public interface IFuncExpression : IExpression {

	int Count { get; }
	IESObject Invoke(ESDomain domain, IESObject[] args);

}

/// <summary>
/// @author Easily
/// </summary>
public interface IListExpression : IExpression {

	int Count { get; }
	List<IExpression> Values { get; }

}

}