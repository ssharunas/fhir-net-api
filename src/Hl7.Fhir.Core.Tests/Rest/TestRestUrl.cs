﻿/* 
 * Copyright (c) 2014, Furore (info@furore.com) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Hl7.Fhir.Support;
using Hl7.Fhir.Rest;
using Hl7.Fhir.Model;

namespace Hl7.Fhir.Tests.Rest
{
    [TestClass]
	public class TestRestUrl
    {
        [TestMethod]
        public void CreateFromEndPoint()
        {
            RestUrl endpoint = new RestUrl("http://localhost/fhir");
            RestUrl resturi;

            resturi = endpoint.ForCollection("patient");
            Assert.AreEqual("http://localhost/fhir/patient", resturi.AsString);

            resturi = endpoint.Resource("patient", "1");
            Assert.AreEqual("http://localhost/fhir/patient/1", resturi.AsString);

            resturi = endpoint.Resource("patient", "1");
            Assert.AreEqual("http://localhost/fhir/patient/1", resturi.AsString);
        }

        [TestMethod]
        public void Query()
        {
            RestUrl endpoint = new RestUrl("http://localhost/fhir");
            RestUrl resturi;
            
            resturi = endpoint.Search("organization").AddParam("family", "Johnson").AddParam("given", "William");
            Assert.AreEqual("http://localhost/fhir/organization/_search?family=Johnson&given=William", resturi.AsString);

            var rl2 = new RestUrl(resturi.Uri);

            rl2.AddParam("given","Piet");
            Assert.AreEqual("http://localhost/fhir/organization/_search?family=Johnson&given=William&given=Piet", rl2.AsString);
        }


        [TestMethod]
        public void TryNavigation()
        {
            var old = new RestUrl("http://www.hl7.org/svc/Organization/");
            var rl = old.NavigateTo("../Patient/1/_history");

            Assert.AreEqual("http://www.hl7.org/svc/Patient/1/_history", rl.ToString());

            old = new RestUrl("http://hl7.org/fhir/Patient/1");
            rl = old.NavigateTo("2");
            Assert.AreEqual("http://hl7.org/fhir/Patient/2",rl.ToString());

            rl = old.NavigateTo("../Observation/3");
            Assert.AreEqual("http://hl7.org/fhir/Observation/3",rl.ToString());
        }

        [TestMethod]
        public void TestEscaping()
        {
            var url = new RestUrl("http://www.server.org/fhir");
            url.AddParam("_since", FhirDateTime.Now().Value);

            var output = url.Uri;
            Assert.IsFalse(output.ToString().Contains("+"));    // don't use un-escaped +
        }


        [TestMethod]
        public void TestBase()
        {
            var u = new RestUrl("http://www.hl7.org/svc");

            Assert.IsTrue(u.IsEndpointFor("http://www.hl7.org/svc"));
            Assert.IsTrue(u.IsEndpointFor("http://www.hl7.org/svc/"));
            Assert.IsTrue(u.IsEndpointFor("http://www.hl7.org/svc/Organization"));
            Assert.IsTrue(u.IsEndpointFor("http://www.hl7.org/svc/Organization/"));
            Assert.IsTrue(u.IsEndpointFor("http://www.hl7.org/svc/Organization/search?name=eve"));
            Assert.IsFalse(u.IsEndpointFor("http://www.hl7.org/svx/Organization"));

            var v = new RestUrl("http://fhirtest.uhn.ca/base");
            Assert.IsTrue(v.IsEndpointFor("http://fhirtest.uhn.ca/base?_getpages=8bba8a5a-233f-4f00-8db4-a00418c806fd&_getpagesoffset=10&_count=10&_format=xml"));

            var x = new RestUrl("http://fhirtest.uhn.ca/base/");
            Assert.IsTrue(x.IsEndpointFor("http://fhirtest.uhn.ca/base?_getpages=8bba8a5a-233f-4f00-8db4-a00418c806fd&_getpagesoffset=10&_count=10&_format=xml"));
        }

        //[TestMethod]
        //public void ParamManipulation()
        //{
        //    var rl = new ResourceLocation("patient/search?name=Kramer&name=Moreau&oauth=XXX");

        //    rl.SetParam("newParamA", "1");
        //    rl.SetParam("newParamB", "2");
        //    Assert.IsTrue(rl.ToString().EndsWith("oauth=XXX&newParamA=1&newParamB=2"));

        //    rl.SetParam("newParamA", "3");
        //    rl.ClearParam("newParamB");
        //    Assert.IsTrue(rl.ToString().EndsWith("oauth=XXX&newParamA=3"));

        //    rl.AddParam("newParamA", "4");
        //    Assert.IsTrue(rl.ToString().EndsWith("oauth=XXX&newParamA=3&newParamA=4"));

        //    rl.AddParam("newParamB", "5");
        //    Assert.IsTrue(rl.ToString().EndsWith("oauth=XXX&newParamA=3&newParamA=4&newParamB=5"));

        //    Assert.AreEqual("patient/search?name=Kramer&name=Moreau&oauth=XXX&newParamA=3&newParamA=4&newParamB=5",
        //            rl.OperationPath.ToString());

        //    rl = new ResourceLocation("patient/search");
        //    rl.SetParam("firstParam", "1");
        //    rl.SetParam("sndParam", "2");
        //    rl.ClearParam("sndParam");
        //    Assert.AreEqual("patient/search?firstParam=1", rl.OperationPath.ToString());

        //    rl.ClearParam("firstParam");
        //    Assert.AreEqual("patient/search", rl.OperationPath.ToString());

        //    rl.SetParam("firstParam", "1");
        //    rl.SetParam("sndParam", "2");
        //    rl.ClearParams();
        //    Assert.AreEqual("patient/search", rl.OperationPath.ToString());
        //}
    }
}
