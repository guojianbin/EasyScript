using System;
using System.Collections.Generic;
using Engine.Bases;
using Engine.DS;

namespace Engine.ES {

/// <summary>
/// @author Easily
/// </summary>
public class Chunk : Disposable {

	private string _name;
	private readonly Stack<IESObject> _stack = new Stack<IESObject>();
	private readonly EasyList<EIL> _ils = new EasyList<EIL>();
	private int _position = -1;

	public Chunk(string name) {
		_name = name;
	}

	public IESObject Execute() {
		return MoveNext();
	}

	private IESObject MoveNext() {
		_position += 1;
		if (_position > _ils.Count) {
			return Complete();
		} else {
			Execute(_ils[_position]);
			return MoveNext();
		}
	}

	private void Execute(EIL il) {
		switch (il.type) {
			case ILType.PUSH:
				_stack.Push(il.target);
				break;
			case ILType.POP:
				_stack.Pop();
				break;
			case ILType.CLR:
				_stack.Clear();
				break;
			case ILType.ADD:
				_stack.Push(new ESNumber(_stack.Pop().GetNumber() + _stack.Pop().GetNumber()));
				break;
			case ILType.SUB:
				_stack.Push(new ESNumber(_stack.Pop().GetNumber() - _stack.Pop().GetNumber()));
				break;
			case ILType.MUL:
				_stack.Push(new ESNumber(_stack.Pop().GetNumber() * _stack.Pop().GetNumber()));
				break;
			case ILType.DIV:
				_stack.Push(new ESNumber(_stack.Pop().GetNumber() / _stack.Pop().GetNumber()));
				break;
			case ILType.NOT:
				_stack.Push(new ESBoolean(!_stack.Pop().Cast<ESBoolean>().Value));
				break;
			case ILType.AND:
				_stack.Push(new ESBoolean(_stack.Pop().Cast<ESBoolean>().Value && _stack.Pop().Cast<ESBoolean>().Value));
				break;
			case ILType.OR:
				_stack.Push(new ESBoolean(_stack.Pop().Cast<ESBoolean>().Value || _stack.Pop().Cast<ESBoolean>().Value));
				break;
			case ILType.EQ:
				_stack.Push(new ESBoolean(_stack.Pop().ToObject().Equals(_stack.Pop().ToObject())));
				break;
			case ILType.NEQ:
				_stack.Push(new ESBoolean(!_stack.Pop().ToObject().Equals(_stack.Pop().ToObject())));
				break;
			case ILType.GT:
				_stack.Push(new ESBoolean(_stack.Pop().GetNumber() > _stack.Pop().GetNumber()));
				break;
			case ILType.GE:
				_stack.Push(new ESBoolean(_stack.Pop().GetNumber() >= _stack.Pop().GetNumber()));
				break;
			case ILType.LT:
				_stack.Push(new ESBoolean(_stack.Pop().GetNumber() < _stack.Pop().GetNumber()));
				break;
			case ILType.LE:
				_stack.Push(new ESBoolean(_stack.Pop().GetNumber() <= _stack.Pop().GetNumber()));
				break;
			case ILType.CALL:
				_stack.Pop().GetString();
				break;
			case ILType.RET:
				_position = _ils.Count;
				break;
			default:
				throw new ArgumentOutOfRangeException(il.type.ToString());
		}
	}

	private IESObject Complete() {
		_position = -1;
		if (_stack.Count > 0) {
			var ret = _stack.Pop();
			_stack.Clear();
			return ret;
		} else {
			return ESDefault.Value;
		}
	}

}

}