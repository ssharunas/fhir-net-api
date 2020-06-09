using Hl7.Fhir.Model;
using Hl7.Fhir.Serialization.Xml;

namespace Hl7.Fhir.Applicator.Navigation
{
	internal class ElementNavigator : FhirObjectNavigatorBase<Element>
	{
		public ElementNavigator(IFhirXmlNode parent, string name, Element element)
			: base(parent, element)
		{
			Name = name;
		}
	}
}
