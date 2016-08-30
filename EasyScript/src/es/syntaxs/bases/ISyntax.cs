using System.Collections.Generic;

namespace Easily.ES {

	/// <summary>
	/// @author Easily
	/// </summary>
	internal interface ISyntax {

		bool IsMatch(List<IExpression> list, int pos);
		void Parse(Parser parser, List<IExpression> list, ref int pos);

	}

}