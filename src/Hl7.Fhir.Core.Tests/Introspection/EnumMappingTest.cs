﻿/* 
 * Copyright (c) 2014, Furore (info@furore.com) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using Hl7.Fhir.Introspection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Hl7.Fhir.Tests.Introspection
{
	[TestClass]
	public class EnumMappingTest
	{
		[TestMethod]
		public void TestCreation()
		{
			EnumMapping mapping = EnumMapping.Create(typeof(TestEnum));

			Assert.AreEqual("Testee", mapping.Name);
			Assert.AreEqual(typeof(TestEnum), mapping.EnumType);
			Assert.IsTrue(mapping.ContainsLiteral("Item1"));
			Assert.IsFalse(mapping.ContainsLiteral("Item2"));
			Assert.IsFalse(mapping.ContainsLiteral("iTeM1"));
			Assert.IsTrue(mapping.ContainsLiteral("ItemTwo"));

			Assert.AreEqual(TestEnum.Item2, mapping.ParseLiteral("ItemTwo"));
			Assert.AreEqual(TestEnum.Item1, mapping.ParseLiteral("Item1"));
			Assert.AreEqual("ItemTwo", mapping.GetLiteral(TestEnum.Item2));
			Assert.AreEqual("Item1", mapping.GetLiteral(TestEnum.Item1));
		}


		[FhirEnumeration("Testee")]
		enum TestEnum
		{
			Item1 = 4,

			[EnumLiteral("ItemTwo")]
			Item2
		}
	}
}
