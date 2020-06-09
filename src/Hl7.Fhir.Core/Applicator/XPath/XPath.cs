using CodePlex.XPathParser;
using Hl7.Fhir.Applicator.XPath.Navigation;
using Hl7.Fhir.Support;

namespace Hl7.Fhir.Applicator.XPath
{
	internal static class XPath
	{
		private static readonly IXPathBuilder<IXPathNavigator> _builder = new FhirXPathBuilder();
		private static readonly XPathParser<IXPathNavigator> _parser = new XPathParser<IXPathNavigator>();

		public static IXPathNavigator Parse(string xpath, IPositionInfo pos = null)
		{
			if (string.IsNullOrEmpty(xpath))
				return null;

			try
			{
				lock (_parser)
					return _parser.Parse(xpath, _builder);
			}
			catch (XPathParserException ex)
			{
				throw Error.Format($"Failed to parse XPath '{xpath}', because: {ex.Message}", pos, ex);
			}
		}

	}
}
