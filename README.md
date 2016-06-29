# EasyScript
A script run in C#

test code:
```
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
}
func main() {
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
```