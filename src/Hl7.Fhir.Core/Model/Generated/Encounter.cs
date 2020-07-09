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
	/// An interaction during which services are provided to the patient
	/// </summary>
	[FhirType("Encounter", IsResource = true)]
	[DataContract]
	public partial class Encounter : Hl7.Fhir.Model.Resource
	{
		/// <summary>
		/// Classification of the encounter
		/// </summary>
		[FhirEnumeration("EncounterClass")]
		public enum EncounterClass
		{
			/// <summary>
			/// An encounter during which the patient is hospitalized and stays overnight.
			/// </summary>
			[EnumLiteral("inpatient")]
			Inpatient,
			/// <summary>
			/// An encounter during which the patient is not hospitalized overnight.
			/// </summary>
			[EnumLiteral("outpatient")]
			Outpatient,
			/// <summary>
			/// An encounter where the patient visits the practitioner in his/her office, e.g. a G.P. visit.
			/// </summary>
			[EnumLiteral("ambulatory")]
			Ambulatory,
			/// <summary>
			/// An encounter where the patient needs urgent care.
			/// </summary>
			[EnumLiteral("emergency")]
			Emergency,
			/// <summary>
			/// An encounter where the practitioner visits the patient at his/her home.
			/// </summary>
			[EnumLiteral("home")]
			Home,
			/// <summary>
			/// An encounter taking place outside the regular environment for giving care.
			/// </summary>
			[EnumLiteral("field")]
			Field,
			/// <summary>
			/// An encounter where the patient needs more prolonged treatment or investigations than outpatients, but who do not need to stay in the hospital overnight.
			/// </summary>
			[EnumLiteral("daytime")]
			Daytime,
			/// <summary>
			/// An encounter that takes place where the patient and practitioner do not physically meet but use electronic means for contact.
			/// </summary>
			[EnumLiteral("virtual")]
			Virtual,
		}

		/// <summary>
		/// Current state of the encounter
		/// </summary>
		[FhirEnumeration("EncounterState")]
		public enum EncounterState
		{
			/// <summary>
			/// The Encounter has not yet started.
			/// </summary>
			[EnumLiteral("planned")]
			Planned,
			/// <summary>
			/// The Encounter has begun and the patient is present / the practitioner and the patient are meeting.
			/// </summary>
			[EnumLiteral("in progress")]
			InProgress,
			/// <summary>
			/// The Encounter has begun, but the patient is temporarily on leave.
			/// </summary>
			[EnumLiteral("onleave")]
			Onleave,
			/// <summary>
			/// The Encounter has ended.
			/// </summary>
			[EnumLiteral("finished")]
			Finished,
			/// <summary>
			/// The Encounter has ended before it has begun.
			/// </summary>
			[EnumLiteral("cancelled")]
			Cancelled,
		}

		[FhirType("EncounterHospitalizationComponent")]
		[DataContract]
		public partial class EncounterHospitalizationComponent : Hl7.Fhir.Model.Element
		{
			/// <summary>
			/// Pre-admission identifier
			/// </summary>
			[FhirElement("preAdmissionIdentifier", InSummary = true, Order = 40)]
			[DataMember]
			public Hl7.Fhir.Model.Identifier PreAdmissionIdentifier { get; set; }

			/// <summary>
			/// The location from which the patient came before admission
			/// </summary>
			[FhirElement("origin", InSummary = true, Order = 50)]
			[References("Location")]
			[DataMember]
			public Hl7.Fhir.Model.ResourceReference Origin { get; set; }

			/// <summary>
			/// From where patient was admitted (physician referral, transfer)
			/// </summary>
			[FhirElement("admitSource", InSummary = true, Order = 60)]
			[DataMember]
			public Hl7.Fhir.Model.CodeableConcept AdmitSource { get; set; }

			/// <summary>
			/// Period during which the patient was admitted
			/// </summary>
			[FhirElement("period", InSummary = true, Order = 70)]
			[DataMember]
			public Hl7.Fhir.Model.Period Period { get; set; }

			/// <summary>
			/// Where the patient stays during this encounter
			/// </summary>
			[FhirElement("accomodation", InSummary = true, Order = 80)]
			[Cardinality(Min = 0, Max = -1)]
			[DataMember]
			public List<Hl7.Fhir.Model.Encounter.EncounterHospitalizationAccomodationComponent> Accomodation { get; set; }

			/// <summary>
			/// Dietary restrictions for the patient
			/// </summary>
			[FhirElement("diet", InSummary = true, Order = 90)]
			[DataMember]
			public Hl7.Fhir.Model.CodeableConcept Diet { get; set; }

			/// <summary>
			/// Special courtesies (VIP, board member)
			/// </summary>
			[FhirElement("specialCourtesy", InSummary = true, Order = 100)]
			[Cardinality(Min = 0, Max = -1)]
			[DataMember]
			public List<Hl7.Fhir.Model.CodeableConcept> SpecialCourtesy { get; set; }

			/// <summary>
			/// Wheelchair, translator, stretcher, etc
			/// </summary>
			[FhirElement("specialArrangement", InSummary = true, Order = 110)]
			[Cardinality(Min = 0, Max = -1)]
			[DataMember]
			public List<Hl7.Fhir.Model.CodeableConcept> SpecialArrangement { get; set; }

			/// <summary>
			/// Location to which the patient is discharged
			/// </summary>
			[FhirElement("destination", InSummary = true, Order = 120)]
			[References("Location")]
			[DataMember]
			public Hl7.Fhir.Model.ResourceReference Destination { get; set; }

			/// <summary>
			/// Category or kind of location after discharge
			/// </summary>
			[FhirElement("dischargeDisposition", InSummary = true, Order = 130)]
			[DataMember]
			public Hl7.Fhir.Model.CodeableConcept DischargeDisposition { get; set; }

			/// <summary>
			/// The final diagnosis given a patient before release from the hospital after all testing, surgery, and workup are complete
			/// </summary>
			[FhirElement("dischargeDiagnosis", InSummary = true, Order = 140)]
			[References()]
			[DataMember]
			public Hl7.Fhir.Model.ResourceReference DischargeDiagnosis { get; set; }

			/// <summary>
			/// Is this hospitalization a readmission?
			/// </summary>
			[FhirElement("reAdmission", InSummary = true, Order = 150)]
			[DataMember]
			public Hl7.Fhir.Model.FhirBoolean ReAdmissionElement { get; set; }

			/// <summary>
			/// Is this hospitalization a readmission?
			/// </summary>
			/// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
			[NotMapped]
			[IgnoreDataMember]
			public bool? ReAdmission
			{
				get { return ReAdmissionElement != null ? ReAdmissionElement.Value : null; }
				set
				{
					if (value is null)
						ReAdmissionElement = null;
					else
						ReAdmissionElement = new Hl7.Fhir.Model.FhirBoolean(value);
				}
			}

			public override IDeepCopyable CopyTo(IDeepCopyable other)
			{
				var dest = other as EncounterHospitalizationComponent;

				if (dest != null)
				{
					base.CopyTo(dest);
					if (PreAdmissionIdentifier != null) dest.PreAdmissionIdentifier = (Hl7.Fhir.Model.Identifier)PreAdmissionIdentifier.DeepCopy();
					if (Origin != null) dest.Origin = (Hl7.Fhir.Model.ResourceReference)Origin.DeepCopy();
					if (AdmitSource != null) dest.AdmitSource = (Hl7.Fhir.Model.CodeableConcept)AdmitSource.DeepCopy();
					if (Period != null) dest.Period = (Hl7.Fhir.Model.Period)Period.DeepCopy();
					if (Accomodation != null) dest.Accomodation = new List<Hl7.Fhir.Model.Encounter.EncounterHospitalizationAccomodationComponent>(Accomodation.DeepCopy());
					if (Diet != null) dest.Diet = (Hl7.Fhir.Model.CodeableConcept)Diet.DeepCopy();
					if (SpecialCourtesy != null) dest.SpecialCourtesy = new List<Hl7.Fhir.Model.CodeableConcept>(SpecialCourtesy.DeepCopy());
					if (SpecialArrangement != null) dest.SpecialArrangement = new List<Hl7.Fhir.Model.CodeableConcept>(SpecialArrangement.DeepCopy());
					if (Destination != null) dest.Destination = (Hl7.Fhir.Model.ResourceReference)Destination.DeepCopy();
					if (DischargeDisposition != null) dest.DischargeDisposition = (Hl7.Fhir.Model.CodeableConcept)DischargeDisposition.DeepCopy();
					if (DischargeDiagnosis != null) dest.DischargeDiagnosis = (Hl7.Fhir.Model.ResourceReference)DischargeDiagnosis.DeepCopy();
					if (ReAdmissionElement != null) dest.ReAdmissionElement = (Hl7.Fhir.Model.FhirBoolean)ReAdmissionElement.DeepCopy();
					return dest;
				}
				else
					throw Error.Argument(nameof(other), "Can only copy to an object of the same type");
			}

			public override IDeepCopyable DeepCopy()
			{
				return CopyTo(new EncounterHospitalizationComponent());
			}

			public override bool Matches(IDeepComparable other)
			{
				var otherT = other as EncounterHospitalizationComponent;
				if (otherT is null) return false;

				if (!base.Matches(otherT)) return false;
				if (!DeepComparable.Matches(PreAdmissionIdentifier, otherT.PreAdmissionIdentifier)) return false;
				if (!DeepComparable.Matches(Origin, otherT.Origin)) return false;
				if (!DeepComparable.Matches(AdmitSource, otherT.AdmitSource)) return false;
				if (!DeepComparable.Matches(Period, otherT.Period)) return false;
				if (!DeepComparable.Matches(Accomodation, otherT.Accomodation)) return false;
				if (!DeepComparable.Matches(Diet, otherT.Diet)) return false;
				if (!DeepComparable.Matches(SpecialCourtesy, otherT.SpecialCourtesy)) return false;
				if (!DeepComparable.Matches(SpecialArrangement, otherT.SpecialArrangement)) return false;
				if (!DeepComparable.Matches(Destination, otherT.Destination)) return false;
				if (!DeepComparable.Matches(DischargeDisposition, otherT.DischargeDisposition)) return false;
				if (!DeepComparable.Matches(DischargeDiagnosis, otherT.DischargeDiagnosis)) return false;
				if (!DeepComparable.Matches(ReAdmissionElement, otherT.ReAdmissionElement)) return false;

				return true;
			}

			public override bool IsExactly(IDeepComparable other)
			{
				var otherT = other as EncounterHospitalizationComponent;
				if (otherT is null) return false;

				if (!base.IsExactly(otherT)) return false;
				if (!DeepComparable.IsExactly(PreAdmissionIdentifier, otherT.PreAdmissionIdentifier)) return false;
				if (!DeepComparable.IsExactly(Origin, otherT.Origin)) return false;
				if (!DeepComparable.IsExactly(AdmitSource, otherT.AdmitSource)) return false;
				if (!DeepComparable.IsExactly(Period, otherT.Period)) return false;
				if (!DeepComparable.IsExactly(Accomodation, otherT.Accomodation)) return false;
				if (!DeepComparable.IsExactly(Diet, otherT.Diet)) return false;
				if (!DeepComparable.IsExactly(SpecialCourtesy, otherT.SpecialCourtesy)) return false;
				if (!DeepComparable.IsExactly(SpecialArrangement, otherT.SpecialArrangement)) return false;
				if (!DeepComparable.IsExactly(Destination, otherT.Destination)) return false;
				if (!DeepComparable.IsExactly(DischargeDisposition, otherT.DischargeDisposition)) return false;
				if (!DeepComparable.IsExactly(DischargeDiagnosis, otherT.DischargeDiagnosis)) return false;
				if (!DeepComparable.IsExactly(ReAdmissionElement, otherT.ReAdmissionElement)) return false;

				return true;
			}

		}


		[FhirType("EncounterHospitalizationAccomodationComponent")]
		[DataContract]
		public partial class EncounterHospitalizationAccomodationComponent : Hl7.Fhir.Model.Element
		{
			/// <summary>
			/// The bed that is assigned to the patient
			/// </summary>
			[FhirElement("bed", InSummary = true, Order = 40)]
			[References("Location")]
			[DataMember]
			public Hl7.Fhir.Model.ResourceReference Bed { get; set; }

			/// <summary>
			/// Period during which the patient was assigned the bed
			/// </summary>
			[FhirElement("period", InSummary = true, Order = 50)]
			[DataMember]
			public Hl7.Fhir.Model.Period Period { get; set; }

			public override IDeepCopyable CopyTo(IDeepCopyable other)
			{
				var dest = other as EncounterHospitalizationAccomodationComponent;

				if (dest != null)
				{
					base.CopyTo(dest);
					if (Bed != null) dest.Bed = (Hl7.Fhir.Model.ResourceReference)Bed.DeepCopy();
					if (Period != null) dest.Period = (Hl7.Fhir.Model.Period)Period.DeepCopy();
					return dest;
				}
				else
					throw Error.Argument(nameof(other), "Can only copy to an object of the same type");
			}

			public override IDeepCopyable DeepCopy()
			{
				return CopyTo(new EncounterHospitalizationAccomodationComponent());
			}

			public override bool Matches(IDeepComparable other)
			{
				var otherT = other as EncounterHospitalizationAccomodationComponent;
				if (otherT is null) return false;

				if (!base.Matches(otherT)) return false;
				if (!DeepComparable.Matches(Bed, otherT.Bed)) return false;
				if (!DeepComparable.Matches(Period, otherT.Period)) return false;

				return true;
			}

			public override bool IsExactly(IDeepComparable other)
			{
				var otherT = other as EncounterHospitalizationAccomodationComponent;
				if (otherT is null) return false;

				if (!base.IsExactly(otherT)) return false;
				if (!DeepComparable.IsExactly(Bed, otherT.Bed)) return false;
				if (!DeepComparable.IsExactly(Period, otherT.Period)) return false;

				return true;
			}

		}


		[FhirType("EncounterLocationComponent")]
		[DataContract]
		public partial class EncounterLocationComponent : Hl7.Fhir.Model.Element
		{
			/// <summary>
			/// Location the encounter takes place
			/// </summary>
			[FhirElement("location", InSummary = true, Order = 40)]
			[References("Location")]
			[Cardinality(Min = 1, Max = 1)]
			[DataMember]
			public Hl7.Fhir.Model.ResourceReference Location { get; set; }

			/// <summary>
			/// Time period during which the patient was present at the location
			/// </summary>
			[FhirElement("period", InSummary = true, Order = 50)]
			[Cardinality(Min = 1, Max = 1)]
			[DataMember]
			public Hl7.Fhir.Model.Period Period { get; set; }

			public override IDeepCopyable CopyTo(IDeepCopyable other)
			{
				var dest = other as EncounterLocationComponent;

				if (dest != null)
				{
					base.CopyTo(dest);
					if (Location != null) dest.Location = (Hl7.Fhir.Model.ResourceReference)Location.DeepCopy();
					if (Period != null) dest.Period = (Hl7.Fhir.Model.Period)Period.DeepCopy();
					return dest;
				}
				else
					throw Error.Argument(nameof(other), "Can only copy to an object of the same type");
			}

			public override IDeepCopyable DeepCopy()
			{
				return CopyTo(new EncounterLocationComponent());
			}

			public override bool Matches(IDeepComparable other)
			{
				var otherT = other as EncounterLocationComponent;
				if (otherT is null) return false;

				if (!base.Matches(otherT)) return false;
				if (!DeepComparable.Matches(Location, otherT.Location)) return false;
				if (!DeepComparable.Matches(Period, otherT.Period)) return false;

				return true;
			}

			public override bool IsExactly(IDeepComparable other)
			{
				var otherT = other as EncounterLocationComponent;
				if (otherT is null) return false;

				if (!base.IsExactly(otherT)) return false;
				if (!DeepComparable.IsExactly(Location, otherT.Location)) return false;
				if (!DeepComparable.IsExactly(Period, otherT.Period)) return false;

				return true;
			}

		}


		[FhirType("EncounterParticipantComponent")]
		[DataContract]
		public partial class EncounterParticipantComponent : Hl7.Fhir.Model.Element
		{
			/// <summary>
			/// Role of participant in encounter
			/// </summary>
			[FhirElement("type", InSummary = true, Order = 40)]
			[Cardinality(Min = 0, Max = -1)]
			[DataMember]
			public List<Hl7.Fhir.Model.CodeableConcept> Type { get; set; }

			/// <summary>
			/// Persons involved in the encounter other than the patient
			/// </summary>
			[FhirElement("individual", InSummary = true, Order = 50)]
			[References("Practitioner", "RelatedPerson")]
			[DataMember]
			public Hl7.Fhir.Model.ResourceReference Individual { get; set; }

			public override IDeepCopyable CopyTo(IDeepCopyable other)
			{
				var dest = other as EncounterParticipantComponent;

				if (dest != null)
				{
					base.CopyTo(dest);
					if (Type != null) dest.Type = new List<Hl7.Fhir.Model.CodeableConcept>(Type.DeepCopy());
					if (Individual != null) dest.Individual = (Hl7.Fhir.Model.ResourceReference)Individual.DeepCopy();
					return dest;
				}
				else
					throw Error.Argument(nameof(other), "Can only copy to an object of the same type");
			}

			public override IDeepCopyable DeepCopy()
			{
				return CopyTo(new EncounterParticipantComponent());
			}

			public override bool Matches(IDeepComparable other)
			{
				var otherT = other as EncounterParticipantComponent;
				if (otherT is null) return false;

				if (!base.Matches(otherT)) return false;
				if (!DeepComparable.Matches(Type, otherT.Type)) return false;
				if (!DeepComparable.Matches(Individual, otherT.Individual)) return false;

				return true;
			}

			public override bool IsExactly(IDeepComparable other)
			{
				var otherT = other as EncounterParticipantComponent;
				if (otherT is null) return false;

				if (!base.IsExactly(otherT)) return false;
				if (!DeepComparable.IsExactly(Type, otherT.Type)) return false;
				if (!DeepComparable.IsExactly(Individual, otherT.Individual)) return false;

				return true;
			}

		}


		/// <summary>
		/// Identifier(s) by which this encounter is known
		/// </summary>
		[FhirElement("identifier", InSummary = true, Order = 70)]
		[Cardinality(Min = 0, Max = -1)]
		[DataMember]
		public List<Hl7.Fhir.Model.Identifier> Identifier { get; set; }

		/// <summary>
		/// planned | in progress | onleave | finished | cancelled
		/// </summary>
		[FhirElement("status", InSummary = true, Order = 80)]
		[Cardinality(Min = 1, Max = 1)]
		[DataMember]
		public Code<Hl7.Fhir.Model.Encounter.EncounterState> StatusElement { get; set; }

		/// <summary>
		/// planned | in progress | onleave | finished | cancelled
		/// </summary>
		/// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
		[NotMapped]
		[IgnoreDataMember]
		public Hl7.Fhir.Model.Encounter.EncounterState? Status
		{
			get { return StatusElement != null ? StatusElement.Value : null; }
			set
			{
				if (value is null)
					StatusElement = null;
				else
					StatusElement = new Code<Hl7.Fhir.Model.Encounter.EncounterState>(value);
			}
		}

		/// <summary>
		/// inpatient | outpatient | ambulatory | emergency +
		/// </summary>
		[FhirElement("class", InSummary = true, Order = 90)]
		[Cardinality(Min = 1, Max = 1)]
		[DataMember]
		public Code<Hl7.Fhir.Model.Encounter.EncounterClass> ClassElement { get; set; }

		/// <summary>
		/// inpatient | outpatient | ambulatory | emergency +
		/// </summary>
		/// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
		[NotMapped]
		[IgnoreDataMember]
		public Hl7.Fhir.Model.Encounter.EncounterClass? Class
		{
			get { return ClassElement != null ? ClassElement.Value : null; }
			set
			{
				if (value is null)
					ClassElement = null;
				else
					ClassElement = new Code<Hl7.Fhir.Model.Encounter.EncounterClass>(value);
			}
		}

		/// <summary>
		/// Specific type of encounter
		/// </summary>
		[FhirElement("type", InSummary = true, Order = 100)]
		[Cardinality(Min = 0, Max = -1)]
		[DataMember]
		public List<Hl7.Fhir.Model.CodeableConcept> Type { get; set; }

		/// <summary>
		/// The patient present at the encounter
		/// </summary>
		[FhirElement("subject", InSummary = true, Order = 110)]
		[References("Patient")]
		[DataMember]
		public Hl7.Fhir.Model.ResourceReference Subject { get; set; }

		/// <summary>
		/// List of participants involved in the encounter
		/// </summary>
		[FhirElement("participant", InSummary = true, Order = 120)]
		[Cardinality(Min = 0, Max = -1)]
		[DataMember]
		public List<Hl7.Fhir.Model.Encounter.EncounterParticipantComponent> Participant { get; set; }

		/// <summary>
		/// The start and end time of the encounter
		/// </summary>
		[FhirElement("period", Order = 130)]
		[DataMember]
		public Hl7.Fhir.Model.Period Period { get; set; }

		/// <summary>
		/// Quantity of time the encounter lasted
		/// </summary>
		[FhirElement("length", Order = 140)]
		[DataMember]
		public Hl7.Fhir.Model.Duration Length { get; set; }

		/// <summary>
		/// Reason the encounter takes place (code)
		/// </summary>
		[FhirElement("reason", InSummary = true, Order = 150)]
		[DataMember]
		public Hl7.Fhir.Model.CodeableConcept Reason { get; set; }

		/// <summary>
		/// Reason the encounter takes place (resource)
		/// </summary>
		[FhirElement("indication", Order = 160)]
		[References()]
		[DataMember]
		public Hl7.Fhir.Model.ResourceReference Indication { get; set; }

		/// <summary>
		/// Indicates the urgency of the encounter
		/// </summary>
		[FhirElement("priority", Order = 170)]
		[DataMember]
		public Hl7.Fhir.Model.CodeableConcept Priority { get; set; }

		/// <summary>
		/// Details about an admission to a clinic
		/// </summary>
		[FhirElement("hospitalization", Order = 180)]
		[DataMember]
		public Hl7.Fhir.Model.Encounter.EncounterHospitalizationComponent Hospitalization { get; set; }

		/// <summary>
		/// List of locations the patient has been at
		/// </summary>
		[FhirElement("location", Order = 190)]
		[Cardinality(Min = 0, Max = -1)]
		[DataMember]
		public List<Hl7.Fhir.Model.Encounter.EncounterLocationComponent> Location { get; set; }

		/// <summary>
		/// Department or team providing care
		/// </summary>
		[FhirElement("serviceProvider", Order = 200)]
		[References("Organization")]
		[DataMember]
		public Hl7.Fhir.Model.ResourceReference ServiceProvider { get; set; }

		/// <summary>
		/// Another Encounter this encounter is part of
		/// </summary>
		[FhirElement("partOf", Order = 210)]
		[References("Encounter")]
		[DataMember]
		public Hl7.Fhir.Model.ResourceReference PartOf { get; set; }

		public override IDeepCopyable CopyTo(IDeepCopyable other)
		{
			var dest = other as Encounter;

			if (dest != null)
			{
				base.CopyTo(dest);
				if (Identifier != null) dest.Identifier = new List<Hl7.Fhir.Model.Identifier>(Identifier.DeepCopy());
				if (StatusElement != null) dest.StatusElement = (Code<Hl7.Fhir.Model.Encounter.EncounterState>)StatusElement.DeepCopy();
				if (ClassElement != null) dest.ClassElement = (Code<Hl7.Fhir.Model.Encounter.EncounterClass>)ClassElement.DeepCopy();
				if (Type != null) dest.Type = new List<Hl7.Fhir.Model.CodeableConcept>(Type.DeepCopy());
				if (Subject != null) dest.Subject = (Hl7.Fhir.Model.ResourceReference)Subject.DeepCopy();
				if (Participant != null) dest.Participant = new List<Hl7.Fhir.Model.Encounter.EncounterParticipantComponent>(Participant.DeepCopy());
				if (Period != null) dest.Period = (Hl7.Fhir.Model.Period)Period.DeepCopy();
				if (Length != null) dest.Length = (Hl7.Fhir.Model.Duration)Length.DeepCopy();
				if (Reason != null) dest.Reason = (Hl7.Fhir.Model.CodeableConcept)Reason.DeepCopy();
				if (Indication != null) dest.Indication = (Hl7.Fhir.Model.ResourceReference)Indication.DeepCopy();
				if (Priority != null) dest.Priority = (Hl7.Fhir.Model.CodeableConcept)Priority.DeepCopy();
				if (Hospitalization != null) dest.Hospitalization = (Hl7.Fhir.Model.Encounter.EncounterHospitalizationComponent)Hospitalization.DeepCopy();
				if (Location != null) dest.Location = new List<Hl7.Fhir.Model.Encounter.EncounterLocationComponent>(Location.DeepCopy());
				if (ServiceProvider != null) dest.ServiceProvider = (Hl7.Fhir.Model.ResourceReference)ServiceProvider.DeepCopy();
				if (PartOf != null) dest.PartOf = (Hl7.Fhir.Model.ResourceReference)PartOf.DeepCopy();
				return dest;
			}
			else
				throw Error.Argument(nameof(other), "Can only copy to an object of the same type");
		}

		public override IDeepCopyable DeepCopy()
		{
			return CopyTo(new Encounter());
		}

		public override bool Matches(IDeepComparable other)
		{
			var otherT = other as Encounter;
			if (otherT is null) return false;

			if (!base.Matches(otherT)) return false;
			if (!DeepComparable.Matches(Identifier, otherT.Identifier)) return false;
			if (!DeepComparable.Matches(StatusElement, otherT.StatusElement)) return false;
			if (!DeepComparable.Matches(ClassElement, otherT.ClassElement)) return false;
			if (!DeepComparable.Matches(Type, otherT.Type)) return false;
			if (!DeepComparable.Matches(Subject, otherT.Subject)) return false;
			if (!DeepComparable.Matches(Participant, otherT.Participant)) return false;
			if (!DeepComparable.Matches(Period, otherT.Period)) return false;
			if (!DeepComparable.Matches(Length, otherT.Length)) return false;
			if (!DeepComparable.Matches(Reason, otherT.Reason)) return false;
			if (!DeepComparable.Matches(Indication, otherT.Indication)) return false;
			if (!DeepComparable.Matches(Priority, otherT.Priority)) return false;
			if (!DeepComparable.Matches(Hospitalization, otherT.Hospitalization)) return false;
			if (!DeepComparable.Matches(Location, otherT.Location)) return false;
			if (!DeepComparable.Matches(ServiceProvider, otherT.ServiceProvider)) return false;
			if (!DeepComparable.Matches(PartOf, otherT.PartOf)) return false;

			return true;
		}

		public override bool IsExactly(IDeepComparable other)
		{
			var otherT = other as Encounter;
			if (otherT is null) return false;

			if (!base.IsExactly(otherT)) return false;
			if (!DeepComparable.IsExactly(Identifier, otherT.Identifier)) return false;
			if (!DeepComparable.IsExactly(StatusElement, otherT.StatusElement)) return false;
			if (!DeepComparable.IsExactly(ClassElement, otherT.ClassElement)) return false;
			if (!DeepComparable.IsExactly(Type, otherT.Type)) return false;
			if (!DeepComparable.IsExactly(Subject, otherT.Subject)) return false;
			if (!DeepComparable.IsExactly(Participant, otherT.Participant)) return false;
			if (!DeepComparable.IsExactly(Period, otherT.Period)) return false;
			if (!DeepComparable.IsExactly(Length, otherT.Length)) return false;
			if (!DeepComparable.IsExactly(Reason, otherT.Reason)) return false;
			if (!DeepComparable.IsExactly(Indication, otherT.Indication)) return false;
			if (!DeepComparable.IsExactly(Priority, otherT.Priority)) return false;
			if (!DeepComparable.IsExactly(Hospitalization, otherT.Hospitalization)) return false;
			if (!DeepComparable.IsExactly(Location, otherT.Location)) return false;
			if (!DeepComparable.IsExactly(ServiceProvider, otherT.ServiceProvider)) return false;
			if (!DeepComparable.IsExactly(PartOf, otherT.PartOf)) return false;

			return true;
		}

	}

}
