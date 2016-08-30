namespace Easily.Bases {

/// <summary>
/// @author Easily
/// </summary>
public static class Struct {

	public static Struct<T1, T2> Create<T1, T2>(T1 item1, T2 item2) {
		return new Struct<T1, T2>(item1, item2);
	}

	public static Struct<T1, T2, T3> Create<T1, T2, T3>(T1 item1, T2 item2, T3 item3) {
		return new Struct<T1, T2, T3>(item1, item2, item3);
	}

	public static Struct<T1, T2, T3> Create<T1, T2, T3, T4>(T1 item1, T2 item2, T3 item3, T4 item4) {
		return new Struct<T1, T2, T3, T4>(item1, item2, item3, item4);
	}

}

/// <summary>
/// @author Easily
/// </summary>
public class Struct<T1, T2> {

	public T1 Item1 { get; set; }
	public T2 Item2 { get; set; }

	public Struct(T1 item1, T2 item2) {
		Item1 = item1;
		Item2 = item2;
	}

}

/// <summary>
/// @author Easily
/// </summary>
public class Struct<T1, T2, T3> : Struct<T1, T2> {

	public T3 Item3 { get; set; }

	public Struct(T1 item1, T2 item2, T3 item3) : base(item1, item2) {
		Item3 = item3;
	}

}

/// <summary>
/// @author Easily
/// </summary>
public class Struct<T1, T2, T3, T4> : Struct<T1, T2, T3> {

	public T4 Item4 { get; set; }

	public Struct(T1 item1, T2 item2, T3 item3, T4 item4) : base(item1, item2, item3) {
		Item4 = item4;
	}

}

}