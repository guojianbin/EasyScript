using System.Collections.Generic;
using System.Linq;
using Engine.Bases;

namespace Easily.ES {

/// <summary>
/// @author Easily
/// </summary>
public class ArrayExpression : Expression, IRightExpression {

	private readonly List<IRightExpression> _list;

	public ArrayExpression(List<IRightExpression> list) {
		_list = list;
	}

	public override IESObject Execute(ESDomain domain) {
		return GetValue(domain);
	}

	public IESObject GetValue(ESDomain domain) {
		if (_list.Count == 0) {
			return new ESArray();
		} else {
			return new ESArray(_list.Select(t => t.Execute(domain)).ToList());
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
		return string.Format("ArrayExpression List: {0}", _list.Format());
	}

}

}