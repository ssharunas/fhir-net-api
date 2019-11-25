using System.Xml.Serialization;

namespace Hl7.Fhir.Model.ESPBI
{

	public class DrugInteractionQueryItem
	{
		public DrugInteractionQueryItem() { }

		public DrugInteractionQueryItem(string activeSubstances, string formName)
		{
			ActiveSubstances = activeSubstances;
			FormName = formName;
		}

		/// <summary>
		/// Skiriamo/kažkada paskirto vaisto bendrinis ar veikliųjų medžiagų pavadinimai atskirti ženklu „/“ 
		/// </summary>
		[XmlElement(ElementName = "activeSubstances")]
		public string ActiveSubstances { get; set; }

		/// <summary>
		/// Skiriamo/kažkada paskirto vaisto ar veikliosios medžiagos farmacinė forma
		/// </summary>
		[XmlElement(ElementName = "pharmForm")]
		public string FormName { get; set; }

		/// <summary>
		/// oldDrug – požymis, kuris nurodo, kad vaisto skyrimas nėra naujas („true“ – skyrimas nėra naujas, „false“ – skyrimas yra naujas).
		/// Nenurodžius šio požymio, jis bus išskaičiuotas iš ESPBI turimų paciento duomenų, t. y.jeigu vaistas patenka į tikrinamų vaistų sąrašą pagal
		/// ESPBI turimus duomenis, požymis įgyja reikšmę „true“. Aktualu nurodyti, jeigu norima gauti sąveikas tik tarp naujai skiriamų ir 
		/// paciento vartojamų vaistų.
		/// </summary>
		[XmlElement(ElementName = "oldDrug")]
		public bool? IsOldDrug { get; set; }

		/// <summary>
		/// DO NOT USE!
		/// This property is used for stupid .NET XmlSerializer.
		/// </summary>
		public bool IsOldDrugSpecified => IsOldDrug != null;
	}
}
