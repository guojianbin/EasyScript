using System.Collections.Generic;
using System.Linq;
using Engine.Bases;

namespace Engine.ES {

/// <summary>
/// @author Easily
/// </summary>
public class InvokeExpression : Expression, IRightExpression, ILogicExpression {

	private readonly IRightExpression _func;
	private readonly List<IRightExpression> _args;

	public InvokeExpression(IRightExpression func, List<IRightExpression> args) {
		_func = func;
		_args = args;
	}

	public override IESObject Execute(ESDomain domain) {
		return GetValue(domain);
	}

	public IESObject GetValue(ESDomain domain) {
		return _func.GetValue<IESFunction>(domain).Invoke(_args.Select(t => t.Execute(domain)).ToArray());
	}

	public bool IsTrue(ESDomain domain) {
		return Execute(domain).IsTrue();
	}

	public override void Checking() {
		_func.Checking();
		_args.ForEach(t => t.Checking());
	}

	protected override void OnDispose() {
		base.OnDispose();
		_func.Dispose();
	}

	public override string ToString() {
		return string.Format("InvokeExpression Func: {0}, Args: {1}", _func, _args.Format());
	}

}

}