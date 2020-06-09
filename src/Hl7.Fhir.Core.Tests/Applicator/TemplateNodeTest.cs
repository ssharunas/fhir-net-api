using Hl7.Fhir.Applicator.Xml;
using Hl7.Fhir.Serialization.Xml;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Hl7.Fhir.Applicator
{
	[TestClass]
	public class TemplateNodeTest
	{
		private const string TEMPLATE_FILE = nameof(TemplateNodeTest) + "Template.xml";
		private const string DATA_FILE = nameof(TemplateNodeTest) + "Data.xml";

		[TestMethod]
		public void SetterTestWithInclude()
		{
			var context = new TestDataSetterContext();
			var template = TemplateNode.Create(TestDataContextBase.GetXmlReader(TEMPLATE_FILE));
			var data = FhirXmlNode.Create(TestDataContextBase.GetXmlReader(DATA_FILE));

			template.ReadData(data, context);

			Assert.AreEqual(1, context.Arrays.Count);
			Assert.AreEqual(3, context.Values.Count);

			Assert.IsTrue(context.Arrays.ContainsKey("Items.Codes"));
			Assert.AreEqual(2, context.Arrays["Items.Codes"].Count);
			Assert.AreEqual("code1", context.Arrays["Items.Codes"][0].Values["Value"]);
			Assert.AreEqual("code2", context.Arrays["Items.Codes"][1].Values["Value"]);

			Assert.AreEqual("666", context.Values["ID"]);
			Assert.AreEqual("This is a remark", context.Values["Items.Remarks"]);
			Assert.AreEqual("Patient/123", context.Values["Items.PatientID"]);
		}

		[TestMethod]
		public void GetterTestNoItems()
		{
			var context = new TestDataGetterContext();
			var template = TemplateNode.Create(TestDataContextBase.GetXmlReader(TEMPLATE_FILE));

			context.AddValue("ID", "666");
			
			var result = template.CreateData(context);

			Assert.AreEqual(1, result.Elements().Count);

			Assert.AreEqual("666", result.GetValue("entry/id/text()"));
			Assert.AreEqual(null, result.GetValues("entry/content/Observation"));


			/*
			Assert.IsTrue(context.Arrays.ContainsKey("Items.Codes"));
			Assert.AreEqual(2, context.Arrays["Items.Codes"].Count);
			Assert.AreEqual("code1", context.Arrays["Items.Codes"][0].Values["Value"]);
			Assert.AreEqual("code2", context.Arrays["Items.Codes"][1].Values["Value"]);

			Assert.AreEqual("666", context.Values["ID"]);
			Assert.AreEqual("This is a remark", context.Values["Items.Remarks"]);
			Assert.AreEqual("Patient/123", context.Values["Items.PatientID"]);
			*/
		}

		[TestMethod]
		public void ValidateTestWithInclude()
		{
			var template = TemplateNode.Create(TestDataContextBase.GetXmlReader(TEMPLATE_FILE));

			var context = new TemplateNodeValidationResult(new TestDataSetterContext());
			template.Validate(context);

			Assert.IsNotNull(context.Template);
			Assert.IsNotNull(context.IncludeErrors);
			Assert.IsNotNull(context.TemplateData);

			Assert.IsFalse(context.HasErros());

			string str = string.Empty;

			foreach (var item in context.Template)
				str += item.LineText + "\n";

			Assert.AreEqual(21, context.Template.Count, "Invalid output xml: " + str);
			Assert.AreEqual(6, context.TemplateData.Count);
		}

		[TestMethod]
		public void CreateRoundtripTest()
		{
			var context = new TestDataSetterContext();
			var template = TemplateNode.Create(TestDataContextBase.GetXmlReader(TEMPLATE_FILE));
			var data = FhirXmlNode.Create(TestDataContextBase.GetXmlReader(DATA_FILE));

			template.ReadData(data, context);

			var getterContext = new TestDataGetterContext(context);
			var created = template.CreateData(getterContext);

			Assert.IsNotNull(data);

			foreach (var element in created.Elements())
				Assert.IsNotNull(data.Element(element.Namespace, element.Name));
		}

		[TestMethod]
		public void UpdateRoundtripTest()
		{
			var context = new TestDataSetterContext();
			var template = TemplateNode.Create(TestDataContextBase.GetXmlReader(TEMPLATE_FILE));
			var data = FhirXmlNode.Create(TestDataContextBase.GetXmlReader(DATA_FILE));
			string dataXml = data.ToXml();

			template.ReadData(data, context);

			var getterContext = new TestDataGetterContext(context);
			template.UpdateData(data, getterContext);

			Assert.IsNotNull(data);
			Assert.AreEqual(dataXml, data.ToXml());
		}

		[TestMethod]
		public void UpdateTest()
		{
			var context = new TestDataSetterContext();
			var template = TemplateNode.Create(TestDataContextBase.GetXmlReader(TEMPLATE_FILE));
			var data = FhirXmlNode.Create(TestDataContextBase.GetXmlReader(DATA_FILE));

			template.ReadData(data, context);

			var getterContext = new TestDataGetterContext(context);
			getterContext.AddArray("Items.Codes", new TestDataGetterContext().AddValue("Value", "AdditionalValue"));
			getterContext["Items.PatientID"] = "modified";
			getterContext["ID"] = "Modified";
			template.UpdateData(data, getterContext);

			Assert.IsNotNull(data);

			var xml = data.ToXml();

			Assert.IsTrue(xml.Contains("<id>Modified</id>"), "ID was not changed!");
			Assert.IsTrue(xml.Contains("<reference value=\"modified\">"), "Patient ID was not changed!");
			Assert.IsTrue(xml.Contains("<code value=\"AdditionalValue\">"), "Additional coding values was not added!");
		}

	}
}
