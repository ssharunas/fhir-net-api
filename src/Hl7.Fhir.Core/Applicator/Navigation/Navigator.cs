using Hl7.Fhir.Model;
using Hl7.Fhir.Serialization;
using Hl7.Fhir.Serialization.Xml;

namespace Hl7.Fhir.Applicator.Navigation
{
	public static class Navigator
	{
		public static IPathable ToPathable(Bundle bundle)
		{
			if (bundle == null)
				return new NullNavigator();

			return new BundleNavigator(bundle);
		}

		public static IPathable ToPathable(ResourceEntry entry)
		{
			if (entry == null)
				return new NullNavigator();

			return new ResourceEntryNavigator(null, entry);
		}

		public static IPathable ToPathable(Resource resource)
		{
			if (resource == null)
				return new NullNavigator();

			return new ResourceNavigator(null, resource);
		}

		public static IPathable ToPathable(Element resource)
		{
			if (resource == null)
				return new NullNavigator();

			string name = SerializationConfig.Inspector.ImportType(resource.GetType()).Name;
			return new ElementNavigator(null, name, resource);
		}

		public static IPathable ToPathable(string xml)
		{
			return FhirXmlNode.Create(FhirParser.XmlReaderFromString(xml));
		}
	}
}
