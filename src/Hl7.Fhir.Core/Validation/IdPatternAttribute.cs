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
using System.ComponentModel.DataAnnotations;

namespace Hl7.Fhir.Validation
{
	[AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
	public class IdPatternAttribute : ValidationAttribute
	{
		protected override ValidationResult IsValid(object value, ValidationContext validationContext)
		{
			if (value != null)
			{
				if (value.GetType() != typeof(string))
					throw Error.Argument(nameof(value), "IdPatternAttribute can only be applied to string properties");

				if (!Id.IsValidValue(value as string))
					return DotNetAttributeValidation.BuildResult(validationContext, $"{value} is not a correctly formatted Id");
			}

			return ValidationResult.Success;
		}
	}
}
