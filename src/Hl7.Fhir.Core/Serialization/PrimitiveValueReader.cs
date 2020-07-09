/* 
 * Copyright (c) 2014, Furore (info@furore.com) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using Hl7.Fhir.Support;
using System;

namespace Hl7.Fhir.Serialization
{
	internal static class PrimitiveValueReader
	{
		internal static object Deserialize(IFhirReader reader, Type nativeType)
		{
			if (nativeType is null)
				throw Error.ArgumentNull(nameof(nativeType));

			if (reader.IsPrimitive())
				return read(reader, nativeType);

			throw Error.Format("Trying to read a value, but reader is not at the start of a primitive", reader);
		}

		private static object read(IFhirReader reader, Type nativeType)
		{
			object primitiveValue = reader.GetPrimitiveValue();

			if (nativeType.IsEnum && primitiveValue is string)
			{
				var enumMapping = SerializationConfig.Inspector.FindEnumMappingByType(nativeType);

				if (enumMapping != null)
				{
					var enumLiteral = (string)primitiveValue;
					if (enumMapping.ContainsLiteral(enumLiteral))
						return enumMapping.ParseLiteral((string)primitiveValue);

					throw Error.Format($"Literal {enumLiteral} is not a valid value for enumeration {enumMapping.Name}", reader);
				}

				throw Error.Format("Cannot find an enumeration mapping for enum " + nativeType.Name, reader);
			}

			try
			{
				return PrimitiveTypeConverter.Convert(primitiveValue, nativeType);
			}
			catch (NotSupportedException exc)
			{
				// thrown when an unsupported conversion was required
				throw Error.Format(exc.Message, reader);
			}
		}
	}
}
