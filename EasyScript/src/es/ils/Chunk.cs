using System;
using System.Collections.Generic;
using Easily.Bases;

namespace Easily.ES {

	/// <summary>
	/// @author Easily
	/// </summary>
	public class Chunk : Disposable {

//		private readonly EasyBag<string, EIL> _cmds = new EasyBag<string, EIL>();
		private readonly ESContext _context = new ESContext();
		private readonly Dictionary<string, IESObject> _env = new Dictionary<string, IESObject>();
		private readonly string _name;
		private readonly Stack<IESObject> _stack = new Stack<IESObject>();
		private readonly EVM _vm;

		public Chunk(EVM vm, string name) {
			_vm = vm;
			_name = name;
		}

		public void Execute() {
//			for (var i = 0; i < _cmds.Count; i++) {
//				var cmd = _cmds[i];
//				switch (cmd.type) {
//					case ILType.PUSH:
//						_stack.Push(cmd.target);
//						break;
//					case ILType.POP:
//						_stack.Pop();
//						break;
//					case ILType.CLR:
//						_stack.Clear();
//						break;
//					case ILType.BIND:
//						cmd.target.Cast<IESBinder>().SetValue(_context, _stack.Pop());
//						break;
//					case ILType.LOAD:
//						_stack.Push(_env[cmd.target.GetString()]);
//						break;
//					case ILType.ADD:
//						_stack.Push(new ESNumber(_stack.Pop().GetNumber() + _stack.Pop().GetNumber()));
//						break;
//					case ILType.SUB:
//						_stack.Push(new ESNumber(_stack.Pop().GetNumber() - _stack.Pop().GetNumber()));
//						break;
//					case ILType.MUL:
//						_stack.Push(new ESNumber(_stack.Pop().GetNumber() * _stack.Pop().GetNumber()));
//						break;
//					case ILType.DIV:
//						_stack.Push(new ESNumber(_stack.Pop().GetNumber() / _stack.Pop().GetNumber()));
//						break;
//					case ILType.NOT:
//						_stack.Push(new ESBoolean(!_stack.Pop().Cast<ESBoolean>().Value));
//						break;
//					case ILType.AND:
//						_stack.Push(new ESBoolean(_stack.Pop().Cast<ESBoolean>().Value && _stack.Pop().Cast<ESBoolean>().Value));
//						break;
//					case ILType.OR:
//						_stack.Push(new ESBoolean(_stack.Pop().Cast<ESBoolean>().Value || _stack.Pop().Cast<ESBoolean>().Value));
//						break;
//					case ILType.EQ:
//						_stack.Push(new ESBoolean(_stack.Pop().ToObject().Equals(_stack.Pop().ToObject())));
//						break;
//					case ILType.NEQ:
//						_stack.Push(new ESBoolean(!_stack.Pop().ToObject().Equals(_stack.Pop().ToObject())));
//						break;
//					case ILType.GT:
//						_stack.Push(new ESBoolean(_stack.Pop().GetNumber() > _stack.Pop().GetNumber()));
//						break;
//					case ILType.GE:
//						_stack.Push(new ESBoolean(_stack.Pop().GetNumber() >= _stack.Pop().GetNumber()));
//						break;
//					case ILType.LT:
//						_stack.Push(new ESBoolean(_stack.Pop().GetNumber() < _stack.Pop().GetNumber()));
//						break;
//					case ILType.LE:
//						_stack.Push(new ESBoolean(_stack.Pop().GetNumber() <= _stack.Pop().GetNumber()));
//						break;
//					case ILType.RET:
//						i = _cmds.Count;
//						break;
//					case ILType.PROP:
//						_stack.Push(cmd.target.GetProperty(_stack.Pop().GetString()));
//						break;
//					case ILType.JUMP:
//						i = _cmds.IndexOf(cmd.target.GetString()) - 1;
//						break;
//					case ILType.JUMP_IF:
//						if (_stack.Pop().ToBoolean()) {
//							i = _cmds.IndexOf(cmd.target.GetString()) - 1;
//						}
//						break;
//					case ILType.CALL:
//						var count = cmd.target.GetInteger();
//						var args = new IESObject[count];
//						for (var j = 0; j < count; j++) {
//							args[j] = _stack.Pop();
//						}
//						_stack.Pop().Cast<IESFunction>().Invoke(args);
//						break;
//					default:
//						throw new ArgumentOutOfRangeException(cmd.type.ToString());
//				}
//			}
		}

	}

}