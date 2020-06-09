using System;

namespace Hl7.Fhir.Serialization.Xml
{
	internal interface IFhirXmlValue
	{
		string Name { get; }

		object Value { get; }
		string ValueAsString { get; set; }
		Uri ValueAsUri { get; }
		int? ValueAsInt { get; }
		DateTimeOffset? ValueAsInstant { get; }
	}
}
