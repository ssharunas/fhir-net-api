using Hl7.Fhir.Model;
using Hl7.Fhir.Serialization.Xml;
using Hl7.Fhir.Support;
using System.Collections.Generic;

namespace Hl7.Fhir.Applicator.Navigation
{
	internal class ResourceNavigator : FhirObjectNavigatorBase<Resource>
	{
		public ResourceNavigator(IFhirXmlNode parent, Resource resource)
			: base(parent, resource)
		{
			Name = Mapping.Name;
		}

		public override IList<IFhirXmlAttribute> Attributes()
		{
			var result = base.Attributes();

			if (result is null)
				result = new List<IFhirXmlAttribute>();

			result.Add(new FhirXmlAttribute("xmlns", XmlNs.FHIR));

			return result;
		}
	}
}
