using System.Collections.Generic;
using System.Xml.Serialization;

namespace Hl7.Fhir.Model.ESPBI
{
	/// <summary>
	///  Vaistų sąveikos tikrinimas
	/// </summary>
	[XmlRoot(ElementName = "drugInteractionQuery", Namespace = "http://www.esveikata.lt/erx-fhir")]
	public class DrugInteractionQuery
	{
		public DrugInteractionQuery() { }

		public DrugInteractionQuery(ulong patientID)
		{
			if (patientID > 0)
				PatientID = patientID;
		}

		/// <summary>
		/// patientId – paciento, kuriam skiriami vaistai, FHIR identifikatorius. 
		/// Parametras neprivalomas.
		/// Pateikus, tikrinant vaistų sąveiką, bus įtraukiami ESPBI esantys pacientui skirti vaistai, kurie gali
		/// sąveikoti su užklausoje pateikiamais vaistais. Nepateikus, tikrinant vaistų sąveiką, bus tikrinamama
		/// tik užklausoje pateiktų vaistų tarpusavio sąveika.
		/// </summary>
		[XmlElement(ElementName = "patientID")]
		public ulong? PatientID { get; set; }

		/// <summary>
		/// DO NOT USE!
		/// This property is used for stupid .NET XmlSerializer.
		/// </summary>
		public bool PatientIDSpecified => PatientID != null;

		/// <summary>
		/// qDrug - pacientui skiriamas ar kažkada paskirtas vaistas, kurio gali nebūti ESPBI sistemoje ir kuris
		/// gali sąveikoti su paciento vartojamais vaistais (parametras neprivalomas, jeigu nurodomas
		/// patientId, tokiu atveju bus tikrinama tik ESPBI esančių paciento galimai vartojamų vaistų sąveika)
		/// </summary>
		[XmlElement(ElementName = "qDrug")]
		public List<DrugInteractionQueryItem> Drugs { get; set; }

		/// <summary>
		/// Pridėti medžiagą tikrinimui. Sąraše turėtų būti bent 1 medžiaga.
		/// </summary>
		public DrugInteractionQuery AddDrug(string activeSubstances, string formName)
		{
			if (Drugs == null)
				Drugs = new List<DrugInteractionQueryItem>();

			Drugs.Add(new DrugInteractionQueryItem(activeSubstances, formName));

			return this;
		}
	}
}
