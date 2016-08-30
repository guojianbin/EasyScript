﻿using System;
using Easily.Utility;

namespace Easily.ES {

	/// <summary>
	/// @author Easily
	/// </summary>
	public sealed class ESDelegate : ESObject, IESFunction {

		private readonly int _count;
		private readonly Delegate _value;

		public object Target {
			get { return null; }
		}

		public ESDelegate(Action func) : this((Delegate)func) {
			// ignored
		}

		public ESDelegate(Delegate value) : this(value, value.Method.GetParameters().Length) {
			// ignored
		}

		public ESDelegate(Delegate value, int count) {
			_value = value;
			_count = count;
		}

		public IESObject Invoke(IESObject[] args) {
			try {
				return ToVirtual(_value.DynamicInvoke(ToObjects(args, _count)));
			} catch (Exception e) {
				throw new InvalidOperationException(string.Format(ToString() + " Arguments: {0}", args.Format()), e);
			}
		}

		public override bool ToBoolean() {
			return _value != null;
		}

		public override string ToString() {
			return string.Format("ESDelegate Target: {0}, Method: {1}", _value.Target, _value.Method);
		}

	}

}