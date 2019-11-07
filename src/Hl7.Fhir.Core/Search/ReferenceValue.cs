/* 
 * Copyright (c) 2014, Furore (info@furore.com) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using Hl7.Fhir.Model;
using Hl7.Fhir.Support;
using System;

namespace Hl7.Fhir.Search
{
	internal class ReferenceValue : ValueExpression
	{
		public string Value { get; private set; }

		public ReferenceValue(string value)
		{
			if (!Uri.IsWellFormedUriString(value, UriKind.Absolute) && !Id.IsValidValue(value))
				throw Error.Argument(nameof(value), "Reference is not a valid Id nor a valid absolute Url");

			Value = value;
		}

		public override string ToString()
		{
			return StringValue.EscapeString(Value);
		}

		public static ReferenceValue Parse(string text)
		{
			return new ReferenceValue(StringValue.UnescapeString(text));
		}
	}
}