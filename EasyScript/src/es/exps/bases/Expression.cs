using System;
using Easily.Bases;

namespace Easily.ES {

	/// <summary>
	/// @author Easily
	/// </summary>
	public abstract class Expression : Disposable, IExpression {

		public static void Checking(IExpression expression) {
			expression.Checking();
		}

		public static IESObject ToVirtual(object obj) {
			return ESUtility.ToVirtual(obj);
		}

		public virtual IESObject Execute(ESContext context) {
			throw new InvalidOperationException(GetType().Name);
		}

		public virtual void Checking() {
			// ignored
		}

	}

}