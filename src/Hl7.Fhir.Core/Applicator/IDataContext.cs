using System.Xml;

namespace Hl7.Fhir.Applicator
{
	public interface IDataContext
	{
		XmlReader GetInclude(string source);
	}
}
