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
	internal class ResourceReader
	{
		private IFhirReader _reader;
		private ModelInspector _inspector;

		public ResourceReader(IFhirReader reader)
		{
			_reader = reader;
			_inspector = SerializationConfig.Inspector;
		}

		public object Deserialize(object existing = null, bool nested = false)
		{
			if (_reader.CurrentToken == TokenType.Object)
			{
				// If there's no a priori knowledge of the type of Resource we will encounter,
				// we'll have to determine from the data itself. 
				var resourceType = _reader.GetResourceTypeName(nested);
				var mappedType = _inspector.FindClassMappingForResource(resourceType);

				if (mappedType == null)
				{
					// Special courtesy case
					if (resourceType == "feed" || resourceType == "Bundle")
						throw Error.Format("Encountered a feed instead of a resource", _reader);
					else
						throw Error.Format($"Encountered unknown resource type {resourceType}", _reader);
				}

				if (existing == null)
				{
					var fac = new DefaultModelFactory();
					existing = fac.Create(mappedType.NativeType);
				}
				else
				{
					if (mappedType.NativeType != existing.GetType())
						throw Error.Argument(nameof(existing), $"Existing instance is of type {existing.GetType().Name}, but data indicates resource is a {resourceType}");
				}

				// Delegate the actual work to the ComplexTypeReader, since
				// the serialization of Resources and ComplexTypes are virtually the same
				return new ComplexTypeReader(_reader).Deserialize(mappedType, existing);
			}

			throw Error.Format("Trying to read a resource, but reader is not at the start of an object", _reader);
		}
	}
}
