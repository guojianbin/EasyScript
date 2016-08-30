using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Easily.Bases;

namespace Easily.ES {

	/// <summary>
	/// @author Easily
	/// </summary>
	public static class ESUtility {

		private const BindingFlags _instance = BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance;
		private const BindingFlags _static = BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static;

		private static readonly Dictionary<TokenType, Func<Token, IExpression>> _t2e = new Dictionary<TokenType, Func<Token, IExpression>>();
		private static readonly HashSet<Type> _numTypes = new HashSet<Type>(new[] {typeof(float), typeof(double), typeof(int), typeof(uint), typeof(long), typeof(ulong), typeof(byte), typeof(sbyte), typeof(char), typeof(short), typeof(ushort)});
		private static readonly IESObject[] _0Vtrs = new IESObject[0];
		private static readonly object[] _0Objs = new object[0];

		private static readonly ExpressionStringArgs _stringArgs = new ExpressionStringArgs();
		private static readonly ExpressionArrayArgs _arrayArgs = new ExpressionArrayArgs();
		private static readonly ExpressionTableArgs _tableArgs = new ExpressionTableArgs();

		static ESUtility() {
			_t2e.Add(TokenType.CMA, t => new MarkerCma());
			_t2e.Add(TokenType.DOT, t => new MarkerDot());
			_t2e.Add(TokenType.COL, t => new MarkerCol());
			_t2e.Add(TokenType.LT, t => new MarkerLt());
			_t2e.Add(TokenType.GT, t => new MarkerGt());
			_t2e.Add(TokenType.MUL, t => new MarkerMul());
			_t2e.Add(TokenType.DIV, t => new MarkerDiv());
			_t2e.Add(TokenType.ADD, t => new MarkerAdd());
			_t2e.Add(TokenType.SUB, t => new MarkerSub());
			_t2e.Add(TokenType.EQ, t => new MarkerEq());
			_t2e.Add(TokenType.EX, t => new MarkerEx());
			_t2e.Add(TokenType.RET, t => new MarkerRet());
			_t2e.Add(TokenType.WHILE, t => new MarkerWhile());
			_t2e.Add(TokenType.FUNC, t => new MarkerFunc());
			_t2e.Add(TokenType.CLASS, t => new MarkerClass());
			_t2e.Add(TokenType.NEW, t => new MarkerNew());
			_t2e.Add(TokenType.FOR, t => new MarkerFor());
			_t2e.Add(TokenType.FOREACH, t => new MarkerForEach());
			_t2e.Add(TokenType.IN, t => new MarkerIn());
			_t2e.Add(TokenType.IF, t => new MarkerIf());
			_t2e.Add(TokenType.ELSE, t => new MarkerElse());
			_t2e.Add(TokenType.BREAK, t => new ExpressionBreak());
			_t2e.Add(TokenType.TRUE, t => new ExpressionBoolean(true));
			_t2e.Add(TokenType.FALSE, t => new ExpressionBoolean(false));
			_t2e.Add(TokenType.WORD, t => new ExpressionWord(t.Value));
			_t2e.Add(TokenType.NUM, t => new ExpressionNumber(t.Value));
			_t2e.Add(TokenType.STR, t => new ExpressionString(t.Value.Substring(1, t.Length - 2)));
			_t2e.Add(TokenType.SS, t => new ExpressionSS(t.Select(Parse).ToList()));
			_t2e.Add(TokenType.MM, t => new ExpressionMM(t.Select(Parse).ToList()));
			_t2e.Add(TokenType.BB, t => new ExpressionBB(t.Select(Parse).ToList()));
		}

		internal static IExpression Parse(Token token) {
			return _t2e[token.Type](token);
		}

		internal static List<IExpression> Parse(IEnumerable<Token> tokens) {
			return tokens.Select(Parse).ToList();
		}

		public static IESObject GetProperty(object obj, string name) {
			var type = obj.GetType();
			var field = type.GetField(name, _instance);
			if (field != null) {
				return new ESField(obj, field);
			}
			var property = type.GetProperty(name, _instance);
			if (property != null) {
				return new ESProperty(obj, property);
			}
			var method = type.GetMethod(name, _instance);
			if (method != null) {
				return new ESMethod(obj, method);
			} else {
				throw new InvalidOperationException(String.Format("Target: {0}, Name: {1}", obj, name));
			}
		}

