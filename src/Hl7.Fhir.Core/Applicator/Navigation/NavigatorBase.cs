using Hl7.Fhir.Serialization;
using Hl7.Fhir.Serialization.Xml;
using Hl7.Fhir.Support;
using System;
using System.Collections.Generic;

namespace Hl7.Fhir.Applicator.Navigation
{
	internal abstract class NavigatorBase : FhirXmlValue, IFhirXmlNode, IWrapper
	{
		public NavigatorBase(IFhirXmlNode parent)
		{
			Parent = parent;
		}

		public NavigatorBase(IFhirXmlNode parent, string name, object value)
		{
			Name = name;
			Value = value;
			Parent = parent;
		}

		public string Namespace => null;

		public IPositionInfo Position => null;

		public IFhirXmlNode Parent { get; }

		public override object Value { get; }

		public override string ValueAsString
		{
			get => ToString(Value);
			set => throw new NotSupportedException();
		}

		public virtual IFhirXmlAttribute Attribute(string name)
		{
			var attributes = Attributes();
			if (attributes?.Count > 0)
			{
				foreach (var attr in attributes)
				{
					if (attr.Name == name)
						return attr;
				}
			}

			return null;
		}

		public abstract IList<IFhirXmlAttribute> Attributes();

		public virtual IFhirXmlNode Element(string @namespace, string name)
		{
			var elements = Elements(@namespace, name);

			if (elements?.Count > 0)
				return elements[0];

			return null;
		}

		public virtual IList<IFhirXmlNode> Elements(string @namespace, string name)
		{
			return Elements(name);
		}

		public virtual IList<IFhirXmlNode> Elements(string name)
		{
			List<IFhirXmlNode> result = new List<IFhirXmlNode>();
			var elements = Elements();
			foreach (var item in elements)
			{
				if (item.Name == name)
					result.Add(item);
			}
			return result;
		}

		public abstract IList<IFhirXmlNode> Elements();

		public abstract object Unwrap();

		internal static string ToString(object value)
		{
			if (value != null)
			{
				if (value.GetType().IsEnum)
				{
					var enumMap = SerializationConfig.Inspector.FindEnumMappingByType(value.GetType());
					if (enumMap != null)
						value = enumMap.GetLiteral((Enum)value);
				}
			}

			if (value != null)
				return PrimitiveTypeConverter.ConvertTo<string>(value);

			return null;
		}

		public IPathable GetNode(string path)
		{
			var result = XPath.XPath.Parse(path).Value(this);

			if (result != null)
				return (result as IPathable) ?? throw Error.InvalidOperation($"Value at path '{path}' is not 'IPathable', but '{result.GetType()}'.");

			return null;
		}

		public IList<IPathable> GetNodes(string path)
		{
			IList<IPathable> result = null;
			var items = XPath.XPath.Parse(path).Values(this);

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
			var result = XPath.XPath.Parse(path).RawValue(this);

			if (result is IWrapper wrapper)
				return wrapper.Unwrap();

			return result;
		}

		public IList<object> GetValues(string path)
		{
			var result = XPath.XPath.Parse(path).Values(this);

			if (result?.Count > 0)
			{
				for (var i = 0; i < result.Count; i++)
				{
					if (result[i] is IWrapper wrapper)
						result[i] = wrapper.Unwrap();
				}
			}

			return result;
		}

		public bool Is(string @namespace, string name)
		{
			return Name == name;
		}

		public string ToXml()
		{
			return FhirXmlNode.ToXml(this);
		}

		public override string ToString()
		{
			string node = "<" + Name;

			if (Elements()?.Count > 0)
				node += "></" + Name + ">";
			else
				node += "/>";

			return $"{typeof(IPathable)} {node}";
		}

		public IFhirXmlNode AddElement(string @namespace, string name, string text)
		{
			throw new NotImplementedException();
		}

		public bool DeleteElement(IFhirXmlNode child)
		{
			throw new NotImplementedException();
		}

		public IFhirXmlAttribute AddAttribute(string key, string value)
		{
			throw new NotImplementedException();
		}
	}
}
