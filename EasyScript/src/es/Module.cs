using System.Collections.Generic;
using Engine.Bases;

namespace Engine.ES {

/// <summary>
/// @author Easily
/// </summary>
public class Module : Disposable, IExpression {

	private readonly IListExpression _list;

	public Module(List<IExpression> expressions) : this(new LLExpression(expressions)) {
		// ignored
	}

	public Module(IListExpression list) {
		_list = list;
		Checking();
	}

	public IESObject Execute(ESDomain domain) {
		return _list.Execute(domain);
	}

	public ESDomain Evaluate(ESDomain domain) {
		domain = new ESDomain(domain);
		Execute(domain);
		return domain;
	}

	public void Checking() {
		_list.Checking();
	}

	public string GetCode() {
		return "[M]";
	}

	protected override void OnDispose() {
		base.OnDispose();
		_list.Dispose();
	}

	public override string ToString() {
		return string.Format("Module List: {0}", _list.Count);
	}

}

}