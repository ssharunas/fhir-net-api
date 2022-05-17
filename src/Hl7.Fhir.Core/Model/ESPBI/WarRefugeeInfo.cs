using Newtonsoft.Json;

namespace Hl7.Fhir.Core.Model.ESPBI
{
	public class WarRefugeeInfo
	{
		[JsonProperty("exists")]
		public long Exists { get; set; }
	}
}
