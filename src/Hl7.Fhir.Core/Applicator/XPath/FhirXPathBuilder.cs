using CodePlex.XPathParser;
using Hl7.Fhir.Applicator.XPath.Navigation;
using System;
using System.Collections.Generic;
using System.Xml.XPath;

namespace Hl7.Fhir.Applicator.XPath
{
	internal class FhirXPathBuilder : IXPathBuilder<IXPathNavigator>
	{
		public void StartBuild() { }
		public IXPathNavigator EndBuild(IXPathNavigator result) => result;

		public IXPathNavigator Number(string value) => new XPathValueNode(value, true);
		public IXPathNavigator String(string value) => new XPathValueNode(value, false);
		public IXPathNavigator Operator(XPathOperator op, IXPathNavigator left, IXPathNavigator right) => new XPathOperatorNode(op, left, right);

		public IXPathNavigator Axis(XPathAxis axis, XPathNodeType nodeType, string prefix, string name)
		{
			switch (axis)
			{
				case XPathAxis.Child:
					if (nodeType == XPathNodeType.Text)
						return new XPathNodeText();
					else
						return new XPathElement(name);
				case XPathAxis.Attribute:
					return new XPathAttribute(name);
				case XPathAxis.Root:
					return new XPathRoot();
				case XPathAxis.Parent:
					return new XPathParent();
				case XPathAxis.Self:
					return new XPathSelf();
			}

			throw new NotSupportedException($"Axis {axis} is not supported in Fhir XPaths");
		}

		public IXPathNavigator JoinStep(IXPathNavigator left, IXPathNavigator right)
		{
			((XpathNavigatorNode)left).Next = right;
			return left;
		}

		public IXPathNavigator Predicate(IXPathNavigator node, IXPathNavigator condition, bool reverseStep)
		{
			((XPathElement)node).Predicate = condition;
			return node;
		}

		public IXPathNavigator Function(string prefix, string name, IList<IXPathNavigator> args)
		{
			return new XPathFunction(name, args);
		}

		public IXPathNavigator Variable(string prefix, string name)
		{
			throw new NotSupportedException("Variables are not supported in Fhir XPaths");
		}
	}
}
