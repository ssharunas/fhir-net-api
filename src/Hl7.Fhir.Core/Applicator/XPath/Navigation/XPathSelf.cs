using Hl7.Fhir.Serialization.Xml;
using System.Collections.Generic;

namespace Hl7.Fhir.Applicator.XPath.Navigation
{
	internal class XPathSelf : XpathNavigatorNode
	{
		public override object Value(IFhirXmlValue node)
		{
			if (node is IFhirXmlNode self)
				return Next != null ? Next.Value(self) : self;

			return null;
		}

		public override List<object> Values(IFhirXmlValue node)
		{
			if (node is IFhirXmlNode self)
				return Next != null ? Next.Values(self) : new List<object> { self };

			return null;
		}

		public override string ToString()
		{
			if (Next != null)
				return "./" + Next.ToString();

			return ".";
		}

	}
}
