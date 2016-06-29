using Engine.Bases;

namespace Easily.ES {

/// <summary>
/// @author Easily
/// </summary>
public class WhileExpression : Expression {

	private readonly ILogicExpression _condition;
	private readonly IExpression _entry;

	public WhileExpression(ILogicExpression condition, IExpression entry) {
		_condition = condition;
		_entry = entry;
	}

	public override IESObject Execute(ESDomain domain) {
		var ret = ESDefault.Value;
		while (true) {
			if (_condition.IsTrue(domain)) {
				ret = _entry.Execute(domain);	
			} else {
				return ret;
			}
			if (domain.IsBreak) {
				domain.IsBreak = false;
				return ret;
			}
			if (domain.IsReturn) {
				return ret;
			}
		}
	}

	public override void Checking() {
		_condition.Checking();
		_entry.Checking();
	}

	protected override void OnDispose() {
		base.OnDispose();
		_condition.Dispose();
		_entry.Dispose();
	}

	public override string ToString() {
		return string.Format("WhileExpression Condition: {0}, Body: {1}", _condition, _entry);
	}

}

}