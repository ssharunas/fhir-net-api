namespace Hl7.Fhir.Serialization.Xml
{
	internal class FhirXmlAttribute : FhirXmlValue, IFhirXmlAttribute
	{
		public FhirXmlAttribute(string name, string value)
		{
			Name = name;
			ValueAsString = value;
		}

		public override string ValueAsString { get; set; }
	}
}
