/* 
 * Copyright (c) 2014, Furore (info@furore.com) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using Hl7.Fhir.Model;
using System;

namespace Hl7.Fhir.Rest
{
	public class FhirOperationException : Exception
	{
		public FhirOperationException(string message) : base(message) { }

		public FhirOperationException(string message, FhirRequest request, FhirResponse response) : base(message)
		{
			Request = request;
			Response = response;

			try
			{
				// Try to parse the body as an OperationOutcome resource, but it is no
				// problem if it's something else, or there is no parseable body at all

				Outcome = response.GetBodyAsEntry<OperationOutcome>(null).Resource;
			}
			catch
			{
				// failed, so the body does not contain an OperationOutcome.
				// Put the raw body as a message in the OperationOutcome as a fallback scenario
				var body = response.GetBodyAsString();
				if (!string.IsNullOrEmpty(body))
					Outcome = OperationOutcome.ForMessage(body);
			}
		}

		public OperationOutcome Outcome { get; }

		public FhirRequest Request { get; }

		public FhirResponse Response { get; }
	}
}
