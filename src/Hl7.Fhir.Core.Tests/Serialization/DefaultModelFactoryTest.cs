/* 
 * Copyright (c) 2014, Furore (info@furore.com) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using Hl7.Fhir.Introspection;
using Hl7.Fhir.Model;
using Hl7.Fhir.Serialization;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace Hl7.Fhir.Tests.Serialization
{
	[TestClass]
	public class DefaultModelFactoryTest
	{
		[TestMethod]
		public void TestSupportedTypes()
		{
			// Test creation of a class with no constructor
			//Assert.IsTrue(DefaultModelFactory.CanCreate(typeof(TestCreate)));
			Assert.IsNotNull(DefaultModelFactory.Create(typeof(TestCreate)));

			// Test creation of class with a public no-args constructor
			//Assert.IsTrue(DefaultModelFactory.CanCreate(typeof(TestCreatePublicConstructor)));
			Assert.IsNotNull(DefaultModelFactory.Create(typeof(TestCreatePublicConstructor)));

			// Test creation of primitives
			//Assert.IsTrue(DefaultModelFactory.CanCreate(typeof(int)));
			Assert.IsNotNull(DefaultModelFactory.Create(typeof(int)));

			// Test creation of Nullable<T>
			//Assert.IsTrue(DefaultModelFactory.CanCreate(typeof(int?)));
			Assert.IsNotNull(DefaultModelFactory.Create(typeof(int?)));

			// Test handling of collection interfaces
			object collection = null;
			//Assert.IsTrue(DefaultModelFactory.CanCreate(typeof(ICollection<string>)));
			collection = DefaultModelFactory.Create(typeof(ICollection<string>));
			Assert.IsNotNull(collection);
			Assert.IsTrue(collection is List<string>);

			//Assert.IsTrue(DefaultModelFactory.CanCreate(typeof(IList<HumanName>)));
			Assert.IsNotNull(DefaultModelFactory.Create(typeof(ICollection<HumanName>)));

			//Assert.IsTrue(DefaultModelFactory.CanCreate(typeof(IList<int?>)));
			collection = DefaultModelFactory.Create(typeof(ICollection<int?>));
			Assert.IsNotNull(collection);
			Assert.IsTrue(collection is List<int?>);

			// Test handling of closed generics
			//Assert.IsTrue(DefaultModelFactory.CanCreate(typeof(Code<Address.AddressUse>)));
			Assert.IsNotNull(DefaultModelFactory.Create(typeof(Code<Address.AddressUse>)));
		}

		[TestMethod]
		public void TestUnsupportedTypes()
		{
			//Assert.IsFalse(DefaultModelFactory.CanCreate(typeof(TestCreatePrivateConstructor)));
			//Assert.IsFalse(DefaultModelFactory.CanCreate(typeof(TestCreateArgConstructor)));

			// Cannot create interface types
			//Assert.IsFalse(DefaultModelFactory.CanCreate(typeof(ICloneable)));

			// Cannot create arrays, since we don't know size upfront
			//Assert.IsFalse(DefaultModelFactory.CanCreate(typeof(int[])));
		}
	}

	[FhirType("Patient")]   // implicitly, this is a resource
	public class NewPatient : Patient { }

	public class TestCreate
	{
	}

	public class TestCreatePublicConstructor
	{
		public TestCreatePublicConstructor()
		{
		}
	}

	public class TestCreateArgConstructor
	{
		public TestCreateArgConstructor(int test)
		{
		}
	}

	public class TestCreatePrivateConstructor
	{
		private TestCreatePrivateConstructor() { }
	}
}
