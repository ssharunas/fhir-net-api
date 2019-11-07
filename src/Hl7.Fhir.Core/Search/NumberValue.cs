/* 
 * Copyright (c) 2014, Furore (info@furore.com) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using Hl7.Fhir.Serialization;

namespace Hl7.Fhir.Search
{
	internal class NumberValue : ValueExpression
	{
		public decimal Value { get; private set; }

		public NumberValue(decimal value)
		{
			Value = value;
		}

		public override string ToString()
		{
			return Value.ConvertTo<string>();
		}

		public static NumberValue Parse(string text)
		{
			return new NumberValue(text.ConvertTo<decimal>());
		}
	}
}