using Hl7.Fhir.Serialization.Xml;
using System.Collections.Generic;

namespace Hl7.Fhir.Applicator.XPath.Navigation
{
	internal class XPathRoot : XpathNavigatorNode
	{
		public override object Value(IFhirXmlValue node)
		{
			if (node is IFhirXmlNode root)
			{
				while (root.Parent != null)
					root = root.Parent;

				return Next != null ? Next.Value(root) : root;
			}

			return null;
		}

		public override List<object> Values(IFhirXmlValue node)
		{
			if (node is IFhirXmlNode root)
			{
				while (root.Parent != null)
					root = root.Parent;

				return Next != null ? Next.Values(root) : new List<object> { root };
			}

			return null;
		}

		public override string ToString()
		{
			if (Next != null)
				return "/" + Next.ToString();

			return "/";
		}

	}
}
