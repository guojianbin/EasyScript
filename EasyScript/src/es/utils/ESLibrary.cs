using System;
using System.IO;
using Easily.Bases;

namespace Easily.ES {

	/// <summary>
	/// @author Easily
	/// </summary>
	internal class ESLibrary : Disposable {

		private readonly ESTable _math;
		private readonly ESTable _string;
		private readonly ESTable _file;

		internal ESLibrary(EVM vm) {
			_string = new ESTable();
			_math = new ESTable();
			_file = new ESTable();

			vm.SetValue("type", typeof(Type));
			vm.SetValue("tostring", new Func<object, string>(t => t.ToString()));
			vm.SetValue("typeof", new Func<object, Type>(t => t.GetType()));
			vm.SetValue("tolist", new Func<object, IESObject>(ESUtility.ToList));
			vm.SetValue("totable", new Func<object, IESObject>(ESUtility.ToTable));
			vm.SetValue("tonumber", new Func<object, float>(ESObject.ToFloat));

			vm.SetValue("string", _string);
			_string.Add("format", new Func<string, object, string>(string.Format));
			_string.Add("format2", new Func<string, object, object, string>(string.Format));
			_string.Add("format3", new Func<string, object, object, object, string>(string.Format));
			_string.Add("concat", new Func<string, string, string>(string.Concat));
			_string.Add("concat3", new Func<string, string, string, string>(string.Concat));
			_string.Add("concat4", new Func<string, string, string, string, string>(string.Concat));

			vm.SetValue("math", _math);
			_math.Add("min", new Func<float, float, float>(Math.Min));
			_math.Add("max", new Func<float, float, float>(Math.Max));
			_math.Add("abs", new Func<float, float>(Math.Abs));

			vm.SetValue("file", _file);
			_file.Add("write", new Action<string, string>(File.WriteAllText));
			_file.Add("read", new Func<string, string>(File.ReadAllText));
		}

		protected override void OnDispose() {
			base.OnDispose();
			_string.Dispose();
			_math.Dispose();
			_file.Dispose();
		}

	}

}