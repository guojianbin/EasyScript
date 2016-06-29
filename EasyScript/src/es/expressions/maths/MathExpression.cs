using System;
using Engine.Bases;

namespace Easily.ES {

/// <summary>
/// @author Easily
/// </summary>
public class MathExpression : Expression, IRightExpression, ILogicExpression {

	protected readonly IRightExpression _value1;
	protected readonly IRightExpression _value2;

	public MathExpression(IRightExpression value1, IRightExpression value2) {
		_value1 = value1;
		_value2 = value2;
	}

	public override IESObject Execute(ESDomain domain) {
		return GetValue(domain);
	}

	public virtual IESObject GetValue(ESDomain domain) {
		throw new InvalidOperationException(ToString());
	}

	public bool IsTrue(ESDomain domain) {
		return Execute(domain).IsTrue();
	}

	public override void Checking() {
		_value1.Checking();
		_value2.Checking();
	}

	protected override void OnDispose() {
		base.OnDispose();
		_value1.Dispose();
		_value2.Dispose();
	}

	public override string ToString() {
		return string.Format("MathExpression Value1: {0}, Value2: {1}", _value1, _value2);
	}

}

}