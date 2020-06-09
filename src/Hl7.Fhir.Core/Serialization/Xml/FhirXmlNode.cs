using Hl7.Fhir.Applicator;
using Hl7.Fhir.Applicator.XPath;
using Hl7.Fhir.Support;
using System.Collections.Generic;
using System.Security;
using System.Text;
using System.Xml;

namespace Hl7.Fhir.Serialization.Xml
{
	internal class FhirXmlNode : FhirXmlValue, IFhirXmlNode
	{
		private IList<IFhirXmlAttribute> _attributes;
		private IList<IFhirXmlNode> _elements;

		protected FhirXmlNode(string name, string @namespace, string text, IPositionInfo pos, List<IFhirXmlNode> children, List<FhirXmlReader.Attr> attributes)
		{
			Name = name;
			Namespace = @namespace;
			ValueAsString = text;
			Position = pos;
			_elements = children;

			if (_elements?.Count > 0)
			{
				foreach (var element in _elements)
					((FhirXmlNode)element).Parent = this;
			}

			if (attributes?.Count > 0)
			{
				_attributes = new List<IFhirXmlAttribute>(attributes.Count);

				foreach (var attribute in attributes)
					_attributes.Add(new FhirXmlAttribute(attribute.Key, attribute.Value));
			}
		}

		public override string ValueAsString { get; set; }
		public virtual string Namespace { get; private set; }
		public virtual IPositionInfo Position { get; private set; }
		public IFhirXmlNode Parent { get; private set; }

		public virtual IList<IFhirXmlAttribute> Attributes()
		{
			return _attributes ?? (_attributes = new List<IFhirXmlAttribute>());
		}

		public IFhirXmlAttribute Attribute(string name)
		{
			foreach (var attribute in Attributes())
			{
				if (attribute.Name == name)
					return attribute;
			}

			return null;
		}

		public virtual IList<IFhirXmlNode> Elements()
		{
			return _elements ?? (_elements = new List<IFhirXmlNode>());
		}

		public IList<IFhirXmlNode> Elements(string @namespace, string name)
		{
			List<IFhirXmlNode> result = new List<IFhirXmlNode>();

			foreach (var child in Elements())
			{
				if (child.Name == name && child.Namespace == @namespace)
					result.Add(child);
			}

			return result;
		}

		public IList<IFhirXmlNode> Elements(string name)
		{
			List<IFhirXmlNode> result = new List<IFhirXmlNode>();

			foreach (var child in Elements())
			{
				if (child.Name == name)
					result.Add(child);
			}

			return result;
		}

		public IFhirXmlNode Element(string @namespace, string name)
		{
			foreach (var child in Elements())
			{
				if (child.Name == name && child.Namespace == @namespace)
					return child;
			}

			return null;
		}

		public bool Is(string @namespace, string name)
		{
			return name == Name && @namespace == Namespace;
		}

		public string ToXml()
		{
			return ToXml(this);
		}

		internal static string ToXml(IFhirXmlNode node)
		{
			if (node == null)
				return null;

			StringBuilder result = new StringBuilder("<").Append(node.Name);

			if (!string.IsNullOrEmpty(node.Namespace) && (node.Parent == null || node.Parent.Namespace != node.Namespace))
			{
				result.Append($" xmlns=\"{node.Namespace}\"");
			}

			var attributes = node.Attributes();
			if (attributes?.Count > 0)
			{
				foreach (var attribute in attributes)
				{
					result
						.Append(" ")
						.Append(XmlConvert.EncodeLocalName(attribute.Name))
						.Append("=\"")
						.Append(SecurityElement.Escape(attribute.ValueAsString))
						.Append('"');
				}
			}

			result.Append(">");

			if (!string.IsNullOrEmpty(node.ValueAsString))
				result.Append(SecurityElement.Escape(node.ValueAsString));

			var elements = node.Elements();
			if (elements?.Count > 0)
			{
				result.AppendLine();
				foreach (var child in elements)
					result.Append(child.ToXml());
			}

			result.Append("</")
				.Append(node.Name)
				.AppendLine(">");

			return result.ToString();
		}

		public override string ToString()
		{
			if (Elements().Count > 0)
				return $"<{Name}></{Name}>";

			return $"<{Name} />";
		}

		private static FhirXmlNode Create(string name, string @namespace, string text, IPositionInfo pos, List<IFhirXmlNode> children, List<FhirXmlReader.Attr> attributes)
		{
			return new FhirXmlNode(name, @namespace, text, pos, children, attributes);
		}

		public static IFhirXmlNode Create(XmlReader reader)
		{
			return FhirXmlReader.Read<IFhirXmlNode>(reader, Create, false);
		}

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
			return XPath.Parse(path)?.Value(this);
		}

		public IList<object> GetValues(string path)
		{
			return XPath.Parse(path)?.Values(this);
		}


		public IFhirXmlNode AddElement(string name, string @namespace, string text)
		{
			var element = new FhirXmlNode(name, @namespace, text, null, null, null)
			{
				Parent = this
			};

			Elements().Add(element);

			return element;
		}

		public bool DeleteElement(IFhirXmlNode child)
		{
			return _elements?.Remove(child) ?? false;
		}

		public IFhirXmlAttribute AddAttribute(string key, string value)
		{
			var attribute = new FhirXmlAttribute(key, value);
			Attributes().Add(attribute);
			return attribute;
		}

	}
}
