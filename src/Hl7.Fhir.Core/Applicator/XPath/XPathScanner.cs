using System.Diagnostics;
using System.Text.RegularExpressions;

namespace CodePlex.XPathParser
{
	// Extends XPathOperator enumeration
	internal enum LexKind
	{
		Unknown,        // Unknown lexeme
		Or,             // Operator 'or'
		And,            // Operator 'and'
		Eq,             // Operator '='
		Ne,             // Operator '!='
		Lt,             // Operator '<'
		Le,             // Operator '<='
		Gt,             // Operator '>'
		Ge,             // Operator '>='
		Plus,           // Operator '+'
		Minus,          // Operator '-'
		Multiply,       // Operator '*'
		Divide,         // Operator 'div'
		Modulo,         // Operator 'mod'
		UnaryMinus,     // Not used
		Union,          // Operator '|'
		LastOperator = Union,

		DotDot,         // '..'
		ColonColon,     // '::'
		SlashSlash,     // Operator '//'
		Number,         // Number (numeric literal)
		Axis,           // AxisName

		Name,           // NameTest, NodeType, FunctionName, AxisName, second part of VariableReference
		String,         // Literal (string literal)
		Eof,            // End of the expression

		FirstStringable = Name,
		LastNonChar = Eof,

		LParens = '(',
		RParens = ')',
		LBracket = '[',
		RBracket = ']',
		Dot = '.',
		At = '@',
		Comma = ',',

		Star = '*',      // NameTest
		Slash = '/',      // Operator '/'
		Dollar = '$',      // First part of VariableReference
		RBrace = '}',      // Used for AVTs
	};

	internal sealed class XPathScanner
	{
		private int _curIndex;
		private char _curChar;
		private string _name;
		private string _prefix;
		private string _stringValue;
		private bool _canBeFunction;
		private LexKind _prevKind;
		private XPathAxis _axis;

		public XPathScanner(string xpathExpr) : this(xpathExpr, 0) { }

		public XPathScanner(string xpathExpr, int startFrom)
		{
			Debug.Assert(xpathExpr != null);
			Source = xpathExpr;
			Kind = LexKind.Unknown;
			SetSourceIndex(startFrom);
			NextLex();
		}

		public string Source { get; }
		public LexKind Kind { get; private set; }
		public int LexStart { get; private set; }
		public int LexSize { get { return _curIndex - LexStart; } }
		public int PrevLexEnd { get; private set; }

		private void SetSourceIndex(int index)
		{
			Debug.Assert(0 <= index && index <= Source.Length);
			_curIndex = index - 1;
			NextChar();
		}

		private void NextChar()
		{
			Debug.Assert(-1 <= _curIndex && _curIndex < Source.Length);
			_curIndex++;
			if (_curIndex < Source.Length)
			{
				_curChar = Source[_curIndex];
			}
			else
			{
				Debug.Assert(_curIndex == Source.Length);
				_curChar = '\0';
			}
		}

		public string Name
		{
			get
			{
				Debug.Assert(Kind == LexKind.Name);
				Debug.Assert(_name != null);
				return _name;
			}
		}

		public string Prefix
		{
			get
			{
				Debug.Assert(Kind == LexKind.Name);
				Debug.Assert(_prefix != null);
				return _prefix;
			}
		}

		public string RawValue
		{
			get
			{
				if (Kind == LexKind.Eof)
				{
					return LexKindToString(Kind);
				}
				else
				{
					return Source.Substring(LexStart, _curIndex - LexStart);
				}
			}
		}

		public string StringValue
		{
			get
			{
				Debug.Assert(Kind == LexKind.String);
				Debug.Assert(_stringValue != null);
				return _stringValue;
			}
		}

		// Returns true if the character following an QName (possibly after intervening
		// ExprWhitespace) is '('. In this case the token must be recognized as a NodeType
		// or a FunctionName unless it is an OperatorName. This distinction cannot be done
		// without knowing the previous lexeme. For example, "or" in "... or (1 != 0)" may
		// be an OperatorName or a FunctionName.
		public bool CanBeFunction
		{
			get
			{
				Debug.Assert(Kind == LexKind.Name);
				return _canBeFunction;
			}
		}

