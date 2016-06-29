using Engine.Bases;

namespace Engine.ES {

/// <summary>
/// @author Easily
/// </summary>
public class ForExpression : Expression {

	private readonly INameExpression _index;
	private readonly IRightExpression _begin;
	private readonly IRightExpression _end;
	private readonly IRightExpression _step;
	private readonly IExpression _entry;

	public ForExpression(INameExpression index, IRightExpression begin, IRightExpression end, IExpression entry) {
		_index = index;
		_begin = begin;
		_end = end;
		_step = new NumberExpression(1);
		_entry = entry;
	}

	public ForExpression(INameExpression index, IRightExpression begin, IRightExpression end, IRightExpression step, IExpression entry) {
		_index = index;
		_begin = begin;
		_end = end;
		_step = step;
		_entry = entry;
	}

	public override IESObject Execute(ESDomain domain) {
		var ret = ESDefault.Value;
		var index = _begin.GetValue(domain).Clone().Cast<IESNumber>();
		var end = _end.GetValue<IESNumber>(domain).Value;
		var step = _step.GetValue<IESNumber>(domain).Value;
		while (true) {
			if (index.Value < end) {
				domain.AddValue(_index.Name, index);
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
			} else {
				index.Value += step;
			}
		}
	}

	public override void Checking() {
		_index.Checking();
		_begin.Checking();
		_end.Checking();
		_entry.Checking();
	}

	protected override void OnDispose() {
		base.OnDispose();
		_index.Dispose();
		_begin.Dispose();
		_end.Dispose();
		_entry.Dispose();
	}

	public override string ToString() {
		return string.Format("ForExpression Index: {0}, Begin: {1}, End: {2}, Body: {3}", _index, _begin, _end, _entry);
	}

}

/// <summary>
/// @author Easily
/// </summary>
public class ForCatchExpression : Expression {

	private readonly INameExpression _index;
	private readonly IRightExpression _begin;
	private readonly IRightExpression _end;
	private readonly IRightExpression _step;
	private readonly IFuncExpression _entry;

	public ForCatchExpression(INameExpression index, IRightExpression begin, IRightExpression end, IExpression entry) {
		_index = index;
		_begin = begin;
		_end = end;
		_step = new NumberExpression(1);
		_entry = new FuncExpression(new[] { _index.Name }, entry);
	}

	public ForCatchExpression(INameExpression index, IRightExpression begin, IRightExpression end, IRightExpression step, IExpression entry) {
		_index = index;
		_begin = begin;
		_end = end;
		_step = step;
		_entry = new FuncExpression(new[] { _index.Name }, entry);
	}

	public override IESObject Execute(ESDomain domain) {
		var ret = ESDefault.Value;
		var index = _begin.GetValue(domain).Clone().Cast<IESNumber>();
		var end = _end.GetValue<IESNumber>(domain).Value;
		var step = _step.GetValue<IESNumber>(domain).Value;
		while (true) {
			if (index.Value < end) {
				domain.AddValue(_index.Name, index);
				ret = _entry.Invoke(domain, new IESObject[] { index });
				index.Value += step;
			} else {
				return ret;
			}
		}
	}

	public override void Checking() {
		_index.Checking();
		_begin.Checking();
		_end.Checking();
		_entry.Checking();
	}

	protected override void OnDispose() {
		base.OnDispose();
		_index.Dispose();
		_begin.Dispose();
		_end.Dispose();
		_entry.Dispose();
	}

	public override string ToString() {
		return string.Format("ForCatchExpression Index: {0}, Begin: {1}, End: {2}, Body: {3}", _index, _begin, _end, _entry);
	}

}

}