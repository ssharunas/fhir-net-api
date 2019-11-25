using System.Collections.Generic;
using System.Xml.Serialization;

namespace Hl7.Fhir.Model.ESPBI
{
	/// <summary>
	/// sąveikaujančių vaistų ar medžiagų informacija
	/// </summary>
	public class DrugInteractionItem
	{
		/// <summary>
		/// sąveikaujantis vaistas ar medžiaga;
		/// </summary>
		public class Drug
		{
			/// <summary>
			///  vaisto ar medžiagos pavadinimas
			/// </summary>
			[XmlElement("activeSubstance")]
			public string ActiveSubstance { get; set; }

			public static implicit operator string(Drug d) => d?.ActiveSubstance;

			public override string ToString()
			{
				return ActiveSubstance;
			}
		}

		/// <summary>
		///  sąveikaujantys vaistai ar medžiagos;
		/// </summary>
		[XmlElement("iDrug")]
		public List<Drug> Drugs { get; set; }

		/// <summary>
		/// pasekmės
		/// </summary>
		[XmlElement("consequences")]
		public string Consequences { get; set; }

		/// <summary>
		/// rekomendacija
		/// </summary>
		[XmlElement("recommendation")]
		public string Recommendation { get; set; }

		/// <summary>
		/// klinikinės svarbos kodas (galimų reikšmių aibės ieškoti https://vaistas.med24.lt puslapyje);
		/// </summary>
		[XmlElement("clinicalImportanceCode")]
		public string ClinicalImportanceCode { get; set; }

		/// <summary>
		/// klinikinės svarbos kodo paaiškinimas;
		/// </summary>
		[XmlElement("clinicalImportanceText")]
		public string ClinicalImportanceText { get; set; }

		/// <summary>
		/// dokumentacijos lygio kodas (galimų reikšmių aibės ieškoti https://vaistas.med24.lt puslapyje)
		/// </summary>
		[XmlElement("scientificDocumentationCode")]
		public string ScientificDocumentationCode { get; set; }

		/// <summary>
		/// dokumentacijos lygio kodo paaiškinimas;
		/// </summary>
		[XmlElement("scientificDocumentationText")]
		public string ScientificDocumentationText { get; set; }

		/// <summary>
		/// nuoroda į https://vaistas.med24.lt puslapį, kuriame pateikiama papildoma sąveikos informacija.
		/// </summary>
		[XmlElement("link")]
		public string Link { get; set; }
	}
}
