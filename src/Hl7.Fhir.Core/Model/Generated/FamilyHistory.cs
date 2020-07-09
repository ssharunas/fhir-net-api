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
	/// Information about patient's relatives, relevant for patient
	/// </summary>
	[FhirType("FamilyHistory", IsResource = true)]
	[DataContract]
	public partial class FamilyHistory : Hl7.Fhir.Model.Resource
	{
		[FhirType("FamilyHistoryRelationConditionComponent")]
		[DataContract]
		public partial class FamilyHistoryRelationConditionComponent : Hl7.Fhir.Model.Element
		{
			/// <summary>
			/// Condition suffered by relation
			/// </summary>
			[FhirElement("type", InSummary = true, Order = 40)]
			[Cardinality(Min = 1, Max = 1)]
			[DataMember]
			public Hl7.Fhir.Model.CodeableConcept Type { get; set; }

			/// <summary>
			/// deceased | permanent disability | etc.
			/// </summary>
			[FhirElement("outcome", InSummary = true, Order = 50)]
			[DataMember]
			public Hl7.Fhir.Model.CodeableConcept Outcome { get; set; }

			/// <summary>
			/// When condition first manifested
			/// </summary>
			[FhirElement("onset", InSummary = true, Order = 60, Choice = ChoiceType.DatatypeChoice)]
			[AllowedTypes(typeof(Hl7.Fhir.Model.Age), typeof(Hl7.Fhir.Model.Range), typeof(Hl7.Fhir.Model.FhirString))]
			[DataMember]
			public Hl7.Fhir.Model.Element Onset { get; set; }

			/// <summary>
			/// Extra information about condition
			/// </summary>
			[FhirElement("note", InSummary = true, Order = 70)]
			[DataMember]
			public Hl7.Fhir.Model.FhirString NoteElement { get; set; }

			/// <summary>
			/// Extra information about condition
			/// </summary>
			/// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
			[NotMapped]
			[IgnoreDataMember]
			public string Note
			{
				get { return NoteElement?.Value; }
				set
				{
					if (value is null)
						NoteElement = null;
					else
						NoteElement = new Hl7.Fhir.Model.FhirString(value);
				}
			}

			public override IDeepCopyable CopyTo(IDeepCopyable other)
			{
				var dest = other as FamilyHistoryRelationConditionComponent;

				if (dest != null)
				{
					base.CopyTo(dest);
					if (Type != null) dest.Type = (Hl7.Fhir.Model.CodeableConcept)Type.DeepCopy();
					if (Outcome != null) dest.Outcome = (Hl7.Fhir.Model.CodeableConcept)Outcome.DeepCopy();
					if (Onset != null) dest.Onset = (Hl7.Fhir.Model.Element)Onset.DeepCopy();
					if (NoteElement != null) dest.NoteElement = (Hl7.Fhir.Model.FhirString)NoteElement.DeepCopy();
					return dest;
				}
				else
					throw Error.Argument(nameof(other), "Can only copy to an object of the same type");
			}

			public override IDeepCopyable DeepCopy()
			{
				return CopyTo(new FamilyHistoryRelationConditionComponent());
			}

			public override bool Matches(IDeepComparable other)
			{
				var otherT = other as FamilyHistoryRelationConditionComponent;
				if (otherT is null) return false;

				if (!base.Matches(otherT)) return false;
				if (!DeepComparable.Matches(Type, otherT.Type)) return false;
				if (!DeepComparable.Matches(Outcome, otherT.Outcome)) return false;
				if (!DeepComparable.Matches(Onset, otherT.Onset)) return false;
				if (!DeepComparable.Matches(NoteElement, otherT.NoteElement)) return false;

				return true;
			}

			public override bool IsExactly(IDeepComparable other)
			{
				var otherT = other as FamilyHistoryRelationConditionComponent;
				if (otherT is null) return false;

				if (!base.IsExactly(otherT)) return false;
				if (!DeepComparable.IsExactly(Type, otherT.Type)) return false;
				if (!DeepComparable.IsExactly(Outcome, otherT.Outcome)) return false;
				if (!DeepComparable.IsExactly(Onset, otherT.Onset)) return false;
				if (!DeepComparable.IsExactly(NoteElement, otherT.NoteElement)) return false;

				return true;
			}

		}


		[FhirType("FamilyHistoryRelationComponent")]
		[DataContract]
		public partial class FamilyHistoryRelationComponent : Hl7.Fhir.Model.Element
		{
			/// <summary>
			/// The family member described
			/// </summary>
			[FhirElement("name", InSummary = true, Order = 40)]
			[DataMember]
			public Hl7.Fhir.Model.FhirString NameElement { get; set; }

			/// <summary>
			/// The family member described
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
			/// Relationship to the subject
			/// </summary>
			[FhirElement("relationship", InSummary = true, Order = 50)]
			[Cardinality(Min = 1, Max = 1)]
			[DataMember]
			public Hl7.Fhir.Model.CodeableConcept Relationship { get; set; }

			/// <summary>
			/// (approximate) date of birth
			/// </summary>
			[FhirElement("born", InSummary = true, Order = 60, Choice = ChoiceType.DatatypeChoice)]
			[AllowedTypes(typeof(Hl7.Fhir.Model.Period), typeof(Hl7.Fhir.Model.Date), typeof(Hl7.Fhir.Model.FhirString))]
			[DataMember]
			public Hl7.Fhir.Model.Element Born { get; set; }

			/// <summary>
			/// Dead? How old/when?
			/// </summary>
			[FhirElement("deceased", InSummary = true, Order = 70, Choice = ChoiceType.DatatypeChoice)]
			[AllowedTypes(typeof(Hl7.Fhir.Model.FhirBoolean), typeof(Hl7.Fhir.Model.Age), typeof(Hl7.Fhir.Model.Range), typeof(Hl7.Fhir.Model.Date), typeof(Hl7.Fhir.Model.FhirString))]
			[DataMember]
			public Hl7.Fhir.Model.Element Deceased { get; set; }

			/// <summary>
			/// General note about related person
			/// </summary>
			[FhirElement("note", InSummary = true, Order = 80)]
			[DataMember]
			public Hl7.Fhir.Model.FhirString NoteElement { get; set; }

			/// <summary>
			/// General note about related person
			/// </summary>
			/// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
			[NotMapped]
			[IgnoreDataMember]
			public string Note
			{
				get { return NoteElement?.Value; }
				set
				{
					if (value is null)
						NoteElement = null;
					else
						NoteElement = new Hl7.Fhir.Model.FhirString(value);
				}
			}

			/// <summary>
			/// Condition that the related person had
			/// </summary>
			[FhirElement("condition", InSummary = true, Order = 90)]
			[Cardinality(Min = 0, Max = -1)]
			[DataMember]
			public List<Hl7.Fhir.Model.FamilyHistory.FamilyHistoryRelationConditionComponent> Condition { get; set; }

			public override IDeepCopyable CopyTo(IDeepCopyable other)
			{
				var dest = other as FamilyHistoryRelationComponent;

				if (dest != null)
				{
					base.CopyTo(dest);
					if (NameElement != null) dest.NameElement = (Hl7.Fhir.Model.FhirString)NameElement.DeepCopy();
					if (Relationship != null) dest.Relationship = (Hl7.Fhir.Model.CodeableConcept)Relationship.DeepCopy();
					if (Born != null) dest.Born = (Hl7.Fhir.Model.Element)Born.DeepCopy();
					if (Deceased != null) dest.Deceased = (Hl7.Fhir.Model.Element)Deceased.DeepCopy();
					if (NoteElement != null) dest.NoteElement = (Hl7.Fhir.Model.FhirString)NoteElement.DeepCopy();
					if (Condition != null) dest.Condition = new List<Hl7.Fhir.Model.FamilyHistory.FamilyHistoryRelationConditionComponent>(Condition.DeepCopy());
					return dest;
				}
				else
					throw Error.Argument(nameof(other), "Can only copy to an object of the same type");
			}

			public override IDeepCopyable DeepCopy()
			{
				return CopyTo(new FamilyHistoryRelationComponent());
			}

			public override bool Matches(IDeepComparable other)
			{
				var otherT = other as FamilyHistoryRelationComponent;
				if (otherT is null) return false;

				if (!base.Matches(otherT)) return false;
				if (!DeepComparable.Matches(NameElement, otherT.NameElement)) return false;
				if (!DeepComparable.Matches(Relationship, otherT.Relationship)) return false;
				if (!DeepComparable.Matches(Born, otherT.Born)) return false;
				if (!DeepComparable.Matches(Deceased, otherT.Deceased)) return false;
				if (!DeepComparable.Matches(NoteElement, otherT.NoteElement)) return false;
				if (!DeepComparable.Matches(Condition, otherT.Condition)) return false;

				return true;
			}

			public override bool IsExactly(IDeepComparable other)
			{
				var otherT = other as FamilyHistoryRelationComponent;
				if (otherT is null) return false;

				if (!base.IsExactly(otherT)) return false;
				if (!DeepComparable.IsExactly(NameElement, otherT.NameElement)) return false;
				if (!DeepComparable.IsExactly(Relationship, otherT.Relationship)) return false;
				if (!DeepComparable.IsExactly(Born, otherT.Born)) return false;
				if (!DeepComparable.IsExactly(Deceased, otherT.Deceased)) return false;
				if (!DeepComparable.IsExactly(NoteElement, otherT.NoteElement)) return false;
				if (!DeepComparable.IsExactly(Condition, otherT.Condition)) return false;

				return true;
			}

		}


		/// <summary>
		/// External Id(s) for this record
		/// </summary>
		[FhirElement("identifier", InSummary = true, Order = 70)]
		[Cardinality(Min = 0, Max = -1)]
		[DataMember]
		public List<Hl7.Fhir.Model.Identifier> Identifier { get; set; }

		/// <summary>
		/// Patient history is about
		/// </summary>
		[FhirElement("subject", InSummary = true, Order = 80)]
		[References("Patient")]
		[Cardinality(Min = 1, Max = 1)]
		[DataMember]
		public Hl7.Fhir.Model.ResourceReference Subject { get; set; }

		/// <summary>
		/// Additional details not covered elsewhere
		/// </summary>
		[FhirElement("note", Order = 90)]
		[DataMember]
		public Hl7.Fhir.Model.FhirString NoteElement { get; set; }

		/// <summary>
		/// Additional details not covered elsewhere
		/// </summary>
		/// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
		[NotMapped]
		[IgnoreDataMember]
		public string Note
		{
			get { return NoteElement?.Value; }
			set
			{
				if (value is null)
					NoteElement = null;
				else
					NoteElement = new Hl7.Fhir.Model.FhirString(value);
			}
		}

		/// <summary>
		/// Relative described by history
		/// </summary>
		[FhirElement("relation", Order = 100)]
		[Cardinality(Min = 0, Max = -1)]
		[DataMember]
		public List<Hl7.Fhir.Model.FamilyHistory.FamilyHistoryRelationComponent> Relation { get; set; }

		public override IDeepCopyable CopyTo(IDeepCopyable other)
		{
			var dest = other as FamilyHistory;

			if (dest != null)
			{
				base.CopyTo(dest);
				if (Identifier != null) dest.Identifier = new List<Hl7.Fhir.Model.Identifier>(Identifier.DeepCopy());
				if (Subject != null) dest.Subject = (Hl7.Fhir.Model.ResourceReference)Subject.DeepCopy();
				if (NoteElement != null) dest.NoteElement = (Hl7.Fhir.Model.FhirString)NoteElement.DeepCopy();
				if (Relation != null) dest.Relation = new List<Hl7.Fhir.Model.FamilyHistory.FamilyHistoryRelationComponent>(Relation.DeepCopy());
				return dest;
			}
			else
				throw Error.Argument(nameof(other), "Can only copy to an object of the same type");
		}

		public override IDeepCopyable DeepCopy()
		{
			return CopyTo(new FamilyHistory());
		}

		public override bool Matches(IDeepComparable other)
		{
			var otherT = other as FamilyHistory;
			if (otherT is null) return false;

			if (!base.Matches(otherT)) return false;
			if (!DeepComparable.Matches(Identifier, otherT.Identifier)) return false;
			if (!DeepComparable.Matches(Subject, otherT.Subject)) return false;
			if (!DeepComparable.Matches(NoteElement, otherT.NoteElement)) return false;
			if (!DeepComparable.Matches(Relation, otherT.Relation)) return false;

			return true;
		}

		public override bool IsExactly(IDeepComparable other)
		{
			var otherT = other as FamilyHistory;
			if (otherT is null) return false;

			if (!base.IsExactly(otherT)) return false;
			if (!DeepComparable.IsExactly(Identifier, otherT.Identifier)) return false;
			if (!DeepComparable.IsExactly(Subject, otherT.Subject)) return false;
			if (!DeepComparable.IsExactly(NoteElement, otherT.NoteElement)) return false;
			if (!DeepComparable.IsExactly(Relation, otherT.Relation)) return false;

			return true;
		}

	}

}
