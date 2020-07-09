using Hl7.Fhir.Introspection;
using Hl7.Fhir.Support;
using Hl7.Fhir.Validation;
using System.Collections.Generic;
using System.Runtime.Serialization;

/*
  Copyright (c) 2011-2013, HL7, Inc.
  All rights reserved.
  
  Redistribution and use in source and binary forms, with or without modification, 
  are permitted provided that the following conditions are met:
  
   * Redistributions of source code must retain the above copyright notice, this 
     list of conditions and the following disclaimer.
   * Redistributions in binary form must reproduce the above copyright notice, 
     this list of conditions and the following disclaimer in the documentation 
     and/or other materials provided with the distribution.
   * Neither the name of HL7 nor the names of its contributors may be used to 
     endorse or promote products derived from this software without specific 
     prior written permission.
  
  THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND 
  ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED 
  WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED. 
  IN NO EVENT SHALL THE COPYRIGHT HOLDER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, 
  INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT 
  NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR 
  PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, 
  WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) 
  ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE 
  POSSIBILITY OF SUCH DAMAGE.
  

*/

//
// Generated on Thu, Oct 23, 2014 14:22+0200 for FHIR v0.0.82
//
namespace Hl7.Fhir.Model
{
	/// <summary>
	/// A grouping of people or organizations with a common purpose
	/// </summary>
	[FhirType("Organization", IsResource = true)]
	[DataContract]
	public partial class Organization : Hl7.Fhir.Model.Resource
	{
		[FhirType("OrganizationContactComponent")]
		[DataContract]
		public partial class OrganizationContactComponent : Hl7.Fhir.Model.Element
		{
			/// <summary>
			/// The type of contact
			/// </summary>
			[FhirElement("purpose", InSummary = true, Order = 40)]
			[DataMember]
			public Hl7.Fhir.Model.CodeableConcept Purpose { get; set; }

			/// <summary>
			/// A name associated with the contact
			/// </summary>
			[FhirElement("name", InSummary = true, Order = 50)]
			[DataMember]
			public Hl7.Fhir.Model.HumanName Name { get; set; }

			/// <summary>
			/// Contact details (telephone, email, etc)  for a contact
			/// </summary>
			[FhirElement("telecom", InSummary = true, Order = 60)]
			[Cardinality(Min = 0, Max = -1)]
			[DataMember]
			public List<Hl7.Fhir.Model.Contact> Telecom { get; set; }

			/// <summary>
			/// Visiting or postal addresses for the contact
			/// </summary>
			[FhirElement("address", InSummary = true, Order = 70)]
			[DataMember]
			public Hl7.Fhir.Model.Address Address { get; set; }

			/// <summary>
			/// Gender for administrative purposes
			/// </summary>
			[FhirElement("gender", InSummary = true, Order = 80)]
			[DataMember]
			public Hl7.Fhir.Model.CodeableConcept Gender { get; set; }

			public override IDeepCopyable CopyTo(IDeepCopyable other)
			{
				var dest = other as OrganizationContactComponent;

				if (dest != null)
				{
					base.CopyTo(dest);
					if (Purpose != null) dest.Purpose = (Hl7.Fhir.Model.CodeableConcept)Purpose.DeepCopy();
					if (Name != null) dest.Name = (Hl7.Fhir.Model.HumanName)Name.DeepCopy();
					if (Telecom != null) dest.Telecom = new List<Hl7.Fhir.Model.Contact>(Telecom.DeepCopy());
					if (Address != null) dest.Address = (Hl7.Fhir.Model.Address)Address.DeepCopy();
					if (Gender != null) dest.Gender = (Hl7.Fhir.Model.CodeableConcept)Gender.DeepCopy();
					return dest;
				}
				else
					throw Error.Argument(nameof(other), "Can only copy to an object of the same type");
			}

			public override IDeepCopyable DeepCopy()
			{
				return CopyTo(new OrganizationContactComponent());
			}

