/* 
 * Copyright (c) 2014, Furore (info@furore.com) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using Hl7.Fhir.Serialization;
using Hl7.Fhir.Support;

namespace Hl7.Fhir.Search
{
	internal class QuantityValue : ValueExpression
	{
		public decimal Number { get; private set; }

		public string Namespace { get; private set; }

		public string Unit { get; private set; }

		public QuantityValue(decimal number, string unit)
		{
			Number = number;
			Unit = unit;
		}

		public QuantityValue(decimal number, string ns, string unit)
		{
			Number = number;
			Unit = unit;
			Namespace = ns;
		}

		public override string ToString()
		{
			var ns = Namespace ?? string.Empty;
			return Number.ConvertTo<string>() + "|" +
				StringValue.EscapeString(ns) + "|" +
				StringValue.EscapeString(Unit);
		}

		public static QuantityValue Parse(string text)
		{
			if (text == null) throw Error.ArgumentNull(nameof(text));

			string[] triple = text.SplitNotEscaped('|');

			if (triple.Length != 3)
				throw Error.Argument(nameof(text), "Quantity needs to have three parts separated by '|'");

			if (triple[0] == string.Empty)
				throw Error.Format("Quantity needs to specify a number");

			var number = triple[0].ConvertTo<decimal>();
			var ns = triple[1] != string.Empty ? StringValue.UnescapeString(triple[1]) : null;

			if (triple[2] == string.Empty)
				throw Error.Format("Quantity needs to specify a unit");

			var unit = StringValue.UnescapeString(triple[2]);

			return new QuantityValue(number, ns, unit);
		}
	}
}