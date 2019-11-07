/* 
 * Copyright (c) 2014, Furore (info@furore.com) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using Hl7.Fhir.Support;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Hl7.Fhir.Validation
{
	public class DotNetAttributeValidation
	{
		public static void Validate(object value, bool recurse = false)
		{
			if (value == null) throw Error.ArgumentNull(nameof(value));

			Validator.ValidateObject(value, ValidationContextFactory.Create(value, null, recurse), true);
		}

		public static bool TryValidate(object value, ICollection<ValidationResult> validationResults = null, bool recurse = false)
		{
			if (value == null) throw Error.ArgumentNull(nameof(value));

			var results = validationResults ?? new List<ValidationResult>();
			return Validator.TryValidateObject(value, ValidationContextFactory.Create(value, null, recurse), results, true);

			// Note, if you pass a null validationResults, you will *not* get results (it's not an out param!)
		}

		internal static ValidationResult BuildResult(ValidationContext context, string message)
		{
			if (!string.IsNullOrEmpty(context?.MemberName))
				return new ValidationResult(message, new[] { context.MemberName });
			else
				return new ValidationResult(message);
		}
	}

}
