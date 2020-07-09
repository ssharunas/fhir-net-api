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
using System.Collections.Generic;

namespace Hl7.Fhir.Serialization
{
	internal static class ComplexTypeReader
	{
		internal static object Deserialize(IFhirReader reader, ClassMapping mapping, object existing = null)
		{
			if (mapping is null)
				throw Error.ArgumentNull(nameof(mapping));

			if (existing != null)
			{
				if (mapping.NativeType != existing.GetType())
					throw Error.Argument(nameof(existing), $"Existing instance is of type {existing.GetType().Name}, but type parameter indicates data type is a {mapping.NativeType.Name}");
			}
			else
			{
				existing = DefaultModelFactory.Create(mapping.NativeType);
			}

			IEnumerable<MemberInfo> members;

			if (reader.CurrentToken == TokenType.Object)
			{
				members = reader.GetMembers();
			}
			else if (reader.IsPrimitive())
			{
				// Ok, we expected a complex type, but we found a primitive instead. This may happen
				// in Json where the value property and the other elements are separately put into
				// member and _member. In this case, we will parse the primitive into the Value property
				// of the complex type
				if (!mapping.HasPrimitiveValueMember)
					throw Error.Format("Complex object does not have a value property, yet the reader is at a primitive", reader);

				// Simulate this as actually receiving a member "Value" in a normal complex object,
				// and resume normally
				members = new[] { new MemberInfo(mapping.PrimitiveValueProperty.Name, reader) };
			}
			else
			{
				throw Error.Format("Trying to read a complex object, but reader is not at the start of an object or primitive", reader);
			}

			read(reader, mapping, members, existing);

			return existing;

		}

		private static void read(IFhirReader reader, ClassMapping mapping, IEnumerable<MemberInfo> members, object existing)
		{
			foreach (var memberData in members)
			{
				// Find a property on the instance that matches the element found in the data
				// NB: This function knows how to handle suffixed names (e.g. xxxxBoolean) (for choice types).
				var mappedProperty = mapping.FindMappedElementByName(memberData.MemberName);

				if (mappedProperty != null)
				{
					//   Message.Info("Handling member {0}.{1}", mapping.Name, memberName);

					object value = null;

					// For primitive members we can save time by not calling the getter
					if (!mappedProperty.IsPrimitive)
						value = mappedProperty.GetValue(existing);

					value = DispatchingReader.DeserializeObject(memberData.Reader, mappedProperty, memberData.MemberName, value);

					mappedProperty.SetValue(existing, value);
				}
				else
				{
					if (SerializationConfig.AcceptUnknownMembers == false)
						throw Error.Format($"Encountered unknown member '{memberData.MemberName}' while deserializing", reader);
					else
						Message.Debug("Skipping unknown member " + memberData.MemberName);
				}
			}

			// Not sure if the reader needs to verify this. Certainly, I want to accept empty elements for the
			// pseudo-resource TagList (no tags) and probably others.
			//if (!hasMember)
			//    throw Error.Format("Fhir serialization does not allow nor support empty elements");
		}
	}
}
