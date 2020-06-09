using Hl7.Fhir.Serialization.Xml;
using System.Collections.Generic;
using System.Globalization;

namespace Hl7.Fhir.Applicator.XPath.Navigation
{
	internal class XPathValueNode : IXPathNavigator
	{
		private bool _isNumber;
		private decimal _number;

		public XPathValueNode(string value, bool isNumber)
		{
			Data = value;
			_isNumber = isNumber;
			if (isNumber)
				_number = decimal.Parse(value, NumberStyles.Any, CultureInfo.InvariantCulture);
		}

		public string Data { get; }

		public bool IsMatch(IFhirXmlValue node)
		{
			return node.ValueAsString == Data;
		}

		public object RawValue(IFhirXmlValue node)
		{
			if (_isNumber)
				return _number;

			return Data;
		}

		public object Value(IFhirXmlValue node)
		{
			return Data;
		}

		public List<object> Values(IFhirXmlValue node)
		{
			if (!string.IsNullOrEmpty(Data))
				return new List<object> { Value(node) };

			return null;
		}

		public override string ToString()
		{
			if (_isNumber)
				return Data;
			return '\'' + Data + '\'';
		}

	}
}
