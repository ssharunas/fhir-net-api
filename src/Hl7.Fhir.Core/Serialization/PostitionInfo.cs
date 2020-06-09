using Hl7.Fhir.Support;

namespace Hl7.Fhir.Serialization
{
	internal class PostitionInfo : IPositionInfo
	{
		public PostitionInfo(int lineNumber, int linePosition)
		{
			LineNumber = lineNumber;
			LinePosition = linePosition;
		}

		public int LineNumber { get; }
		public int LinePosition { get; }

		public override string ToString()
		{
			return $"line: {LineNumber}, position: {LinePosition}";
		}
	}
}
