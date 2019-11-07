﻿/* 
 * Copyright (c) 2014, Furore (info@furore.com) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using Hl7.Fhir.Support;
using System;
using System.Collections;
using System.ComponentModel.DataAnnotations;

namespace Hl7.Fhir.Validation
{
	[AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
	public class CardinalityAttribute : ValidationAttribute
	{
		public CardinalityAttribute()
		{
			Min = 0;
			Max = 1;
		}

		public int Min { get; set; }
		public int Max { get; set; }

		protected override ValidationResult IsValid(object value, ValidationContext validationContext)
		{
			if (value == null)
			{
				return (Min == 0) ? ValidationResult.Success :
					DotNetAttributeValidation.BuildResult(validationContext, $"Element with min. cardinality {Min} cannot be null");
			}

			var count = 1;

			if (value is IList && !ReflectionHelper.IsArray(value))
			{
				var list = value as IList;
				foreach (var elem in list)
					if (elem == null) return DotNetAttributeValidation.BuildResult(validationContext, "Repeating element cannot have empty/null values");
				count = list.Count;
			}

			if (count < Min)
				return DotNetAttributeValidation.BuildResult(validationContext, $"Element has {count} elements, but min. cardinality is {Min}");

			if (Max != -1 && count > Max)
				return DotNetAttributeValidation.BuildResult(validationContext, $"Element has {count} elements, but max. cardinality is {Max}");

			return ValidationResult.Success;
		}
	}
}
