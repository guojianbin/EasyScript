using System;
using Engine.Bases;

namespace Engine.ES {

/// <summary>
/// @author Easily
/// </summary>
public class LogicExpression : Expression, ILogicExpression, IRightExpression {

	public override IESObject Execute(ESDomain domain) {
		return GetValue(domain);
	}

	public IESObject GetValue(ESDomain domain) {
		return new ESBoolean(IsTrue(domain));
	}

	public virtual bool IsTrue(ESDomain domain) {
		throw new InvalidOperationException(ToString());
	}

}

}