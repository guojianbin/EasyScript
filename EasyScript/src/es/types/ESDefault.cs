namespace Easily.ES {

/// <summary>
/// @author Easily
/// </summary>
public class ESDefault : ESObject {

	public static readonly IESObject Value = new ESDefault();

	public override bool IsTrue() {
		return false;
	}

	public override string ToString() {
		return "DEFAULT";
	}

}

}