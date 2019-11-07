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
using System.Collections.Generic;
using System.Linq;

namespace Hl7.Fhir.Search
{
	/// <summary>
	/// Types of comparison operator applicable to searching on integer values
	/// </summary>
	internal enum Operator
	{
		LT,     // less than
		LTE,    // less than or equals
		EQ,     // equals (default)
		APPROX, // approximately equals
		GTE,    // greater than or equals
		GT,     // greater than

		ISNULL, // has no value
		NOTNULL, // has value
		IN,      // equals one of a set of values
		CHAIN    // chain to subexpression
	}

	internal class Criterium : Expression
	{
		public const string MISSINGMODIF = "missing";
		public const string MISSINGTRUE = "true";
		public const string MISSINGFALSE = "false";

		public string ParamName { get; set; }

		public Operator Type { get; set; } = Operator.EQ;

		public string Modifier { get; set; }

		public Expression Operand { get; set; }

		public static Criterium Parse(string key, string value)
		{
			if (string.IsNullOrEmpty(key)) throw Error.ArgumentNull(nameof(key));
			if (string.IsNullOrEmpty(value)) throw Error.ArgumentNull(nameof(value));

			// Split chained parts (if any) into name + modifier tuples
			var chainPath = key
				.Split(new[] { Query.SEARCH_CHAINSEPARATOR }, StringSplitOptions.RemoveEmptyEntries)
				.Select(s => pathToKeyModifTuple(s));

			if (chainPath.Count() == 0)
				throw Error.Argument(nameof(key), "Supplied an empty search parameter name or chain");

			return fromPathTuples(chainPath, value);
		}

		public static Criterium Parse(string text)
		{
			if (string.IsNullOrEmpty(text)) throw Error.ArgumentNull(nameof(text));

			var keyVal = text.SplitLeft('=');

			if (keyVal.Item2 == null) throw Error.Argument(nameof(text), "Value must contain an '=' to separate key and value");

			return Parse(keyVal.Item1, keyVal.Item2);
		}

		public static Criterium Parse(Extension parameter)
		{
			return Parse(Query.ExtractParamKey(parameter), Query.ExtractParamValue(parameter));
		}

		public override string ToString()
		{
			var result = ParamName;

			// Turn ISNULL and NOTNULL operators into the :missing modifier
			if (Type == Operator.ISNULL || Type == Operator.NOTNULL)
				result += Query.SEARCH_MODIFIERSEPARATOR + MISSINGMODIF;
			else
				if (!string.IsNullOrEmpty(Modifier)) result += Query.SEARCH_MODIFIERSEPARATOR + Modifier;

			if (Type == Operator.CHAIN)
			{
				if (Operand is Criterium)
					return result + Query.SEARCH_CHAINSEPARATOR + Operand.ToString();
				else
					return result + Query.SEARCH_CHAINSEPARATOR + " ** INVALID CHAIN OPERATION ** Chain operation must have a Criterium as operand";
			}
			else
			{
				return result + "=" + buildValue();
			}
		}

		private static Tuple<string, string> pathToKeyModifTuple(string pathPart)
		{
			var pair = pathPart.Split(Query.SEARCH_MODIFIERSEPARATOR);

			string name = pair[0];
			string modifier = pair.Length == 2 ? pair[1] : null;

			return Tuple.Create(name, modifier);
		}

		private static Criterium fromPathTuples(IEnumerable<Tuple<string, string>> path, string value)
		{
			var first = path.First();
			var name = first.Item1;
			var modifier = first.Item2;
			Operator type;
			Expression operand;

			// If this is a chained search, unfold the chain first
			if (path.Count() > 1)
			{
				type = Operator.CHAIN;
				operand = fromPathTuples(path.Skip(1), value);
			}
			else if (modifier == MISSINGMODIF) // :missing modifier is actually not a real modifier and is turned into either a ISNULL or NOTNULL operator
			{
				modifier = null;

				if (value == MISSINGTRUE)
					type = Operator.ISNULL;
				else if (value == MISSINGFALSE)
					type = Operator.NOTNULL;
				else
					throw Error.Argument(nameof(value), "For the :missing modifier, only values 'true' and 'false' are allowed");

				operand = null;
			}
			else // else see if the value starts with a comparator
			{
				var compVal = findComparator(value);

				type = compVal.Item1;
				value = compVal.Item2;

				if (value == null) throw Error.Format("Value is empty");

				// Parse the value. If there's > 1, we are using the IN operator, unless
				// the input already specifies another comparison, which would be illegal
				var values = ChoiceValue.Parse(value);

				if (values.Choices.Length > 1)
				{
					if (type != Operator.EQ)
						throw Error.InvalidOperation("Multiple values cannot be used in combination with a comparison operator");
					type = Operator.IN;
					operand = values;
				}
				else
				{
					// Not really a multi value, just a single ValueExpression
					operand = values.Choices[0];
				}
			}

			// Construct the new criterium based on the parsed values
			return new Criterium()
			{
				ParamName = name,
				Type = type,
				Modifier = modifier,
				Operand = operand
			};
		}

		private string buildValue()
		{
			// Turn ISNULL and NOTNULL operators into either true/or false to match the :missing modifier
			if (Type == Operator.ISNULL) return "true";
			if (Type == Operator.NOTNULL) return "false";

			if (Operand == null) throw Error.InvalidOperation("Criterium does not have an operand");
			if (!(Operand is ValueExpression)) Error.Format("Expected a ValueExpression as operand");

			string value = Operand.ToString();

			// Add comparator if we have one
			switch (Type)
			{
				case Operator.APPROX: return "~" + value;
				case Operator.EQ: return value;
				case Operator.IN: return value;
				case Operator.GT: return ">" + value;
				case Operator.GTE: return ">=" + value;
				case Operator.LT: return "<" + value;
				case Operator.LTE: return "<=" + value;
				default:
					throw Error.NotImplemented($"Operator of type '{Type}' is not supported");
			}
		}

		private static Tuple<Operator, string> findComparator(string value)
		{
			Operator comparison = Operator.EQ;

			if (value.StartsWith(">=") && value.Length > 2)
			{ comparison = Operator.GTE; value = value.Substring(2); }
			else if (value.StartsWith(">"))
			{ comparison = Operator.GT; value = value.Substring(1); }
			else if (value.StartsWith("<=") && value.Length > 2)
			{ comparison = Operator.LTE; value = value.Substring(2); }
			else if (value.StartsWith("<"))
			{ comparison = Operator.LT; value = value.Substring(1); }
			else if (value.StartsWith("~"))
			{ comparison = Operator.APPROX; value = value.Substring(1); }

			return Tuple.Create(comparison, value);
		}

	}
}
