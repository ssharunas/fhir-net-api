using Hl7.Fhir.Serialization.Xml;
using Hl7.Fhir.Support;
using System;
using System.Collections.Generic;

namespace Hl7.Fhir.Applicator.XPath.Navigation
{
	internal class XPathSelfAndDescendants : XPathSelf
	{
		private class FakeNode : IFhirXmlNode
		{
			private IFhirXmlNode _node;
			public FakeNode(IFhirXmlNode node) => _node = node;
			public string Namespace => null;
			public IFhirXmlNode Parent => null;
			public string Name => null;
			public object Value => null;
			public string ValueAsString { get; set; }
			public Uri ValueAsUri => null;
			public int? ValueAsInt => null;
			public DateTimeOffset? ValueAsInstant => null;
			public IPositionInfo Position => null;
			public IFhirXmlAttribute AddAttribute(string key, string value) => null;
			public IFhirXmlNode AddElement(string name, string @namespace, string text) => null;
			public IFhirXmlAttribute Attribute(string name) => null;
			public IList<IFhirXmlAttribute> Attributes() => null;
			public bool DeleteElement(IFhirXmlNode child) => false;
			public IPathable GetNode(string path) => null;
			public IList<IPathable> GetNodes(string path) => null;
			public object GetValue(string path) => null;
			public IList<object> GetValues(string path) => null;
			public bool Is(string @namespace, string name) => false;
			public IFhirXmlNode Element(string @namespace, string name) => null;
			public IList<IFhirXmlNode> Elements() => Elements(null, null);
			public IList<IFhirXmlNode> Elements(string name) => Elements(null, name);

			public IList<IFhirXmlNode> Elements(string @namespace, string name)
			{
				if (!string.IsNullOrEmpty(@namespace) && @namespace != _node.Namespace ||
					!string.IsNullOrEmpty(name) && name != _node.Name)
				{
					return new IFhirXmlNode[0];
				}

				return new IFhirXmlNode[] { _node };
			}

			public string ToXml() => _node.ToXml();
		}

		private void CollectAllValues(ref List<object> result, IFhirXmlNode node)
		{
			if (node != null && Next != null)
			{
				var values = Next.Values(node);

				if (values?.Count > 0)
					result.AddRange(values);

				foreach (var child in node.Elements())
					CollectAllValues(ref result, child);
			}
		}

		public override List<object> Values(IFhirXmlValue node)
		{
			var result = new List<object>();

			if (node is IFhirXmlNode self)
			{
				var parent = self.Parent ?? new FakeNode(self);
				CollectAllValues(ref result, parent);
			}

			if (result.Count > 0)
				return result;

			return null;
		}

		public override string ToString()
		{
			return "//" + Next?.ToString();
		}
	}
}
