using System;
using System.Collections.Generic;
using System.Linq;
using Engine.Bases;

namespace Engine.ES {

/// <summary>
/// @author Easily
/// </summary>
public class Parser : Disposable {

	private readonly Dictionary<TokenType, Func<Token, IExpression>> _t2e = new Dictionary<TokenType, Func<Token, IExpression>>();

	public Parser() {
		_t2e.Add(TokenType.CMA, t => new CmaMarker());
		_t2e.Add(TokenType.DOT, t => new DotMarker());
		_t2e.Add(TokenType.COL, t => new ColMarker());
		_t2e.Add(TokenType.LT, t => new LtMarker());
		_t2e.Add(TokenType.GT, t => new GtMarker());
		_t2e.Add(TokenType.MUL, t => new MulMarker());
		_t2e.Add(TokenType.DIV, t => new DivMarker());
		_t2e.Add(TokenType.PLUS, t => new PlusMarker());
		_t2e.Add(TokenType.SUB, t => new SubMarker());
		_t2e.Add(TokenType.EQ, t => new EqMarker());
		_t2e.Add(TokenType.EX, t => new ExMarker());
		_t2e.Add(TokenType.RET, t => new RetMarker());
		_t2e.Add(TokenType.CATCH, t => new CatchMarker());
		_t2e.Add(TokenType.BREAK, t => new BreakMarker());
		_t2e.Add(TokenType.WHILE, t => new WhileMarker());
		_t2e.Add(TokenType.FUNC, t => new FuncMarker());
		_t2e.Add(TokenType.FOR, t => new ForMarker());
		_t2e.Add(TokenType.FOREACH, t => new ForEachMarker());
		_t2e.Add(TokenType.IN, t => new InMarker());
		_t2e.Add(TokenType.IF, t => new IfMarker());
		_t2e.Add(TokenType.ELSE, t => new ElseMarker());
		_t2e.Add(TokenType.TRUE, t => new BooleanExpression(true));
		_t2e.Add(TokenType.FALSE, t => new BooleanExpression(false));
		_t2e.Add(TokenType.WORD, t => new WordExpression(t.Value));
		_t2e.Add(TokenType.NUM, t => new NumberExpression(t.Value));
		_t2e.Add(TokenType.STR, t => new StringExpression(t.Value.Substring(1, t.Length - 2)));
		_t2e.Add(TokenType.SS, t => new SSExpression(t.Tokens.Select(v => Parse(v)).ToList()));
		_t2e.Add(TokenType.MM, t => new MMExpression(t.Tokens.Select(v => Parse(v)).ToList()));
		_t2e.Add(TokenType.BB, t => new BBExpression(t.Tokens.Select(v => Parse(v)).ToList()));
	}

	public Module Execute(List<Token> tokens) {
		return new Module(Parse(tokens.Select(t => Parse(t)).ToList()));
	}

	private IExpression Parse(Token token) {
		if (_t2e.ContainsKey(token.Type)) {
			return _t2e[token.Type](token);
		} else {
			throw new ArgumentException(token.Type.ToString());
		}
	}

	private List<IExpression> Parse(List<IExpression> list) {
		ParseLv0(list);
		ParseLv1(list);
		ParseLv2(list);
		ParseLv3(list);
		ParseLv4(list);
		ParseLv5(list);
		return list;
	}

