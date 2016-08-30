using System;

namespace Easily.ES {

	/// <summary>
	/// @author Easily
	/// </summary>
	public class ExpressionMarker : Expression {

		public override void Checking() {
			throw new ArgumentException(GetType().Name);
		}

	}

	public class MarkerCma : ExpressionMarker {

		
	}

	public class MarkerDot : ExpressionMarker {

		
	}

	public class MarkerCol : ExpressionMarker {

		
	}

	public class MarkerLt : ExpressionMarker {

		
	}

	public class MarkerGt : ExpressionMarker {

		
	}

	public class MarkerAdd : ExpressionMarker {

		
	}

	public class MarkerSub : ExpressionMarker {

		
	}

	public class MarkerDiv : ExpressionMarker {

		
	}

	public class MarkerMul : ExpressionMarker {

		
	}

	public class MarkerEq : ExpressionMarker {

		
	}

	public class MarkerEx : ExpressionMarker {

		
	}

	public class MarkerRet : ExpressionMarker {

		
	}

	public class MarkerWhile : ExpressionMarker {

		
	}

	public class MarkerFor : ExpressionMarker {

		
	}

	public class MarkerForEach : ExpressionMarker {

		
	}

	public class MarkerIn : ExpressionMarker {

		
	}

	public class MarkerFunc : ExpressionMarker {

		
	}

	public class MarkerClass : ExpressionMarker {

		
	}

	public class MarkerNew : ExpressionMarker {

		
	}

	public class MarkerIf : ExpressionMarker {

		
	}

	public class MarkerElse : ExpressionMarker {

		
	}

}