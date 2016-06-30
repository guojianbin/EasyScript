namespace Easily.ES {

/// <summary>
/// @author Easily
/// </summary>
public class ESDefault : ESObject {

	public static readonly IESObject Value = NIO<ESDefault>();

	public override bool IsTrue() {
		return false;
	}

	public override string ToString() {
		return "Default";
	}

}

}
