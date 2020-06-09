using Hl7.Fhir.Serialization.Xml;
using System.Collections.Generic;

namespace Hl7.Fhir.Applicator.XPath.Navigation
{
	internal class XPathNodeText : XpathNavigatorNode
	{
		public override object Value(IFhirXmlValue node)
		{
			if (string.IsNullOrEmpty(node?.ValueAsString))
				return null;

			return node.ValueAsString;
		}

		public override List<object> Values(IFhirXmlValue node)
		{
			var value = Value(node);

			if (value != null)
				return new List<object> { value };

			return null;
		}
	}
}
