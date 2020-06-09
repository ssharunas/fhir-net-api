using Hl7.Fhir.Support;
using System.Collections.Generic;
using System.Xml;

namespace Hl7.Fhir.Serialization.Xml
{
	internal class FhirXmlReader
	{
		private static readonly IList<string> DEFAULT_NAMESPACES = new[] { "xmlns", "xsi", "schemaLocation" };

		internal struct Attr
		{
			public string Key;
			public string Value;

			public Attr(string key, string value)
			{
				Key = key;
				Value = value;
			}
		}

		public delegate T Creator<T>(string name, string @namespace, string text, IPositionInfo pos, List<T> children, List<Attr> attributes);

		public static T Read<T>(XmlReader reader, Creator<T> create, bool isSkipRead)
		{
			try
			{
				List<T> children = null;
				List<Attr> attributes = null;
				string elementName = null;
				string elementNamespace = null;
				string elementText = null;
				IPositionInfo elementPosition = null;

				while (isSkipRead || reader.Read())
				{
					isSkipRead = false;
					switch (reader.NodeType)
					{
						case XmlNodeType.Element:
							if (children != null)
							{
								children.Add(Read<T>(reader, create, true));
							}
							else
							{
								bool isEmpty = reader.IsEmptyElement;
								children = new List<T>();
								elementName = reader.LocalName;
								elementNamespace = reader.NamespaceURI;
								elementPosition = SerializationUtil.GetLineInfo(reader);

								var elementPrefix = reader.Prefix;
								while (reader.MoveToNextAttribute())
								{
									if (!DEFAULT_NAMESPACES.Contains(reader.LocalName) && reader.LocalName != elementPrefix) //skip xmlns declarations
									{
										if (reader.NodeType != XmlNodeType.Attribute)
											throw Error.Argument(nameof(reader), $"Invalid xml node type! Expected attribute, but got {reader.NodeType}. At {reader.GetLineInfo()}");

										if (!string.IsNullOrEmpty(reader.NamespaceURI))
											throw Error.Format($"Encountered unsupported attribute {reader.Name}", reader.GetLineInfo());

										string value = null;
										if (!string.IsNullOrEmpty(reader.Value))
											value = reader.Value;

										if (attributes == null)
											attributes = new List<Attr>();

										attributes.Add(new Attr(reader.LocalName, value));
									}
								}

								if (isEmpty)
									goto finishLoop;
							}

							break;

						case XmlNodeType.Text:
							if (!string.IsNullOrEmpty(reader.Value))
								elementText = reader.Value;
							break;

						case XmlNodeType.EndElement:
							goto finishLoop;
					}
				}

				finishLoop:

				return create(elementName, elementNamespace, elementText, elementPosition, children, attributes);
			}
			catch (XmlException ex)
			{
				throw Error.Format("Invalid xml structure!" + ex.Message, new PostitionInfo(ex.LineNumber, ex.LinePosition), ex);
			}
		}

	}
}
