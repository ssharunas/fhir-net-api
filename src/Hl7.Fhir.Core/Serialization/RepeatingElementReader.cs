/* 
 * Copyright (c) 2014, Furore (info@furore.com) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using Hl7.Fhir.Introspection;
using Hl7.Fhir.Support;
using System.Collections;
using System.Collections.Generic;


namespace Hl7.Fhir.Serialization
{
	internal static class RepeatingElementReader
	{
		public static object Deserialize(IFhirReader reader, PropertyMapping prop, string memberName, object existing = null)
		{
			if (prop == null)
				throw Error.ArgumentNull(nameof(prop));

			if (existing != null && !(existing is IList))
				throw Error.Argument(nameof(existing), "Can only read repeating elements into a type implementing IList");

			IList result = existing as IList;

			bool overwriteMode;
			IEnumerable<IFhirReader> elements;

			if (reader.CurrentToken == TokenType.Array)        // Json has members that are arrays, if we encounter multiple, update the old values of the array
			{
				overwriteMode = result != null && result.Count > 0;
				elements = reader.GetArrayElements();
			}
			else if (reader.CurrentToken == TokenType.Object)  // Xml has repeating members, so this results in an "array" of just 1 member
			{
				//TODO: This makes   member : {x} in Json valid too,
				//even if json should have member : [{x}]
				overwriteMode = false;
				elements = new List<IFhirReader>() { reader };
			}
			else
				throw Error.Format("Expecting to be either at a repeating complex element or an array when parsing a repeating member.", reader);

			if (result == null)
				result = ReflectionHelper.CreateGenericList(prop.ElementType);

			var position = 0;
			foreach (var element in elements)
			{
				if (overwriteMode)
				{
					if (position >= result.Count)
						throw Error.Format("The value and extension array are not well-aligned", reader);

					// Arrays may contain null values as placeholders
					if (element.CurrentToken != TokenType.Null)
						result[position] = DispatchingReader.DeserializeArray(element, prop, memberName, existing: result[position]);
				}
				else
				{
					object item = null;
					if (element.CurrentToken != TokenType.Null)
						item = DispatchingReader.DeserializeArray(element, prop, memberName);
					//else
					//	item = null;  // Arrays may contain null values as placeholders

					result.Add(item);
				}

				position++;
			}

			return result;
		}
	}
}
