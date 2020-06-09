using Hl7.Fhir.Serialization.Xml;
using System.Collections.Generic;

namespace Hl7.Fhir.Applicator.XPath.Navigation
{
	internal class XPathParent : XpathNavigatorNode
	{
		public override object Value(IFhirXmlValue node)
		{
			if (node is IFhirXmlNode root && root.Parent != null)
				return Next != null ? Next.Value(root.Parent) : root.Parent;

			return null;
		}

		public override string ToString()
		{
			if (Next != null)
				return "../" + Next.ToString();

			return "..";
		}

		public override List<object> Values(IFhirXmlValue node)
		{
			if (node is IFhirXmlNode root && root.Parent != null)
				return Next != null ? Next.Values(root.Parent) : new List<object> { root.Parent };

			return null;
		}
	}
}
