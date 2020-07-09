using Hl7.Fhir.Applicator.XPath.Navigation;
using Hl7.Fhir.Serialization.Xml;
using System.Collections.Generic;

namespace Hl7.Fhir.Applicator.Xml
{
	internal static class TemplateNodeAutoIdGenerator
	{
		public interface IGenerator
		{
			IXPathNavigator Generate(string name, IList<FhirXmlReader.Attr> attributes);
		}

		public class DefaultGenerator : IGenerator
		{
			private FhirXmlReader.Attr? GetAttr(IList<FhirXmlReader.Attr> attributes, string name)
			{
				if (attributes?.Count > 0)
				{
					foreach (var attr in attributes)
					{
						if (attr.Key == name)
							return attr;
					}
				}

				return null;
			}

			public IXPathNavigator Generate(string name, IList<FhirXmlReader.Attr> attributes)
			{
				if (name == "extension")
				{
					var attr = GetAttr(attributes, "url");
					if (!string.IsNullOrEmpty(attr?.Value))
					{
						return XPath.XPath.Parse($"@{attr.Value.Key} = '{attr.Value.Value}' ");
					}
				}

				return null;
			}
		}

		private static IGenerator _generator;

		public static void SetGenerator(IGenerator generator)
		{
			_generator = generator;
		}

		public static IXPathNavigator Generate(string name, IList<FhirXmlReader.Attr> attributes)
		{
			if (_generator is null)
				SetGenerator(new DefaultGenerator());

			return _generator.Generate(name, attributes);
		}

	}
}
