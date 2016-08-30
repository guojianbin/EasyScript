namespace Easily.Bases {

/// <summary>
/// @author Easily
/// </summary>
public class EasyRound {

	private readonly int _len;
	private int _value;

	public int Value {
		get { return _value; }
	}

	public EasyRound(int len) {
		_len = len;
	}

	public int Increase() {
		_value = (_value + 1) % _len;
		return _value;
	}

}

}