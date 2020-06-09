using Hl7.Fhir.Serialization.Xml;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Xml;

namespace Hl7.Fhir.Applicator
{
	[TestClass]
	public class XPathTests
	{
		private IFhirXmlNode GetXml()
		{
			var stream = GetType().Assembly.GetManifestResourceStream(GetType().Namespace + "." + "XPathTest.xml");
			return FhirXmlNode.Create(XmlReader.Create(stream));
		}

		[TestMethod]
		public void IsMatchTest()
		{
			var xml = GetXml();

			Assert.IsTrue(XPath.XPath.Parse("/").IsMatch(xml));
			Assert.IsTrue(XPath.XPath.Parse("extension").IsMatch(xml));
			Assert.IsTrue(XPath.XPath.Parse("/extension").IsMatch(xml));
			Assert.IsTrue(XPath.XPath.Parse("/extension[@url = 'http://esveikata.lt/Profile/ltnhr-practitioner#department']").IsMatch(xml));
			Assert.IsTrue(XPath.XPath.Parse("/extension[@url = 'http://esveikata.lt/Profile/ltnhr-practitioner#department']/valueResource").IsMatch(xml));
			Assert.IsTrue(XPath.XPath.Parse("/extension/.././careProvider").IsMatch(xml));
			Assert.IsTrue(XPath.XPath.Parse("/extension/../.").IsMatch(xml));
			Assert.IsTrue(XPath.XPath.Parse("/careProvider").IsMatch(xml));
			Assert.IsTrue(XPath.XPath.Parse("/careProvider[starts-with(reference/@value, 'Doctor')]").IsMatch(xml));

			Assert.IsFalse(XPath.XPath.Parse("/xxx").IsMatch(xml));
			Assert.IsFalse(XPath.XPath.Parse("xxx").IsMatch(xml));
			Assert.IsFalse(XPath.XPath.Parse("/extension[@url = 'xxx']").IsMatch(xml));
			Assert.IsFalse(XPath.XPath.Parse("/extension/../../.").IsMatch(xml));
			Assert.IsFalse(XPath.XPath.Parse("/extension/../.././careProvider").IsMatch(xml));
			Assert.IsFalse(XPath.XPath.Parse("/careProvider[starts-with(reference/@value, 'xxx')]").IsMatch(xml));

			Assert.IsTrue(XPath.XPath.Parse("true() = true()").IsMatch(xml));
			Assert.IsFalse(XPath.XPath.Parse("true() = false()").IsMatch(xml));

			Assert.IsTrue(XPath.XPath.Parse("ceiling(3.6) = number('4')").IsMatch(xml));
			Assert.IsTrue(XPath.XPath.Parse("floor(3.6) = 3").IsMatch(xml));
			Assert.IsTrue(XPath.XPath.Parse("round(3.2) = 3").IsMatch(xml));
			Assert.IsFalse(XPath.XPath.Parse("ceiling(3.6) = number('3')").IsMatch(xml));
			Assert.IsFalse(XPath.XPath.Parse("floor(3.6) = 4").IsMatch(xml));
			Assert.IsFalse(XPath.XPath.Parse("round(3.2) = 4").IsMatch(xml));

			Assert.IsTrue(XPath.XPath.Parse("choose(true(), true(), false())").IsMatch(xml));
			Assert.IsFalse(XPath.XPath.Parse("choose(true(), false(), true())").IsMatch(xml));

			Assert.IsTrue(XPath.XPath.Parse("not(false())").IsMatch(xml));
			Assert.IsTrue(XPath.XPath.Parse("not(/xxx)").IsMatch(xml));
			Assert.IsFalse(XPath.XPath.Parse("not(/extension)").IsMatch(xml));

			Assert.IsTrue(XPath.XPath.Parse("string-length('12345') = 5").IsMatch(xml));

			Assert.IsTrue(XPath.XPath.Parse("count(participant/type/coding/code[@value = 'ADM']) = 1").IsMatch(xml));
			Assert.IsTrue(XPath.XPath.Parse("count(careProvider) = 2").IsMatch(xml));
			Assert.IsFalse(XPath.XPath.Parse("count(extension) = 2").IsMatch(xml));

			Assert.IsTrue(XPath.XPath.Parse("function-available('function-available')").IsMatch(xml));
			Assert.IsFalse(XPath.XPath.Parse("function-available('xxx')").IsMatch(xml));

			Assert.IsTrue(XPath.XPath.Parse("index-of(/careProvider, /careProvider[starts-with(reference/@value, 'Doctor')]) = 0").IsMatch(xml));
			Assert.IsTrue(XPath.XPath.Parse("index-of(/careProvider, /careProvider[starts-with(reference/@value, 'Organisation')]) = 1").IsMatch(xml));
			Assert.IsFalse(XPath.XPath.Parse("index-of(/careProvider, /extension) = 1").IsMatch(xml));
		}


		[TestMethod]
		public void ValueTest()
		{
			var xml = GetXml();

			Assert.AreEqual(false, XPath.XPath.Parse("function-available('xxx')").RawValue(xml));
			Assert.AreEqual(true, XPath.XPath.Parse("function-available('function-available')").RawValue(xml));

			Assert.AreEqual("Organization/123546", XPath.XPath.Parse("extension/valueResource/reference/@value").Value(xml));
			Assert.AreEqual("123", XPath.XPath.Parse("id/text()").Value(xml));
			Assert.AreEqual("feed", XPath.XPath.Parse("name(.)").Value(xml));
			Assert.AreEqual("id", XPath.XPath.Parse("name(id/.)").Value(xml));
			Assert.AreEqual("Organization/123546", XPath.XPath.Parse("extension/valueResource/reference/@value/text()").Value(xml));

			Assert.AreEqual(1, XPath.XPath.Parse("/careProvider[is-first(../careProvider)]").Values(xml).Count);
		}
	}
}
