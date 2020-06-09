using Hl7.Fhir.Serialization.Xml;
using System.Collections.Generic;

namespace Hl7.Fhir.Applicator.XPath.Navigation
{
	internal interface IXPathNavigator
	{
		bool IsMatch(IFhirXmlValue root);
		object RawValue(IFhirXmlValue node);
		object Value(IFhirXmlValue node);
		List<object> Values(IFhirXmlValue node);
	}
}
