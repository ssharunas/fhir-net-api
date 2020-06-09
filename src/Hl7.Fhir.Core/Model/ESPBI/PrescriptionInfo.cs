using System.Xml.Serialization;

namespace Hl7.Fhir.Model.ESPBI
{
	[XmlRoot(ElementName = "prescriptionInfo", Namespace = "http://www.esveikata.lt/erx-fhir")]
	public class PrescriptionInfo
	{
		[XmlElement("firstPrescribing")]
		public bool IsFirstPrescribing { get; set; }
	}
}
