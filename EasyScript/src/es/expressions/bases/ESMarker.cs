using System;

namespace Engine.ES {

/// <summary>
/// @author Easily
/// </summary>
public class CmaMarker : ESMarker { }
public class DotMarker : ESMarker { }
public class ColMarker : ESMarker { }
public class LtMarker : ESMarker { }
public class GtMarker : ESMarker { }
public class PlusMarker : ESMarker { }
public class SubMarker : ESMarker { }
public class DivMarker : ESMarker { }
public class MulMarker : ESMarker { }
public class EqMarker : ESMarker { }
public class ExMarker : ESMarker { }
public class RetMarker : ESMarker { }
public class CatchMarker : ESMarker { }
public class BreakMarker : ESMarker { }
public class WhileMarker : ESMarker { }
public class ForMarker : ESMarker { }
public class ForEachMarker : ESMarker { }
public class InMarker : ESMarker { }
public class FuncMarker : ESMarker { }
public class IfMarker : ESMarker { }
public class ElseMarker : ESMarker { }

/// <summary>
/// @author Easily
/// </summary>
public class ESMarker : Expression {

	public override void Checking() {
		throw new ArgumentException(GetType().Name);
	}

	public override string GetCode() {
		var name = GetType().Name;
		return string.Format("[{0}_MK]", name.Substring(0, name.IndexOf("Marker")));
	}

}

}