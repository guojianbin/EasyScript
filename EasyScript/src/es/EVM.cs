using Engine.Bases;

namespace Easily.ES {

/// <summary>
/// @author Easily
/// </summary>
public class EVM : Disposable {

	private readonly Parser _parser;
	private readonly ESDomain _domain;
	private readonly ESLibrary _libs;

	public EVM() {
		_parser = new Parser();
		_domain = new ESDomain();
		_libs = new ESLibrary(this);
	}

	public void SetValue(string key, object value) {
		_domain.SetValue(key, ESUtility.ToVirtual(value));
	}

	public IESObject GetValue(string key) {
		return _domain.GetValue(key);
	}

	public T GetValue<T>(string key) {
		return _domain.GetValue<T>(key);
	}

	public IESObject Remove(string key) {
		var value = GetValue(key);
		_domain.Remove(key);
		return value;
	}

	public bool Contains(string key) {
		return _domain.Contains(key);
	}

	public void Invoke(string name) {
		GetValue<IESFunction>(name).Invoke(ESUtility.ToVirtuals(null, 0));
	}

	public void Invoke(string name, params object[] args) {
		GetValue<IESFunction>(name).Invoke(ESUtility.ToVirtuals(args, args.Length));
	}

	public void Execute(Module module) {
		module.Execute(_domain);
	}

	public void Execute(string script) {
		Execute(Compile(script));
	}

	public ESDomain Evaluate(Module module) {
		return module.Evaluate(_domain);
	}

	public Module Compile(string script) {
		return _parser.Execute(Scanner.Parse(script));
	}

	protected override void OnDispose() {
		base.OnDispose();
		_libs.Dispose();
		_domain.Dispose();
		_parser.Dispose();
	}

}

}