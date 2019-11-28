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
	/// Prescription of medication to for patient
	/// </summary>
	[FhirType("MedicationPrescription", IsResource = true)]
	[DataContract]
	public partial class MedicationPrescription : Hl7.Fhir.Model.Resource
	{
		/// <summary>
		/// A code specifying the state of the prescribing event. Describes the lifecycle of the prescription.
		/// </summary>
		[FhirEnumeration("MedicationPrescriptionStatus")]
		public enum MedicationPrescriptionStatus
		{
			/// <summary>
			/// The prescription is 'actionable', but not all actions that are implied by it have occurred yet.
			/// </summary>
			[EnumLiteral("active")]
			Active,
			/// <summary>
			/// Actions implied by the prescription have been temporarily halted, but are expected to continue later.  May also be called "suspended".
			/// </summary>
			[EnumLiteral("on hold")]
			OnHold,
			/// <summary>
			/// All actions that are implied by the prescription have occurred (this will rarely be made explicit).
			/// </summary>
			[EnumLiteral("completed")]
			Completed,
			/// <summary>
			/// The prescription was entered in error and therefore nullified.
			/// </summary>
			[EnumLiteral("entered in error")]
			EnteredInError,
			/// <summary>
			/// Actions implied by the prescription have been permanently halted, before all of them occurred.
			/// </summary>
			[EnumLiteral("stopped")]
			Stopped,
			/// <summary>
			/// The prescription was replaced by a newer one, which encompasses all the information in the previous one.
			/// </summary>
			[EnumLiteral("superceded")]
			Superceded,
		}

		[FhirType("MedicationPrescriptionDosageInstructionComponent")]
		[DataContract]
		public partial class MedicationPrescriptionDosageInstructionComponent : Hl7.Fhir.Model.Element
		{
			/// <summary>
			/// Dosage instructions expressed as text
			/// </summary>
			[FhirElement("text", InSummary = true, Order = 40)]
			[DataMember]
			public Hl7.Fhir.Model.FhirString TextElement { get; set; }

			/// <summary>
			/// Dosage instructions expressed as text
			/// </summary>
			/// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
			[NotMapped]
			[IgnoreDataMember]
			public string Text
			{
				get { return TextElement != null ? TextElement.Value : null; }
				set
				{
					if (value == null)
						TextElement = null;
					else
						TextElement = new Hl7.Fhir.Model.FhirString(value);
				}
			}

			/// <summary>
			/// Supplemental instructions - e.g. "with meals"
			/// </summary>
			[FhirElement("additionalInstructions", InSummary = true, Order = 50)]
			[DataMember]
			public Hl7.Fhir.Model.CodeableConcept AdditionalInstructions { get; set; }

			/// <summary>
			/// When medication should be administered
			/// </summary>
			[FhirElement("timing", InSummary = true, Order = 60, Choice = ChoiceType.DatatypeChoice)]
			[AllowedTypes(typeof(Hl7.Fhir.Model.FhirDateTime), typeof(Hl7.Fhir.Model.Period), typeof(Hl7.Fhir.Model.Schedule))]
			[DataMember]
			public Hl7.Fhir.Model.Element Timing { get; set; }

			/// <summary>
			/// Take "as needed" f(or x)
			/// </summary>
			[FhirElement("asNeeded", InSummary = true, Order = 70, Choice = ChoiceType.DatatypeChoice)]
			[AllowedTypes(typeof(Hl7.Fhir.Model.FhirBoolean), typeof(Hl7.Fhir.Model.CodeableConcept))]
			[DataMember]
			public Hl7.Fhir.Model.Element AsNeeded { get; set; }

			/// <summary>
			/// Body site to administer to
			/// </summary>
			[FhirElement("site", InSummary = true, Order = 80)]
			[DataMember]
			public Hl7.Fhir.Model.CodeableConcept Site { get; set; }

			/// <summary>
			/// How drug should enter body
			/// </summary>
			[FhirElement("route", InSummary = true, Order = 90)]
			[DataMember]
			public Hl7.Fhir.Model.CodeableConcept Route { get; set; }

			/// <summary>
			/// Technique for administering medication
			/// </summary>
			[FhirElement("method", InSummary = true, Order = 100)]
			[DataMember]
			public Hl7.Fhir.Model.CodeableConcept Method { get; set; }

			/// <summary>
			/// Amount of medication per dose
			/// </summary>
			[FhirElement("doseQuantity", InSummary = true, Order = 110)]
			[DataMember]
			public Hl7.Fhir.Model.Quantity DoseQuantity { get; set; }

			/// <summary>
			/// Amount of medication per unit of time
			/// </summary>
			[FhirElement("rate", InSummary = true, Order = 120)]
			[DataMember]
			public Hl7.Fhir.Model.Ratio Rate { get; set; }

			/// <summary>
			/// Upper limit on medication per unit of time
			/// </summary>
			[FhirElement("maxDosePerPeriod", InSummary = true, Order = 130)]
			[DataMember]
			public Hl7.Fhir.Model.Ratio MaxDosePerPeriod { get; set; }

			public override IDeepCopyable CopyTo(IDeepCopyable other)
			{
				var dest = other as MedicationPrescriptionDosageInstructionComponent;

				if (dest != null)
				{
					base.CopyTo(dest);
					if (TextElement != null) dest.TextElement = (Hl7.Fhir.Model.FhirString)TextElement.DeepCopy();
					if (AdditionalInstructions != null) dest.AdditionalInstructions = (Hl7.Fhir.Model.CodeableConcept)AdditionalInstructions.DeepCopy();
					if (Timing != null) dest.Timing = (Hl7.Fhir.Model.Element)Timing.DeepCopy();
					if (AsNeeded != null) dest.AsNeeded = (Hl7.Fhir.Model.Element)AsNeeded.DeepCopy();
					if (Site != null) dest.Site = (Hl7.Fhir.Model.CodeableConcept)Site.DeepCopy();
					if (Route != null) dest.Route = (Hl7.Fhir.Model.CodeableConcept)Route.DeepCopy();
					if (Method != null) dest.Method = (Hl7.Fhir.Model.CodeableConcept)Method.DeepCopy();
					if (DoseQuantity != null) dest.DoseQuantity = (Hl7.Fhir.Model.Quantity)DoseQuantity.DeepCopy();
					if (Rate != null) dest.Rate = (Hl7.Fhir.Model.Ratio)Rate.DeepCopy();
					if (MaxDosePerPeriod != null) dest.MaxDosePerPeriod = (Hl7.Fhir.Model.Ratio)MaxDosePerPeriod.DeepCopy();
					return dest;
				}
				else
					throw Error.Argument(nameof(other), "Can only copy to an object of the same type");
			}

			public override IDeepCopyable DeepCopy()
			{
				return CopyTo(new MedicationPrescriptionDosageInstructionComponent());
			}

			public override bool Matches(IDeepComparable other)
			{
				var otherT = other as MedicationPrescriptionDosageInstructionComponent;
				if (otherT == null) return false;

				if (!base.Matches(otherT)) return false;
				if (!DeepComparable.Matches(TextElement, otherT.TextElement)) return false;
				if (!DeepComparable.Matches(AdditionalInstructions, otherT.AdditionalInstructions)) return false;
				if (!DeepComparable.Matches(Timing, otherT.Timing)) return false;
				if (!DeepComparable.Matches(AsNeeded, otherT.AsNeeded)) return false;
				if (!DeepComparable.Matches(Site, otherT.Site)) return false;
				if (!DeepComparable.Matches(Route, otherT.Route)) return false;
				if (!DeepComparable.Matches(Method, otherT.Method)) return false;
				if (!DeepComparable.Matches(DoseQuantity, otherT.DoseQuantity)) return false;
				if (!DeepComparable.Matches(Rate, otherT.Rate)) return false;
				if (!DeepComparable.Matches(MaxDosePerPeriod, otherT.MaxDosePerPeriod)) return false;

				return true;
			}

			public override bool IsExactly(IDeepComparable other)
			{
				var otherT = other as MedicationPrescriptionDosageInstructionComponent;
				if (otherT == null) return false;

				if (!base.IsExactly(otherT)) return false;
				if (!DeepComparable.IsExactly(TextElement, otherT.TextElement)) return false;
				if (!DeepComparable.IsExactly(AdditionalInstructions, otherT.AdditionalInstructions)) return false;
				if (!DeepComparable.IsExactly(Timing, otherT.Timing)) return false;
				if (!DeepComparable.IsExactly(AsNeeded, otherT.AsNeeded)) return false;
				if (!DeepComparable.IsExactly(Site, otherT.Site)) return false;
				if (!DeepComparable.IsExactly(Route, otherT.Route)) return false;
				if (!DeepComparable.IsExactly(Method, otherT.Method)) return false;
				if (!DeepComparable.IsExactly(DoseQuantity, otherT.DoseQuantity)) return false;
				if (!DeepComparable.IsExactly(Rate, otherT.Rate)) return false;
				if (!DeepComparable.IsExactly(MaxDosePerPeriod, otherT.MaxDosePerPeriod)) return false;

				return true;
			}

		}


		[FhirType("MedicationPrescriptionSubstitutionComponent")]
		[DataContract]
		public partial class MedicationPrescriptionSubstitutionComponent : Hl7.Fhir.Model.Element
		{
			/// <summary>
			/// generic | formulary +
			/// </summary>
			[FhirElement("type", InSummary = true, Order = 40)]
			[Cardinality(Min = 1, Max = 1)]
			[DataMember]
			public Hl7.Fhir.Model.CodeableConcept Type { get; set; }

			/// <summary>
			/// Why should substitution (not) be made
			/// </summary>
			[FhirElement("reason", InSummary = true, Order = 50)]
			[DataMember]
			public Hl7.Fhir.Model.CodeableConcept Reason { get; set; }

			public override IDeepCopyable CopyTo(IDeepCopyable other)
			{
				var dest = other as MedicationPrescriptionSubstitutionComponent;

				if (dest != null)
				{
					base.CopyTo(dest);
					if (Type != null) dest.Type = (Hl7.Fhir.Model.CodeableConcept)Type.DeepCopy();
					if (Reason != null) dest.Reason = (Hl7.Fhir.Model.CodeableConcept)Reason.DeepCopy();
					return dest;
				}
				else
					throw Error.Argument(nameof(other), "Can only copy to an object of the same type");
			}

			public override IDeepCopyable DeepCopy()
			{
				return CopyTo(new MedicationPrescriptionSubstitutionComponent());
			}

			public override bool Matches(IDeepComparable other)
			{
				var otherT = other as MedicationPrescriptionSubstitutionComponent;
				if (otherT == null) return false;

				if (!base.Matches(otherT)) return false;
				if (!DeepComparable.Matches(Type, otherT.Type)) return false;
				if (!DeepComparable.Matches(Reason, otherT.Reason)) return false;

				return true;
			}

			public override bool IsExactly(IDeepComparable other)
			{
				var otherT = other as MedicationPrescriptionSubstitutionComponent;
				if (otherT == null) return false;

				if (!base.IsExactly(otherT)) return false;
				if (!DeepComparable.IsExactly(Type, otherT.Type)) return false;
				if (!DeepComparable.IsExactly(Reason, otherT.Reason)) return false;

				return true;
			}

		}


		[FhirType("MedicationPrescriptionDispenseComponent")]
		[DataContract]
		public partial class MedicationPrescriptionDispenseComponent : Hl7.Fhir.Model.Element
		{
			/// <summary>
			/// Product to be supplied
			/// </summary>
			[FhirElement("medication", InSummary = true, Order = 40)]
			[References("Medication")]
			[DataMember]
			public Hl7.Fhir.Model.ResourceReference Medication { get; set; }

			/// <summary>
			/// Time period supply is authorized for
			/// </summary>
			[FhirElement("validityPeriod", InSummary = true, Order = 50)]
			[DataMember]
			public Hl7.Fhir.Model.Period ValidityPeriod { get; set; }

			/// <summary>
			/// # of refills authorized
			/// </summary>
			[FhirElement("numberOfRepeatsAllowed", InSummary = true, Order = 60)]
			[DataMember]
			public Hl7.Fhir.Model.Integer NumberOfRepeatsAllowedElement { get; set; }

			/// <summary>
			/// # of refills authorized
			/// </summary>
			/// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
			[NotMapped]
			[IgnoreDataMember]
			public int? NumberOfRepeatsAllowed
			{
				get { return NumberOfRepeatsAllowedElement != null ? NumberOfRepeatsAllowedElement.Value : null; }
				set
				{
					if (value == null)
						NumberOfRepeatsAllowedElement = null;
					else
						NumberOfRepeatsAllowedElement = new Hl7.Fhir.Model.Integer(value);
				}
			}

			/// <summary>
			/// Amount of medication to supply per dispense
			/// </summary>
			[FhirElement("quantity", InSummary = true, Order = 70)]
			[DataMember]
			public Hl7.Fhir.Model.Quantity Quantity { get; set; }

			/// <summary>
			/// Days supply per dispense
			/// </summary>
			[FhirElement("expectedSupplyDuration", InSummary = true, Order = 80)]
			[DataMember]
			public Hl7.Fhir.Model.Duration ExpectedSupplyDuration { get; set; }

			public override IDeepCopyable CopyTo(IDeepCopyable other)
			{
				var dest = other as MedicationPrescriptionDispenseComponent;

				if (dest != null)
				{
					base.CopyTo(dest);
					if (Medication != null) dest.Medication = (Hl7.Fhir.Model.ResourceReference)Medication.DeepCopy();
					if (ValidityPeriod != null) dest.ValidityPeriod = (Hl7.Fhir.Model.Period)ValidityPeriod.DeepCopy();
					if (NumberOfRepeatsAllowedElement != null) dest.NumberOfRepeatsAllowedElement = (Hl7.Fhir.Model.Integer)NumberOfRepeatsAllowedElement.DeepCopy();
					if (Quantity != null) dest.Quantity = (Hl7.Fhir.Model.Quantity)Quantity.DeepCopy();
					if (ExpectedSupplyDuration != null) dest.ExpectedSupplyDuration = (Hl7.Fhir.Model.Duration)ExpectedSupplyDuration.DeepCopy();
					return dest;
				}
				else
					throw Error.Argument(nameof(other), "Can only copy to an object of the same type");
			}

			public override IDeepCopyable DeepCopy()
			{
				return CopyTo(new MedicationPrescriptionDispenseComponent());
			}

			public override bool Matches(IDeepComparable other)
			{
				var otherT = other as MedicationPrescriptionDispenseComponent;
				if (otherT == null) return false;

				if (!base.Matches(otherT)) return false;
				if (!DeepComparable.Matches(Medication, otherT.Medication)) return false;
				if (!DeepComparable.Matches(ValidityPeriod, otherT.ValidityPeriod)) return false;
				if (!DeepComparable.Matches(NumberOfRepeatsAllowedElement, otherT.NumberOfRepeatsAllowedElement)) return false;
				if (!DeepComparable.Matches(Quantity, otherT.Quantity)) return false;
				if (!DeepComparable.Matches(ExpectedSupplyDuration, otherT.ExpectedSupplyDuration)) return false;

				return true;
			}

			public override bool IsExactly(IDeepComparable other)
			{
				var otherT = other as MedicationPrescriptionDispenseComponent;
				if (otherT == null) return false;

				if (!base.IsExactly(otherT)) return false;
				if (!DeepComparable.IsExactly(Medication, otherT.Medication)) return false;
				if (!DeepComparable.IsExactly(ValidityPeriod, otherT.ValidityPeriod)) return false;
				if (!DeepComparable.IsExactly(NumberOfRepeatsAllowedElement, otherT.NumberOfRepeatsAllowedElement)) return false;
				if (!DeepComparable.IsExactly(Quantity, otherT.Quantity)) return false;
				if (!DeepComparable.IsExactly(ExpectedSupplyDuration, otherT.ExpectedSupplyDuration)) return false;

				return true;
			}

		}


		/// <summary>
		/// External identifier
		/// </summary>
		[FhirElement("identifier", Order = 70)]
		[Cardinality(Min = 0, Max = -1)]
		[DataMember]
		public List<Hl7.Fhir.Model.Identifier> Identifier { get; set; }

		/// <summary>
		/// When prescription was authorized
		/// </summary>
		[FhirElement("dateWritten", Order = 80)]
		[DataMember]
		public Hl7.Fhir.Model.FhirDateTime DateWrittenElement { get; set; }

		/// <summary>
		/// When prescription was authorized
		/// </summary>
		/// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
		[NotMapped]
		[IgnoreDataMember]
		public string DateWritten
		{
			get { return DateWrittenElement != null ? DateWrittenElement.Value : null; }
			set
			{
				if (value == null)
					DateWrittenElement = null;
				else
					DateWrittenElement = new Hl7.Fhir.Model.FhirDateTime(value);
			}
		}

		/// <summary>
		/// active | on hold | completed | entered in error | stopped | superceded
		/// </summary>
		[FhirElement("status", Order = 90)]
		[DataMember]
		public Code<Hl7.Fhir.Model.MedicationPrescription.MedicationPrescriptionStatus> StatusElement { get; set; }

		/// <summary>
		/// active | on hold | completed | entered in error | stopped | superceded
		/// </summary>
		/// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
		[NotMapped]
		[IgnoreDataMember]
		public Hl7.Fhir.Model.MedicationPrescription.MedicationPrescriptionStatus? Status
		{
			get { return StatusElement != null ? StatusElement.Value : null; }
			set
			{
				if (value == null)
					StatusElement = null;
				else
					StatusElement = new Code<Hl7.Fhir.Model.MedicationPrescription.MedicationPrescriptionStatus>(value);
			}
		}

		/// <summary>
		/// Who prescription is for
		/// </summary>
		[FhirElement("patient", Order = 100)]
		[References("Patient")]
		[DataMember]
		public Hl7.Fhir.Model.ResourceReference Patient { get; set; }

		/// <summary>
		/// Who ordered the medication(s)
		/// </summary>
		[FhirElement("prescriber", Order = 110)]
		[References("Practitioner")]
		[DataMember]
		public Hl7.Fhir.Model.ResourceReference Prescriber { get; set; }

		/// <summary>
		/// Created during encounter / admission / stay
		/// </summary>
		[FhirElement("encounter", Order = 120)]
		[References("Encounter")]
		[DataMember]
		public Hl7.Fhir.Model.ResourceReference Encounter { get; set; }

		/// <summary>
		/// Reason or indication for writing the prescription
		/// </summary>
		[FhirElement("reason", Order = 130, Choice = ChoiceType.DatatypeChoice)]
		[AllowedTypes(typeof(Hl7.Fhir.Model.CodeableConcept), typeof(Hl7.Fhir.Model.ResourceReference))]
		[DataMember]
		public Hl7.Fhir.Model.Element Reason { get; set; }

		/// <summary>
		/// Medication to be taken
		/// </summary>
		[FhirElement("medication", Order = 140)]
		[References("Medication")]
		[DataMember]
		public Hl7.Fhir.Model.ResourceReference Medication { get; set; }

		/// <summary>
		/// How medication should be taken
		/// </summary>
		[FhirElement("dosageInstruction", Order = 150)]
		[Cardinality(Min = 0, Max = -1)]
		[DataMember]
		public List<Hl7.Fhir.Model.MedicationPrescription.MedicationPrescriptionDosageInstructionComponent> DosageInstruction { get; set; }

		/// <summary>
		/// Medication supply authorization
		/// </summary>
		[FhirElement("dispense", Order = 160)]
		[DataMember]
		public Hl7.Fhir.Model.MedicationPrescription.MedicationPrescriptionDispenseComponent Dispense { get; set; }

		/// <summary>
		/// Any restrictions on medication substitution?
		/// </summary>
		[FhirElement("substitution", Order = 170)]
		[DataMember]
		public Hl7.Fhir.Model.MedicationPrescription.MedicationPrescriptionSubstitutionComponent Substitution { get; set; }

		public override IDeepCopyable CopyTo(IDeepCopyable other)
		{
			var dest = other as MedicationPrescription;

			if (dest != null)
			{
				base.CopyTo(dest);
				if (Identifier != null) dest.Identifier = new List<Hl7.Fhir.Model.Identifier>(Identifier.DeepCopy());
				if (DateWrittenElement != null) dest.DateWrittenElement = (Hl7.Fhir.Model.FhirDateTime)DateWrittenElement.DeepCopy();
				if (StatusElement != null) dest.StatusElement = (Code<Hl7.Fhir.Model.MedicationPrescription.MedicationPrescriptionStatus>)StatusElement.DeepCopy();
				if (Patient != null) dest.Patient = (Hl7.Fhir.Model.ResourceReference)Patient.DeepCopy();
				if (Prescriber != null) dest.Prescriber = (Hl7.Fhir.Model.ResourceReference)Prescriber.DeepCopy();
				if (Encounter != null) dest.Encounter = (Hl7.Fhir.Model.ResourceReference)Encounter.DeepCopy();
				if (Reason != null) dest.Reason = (Hl7.Fhir.Model.Element)Reason.DeepCopy();
				if (Medication != null) dest.Medication = (Hl7.Fhir.Model.ResourceReference)Medication.DeepCopy();
				if (DosageInstruction != null) dest.DosageInstruction = new List<Hl7.Fhir.Model.MedicationPrescription.MedicationPrescriptionDosageInstructionComponent>(DosageInstruction.DeepCopy());
				if (Dispense != null) dest.Dispense = (Hl7.Fhir.Model.MedicationPrescription.MedicationPrescriptionDispenseComponent)Dispense.DeepCopy();
				if (Substitution != null) dest.Substitution = (Hl7.Fhir.Model.MedicationPrescription.MedicationPrescriptionSubstitutionComponent)Substitution.DeepCopy();
				return dest;
			}
			else
				throw Error.Argument(nameof(other), "Can only copy to an object of the same type");
		}

		public override IDeepCopyable DeepCopy()
		{
			return CopyTo(new MedicationPrescription());
		}

		public override bool Matches(IDeepComparable other)
		{
			var otherT = other as MedicationPrescription;
			if (otherT == null) return false;

			if (!base.Matches(otherT)) return false;
			if (!DeepComparable.Matches(Identifier, otherT.Identifier)) return false;
			if (!DeepComparable.Matches(DateWrittenElement, otherT.DateWrittenElement)) return false;
			if (!DeepComparable.Matches(StatusElement, otherT.StatusElement)) return false;
			if (!DeepComparable.Matches(Patient, otherT.Patient)) return false;
			if (!DeepComparable.Matches(Prescriber, otherT.Prescriber)) return false;
			if (!DeepComparable.Matches(Encounter, otherT.Encounter)) return false;
			if (!DeepComparable.Matches(Reason, otherT.Reason)) return false;
			if (!DeepComparable.Matches(Medication, otherT.Medication)) return false;
			if (!DeepComparable.Matches(DosageInstruction, otherT.DosageInstruction)) return false;
			if (!DeepComparable.Matches(Dispense, otherT.Dispense)) return false;
			if (!DeepComparable.Matches(Substitution, otherT.Substitution)) return false;

			return true;
		}

		public override bool IsExactly(IDeepComparable other)
		{
			var otherT = other as MedicationPrescription;
			if (otherT == null) return false;

			if (!base.IsExactly(otherT)) return false;
			if (!DeepComparable.IsExactly(Identifier, otherT.Identifier)) return false;
			if (!DeepComparable.IsExactly(DateWrittenElement, otherT.DateWrittenElement)) return false;
			if (!DeepComparable.IsExactly(StatusElement, otherT.StatusElement)) return false;
			if (!DeepComparable.IsExactly(Patient, otherT.Patient)) return false;
			if (!DeepComparable.IsExactly(Prescriber, otherT.Prescriber)) return false;
			if (!DeepComparable.IsExactly(Encounter, otherT.Encounter)) return false;
			if (!DeepComparable.IsExactly(Reason, otherT.Reason)) return false;
			if (!DeepComparable.IsExactly(Medication, otherT.Medication)) return false;
			if (!DeepComparable.IsExactly(DosageInstruction, otherT.DosageInstruction)) return false;
			if (!DeepComparable.IsExactly(Dispense, otherT.Dispense)) return false;
			if (!DeepComparable.IsExactly(Substitution, otherT.Substitution)) return false;

			return true;
		}

	}

}
