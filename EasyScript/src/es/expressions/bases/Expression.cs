using System;
using System.Collections.Generic;
using Engine.Bases;

namespace Engine.ES {

/// <summary>
/// @author Easily
/// </summary>
public class Expression : Disposable, IExpression {

	public virtual IESObject Execute(ESDomain domain) {
		throw new InvalidOperationException(GetType().Name);
	}

	public virtual void Checking() {
		// ignored
	}

	public virtual string GetCode() {
		throw new InvalidOperationException(GetType().Name);
	}

	public static IESObject ToVirtual(object obj) {
		return ESUtility.ToVirtual(obj);
	}

	public IEnumerable<EIL> ToILs() {
		throw new InvalidOperationException(GetType().Name);
	}

}

}