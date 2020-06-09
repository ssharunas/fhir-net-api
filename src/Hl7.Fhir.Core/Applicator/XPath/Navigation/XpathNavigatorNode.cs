using Hl7.Fhir.Serialization.Xml;
using System;
using System.Collections.Generic;

namespace Hl7.Fhir.Applicator.XPath.Navigation
{
	internal abstract class XpathNavigatorNode : IXPathNavigator
	{
		private IXPathNavigator _next;

		public IXPathNavigator Next
		{
			get => _next;
			set
			{
				if (_next != null)
					throw new Exception("Kitas elementas jau buvo užsetintas!!!");

				_next = value;
			}
		}

		public bool IsMatch(IFhirXmlValue node)
		{
			return Value(node) != null;
		}

		public virtual object RawValue(IFhirXmlValue node) => Value(node);

		public abstract object Value(IFhirXmlValue node);

		public abstract List<object> Values(IFhirXmlValue node);
	}
}
