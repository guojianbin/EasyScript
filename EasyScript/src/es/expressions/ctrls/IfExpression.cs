using Engine.Bases;

namespace Easily.ES {

/// <summary>
/// @author Easily
/// </summary>
public class IfExpression : Expression {

	private readonly ILogicExpression _condition;
	private readonly IExpression _branch;

	public IfExpression(ILogicExpression condition, IExpression branch) {
		_condition = condition;
		_branch = branch;
	}

	public override IESObject Execute(ESDomain domain) {
		if (_condition.IsTrue(domain)) {
			return _branch.Execute(domain);
		} else {
			return ESDefault.Value;
		}
	}

	public override void Checking() {
		_condition.Checking();
		_branch.Checking();
	}

	protected override void OnDispose() {
		base.OnDispose();
		_condition.Dispose();
		_branch.Dispose();
	}

	public override string ToString() {
		return string.Format("IfExpression Logic: {0}, Branch: {1}", _condition, _branch);
	}

}

}