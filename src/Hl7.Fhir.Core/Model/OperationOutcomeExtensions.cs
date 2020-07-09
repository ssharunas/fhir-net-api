/* 
 * Copyright (c) 2014, Furore (info@furore.com) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

namespace Hl7.Fhir.Model
{
	public static class OutcomeExtensions
	{
		public static bool Success(this OperationOutcome outcome)
		{
			if (outcome.Issue is null)
				return true;

			foreach (var issue in outcome.Issue)
			{
				if (issue.Severity == OperationOutcome.IssueSeverity.Error ||
					issue.Severity == OperationOutcome.IssueSeverity.Fatal)
					return false;
			}
			return true;
		}
	}
}