	private void ParseLv0(List<IExpression> list) {
		var pos = -1;
		while (pos + 1 < list.Count && (pos = list.FindIndex(pos + 1, e => e is IfMarker)) != -1) {
			if (pos + 1 < list.Count && list[pos + 1] is SSExpression 
			    && pos + 2 < list.Count && list[pos + 2] is BBExpression
			    && pos + 3 < list.Count && list[pos + 3] is ElseMarker
			    && pos + 4 < list.Count && list[pos + 4] is BBExpression) {// if-else
				var item1 = list[pos + 1].Cast<SSExpression>();
				var item2 = list[pos + 2].Cast<BBExpression>();
				var item3 = list[pos + 4].Cast<BBExpression>();
				list.RemoveRange(pos, 5);
				Parse(item1.Values);
				Parse(item2.Values);
				Parse(item3.Values);
				list.Insert(pos, new IfElseExpression(item1.Unbound().Cast<ILogicExpression>(), item2.Unbound(), item3.Unbound()));
			}
		}

		pos = -1;
		while (pos + 1 < list.Count && (pos = list.FindIndex(pos + 1, e => e is IfMarker)) != -1) {
			if (pos + 1 < list.Count && list[pos + 1] is SSExpression
			    && pos + 2 < list.Count && list[pos + 2] is BBExpression) {// if
				var item1 = list[pos + 1].Cast<SSExpression>();
				var item2 = list[pos + 2].Cast<BBExpression>();
				list.RemoveRange(pos, 3);
				Parse(item1.Values);
				Parse(item2.Values);
				list.Insert(pos, new IfExpression(item1.Unbound().Cast<ILogicExpression>(), item2.Unbound()));
			}
		}

		pos = -1;
		while (pos + 1 < list.Count && (pos = list.FindIndex(pos + 1, e => e is WhileMarker)) != -1) {
			if (pos + 1 < list.Count && list[pos + 1] is SSExpression
			    && pos + 2 < list.Count && list[pos + 2] is BBExpression) {// while
				var item1 = list[pos + 1].Cast<SSExpression>();
				var item2 = list[pos + 2].Cast<BBExpression>();
				list.RemoveRange(pos, 3);
				Parse(item1.Values);
				Parse(item2.Values);
				list.Insert(pos, new WhileExpression(item1.Unbound().Cast<ILogicExpression>(), item2.Unbound()));
			}
		}

		pos = -1;
		while (pos + 1 < list.Count && (pos = list.FindIndex(pos + 1, e => e is ForMarker)) != -1) {
			if (pos + 1 < list.Count && list[pos + 1] is SSExpression
			    && pos + 2 < list.Count && list[pos + 2] is BBExpression) {// for
				var item1 = (SSExpression)list[pos + 1];
				var item2 = (BBExpression)list[pos + 2];
				list.RemoveRange(pos, 3);
				Parse(item1.Values);
				Parse(item2.Values);
				if (item1.Count == 3) {// for(i=0,n)
					if (item1.Values[0] is AssignExpression
						&& item1.Values[1] is CmaMarker
						&& item1.Values[2] is IRightExpression) {
						var assign = item1.Values[0].Cast<AssignExpression>();
						list.Insert(pos, new ForExpression(
							assign.LValue.Cast<INameExpression>(), assign.RValue, item1.Values[2].Cast<IRightExpression>(), item2.Unbound()));
					}
				} else if (item1.Count == 5) {// for(i=0,n,s)
					if (item1.Values[0] is AssignExpression
						&& item1.Values[1] is CmaMarker
						&& item1.Values[2] is IRightExpression
						&& item1.Values[3] is CmaMarker
						&& item1.Values[4] is IRightExpression) {
						var assign = item1.Values[0].Cast<AssignExpression>();
						list.Insert(pos, new ForExpression(
							assign.LValue.Cast<INameExpression>(), assign.RValue, 
							item1.Values[2].Cast<IRightExpression>(), item1.Values[4].Cast<IRightExpression>(), item2.Unbound()));
					}
				}
			} else if (pos + 1 < list.Count && list[pos + 1] is SSExpression
				&& pos + 2 < list.Count && list[pos + 2] is CatchMarker
				&& pos + 3 < list.Count && list[pos + 3] is BBExpression) {// for catch
				var item1 = list[pos + 1].Cast<SSExpression>();
				var item2 = list[pos + 3].Cast<BBExpression>();
				list.RemoveRange(pos, 4);
				Parse(item1.Values);
				Parse(item2.Values);
				if (item1.Count == 3) {// for(i=0,n)catch
					if (item1.Values[0] is AssignExpression
						&& item1.Values[1] is CmaMarker
						&& item1.Values[2] is IRightExpression) {
						var assign = item1.Values[0].Cast<AssignExpression>();
						list.Insert(pos, new ForCatchExpression(
							assign.LValue.Cast<INameExpression>(), assign.RValue, 
							item1.Values[2].Cast<IRightExpression>(), item2.Unbound()));
					}
				} else if (item1.Count == 5) {// for(i=0,n,s)catch
					if (item1.Values[0] is AssignExpression
						&& item1.Values[1] is CmaMarker
						&& item1.Values[2] is IRightExpression
						&& item1.Values[3] is CmaMarker
						&& item1.Values[4] is IRightExpression) {
						var assign = (AssignExpression)item1.Values[0];
						list.Insert(pos, new ForCatchExpression((INameExpression)assign.LValue,
							assign.RValue, (IRightExpression)item1.Values[2], (IRightExpression)item1.Values[4], item2.Unbound()));
					}
				}
			}
		}

		pos = -1;
		while (pos + 1 < list.Count && (pos = list.FindIndex(pos + 1, e => e is ForEachMarker)) != -1) {
			if (pos + 1 < list.Count && list[pos + 1] is SSExpression
			    && pos + 2 < list.Count && list[pos + 2] is BBExpression) {
				var item1 = (SSExpression)list[pos + 1];
				var item2 = (BBExpression)list[pos + 2];
				list.RemoveRange(pos, 3);
				Parse(item1.Values);
				Parse(item2.Values);
				if (item1.Count == 3) { // foreach(x in l)
					list.Insert(pos, new ForEachExpression(
						item1.Values[0].Cast<INameExpression>(), item1.Values[2].Cast<IRightExpression>(), item2));
				}
			} else if (pos + 1 < list.Count && list[pos + 1] is SSExpression
			    && pos + 2 < list.Count && list[pos + 2] is CatchMarker
			    && pos + 3 < list.Count && list[pos + 3] is BBExpression) {
				var item1 = (SSExpression)list[pos + 1];
				var item2 = (BBExpression)list[pos + 3];
				list.RemoveRange(pos, 4);
				Parse(item1.Values);
				Parse(item2.Values);
				if (item1.Count == 3) { // foreach(x in l)catch
					list.Insert(pos, new ForEachCatchExpression(
						item1.Values[0].Cast<INameExpression>(), item1.Values[2].Cast<IRightExpression>(), item2));
				}
			}
		}

		pos = -1;
		while (pos + 1 < list.Count && (pos = list.FindIndex(pos + 1, e => e is FuncMarker)) != -1) {
			if (pos + 1 < list.Count && list[pos + 1] is INameExpression 
			    && pos + 2 < list.Count && list[pos + 2] is SSExpression
			    && pos + 3 < list.Count && list[pos + 3] is BBExpression) {// func name()
				var item1 = list[pos + 1].Cast<INameExpression>();
				var item2 = list[pos + 2].Cast<SSExpression>();
				var item3 = list[pos + 3].Cast<BBExpression>();
				list.RemoveRange(pos, 4);
				var args = new List<string>();
				Parse(item2.Values, args);
				Parse(item3.Values);
				list.Insert(pos, new FuncExpression(item1.Name, args, item3.Unbound()));
			} else if (pos + 1 < list.Count && list[pos + 1] is SSExpression
				&& pos + 2 < list.Count && list[pos + 2] is BBExpression) {// func()
				var item1 = list[pos + 1].Cast<SSExpression>();
				var item2 = list[pos + 2].Cast<BBExpression>();
				list.RemoveRange(pos, 3);
				var args = new List<string>();
				Parse(item1.Values, args);
				Parse(item2.Values);
				list.Insert(pos, new FuncExpression(args, item2.Unbound()));
			}
		}

		pos = -1;
		while (pos + 1 < list.Count && (pos = list.FindIndex(pos + 1, e => e is BreakMarker)) != -1) {//break
			list[pos] = new BreakExpression();
		}
	}

