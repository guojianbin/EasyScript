using Engine.Bases;

namespace Engine.ES {

/// <summary>
/// @author Easily
/// </summary>
public class IfElseExpression : Expression {

	private readonly ILogicExpression _condition;
	private readonly IExpression _branch1;
	private readonly IExpression _branch2;

	public IfElseExpression(ILogicExpression condition, IExpression branch1, IExpression branch2) {
		_condition = condition;
		_branch1 = branch1;
		_branch2 = branch2;
	}

	public override IESObject Execute(ESDomain domain) {
		if (_condition.IsTrue(domain)) {
			return _branch1.Execute(domain);
		} else {
			return _branch2.Execute(domain);
		}
	}

	public override void Checking() {
		_condition.Checking();
		_branch1.Checking();
		_branch2.Checking();
	}

	protected override void OnDispose() {
		base.OnDispose();
		_condition.Dispose();
		_branch1.Dispose();
		_branch2.Dispose();
	}

	public override string ToString() {
		return string.Format("IfElseExpression Logic: {0}, Branch1: {1}, Branch2: {2}", _condition, _branch1, _branch2);
	}

}

}