			public override bool Matches(IDeepComparable other)
			{
				var otherT = other as OrganizationContactComponent;
				if (otherT is null) return false;

				if (!base.Matches(otherT)) return false;
				if (!DeepComparable.Matches(Purpose, otherT.Purpose)) return false;
				if (!DeepComparable.Matches(Name, otherT.Name)) return false;
				if (!DeepComparable.Matches(Telecom, otherT.Telecom)) return false;
				if (!DeepComparable.Matches(Address, otherT.Address)) return false;
				if (!DeepComparable.Matches(Gender, otherT.Gender)) return false;

				return true;
			}

			public override bool IsExactly(IDeepComparable other)
			{
				var otherT = other as OrganizationContactComponent;
				if (otherT is null) return false;

				if (!base.IsExactly(otherT)) return false;
				if (!DeepComparable.IsExactly(Purpose, otherT.Purpose)) return false;
				if (!DeepComparable.IsExactly(Name, otherT.Name)) return false;
				if (!DeepComparable.IsExactly(Telecom, otherT.Telecom)) return false;
				if (!DeepComparable.IsExactly(Address, otherT.Address)) return false;
				if (!DeepComparable.IsExactly(Gender, otherT.Gender)) return false;

				return true;
			}

		}


		/// <summary>
		/// Identifies this organization  across multiple systems
		/// </summary>
		[FhirElement("identifier", Order = 70)]
		[Cardinality(Min = 0, Max = -1)]
		[DataMember]
		public List<Hl7.Fhir.Model.Identifier> Identifier { get; set; }

		/// <summary>
		/// Name used for the organization
		/// </summary>
		[FhirElement("name", Order = 80)]
		[DataMember]
		public Hl7.Fhir.Model.FhirString NameElement { get; set; }

		/// <summary>
		/// Name used for the organization
		/// </summary>
		/// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
		[NotMapped]
		[IgnoreDataMember]
		public string Name
		{
			get { return NameElement?.Value; }
			set
			{
				if (value is null)
					NameElement = null;
				else
					NameElement = new Hl7.Fhir.Model.FhirString(value);
			}
		}

		/// <summary>
		/// Kind of organization
		/// </summary>
		[FhirElement("type", Order = 90)]
		[DataMember]
		public Hl7.Fhir.Model.CodeableConcept Type { get; set; }

		/// <summary>
		/// A contact detail for the organization
		/// </summary>
		[FhirElement("telecom", Order = 100)]
		[Cardinality(Min = 0, Max = -1)]
		[DataMember]
		public List<Hl7.Fhir.Model.Contact> Telecom { get; set; }

		/// <summary>
		/// An address for the organization
		/// </summary>
		[FhirElement("address", Order = 110)]
		[Cardinality(Min = 0, Max = -1)]
		[DataMember]
		public List<Hl7.Fhir.Model.Address> Address { get; set; }

		/// <summary>
		/// The organization of which this organization forms a part
		/// </summary>
		[FhirElement("partOf", Order = 120)]
		[References("Organization")]
		[DataMember]
		public Hl7.Fhir.Model.ResourceReference PartOf { get; set; }

		/// <summary>
		/// Contact for the organization for a certain purpose
		/// </summary>
		[FhirElement("contact", Order = 130)]
		[Cardinality(Min = 0, Max = -1)]
		[DataMember]
		public List<Hl7.Fhir.Model.Organization.OrganizationContactComponent> Contact { get; set; }

		/// <summary>
		/// Location(s) the organization uses to provide services
		/// </summary>
		[FhirElement("location", Order = 140)]
		[References("Location")]
		[Cardinality(Min = 0, Max = -1)]
		[DataMember]
		public List<Hl7.Fhir.Model.ResourceReference> Location { get; set; }

		/// <summary>
		/// Whether the organization's record is still in active use
		/// </summary>
		[FhirElement("active", Order = 150)]
		[DataMember]
		public Hl7.Fhir.Model.FhirBoolean ActiveElement { get; set; }

