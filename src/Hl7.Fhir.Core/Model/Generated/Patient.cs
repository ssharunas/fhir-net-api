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
	/// Information about a person or animal receiving health care services
	/// </summary>
	[FhirType("Patient", IsResource = true)]
	[DataContract]
	public partial class Patient : Hl7.Fhir.Model.Resource
	{
		/// <summary>
		/// The type of link between this patient resource and another patient resource.
		/// </summary>
		[FhirEnumeration("LinkType")]
		public enum LinkType
		{
			/// <summary>
			/// The patient resource containing this link must no longer be used. The link points forward to another patient resource that must be used in lieu of the patient resource that contains the link.
			/// </summary>
			[EnumLiteral("replace")]
			Replace,
			/// <summary>
			/// The patient resource containing this link is in use and valid but not considered the main source of information about a patient. The link points forward to another patient resource that should be consulted to retrieve additional patient information.
			/// </summary>
			[EnumLiteral("refer")]
			Refer,
			/// <summary>
			/// The patient resource containing this link is in use and valid, but points to another patient resource that is known to contain data about the same person. Data in this resource might overlap or contradict information found in the other patient resource. This link does not indicate any relative importance of the resources concerned, and both should be regarded as equally valid.
			/// </summary>
			[EnumLiteral("seealso")]
			Seealso,
		}

		[FhirType("ContactComponent")]
		[DataContract]
		public partial class ContactComponent : Hl7.Fhir.Model.Element
		{
			/// <summary>
			/// The kind of relationship
			/// </summary>
			[FhirElement("relationship", InSummary = true, Order = 40)]
			[Cardinality(Min = 0, Max = -1)]
			[DataMember]
			public List<Hl7.Fhir.Model.CodeableConcept> Relationship { get; set; }

			/// <summary>
			/// A name associated with the person
			/// </summary>
			[FhirElement("name", InSummary = true, Order = 50)]
			[DataMember]
			public Hl7.Fhir.Model.HumanName Name { get; set; }

			/// <summary>
			/// A contact detail for the person
			/// </summary>
			[FhirElement("telecom", InSummary = true, Order = 60)]
			[Cardinality(Min = 0, Max = -1)]
			[DataMember]
			public List<Hl7.Fhir.Model.Contact> Telecom { get; set; }

			/// <summary>
			/// Address for the contact person
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

			/// <summary>
			/// Organization that is associated with the contact
			/// </summary>
			[FhirElement("organization", InSummary = true, Order = 90)]
			[References("Organization")]
			[DataMember]
			public Hl7.Fhir.Model.ResourceReference Organization { get; set; }

			public override IDeepCopyable CopyTo(IDeepCopyable other)
			{
				var dest = other as ContactComponent;

				if (dest != null)
				{
					base.CopyTo(dest);
					if (Relationship != null) dest.Relationship = new List<Hl7.Fhir.Model.CodeableConcept>(Relationship.DeepCopy());
					if (Name != null) dest.Name = (Hl7.Fhir.Model.HumanName)Name.DeepCopy();
					if (Telecom != null) dest.Telecom = new List<Hl7.Fhir.Model.Contact>(Telecom.DeepCopy());
					if (Address != null) dest.Address = (Hl7.Fhir.Model.Address)Address.DeepCopy();
					if (Gender != null) dest.Gender = (Hl7.Fhir.Model.CodeableConcept)Gender.DeepCopy();
					if (Organization != null) dest.Organization = (Hl7.Fhir.Model.ResourceReference)Organization.DeepCopy();
					return dest;
				}
				else
					throw Error.Argument(nameof(other), "Can only copy to an object of the same type");
			}

			public override IDeepCopyable DeepCopy()
			{
				return CopyTo(new ContactComponent());
			}

			public override bool Matches(IDeepComparable other)
			{
				var otherT = other as ContactComponent;
				if (otherT is null) return false;

				if (!base.Matches(otherT)) return false;
				if (!DeepComparable.Matches(Relationship, otherT.Relationship)) return false;
				if (!DeepComparable.Matches(Name, otherT.Name)) return false;
				if (!DeepComparable.Matches(Telecom, otherT.Telecom)) return false;
				if (!DeepComparable.Matches(Address, otherT.Address)) return false;
				if (!DeepComparable.Matches(Gender, otherT.Gender)) return false;
				if (!DeepComparable.Matches(Organization, otherT.Organization)) return false;

				return true;
			}

			public override bool IsExactly(IDeepComparable other)
			{
				var otherT = other as ContactComponent;
				if (otherT is null) return false;

				if (!base.IsExactly(otherT)) return false;
				if (!DeepComparable.IsExactly(Relationship, otherT.Relationship)) return false;
				if (!DeepComparable.IsExactly(Name, otherT.Name)) return false;
				if (!DeepComparable.IsExactly(Telecom, otherT.Telecom)) return false;
				if (!DeepComparable.IsExactly(Address, otherT.Address)) return false;
				if (!DeepComparable.IsExactly(Gender, otherT.Gender)) return false;
				if (!DeepComparable.IsExactly(Organization, otherT.Organization)) return false;

				return true;
			}

		}


		[FhirType("AnimalComponent")]
		[DataContract]
		public partial class AnimalComponent : Hl7.Fhir.Model.Element
		{
			/// <summary>
			/// E.g. Dog, Cow
			/// </summary>
			[FhirElement("species", InSummary = true, Order = 40)]
			[Cardinality(Min = 1, Max = 1)]
			[DataMember]
			public Hl7.Fhir.Model.CodeableConcept Species { get; set; }

			/// <summary>
			/// E.g. Poodle, Angus
			/// </summary>
			[FhirElement("breed", InSummary = true, Order = 50)]
			[DataMember]
			public Hl7.Fhir.Model.CodeableConcept Breed { get; set; }

			/// <summary>
			/// E.g. Neutered, Intact
			/// </summary>
			[FhirElement("genderStatus", InSummary = true, Order = 60)]
			[DataMember]
			public Hl7.Fhir.Model.CodeableConcept GenderStatus { get; set; }

			public override IDeepCopyable CopyTo(IDeepCopyable other)
			{
				var dest = other as AnimalComponent;

				if (dest != null)
				{
					base.CopyTo(dest);
					if (Species != null) dest.Species = (Hl7.Fhir.Model.CodeableConcept)Species.DeepCopy();
					if (Breed != null) dest.Breed = (Hl7.Fhir.Model.CodeableConcept)Breed.DeepCopy();
					if (GenderStatus != null) dest.GenderStatus = (Hl7.Fhir.Model.CodeableConcept)GenderStatus.DeepCopy();
					return dest;
				}
				else
					throw Error.Argument(nameof(other), "Can only copy to an object of the same type");
			}

			public override IDeepCopyable DeepCopy()
			{
				return CopyTo(new AnimalComponent());
			}

			public override bool Matches(IDeepComparable other)
			{
				var otherT = other as AnimalComponent;
				if (otherT is null) return false;

				if (!base.Matches(otherT)) return false;
				if (!DeepComparable.Matches(Species, otherT.Species)) return false;
				if (!DeepComparable.Matches(Breed, otherT.Breed)) return false;
				if (!DeepComparable.Matches(GenderStatus, otherT.GenderStatus)) return false;

				return true;
			}

			public override bool IsExactly(IDeepComparable other)
			{
				var otherT = other as AnimalComponent;
				if (otherT is null) return false;

				if (!base.IsExactly(otherT)) return false;
				if (!DeepComparable.IsExactly(Species, otherT.Species)) return false;
				if (!DeepComparable.IsExactly(Breed, otherT.Breed)) return false;
				if (!DeepComparable.IsExactly(GenderStatus, otherT.GenderStatus)) return false;

				return true;
			}

		}


		[FhirType("PatientLinkComponent")]
		[DataContract]
		public partial class PatientLinkComponent : Hl7.Fhir.Model.Element
		{
			/// <summary>
			/// The other patient resource that the link refers to
			/// </summary>
			[FhirElement("other", InSummary = true, Order = 40)]
			[References("Patient")]
			[Cardinality(Min = 1, Max = 1)]
			[DataMember]
			public Hl7.Fhir.Model.ResourceReference Other { get; set; }

			/// <summary>
			/// replace | refer | seealso - type of link
			/// </summary>
			[FhirElement("type", InSummary = true, Order = 50)]
			[Cardinality(Min = 1, Max = 1)]
			[DataMember]
			public Code<Hl7.Fhir.Model.Patient.LinkType> TypeElement { get; set; }

			/// <summary>
			/// replace | refer | seealso - type of link
			/// </summary>
			/// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
			[NotMapped]
			[IgnoreDataMember]
			public Hl7.Fhir.Model.Patient.LinkType? Type
			{
				get { return TypeElement?.Value; }
				set
				{
					if (value is null)
						TypeElement = null;
					else
						TypeElement = new Code<Hl7.Fhir.Model.Patient.LinkType>(value);
				}
			}

			public override IDeepCopyable CopyTo(IDeepCopyable other)
			{
				var dest = other as PatientLinkComponent;

				if (dest != null)
				{
					base.CopyTo(dest);
					if (Other != null) dest.Other = (Hl7.Fhir.Model.ResourceReference)Other.DeepCopy();
					if (TypeElement != null) dest.TypeElement = (Code<Hl7.Fhir.Model.Patient.LinkType>)TypeElement.DeepCopy();
					return dest;
				}
				else
					throw Error.Argument(nameof(other), "Can only copy to an object of the same type");
			}

			public override IDeepCopyable DeepCopy()
			{
				return CopyTo(new PatientLinkComponent());
			}

			public override bool Matches(IDeepComparable other)
			{
				var otherT = other as PatientLinkComponent;
				if (otherT is null) return false;

				if (!base.Matches(otherT)) return false;
				if (!DeepComparable.Matches(Other, otherT.Other)) return false;
				if (!DeepComparable.Matches(TypeElement, otherT.TypeElement)) return false;

				return true;
			}

			public override bool IsExactly(IDeepComparable other)
			{
				var otherT = other as PatientLinkComponent;
				if (otherT is null) return false;

				if (!base.IsExactly(otherT)) return false;
				if (!DeepComparable.IsExactly(Other, otherT.Other)) return false;
				if (!DeepComparable.IsExactly(TypeElement, otherT.TypeElement)) return false;

				return true;
			}

		}


		/// <summary>
		/// An identifier for the person as this patient
		/// </summary>
		[FhirElement("identifier", InSummary = true, Order = 70)]
		[Cardinality(Min = 0, Max = -1)]
		[DataMember]
		public List<Hl7.Fhir.Model.Identifier> Identifier { get; set; }

		/// <summary>
		/// A name associated with the patient
		/// </summary>
		[FhirElement("name", InSummary = true, Order = 80)]
		[Cardinality(Min = 0, Max = -1)]
		[DataMember]
		public List<Hl7.Fhir.Model.HumanName> Name { get; set; }

		/// <summary>
		/// A contact detail for the individual
		/// </summary>
		[FhirElement("telecom", InSummary = true, Order = 90)]
		[Cardinality(Min = 0, Max = -1)]
		[DataMember]
		public List<Hl7.Fhir.Model.Contact> Telecom { get; set; }

		/// <summary>
		/// Gender for administrative purposes
		/// </summary>
		[FhirElement("gender", InSummary = true, Order = 100)]
		[DataMember]
		public Hl7.Fhir.Model.CodeableConcept Gender { get; set; }

		/// <summary>
		/// The date and time of birth for the individual
		/// </summary>
		[FhirElement("birthDate", InSummary = true, Order = 110)]
		[DataMember]
		public Hl7.Fhir.Model.FhirDateTime BirthDateElement { get; set; }

		/// <summary>
		/// The date and time of birth for the individual
		/// </summary>
		/// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
		[NotMapped]
		[IgnoreDataMember]
		public string BirthDate
		{
			get { return BirthDateElement?.Value; }
			set
			{
				if (value is null)
					BirthDateElement = null;
				else
					BirthDateElement = new Hl7.Fhir.Model.FhirDateTime(value);
			}
		}

		/// <summary>
		/// Indicates if the individual is deceased or not
		/// </summary>
		[FhirElement("deceased", InSummary = true, Order = 120, Choice = ChoiceType.DatatypeChoice)]
		[AllowedTypes(typeof(Hl7.Fhir.Model.FhirBoolean), typeof(Hl7.Fhir.Model.FhirDateTime))]
		[DataMember]
		public Hl7.Fhir.Model.Element Deceased { get; set; }

		/// <summary>
		/// Addresses for the individual
		/// </summary>
		[FhirElement("address", InSummary = true, Order = 130)]
		[Cardinality(Min = 0, Max = -1)]
		[DataMember]
		public List<Hl7.Fhir.Model.Address> Address { get; set; }

		/// <summary>
		/// Marital (civil) status of a person
		/// </summary>
		[FhirElement("maritalStatus", InSummary = true, Order = 140)]
		[DataMember]
		public Hl7.Fhir.Model.CodeableConcept MaritalStatus { get; set; }

		/// <summary>
		/// Whether patient is part of a multiple birth
		/// </summary>
		[FhirElement("multipleBirth", InSummary = true, Order = 150, Choice = ChoiceType.DatatypeChoice)]
		[AllowedTypes(typeof(Hl7.Fhir.Model.FhirBoolean), typeof(Hl7.Fhir.Model.Integer))]
		[DataMember]
		public Hl7.Fhir.Model.Element MultipleBirth { get; set; }

		/// <summary>
		/// Image of the person
		/// </summary>
		[FhirElement("photo", Order = 160)]
		[Cardinality(Min = 0, Max = -1)]
		[DataMember]
		public List<Hl7.Fhir.Model.Attachment> Photo { get; set; }

		/// <summary>
		/// A contact party (e.g. guardian, partner, friend) for the patient
		/// </summary>
		[FhirElement("contact", Order = 170)]
		[Cardinality(Min = 0, Max = -1)]
		[DataMember]
		public List<Hl7.Fhir.Model.Patient.ContactComponent> Contact { get; set; }

		/// <summary>
		/// If this patient is an animal (non-human)
		/// </summary>
		[FhirElement("animal", InSummary = true, Order = 180)]
		[DataMember]
		public Hl7.Fhir.Model.Patient.AnimalComponent Animal { get; set; }

		/// <summary>
		/// Languages which may be used to communicate with the patient about his or her health
		/// </summary>
		[FhirElement("communication", Order = 190)]
		[Cardinality(Min = 0, Max = -1)]
		[DataMember]
		public List<Hl7.Fhir.Model.CodeableConcept> Communication { get; set; }

		/// <summary>
		/// Patient's nominated care provider
		/// </summary>
		[FhirElement("careProvider", Order = 200)]
		[References("Organization", "Practitioner")]
		[Cardinality(Min = 0, Max = -1)]
		[DataMember]
		public List<Hl7.Fhir.Model.ResourceReference> CareProvider { get; set; }

		/// <summary>
		/// Organization that is the custodian of the patient record
		/// </summary>
		[FhirElement("managingOrganization", InSummary = true, Order = 210)]
		[References("Organization")]
		[DataMember]
		public Hl7.Fhir.Model.ResourceReference ManagingOrganization { get; set; }

		/// <summary>
		/// Link to another patient resource that concerns the same actual person
		/// </summary>
		[FhirElement("link", InSummary = true, Order = 220)]
		[Cardinality(Min = 0, Max = -1)]
		[DataMember]
		public List<Hl7.Fhir.Model.Patient.PatientLinkComponent> Link { get; set; }

		/// <summary>
		/// Whether this patient's record is in active use
		/// </summary>
		[FhirElement("active", InSummary = true, Order = 230)]
		[DataMember]
		public Hl7.Fhir.Model.FhirBoolean ActiveElement { get; set; }

		/// <summary>
		/// Whether this patient's record is in active use
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
			var dest = other as Patient;

			if (dest != null)
			{
				base.CopyTo(dest);
				if (Identifier != null) dest.Identifier = new List<Hl7.Fhir.Model.Identifier>(Identifier.DeepCopy());
				if (Name != null) dest.Name = new List<Hl7.Fhir.Model.HumanName>(Name.DeepCopy());
				if (Telecom != null) dest.Telecom = new List<Hl7.Fhir.Model.Contact>(Telecom.DeepCopy());
				if (Gender != null) dest.Gender = (Hl7.Fhir.Model.CodeableConcept)Gender.DeepCopy();
				if (BirthDateElement != null) dest.BirthDateElement = (Hl7.Fhir.Model.FhirDateTime)BirthDateElement.DeepCopy();
				if (Deceased != null) dest.Deceased = (Hl7.Fhir.Model.Element)Deceased.DeepCopy();
				if (Address != null) dest.Address = new List<Hl7.Fhir.Model.Address>(Address.DeepCopy());
				if (MaritalStatus != null) dest.MaritalStatus = (Hl7.Fhir.Model.CodeableConcept)MaritalStatus.DeepCopy();
				if (MultipleBirth != null) dest.MultipleBirth = (Hl7.Fhir.Model.Element)MultipleBirth.DeepCopy();
				if (Photo != null) dest.Photo = new List<Hl7.Fhir.Model.Attachment>(Photo.DeepCopy());
				if (Contact != null) dest.Contact = new List<Hl7.Fhir.Model.Patient.ContactComponent>(Contact.DeepCopy());
				if (Animal != null) dest.Animal = (Hl7.Fhir.Model.Patient.AnimalComponent)Animal.DeepCopy();
				if (Communication != null) dest.Communication = new List<Hl7.Fhir.Model.CodeableConcept>(Communication.DeepCopy());
				if (CareProvider != null) dest.CareProvider = new List<Hl7.Fhir.Model.ResourceReference>(CareProvider.DeepCopy());
				if (ManagingOrganization != null) dest.ManagingOrganization = (Hl7.Fhir.Model.ResourceReference)ManagingOrganization.DeepCopy();
				if (Link != null) dest.Link = new List<Hl7.Fhir.Model.Patient.PatientLinkComponent>(Link.DeepCopy());
				if (ActiveElement != null) dest.ActiveElement = (Hl7.Fhir.Model.FhirBoolean)ActiveElement.DeepCopy();
				return dest;
			}
			else
				throw Error.Argument(nameof(other), "Can only copy to an object of the same type");
		}

		public override IDeepCopyable DeepCopy()
		{
			return CopyTo(new Patient());
		}

		public override bool Matches(IDeepComparable other)
		{
			var otherT = other as Patient;
			if (otherT is null) return false;

			if (!base.Matches(otherT)) return false;
			if (!DeepComparable.Matches(Identifier, otherT.Identifier)) return false;
			if (!DeepComparable.Matches(Name, otherT.Name)) return false;
			if (!DeepComparable.Matches(Telecom, otherT.Telecom)) return false;
			if (!DeepComparable.Matches(Gender, otherT.Gender)) return false;
			if (!DeepComparable.Matches(BirthDateElement, otherT.BirthDateElement)) return false;
			if (!DeepComparable.Matches(Deceased, otherT.Deceased)) return false;
			if (!DeepComparable.Matches(Address, otherT.Address)) return false;
			if (!DeepComparable.Matches(MaritalStatus, otherT.MaritalStatus)) return false;
			if (!DeepComparable.Matches(MultipleBirth, otherT.MultipleBirth)) return false;
			if (!DeepComparable.Matches(Photo, otherT.Photo)) return false;
			if (!DeepComparable.Matches(Contact, otherT.Contact)) return false;
			if (!DeepComparable.Matches(Animal, otherT.Animal)) return false;
			if (!DeepComparable.Matches(Communication, otherT.Communication)) return false;
			if (!DeepComparable.Matches(CareProvider, otherT.CareProvider)) return false;
			if (!DeepComparable.Matches(ManagingOrganization, otherT.ManagingOrganization)) return false;
			if (!DeepComparable.Matches(Link, otherT.Link)) return false;
			if (!DeepComparable.Matches(ActiveElement, otherT.ActiveElement)) return false;

			return true;
		}

		public override bool IsExactly(IDeepComparable other)
		{
			var otherT = other as Patient;
			if (otherT is null) return false;

			if (!base.IsExactly(otherT)) return false;
			if (!DeepComparable.IsExactly(Identifier, otherT.Identifier)) return false;
			if (!DeepComparable.IsExactly(Name, otherT.Name)) return false;
			if (!DeepComparable.IsExactly(Telecom, otherT.Telecom)) return false;
			if (!DeepComparable.IsExactly(Gender, otherT.Gender)) return false;
			if (!DeepComparable.IsExactly(BirthDateElement, otherT.BirthDateElement)) return false;
			if (!DeepComparable.IsExactly(Deceased, otherT.Deceased)) return false;
			if (!DeepComparable.IsExactly(Address, otherT.Address)) return false;
			if (!DeepComparable.IsExactly(MaritalStatus, otherT.MaritalStatus)) return false;
			if (!DeepComparable.IsExactly(MultipleBirth, otherT.MultipleBirth)) return false;
			if (!DeepComparable.IsExactly(Photo, otherT.Photo)) return false;
			if (!DeepComparable.IsExactly(Contact, otherT.Contact)) return false;
			if (!DeepComparable.IsExactly(Animal, otherT.Animal)) return false;
			if (!DeepComparable.IsExactly(Communication, otherT.Communication)) return false;
			if (!DeepComparable.IsExactly(CareProvider, otherT.CareProvider)) return false;
			if (!DeepComparable.IsExactly(ManagingOrganization, otherT.ManagingOrganization)) return false;
			if (!DeepComparable.IsExactly(Link, otherT.Link)) return false;
			if (!DeepComparable.IsExactly(ActiveElement, otherT.ActiveElement)) return false;

			return true;
		}

	}

}
