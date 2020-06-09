/* 
 * Copyright (c) 2014, Furore (info@furore.com) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using Hl7.Fhir.Introspection;
using Hl7.Fhir.Support;
using System.Collections;


namespace Hl7.Fhir.Serialization
{
	internal class RepeatingElementWriter
	{
		private readonly IFhirWriter _current;

		public RepeatingElementWriter(IFhirWriter writer)
		{
			_current = writer;
		}

		public void Serialize(PropertyMapping prop, object instance, bool summary, ComplexTypeWriter.SerializationMode mode)
		{

			if (prop == null) throw Error.ArgumentNull(nameof(prop));

			var elements = instance as IList;
			if (elements == null)
				throw Error.Argument(nameof(elements), "Can only write repeating elements from a type implementing IList");

			_current.WriteStartArray();

			foreach (var element in elements)
			{
				new DispatchingWriter(_current).Serialize(prop, element, summary, mode);
			}

			_current.WriteEndArray();
		}
	}
}