	private void ParseLv1(List<IExpression> list) {
		var pos = 0;
		while (true) {
			pos = list.FindIndex(pos, t => t is DotMarker || t is SSExpression || t is MMExpression);
			if (pos == -1) {
				break;
			}
			if (list[pos] is DotMarker) { // x.x
				if (pos - 1 >= 0 && pos - 1 < list.Count && list[pos - 1] is IRightExpression
					&& pos + 1 < list.Count && list[pos + 1] is INameExpression) {
					var item1 = list[pos - 1].Cast<IRightExpression>();
					var item2 = list[pos + 1].Cast<INameExpression>();
					list.RemoveRange(pos - 1, 3);
					list.Insert(pos - 1, new PropertyExpression(item1, item2));
				}
			} else if (list[pos] is SSExpression) {
				if (pos - 1 >= 0 && pos - 1 < list.Count && list[pos - 1] is IRightExpression) {// x()
					var item1 = list[pos - 1].Cast<IRightExpression>();
					var item2 = list[pos].Cast<SSExpression>();
					list.RemoveRange(pos - 1, 2);
					Parse(item2.Values);
					var args = new List<IRightExpression>();
					Parse(item2.Values, args);
					list.Insert(pos - 1, new InvokeExpression(item1, args));
				} else { //()
					Parse(((SSExpression)list[pos]).Values);
					list[pos] = ((SSExpression)list[pos]).Unbound();
					pos += 1;
				}
			} else if (list[pos] is MMExpression) { //x[]
				if (pos - 1 >= 0 && pos - 1 < list.Count && list[pos - 1] is IRightExpression) {
					var item1 = list[pos - 1].Cast<IRightExpression>();
					var item2 = list[pos].Cast<MMExpression>();
					list.RemoveRange(pos - 1, 2);
					Parse(item2.Values);
					list.Insert(pos - 1, new IndexExpression(item1, (IRightExpression)item2.Unbound()));
				} else {
					pos += 1;
				}
			}
		}
	}

