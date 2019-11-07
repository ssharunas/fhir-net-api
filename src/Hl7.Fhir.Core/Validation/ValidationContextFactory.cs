/* 
 * Copyright (c) 2014, Furore (info@furore.com) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Hl7.Fhir.Validation
{
	public static class ValidationContextFactory
	{
		public static ValidationContext Create(object instance, IDictionary<object, object> items, bool recurse = false)
		{
			var result = new ValidationContext(instance, null, items);
			result.SetValidateRecursively(recurse);
			return result;
		}
	}
}
