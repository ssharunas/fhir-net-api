using Hl7.Fhir.Model;
using Hl7.Fhir.Serialization;
using Hl7.Fhir.Serialization.Xml;
using System;
using System.Collections.Generic;

namespace Hl7.Fhir.Applicator.Navigation
{
	internal abstract class HardcodedNavigator<T> : NavigatorBase
	{
		protected class Creator
		{
			private readonly string _nodeName;

			public Creator(IFhirXmlNode obj, string nodeName)
			{
				Wrapper = obj;
				_nodeName = nodeName;
			}

			public IFhirXmlNode Wrapper { get; }

			public IList<IFhirXmlNode> CreateNodes(int? value)
			{
				if (value != null)
					return new[] { new SimpleNavigator(value, Wrapper, _nodeName, value.ToString()) };

				return null;
			}

			public IList<IFhirXmlNode> CreateNodes(DateTimeOffset? value)
			{
				if (value != null)
					return new[] { new SimpleNavigator(value, Wrapper, _nodeName, value.ToString()) };

				return null;
			}

			public IList<IFhirXmlNode> CreateNodes(Uri value)
			{
				if (SerializationUtil.HasUriValue(value))
					return new[] { new SimpleNavigator(value, Wrapper, _nodeName, value.ToString()) };

				return null;
			}

			public IList<IFhirXmlNode> CreateNodes(string value)
			{
				if (!string.IsNullOrEmpty(value))
					return new[] { new SimpleNavigator(value, Wrapper, _nodeName, value) };

				return null;
			}

			public IList<IFhirXmlNode> CreateNodes(UriLinkList value)
			{
				IList<IFhirXmlNode> result = null;

				if (value?.Count > 0)
				{
					result = new List<IFhirXmlNode>(value.Count);

					foreach (var item in value)
					{
						result.Add(new SimpleNavigator(item, Wrapper, BundleXmlParser.XATOM_LINK)
							.Add(BundleXmlParser.XATOM_LINK_HREF, item.Uri)
							.Add(BundleXmlParser.XATOM_LINK_REL, item.Rel));
					}
				}

				return result;
			}
		}

		public HardcodedNavigator(IFhirXmlNode parent, T node) : base(parent)
		{
			Node = node;
		}

		protected T Node { get; }

		protected abstract IDictionary<string, Func<Creator, T, IList<IFhirXmlNode>>> AvailableElements { get; }

		public override IList<IFhirXmlAttribute> Attributes() => null;

		public override IList<IFhirXmlNode> Elements(string name)
		{
			if (AvailableElements.ContainsKey(name))
				return GetElements(name, AvailableElements[name]);

			return new List<IFhirXmlNode>();
		}

		public override IList<IFhirXmlNode> Elements()
		{
			List<IFhirXmlNode> result = new List<IFhirXmlNode>();

			foreach (var element in AvailableElements)
			{
				var elements = GetElements(element.Key, element.Value);

				if (elements?.Count > 0)
					result.AddRange(elements);
			}

			return result;
		}

		private IList<IFhirXmlNode> GetElements(string name, Func<Creator, T, IList<IFhirXmlNode>> valueGetter)
		{
			List<IFhirXmlNode> result = new List<IFhirXmlNode>();

			var value = valueGetter(new Creator(this, name), Node);
			if (value?.Count > 0)
			{
				foreach (var item in value)
				{
					if (item != null)
						result.Add(item);
				}
			}

			return result;
		}

		public override object Unwrap()
		{
			return Node;
		}
	}
}