	private void ParseLv2(List<IExpression> list) {
		var pos = -1;
		while (pos + 1 < list.Count && (pos = list.FindIndex(pos + 1, e => e is SubMarker)) != -1) {
			if (pos == 0 || (pos - 1 >= 0 && pos - 1 < list.Count && !(list[pos - 1] is IRightExpression))
				&& pos + 1 < list.Count && list[pos + 1] is IRightExpression) {// -x
				var item = list[pos + 1].Cast<IRightExpression>();
				list.RemoveRange(pos, 2);
				list.Insert(pos, new NegativeExpression(item));
			}
		}

		pos = -1;
		while (pos + 1 < list.Count && (pos = list.FindIndex(pos + 1, e => e is MulMarker)) != -1) {
			if (pos - 1 >= 0 && pos - 1 < list.Count && list[pos - 1] is IRightExpression 
				&& pos + 1 < list.Count && list[pos + 1] is IRightExpression) {// x*y
				var item1 = list[pos - 1].Cast<IRightExpression>();
				var item2 = list[pos + 1].Cast<IRightExpression>();
				list.RemoveRange(pos - 1, 3);
				list.Insert(pos - 1, new MulExpression(item1, item2));
				pos = pos - 1;
			}
		}

		pos = -1;
		while (pos + 1 < list.Count && (pos = list.FindIndex(pos + 1, e => e is DivMarker)) != -1) {
			if (pos - 1 >= 0 && pos - 1 < list.Count && list[pos - 1] is IRightExpression 
				&& pos + 1 < list.Count && list[pos + 1] is IRightExpression) {// x/y
				var item1 = list[pos - 1].Cast<IRightExpression>();
				var item2 = list[pos + 1].Cast<IRightExpression>();
				list.RemoveRange(pos - 1, 3);
				list.Insert(pos - 1, new DivExpression(item1, item2));
				pos = pos - 1;
			}
		}

		pos = -1;
		while (pos + 1 < list.Count && (pos = list.FindIndex(pos + 1, e => e is PlusMarker)) != -1) {
			if (pos - 1 >= 0 && pos - 1 < list.Count && list[pos - 1] is IRightExpression 
				&& pos + 1 < list.Count && list[pos + 1] is IRightExpression) {// x+y
				var item1 = list[pos - 1].Cast<IRightExpression>();
				var item2 = list[pos + 1].Cast<IRightExpression>();
				list.RemoveRange(pos - 1, 3);
				list.Insert(pos - 1, new PlusExpression(item1, item2));
				pos = pos - 1;
			} 
		}

		pos = -1;
		while (pos + 1 < list.Count && (pos = list.FindIndex(pos + 1, e => e is SubMarker)) != -1) {
			if (pos - 1 >= 0 && pos - 1 < list.Count && list[pos - 1] is IRightExpression 
				&& pos + 1 < list.Count && list[pos + 1] is IRightExpression) {// x-y
				var item1 = list[pos - 1].Cast<IRightExpression>();
				var item2 = list[pos + 1].Cast<IRightExpression>();
				list.RemoveRange(pos - 1, 3);
				list.Insert(pos - 1, new SubExpression(item1, item2));
				pos = pos - 1;
			}
		}
	}

