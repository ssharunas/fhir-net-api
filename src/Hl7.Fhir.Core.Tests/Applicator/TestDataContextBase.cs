using System.Xml;

namespace Hl7.Fhir.Applicator
{
	internal abstract class TestDataContextBase : IDataContext
	{
		public XmlReader GetInclude(string source)
		{
			return GetXmlReader(source);
		}

		public static XmlReader GetXmlReader(string xml)
		{
			return XmlReader.Create(typeof(TestDataContextBase).Assembly.GetManifestResourceStream(typeof(TestDataContextBase).Namespace + "." + xml));
		}
	}
}
