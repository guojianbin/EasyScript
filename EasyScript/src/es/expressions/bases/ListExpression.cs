using System;
using System.Collections.Generic;
using System.Linq;
using Engine.Bases;

namespace Easily.ES {

/// <summary>
/// @author Easily
/// </summary>
public class ListExpression : Expression, IListExpression {

	private readonly List<IExpression> _list;

	public List<IExpression> Values {
		get { return _list; }
	}

	public int Count {
		get { return _list.Count; }
	}

	public ListExpression(List<IExpression> list) {
		_list = list;
	}

	public override IESObject Execute(ESDomain domain) {
		throw new InvalidOperationException();
	}

	public IExpression Unbound() {
		var value = (IExpression)this;
		var list = (IListExpression)this;
		while (true) {
			if (list.Count != 1) {
				return value;
			}
			value = list.Values.Single();
			var temp = value as IListExpression;
			if (temp != null) {
				list = temp;
			} else {
				return value;
			}
		}
	}

	public override void Checking() {
		_list.ForEach(t => t.Checking());
	}

	protected override void OnDispose() {
		base.OnDispose();
		_list.ForEach(t => t.Dispose());
		_list.Clear();
	}

	public override string ToString() {
		return string.Format("ListExpression List: {0}", _list.Format());
	}

}

/// <summary>
/// @author Easily
/// </summary>
public class SSExpression : ListExpression, IRightExpression {

	public SSExpression(List<IExpression> list) : base(list) {
		// ignored
	}

	public IESObject GetValue(ESDomain domain) {
		if (Count == 1) {
			return Values[0].Execute(domain);
		} else {
			throw new InvalidOperationException("Count not eq 1");
		}
	}

	public override string ToString() {
		return string.Format("SSExpression List: {0}", Values.Format());
	}

}

/// <summary>
/// @author Easily
/// </summary>
public class MMExpression : ListExpression {

	public MMExpression(List<IExpression> list) : base(list) {
		// ignored
	}

	public override IESObject Execute(ESDomain domain) {
		throw new InvalidOperationException("execute MMExpression");
	}

	public override string ToString() {
		return string.Format("MMExpression List: {0}", Values.Format());
	}

}

/// <summary>
/// @author Easily
/// </summary>
public class BBExpression : ListExpression {

	public BBExpression(List<IExpression> list) : base(list) {
		// ignored
	}

	public override IESObject Execute(ESDomain domain) {
		var ret = ESDefault.Value;
		var index = 0;
		while (true) {
			if (index < Count) {
				ret = Values[index].Execute(domain);
			} else {
				return ret;
			}
			if (domain.IsReturn) {
				return ret;
			} else {
				index += 1;
			}
		}
	}

	public override string ToString() {
		return string.Format("BBExpression List: {0}", Values.Format());
	}

}

/// <summary>
/// @author Easily
/// </summary>
public class LLExpression : ListExpression {

	public LLExpression(List<IExpression> list) : base(list) {
		// ignored
	}

	public override IESObject Execute(ESDomain domain) {
		var ret = ESDefault.Value;
		var index = 0;
		while (true) {
			if (index < Count) {
				ret = Values[index].Execute(domain);
				index += 1;
			} else {
				return ret;
			}
		}
	}

	public override string ToString() {
		return string.Format("LLExpression List: {0}", Values.Format());
	}

}

}