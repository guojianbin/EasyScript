using System;
using System.Linq;
using Easily.Bases;
using Easily.Utility;

namespace Easily.ES {

	/// <summary>
	/// @author Easily
	/// </summary>
	public sealed class ESType : ESObject, IESClass {

		private readonly Type _value;

		public Type Value {
			get { return _value; }
		}

		public ESType(Type value) {
			_value = value;
		}

		public override bool ToBoolean() {
			return _value != null;
		}

		public override object ToObject() {
			return _value;
		}

		public override IESObject GetProperty(string name) {
			return ESUtility.GetProperty(_value, name);
		}

		public override IESObject GetMethod(string name, int count) {
			return ESUtility.GetMethod(_value, name, count);
		}

		public IESObject New(ESContext context, IESObject[] args) {
			try {
				var objects = ESUtility.ToObjects(args, args.Length);
				var ctor = _value.GetConstructor(ESUtility.ConvertTo(objects, t => t.GetType()));
				return ToVirtual(ctor.Invoke(objects));
			} catch (Exception e) {
				throw new InvalidOperationException(string.Format(ToString() + " Arguments: {0}", args.Format()), e);
			}
		}

		public override string ToString() {
			return string.Format("ESType Value: {0}", _value);
		}

	}

}