/* 
 * Copyright (c) 2014, Furore (info@furore.com) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using Hl7.Fhir.Support;

namespace Hl7.Fhir.Serialization
{
	internal static class ResourceReader
	{
		public static object Deserialize(IFhirReader reader, object existing = null, bool nested = false)
		{
			if (reader.CurrentToken == TokenType.Object)
			{
				// If there's no a priori knowledge of the type of Resource we will encounter,
				// we'll have to determine from the data itself. 
				var resourceType = reader.GetResourceTypeName(nested);
				var mappedType = SerializationConfig.Inspector.FindClassMappingForResource(resourceType);

				if (mappedType == null)
				{
					// Special courtesy case
					if (resourceType == "feed" || resourceType == "Bundle")
						throw Error.Format("Encountered a feed instead of a resource", reader);
					else
						throw Error.Format($"Encountered unknown resource type '{resourceType}'.", reader);
				}

				// Delegate the actual work to the ComplexTypeReader, since
				// the serialization of Resources and ComplexTypes are virtually the same
				return ComplexTypeReader.Deserialize(reader, mappedType, existing);
			}

			throw Error.Format("Trying to read a resource, but reader is not at the start of an object", reader);
		}
	}
}
