using System.Collections.Generic;

namespace Easily.ES {

	/// <summary>
	/// @author Easily
	/// </summary>
	internal class SyntaxNotifier : Syntax {

		public delegate void OnParse(Parser parser, List<IExpression> list, ref int pos);
		private readonly OnParse _onParse;

		public SyntaxNotifier(OnParse onParse) {
			_onParse = onParse;
		}

		public override void Parse(Parser parser, List<IExpression> list, ref int pos) {
			_onParse(parser, list, ref pos);
		}

	}

}