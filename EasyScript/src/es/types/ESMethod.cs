﻿using System;
using System.Reflection;
using Easily.Utility;

namespace Easily.ES {

	/// <summary>
	/// @author Easily
	/// </summary>
	public sealed class ESMethod : ESObject, IESFunction {

		private readonly int _count;
		private readonly object _target;
		private readonly MethodInfo _value;

		public object Target {
			get { return _target; }
		}

		public ESMethod(MethodInfo value) : this(null, value) {
			// ignored
		}

		public ESMethod(object target, MethodInfo value) : this(target, value, value.GetParameters().Length) {
			// ignored
		}

		public ESMethod(object target, MethodInfo value, int count) {
			_target = target;
			_value = value;
			_count = count;
		}

		public IESObject Invoke(IESObject[] args) {
			try {
				return ToVirtual(_value.Invoke(_target, ToObjects(args, _count)));
			} catch (Exception e) {
				throw new InvalidOperationException(string.Format(ToString() + " Arguments: {0}", args.Format()), e);
			}
		}

		public override bool ToBoolean() {
			return _value != null;
		}

		public override string ToString() {
			return string.Format("ESMethod Target: {0}, Value: {1}", _target, _value);
		}

	}

}