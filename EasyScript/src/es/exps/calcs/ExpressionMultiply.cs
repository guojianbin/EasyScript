using Easily.Bases;

namespace Easily.ES {

	/// <summary>
	/// @author Easily
	/// </summary>
	public class ExpressionMultiply : ExpressionMath {

		public ExpressionMultiply(IExpressionRight value1, IExpressionRight value2) : base(value1, value2) {
			// ignored
		}

		public override IESObject GetValue(ESContext context) {
			var v1 = _value1.GetValue<IESNumber>(context);
			var v2 = _value2.GetValue<IESNumber>(context);
			return new ESNumber(v1.Value * v2.Value);
		}

	}

}