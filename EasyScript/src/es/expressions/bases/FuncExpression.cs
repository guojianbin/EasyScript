using System.Collections.Generic;
using Engine.Bases;

namespace Engine.ES {

/// <summary>
/// @author Easily
/// </summary>
public class FuncExpression : Expression, IFuncExpression, IRightExpression {

	private readonly string _name;
	private readonly List<string> _args = new List<string>();
	private readonly IExpression _entry;

	public int Count {
		get { return _args.Count; }
	}

	public FuncExpression(IEnumerable<string> args, IExpression entry) : this("func_" + NewUID(), args, entry) {
		// ignored
	}

	public FuncExpression(string name, IEnumerable<string> args, IExpression entry) {
		_name = name;
		_args.AddRange(args);
		_entry = entry;
	}

	public override IESObject Execute(ESDomain domain) {
		return GetValue(domain);
	}

	public IESObject GetValue(ESDomain domain) {
		var func = new ESFunction(domain, this);
		domain.AddValue(_name, func);
		return func;
	}

	public IESObject Invoke(ESDomain domain, IESObject[] args) {
		domain = new ESDomain(domain);
		_args.ForEach((i, t) => domain.AddValue(t, args[i].Clone()));
		return _entry.Execute(domain);
	}

	public override void Checking() {
		_entry.Checking();
	}

	public override string GetCode() {
		return "[FUNC]";
	}

	protected override void OnDispose() {
		base.OnDispose();
		_entry.Dispose();
	}

	public override string ToString() {
		return string.Format("FuncExpression Name: {0}, Args: {1}, Body: {2}", _name, _args.Count, _entry);
	}

}

}