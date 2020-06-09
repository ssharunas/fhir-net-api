using Hl7.Fhir.Serialization.Xml;

namespace Hl7.Fhir.Applicator.Xml
{
	internal partial class TemplateNode
	{
		private struct Attribute
		{
			private readonly FhirXmlReader.Attr _attr;

			public Attribute(FhirXmlReader.Attr attr)
			{
				_attr = attr;
				HasData = HasDataInString(_attr.Value);
			}

			public string Key => _attr.Key;
			public string Value => _attr.Value;
			public readonly bool HasData;
		}

		private class FhirXmlNodeWrapper : FhirXmlNode
		{
			public FhirXmlNodeWrapper() :base(null, null, null, null, null, null) { }

			public FhirXmlNode UnWrap()
			{
				if (Elements()?.Count > 0)
				{
					var element = Elements()[0] as FhirXmlNode;
					return element;
				}

				return null;
			}
		}
	}
}