	private void ParseLv3(List<IExpression> list) {
		var pos = -1;
		while (pos + 1 < list.Count && (pos = list.FindIndex(pos + 1, e => e is ExMarker)) != -1) {
			if (pos - 1 >= 0 && pos - 1 < list.Count && list[pos - 1] is IRightExpression 
			    && pos + 1 < list.Count && list[pos + 1] is EqMarker
				&& pos + 2 < list.Count && list[pos + 2] is IRightExpression) {// x!=y
				var item1 = list[pos - 1].Cast<IRightExpression>();
				var item2 = list[pos + 2].Cast<IRightExpression>();
				list.RemoveRange(pos - 1, 4);
				list.Insert(pos - 1, new NEQExpression(item1, item2));
				pos = pos - 1;
			} else if (pos + 1 < list.Count && list[pos + 1] is SSExpression) { // !(x)
				var item = list[pos + 1].Cast<SSExpression>();
				list.RemoveRange(pos, 2);
				list.Insert(pos, new NotExpression((ILogicExpression)item.Unbound()));
			} else if (pos + 1 < list.Count && list[pos + 1] is IRightExpression) { // !x
				var item = list[pos + 1].Cast<ILogicExpression>();
				list.RemoveRange(pos, 2);
				list.Insert(pos, new NotExpression(item));
			}
		}

		pos = -1;
		while (pos + 1 < list.Count && (pos = list.FindIndex(pos + 1, e => e is GtMarker)) != -1) {
			if (pos - 1 >= 0 && pos - 1 < list.Count && list[pos - 1] is IRightExpression
				&& pos + 1 < list.Count && list[pos + 1] is IRightExpression) {// x>y
				var item1 = list[pos - 1].Cast<IRightExpression>();
				var item2 = list[pos + 1].Cast<IRightExpression>();
				list.RemoveRange(pos - 1, 3);
				list.Insert(pos - 1, new GTExpression(item1, item2));
				pos = pos - 1;
			} else if (pos - 1 >= 0 && pos - 1 < list.Count && list[pos - 1] is IRightExpression
			    && pos + 1 < list.Count && list[pos + 1] is EqMarker
				&& pos + 2 < list.Count && list[pos + 2] is IRightExpression) {// x>=y
				var item1 = list[pos - 1].Cast<IRightExpression>();
				var item2 = list[pos + 2].Cast<IRightExpression>();
				list.RemoveRange(pos - 1, 4);
				list.Insert(pos - 1, new GEExpression(item1, item2));
				pos = pos - 1;
			} 
		}

		pos = -1;
		while (pos + 1 < list.Count && (pos = list.FindIndex(pos + 1, e => e is LtMarker)) != -1) {
			if (pos - 1 >= 0 && pos - 1 < list.Count && list[pos - 1] is IRightExpression
			    && pos + 1 < list.Count && list[pos + 1] is IRightExpression) {// x<y
				var item1 = list[pos - 1].Cast<IRightExpression>();
				var item2 = list[pos + 1].Cast<IRightExpression>();
				list.RemoveRange(pos - 1, 3);
				list.Insert(pos - 1, new LTExpression(item1, item2));
				pos = pos - 1;
			} else if (pos - 1 >= 0 && pos - 1 < list.Count && list[pos - 1] is IRightExpression
			    && pos + 1 < list.Count && list[pos + 1] is EqMarker
			    && pos + 2 < list.Count && list[pos + 2] is IRightExpression) {// x<=y
				var item1 = list[pos - 1].Cast<IRightExpression>();
				var item2 = list[pos + 2].Cast<IRightExpression>();
				list.RemoveRange(pos - 1, 4);
				list.Insert(pos - 1, new LEExpression(item1, item2));
				pos = pos - 1;
			} 
		}

		pos = -1;
		while (pos + 1 < list.Count && (pos = list.FindIndex(pos + 1, e => e is EqMarker)) != -1) {
			if (pos - 1 >= 0 && pos - 1 < list.Count && list[pos - 1] is IRightExpression 
			    && pos + 1 < list.Count && list[pos + 1] is EqMarker
			    && pos + 2 < list.Count && list[pos + 2] is IRightExpression) {// x==y
				var item1 = list[pos - 1].Cast<IRightExpression>();
				var item2 = list[pos + 2].Cast<IRightExpression>();
				list.RemoveRange(pos - 1, 4);
				list.Insert(pos - 1, new EQExpression(item1, item2));
				pos = pos - 1;
			}
		}
	}

