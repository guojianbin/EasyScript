using System.Collections.Generic;
using System.Linq;
using Engine.Bases;

namespace Easily.ES {

/// <summary>
/// @author Easily
/// </summary>
public class TableExpression : Expression, IRightExpression {

	private readonly Dictionary<string, IRightExpression> _dict;

	public TableExpression(Dictionary<string, IRightExpression> dict) {
		_dict = dict;
	}

	public override IESObject Execute(ESDomain domain) {
		return GetValue(domain);
	}

	public IESObject GetValue(ESDomain domain) {
		if (_dict.Count == 0) {
			return new ESTable();
		} else {
			return new ESTable(_dict.ToDictionary(t => t.Key, t => t.Value.GetValue(domain)));
		}
	}

	protected override void OnDispose() {
		base.OnDispose();
		_dict.Clear();
	}

	public override string ToString() {
		return string.Format("TableExpression Map: {0}", _dict.Count);
	}

}

}