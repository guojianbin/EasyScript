using System;
using Easily.ES;

static class Program {

	static void Main(string[] args) {
		var evm = new EVM();
		evm.SetValue("print", new Action<object>(Console.WriteLine));
		const string script = @"
func calc() {
	print(2 + 3)
	print(2 - 3)
	print(2 * 3)
	print(2 / 3)
}
func ctrl() {
	n = 1
	if (n > 0) {
		print(""n > 0"")
	} else {
		print(""n < 0"")
	}
	arr = [1,2,3]
	for (i = 0, arr.count) {
		print(arr[i])
	}
	foreach (i in arr) {
		print(i)
	}
	map = {a:1, b:2}
	foreach (o in map) {
		print(o)
	}
	print(map[""a""])
	print(map.b)
}
func closure() {
	n = 100
	f = func() {
		n = n + 1
		return n
	}
	for (i = 0, 10)  {
		print(f())
	}
	f2 = func(m) {
		return f() + m
	}
	for (i = 0, 10)  {
		print(f2(100))
	}
}
func main() {
	print(""hello world!"")
	calc()
	ctrl()
	closure()
}
main()
";
		evm.Execute(script);
	}

}
