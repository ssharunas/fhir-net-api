using Hl7.Fhir.Core.Validation;

namespace Hl7.Fhir.Validation
{
	public class RequiredAttribute : ValidationAttribute
	{
		public RequiredAttribute()
			: base(() => DataAnnotationsResources.RequiredAttribute_ValidationError)
		{
		}

		/// <summary>
		/// Gets or sets a flag indicating whether the attribute should allow empty strings.
		/// </summary>
		public bool AllowEmptyStrings { get; set; }

		/// <summary>
		/// Override of <see cref="ValidationAttribute.IsValid(object)"/>
		/// </summary>
		/// <param name="value">The value to test</param>
		/// <returns><c>false</c> if the <paramref name="value"/> is null or an empty string. If <see cref="RequiredAttribute.AllowEmptyStrings"/>
		/// then <c>false</c> is returned only if <paramref name="value"/> is null.</returns>
		public override bool IsValid(object value)
		{
			if (value == null)
			{
				return false;
			}

			// only check string length if empty strings are not allowed
			var stringValue = value as string;
			if (stringValue != null && !AllowEmptyStrings)
			{
				return stringValue.Trim().Length != 0;
			}

			return true;
		}

	}
}