	private void ParseLv4(List<IExpression> list) {
		var pos = -1;
		while (pos + 1 < list.Count && (pos = list.FindIndex(pos + 1, e => e is MMExpression)) != -1) {//[a,b]
			var item = (MMExpression)list[pos];
			Parse(item.Values);
			list.RemoveRange(pos, 1);
			var result = new List<IRightExpression>();
			Parse(item.Values, result);
			list.Insert(pos, new ArrayExpression(result));
		}

		pos = -1;
		while (pos + 1 < list.Count && (pos = list.FindIndex(pos + 1, e => e is BBExpression)) != -1) {//{a:1,b:2}
			var item = (BBExpression)list[pos];
			Parse(item.Values);
			list.RemoveRange(pos, 1);
			var result = new Dictionary<string, IRightExpression>();
			Parse(item.Values, result);
			list.Insert(pos, new TableExpression(result));
		}

		pos = -1;
		while (pos + 1 < list.Count && (pos = list.FindIndex(pos + 1, e => e is EqMarker)) != -1) {
			if (pos - 1 >= 0 && pos - 1 < list.Count && list[pos - 1] is ILeftExpression 
			    && pos + 1 < list.Count && list[pos + 1] is IRightExpression) {// x=y
				var item1 = list[pos - 1].Cast<ILeftExpression>();
				var item2 = list[pos + 1].Cast<IRightExpression>();
				list.RemoveRange(pos - 1, 3);
				list.Insert(pos - 1, new AssignExpression(item1, item2));
				pos = pos - 1;
			}
		}

		pos = -1;
		while (pos + 1 < list.Count && (pos = list.FindIndex(pos + 1, e => e is RetMarker)) != -1) {
			if (pos + 1 < list.Count && list[pos + 1] is IRightExpression) { // return x
				var item = (IRightExpression)list[pos + 1];
				list.RemoveRange(pos, 2);
				list.Insert(pos, new ReturnExpression(item));
			} else if (pos == list.Count - 1) {// return
				list.RemoveRange(pos, 1);
				list.Insert(pos, new ReturnExpression());
			} 
		}
	}

	private void ParseLv5(List<IExpression> list) {
		var pos = -1;
		while (pos + 1 < list.Count && (pos = list.FindIndex(pos + 1, e => e is EqMarker)) != -1) {
			if (pos - 1 >= 0 && pos - 1 < list.Count && list[pos - 1] is SSExpression
				&& pos + 1 < list.Count && list[pos + 1] is GtMarker
				&& pos + 2 < list.Count && list[pos + 2] is IRightExpression) { // () => xx
				var item1 = list[pos - 1].Cast<SSExpression>();
				var item2 = list[pos + 2].Cast<IRightExpression>();
				list.RemoveRange(pos - 1, 4);
				var args = new List<string>();
				Parse(item1.Values, args);
				list.Insert(pos - 1, new FuncExpression(args, new ReturnExpression(item2)));
				pos = pos - 1;
			} else if (pos - 1 >= 0 && pos - 1 < list.Count && list[pos - 1] is INameExpression
				&& pos + 1 < list.Count && list[pos + 1] is GtMarker
				&& pos + 2 < list.Count && list[pos + 2] is IRightExpression) { // a => xx
				var item1 = list[pos - 1].Cast<INameExpression>();
				var item2 = list[pos + 2].Cast<IRightExpression>();
				list.RemoveRange(pos - 1, 4);
				list.Insert(pos - 1, new FuncExpression(new[]{ item1.Name }, new ReturnExpression(item2)));
				pos = pos - 1;
			}
		}
	}

	private static void Parse(List<IExpression> list, List<string> result) {
		if (list.Count == 1) {
			if (list[0] is INameExpression) {
				result.Add(list[0].Cast<INameExpression>().Name);
				list.Clear();
			}
		} else if (list.Count > 1) {
			if (list[0] is INameExpression
				&& list[1] is CmaMarker) {
				result.Add(list[0].Cast<INameExpression>().Name);
				list.RemoveRange(0, 2);
				Parse(list, result);
			}
		} 
	}

	private static void Parse(List<IExpression> list, List<IRightExpression> result) {
		if (list.Count == 1) {
			if (list[0] is IRightExpression) {
				result.Add(list[0].Cast<IRightExpression>());
				list.Clear();
			}
		} else if (list.Count > 1) {
			if (list[0] is IRightExpression 
				&& list[1] is CmaMarker) {
				result.Add(list[0].Cast<IRightExpression>());
				list.RemoveRange(0, 2);
				Parse(list, result);
			}
		}
	}

	private static void Parse(List<IExpression> list, Dictionary<string, IRightExpression> result) {
		if (list.Count == 3) {
			if (list[0] is INameExpression 
				&& list[1] is ColMarker 
				&& list[2] is IRightExpression) {
				result.Add(list[0].Cast<INameExpression>().Name, list[2].Cast<IRightExpression>());
				list.Clear();
			}
		} else if (list.Count > 3) {
			if (list[0] is INameExpression 
				&& list[1] is ColMarker 
				&& list[2] is IRightExpression 
				&& list[3] is CmaMarker) {
				result.Add(list[0].Cast<INameExpression>().Name, list[2].Cast<IRightExpression>());
				list.RemoveRange(0, 4);
				Parse(list, result);
			}
		}
	}

	protected override void OnDispose() {
		base.OnDispose();
		_t2e.Clear();
	}

}

}