		public XPathAxis Axis
		{
			get
			{
				Debug.Assert(Kind == LexKind.Axis);
				Debug.Assert(_axis != XPathAxis.Unknown);
				return _axis;
			}
		}

		private void SkipSpace()
		{
			while (IsWhiteSpace(_curChar))
			{
				NextChar();
			}
		}

		private static bool IsAsciiDigit(char ch)
		{
			return (uint)(ch - '0') <= 9;
		}

		public static bool IsWhiteSpace(char ch)
		{
			return ch <= ' ' && (ch == ' ' || ch == '\t' || ch == '\n' || ch == '\r');
		}

		public void NextLex()
		{
			PrevLexEnd = _curIndex;
			_prevKind = Kind;
			SkipSpace();
			LexStart = _curIndex;

			switch (_curChar)
			{
				case '\0':
					Kind = LexKind.Eof;
					return;
				case '(':
				case ')':
				case '[':
				case ']':
				case '@':
				case ',':
				case '$':
				case '}':
					Kind = (LexKind)_curChar;
					NextChar();
					break;
				case '.':
					NextChar();
					if (_curChar == '.')
					{
						Kind = LexKind.DotDot;
						NextChar();
					}
					else if (IsAsciiDigit(_curChar))
					{
						SetSourceIndex(LexStart);
						goto case '0';
					}
					else
					{
						Kind = LexKind.Dot;
					}
					break;
				case ':':
					NextChar();
					if (_curChar == ':')
					{
						Kind = LexKind.ColonColon;
						NextChar();
					}
					else
					{
						Kind = LexKind.Unknown;
					}
					break;
				case '*':
					Kind = LexKind.Star;
					NextChar();
					CheckOperator(true);
					break;
				case '/':
					NextChar();
					if (_curChar == '/')
					{
						Kind = LexKind.SlashSlash;
						NextChar();
					}
					else
					{
						Kind = LexKind.Slash;
					}
					break;
				case '|':
					Kind = LexKind.Union;
					NextChar();
					break;
				case '+':
					Kind = LexKind.Plus;
					NextChar();
					break;
				case '-':
					Kind = LexKind.Minus;
					NextChar();
					break;
				case '=':
					Kind = LexKind.Eq;
					NextChar();
					break;
				case '!':
					NextChar();
					if (_curChar == '=')
					{
						Kind = LexKind.Ne;
						NextChar();
					}
					else
					{
						Kind = LexKind.Unknown;
					}
					break;
				case '<':
					NextChar();
					if (_curChar == '=')
					{
						Kind = LexKind.Le;
						NextChar();
					}
					else
					{
						Kind = LexKind.Lt;
					}
					break;
				case '>':
					NextChar();
					if (_curChar == '=')
					{
						Kind = LexKind.Ge;
						NextChar();
					}
					else
					{
						Kind = LexKind.Gt;
					}
					break;
				case '"':
				case '\'':
					Kind = LexKind.String;
					ScanString();
					break;
				case '0':
				case '1':
				case '2':
				case '3':
				case '4':
				case '5':
				case '6':
				case '7':
				case '8':
				case '9':
					Kind = LexKind.Number;
					ScanNumber();
					break;
				default:
					this._name = ScanNCName();
					if (this._name != null)
					{
						Kind = LexKind.Name;
						this._prefix = string.Empty;
						this._canBeFunction = false;
						this._axis = XPathAxis.Unknown;
						bool colonColon = false;
						int saveSourceIndex = _curIndex;

						// "foo:bar" or "foo:*" -- one lexeme (no spaces allowed)
						// "foo::" or "foo ::"  -- two lexemes, reported as one (AxisName)
						// "foo:?" or "foo :?"  -- lexeme "foo" reported
						if (_curChar == ':')
						{
							NextChar();
							if (_curChar == ':')
							{   // "foo::" -> OperatorName, AxisName
								NextChar();
								colonColon = true;
								SetSourceIndex(saveSourceIndex);
							}
							else
							{                // "foo:bar", "foo:*" or "foo:?"
								string ncName = ScanNCName();
								if (ncName != null)
								{
									this._prefix = this._name;
									this._name = ncName;
									// Look ahead for '(' to determine whether QName can be a FunctionName
									saveSourceIndex = _curIndex;
									SkipSpace();
									this._canBeFunction = (_curChar == '(');
									SetSourceIndex(saveSourceIndex);
								}
								else if (_curChar == '*')
								{
									NextChar();
									this._prefix = this._name;
									this._name = "*";
								}
								else
								{            // "foo:?" -> OperatorName, NameTest
											 // Return "foo" and leave ":" to be reported later as an unknown lexeme
									SetSourceIndex(saveSourceIndex);
								}
							}
						}
						else
						{
							SkipSpace();
							if (_curChar == ':')
							{   // "foo ::" or "foo :?"
								NextChar();
								if (_curChar == ':')
								{
									NextChar();
									colonColon = true;
								}
								SetSourceIndex(saveSourceIndex);
							}
							else
							{
								this._canBeFunction = (_curChar == '(');
							}
						}
						if (!CheckOperator(false) && colonColon)
						{
							this._axis = CheckAxis();
						}
					}
					else
					{
						Kind = LexKind.Unknown;
						NextChar();
					}
					break;
			}
		}

