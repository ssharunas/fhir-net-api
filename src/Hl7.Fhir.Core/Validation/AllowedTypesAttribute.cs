/* 
 * Copyright (c) 2014, Furore (info@furore.com) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using System;
using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Hl7.Fhir.Validation
{
	[AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
	public class AllowedTypesAttribute : ValidationAttribute
	{
		public AllowedTypesAttribute(params Type[] types)
		{
			Types = types;
		}

		public Type[] Types { get; set; }

		protected override ValidationResult IsValid(object value, ValidationContext validationContext)
		{
			ValidationResult result = ValidationResult.Success;

			if (value != null)
			{
				if (value is IEnumerable list)
				{
					foreach (var item in list)
					{
						result = validateValue(item, validationContext);

						if (result != ValidationResult.Success)
							break;
					}
				}
				else
				{
					result = validateValue(value, validationContext);
				}
			}

			return result;
		}

		private ValidationResult validateValue(object item, ValidationContext context)
		{
			if (item != null)
			{
				if (!Types.Any(type => type.IsAssignableFrom(item.GetType())))
					return DotNetAttributeValidation.BuildResult(context, $"Value is of type {item.GetType()}, which is not an allowed choice");
			}

			return ValidationResult.Success;
		}

	}
}
