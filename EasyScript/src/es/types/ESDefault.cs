namespace Easily.ES {

	/// <summary>
	/// @author Easily
	/// </summary>
	public sealed class ESDefault : ESObject {

		public static readonly IESObject Value = NIO<ESDefault>();

		public override bool ToBoolean() {
			return false;
		}

		public override string ToString() {
			return "Default";
		}

	}

}