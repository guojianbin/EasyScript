using System.Collections.Generic;
using System.Linq;

/// <summary>
/// @author Easily
/// </summary>
public class XNumber {

	private static readonly List<char> _n2c = new List<char>();
	private readonly string _str;

	public static int MaxRadix {
		get { return _n2c.Count; }
	}

	private XNumber(ulong value) {
		_str = Convert(value, (byte)_n2c.Count);
	}

	static XNumber() {
		_n2c.AddRange(Enumerable.Range('0', '9' - '0' + 1).Select(c => (char)c));
		_n2c.AddRange(Enumerable.Range('a', 'z' - 'a' + 1).Select(c => (char)c));
		_n2c.AddRange(Enumerable.Range('A', 'Z' - 'A' + 1).Select(c => (char)c));
	}

	public static string ToString(ulong value) {
		return new XNumber(value)._str;
	}

	private static string Convert(ulong value, byte radix) {
		var list = new LinkedList<char>();
		while (true) {
			var d = value / radix;
			var m = (int)(value % radix);
			if (d == 0) {
				list.AddFirst(_n2c[m]);
				return new string(list.ToArray());
			} else {
				list.AddFirst(_n2c[m]);
				value = d;
			}
		}
	}

}