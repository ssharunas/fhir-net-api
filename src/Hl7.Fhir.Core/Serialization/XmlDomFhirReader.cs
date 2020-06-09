using Hl7.Fhir.Serialization.Xml;
using Hl7.Fhir.Support;
using System.Collections.Generic;
using System.Linq;

namespace Hl7.Fhir.Serialization
{
	internal class XmlDomFhirReader : IFhirReader
	{
		public const string BINARY_CONTENT_MEMBER_NAME = "content";
		private const string XHTMLDIV = "div";
		private IFhirXmlValue _current;
		private string _currentText;

		public XmlDomFhirReader(IFhirXmlValue node)
		{
			_current = node;
		}

		private XmlDomFhirReader(string text)
		{
			_currentText = text;
		}

		public TokenType CurrentToken
		{
			get
			{
				// Note: <div> XElements have already been tranformed to an XText with the <div> content,
				// so no special checking necessary
				if (_current is IFhirXmlNode)
					return TokenType.Object;
				if (_current is IFhirXmlAttribute)
					return TokenType.String;
				if (!string.IsNullOrEmpty(_currentText))
					return TokenType.String;
				else
					throw Error.Format($"Parser cannot handle xml objects of type {_current.GetType().Name}", this);
			}
		}

		public int LineNumber => (_current as IFhirXmlNode)?.Position.LineNumber ?? -1;

		public int LinePosition => (_current as IFhirXmlNode)?.Position.LinePosition ?? -1;

		public IEnumerable<IFhirReader> GetArrayElements()
		{
			throw Error.NotSupported("Xml does not support arrays like Json. This method won't be called if CurrentToken is never set to Array");
		}

		public IEnumerable<MemberInfo> GetMembers()
		{
			if (_current is IFhirXmlNode rootElem)
			{
				var result = new List<MemberInfo>();

				// First, any attributes
				foreach (var attr in rootElem.Attributes())
				{
					result.Add(new MemberInfo(attr.Name, new XmlDomFhirReader(attr)));
				}

				foreach (var node in rootElem.Elements())
				{
					// All normal elements
					if (node.Namespace == XmlNs.FHIR)
						result.Add(new MemberInfo(node.Name, new XmlDomFhirReader(node)));
					// The special xhtml div element
					else if (node.Is(XmlNs.XHTML, XHTMLDIV))
						result.Add(new MemberInfo(node.Name, new XmlDomFhirReader(node.ToXml())));
					else
						throw Error.Format($"Encountered element '{node.Name}' from unsupported namespace '{node.Namespace}'", this);
				}

				if (!string.IsNullOrEmpty(rootElem.ValueAsString))
				{
					result.Add(new MemberInfo(BINARY_CONTENT_MEMBER_NAME, new XmlDomFhirReader(rootElem.ValueAsString)));
				}

				return result;
			}
			else
				throw Error.Format("Cannot get members: reader not at an element", this);
		}

		public object GetPrimitiveValue()
		{
			if (_current is IFhirXmlAttribute)
				return _current.ValueAsString;
			else if (!string.IsNullOrEmpty(_currentText))
				return _currentText;
			else
				throw Error.Format("Parser is not at a primitive value", this);
		}

		public string GetResourceTypeName(bool nested)
		{
			if (nested && _current is IFhirXmlNode element)
				_current = element.Elements()?.First();

			if (_current is IFhirXmlNode)
				return _current.Name;
			else
				throw Error.Format("Cannot get resource type name: reader not at an element", this);
		}
	}
}