		private bool CheckOperator(bool star)
		{
			LexKind opKind;

			if (star)
			{
				opKind = LexKind.Multiply;
			}
			else
			{
				if (_prefix.Length != 0 || _name.Length > 3)
					return false;

				switch (_name)
				{
					case "or": opKind = LexKind.Or; break;
					case "and": opKind = LexKind.And; break;
					case "div": opKind = LexKind.Divide; break;
					case "mod": opKind = LexKind.Modulo; break;
					default: return false;
				}
			}

			// If there is a preceding token and the preceding token is not one of '@', '::', '(', '[', ',' or an Operator,
			// then a '*' must be recognized as a MultiplyOperator and an NCName must be recognized as an OperatorName.
			if (_prevKind <= LexKind.LastOperator)
				return false;

			switch (_prevKind)
			{
				case LexKind.Slash:
				case LexKind.SlashSlash:
				case LexKind.At:
				case LexKind.ColonColon:
				case LexKind.LParens:
				case LexKind.LBracket:
				case LexKind.Comma:
				case LexKind.Dollar:
					return false;
			}

			this.Kind = opKind;
			return true;
		}

		private XPathAxis CheckAxis()
		{
			this.Kind = LexKind.Axis;
			switch (_name)
			{
				case "ancestor": return XPathAxis.Ancestor;
				case "ancestor-or-self": return XPathAxis.AncestorOrSelf;
				case "attribute": return XPathAxis.Attribute;
				case "child": return XPathAxis.Child;
				case "descendant": return XPathAxis.Descendant;
				case "descendant-or-self": return XPathAxis.DescendantOrSelf;
				case "following": return XPathAxis.Following;
				case "following-sibling": return XPathAxis.FollowingSibling;
				case "namespace": return XPathAxis.Namespace;
				case "parent": return XPathAxis.Parent;
				case "preceding": return XPathAxis.Preceding;
				case "preceding-sibling": return XPathAxis.PrecedingSibling;
				case "self": return XPathAxis.Self;
				default:
					this.Kind = LexKind.Name;
					return XPathAxis.Unknown;
			}
		}

