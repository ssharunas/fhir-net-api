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
	/// Immunization profile
	/// </summary>
	[FhirType("ImmunizationRecommendation", IsResource = true)]
	[DataContract]
	public partial class ImmunizationRecommendation : Hl7.Fhir.Model.Resource
	{
		[FhirType("ImmunizationRecommendationRecommendationDateCriterionComponent")]
		[DataContract]
		public partial class ImmunizationRecommendationRecommendationDateCriterionComponent : Hl7.Fhir.Model.Element
		{
			/// <summary>
			/// Type of date
			/// </summary>
			[FhirElement("code", InSummary = true, Order = 40)]
			[Cardinality(Min = 1, Max = 1)]
			[DataMember]
			public Hl7.Fhir.Model.CodeableConcept Code { get; set; }

			/// <summary>
			/// Recommended date
			/// </summary>
			[FhirElement("value", InSummary = true, Order = 50)]
			[Cardinality(Min = 1, Max = 1)]
			[DataMember]
			public Hl7.Fhir.Model.FhirDateTime ValueElement { get; set; }

			/// <summary>
			/// Recommended date
			/// </summary>
			/// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
			[NotMapped]
			[IgnoreDataMember]
			public string Value
			{
				get { return ValueElement?.Value; }
				set
				{
					if (value is null)
						ValueElement = null;
					else
						ValueElement = new Hl7.Fhir.Model.FhirDateTime(value);
				}
			}

			public override IDeepCopyable CopyTo(IDeepCopyable other)
			{
				var dest = other as ImmunizationRecommendationRecommendationDateCriterionComponent;

				if (dest != null)
				{
					base.CopyTo(dest);
					if (Code != null) dest.Code = (Hl7.Fhir.Model.CodeableConcept)Code.DeepCopy();
					if (ValueElement != null) dest.ValueElement = (Hl7.Fhir.Model.FhirDateTime)ValueElement.DeepCopy();
					return dest;
				}
				else
					throw Error.Argument(nameof(other), "Can only copy to an object of the same type");
			}

			public override IDeepCopyable DeepCopy()
			{
				return CopyTo(new ImmunizationRecommendationRecommendationDateCriterionComponent());
			}

			public override bool Matches(IDeepComparable other)
			{
				var otherT = other as ImmunizationRecommendationRecommendationDateCriterionComponent;
				if (otherT is null) return false;

				if (!base.Matches(otherT)) return false;
				if (!DeepComparable.Matches(Code, otherT.Code)) return false;
				if (!DeepComparable.Matches(ValueElement, otherT.ValueElement)) return false;

				return true;
			}

			public override bool IsExactly(IDeepComparable other)
			{
				var otherT = other as ImmunizationRecommendationRecommendationDateCriterionComponent;
				if (otherT is null) return false;

				if (!base.IsExactly(otherT)) return false;
				if (!DeepComparable.IsExactly(Code, otherT.Code)) return false;
				if (!DeepComparable.IsExactly(ValueElement, otherT.ValueElement)) return false;

				return true;
			}

		}


		[FhirType("ImmunizationRecommendationRecommendationProtocolComponent")]
		[DataContract]
		public partial class ImmunizationRecommendationRecommendationProtocolComponent : Hl7.Fhir.Model.Element
		{
			/// <summary>
			/// Number of dose within sequence
			/// </summary>
			[FhirElement("doseSequence", InSummary = true, Order = 40)]
			[DataMember]
			public Hl7.Fhir.Model.Integer DoseSequenceElement { get; set; }

			/// <summary>
			/// Number of dose within sequence
			/// </summary>
			/// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
			[NotMapped]
			[IgnoreDataMember]
			public int? DoseSequence
			{
				get { return DoseSequenceElement?.Value; }
				set
				{
					if (value is null)
						DoseSequenceElement = null;
					else
						DoseSequenceElement = new Hl7.Fhir.Model.Integer(value);
				}
			}

			/// <summary>
			/// Protocol details
			/// </summary>
			[FhirElement("description", InSummary = true, Order = 50)]
			[DataMember]
			public Hl7.Fhir.Model.FhirString DescriptionElement { get; set; }

			/// <summary>
			/// Protocol details
			/// </summary>
			/// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
			[NotMapped]
			[IgnoreDataMember]
			public string Description
			{
				get { return DescriptionElement?.Value; }
				set
				{
					if (value is null)
						DescriptionElement = null;
					else
						DescriptionElement = new Hl7.Fhir.Model.FhirString(value);
				}
			}

			/// <summary>
			/// Who is responsible for protocol
			/// </summary>
			[FhirElement("authority", InSummary = true, Order = 60)]
			[References("Organization")]
			[DataMember]
			public Hl7.Fhir.Model.ResourceReference Authority { get; set; }

			/// <summary>
			/// Name of vaccination series
			/// </summary>
			[FhirElement("series", InSummary = true, Order = 70)]
			[DataMember]
			public Hl7.Fhir.Model.FhirString SeriesElement { get; set; }

			/// <summary>
			/// Name of vaccination series
			/// </summary>
			/// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
			[NotMapped]
			[IgnoreDataMember]
			public string Series
			{
				get { return SeriesElement?.Value; }
				set
				{
					if (value is null)
						SeriesElement = null;
					else
						SeriesElement = new Hl7.Fhir.Model.FhirString(value);
				}
			}

			public override IDeepCopyable CopyTo(IDeepCopyable other)
			{
				var dest = other as ImmunizationRecommendationRecommendationProtocolComponent;

				if (dest != null)
				{
					base.CopyTo(dest);
					if (DoseSequenceElement != null) dest.DoseSequenceElement = (Hl7.Fhir.Model.Integer)DoseSequenceElement.DeepCopy();
					if (DescriptionElement != null) dest.DescriptionElement = (Hl7.Fhir.Model.FhirString)DescriptionElement.DeepCopy();
					if (Authority != null) dest.Authority = (Hl7.Fhir.Model.ResourceReference)Authority.DeepCopy();
					if (SeriesElement != null) dest.SeriesElement = (Hl7.Fhir.Model.FhirString)SeriesElement.DeepCopy();
					return dest;
				}
				else
					throw Error.Argument(nameof(other), "Can only copy to an object of the same type");
			}

			public override IDeepCopyable DeepCopy()
			{
				return CopyTo(new ImmunizationRecommendationRecommendationProtocolComponent());
			}

			public override bool Matches(IDeepComparable other)
			{
				var otherT = other as ImmunizationRecommendationRecommendationProtocolComponent;
				if (otherT is null) return false;

				if (!base.Matches(otherT)) return false;
				if (!DeepComparable.Matches(DoseSequenceElement, otherT.DoseSequenceElement)) return false;
				if (!DeepComparable.Matches(DescriptionElement, otherT.DescriptionElement)) return false;
				if (!DeepComparable.Matches(Authority, otherT.Authority)) return false;
				if (!DeepComparable.Matches(SeriesElement, otherT.SeriesElement)) return false;

				return true;
			}

			public override bool IsExactly(IDeepComparable other)
			{
				var otherT = other as ImmunizationRecommendationRecommendationProtocolComponent;
				if (otherT is null) return false;

				if (!base.IsExactly(otherT)) return false;
				if (!DeepComparable.IsExactly(DoseSequenceElement, otherT.DoseSequenceElement)) return false;
				if (!DeepComparable.IsExactly(DescriptionElement, otherT.DescriptionElement)) return false;
				if (!DeepComparable.IsExactly(Authority, otherT.Authority)) return false;
				if (!DeepComparable.IsExactly(SeriesElement, otherT.SeriesElement)) return false;

				return true;
			}

		}


		[FhirType("ImmunizationRecommendationRecommendationComponent")]
		[DataContract]
		public partial class ImmunizationRecommendationRecommendationComponent : Hl7.Fhir.Model.Element
		{
			/// <summary>
			/// Date recommendation created
			/// </summary>
			[FhirElement("date", InSummary = true, Order = 40)]
			[Cardinality(Min = 1, Max = 1)]
			[DataMember]
			public Hl7.Fhir.Model.FhirDateTime DateElement { get; set; }

			/// <summary>
			/// Date recommendation created
			/// </summary>
			/// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
			[NotMapped]
			[IgnoreDataMember]
			public string Date
			{
				get { return DateElement?.Value; }
				set
				{
					if (value is null)
						DateElement = null;
					else
						DateElement = new Hl7.Fhir.Model.FhirDateTime(value);
				}
			}

			/// <summary>
			/// Vaccine recommendation applies to
			/// </summary>
			[FhirElement("vaccineType", InSummary = true, Order = 50)]
			[Cardinality(Min = 1, Max = 1)]
			[DataMember]
			public Hl7.Fhir.Model.CodeableConcept VaccineType { get; set; }

			/// <summary>
			/// Recommended dose number
			/// </summary>
			[FhirElement("doseNumber", InSummary = true, Order = 60)]
			[DataMember]
			public Hl7.Fhir.Model.Integer DoseNumberElement { get; set; }

			/// <summary>
			/// Recommended dose number
			/// </summary>
			/// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
			[NotMapped]
			[IgnoreDataMember]
			public int? DoseNumber
			{
				get { return DoseNumberElement?.Value; }
				set
				{
					if (value is null)
						DoseNumberElement = null;
					else
						DoseNumberElement = new Hl7.Fhir.Model.Integer(value);
				}
			}

			/// <summary>
			/// Vaccine administration status
			/// </summary>
			[FhirElement("forecastStatus", InSummary = true, Order = 70)]
			[Cardinality(Min = 1, Max = 1)]
			[DataMember]
			public Hl7.Fhir.Model.CodeableConcept ForecastStatus { get; set; }

			/// <summary>
			/// Dates governing proposed immunization
			/// </summary>
			[FhirElement("dateCriterion", InSummary = true, Order = 80)]
			[Cardinality(Min = 0, Max = -1)]
			[DataMember]
			public List<Hl7.Fhir.Model.ImmunizationRecommendation.ImmunizationRecommendationRecommendationDateCriterionComponent> DateCriterion { get; set; }

			/// <summary>
			/// Protocol used by recommendation
			/// </summary>
			[FhirElement("protocol", InSummary = true, Order = 90)]
			[DataMember]
			public Hl7.Fhir.Model.ImmunizationRecommendation.ImmunizationRecommendationRecommendationProtocolComponent Protocol { get; set; }

			/// <summary>
			/// Past immunizations supporting recommendation
			/// </summary>
			[FhirElement("supportingImmunization", InSummary = true, Order = 100)]
			[References("Immunization")]
			[Cardinality(Min = 0, Max = -1)]
			[DataMember]
			public List<Hl7.Fhir.Model.ResourceReference> SupportingImmunization { get; set; }

			/// <summary>
			/// Patient observations supporting recommendation
			/// </summary>
			[FhirElement("supportingPatientInformation", InSummary = true, Order = 110)]
			[References("Observation", "AdverseReaction", "AllergyIntolerance")]
			[Cardinality(Min = 0, Max = -1)]
			[DataMember]
			public List<Hl7.Fhir.Model.ResourceReference> SupportingPatientInformation { get; set; }

			public override IDeepCopyable CopyTo(IDeepCopyable other)
			{
				var dest = other as ImmunizationRecommendationRecommendationComponent;

				if (dest != null)
				{
					base.CopyTo(dest);
					if (DateElement != null) dest.DateElement = (Hl7.Fhir.Model.FhirDateTime)DateElement.DeepCopy();
					if (VaccineType != null) dest.VaccineType = (Hl7.Fhir.Model.CodeableConcept)VaccineType.DeepCopy();
					if (DoseNumberElement != null) dest.DoseNumberElement = (Hl7.Fhir.Model.Integer)DoseNumberElement.DeepCopy();
					if (ForecastStatus != null) dest.ForecastStatus = (Hl7.Fhir.Model.CodeableConcept)ForecastStatus.DeepCopy();
					if (DateCriterion != null) dest.DateCriterion = new List<Hl7.Fhir.Model.ImmunizationRecommendation.ImmunizationRecommendationRecommendationDateCriterionComponent>(DateCriterion.DeepCopy());
					if (Protocol != null) dest.Protocol = (Hl7.Fhir.Model.ImmunizationRecommendation.ImmunizationRecommendationRecommendationProtocolComponent)Protocol.DeepCopy();
					if (SupportingImmunization != null) dest.SupportingImmunization = new List<Hl7.Fhir.Model.ResourceReference>(SupportingImmunization.DeepCopy());
					if (SupportingPatientInformation != null) dest.SupportingPatientInformation = new List<Hl7.Fhir.Model.ResourceReference>(SupportingPatientInformation.DeepCopy());
					return dest;
				}
				else
					throw Error.Argument(nameof(other), "Can only copy to an object of the same type");
			}

			public override IDeepCopyable DeepCopy()
			{
				return CopyTo(new ImmunizationRecommendationRecommendationComponent());
			}

			public override bool Matches(IDeepComparable other)
			{
				var otherT = other as ImmunizationRecommendationRecommendationComponent;
				if (otherT is null) return false;

				if (!base.Matches(otherT)) return false;
				if (!DeepComparable.Matches(DateElement, otherT.DateElement)) return false;
				if (!DeepComparable.Matches(VaccineType, otherT.VaccineType)) return false;
				if (!DeepComparable.Matches(DoseNumberElement, otherT.DoseNumberElement)) return false;
				if (!DeepComparable.Matches(ForecastStatus, otherT.ForecastStatus)) return false;
				if (!DeepComparable.Matches(DateCriterion, otherT.DateCriterion)) return false;
				if (!DeepComparable.Matches(Protocol, otherT.Protocol)) return false;
				if (!DeepComparable.Matches(SupportingImmunization, otherT.SupportingImmunization)) return false;
				if (!DeepComparable.Matches(SupportingPatientInformation, otherT.SupportingPatientInformation)) return false;

				return true;
			}

			public override bool IsExactly(IDeepComparable other)
			{
				var otherT = other as ImmunizationRecommendationRecommendationComponent;
				if (otherT is null) return false;

				if (!base.IsExactly(otherT)) return false;
				if (!DeepComparable.IsExactly(DateElement, otherT.DateElement)) return false;
				if (!DeepComparable.IsExactly(VaccineType, otherT.VaccineType)) return false;
				if (!DeepComparable.IsExactly(DoseNumberElement, otherT.DoseNumberElement)) return false;
				if (!DeepComparable.IsExactly(ForecastStatus, otherT.ForecastStatus)) return false;
				if (!DeepComparable.IsExactly(DateCriterion, otherT.DateCriterion)) return false;
				if (!DeepComparable.IsExactly(Protocol, otherT.Protocol)) return false;
				if (!DeepComparable.IsExactly(SupportingImmunization, otherT.SupportingImmunization)) return false;
				if (!DeepComparable.IsExactly(SupportingPatientInformation, otherT.SupportingPatientInformation)) return false;

				return true;
			}

		}


		/// <summary>
		/// Business identifier
		/// </summary>
		[FhirElement("identifier", Order = 70)]
		[Cardinality(Min = 0, Max = -1)]
		[DataMember]
		public List<Hl7.Fhir.Model.Identifier> Identifier { get; set; }

		/// <summary>
		/// Who this profile is for
		/// </summary>
		[FhirElement("subject", Order = 80)]
		[References("Patient")]
		[Cardinality(Min = 1, Max = 1)]
		[DataMember]
		public Hl7.Fhir.Model.ResourceReference Subject { get; set; }

		/// <summary>
		/// Vaccine administration recommendations
		/// </summary>
		[FhirElement("recommendation", Order = 90)]
		[Cardinality(Min = 1, Max = -1)]
		[DataMember]
		public List<Hl7.Fhir.Model.ImmunizationRecommendation.ImmunizationRecommendationRecommendationComponent> Recommendation { get; set; }

		public override IDeepCopyable CopyTo(IDeepCopyable other)
		{
			var dest = other as ImmunizationRecommendation;

			if (dest != null)
			{
				base.CopyTo(dest);
				if (Identifier != null) dest.Identifier = new List<Hl7.Fhir.Model.Identifier>(Identifier.DeepCopy());
				if (Subject != null) dest.Subject = (Hl7.Fhir.Model.ResourceReference)Subject.DeepCopy();
				if (Recommendation != null) dest.Recommendation = new List<Hl7.Fhir.Model.ImmunizationRecommendation.ImmunizationRecommendationRecommendationComponent>(Recommendation.DeepCopy());
				return dest;
			}
			else
				throw Error.Argument(nameof(other), "Can only copy to an object of the same type");
		}

		public override IDeepCopyable DeepCopy()
		{
			return CopyTo(new ImmunizationRecommendation());
		}

		public override bool Matches(IDeepComparable other)
		{
			var otherT = other as ImmunizationRecommendation;
			if (otherT is null) return false;

			if (!base.Matches(otherT)) return false;
			if (!DeepComparable.Matches(Identifier, otherT.Identifier)) return false;
			if (!DeepComparable.Matches(Subject, otherT.Subject)) return false;
			if (!DeepComparable.Matches(Recommendation, otherT.Recommendation)) return false;

			return true;
		}

		public override bool IsExactly(IDeepComparable other)
		{
			var otherT = other as ImmunizationRecommendation;
			if (otherT is null) return false;

			if (!base.IsExactly(otherT)) return false;
			if (!DeepComparable.IsExactly(Identifier, otherT.Identifier)) return false;
			if (!DeepComparable.IsExactly(Subject, otherT.Subject)) return false;
			if (!DeepComparable.IsExactly(Recommendation, otherT.Recommendation)) return false;

			return true;
		}

	}

}
