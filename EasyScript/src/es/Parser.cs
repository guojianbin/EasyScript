using System.Collections.Generic;
using Easily.Bases;

namespace Easily.ES {

	/// <summary>
	/// @author Easily
	/// </summary>
	internal enum ParseLevel {

		EXPRESSIONS = 1,
		FOR_ARGS,
		STRING_ARGS,
		ARRAY_ARGS,
		TABLE_ARGS

	}

	/// <summary>
	/// @author Easily
	/// </summary>
	internal class Parser : Disposable {

		private readonly Dictionary<ParseLevel, List<ISyntax[]>> _parsers = new Dictionary<ParseLevel, List<ISyntax[]>>();

		internal Parser() {
			_parsers.Add(ParseLevel.FOR_ARGS, new List<ISyntax[]> { new ISyntax[] { new SyntaxForArgs() } });
			_parsers.Add(ParseLevel.STRING_ARGS, new List<ISyntax[]> { new ISyntax[] { new SyntaxStringArgs() } });
			_parsers.Add(ParseLevel.ARRAY_ARGS, new List<ISyntax[]> { new ISyntax[] { new SyntaxArrayArgs() } });
			_parsers.Add(ParseLevel.TABLE_ARGS, new List<ISyntax[]> { new ISyntax[] { new SyntaxTableArgs() } });
			_parsers.Add(ParseLevel.EXPRESSIONS, new List<ISyntax[]> {
				new ISyntax[] { new SyntaxClass(), new SyntaxWhile(), new SyntaxFor(), new SyntaxForEach() },
				new ISyntax[] { new SyntaxIfElseIf(), new SyntaxIfElseIf2(), new SyntaxIfElse(), new SyntaxIf(), new SyntaxFunc() },
				new ISyntax[] { new SyntaxNew() },
				new ISyntax[] { new SyntaxMethod(), new SyntaxProperty(), new SyntaxInvoke(), new SyntaxIndex() },
				new ISyntax[] { new SyntaxParens() },
				new ISyntax[] { new SyntaxNegate() },
				new ISyntax[] { new SyntaxMultiply(), new SyntaxDivision() },
				new ISyntax[] { new SyntaxAdd(), new SyntaxSubtract() },
				new ISyntax[] { new SyntaxNot(), new SyntaxNEQ(), new SyntaxEQ(), new SyntaxGT(), new SyntaxGE(), new SyntaxLT(), new SyntaxLE() },
				new ISyntax[] { new SyntaxArray(), new SyntaxTable() },
				new ISyntax[] { new SyntaxLambda() },
				new ISyntax[] { new SyntaxBind() },
				new ISyntax[] { new SyntaxReturn() }
			});
		}

		internal Module Parse(IEnumerable<Token> tokens) {
			var list = ESUtility.Parse(tokens);
			Parse(list);
			return new Module(new ExpressionModule(list));
		}

		internal void Parse(List<IExpression> list) {
			Parse(ParseLevel.EXPRESSIONS, list);
		}

		internal void Parse(ParseLevel level, List<IExpression> list) {
			Parse(_parsers[level], list);
		}

		private void Parse(List<ISyntax[]> syntaxs, List<IExpression> list) {
			for (var i = 0; i < syntaxs.Count; i++) {
				Parse(syntaxs[i], list);
			}
		}

		private void Parse(ISyntax[] syntaxs, List<IExpression> list) {
			for (var pos = 0; pos < list.Count; pos++) {
				foreach (var syntax in syntaxs) {
					if (syntax.IsMatch(list, pos)) {
						syntax.Parse(this, list, ref pos);
						break;
					}
				}
			}
		}

		protected override void OnDispose() {
			base.OnDispose();
			_parsers.Clear();
		}

	}

}