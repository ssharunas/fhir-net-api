using Hl7.Fhir.Applicator.Xml;
using System;

namespace Hl7.Fhir.Applicator
{
	public interface ITemplate
	{
		Uri GetLocation(ulong id);
		TemplateNodeValidationResult Validate(IDataContext context);
	}
}
