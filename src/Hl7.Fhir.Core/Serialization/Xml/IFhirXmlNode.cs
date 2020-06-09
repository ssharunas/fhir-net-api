using Hl7.Fhir.Applicator;
using Hl7.Fhir.Support;
using System.Collections.Generic;

namespace Hl7.Fhir.Serialization.Xml
{
	internal interface IFhirXmlNode : IFhirXmlValue, IPathable
	{
		string Namespace { get; }
		IFhirXmlNode Parent { get; }

		/// <returns>Not null</returns>
		IList<IFhirXmlNode> Elements();
		/// <returns>Not null</returns>
		IList<IFhirXmlNode> Elements(string @namespace, string name);
		/// <returns>Not null</returns>
		IList<IFhirXmlNode> Elements(string name);
		IFhirXmlNode Element(string @namespace, string name);

		/// <returns>Not null</returns>
		IList<IFhirXmlAttribute> Attributes();
		IFhirXmlAttribute Attribute(string name);

		IFhirXmlNode AddElement(string name, string @namespace, string text);
		bool DeleteElement(IFhirXmlNode child);
		//bool DeleteAttribute(IFhirXmlAttribute attribute);
		IFhirXmlAttribute AddAttribute(string key, string value);

		IPositionInfo Position { get; }
		bool Is(string @namespace, string name);

		string ToXml();
	}
}
