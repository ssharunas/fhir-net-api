/* 
 * Copyright (c) 2014, Furore (info@furore.com) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using Hl7.Fhir.Support;
using System.Linq;

namespace Hl7.Fhir.Search
{
	internal class CompositeValue : ValueExpression
	{
		private const char TUPLESEPARATOR = '$';

		public ValueExpression[] Components { get; private set; }

		public CompositeValue(ValueExpression[] components)
		{
			if (components == null) throw Error.ArgumentNull(nameof(components));

			Components = components;
		}

		public override string ToString()
		{
			return string.Join(TUPLESEPARATOR.ToString(), Components.Select(v => v.ToString()));
		}

		public static CompositeValue Parse(string text)
		{
			if (text == null) throw Error.ArgumentNull(nameof(text));

			var values = text.SplitNotEscaped(TUPLESEPARATOR);

			return new CompositeValue(values.Select(v => new UntypedValue(v)).ToArray());
		}
	}
}