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
	internal class ChoiceValue : ValueExpression
	{
		private const char VALUESEPARATOR = ',';

		public ValueExpression[] Choices { get; private set; }

		public ChoiceValue(ValueExpression[] choices)
		{
			if (choices == null) throw Error.ArgumentNull(nameof(choices));

			Choices = choices;
		}

		public override string ToString()
		{
			return string.Join<ValueExpression>(VALUESEPARATOR.ToString(), Choices);
		}

		public static ChoiceValue Parse(string text)
		{
			if (text == null) throw Error.ArgumentNull(nameof(text));

			var values = text.SplitNotEscaped(VALUESEPARATOR);

			return new ChoiceValue(values.Select(v => splitIntoComposite(v)).ToArray());
		}

		private static ValueExpression splitIntoComposite(string text)
		{
			var composite = CompositeValue.Parse(text);

			// If there's only one component, this really was a single value
			if (composite.Components.Length == 1)
				return composite.Components[0];
			else
				return composite;
		}
	}
}