using Hl7.Fhir.Applicator;
using System;

namespace Hl7.Fhir.Core.Serialization
{
	public class FhirSerializationException : FormatException
	{
		public FhirSerializationException() { }

		public FhirSerializationException(string message, Exception innerException, IPathable pathable) : base(message, innerException)
		{
			Node = pathable;
			NodeXml = pathable?.ToXml();
		}

		public IPathable Node { get; set; }
		public string NodeXml { get; }
	}
}
