﻿/* 
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
		public OperationOutcome Outcome { get; set; }

		public string RequestBody { get; set; }

		public string ResponseBody { get; set; }

		public FhirOperationException(string message) : base(message)
		{
		}


		public FhirOperationException(string message, Exception inner) : base(message, inner)
		{
		}

		public FhirOperationException(string message, OperationOutcome outcome, Exception inner)
			: base(message, inner)
		{
			Outcome = outcome;
		}

		public FhirOperationException(string message, OperationOutcome outcome)
			: base(message)
		{
			Outcome = outcome;
		}

	}



}