		/// <summary>
		/// Whether the organization's record is still in active use
		/// </summary>
		/// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
		[NotMapped]
		[IgnoreDataMember]
		public bool? Active
		{
			get { return ActiveElement?.Value; }
			set
			{
				if (value is null)
					ActiveElement = null;
				else
					ActiveElement = new Hl7.Fhir.Model.FhirBoolean(value);
			}
		}

		public override IDeepCopyable CopyTo(IDeepCopyable other)
		{
			var dest = other as Organization;

			if (dest != null)
			{
				base.CopyTo(dest);
				if (Identifier != null) dest.Identifier = new List<Hl7.Fhir.Model.Identifier>(Identifier.DeepCopy());
				if (NameElement != null) dest.NameElement = (Hl7.Fhir.Model.FhirString)NameElement.DeepCopy();
				if (Type != null) dest.Type = (Hl7.Fhir.Model.CodeableConcept)Type.DeepCopy();
				if (Telecom != null) dest.Telecom = new List<Hl7.Fhir.Model.Contact>(Telecom.DeepCopy());
				if (Address != null) dest.Address = new List<Hl7.Fhir.Model.Address>(Address.DeepCopy());
				if (PartOf != null) dest.PartOf = (Hl7.Fhir.Model.ResourceReference)PartOf.DeepCopy();
				if (Contact != null) dest.Contact = new List<Hl7.Fhir.Model.Organization.OrganizationContactComponent>(Contact.DeepCopy());
				if (Location != null) dest.Location = new List<Hl7.Fhir.Model.ResourceReference>(Location.DeepCopy());
				if (ActiveElement != null) dest.ActiveElement = (Hl7.Fhir.Model.FhirBoolean)ActiveElement.DeepCopy();
				return dest;
			}
			else
				throw Error.Argument(nameof(other), "Can only copy to an object of the same type");
		}

		public override IDeepCopyable DeepCopy()
		{
			return CopyTo(new Organization());
		}

		public override bool Matches(IDeepComparable other)
		{
			var otherT = other as Organization;
			if (otherT is null) return false;

			if (!base.Matches(otherT)) return false;
			if (!DeepComparable.Matches(Identifier, otherT.Identifier)) return false;
			if (!DeepComparable.Matches(NameElement, otherT.NameElement)) return false;
			if (!DeepComparable.Matches(Type, otherT.Type)) return false;
			if (!DeepComparable.Matches(Telecom, otherT.Telecom)) return false;
			if (!DeepComparable.Matches(Address, otherT.Address)) return false;
			if (!DeepComparable.Matches(PartOf, otherT.PartOf)) return false;
			if (!DeepComparable.Matches(Contact, otherT.Contact)) return false;
			if (!DeepComparable.Matches(Location, otherT.Location)) return false;
			if (!DeepComparable.Matches(ActiveElement, otherT.ActiveElement)) return false;

			return true;
		}

		public override bool IsExactly(IDeepComparable other)
		{
			var otherT = other as Organization;
			if (otherT is null) return false;

			if (!base.IsExactly(otherT)) return false;
			if (!DeepComparable.IsExactly(Identifier, otherT.Identifier)) return false;
			if (!DeepComparable.IsExactly(NameElement, otherT.NameElement)) return false;
			if (!DeepComparable.IsExactly(Type, otherT.Type)) return false;
			if (!DeepComparable.IsExactly(Telecom, otherT.Telecom)) return false;
			if (!DeepComparable.IsExactly(Address, otherT.Address)) return false;
			if (!DeepComparable.IsExactly(PartOf, otherT.PartOf)) return false;
			if (!DeepComparable.IsExactly(Contact, otherT.Contact)) return false;
			if (!DeepComparable.IsExactly(Location, otherT.Location)) return false;
			if (!DeepComparable.IsExactly(ActiveElement, otherT.ActiveElement)) return false;

			return true;
		}

	}

}
