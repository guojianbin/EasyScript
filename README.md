# EasyScript
A script run in C#

test code:
```
function calc() {
	print(2 + 3)
	print(2 - 3)
	print(2 * 3)
	print(2 / 3)
}
function ctrl() {
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
function closure() {
	n = 100
	f = function() {
		n = n + 1
		return n
	}
	for (i = 0, 10)  {
		print(f())
	}
	f2 = function(m) {
		return f() + m
	}
	for (i = 0, 10)  {
		print(f2(100))
	}
}
function main() {
	print(""hello world!"")
	calc()
	ctrl()
	closure()
}
main()
```

output:
```
hello world!
5
-1
6
0.6666667
n > 0
1
2
3
1
2
3
[a, ESNumber Value: 1]
[b, ESNumber Value: 2]
1
2
101
102
103
104
105
106
107
108
109
110
211
212
213
214
215
216
217
218
219
220
```