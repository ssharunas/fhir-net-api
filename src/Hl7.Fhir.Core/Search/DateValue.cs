﻿/* 
 * Copyright (c) 2014, Furore (info@furore.com) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using Hl7.Fhir.Model;
using Hl7.Fhir.Serialization;
using Hl7.Fhir.Support;
using System;

namespace Hl7.Fhir.Search
{
	internal class DateValue : ValueExpression
	{
		public string Value { get; private set; }

		public DateValue(DateTimeOffset value)
		{
			Value = value.ConvertTo<string>();
		}

		public DateValue(string date)
		{
			if (!FhirDateTime.IsValidValue(date)) throw Error.Argument(nameof(date), "Is not a valid FHIR date/time string");

			Value = date;
		}

		public override string ToString()
		{
			return Value;
		}

		public static DateValue Parse(string text)
		{
			return new DateValue(text);
		}
	}
}