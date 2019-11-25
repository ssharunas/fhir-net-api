using System.Collections.Generic;
using System.Xml.Serialization;

namespace Hl7.Fhir.Model.ESPBI
{
	/// <summary>
	/// Užklausos apie saveikaujančias medžiagas ar vaistus rezultatas.
	/// </summary>
	[XmlRoot(ElementName = "drugInteractionQueryResult", Namespace = "http://www.esveikata.lt/erx-fhir")]
	public class DrugInteraction
	{
		/// <summary>
		/// vaistų sąveikos tikrinimo užklausos identifikatorius, kuris turi būti paduodamas MedicationPrescription 
		/// resurso drugInteractionQueryId plėtinyje;
		/// </summary>
		[XmlElement("drugInteractionQueryId")]
		public long ID { get; set; }

		/// <summary>
		///  nurodo, koks yra maksimalus vaistų, kurių sąveika yra tikrinama, skaičius (0 – neribotas);
		/// </summary>
		[XmlElement("drugLimit")]
		public int Limit { get; set; }

		/// <summary>
		/// drugLimitReached – požymis, kuris nurodo, ar buvo viršytas tikrinamų vaistų maksimalus skaičius
		/// (true - reiškia, kad viršytas ir ne visų paciento vartojamų vaistų sąveika yra patikrinta, false - patikrinta visų vaistų sąveika);
		/// </summary>
		[XmlElement("drugLimitReached")]
		public bool IsLimitReached { get; set; }

		/// <summary>
		/// Vaistų ar medžiagų sąveikos
		/// </summary>
		[XmlElement("drugInteraction")]
		public List<DrugInteractionItem> Interactions { get; set; }
	}
}
