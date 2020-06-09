using Hl7.Fhir.Serialization.Xml;
using System;
using System.Collections.Generic;

namespace Hl7.Fhir.Applicator.XPath.Navigation
{
	internal class XPathAttribute : XpathNavigatorNode
	{
		public XPathAttribute(string name)
		{
			Name = name;
		}

		private string Name { get; set; }

		public override object RawValue(IFhirXmlValue xml)
		{
			if (xml is IFhirXmlNode node)
				return node.Attribute(Name)?.Value;

			throw new InvalidOperationException($"Could not execure XPath attribute selector on non-element node of type '{xml?.GetType()}'.");
		}

		public override object Value(IFhirXmlValue xml)
		{
			if (xml is IFhirXmlNode node)
				return node.Attribute(Name)?.ValueAsString;

			throw new InvalidOperationException($"Could not execure XPath attribute selector on non-element node of type '{xml?.GetType()}'.");
		}

		public override List<object> Values(IFhirXmlValue xml)
		{
			if (xml is IFhirXmlNode node)
			{
				List<object> result = null;
				var attr = node.Attribute(Name);

				if (attr != null)
				{
					if (Next != null)
						result = Next.Values(attr);
					else if (!string.IsNullOrEmpty(attr.ValueAsString))
						result = new List<object> { attr.ValueAsString };
				}

				return result;
			}

			throw new InvalidOperationException($"Could not execure XPath attribute selector on non-element node of type '{xml?.GetType()}'.");
		}

		public override string ToString()
		{
			return "@" + Name;
		}

	}
}
