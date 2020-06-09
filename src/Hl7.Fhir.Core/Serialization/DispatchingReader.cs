/* 
 * Copyright (c) 2014, Furore (info@furore.com) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using Hl7.Fhir.Introspection;
using Hl7.Fhir.Support;

namespace Hl7.Fhir.Serialization
{
	internal static class DispatchingReader
	{
		public static object DeserializeArray(IFhirReader reader, PropertyMapping prop, string memberName, object existing = null)
		{
			return Deserialize(reader, prop, memberName, existing, true);
		}

		public static object DeserializeObject(IFhirReader reader, PropertyMapping prop, string memberName, object existing = null)
		{
			return Deserialize(reader, prop, memberName, existing, false);
		}

		private static object Deserialize(IFhirReader reader, PropertyMapping prop, string memberName, object existing, bool isArrayMode)
		{
			if (prop == null) throw Error.ArgumentNull(nameof(prop));

			// ArrayMode avoid the dispatcher making nested calls into the RepeatingElementReader again
			// when reading array elements. FHIR does not support nested arrays, and this avoids an endlessly
			// nesting series of dispatcher calls
			if (!isArrayMode && prop.IsCollection)
				return RepeatingElementReader.Deserialize(reader, prop, memberName, existing);

			// If this is a primitive type, no classmappings and reflection is involved,
			// just parse the primitive from the input
			// NB: no choices for primitives!
			if (prop.IsPrimitive)
				return PrimitiveValueReader.Deserialize(reader, prop.ElementType);

			// A Choice property that contains a choice of any resource
			// (as used in Resource.contained)
			if (prop.Choice == ChoiceType.ResourceChoice)
				return ResourceReader.Deserialize(reader, existing, nested: true);

			ClassMapping mapping;

			// Handle other Choices having any datatype or a list of datatypes
			if (prop.Choice == ChoiceType.DatatypeChoice)
			{
				// For Choice properties, determine the actual type of the element using
				// the suffix of the membername (i.e. deceasedBoolean, deceasedDate)
				// This function implements type substitution.
				mapping = determineElementPropertyType(reader, prop, memberName);
			}
			// Else use the actual return type of the property
			else
			{
				mapping = SerializationConfig.Inspector.ImportType(prop.ElementType);
			}

			return ComplexTypeReader.Deserialize(reader, mapping, existing);
		}

		private static ClassMapping determineElementPropertyType(IFhirReader reader, PropertyMapping mappedProperty, string memberName)
		{
			var typeName = mappedProperty.GetChoiceSuffixFromName(memberName);

			if (string.IsNullOrEmpty(typeName))
				throw Error.Format($"Encountered polymorph member {memberName}, but is does not specify the type used", reader);

			// Exception: valueResource actually means the element is of type ResourceReference
			if (typeName == "Resource")
				typeName = "ResourceReference";

			// NB: this will return the latest type registered for that name, so supports type mapping/overriding
			// Maybe we should Import the types present on the choice, to make sure they are available. For now
			// assume the caller has Imported all types in the right (overriding) order.
			ClassMapping result = SerializationConfig.Inspector.FindClassMappingForFhirDataType(typeName);

			if (result == null)
				throw Error.Format($"Encountered polymorph member {memberName}, which uses unknown datatype {typeName}", reader);

			return result;
		}

	}
}
