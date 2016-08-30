using Easily.Bases;

namespace Easily.ES {

	/// <summary>
	/// @author Easily
	/// </summary>
	public class EVM : Disposable {

		private readonly ESContext _context;
		private readonly ESLibrary _libs;
		private readonly Parser _parser;

		public EVM() {
			_parser = new Parser();
			_context = new ESContext();
			_libs = new ESLibrary(this);
		}

		public void SetValue(string key, object value) {
			_context.SetValue(key, ESUtility.ToVirtual(value));
		}

		public IESObject GetValue(string key) {
			return _context.GetValue(key);
		}

		public T GetValue<T>(string key) {
			return _context.GetValue<T>(key);
		}

		public IESObject Remove(string key) {
			var value = GetValue(key);
			_context.Remove(key);
			return value;
		}

		public bool Contains(string key) {
			return _context.Contains(key);
		}

		public void Invoke(string name) {
			GetValue<IESFunction>(name).Invoke(ESUtility.ToVirtuals(null, 0));
		}

		public void Invoke(string name, params object[] args) {
			GetValue<IESFunction>(name).Invoke(ESUtility.ToVirtuals(args, args.Length));
		}

		public void Execute(Module module) {
			module.Execute(_context);
		}

		public void Execute(string script) {
			Execute(Compile(script));
		}

		public ESContext Evaluate(Module module) {
			return module.Evaluate(_context);
		}

		public ESContext Evaluate(string script) {
			return Evaluate(Compile(script));
		}

		public Module Compile(string script) {
			return _parser.Parse(Scanner.Parse(script));
		}

		protected override void OnDispose() {
			base.OnDispose();
			_libs.Dispose();
			_context.Dispose();
			_parser.Dispose();
		}

	}

}