		public static IESObject GetProperty(Type type, string name) {
			var field = type.GetField(name, _static);
			if (field != null) {
				return new ESField(field);
			}
			var property = type.GetProperty(name, _static);
			if (property != null) {
				return new ESProperty(property);
			}
			var method = type.GetMethod(name, _static);
			if (method != null) {
				return new ESMethod(method);
			} else {
				throw new InvalidOperationException(String.Format("Type: {0}, Name: {1}", type, name));
			}
		}

		public static Func<object, object> GetConverter(Type type) {
			if (type == typeof(int)) {
				return v => ToInt32(v);
			} else if (type == typeof(uint)) {
				return v => ToUInt32(v);
			} else if (type == typeof(long)) {
				return v => ToInt64(v);
			} else if (type == typeof(ulong)) {
				return v => ToUInt64(v);
			} else if (type == typeof(short)) {
				return v => ToInt16(v);
			} else if (type == typeof(ushort)) {
				return v => ToUInt16(v);
			} else {
				throw new InvalidOperationException(type.ToString());
			}
		}

		public static Type GetType(object target, string name) {
			var type = target.GetType();
			var field = type.GetField(name, _instance);
			if (field != null) {
				return field.FieldType;
			}
			var property = type.GetProperty(name, _instance);
			if (property != null) {
				return property.PropertyType;
			} else {
				throw new InvalidOperationException(String.Format("Target: {0}, Name: {1}", target, name));
			}
		}

		public static object GetValue(object obj, string name) {
			var type = obj.GetType();
			var field = type.GetField(name, _instance);
			if (field != null) {
				return field.GetValue(obj);
			}
			var property = type.GetProperty(name, _instance);
			if (property != null) {
				return property.GetValue(obj, null);
			} else {
				throw new InvalidOperationException(String.Format("Target: {0}, Name: {1}", obj, name));
			}
		}

		public static void SetValue(object obj, string name, object value) {
			var type = obj.GetType();
			var field = type.GetField(name, _instance);
			if (field != null) {
				field.SetValue(obj, value);
				return;
			}
			var property = type.GetProperty(name, _instance);
			if (property != null) {
				property.SetValue(obj, value, null);
			} else {
				throw new InvalidOperationException(String.Format("Target: {0}, Name: {1}", obj, name));
			}
		}

		public static IESObject ToVirtual(object obj) {
			if (obj == null) {
				return ESDefault.Value;
			} else if (obj is IESObject) {
				return (IESObject)obj;
			} else if (obj is bool) {
				return new ESBoolean((bool)obj);
			} else if (obj is string) {
				return new ESString((string)obj);
			} else if (obj is Delegate) {
				return new ESDelegate((Delegate)obj);
			} else if (obj is Type) {
				return new ESType((Type)obj);
			} else if (obj is IDictionary) {
				return ToTable(obj);
			} else if (obj is IEnumerator) {
				return ToList(obj);
			} else if (_numTypes.Contains(obj.GetType())) {
				return new ESNumber(obj);
			} else {
				return new ESSprite(obj);
			}
		}

		public static IESObject[] ToVirtuals(object[] objs, int count) {
			if (count == 0) {
				return _0Vtrs;
			}
			var arr = new IESObject[count];
			if (objs == null || objs.Length == 0) {
				return arr;
			}
			var len = Math.Min(objs.Length, count);
			for (var i = 0; i < len; i++) {
				arr[i] = ToVirtual(objs[i]);
			}
			return arr;
		}

		public static IESObject[] ToVirtuals(object[] objs) {
			return objs == null ? _0Vtrs : ToVirtuals(objs, objs.Length);
		}

		public static IESObject[] ToVirtuals<T>(IEnumerable<T> list) {
			return list == null ? _0Vtrs : ToVirtuals(list.Cast<object>().ToArray());
		}

		public static IESObject[] ToVirtuals(IEnumerable list) {
			return list == null ? _0Vtrs : ToVirtuals(list.Cast<object>());
		}

		public static IESObject[] ToArray(ESContext context, List<IExpressionRight> _args) {
			return _args.Count == 0 ? _0Vtrs : _args.Select(t => t.Execute(context)).ToArray();
		}

