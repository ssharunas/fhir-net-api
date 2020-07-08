using Hl7.Fhir.Serialization;
using Hl7.Fhir.Serialization.Xml;
using Hl7.Fhir.Support;
using System.Collections.Generic;

namespace Hl7.Fhir.Applicator
{
	public class TemplateResponseReader : IPathable
	{
		public TemplateResponseReader(string response, IDataGetterContext context)
		{
			Context = context;
			Response = FhirXmlNode.Create(FhirParser.XmlReaderFromString(response));
		}

		public IDataGetterContext Context { get; private set; }
		private IFhirXmlNode Response { get; set; }

		public IPathable GetNode(string path)
		{
			var result = GetValue(path);

			if (result != null)
				return (result as IPathable) ?? throw Error.InvalidOperation($"Value at path '{path}' is not 'IPathable', but '{result.GetType()}'.");

			return null;
		}

		public IList<IPathable> GetNodes(string path)
		{
			IList<IPathable> result = null;
			var items = GetValues(path);

			if (items?.Count > 0)
			{
				result = new List<IPathable>(items.Count);

				foreach (var item in items)
				{
					if (item != null)
						result.Add((item as IPathable) ?? throw Error.InvalidOperation($"Value at path '{path}' is not 'IPathable', but '{item.GetType()}'."));
				}
			}

			return result;
		}

		public object GetValue(string path)
		{
			return XPath.XPath.Parse(path)?.Value(Response);
		}

		public IList<object> GetValues(string path)
		{
			return XPath.XPath.Parse(path)?.Values(Response);
		}

		public string ToXml()
		{
			return Response?.ToXml();
		}
	}
}
