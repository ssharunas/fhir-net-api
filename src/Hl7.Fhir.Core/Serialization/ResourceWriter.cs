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
	internal class ResourceWriter
	{
		private readonly IFhirWriter _writer;

		public ResourceWriter(IFhirWriter writer)
		{
			_writer = writer;
		}

		public void Serialize(object instance, bool summary, bool contained = false)
		{
			if (instance is null) throw Error.ArgumentNull(nameof(instance));

			var mapping = SerializationConfig.Inspector.ImportType(instance.GetType());

			_writer.WriteStartRootObject(mapping.Name, contained);

			new ComplexTypeWriter(_writer).Serialize(mapping, instance, summary);

			_writer.WriteEndRootObject(contained);
		}
	}
}
