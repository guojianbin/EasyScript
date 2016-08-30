using System;
using System.Collections.Generic;
using Easily.Bases;

namespace Easily.ES {

	/// <summary>
	/// @author Easily
	/// </summary>]
	public interface IExpression : IDisposable {

		IESObject Execute(ESContext context);
		void Checking();

	}

	/// <summary>
	/// @author Easily
	/// </summary>
	public interface IExpressionLeft : IExpression {

		void SetValue(ESContext context, IESObject value);

	}

	/// <summary>
	/// @author Easily
	/// </summary>
	public interface IExpressionRight : IExpression {

		IESObject GetValue(ESContext context);

	}

	/// <summary>
	/// @author Easily
	/// </summary>
	public interface IExpressionName : IExpression {

		string Name { get; }

	}

	/// <summary>
	/// @author Easily
	/// </summary>
	public interface IExpressionLogic : IExpression {

		bool ToBoolean(ESContext context);

	}

	/// <summary>
	/// @author Easily
	/// </summary>
	public interface IExpressionFunc : IExpression {

		int Count { get; }
		IESObject Invoke(ESContext context, IESObject[] args);

	}

	/// <summary>
	/// @author Easily
	/// </summary>
	public interface IExpressionClass : IExpression {

		IESObject New(ESContext context, IESObject[] args);

	}

	/// <summary>
	/// @author Easily
	/// </summary>
	public interface IExpressionList : IExpression, IEnumerable<IExpression> {

		int Count { get; }

		IExpression this[int index] { get; }

	}

}