		private void ScanNumber()
		{
			Debug.Assert(IsAsciiDigit(_curChar) || _curChar == '.');
			while (IsAsciiDigit(_curChar))
			{
				NextChar();
			}
			if (_curChar == '.')
			{
				NextChar();
				while (IsAsciiDigit(_curChar))
				{
					NextChar();
				}
			}
			if ((_curChar & (~0x20)) == 'E')
			{
				NextChar();
				if (_curChar == '+' || _curChar == '-')
				{
					NextChar();
				}
				while (IsAsciiDigit(_curChar))
				{
					NextChar();
				}
				throw ScientificNotationException();
			}
		}

		private void ScanString()
		{
			int startIdx = _curIndex + 1;
			int endIdx = Source.IndexOf(_curChar, startIdx);

			if (endIdx < 0)
			{
				SetSourceIndex(Source.Length);
				throw UnclosedStringException();
			}

			this._stringValue = Source.Substring(startIdx, endIdx - startIdx);
			SetSourceIndex(endIdx + 1);
		}

		static readonly Regex re = new Regex(@"\p{_xmlI}[\p{_xmlC}-[:]]*", RegexOptions.Compiled);

		private string ScanNCName()
		{
			Match m = re.Match(Source, _curIndex);
			if (m.Success)
			{
				_curIndex += m.Length - 1;
				NextChar();
				return m.Value;
			}
			return null;
		}

		public void PassToken(LexKind t)
		{
			CheckToken(t);
			NextLex();
		}

		public void CheckToken(LexKind t)
		{
			Debug.Assert(LexKind.FirstStringable <= t);
			if (Kind != t)
			{
				if (t == LexKind.Eof)
				{
					throw EofExpectedException(RawValue);
				}
				else
				{
					throw TokenExpectedException(LexKindToString(t), RawValue);
				}
			}
		}

		// May be called for the following tokens: Name, String, Eof, Comma, LParens, RParens, LBracket, RBracket, RBrace
		private string LexKindToString(LexKind t)
		{
			Debug.Assert(LexKind.FirstStringable <= t);

			if (LexKind.LastNonChar < t)
			{
				Debug.Assert("()[].@,*/$}".IndexOf((char)t) >= 0);
				return new string((char)t, 1);
			}

			switch (t)
			{
				case LexKind.Name: return "<name>";
				case LexKind.String: return "<string literal>";
				case LexKind.Eof: return "<eof>";
				default:
					Debug.Fail("Unexpected LexKind: " + t.ToString());
					return string.Empty;
			}
		}

		// XPath error messages
		// --------------------

		public XPathParserException UnexpectedTokenException(string token)
		{
			return new XPathParserException(Source, LexStart, _curIndex,
				string.Format("Unexpected token '{0}' in the expression.", token)
			);
		}
		public XPathParserException NodeTestExpectedException(string token)
		{
			return new XPathParserException(Source, LexStart, _curIndex,
				string.Format("Expected a node test, found '{0}'.", token)
			);
		}
		public XPathParserException PredicateAfterDotException()
		{
			return new XPathParserException(Source, LexStart, _curIndex,
				"Abbreviated step '.' cannot be followed by a predicate. Use the full form 'self::node()[predicate]' instead."
			);
		}
		public XPathParserException PredicateAfterDotDotException()
		{
			return new XPathParserException(Source, LexStart, _curIndex,
				"Abbreviated step '..' cannot be followed by a predicate. Use the full form 'parent::node()[predicate]' instead."
			);
		}
		public XPathParserException ScientificNotationException()
		{
			return new XPathParserException(Source, LexStart, _curIndex,
				"Scientific notation is not allowed."
			);
		}
		public XPathParserException UnclosedStringException()
		{
			return new XPathParserException(Source, LexStart, _curIndex,
				"String literal was not closed."
			);
		}
		public XPathParserException EofExpectedException(string token)
		{
			return new XPathParserException(Source, LexStart, _curIndex,
				string.Format("Expected end of the expression, found '{0}'.", token)
			);
		}
		public XPathParserException TokenExpectedException(string expectedToken, string actualToken)
		{
			return new XPathParserException(Source, LexStart, _curIndex,
				string.Format("Expected token '{0}', found '{1}'.", expectedToken, actualToken)
			);
		}
	}
}