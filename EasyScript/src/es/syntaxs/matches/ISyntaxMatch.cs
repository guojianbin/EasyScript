using System.Collections.Generic;

namespace Easily.ES {

	/// <summary>
	/// @author Easily
	/// </summary>
	public interface ISyntaxMatch {

		bool IsMatch(List<IExpression> list, int pos);

	}

}