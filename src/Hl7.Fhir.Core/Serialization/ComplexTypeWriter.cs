/* 
 * Copyright (c) 2014, Furore (info@furore.com) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using Hl7.Fhir.Introspection;
using Hl7.Fhir.Support;
using System;
using System.Collections;
using System.Linq;


namespace Hl7.Fhir.Serialization
{
	internal class ComplexTypeWriter
	{
		private readonly IFhirWriter _current;

		internal enum SerializationMode
		{
			AllMembers,
			ValueElement,
			NonValueElements
		}

		public ComplexTypeWriter(IFhirWriter writer)
		{
			_current = writer;
		}

		internal void Serialize(ClassMapping mapping, object instance, bool summary, SerializationMode mode = SerializationMode.AllMembers)
		{
			if (mapping is null)
				throw Error.ArgumentNull(nameof(mapping));

			_current.WriteStartComplexContent();

			// Emit members that need xml /attributes/ first (to facilitate stream writer API)
			foreach (var prop in mapping.PropertyMappings.Where(pm => pm.SerializationHint == XmlSerializationHint.Attribute))
			{
				if (!summary || prop.InSummary)
					write(instance, summary, prop, mode);
			}

			// Then emit the rest
			foreach (var prop in mapping.PropertyMappings.Where(pm => pm.SerializationHint != XmlSerializationHint.Attribute))
			{
				if (!summary || prop.InSummary)
					write(instance, summary, prop, mode);
			}

			_current.WriteEndComplexContent();
		}

		private void write(/*ClassMapping mapping,*/ object instance, bool summary, PropertyMapping prop, SerializationMode mode)
		{
			// Check whether we are asked to just serialize the value element (Value members of primitive Fhir datatypes)
			// or only the other members (Extension, Id etc in primitive Fhir datatypes)
			// Default is all
			if (mode == SerializationMode.ValueElement && !prop.RepresentsValueElement)
				return;

			if (mode == SerializationMode.NonValueElements && prop.RepresentsValueElement)
				return;

			var value = prop.GetValue(instance);
			var isEmptyArray = (value as IList)?.Count == 0;

			if (value != null && !isEmptyArray)
			{
				string memberName = prop.Name;

				// For Choice properties, determine the actual name of the element
				// by appending its type to the base property name (i.e. deceasedBoolean, deceasedDate)
				if (prop.Choice == ChoiceType.DatatypeChoice)
					memberName = DetermineElementMemberName(prop.Name, value.GetType());

				_current.WriteStartProperty(memberName);

				var writer = new DispatchingWriter(_current);

				// Now, if our writer does not use dual properties for primitive values + rest (xml),
				// or this is a complex property without value element, serialize data normally
				if (!_current.HasValueElementSupport || !serializedIntoTwoProperties(prop, value))
					writer.Serialize(prop, value, summary, SerializationMode.AllMembers);
				else
				{
					// else split up between two properties, name and _name
					writer.Serialize(prop, value, summary, SerializationMode.ValueElement);
					_current.WriteEndProperty();
					_current.WriteStartProperty("_" + memberName);
					writer.Serialize(prop, value, summary, SerializationMode.NonValueElements);
				}

				_current.WriteEndProperty();
			}
		}

		// If we have a normal complex property, for which the type has a primitive value member...
		private bool serializedIntoTwoProperties(PropertyMapping prop, object instance)
		{
			if (instance is IList list)
				instance = list[0];

			if (instance != null && !prop.IsPrimitive && prop.Choice != ChoiceType.ResourceChoice)
			{
				var mapping = SerializationConfig.Inspector.ImportType(instance.GetType());
				return mapping.HasPrimitiveValueMember;
			}
			else
				return false;
		}

		private static string upperCamel(string p)
		{
			if (string.IsNullOrEmpty(p))
				return p;

			return char.ToUpperInvariant(p[0]) + p.Remove(0, 1);
		}

		internal static string DetermineElementMemberName(string memberName, Type type)
		{
			var suffix = SerializationConfig.Inspector.ImportType(type).Name;

			if (suffix == "ResourceReference")
				suffix = "Resource";

			return memberName + upperCamel(suffix);
		}

	}
}
