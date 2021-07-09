using Hl7.Fhir.Introspection;
using Hl7.Fhir.Model;
using Hl7.Fhir.Serialization;
using Hl7.Fhir.Serialization.Xml;
using Hl7.Fhir.Support;
using System.Collections;
using System.Collections.Generic;

namespace Hl7.Fhir.Applicator.Navigation
{
	internal abstract class FhirObjectNavigatorBase<T> : NavigatorBase
	{
		private T _instance;

		public FhirObjectNavigatorBase(IFhirXmlNode parent, T element)
			: base(parent)
		{
			Mapping = SerializationConfig.Inspector.ImportType(element.GetType());
			_instance = element;
		}

		protected ClassMapping Mapping { get; }

		public override IList<IFhirXmlAttribute> Attributes()
		{
			var result = new List<IFhirXmlAttribute>();

			foreach (var mapping in Mapping.PropertyMappings)
			{
				if (mapping.SerializationHint == XmlSerializationHint.Attribute || mapping.RepresentsValueElement)
				{
					var value = mapping.GetValue(_instance);

					if (value != null)
						result.Add(new NavigatorAttribute(mapping.Name, value));
				}
			}

			return result;
		}

		public override IList<IFhirXmlNode> Elements()
		{
			var result = new List<IFhirXmlNode>();

			foreach (var mapping in Mapping.PropertyMappings)
			{
				AddElements(result, mapping);
			}

			return result;
		}

		private void AddElements(List<IFhirXmlNode> result, PropertyMapping mapping)
		{
			if (mapping.SerializationHint == XmlSerializationHint.Attribute || mapping.RepresentsValueElement)
				return;

			object value = mapping.GetValue(_instance);
			if (value != null)
			{
				if (mapping.IsCollection)
				{
					var list = value as IList;
					if (list is null)
						throw Error.InvalidOperation("Found non-list, anotated as a list.");

					foreach (var item in list)
						AddElements(result, mapping, item);
				}
				else
				{
					AddElements(result, mapping, value);
				}
			}
		}

		private void AddElements(List<IFhirXmlNode> result, PropertyMapping mapping, object value)
		{
			if (mapping.IsPrimitive)
			{
				throw Error.InvalidOperation("Shouldn't it be an atttribute?");
			}
			else if (value is Element element)
			{
				string name = mapping.Name;

				if (mapping.Choice == ChoiceType.DatatypeChoice)
					name = ComplexTypeWriter.DetermineElementMemberName(name, element.GetType());

				result.Add(new ElementNavigator(this, name, element));
			}
			else if (value is Resource resource)
			{
				result.Add(new ResourceNavigator(this, resource));
			}
			else
			{
				throw Error.InvalidOperation("Unknown element type");
			}
		}

		public override object Unwrap()
		{
			return _instance;
		}
	}
}
