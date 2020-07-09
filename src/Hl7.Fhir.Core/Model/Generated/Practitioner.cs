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
	/// A person with a  formal responsibility in the provisioning of healthcare or related services
	/// </summary>
	[FhirType("Practitioner", IsResource = true)]
	[DataContract]
	public partial class Practitioner : Hl7.Fhir.Model.Resource
	{
		[FhirType("PractitionerQualificationComponent")]
		[DataContract]
		public partial class PractitionerQualificationComponent : Hl7.Fhir.Model.Element
		{
			/// <summary>
			/// Coded representation of the qualification
			/// </summary>
			[FhirElement("code", InSummary = true, Order = 40)]
			[Cardinality(Min = 1, Max = 1)]
			[DataMember]
			public Hl7.Fhir.Model.CodeableConcept Code { get; set; }

			/// <summary>
			/// Period during which the qualification is valid
			/// </summary>
			[FhirElement("period", InSummary = true, Order = 50)]
			[DataMember]
			public Hl7.Fhir.Model.Period Period { get; set; }

			/// <summary>
			/// Organization that regulates and issues the qualification
			/// </summary>
			[FhirElement("issuer", InSummary = true, Order = 60)]
			[References("Organization")]
			[DataMember]
			public Hl7.Fhir.Model.ResourceReference Issuer { get; set; }

			public override IDeepCopyable CopyTo(IDeepCopyable other)
			{
				var dest = other as PractitionerQualificationComponent;

				if (dest != null)
				{
					base.CopyTo(dest);
					if (Code != null) dest.Code = (Hl7.Fhir.Model.CodeableConcept)Code.DeepCopy();
					if (Period != null) dest.Period = (Hl7.Fhir.Model.Period)Period.DeepCopy();
					if (Issuer != null) dest.Issuer = (Hl7.Fhir.Model.ResourceReference)Issuer.DeepCopy();
					return dest;
				}
				else
					throw Error.Argument(nameof(other), "Can only copy to an object of the same type");
			}

			public override IDeepCopyable DeepCopy()
			{
				return CopyTo(new PractitionerQualificationComponent());
			}

			public override bool Matches(IDeepComparable other)
			{
				var otherT = other as PractitionerQualificationComponent;
				if (otherT is null) return false;

				if (!base.Matches(otherT)) return false;
				if (!DeepComparable.Matches(Code, otherT.Code)) return false;
				if (!DeepComparable.Matches(Period, otherT.Period)) return false;
				if (!DeepComparable.Matches(Issuer, otherT.Issuer)) return false;

				return true;
			}

			public override bool IsExactly(IDeepComparable other)
			{
				var otherT = other as PractitionerQualificationComponent;
				if (otherT is null) return false;

				if (!base.IsExactly(otherT)) return false;
				if (!DeepComparable.IsExactly(Code, otherT.Code)) return false;
				if (!DeepComparable.IsExactly(Period, otherT.Period)) return false;
				if (!DeepComparable.IsExactly(Issuer, otherT.Issuer)) return false;

				return true;
			}

		}


		/// <summary>
		/// A identifier for the person as this agent
		/// </summary>
		[FhirElement("identifier", InSummary = true, Order = 70)]
		[Cardinality(Min = 0, Max = -1)]
		[DataMember]
		public List<Hl7.Fhir.Model.Identifier> Identifier { get; set; }

		/// <summary>
		/// A name associated with the person
		/// </summary>
		[FhirElement("name", InSummary = true, Order = 80)]
		[DataMember]
		public Hl7.Fhir.Model.HumanName Name { get; set; }

		/// <summary>
		/// A contact detail for the practitioner
		/// </summary>
		[FhirElement("telecom", InSummary = true, Order = 90)]
		[Cardinality(Min = 0, Max = -1)]
		[DataMember]
		public List<Hl7.Fhir.Model.Contact> Telecom { get; set; }

		/// <summary>
		/// Where practitioner can be found/visited
		/// </summary>
		[FhirElement("address", InSummary = true, Order = 100)]
		[DataMember]
		public Hl7.Fhir.Model.Address Address { get; set; }

		/// <summary>
		/// Gender for administrative purposes
		/// </summary>
		[FhirElement("gender", InSummary = true, Order = 110)]
		[DataMember]
		public Hl7.Fhir.Model.CodeableConcept Gender { get; set; }

		/// <summary>
		/// The date and time of birth for the practitioner
		/// </summary>
		[FhirElement("birthDate", InSummary = true, Order = 120)]
		[DataMember]
		public Hl7.Fhir.Model.FhirDateTime BirthDateElement { get; set; }

		/// <summary>
		/// The date and time of birth for the practitioner
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
		/// Image of the person
		/// </summary>
		[FhirElement("photo", Order = 130)]
		[Cardinality(Min = 0, Max = -1)]
		[DataMember]
		public List<Hl7.Fhir.Model.Attachment> Photo { get; set; }

		/// <summary>
		/// The represented organization
		/// </summary>
		[FhirElement("organization", InSummary = true, Order = 140)]
		[References("Organization")]
		[DataMember]
		public Hl7.Fhir.Model.ResourceReference Organization { get; set; }

		/// <summary>
		/// Roles which this practitioner may perform
		/// </summary>
		[FhirElement("role", InSummary = true, Order = 150)]
		[Cardinality(Min = 0, Max = -1)]
		[DataMember]
		public List<Hl7.Fhir.Model.CodeableConcept> Role { get; set; }

		/// <summary>
		/// Specific specialty of the practitioner
		/// </summary>
		[FhirElement("specialty", InSummary = true, Order = 160)]
		[Cardinality(Min = 0, Max = -1)]
		[DataMember]
		public List<Hl7.Fhir.Model.CodeableConcept> Specialty { get; set; }

		/// <summary>
		/// The period during which the practitioner is authorized to perform in these role(s)
		/// </summary>
		[FhirElement("period", InSummary = true, Order = 170)]
		[DataMember]
		public Hl7.Fhir.Model.Period Period { get; set; }

		/// <summary>
		/// The location(s) at which this practitioner provides care
		/// </summary>
		[FhirElement("location", Order = 180)]
		[References("Location")]
		[Cardinality(Min = 0, Max = -1)]
		[DataMember]
		public List<Hl7.Fhir.Model.ResourceReference> Location { get; set; }

		/// <summary>
		/// Qualifications obtained by training and certification
		/// </summary>
		[FhirElement("qualification", Order = 190)]
		[Cardinality(Min = 0, Max = -1)]
		[DataMember]
		public List<Hl7.Fhir.Model.Practitioner.PractitionerQualificationComponent> Qualification { get; set; }

		/// <summary>
		/// A language the practitioner is able to use in patient communication
		/// </summary>
		[FhirElement("communication", Order = 200)]
		[Cardinality(Min = 0, Max = -1)]
		[DataMember]
		public List<Hl7.Fhir.Model.CodeableConcept> Communication { get; set; }

		public override IDeepCopyable CopyTo(IDeepCopyable other)
		{
			var dest = other as Practitioner;

			if (dest != null)
			{
				base.CopyTo(dest);
				if (Identifier != null) dest.Identifier = new List<Hl7.Fhir.Model.Identifier>(Identifier.DeepCopy());
				if (Name != null) dest.Name = (Hl7.Fhir.Model.HumanName)Name.DeepCopy();
				if (Telecom != null) dest.Telecom = new List<Hl7.Fhir.Model.Contact>(Telecom.DeepCopy());
				if (Address != null) dest.Address = (Hl7.Fhir.Model.Address)Address.DeepCopy();
				if (Gender != null) dest.Gender = (Hl7.Fhir.Model.CodeableConcept)Gender.DeepCopy();
				if (BirthDateElement != null) dest.BirthDateElement = (Hl7.Fhir.Model.FhirDateTime)BirthDateElement.DeepCopy();
				if (Photo != null) dest.Photo = new List<Hl7.Fhir.Model.Attachment>(Photo.DeepCopy());
				if (Organization != null) dest.Organization = (Hl7.Fhir.Model.ResourceReference)Organization.DeepCopy();
				if (Role != null) dest.Role = new List<Hl7.Fhir.Model.CodeableConcept>(Role.DeepCopy());
				if (Specialty != null) dest.Specialty = new List<Hl7.Fhir.Model.CodeableConcept>(Specialty.DeepCopy());
				if (Period != null) dest.Period = (Hl7.Fhir.Model.Period)Period.DeepCopy();
				if (Location != null) dest.Location = new List<Hl7.Fhir.Model.ResourceReference>(Location.DeepCopy());
				if (Qualification != null) dest.Qualification = new List<Hl7.Fhir.Model.Practitioner.PractitionerQualificationComponent>(Qualification.DeepCopy());
				if (Communication != null) dest.Communication = new List<Hl7.Fhir.Model.CodeableConcept>(Communication.DeepCopy());
				return dest;
			}
			else
				throw Error.Argument(nameof(other), "Can only copy to an object of the same type");
		}

		public override IDeepCopyable DeepCopy()
		{
			return CopyTo(new Practitioner());
		}

		public override bool Matches(IDeepComparable other)
		{
			var otherT = other as Practitioner;
			if (otherT is null) return false;

			if (!base.Matches(otherT)) return false;
			if (!DeepComparable.Matches(Identifier, otherT.Identifier)) return false;
			if (!DeepComparable.Matches(Name, otherT.Name)) return false;
			if (!DeepComparable.Matches(Telecom, otherT.Telecom)) return false;
			if (!DeepComparable.Matches(Address, otherT.Address)) return false;
			if (!DeepComparable.Matches(Gender, otherT.Gender)) return false;
			if (!DeepComparable.Matches(BirthDateElement, otherT.BirthDateElement)) return false;
			if (!DeepComparable.Matches(Photo, otherT.Photo)) return false;
			if (!DeepComparable.Matches(Organization, otherT.Organization)) return false;
			if (!DeepComparable.Matches(Role, otherT.Role)) return false;
			if (!DeepComparable.Matches(Specialty, otherT.Specialty)) return false;
			if (!DeepComparable.Matches(Period, otherT.Period)) return false;
			if (!DeepComparable.Matches(Location, otherT.Location)) return false;
			if (!DeepComparable.Matches(Qualification, otherT.Qualification)) return false;
			if (!DeepComparable.Matches(Communication, otherT.Communication)) return false;

			return true;
		}

		public override bool IsExactly(IDeepComparable other)
		{
			var otherT = other as Practitioner;
			if (otherT is null) return false;

			if (!base.IsExactly(otherT)) return false;
			if (!DeepComparable.IsExactly(Identifier, otherT.Identifier)) return false;
			if (!DeepComparable.IsExactly(Name, otherT.Name)) return false;
			if (!DeepComparable.IsExactly(Telecom, otherT.Telecom)) return false;
			if (!DeepComparable.IsExactly(Address, otherT.Address)) return false;
			if (!DeepComparable.IsExactly(Gender, otherT.Gender)) return false;
			if (!DeepComparable.IsExactly(BirthDateElement, otherT.BirthDateElement)) return false;
			if (!DeepComparable.IsExactly(Photo, otherT.Photo)) return false;
			if (!DeepComparable.IsExactly(Organization, otherT.Organization)) return false;
			if (!DeepComparable.IsExactly(Role, otherT.Role)) return false;
			if (!DeepComparable.IsExactly(Specialty, otherT.Specialty)) return false;
			if (!DeepComparable.IsExactly(Period, otherT.Period)) return false;
			if (!DeepComparable.IsExactly(Location, otherT.Location)) return false;
			if (!DeepComparable.IsExactly(Qualification, otherT.Qualification)) return false;
			if (!DeepComparable.IsExactly(Communication, otherT.Communication)) return false;

			return true;
		}

	}

}
