using CodePlex.XPathParser;
using Hl7.Fhir.Serialization.Xml;
using Hl7.Fhir.Support;
using System;
using System.Collections.Generic;

namespace Hl7.Fhir.Applicator.XPath.Navigation
{
	internal class XPathOperatorNode : IXPathNavigator
	{
		private XPathOperator _operator;
		private IXPathNavigator _left;
		private IXPathNavigator _right;

		public XPathOperatorNode(XPathOperator @operator, IXPathNavigator left, IXPathNavigator right)
		{
			_operator = @operator;
			_left = left;
			_right = right;
		}

		public bool IsMatch(IFhirXmlValue node)
		{
			return (bool)Value(node);
		}

		private bool AreEqual(object a, object b)
		{
			if (a == null && b == null)
				return true;

			if (a == null || b == null)
				return false;

			if (a.GetType() != b.GetType())
			{
				throw Error.InvalidOperation(
					$"Failed to navigate xpath: left and right operands must have the same type, but left side is {a.GetType()} and right is {b.GetType()}. Operator: {this}");
			}

			return Equals(a, b);
		}

		private bool IsLess(object a, object b)
		{
			if (a == null && b == null)
				return false;

			if (a == null)
				return true;

			if (b == null)
				return false;

			if (a.GetType() != b.GetType())
			{
				throw Error.InvalidOperation(
					$"Failed to navigate xpath: left and right operands must have the same type, but left side is {a.GetType()} and right is {b.GetType()}. Operator: {this}");
			}

			if (a is IComparable comparable)
				return comparable.CompareTo(b) < 0;

			return string.Compare(a.ToString(), b.ToString()) < 0;
		}

		public virtual object RawValue(IFhirXmlValue node) => Value(node);

		public object Value(IFhirXmlValue node)
		{
			switch (_operator)
			{
				case XPathOperator.And:
					return _left.IsMatch(node) && _right.IsMatch(node);

				case XPathOperator.Or:
					return _left.IsMatch(node) || _right.IsMatch(node);

				case XPathOperator.Ne:
					return !AreEqual(_left.Value(node), _right.Value(node));

				case XPathOperator.Eq:
					return AreEqual(_left.Value(node), _right.Value(node));

				case XPathOperator.Lt:
					return IsLess(_left.Value(node), _right.Value(node));

				case XPathOperator.Le:
					return AreEqual(_left.Value(node), _right.Value(node)) || IsLess(_left.Value(node), _right.Value(node));

				case XPathOperator.Gt:
					return !IsLess(_left.Value(node), _right.Value(node));

				case XPathOperator.Ge:
					return AreEqual(_left.Value(node), _right.Value(node)) || !IsLess(_left.Value(node), _right.Value(node));
			}

			throw new NotSupportedException($"Operator {_operator} is not supported!");
		}

		public List<object> Values(IFhirXmlValue node)
		{
			return new List<object> { Value(node) };
		}

		public override string ToString()
		{
			string @operator = "�";
			switch (_operator)
			{
				case XPathOperator.And:
					@operator = " and ";
					break;

				case XPathOperator.Or:
					@operator = " or ";
					break;

				case XPathOperator.Ne:
					@operator = " != ";
					break;

				case XPathOperator.Eq:
					@operator = " = ";
					break;

				case XPathOperator.Lt:
					@operator = " < ";
					break;

				case XPathOperator.Le:
					@operator = " <= ";
					break;

				case XPathOperator.Gt:
					@operator = " > ";
					break;

				case XPathOperator.Ge:
					@operator = " >= ";
					break;
			}

			return _left?.ToString() + @operator + _right?.ToString();
		}

	}
}
