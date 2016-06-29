using Engine.Bases;

namespace Easily.ES {

/// <summary>
/// @author Easily
/// </summary>
public class NotExpression : LogicExpression {

	private readonly ILogicExpression _target;

	public NotExpression(ILogicExpression target) {
		_target = target;
	}

	public override bool IsTrue(ESDomain domain) {
		return !_target.IsTrue(domain);
	}

	public override void Checking() {
		_target.Checking();
	}

	protected override void OnDispose() {
		base.OnDispose();
		_target.Dispose();
	}

	public override string ToString() {
		return string.Format("NotExpression Target: {0}", _target);
	}

}

}