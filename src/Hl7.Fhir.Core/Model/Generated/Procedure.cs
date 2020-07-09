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
	/// An action that is performed on a patient
	/// </summary>
	[FhirType("Procedure", IsResource = true)]
	[DataContract]
	public partial class Procedure : Hl7.Fhir.Model.Resource
	{
		/// <summary>
		/// The nature of the relationship with this procedure
		/// </summary>
		[FhirEnumeration("ProcedureRelationshipType")]
		public enum ProcedureRelationshipType
		{
			/// <summary>
			/// This procedure had to be performed because of the related one.
			/// </summary>
			[EnumLiteral("caused-by")]
			CausedBy,
			/// <summary>
			/// This procedure caused the related one to be performed.
			/// </summary>
			[EnumLiteral("because-of")]
			BecauseOf,
		}

		[FhirType("ProcedureRelatedItemComponent")]
		[DataContract]
		public partial class ProcedureRelatedItemComponent : Hl7.Fhir.Model.Element
		{
			/// <summary>
			/// caused-by | because-of
			/// </summary>
			[FhirElement("type", InSummary = true, Order = 40)]
			[DataMember]
			public Code<Hl7.Fhir.Model.Procedure.ProcedureRelationshipType> TypeElement { get; set; }

			/// <summary>
			/// caused-by | because-of
			/// </summary>
			/// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
			[NotMapped]
			[IgnoreDataMember]
			public Hl7.Fhir.Model.Procedure.ProcedureRelationshipType? Type
			{
				get { return TypeElement?.Value; }
				set
				{
					if (value is null)
						TypeElement = null;
					else
						TypeElement = new Code<Hl7.Fhir.Model.Procedure.ProcedureRelationshipType>(value);
				}
			}

			/// <summary>
			/// The related item - e.g. a procedure
			/// </summary>
			[FhirElement("target", InSummary = true, Order = 50)]
			[References("AdverseReaction", "AllergyIntolerance", "CarePlan", "Condition", "DeviceObservationReport", "DiagnosticReport", "FamilyHistory", "ImagingStudy", "Immunization", "ImmunizationRecommendation", "MedicationAdministration", "MedicationDispense", "MedicationPrescription", "MedicationStatement", "Observation", "Procedure")]
			[DataMember]
			public Hl7.Fhir.Model.ResourceReference Target { get; set; }

			public override IDeepCopyable CopyTo(IDeepCopyable other)
			{
				var dest = other as ProcedureRelatedItemComponent;

				if (dest != null)
				{
					base.CopyTo(dest);
					if (TypeElement != null) dest.TypeElement = (Code<Hl7.Fhir.Model.Procedure.ProcedureRelationshipType>)TypeElement.DeepCopy();
					if (Target != null) dest.Target = (Hl7.Fhir.Model.ResourceReference)Target.DeepCopy();
					return dest;
				}
				else
					throw Error.Argument(nameof(other), "Can only copy to an object of the same type");
			}

			public override IDeepCopyable DeepCopy()
			{
				return CopyTo(new ProcedureRelatedItemComponent());
			}

			public override bool Matches(IDeepComparable other)
			{
				var otherT = other as ProcedureRelatedItemComponent;
				if (otherT is null) return false;

				if (!base.Matches(otherT)) return false;
				if (!DeepComparable.Matches(TypeElement, otherT.TypeElement)) return false;
				if (!DeepComparable.Matches(Target, otherT.Target)) return false;

				return true;
			}

			public override bool IsExactly(IDeepComparable other)
			{
				var otherT = other as ProcedureRelatedItemComponent;
				if (otherT is null) return false;

				if (!base.IsExactly(otherT)) return false;
				if (!DeepComparable.IsExactly(TypeElement, otherT.TypeElement)) return false;
				if (!DeepComparable.IsExactly(Target, otherT.Target)) return false;

				return true;
			}

		}


		[FhirType("ProcedurePerformerComponent")]
		[DataContract]
		public partial class ProcedurePerformerComponent : Hl7.Fhir.Model.Element
		{
			/// <summary>
			/// The reference to the practitioner
			/// </summary>
			[FhirElement("person", InSummary = true, Order = 40)]
			[References("Practitioner")]
			[DataMember]
			public Hl7.Fhir.Model.ResourceReference Person { get; set; }

			/// <summary>
			/// The role the person was in
			/// </summary>
			[FhirElement("role", InSummary = true, Order = 50)]
			[DataMember]
			public Hl7.Fhir.Model.CodeableConcept Role { get; set; }

			public override IDeepCopyable CopyTo(IDeepCopyable other)
			{
				var dest = other as ProcedurePerformerComponent;

				if (dest != null)
				{
					base.CopyTo(dest);
					if (Person != null) dest.Person = (Hl7.Fhir.Model.ResourceReference)Person.DeepCopy();
					if (Role != null) dest.Role = (Hl7.Fhir.Model.CodeableConcept)Role.DeepCopy();
					return dest;
				}
				else
					throw Error.Argument(nameof(other), "Can only copy to an object of the same type");
			}

			public override IDeepCopyable DeepCopy()
			{
				return CopyTo(new ProcedurePerformerComponent());
			}

			public override bool Matches(IDeepComparable other)
			{
				var otherT = other as ProcedurePerformerComponent;
				if (otherT is null) return false;

				if (!base.Matches(otherT)) return false;
				if (!DeepComparable.Matches(Person, otherT.Person)) return false;
				if (!DeepComparable.Matches(Role, otherT.Role)) return false;

				return true;
			}

			public override bool IsExactly(IDeepComparable other)
			{
				var otherT = other as ProcedurePerformerComponent;
				if (otherT is null) return false;

				if (!base.IsExactly(otherT)) return false;
				if (!DeepComparable.IsExactly(Person, otherT.Person)) return false;
				if (!DeepComparable.IsExactly(Role, otherT.Role)) return false;

				return true;
			}

		}


		/// <summary>
		/// External Ids for this procedure
		/// </summary>
		[FhirElement("identifier", InSummary = true, Order = 70)]
		[Cardinality(Min = 0, Max = -1)]
		[DataMember]
		public List<Hl7.Fhir.Model.Identifier> Identifier { get; set; }

		/// <summary>
		/// Who procedure was performed on
		/// </summary>
		[FhirElement("subject", InSummary = true, Order = 80)]
		[References("Patient")]
		[Cardinality(Min = 1, Max = 1)]
		[DataMember]
		public Hl7.Fhir.Model.ResourceReference Subject { get; set; }

		/// <summary>
		/// Identification of the procedure
		/// </summary>
		[FhirElement("type", InSummary = true, Order = 90)]
		[Cardinality(Min = 1, Max = 1)]
		[DataMember]
		public Hl7.Fhir.Model.CodeableConcept Type { get; set; }

		/// <summary>
		/// Precise location details
		/// </summary>
		[FhirElement("bodySite", InSummary = true, Order = 100)]
		[Cardinality(Min = 0, Max = -1)]
		[DataMember]
		public List<Hl7.Fhir.Model.CodeableConcept> BodySite { get; set; }

		/// <summary>
		/// Reason procedure performed
		/// </summary>
		[FhirElement("indication", InSummary = true, Order = 110)]
		[Cardinality(Min = 0, Max = -1)]
		[DataMember]
		public List<Hl7.Fhir.Model.CodeableConcept> Indication { get; set; }

		/// <summary>
		/// The people who performed the procedure
		/// </summary>
		[FhirElement("performer", InSummary = true, Order = 120)]
		[Cardinality(Min = 0, Max = -1)]
		[DataMember]
		public List<Hl7.Fhir.Model.Procedure.ProcedurePerformerComponent> Performer { get; set; }

		/// <summary>
		/// The date the procedure was performed
		/// </summary>
		[FhirElement("date", InSummary = true, Order = 130)]
		[DataMember]
		public Hl7.Fhir.Model.Period Date { get; set; }

		/// <summary>
		/// The encounter when procedure performed
		/// </summary>
		[FhirElement("encounter", InSummary = true, Order = 140)]
		[References("Encounter")]
		[DataMember]
		public Hl7.Fhir.Model.ResourceReference Encounter { get; set; }

		/// <summary>
		/// What was result of procedure?
		/// </summary>
		[FhirElement("outcome", InSummary = true, Order = 150)]
		[DataMember]
		public Hl7.Fhir.Model.FhirString OutcomeElement { get; set; }

		/// <summary>
		/// What was result of procedure?
		/// </summary>
		/// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
		[NotMapped]
		[IgnoreDataMember]
		public string Outcome
		{
			get { return OutcomeElement?.Value; }
			set
			{
				if (value is null)
					OutcomeElement = null;
				else
					OutcomeElement = new Hl7.Fhir.Model.FhirString(value);
			}
		}

		/// <summary>
		/// Any report that results from the procedure
		/// </summary>
		[FhirElement("report", Order = 160)]
		[References("DiagnosticReport")]
		[Cardinality(Min = 0, Max = -1)]
		[DataMember]
		public List<Hl7.Fhir.Model.ResourceReference> Report { get; set; }

		/// <summary>
		/// Complication following the procedure
		/// </summary>
		[FhirElement("complication", Order = 170)]
		[Cardinality(Min = 0, Max = -1)]
		[DataMember]
		public List<Hl7.Fhir.Model.CodeableConcept> Complication { get; set; }

		/// <summary>
		/// Instructions for follow up
		/// </summary>
		[FhirElement("followUp", Order = 180)]
		[DataMember]
		public Hl7.Fhir.Model.FhirString FollowUpElement { get; set; }

		/// <summary>
		/// Instructions for follow up
		/// </summary>
		/// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
		[NotMapped]
		[IgnoreDataMember]
		public string FollowUp
		{
			get { return FollowUpElement?.Value; }
			set
			{
				if (value is null)
					FollowUpElement = null;
				else
					FollowUpElement = new Hl7.Fhir.Model.FhirString(value);
			}
		}

		/// <summary>
		/// A procedure that is related to this one
		/// </summary>
		[FhirElement("relatedItem", Order = 190)]
		[Cardinality(Min = 0, Max = -1)]
		[DataMember]
		public List<Hl7.Fhir.Model.Procedure.ProcedureRelatedItemComponent> RelatedItem { get; set; }

		/// <summary>
		/// Additional information about procedure
		/// </summary>
		[FhirElement("notes", Order = 200)]
		[DataMember]
		public Hl7.Fhir.Model.FhirString NotesElement { get; set; }

		/// <summary>
		/// Additional information about procedure
		/// </summary>
		/// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
		[NotMapped]
		[IgnoreDataMember]
		public string Notes
		{
			get { return NotesElement?.Value; }
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
			var dest = other as Procedure;

			if (dest != null)
			{
				base.CopyTo(dest);
				if (Identifier != null) dest.Identifier = new List<Hl7.Fhir.Model.Identifier>(Identifier.DeepCopy());
				if (Subject != null) dest.Subject = (Hl7.Fhir.Model.ResourceReference)Subject.DeepCopy();
				if (Type != null) dest.Type = (Hl7.Fhir.Model.CodeableConcept)Type.DeepCopy();
				if (BodySite != null) dest.BodySite = new List<Hl7.Fhir.Model.CodeableConcept>(BodySite.DeepCopy());
				if (Indication != null) dest.Indication = new List<Hl7.Fhir.Model.CodeableConcept>(Indication.DeepCopy());
				if (Performer != null) dest.Performer = new List<Hl7.Fhir.Model.Procedure.ProcedurePerformerComponent>(Performer.DeepCopy());
				if (Date != null) dest.Date = (Hl7.Fhir.Model.Period)Date.DeepCopy();
				if (Encounter != null) dest.Encounter = (Hl7.Fhir.Model.ResourceReference)Encounter.DeepCopy();
				if (OutcomeElement != null) dest.OutcomeElement = (Hl7.Fhir.Model.FhirString)OutcomeElement.DeepCopy();
				if (Report != null) dest.Report = new List<Hl7.Fhir.Model.ResourceReference>(Report.DeepCopy());
				if (Complication != null) dest.Complication = new List<Hl7.Fhir.Model.CodeableConcept>(Complication.DeepCopy());
				if (FollowUpElement != null) dest.FollowUpElement = (Hl7.Fhir.Model.FhirString)FollowUpElement.DeepCopy();
				if (RelatedItem != null) dest.RelatedItem = new List<Hl7.Fhir.Model.Procedure.ProcedureRelatedItemComponent>(RelatedItem.DeepCopy());
				if (NotesElement != null) dest.NotesElement = (Hl7.Fhir.Model.FhirString)NotesElement.DeepCopy();
				return dest;
			}
			else
				throw Error.Argument(nameof(other), "Can only copy to an object of the same type");
		}

		public override IDeepCopyable DeepCopy()
		{
			return CopyTo(new Procedure());
		}

		public override bool Matches(IDeepComparable other)
		{
			var otherT = other as Procedure;
			if (otherT is null) return false;

			if (!base.Matches(otherT)) return false;
			if (!DeepComparable.Matches(Identifier, otherT.Identifier)) return false;
			if (!DeepComparable.Matches(Subject, otherT.Subject)) return false;
			if (!DeepComparable.Matches(Type, otherT.Type)) return false;
			if (!DeepComparable.Matches(BodySite, otherT.BodySite)) return false;
			if (!DeepComparable.Matches(Indication, otherT.Indication)) return false;
			if (!DeepComparable.Matches(Performer, otherT.Performer)) return false;
			if (!DeepComparable.Matches(Date, otherT.Date)) return false;
			if (!DeepComparable.Matches(Encounter, otherT.Encounter)) return false;
			if (!DeepComparable.Matches(OutcomeElement, otherT.OutcomeElement)) return false;
			if (!DeepComparable.Matches(Report, otherT.Report)) return false;
			if (!DeepComparable.Matches(Complication, otherT.Complication)) return false;
			if (!DeepComparable.Matches(FollowUpElement, otherT.FollowUpElement)) return false;
			if (!DeepComparable.Matches(RelatedItem, otherT.RelatedItem)) return false;
			if (!DeepComparable.Matches(NotesElement, otherT.NotesElement)) return false;

			return true;
		}

		public override bool IsExactly(IDeepComparable other)
		{
			var otherT = other as Procedure;
			if (otherT is null) return false;

			if (!base.IsExactly(otherT)) return false;
			if (!DeepComparable.IsExactly(Identifier, otherT.Identifier)) return false;
			if (!DeepComparable.IsExactly(Subject, otherT.Subject)) return false;
			if (!DeepComparable.IsExactly(Type, otherT.Type)) return false;
			if (!DeepComparable.IsExactly(BodySite, otherT.BodySite)) return false;
			if (!DeepComparable.IsExactly(Indication, otherT.Indication)) return false;
			if (!DeepComparable.IsExactly(Performer, otherT.Performer)) return false;
			if (!DeepComparable.IsExactly(Date, otherT.Date)) return false;
			if (!DeepComparable.IsExactly(Encounter, otherT.Encounter)) return false;
			if (!DeepComparable.IsExactly(OutcomeElement, otherT.OutcomeElement)) return false;
			if (!DeepComparable.IsExactly(Report, otherT.Report)) return false;
			if (!DeepComparable.IsExactly(Complication, otherT.Complication)) return false;
			if (!DeepComparable.IsExactly(FollowUpElement, otherT.FollowUpElement)) return false;
			if (!DeepComparable.IsExactly(RelatedItem, otherT.RelatedItem)) return false;
			if (!DeepComparable.IsExactly(NotesElement, otherT.NotesElement)) return false;

			return true;
		}

	}

}
