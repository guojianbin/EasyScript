using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Easily.Bases;

namespace Easily.ES {

	/// <summary>
	/// @author Easily
	/// </summary>
	public class ExpressionList : Expression, IExpressionList {

		private readonly List<IExpression> _list;

		public int Count {
			get { return _list.Count; }
		}

		public IExpression this[int index] {
			get { return _list[index]; }
		}

		public ExpressionList(List<IExpression> list) {
			_list = list;
		}

		public static implicit operator List<IExpression>(ExpressionList obj) {
			return obj._list;
		}

		public override IESObject Execute(ESContext context) {
			throw new InvalidOperationException();
		}

		public override void Checking() {
			_list.ForEach(Checking);
		}

		public IExpression Unbound() {
			var value = (IExpression)this;
			var list = (IExpressionList)this;
			while (true) {
				if (list.Count != 1) {
					return value;
				}
				value = list.Single();
				var temp = value as IExpressionList;
				if (temp != null) {
					list = temp;
				} else {
					return value;
				}
			}
		}

		protected override void OnDispose() {
			base.OnDispose();
			_list.Dispose();
		}

		public override string ToString() {
			return string.Format("ExpressionList List: {0}", Count);
		}

		public IEnumerator<IExpression> GetEnumerator() {
			return _list.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator() {
			return GetEnumerator();
		}

	}

}