using System;
using System.Collections.Generic;

namespace Hl7.Fhir.Validation
{
	public class ValidationResult
	{
		public static readonly ValidationResult Success;

		public ValidationResult(string errorMessage) : this(errorMessage, null) { }

		public ValidationResult(string errorMessage, IEnumerable<string> memberNames)
		{
			ErrorMessage = errorMessage;
			MemberNames = memberNames ?? new string[0];
		}

		protected ValidationResult(ValidationResult validationResult)
		{
			if (validationResult == null)
				throw new ArgumentNullException("validationResult");

			ErrorMessage = validationResult.ErrorMessage;
			MemberNames = validationResult.MemberNames;
		}

		/// <summary>
		/// Gets the collection of member names affected by this result.  The collection may be empty but will never be null.
		/// </summary>
		public IEnumerable<string> MemberNames { get; }

		/// <summary>
		/// Gets the error message for this result.  It may be null.
		/// </summary>
		public string ErrorMessage { get; set; }

		/// <summary>
		/// Override the string representation of this instance, returning
		/// the <see cref="ErrorMessage"/> if not <c>null</c>, otherwise
		/// the base <see cref="Object.ToString"/> result.
		/// </summary>
		/// <remarks>
		/// If the <see cref="ErrorMessage"/> is empty, it will still qualify
		/// as being specified, and therefore returned from <see cref="ToString"/>.
		/// </remarks>
		/// <returns>The <see cref="ErrorMessage"/> property value if specified,
		/// otherwise, the base <see cref="Object.ToString"/> result.</returns>
		public override string ToString()
		{
			return ErrorMessage ?? base.ToString();
		}
	}
}
