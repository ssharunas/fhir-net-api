using Hl7.Fhir.Serialization.Xml;
using System.Collections.Generic;

namespace Hl7.Fhir.Applicator.Navigation
{
	internal class SimpleNavigator : NavigatorBase
	{
		private IList<IFhirXmlAttribute> _attrubutes;
		private object _instance;

		public SimpleNavigator(object instance, IFhirXmlNode parent, string elementName, object value = null)
			: base(parent, elementName, value)
		{
			_instance = instance;
		}

		public SimpleNavigator Add(string attributeName, object attributeValue)
		{
			if (attributeValue != null && !string.IsNullOrEmpty(attributeName))
			{
				if (_attrubutes == null)
					_attrubutes = new List<IFhirXmlAttribute>();

				_attrubutes.Add(new NavigatorAttribute(attributeName, attributeValue));
			}

			return this;
		}

		public override IList<IFhirXmlAttribute> Attributes() => _attrubutes;
		public override IList<IFhirXmlNode> Elements() => null;

		public override object Unwrap()
		{
			return _instance;
		}
	}
}
