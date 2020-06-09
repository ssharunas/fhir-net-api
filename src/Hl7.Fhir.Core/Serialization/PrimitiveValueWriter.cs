/* 
 * Copyright (c) 2014, Furore (info@furore.com) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using Hl7.Fhir.Introspection;
using System;

namespace Hl7.Fhir.Serialization
{
	internal class PrimitiveValueWriter
	{
		private readonly IFhirWriter _current;

		public PrimitiveValueWriter(IFhirWriter data)
		{
			_current = data;
		}

		internal void Serialize(object value, XmlSerializationHint hint)
		{
			if (value != null)
			{
				var nativeType = value.GetType();

				if (nativeType.IsEnum)
				{
					var enumMapping = SerializationConfig.Inspector.FindEnumMappingByType(nativeType);

					if (enumMapping != null)
						value = enumMapping.GetLiteral((Enum)value);
				}
			}

			_current.WritePrimitiveContents(value, hint);
		}
	}

}
