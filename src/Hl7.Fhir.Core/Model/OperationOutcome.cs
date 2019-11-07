using System.Collections.Generic;

namespace Hl7.Fhir.Model
{
	public partial class OperationOutcome
	{
		public static OperationOutcome ForMessage(string message, IssueSeverity severity = IssueSeverity.Error)
		{
			return new OperationOutcome()
			{
				Issue = new List<OperationOutcomeIssueComponent>()
				{
					new OperationOutcomeIssueComponent() { Severity = severity, Details = message }
				}
			};
		}
	}
}
