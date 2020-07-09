﻿using Hl7.Fhir.Introspection;
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
	/// Detailed information about conditions, problems or diagnoses
	/// </summary>
	[FhirType("Condition", IsResource = true)]
	[DataContract]
	public partial class Condition : Hl7.Fhir.Model.Resource
	{
		/// <summary>
		/// The clinical status of the Condition or diagnosis
		/// </summary>
		[FhirEnumeration("ConditionStatus")]
		public enum ConditionStatus
		{
			/// <summary>
			/// This is a tentative diagnosis - still a candidate that is under consideration.
			/// </summary>
			[EnumLiteral("provisional")]
			Provisional,
			/// <summary>
			/// The patient is being treated on the basis that this is the condition, but it is still not confirmed.
			/// </summary>
			[EnumLiteral("working")]
			Working,
			/// <summary>
			/// There is sufficient diagnostic and/or clinical evidence to treat this as a confirmed condition.
			/// </summary>
			[EnumLiteral("confirmed")]
			Confirmed,
			/// <summary>
			/// This condition has been ruled out by diagnostic and clinical evidence.
			/// </summary>
			[EnumLiteral("refuted")]
			Refuted,
		}

		/// <summary>
		/// The type of relationship between a condition and its related item
		/// </summary>
		[FhirEnumeration("ConditionRelationshipType")]
		public enum ConditionRelationshipType
		{
			/// <summary>
			/// this condition follows the identified condition/procedure/substance and is a consequence of it.
			/// </summary>
			[EnumLiteral("due-to")]
			DueTo,
			/// <summary>
			/// this condition follows the identified condition/procedure/substance, but it is not known whether they are causually linked.
			/// </summary>
			[EnumLiteral("following")]
			Following,
		}

		[FhirType("ConditionRelatedItemComponent")]
		[DataContract]
		public partial class ConditionRelatedItemComponent : Hl7.Fhir.Model.Element
		{
			/// <summary>
			/// due-to | following
			/// </summary>
			[FhirElement("type", InSummary = true, Order = 40)]
			[Cardinality(Min = 1, Max = 1)]
			[DataMember]
			public Code<Hl7.Fhir.Model.Condition.ConditionRelationshipType> TypeElement { get; set; }

			/// <summary>
			/// due-to | following
			/// </summary>
			/// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
			[NotMapped]
			[IgnoreDataMember]
			public Hl7.Fhir.Model.Condition.ConditionRelationshipType? Type
			{
				get { return TypeElement != null ? TypeElement.Value : null; }
				set
				{
					if (value is null)
						TypeElement = null;
					else
						TypeElement = new Code<Hl7.Fhir.Model.Condition.ConditionRelationshipType>(value);
				}
			}

			/// <summary>
			/// Relationship target by means of a predefined code
			/// </summary>
			[FhirElement("code", InSummary = true, Order = 50)]
			[DataMember]
			public Hl7.Fhir.Model.CodeableConcept Code { get; set; }

			/// <summary>
			/// Relationship target resource
			/// </summary>
			[FhirElement("target", InSummary = true, Order = 60)]
			[References("Condition", "Procedure", "MedicationAdministration", "Immunization", "MedicationStatement")]
			[DataMember]
			public Hl7.Fhir.Model.ResourceReference Target { get; set; }

			public override IDeepCopyable CopyTo(IDeepCopyable other)
			{
				var dest = other as ConditionRelatedItemComponent;

				if (dest != null)
				{
					base.CopyTo(dest);
					if (TypeElement != null) dest.TypeElement = (Code<Hl7.Fhir.Model.Condition.ConditionRelationshipType>)TypeElement.DeepCopy();
					if (Code != null) dest.Code = (Hl7.Fhir.Model.CodeableConcept)Code.DeepCopy();
					if (Target != null) dest.Target = (Hl7.Fhir.Model.ResourceReference)Target.DeepCopy();
					return dest;
				}
				else
					throw Error.Argument(nameof(other), "Can only copy to an object of the same type");
			}

			public override IDeepCopyable DeepCopy()
			{
				return CopyTo(new ConditionRelatedItemComponent());
			}

			public override bool Matches(IDeepComparable other)
			{
				var otherT = other as ConditionRelatedItemComponent;
				if (otherT is null) return false;

				if (!base.Matches(otherT)) return false;
				if (!DeepComparable.Matches(TypeElement, otherT.TypeElement)) return false;
				if (!DeepComparable.Matches(Code, otherT.Code)) return false;
				if (!DeepComparable.Matches(Target, otherT.Target)) return false;

				return true;
			}

			public override bool IsExactly(IDeepComparable other)
			{
				var otherT = other as ConditionRelatedItemComponent;
				if (otherT is null) return false;

				if (!base.IsExactly(otherT)) return false;
				if (!DeepComparable.IsExactly(TypeElement, otherT.TypeElement)) return false;
				if (!DeepComparable.IsExactly(Code, otherT.Code)) return false;
				if (!DeepComparable.IsExactly(Target, otherT.Target)) return false;

				return true;
			}

		}


		[FhirType("ConditionEvidenceComponent")]
		[DataContract]
		public partial class ConditionEvidenceComponent : Hl7.Fhir.Model.Element
		{
			/// <summary>
			/// Manifestation/symptom
			/// </summary>
			[FhirElement("code", InSummary = true, Order = 40)]
			[DataMember]
			public Hl7.Fhir.Model.CodeableConcept Code { get; set; }

			/// <summary>
			/// Supporting information found elsewhere
			/// </summary>
			[FhirElement("detail", InSummary = true, Order = 50)]
			[References()]
			[Cardinality(Min = 0, Max = -1)]
			[DataMember]
			public List<Hl7.Fhir.Model.ResourceReference> Detail { get; set; }

			public override IDeepCopyable CopyTo(IDeepCopyable other)
			{
				var dest = other as ConditionEvidenceComponent;

				if (dest != null)
				{
					base.CopyTo(dest);
					if (Code != null) dest.Code = (Hl7.Fhir.Model.CodeableConcept)Code.DeepCopy();
					if (Detail != null) dest.Detail = new List<Hl7.Fhir.Model.ResourceReference>(Detail.DeepCopy());
					return dest;
				}
				else
					throw Error.Argument(nameof(other), "Can only copy to an object of the same type");
			}

			public override IDeepCopyable DeepCopy()
			{
				return CopyTo(new ConditionEvidenceComponent());
			}

			public override bool Matches(IDeepComparable other)
			{
				var otherT = other as ConditionEvidenceComponent;
				if (otherT is null) return false;

				if (!base.Matches(otherT)) return false;
				if (!DeepComparable.Matches(Code, otherT.Code)) return false;
				if (!DeepComparable.Matches(Detail, otherT.Detail)) return false;

				return true;
			}

			public override bool IsExactly(IDeepComparable other)
			{
				var otherT = other as ConditionEvidenceComponent;
				if (otherT is null) return false;

				if (!base.IsExactly(otherT)) return false;
				if (!DeepComparable.IsExactly(Code, otherT.Code)) return false;
				if (!DeepComparable.IsExactly(Detail, otherT.Detail)) return false;

				return true;
			}

		}


		[FhirType("ConditionStageComponent")]
		[DataContract]
		public partial class ConditionStageComponent : Hl7.Fhir.Model.Element
		{
			/// <summary>
			/// Simple summary (disease specific)
			/// </summary>
			[FhirElement("summary", InSummary = true, Order = 40)]
			[DataMember]
			public Hl7.Fhir.Model.CodeableConcept Summary { get; set; }

			/// <summary>
			/// Formal record of assessment
			/// </summary>
			[FhirElement("assessment", InSummary = true, Order = 50)]
			[References()]
			[Cardinality(Min = 0, Max = -1)]
			[DataMember]
			public List<Hl7.Fhir.Model.ResourceReference> Assessment { get; set; }

			public override IDeepCopyable CopyTo(IDeepCopyable other)
			{
				var dest = other as ConditionStageComponent;

				if (dest != null)
				{
					base.CopyTo(dest);
					if (Summary != null) dest.Summary = (Hl7.Fhir.Model.CodeableConcept)Summary.DeepCopy();
					if (Assessment != null) dest.Assessment = new List<Hl7.Fhir.Model.ResourceReference>(Assessment.DeepCopy());
					return dest;
				}
				else
					throw Error.Argument(nameof(other), "Can only copy to an object of the same type");
			}

			public override IDeepCopyable DeepCopy()
			{
				return CopyTo(new ConditionStageComponent());
			}

			public override bool Matches(IDeepComparable other)
			{
				var otherT = other as ConditionStageComponent;
				if (otherT is null) return false;

				if (!base.Matches(otherT)) return false;
				if (!DeepComparable.Matches(Summary, otherT.Summary)) return false;
				if (!DeepComparable.Matches(Assessment, otherT.Assessment)) return false;

				return true;
			}

			public override bool IsExactly(IDeepComparable other)
			{
				var otherT = other as ConditionStageComponent;
				if (otherT is null) return false;

				if (!base.IsExactly(otherT)) return false;
				if (!DeepComparable.IsExactly(Summary, otherT.Summary)) return false;
				if (!DeepComparable.IsExactly(Assessment, otherT.Assessment)) return false;

				return true;
			}

		}


		[FhirType("ConditionLocationComponent")]
		[DataContract]
		public partial class ConditionLocationComponent : Hl7.Fhir.Model.Element
		{
			/// <summary>
			/// Location - may include laterality
			/// </summary>
			[FhirElement("code", InSummary = true, Order = 40)]
			[DataMember]
			public Hl7.Fhir.Model.CodeableConcept Code { get; set; }

			/// <summary>
			/// Precise location details
			/// </summary>
			[FhirElement("detail", InSummary = true, Order = 50)]
			[DataMember]
			public Hl7.Fhir.Model.FhirString DetailElement { get; set; }

			/// <summary>
			/// Precise location details
			/// </summary>
			/// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
			[NotMapped]
			[IgnoreDataMember]
			public string Detail
			{
				get { return DetailElement != null ? DetailElement.Value : null; }
				set
				{
					if (value is null)
						DetailElement = null;
					else
						DetailElement = new Hl7.Fhir.Model.FhirString(value);
				}
			}

			public override IDeepCopyable CopyTo(IDeepCopyable other)
			{
				var dest = other as ConditionLocationComponent;

				if (dest != null)
				{
					base.CopyTo(dest);
					if (Code != null) dest.Code = (Hl7.Fhir.Model.CodeableConcept)Code.DeepCopy();
					if (DetailElement != null) dest.DetailElement = (Hl7.Fhir.Model.FhirString)DetailElement.DeepCopy();
					return dest;
				}
				else
					throw Error.Argument(nameof(other), "Can only copy to an object of the same type");
			}

			public override IDeepCopyable DeepCopy()
			{
				return CopyTo(new ConditionLocationComponent());
			}

			public override bool Matches(IDeepComparable other)
			{
				var otherT = other as ConditionLocationComponent;
				if (otherT is null) return false;

				if (!base.Matches(otherT)) return false;
				if (!DeepComparable.Matches(Code, otherT.Code)) return false;
				if (!DeepComparable.Matches(DetailElement, otherT.DetailElement)) return false;

				return true;
			}

			public override bool IsExactly(IDeepComparable other)
			{
				var otherT = other as ConditionLocationComponent;
				if (otherT is null) return false;

				if (!base.IsExactly(otherT)) return false;
				if (!DeepComparable.IsExactly(Code, otherT.Code)) return false;
				if (!DeepComparable.IsExactly(DetailElement, otherT.DetailElement)) return false;

				return true;
			}

		}


		/// <summary>
		/// External Ids for this condition
		/// </summary>
		[FhirElement("identifier", Order = 70)]
		[Cardinality(Min = 0, Max = -1)]
		[DataMember]
		public List<Hl7.Fhir.Model.Identifier> Identifier { get; set; }

		/// <summary>
		/// Who has the condition?
		/// </summary>
		[FhirElement("subject", Order = 80)]
		[References("Patient")]
		[Cardinality(Min = 1, Max = 1)]
		[DataMember]
		public Hl7.Fhir.Model.ResourceReference Subject { get; set; }

		/// <summary>
		/// Encounter when condition first asserted
		/// </summary>
		[FhirElement("encounter", Order = 90)]
		[References("Encounter")]
		[DataMember]
		public Hl7.Fhir.Model.ResourceReference Encounter { get; set; }

		/// <summary>
		/// Person who asserts this condition
		/// </summary>
		[FhirElement("asserter", Order = 100)]
		[References("Practitioner", "Patient")]
		[DataMember]
		public Hl7.Fhir.Model.ResourceReference Asserter { get; set; }

		/// <summary>
		/// When first detected/suspected/entered
		/// </summary>
		[FhirElement("dateAsserted", Order = 110)]
		[DataMember]
		public Hl7.Fhir.Model.Date DateAssertedElement { get; set; }

		/// <summary>
		/// When first detected/suspected/entered
		/// </summary>
		/// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
		[NotMapped]
		[IgnoreDataMember]
		public string DateAsserted
		{
			get { return DateAssertedElement != null ? DateAssertedElement.Value : null; }
			set
			{
				if (value is null)
					DateAssertedElement = null;
				else
					DateAssertedElement = new Hl7.Fhir.Model.Date(value);
			}
		}

		/// <summary>
		/// Identification of the condition, problem or diagnosis
		/// </summary>
		[FhirElement("code", Order = 120)]
		[Cardinality(Min = 1, Max = 1)]
		[DataMember]
		public Hl7.Fhir.Model.CodeableConcept Code { get; set; }

		/// <summary>
		/// E.g. complaint | symptom | finding | diagnosis
		/// </summary>
		[FhirElement("category", Order = 130)]
		[DataMember]
		public Hl7.Fhir.Model.CodeableConcept Category { get; set; }

		/// <summary>
		/// provisional | working | confirmed | refuted
		/// </summary>
		[FhirElement("status", Order = 140)]
		[Cardinality(Min = 1, Max = 1)]
		[DataMember]
		public Code<Hl7.Fhir.Model.Condition.ConditionStatus> StatusElement { get; set; }

		/// <summary>
		/// provisional | working | confirmed | refuted
		/// </summary>
		/// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
		[NotMapped]
		[IgnoreDataMember]
		public Hl7.Fhir.Model.Condition.ConditionStatus? Status
		{
			get { return StatusElement != null ? StatusElement.Value : null; }
			set
			{
				if (value is null)
					StatusElement = null;
				else
					StatusElement = new Code<Hl7.Fhir.Model.Condition.ConditionStatus>(value);
			}
		}

		/// <summary>
		/// Degree of confidence
		/// </summary>
		[FhirElement("certainty", Order = 150)]
		[DataMember]
		public Hl7.Fhir.Model.CodeableConcept Certainty { get; set; }

		/// <summary>
		/// Subjective severity of condition
		/// </summary>
		[FhirElement("severity", Order = 160)]
		[DataMember]
		public Hl7.Fhir.Model.CodeableConcept Severity { get; set; }

		/// <summary>
		/// Estimated or actual date, or age
		/// </summary>
		[FhirElement("onset", Order = 170, Choice = ChoiceType.DatatypeChoice)]
		[AllowedTypes(typeof(Hl7.Fhir.Model.Date), typeof(Hl7.Fhir.Model.Age))]
		[DataMember]
		public Hl7.Fhir.Model.Element Onset { get; set; }

		/// <summary>
		/// If/when in resolution/remission
		/// </summary>
		[FhirElement("abatement", Order = 180, Choice = ChoiceType.DatatypeChoice)]
		[AllowedTypes(typeof(Hl7.Fhir.Model.Date), typeof(Hl7.Fhir.Model.Age), typeof(Hl7.Fhir.Model.FhirBoolean))]
		[DataMember]
		public Hl7.Fhir.Model.Element Abatement { get; set; }

		/// <summary>
		/// Stage/grade, usually assessed formally
		/// </summary>
		[FhirElement("stage", Order = 190)]
		[DataMember]
		public Hl7.Fhir.Model.Condition.ConditionStageComponent Stage { get; set; }

		/// <summary>
		/// Supporting evidence
		/// </summary>
		[FhirElement("evidence", Order = 200)]
		[Cardinality(Min = 0, Max = -1)]
		[DataMember]
		public List<Hl7.Fhir.Model.Condition.ConditionEvidenceComponent> Evidence { get; set; }

		/// <summary>
		/// Anatomical location, if relevant
		/// </summary>
		[FhirElement("location", Order = 210)]
		[Cardinality(Min = 0, Max = -1)]
		[DataMember]
		public List<Hl7.Fhir.Model.Condition.ConditionLocationComponent> Location { get; set; }

		/// <summary>
		/// Causes or precedents for this Condition
		/// </summary>
		[FhirElement("relatedItem", Order = 220)]
		[Cardinality(Min = 0, Max = -1)]
		[DataMember]
		public List<Hl7.Fhir.Model.Condition.ConditionRelatedItemComponent> RelatedItem { get; set; }

		/// <summary>
		/// Additional information about the Condition
		/// </summary>
		[FhirElement("notes", Order = 230)]
		[DataMember]
		public Hl7.Fhir.Model.FhirString NotesElement { get; set; }

		/// <summary>
		/// Additional information about the Condition
		/// </summary>
		/// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
		[NotMapped]
		[IgnoreDataMember]
		public string Notes
		{
			get { return NotesElement != null ? NotesElement.Value : null; }
			set
			{
				if (value is null)
					NotesElement = null;
				else
					NotesElement = new Hl7.Fhir.Model.FhirString(value);
			}
		}

		public override IDeepCopyable CopyTo(IDeepCopyable other)
		{
			var dest = other as Condition;

			if (dest != null)
			{
				base.CopyTo(dest);
				if (Identifier != null) dest.Identifier = new List<Hl7.Fhir.Model.Identifier>(Identifier.DeepCopy());
				if (Subject != null) dest.Subject = (Hl7.Fhir.Model.ResourceReference)Subject.DeepCopy();
				if (Encounter != null) dest.Encounter = (Hl7.Fhir.Model.ResourceReference)Encounter.DeepCopy();
				if (Asserter != null) dest.Asserter = (Hl7.Fhir.Model.ResourceReference)Asserter.DeepCopy();
				if (DateAssertedElement != null) dest.DateAssertedElement = (Hl7.Fhir.Model.Date)DateAssertedElement.DeepCopy();
				if (Code != null) dest.Code = (Hl7.Fhir.Model.CodeableConcept)Code.DeepCopy();
				if (Category != null) dest.Category = (Hl7.Fhir.Model.CodeableConcept)Category.DeepCopy();
				if (StatusElement != null) dest.StatusElement = (Code<Hl7.Fhir.Model.Condition.ConditionStatus>)StatusElement.DeepCopy();
				if (Certainty != null) dest.Certainty = (Hl7.Fhir.Model.CodeableConcept)Certainty.DeepCopy();
				if (Severity != null) dest.Severity = (Hl7.Fhir.Model.CodeableConcept)Severity.DeepCopy();
				if (Onset != null) dest.Onset = (Hl7.Fhir.Model.Element)Onset.DeepCopy();
				if (Abatement != null) dest.Abatement = (Hl7.Fhir.Model.Element)Abatement.DeepCopy();
				if (Stage != null) dest.Stage = (Hl7.Fhir.Model.Condition.ConditionStageComponent)Stage.DeepCopy();
				if (Evidence != null) dest.Evidence = new List<Hl7.Fhir.Model.Condition.ConditionEvidenceComponent>(Evidence.DeepCopy());
				if (Location != null) dest.Location = new List<Hl7.Fhir.Model.Condition.ConditionLocationComponent>(Location.DeepCopy());
				if (RelatedItem != null) dest.RelatedItem = new List<Hl7.Fhir.Model.Condition.ConditionRelatedItemComponent>(RelatedItem.DeepCopy());
				if (NotesElement != null) dest.NotesElement = (Hl7.Fhir.Model.FhirString)NotesElement.DeepCopy();
				return dest;
			}
			else
				throw Error.Argument(nameof(other), "Can only copy to an object of the same type");
		}

		public override IDeepCopyable DeepCopy()
		{
			return CopyTo(new Condition());
		}

		public override bool Matches(IDeepComparable other)
		{
			var otherT = other as Condition;
			if (otherT is null) return false;

			if (!base.Matches(otherT)) return false;
			if (!DeepComparable.Matches(Identifier, otherT.Identifier)) return false;
			if (!DeepComparable.Matches(Subject, otherT.Subject)) return false;
			if (!DeepComparable.Matches(Encounter, otherT.Encounter)) return false;
			if (!DeepComparable.Matches(Asserter, otherT.Asserter)) return false;
			if (!DeepComparable.Matches(DateAssertedElement, otherT.DateAssertedElement)) return false;
			if (!DeepComparable.Matches(Code, otherT.Code)) return false;
			if (!DeepComparable.Matches(Category, otherT.Category)) return false;
			if (!DeepComparable.Matches(StatusElement, otherT.StatusElement)) return false;
			if (!DeepComparable.Matches(Certainty, otherT.Certainty)) return false;
			if (!DeepComparable.Matches(Severity, otherT.Severity)) return false;
			if (!DeepComparable.Matches(Onset, otherT.Onset)) return false;
			if (!DeepComparable.Matches(Abatement, otherT.Abatement)) return false;
			if (!DeepComparable.Matches(Stage, otherT.Stage)) return false;
			if (!DeepComparable.Matches(Evidence, otherT.Evidence)) return false;
			if (!DeepComparable.Matches(Location, otherT.Location)) return false;
			if (!DeepComparable.Matches(RelatedItem, otherT.RelatedItem)) return false;
			if (!DeepComparable.Matches(NotesElement, otherT.NotesElement)) return false;

			return true;
		}

		public override bool IsExactly(IDeepComparable other)
		{
			var otherT = other as Condition;
			if (otherT is null) return false;

			if (!base.IsExactly(otherT)) return false;
			if (!DeepComparable.IsExactly(Identifier, otherT.Identifier)) return false;
			if (!DeepComparable.IsExactly(Subject, otherT.Subject)) return false;
			if (!DeepComparable.IsExactly(Encounter, otherT.Encounter)) return false;
			if (!DeepComparable.IsExactly(Asserter, otherT.Asserter)) return false;
			if (!DeepComparable.IsExactly(DateAssertedElement, otherT.DateAssertedElement)) return false;
			if (!DeepComparable.IsExactly(Code, otherT.Code)) return false;
			if (!DeepComparable.IsExactly(Category, otherT.Category)) return false;
			if (!DeepComparable.IsExactly(StatusElement, otherT.StatusElement)) return false;
			if (!DeepComparable.IsExactly(Certainty, otherT.Certainty)) return false;
			if (!DeepComparable.IsExactly(Severity, otherT.Severity)) return false;
			if (!DeepComparable.IsExactly(Onset, otherT.Onset)) return false;
			if (!DeepComparable.IsExactly(Abatement, otherT.Abatement)) return false;
			if (!DeepComparable.IsExactly(Stage, otherT.Stage)) return false;
			if (!DeepComparable.IsExactly(Evidence, otherT.Evidence)) return false;
			if (!DeepComparable.IsExactly(Location, otherT.Location)) return false;
			if (!DeepComparable.IsExactly(RelatedItem, otherT.RelatedItem)) return false;
			if (!DeepComparable.IsExactly(NotesElement, otherT.NotesElement)) return false;

			return true;
		}

	}

}
