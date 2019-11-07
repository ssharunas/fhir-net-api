/* 
 * Copyright (c) 2014, Furore (info@furore.com) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using Hl7.Fhir.Model;
using System;
using System.Collections.Generic;

namespace Hl7.Fhir.Serialization
{
	internal static class QueryParser
	{
		internal static Query Load(string resource, IEnumerable<Tuple<string, string>> parameters)
		{
			Query result = new Query(resource, null);

			foreach (var p in parameters)
				result.AddParameter(p.Item1, p.Item2);

			return result;
		}
	}
}