		public static object[] ToObjects(IESObject[] objs, int count) {
			if (count == 0) {
				return _0Objs;
			}
			var arr = new object[count];
			if (objs == null || objs.Length == 0) {
				return arr;
			}
			var len = Math.Min(objs.Length, count);
			for (var i = 0; i < len; i++) {
				arr[i] = objs[i].ToObject();
			}
			return arr;
		}

		public static IESObject GetProperty(ESArray array, string name) {
			if (name == "add") {
				return ToVirtual(new Action<object>(v => array.Add(ToVirtual(v))));
			} else if (name == "remove") {
				return ToVirtual(new Action<float>(i => array.RemoveAt((int)i)));
			} else if (name == "insert") {
				return ToVirtual(new Action<float, object>((i, o) => array.Insert((int)i, ToVirtual(o))));
			} else if (name == "clear") {
				return ToVirtual(new Action(array.Clear));
			} else if (name == "count") {
				return new ESSprite(array.Count);
			} else {
				return GetProperty(array.Source, name);
			}
		}

		public static IESObject GetProperty(ESTable table, string name) {
			if (name == "add") {
				return ToVirtual(new Action<string, object>((k, v) => table.Add(k, ToVirtual(v))));
			} else if (name == "remove") {
				return ToVirtual(new Action<string>(k => table.Remove(k)));
			} else if (name == "clear") {
				return ToVirtual(new Action(table.Clear));
			} else if (name == "keys") {
				return ToVirtual(table.Keys);
			} else if (name == "values") {
				return ToVirtual(table.Values);
			} else if (name == "count") {
				return new ESNumber(table.Count);
			} else {
				return new ESKey(table, name);
			}
		}

		public static ESArray ToList(object obj) {
			if (obj is ESArray) {
				return (ESArray)obj;
			} else if (obj is IEnumerable) {
				return new ESArray((IEnumerable)obj);
			} else if (obj is ESSprite) {
				return new ESArray(((ESSprite)obj).GetValue<IEnumerable>());
			} else {
				throw new InvalidCastException(obj.ToString());
			}
		}

		public static ESTable ToTable(object obj) {
			if (obj is ESTable) {
				return (ESTable)obj;
			} else if (obj is IEnumerable) {
				return new ESTable((IEnumerable)obj);
			} else if (obj is ESSprite) {
				return new ESTable(((ESSprite)obj).GetValue<IEnumerable>());
			} else {
				throw new InvalidCastException(obj.ToString());
			}
		}

		public static ExpressionStringArgs ToStringArgs(IExpressionList list) {
			return list.Count > 0 ? list[0].Cast<ExpressionStringArgs>() : _stringArgs;
		}

		public static ExpressionArrayArgs ToArrayArgs(IExpressionList list) {
			return list.Count > 0 ? list[0].Cast<ExpressionArrayArgs>() : _arrayArgs;
		}

		public static ExpressionTableArgs ToTableArgs(IExpressionList list) {
			return list.Count > 0 ? list[0].Cast<ExpressionTableArgs>() : _tableArgs;
		}

		public static byte ToByte(object value) {
			return Convert.ToByte(value);
		}

		public static byte ToByte(string value) {
			return Convert.ToByte(value);
		}

		public static float ToFloat(object value) {
			return Convert.ToSingle(value);
		}

		public static float ToFloat(string value) {
			return Convert.ToSingle(value);
		}

		public static float ToFloat(int value) {
			return Convert.ToSingle(value);
		}

		public static short ToInt16(object value) {
			return Convert.ToInt16(value);
		}

		public static ushort ToUInt16(object value) {
			return Convert.ToUInt16(value);
		}

		public static int ToInt32(object value) {
			return Convert.ToInt32(value);
		}

		public static int ToInt32(float value) {
			return Convert.ToInt32(value);
		}

		public static int ToInt32(string value) {
			return Convert.ToInt32(value);
		}

		public static uint ToUInt32(object value) {
			return Convert.ToUInt32(value);
		}

		public static uint ToUInt32(float value) {
			return Convert.ToUInt32(value);
		}

		public static uint ToUInt32(string value) {
			return Convert.ToUInt32(value);
		}

		public static long ToInt64(object value) {
			return Convert.ToInt64(value);
		}

		public static ulong ToUInt64(object value) {
			return Convert.ToUInt64(value);
		}

		public static string ToString(object value) {
			return Convert.ToString(value);
		}

	}

}