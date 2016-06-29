using Engine.Bases;

namespace Easily.ES {

/// <summary>
/// @author Easily
/// </summary>
public class ForEachExpression : Expression {

	private readonly INameExpression _index;
	private readonly IRightExpression _list;
	private readonly IExpression _entry;

	public ForEachExpression(INameExpression index, IRightExpression list, IExpression entry) {
		_index = index;
		_list = list;
		_entry = entry;
	}

	public override IESObject Execute(ESDomain domain) {
		var ret = ESDefault.Value;
		var items = _list.GetValue<IESEnumerable>(domain);
		var iter = items.GetEnumerator();
		while (true) {
			if (iter.MoveNext()) {
				domain.AddValue(_index.Name, ToVirtual(iter.Current));
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
		_index.Checking();
		_list.Checking();
		_entry.Checking();
	}

	protected override void OnDispose() {
		base.OnDispose();
		_index.Dispose();
		_list.Dispose();
		_entry.Dispose();
	}

	public override string ToString() {
		return string.Format("ForEachExpression Index: {0}, List: {1}, Body: {2}", _index, _list, _entry);
	}

}

/// <summary>
/// @author Easily
/// </summary>
public class ForEachCatchExpression : Expression {

	private readonly INameExpression _index;
	private readonly IRightExpression _list;
	private readonly IFuncExpression _entry;

	public ForEachCatchExpression(INameExpression index, IRightExpression list, IExpression entry) {
		_index = index;
		_list = list;
		_entry = new FuncExpression(new[] { _index.Name }, entry);
	}

	public override IESObject Execute(ESDomain domain) {
		var ret = ESDefault.Value;
		var items = _list.GetValue<IESEnumerable>(domain);
		var iter = items.GetEnumerator();
		while (true) {
			if (iter.MoveNext()) {
				var current = ToVirtual(iter.Current);
				domain.AddValue(_index.Name, current);
				ret = _entry.Invoke(domain, new[] { current });
			} else {
				return ret;
			}
		}
	}

	public override void Checking() {
		_index.Checking();
		_list.Checking();
		_entry.Checking();
	}

	protected override void OnDispose() {
		base.OnDispose();
		_index.Dispose();
		_list.Dispose();
		_entry.Dispose();
	}

	public override string ToString() {
		return string.Format("ForEachCatchExpression Catch Index: {0}, List: {1}, Body: {2}", _index, _list, _entry);
	}

}

}