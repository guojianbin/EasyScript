using System;

namespace Easily.ES {

	/// <summary>
	/// @author Easily
	/// </summary>
	public class ExpressionCharter : Expression {

		public override void Checking() {
			throw new ArgumentException(GetType().Name);
		}

	}

	/// <summary>
	/// @author Easily
	/// </summary>
	public class CharterCMA : ExpressionCharter {

	}

	/// <summary>
	/// @author Easily
	/// </summary>
	public class CharterDOT : ExpressionCharter {

	}

	/// <summary>
	/// @author Easily
	/// </summary>
	public class CharterRDO : ExpressionCharter {

	}

	/// <summary>
	/// @author Easily
	/// </summary>
	public class CharterCOL : ExpressionCharter {

	}

	/// <summary>
	/// @author Easily
	/// </summary>
	public class CharterLT : ExpressionCharter {

	}

	/// <summary>
	/// @author Easily
	/// </summary>
	public class CharterGT : ExpressionCharter {

	}

	/// <summary>
	/// @author Easily
	/// </summary>
	public class CharterAdd : ExpressionCharter {

	}

	/// <summary>
	/// @author Easily
	/// </summary>
	public class CharterSub : ExpressionCharter {

	}

	/// <summary>
	/// @author Easily
	/// </summary>
	public class CharterDiv : ExpressionCharter {

	}

	/// <summary>
	/// @author Easily
	/// </summary>
	public class CharterMul : ExpressionCharter {

	}

	/// <summary>
	/// @author Easily
	/// </summary>
	public class CharterEQ : ExpressionCharter {

	}

	/// <summary>
	/// @author Easily
	/// </summary>
	public class CharterEX : ExpressionCharter {

	}

	/// <summary>
	/// @author Easily
	/// </summary>
	public class CharterRet : ExpressionCharter {

	}

	/// <summary>
	/// @author Easily
	/// </summary>
	public class CharterWhile : ExpressionCharter {

	}

	/// <summary>
	/// @author Easily
	/// </summary>
	public class CharterFor : ExpressionCharter {

	}

	/// <summary>
	/// @author Easily
	/// </summary>
	public class CharterForEach : ExpressionCharter {

	}

	/// <summary>
	/// @author Easily
	/// </summary>
	public class CharterIn : ExpressionCharter {

	}

	/// <summary>
	/// @author Easily
	/// </summary>
	public class CharterFunc : ExpressionCharter {

	}

	/// <summary>
	/// @author Easily
	/// </summary>
	public class CharterClass : ExpressionCharter {

	}

	/// <summary>
	/// @author Easily
	/// </summary>
	public class CharterNew : ExpressionCharter {

	}

	/// <summary>
	/// @author Easily
	/// </summary>
	public class CharterIf : ExpressionCharter {

	}

	/// <summary>
	/// @author Easily
	/// </summary>
	public class CharterElse : ExpressionCharter {

	}

}