using Hl7.Fhir.Model.ESPBI;
using Hl7.Fhir.Serialization;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Text;
using System.Xml.Serialization;

namespace Hl7.Fhir.Model
{
	[TestClass]
	public class EspbiTest
	{
		[TestMethod]
		public void TestDrugInteractionSerialization()
		{
			var query = new DrugInteractionQuery(1).AddDrug("vorfarinas", "tabletės").AddDrug("aceklofenakas", "tabletės");

			var serialized = EspbiSerializer.Serialize(query);

			Assert.IsNotNull(serialized);

			Assert.AreEqual(@"<?xml version=""1.0"" encoding=""utf-8""?>
<drugInteractionQuery xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"" xmlns=""http://www.esveikata.lt/erx-fhir"">
  <patientId>1</patientId>
  <qDrug>
    <activeSubstances>vorfarinas</activeSubstances>
    <pharmForm>tabletės</pharmForm>
  </qDrug>
  <qDrug>
    <activeSubstances>aceklofenakas</activeSubstances>
    <pharmForm>tabletės</pharmForm>
  </qDrug>
</drugInteractionQuery>", serialized);
		}

		[TestMethod]
		public void TestDrugInteractionDeserialization()
		{
			string result = @"<?xml version=""1.0"" encoding=""UTF-8"" standalone=""yes""?><drugInteractionQueryResult xmlns=""http://www.esveikata.lt/erx-fhir""><drugInteractionQueryId>1913626</drugInteractionQueryId><drugLimit>30</drugLimit><drugLimitReached>false</drugLimitReached><drugInteraction><iDrug><activeSubstance>varfarinas</activeSubstance></iDrug><iDrug><activeSubstance>aceklofenakas</activeSubstance></iDrug><consequences>Nesteroidinių priešuždegiminių vaistų (NVNU) ir varfarino vartojimas kartu gali sukelti sunkius kraujavimus. Kraujavimo iš virškinamojo trakto rizika padidėja nuo 2 iki 3 karto, palyginti su gydymu vien varfarinu.</consequences><recommendation>Reikia vengti gydyti nesteroidiniais vaistai nuo uždegimo (NVNU) pacientus, kurie yra gydomi varfarinu. TNS vertinimas nėra pakankama kraujavimo rizikos stebėjimo priemonė, nes NVNU taip pat daro įtaką trombocitų funkcijai. Jei negalima išvengti gydymo kartu, apsvarstykite galimybę skirti skrandžio apsaugą protonų siurblio inhibitoriais (pavyzdžiui, lansoprazolu, omeprazolu ar pantoprazolu).</recommendation><clinicalImportanceCode>D</clinicalImportanceCode><clinicalImportanceText>Kliniškai reikšminga sąveika, kurios vertėtų vengti</clinicalImportanceText><scientificDocumentationCode>4</scientificDocumentationCode><scientificDocumentationText>Duomenys gauti iš atitinkamų pacientų grupėje atliktų kontroliuojamų tyrimų</scientificDocumentationText><link>https://vaistas.med24.lt/#search/interactions/7920/0?api_link=1&amp;temp_token=eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJuYW1lIjoiR3Vlc3QiLCJleHBpcmVzIjoxNTc0MTU0NjM4fQ.G6_72qxRd-PxYOhxpKNcVvFUQEcT2H7Qbbk3u57r0dM</link></drugInteraction></drugInteractionQueryResult>";

			var obj = EspbiSerializer.Deserialize<DrugInteraction>(Encoding.UTF8.GetBytes(result));

			Assert.IsNotNull(obj);
		}
	}
}
