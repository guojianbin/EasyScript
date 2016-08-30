using Easily.Bases;

namespace Easily.ES {

	/// <summary>
	/// @author Easily
	/// </summary>
	public class ExpressionBreak : Expression {

		public override IESObject Execute(ESContext context) {
			context.IsBreak = true;
			return ESDefault.Value;
		}

		public override string ToString() {
			return "BREAK";
		}

	}

}