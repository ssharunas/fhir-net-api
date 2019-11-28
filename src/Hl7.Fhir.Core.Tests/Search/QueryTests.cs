﻿/* 
 * Copyright (c) 2014, Furore (info@furore.com) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using Hl7.Fhir.Model;
using Hl7.Fhir.Serialization;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Hl7.Fhir.Tests.Search
{
	[TestClass]
	public class QueryTests
	{
		[TestMethod]
		public void RetrieveCriteria()
		{
			var q = Query
				.For<Patient>().Where("name:exact=ewout").OrderBy("birthDate", SortOrder.Descending)
				.SummaryOnly().Include("Patient.managingOrganization")
				.LimitTo(20);
			Assert.AreEqual(1, q.Criteria.Count());
			Assert.AreEqual("ewout", Query.ExtractParamValue(q.Criteria.First()));
		}

		[TestMethod]
		public void CountSetToNullAndGet()
		{
			var q = new Query();
			q.Count = null;
			Assert.IsFalse(q.Count.HasValue);
		}

		[TestMethod]
		public void ManipulateParameters()
		{
			var q = new Query();

			q.AddParameter("testX", "someVal");
			q.AddParameter("testX", "someVal2");
			q.AddParameter("testXY", "someVal3");

			var vals = q.GetValues("testX");
			Assert.AreEqual(2, vals.Count());
			Assert.AreEqual("someVal", vals.First());
			Assert.AreEqual("someVal2", vals.Skip(1).First());
			Assert.AreEqual("someVal3", q.GetSingleValue("testXY"));

			q.RemoveParameter("testXY");
			Assert.IsNull(q.GetSingleValue("testXY"));
			Assert.AreEqual(2, q.GetValues("testX").Count());
		}


		[TestMethod]
		public void TestProperties()
		{
			var q = new Query();

			q.QueryName = "special";
			q.ResourceType = "Patient";
			q.Count = 31;
			q.Summary = true;
			q.Sort = new List<Tuple<string, SortOrder>>() { Tuple.Create("sorted", SortOrder.Descending) };
			q.Includes.Add("Patient.name");
			q.Includes.Add("Observation.subject");

			Assert.AreEqual("special", q.QueryName);
			Assert.AreEqual("Patient", q.ResourceType);
			Assert.AreEqual(31, q.Count);
			Assert.AreEqual(true, q.Summary);
			Assert.AreEqual(Tuple.Create("sorted", SortOrder.Descending), q.Sort.Single());
			Assert.AreEqual(2, q.Includes.Count);
			Assert.AreEqual("Patient.name", q.Includes.First());
			Assert.AreEqual("Observation.subject", q.Includes.Skip(1).First());

			q.QueryName = "special2";
			q.ResourceType = "Observation";
			q.Count = 32;
			q.Summary = false;
			q.Sort = new List<Tuple<string, SortOrder>>() { Tuple.Create("sorted2", SortOrder.Ascending) };
			q.Includes.Add("Patient.name2");
			q.Includes.Remove("Patient.name");
			q.Includes.Add("Observation.subject2");

			Assert.AreEqual("special2", q.QueryName);
			Assert.AreEqual("Observation", q.ResourceType);
			Assert.AreEqual(32, q.Count);
			Assert.AreEqual(false, q.Summary);
			Assert.AreEqual(Tuple.Create("sorted2", SortOrder.Ascending), q.Sort.Single());
			Assert.AreEqual(3, q.Includes.Count);
			Assert.IsTrue(q.Includes.Contains("Patient.name2"));
			Assert.IsFalse(q.Includes.Contains("Patient.name"));
			Assert.IsTrue(q.Includes.Contains("Observation.subject"));
			Assert.IsTrue(q.Includes.Contains("Observation.subject2"));
		}

		[TestMethod]
		public void TestSort()
		{
			var q = new Query();
			q.AddSort("_id", SortOrder.Ascending);

			Assert.AreEqual(Tuple.Create("_id", SortOrder.Ascending), q.Sort.Single());
		}

		[TestMethod]
		public void ManageSearchResult()
		{
			var q = Query.For<Patient>()
				.Where("name:exact=ewout").OrderBy("birthDate", SortOrder.Descending)
				.SummaryOnly().Include("Patient.managingOrganization")
				.LimitTo(20);

			var x = FhirSerializer.SerializeResourceToXml(q);
			Console.WriteLine(x);

			Assert.AreEqual("Patient", q.ResourceType);

			var p = q.Parameter.SingleWithName("name");
			Assert.AreEqual("name:exact", Query.ExtractParamKey(p));
			Assert.AreEqual("ewout", Query.ExtractParamValue(p));

			var o = q.Sort.Single();
			Assert.AreEqual("birthDate", o.Item1);
			Assert.AreEqual(SortOrder.Descending, o.Item2);

			Assert.IsTrue(q.Summary);
			Assert.IsTrue(q.Includes.Contains("Patient.managingOrganization"));
			Assert.AreEqual(20, q.Count);
		}

		[TestMethod]
		public void ReapplyParam()
		{
			var q = new Query()
				.WithName("mySearch").OrderBy("adsfadf").OrderBy("q", SortOrder.Descending)
				.LimitTo(10).LimitTo(20).WithName("miSearch").SummaryOnly().SummaryOnly(false);

			Assert.AreEqual("miSearch", q.QueryName);
			Assert.IsFalse(q.Summary);

			var o = q.Sort.First();
			Assert.AreEqual("adsfadf", o.Item1);
			Assert.AreEqual(SortOrder.Ascending, o.Item2);

			o = q.Sort.Skip(1).First();
			Assert.AreEqual("q", o.Item1);
			Assert.AreEqual(SortOrder.Descending, o.Item2);

			Assert.AreEqual(20, q.Count);

			Assert.IsFalse(q.Summary);
		}
	}
}
