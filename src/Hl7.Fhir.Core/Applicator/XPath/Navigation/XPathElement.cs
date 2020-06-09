using Hl7.Fhir.Serialization.Xml;
using System;
using System.Collections.Generic;

namespace Hl7.Fhir.Applicator.XPath.Navigation
{
	internal class XPathElement : XpathNavigatorNode
	{
		private IXPathNavigator _predicate;

		public XPathElement(string name)
		{
			Name = name;
		}

		private string Name { get; set; }

		public IXPathNavigator Predicate
		{
			get => _predicate;
			set
			{
				if (_predicate != null) //Should never happen
					throw new InvalidOperationException("Predicate was already assigned!");

				_predicate = value;
			}
		}

		public override object Value(IFhirXmlValue xml)
		{
			if (xml is IFhirXmlNode node)
			{
				foreach (var child in node.Elements(Name))
				{
					if (Predicate != null && !Predicate.IsMatch(child))
						continue;

					object value = Next != null ? Next.Value(child) : child;

					if (value != null) //TODO: lists
						return value;
				}

				return null;
			}

			throw new InvalidOperationException($"Could not execure XPath element selector on non-element node of type '{xml?.GetType()}'.");
		}

		public override List<object> Values(IFhirXmlValue xml)
		{
			if (xml is IFhirXmlNode node)
			{
				List<object> result = null;

				foreach (var child in node.Elements(Name))
				{
					if (Predicate != null && !Predicate.IsMatch(child))
						continue;

					List<object> values = null;

					if (Next != null)
						values = Next.Values(child);
					else
						values = new List<object> { child };

					if (values != null)
					{
						if (result == null)
							result = values;
						else
							result.AddRange(values);
					}
				}

				return result;
			}

			throw new InvalidOperationException($"Could not execure XPath element selector on non-element node of type '{xml?.GetType()}'.");
		}

		public override string ToString()
		{
			string result = Name;

			if (Predicate != null)
				result += "[" + Predicate.ToString() + "]";

			if (Next != null)
				result += "/" + Next.ToString();

			return result;
		}
	}
}
