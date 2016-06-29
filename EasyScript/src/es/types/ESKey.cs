using System.Collections;

namespace Easily.ES {

/// <summary>
/// @author Easily
/// </summary>
public class ESKey : ESObject, IESIndex, IESProperty, IESString, IESNumber, IESInteger, IESFunction, IESEnumerable {

	private readonly ESTable _table;
	private readonly string _key;

	public object Target {
		get { return _table; }
	}

	float IESNumber.Value {
		set { SetValue(new ESNumber(value)); }
		get { return ToFloat(ToObject()); }
	}

	int IESInteger.Value {
		set { SetValue(new ESNumber(value)); }
		get { return ToInt32(ToObject()); }
	}

	string IESString.Value {
		set { SetValue(new ESString(value)); }
		get { return ToString(ToObject()); }
	}

	IESObject IESIndex.this[IESObject index] {
		get { return GetValue<IESIndex>()[index]; }
		set { GetValue<IESIndex>()[index] = value; }
	}

	public ESKey(ESTable table, string key) {
		_table = table;
		_key = key;
	}

	public void SetValue(IESObject value) {
		_table[_key] = value;
	}

	public void SetValue(object value) {
		SetValue(ToVirtual(value));
	}

	public IESObject GetValue() {
		return _table[_key];
	}

	public T GetValue<T>() {
		return (T)GetValue();
	}

	public override object ToObject() {
		return GetValue().ToObject();
	}

	public override IESObject GetProperty(string name) {
		return GetValue().GetProperty(name);
	}

	public IESObject Invoke(IESObject[] args) {
		return GetValue<IESFunction>().Invoke(args);
	}

	public IEnumerator GetEnumerator() {
		return ToObject<IEnumerable>().GetEnumerator();
	}

	public override bool IsTrue() {
		return GetValue().IsTrue();
	}

	public override string ToString() {
		return string.Format("ESKey Table: {0}, Key: {1}", _table, _key);
	}

}

}