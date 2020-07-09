using Hl7.Fhir.Introspection;
using Hl7.Fhir.Support;
using Hl7.Fhir.Validation;
using System.Collections.Generic;
using System.Linq;
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
	/// A set of images produced in single study (one or more series of references images)
	/// </summary>
	[FhirType("ImagingStudy", IsResource = true)]
	[DataContract]
	public partial class ImagingStudy : Hl7.Fhir.Model.Resource
	{
		/// <summary>
		/// Type of acquired image data in the instance
		/// </summary>
		[FhirEnumeration("ImagingModality")]
		public enum ImagingModality
		{
			[EnumLiteral("AR")]
			AR,
			[EnumLiteral("BMD")]
			BMD,
			[EnumLiteral("BDUS")]
			BDUS,
			[EnumLiteral("EPS")]
			EPS,
			[EnumLiteral("CR")]
			CR,
			[EnumLiteral("CT")]
			CT,
			[EnumLiteral("DX")]
			DX,
			[EnumLiteral("ECG")]
			ECG,
			[EnumLiteral("ES")]
			ES,
			[EnumLiteral("XC")]
			XC,
			[EnumLiteral("GM")]
			GM,
			[EnumLiteral("HD")]
			HD,
			[EnumLiteral("IO")]
			IO,
			[EnumLiteral("IVOCT")]
			IVOCT,
			[EnumLiteral("IVUS")]
			IVUS,
			[EnumLiteral("KER")]
			KER,
			[EnumLiteral("LEN")]
			LEN,
			[EnumLiteral("MR")]
			MR,
			[EnumLiteral("MG")]
			MG,
			[EnumLiteral("NM")]
			NM,
			[EnumLiteral("OAM")]
			OAM,
			[EnumLiteral("OCT")]
			OCT,
			[EnumLiteral("OPM")]
			OPM,
			[EnumLiteral("OP")]
			OP,
			[EnumLiteral("OPR")]
			OPR,
			[EnumLiteral("OPT")]
			OPT,
			[EnumLiteral("OPV")]
			OPV,
			[EnumLiteral("PX")]
			PX,
			[EnumLiteral("PT")]
			PT,
			[EnumLiteral("RF")]
			RF,
			[EnumLiteral("RG")]
			RG,
			[EnumLiteral("SM")]
			SM,
			[EnumLiteral("SRF")]
			SRF,
			[EnumLiteral("US")]
			US,
			[EnumLiteral("VA")]
			VA,
			[EnumLiteral("XA")]
			XA,
		}

		/// <summary>
		/// Availability of the resource
		/// </summary>
		[FhirEnumeration("InstanceAvailability")]
		public enum InstanceAvailability
		{
			/// <summary>
			/// Resources are immediately available,.
			/// </summary>
			[EnumLiteral("ONLINE")]
			ONLINE,
			/// <summary>
			/// Resources need to be retrieved by manual intervention.
			/// </summary>
			[EnumLiteral("OFFLINE")]
			OFFLINE,
			/// <summary>
			/// Resources need to be retrieved from relatively slow media.
			/// </summary>
			[EnumLiteral("NEARLINE")]
			NEARLINE,
			/// <summary>
			/// Resources cannot be retrieved.
			/// </summary>
			[EnumLiteral("UNAVAILABLE")]
			UNAVAILABLE,
		}

		/// <summary>
		/// Type of data in the instance
		/// </summary>
		[FhirEnumeration("Modality")]
		public enum Modality
		{
			[EnumLiteral("AR")]
			AR,
			[EnumLiteral("AU")]
			AU,
			[EnumLiteral("BDUS")]
			BDUS,
			[EnumLiteral("BI")]
			BI,
			[EnumLiteral("BMD")]
			BMD,
			[EnumLiteral("CR")]
			CR,
			[EnumLiteral("CT")]
			CT,
			[EnumLiteral("DG")]
			DG,
			[EnumLiteral("DX")]
			DX,
			[EnumLiteral("ECG")]
			ECG,
			[EnumLiteral("EPS")]
			EPS,
			[EnumLiteral("ES")]
			ES,
			[EnumLiteral("GM")]
			GM,
			[EnumLiteral("HC")]
			HC,
			[EnumLiteral("HD")]
			HD,
			[EnumLiteral("IO")]
			IO,
			[EnumLiteral("IVOCT")]
			IVOCT,
			[EnumLiteral("IVUS")]
			IVUS,
			[EnumLiteral("KER")]
			KER,
			[EnumLiteral("KO")]
			KO,
			[EnumLiteral("LEN")]
			LEN,
			[EnumLiteral("LS")]
			LS,
			[EnumLiteral("MG")]
			MG,
			[EnumLiteral("MR")]
			MR,
			[EnumLiteral("NM")]
			NM,
			[EnumLiteral("OAM")]
			OAM,
			[EnumLiteral("OCT")]
			OCT,
			[EnumLiteral("OP")]
			OP,
			[EnumLiteral("OPM")]
			OPM,
			[EnumLiteral("OPT")]
			OPT,
			[EnumLiteral("OPV")]
			OPV,
			[EnumLiteral("OT")]
			OT,
			[EnumLiteral("PR")]
			PR,
			[EnumLiteral("PT")]
			PT,
			[EnumLiteral("PX")]
			PX,
			[EnumLiteral("REG")]
			REG,
			[EnumLiteral("RF")]
			RF,
			[EnumLiteral("RG")]
			RG,
			[EnumLiteral("RTDOSE")]
			RTDOSE,
			[EnumLiteral("RTIMAGE")]
			RTIMAGE,
			[EnumLiteral("RTPLAN")]
			RTPLAN,
			[EnumLiteral("RTRECORD")]
			RTRECORD,
			[EnumLiteral("RTSTRUCT")]
			RTSTRUCT,
			[EnumLiteral("SEG")]
			SEG,
			[EnumLiteral("SM")]
			SM,
			[EnumLiteral("SMR")]
			SMR,
			[EnumLiteral("SR")]
			SR,
			[EnumLiteral("SRF")]
			SRF,
			[EnumLiteral("TG")]
			TG,
			[EnumLiteral("US")]
			US,
			[EnumLiteral("VA")]
			VA,
			[EnumLiteral("XA")]
			XA,
			[EnumLiteral("XC")]
			XC,
		}

		[FhirType("ImagingStudySeriesComponent")]
		[DataContract]
		public partial class ImagingStudySeriesComponent : Hl7.Fhir.Model.Element
		{
			/// <summary>
			/// Number of this series in overall sequence (0020,0011)
			/// </summary>
			[FhirElement("number", InSummary = true, Order = 40)]
			[DataMember]
			public Hl7.Fhir.Model.Integer NumberElement { get; set; }

			/// <summary>
			/// Number of this series in overall sequence (0020,0011)
			/// </summary>
			/// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
			[NotMapped]
			[IgnoreDataMember]
			public int? Number
			{
				get { return NumberElement?.Value; }
				set
				{
					if (value is null)
						NumberElement = null;
					else
						NumberElement = new Hl7.Fhir.Model.Integer(value);
				}
			}

			/// <summary>
			/// The modality of the instances in the series (0008,0060)
			/// </summary>
			[FhirElement("modality", InSummary = true, Order = 50)]
			[Cardinality(Min = 1, Max = 1)]
			[DataMember]
			public Code<Hl7.Fhir.Model.ImagingStudy.Modality> ModalityElement { get; set; }

			/// <summary>
			/// The modality of the instances in the series (0008,0060)
			/// </summary>
			/// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
			[NotMapped]
			[IgnoreDataMember]
			public Hl7.Fhir.Model.ImagingStudy.Modality? Modality
			{
				get { return ModalityElement?.Value; }
				set
				{
					if (value is null)
						ModalityElement = null;
					else
						ModalityElement = new Code<Hl7.Fhir.Model.ImagingStudy.Modality>(value);
				}
			}

			/// <summary>
			/// Formal identifier for this series (0020,000E)
			/// </summary>
			[FhirElement("uid", InSummary = true, Order = 60)]
			[Cardinality(Min = 1, Max = 1)]
			[DataMember]
			public Hl7.Fhir.Model.Oid UidElement { get; set; }

			/// <summary>
			/// Formal identifier for this series (0020,000E)
			/// </summary>
			/// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
			[NotMapped]
			[IgnoreDataMember]
			public string Uid
			{
				get { return UidElement?.Value; }
				set
				{
					if (value is null)
						UidElement = null;
					else
						UidElement = new Hl7.Fhir.Model.Oid(value);
				}
			}

			/// <summary>
			/// A description of the series (0008,103E)
			/// </summary>
			[FhirElement("description", InSummary = true, Order = 70)]
			[DataMember]
			public Hl7.Fhir.Model.FhirString DescriptionElement { get; set; }

			/// <summary>
			/// A description of the series (0008,103E)
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
			/// Number of Series Related Instances (0020,1209)
			/// </summary>
			[FhirElement("numberOfInstances", InSummary = true, Order = 80)]
			[Cardinality(Min = 1, Max = 1)]
			[DataMember]
			public Hl7.Fhir.Model.Integer NumberOfInstancesElement { get; set; }

			/// <summary>
			/// Number of Series Related Instances (0020,1209)
			/// </summary>
			/// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
			[NotMapped]
			[IgnoreDataMember]
			public int? NumberOfInstances
			{
				get { return NumberOfInstancesElement?.Value; }
				set
				{
					if (value is null)
						NumberOfInstancesElement = null;
					else
						NumberOfInstancesElement = new Hl7.Fhir.Model.Integer(value);
				}
			}

			/// <summary>
			/// ONLINE | OFFLINE | NEARLINE | UNAVAILABLE (0008,0056)
			/// </summary>
			[FhirElement("availability", InSummary = true, Order = 90)]
			[DataMember]
			public Code<Hl7.Fhir.Model.ImagingStudy.InstanceAvailability> AvailabilityElement { get; set; }

			/// <summary>
			/// ONLINE | OFFLINE | NEARLINE | UNAVAILABLE (0008,0056)
			/// </summary>
			/// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
			[NotMapped]
			[IgnoreDataMember]
			public Hl7.Fhir.Model.ImagingStudy.InstanceAvailability? Availability
			{
				get { return AvailabilityElement?.Value; }
				set
				{
					if (value is null)
						AvailabilityElement = null;
					else
						AvailabilityElement = new Code<Hl7.Fhir.Model.ImagingStudy.InstanceAvailability>(value);
				}
			}

			/// <summary>
			/// Retrieve URI (0008,1115 &gt; 0008,1190)
			/// </summary>
			[FhirElement("url", InSummary = true, Order = 100)]
			[DataMember]
			public Hl7.Fhir.Model.FhirUri UrlElement { get; set; }

			/// <summary>
			/// Retrieve URI (0008,1115 &gt; 0008,1190)
			/// </summary>
			/// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
			[NotMapped]
			[IgnoreDataMember]
			public string Url
			{
				get { return UrlElement?.Value; }
				set
				{
					if (value is null)
						UrlElement = null;
					else
						UrlElement = new Hl7.Fhir.Model.FhirUri(value);
				}
			}

			/// <summary>
			/// Body part examined (Map from 0018,0015)
			/// </summary>
			[FhirElement("bodySite", InSummary = true, Order = 110)]
			[DataMember]
			public Hl7.Fhir.Model.Coding BodySite { get; set; }

			/// <summary>
			/// When the series started
			/// </summary>
			[FhirElement("dateTime", InSummary = true, Order = 120)]
			[DataMember]
			public Hl7.Fhir.Model.FhirDateTime DateTimeElement { get; set; }

			/// <summary>
			/// When the series started
			/// </summary>
			/// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
			[NotMapped]
			[IgnoreDataMember]
			public string DateTime
			{
				get { return DateTimeElement?.Value; }
				set
				{
					if (value is null)
						DateTimeElement = null;
					else
						DateTimeElement = new Hl7.Fhir.Model.FhirDateTime(value);
				}
			}

			/// <summary>
			/// A single instance taken from a patient (image or other)
			/// </summary>
			[FhirElement("instance", InSummary = true, Order = 130)]
			[Cardinality(Min = 1, Max = -1)]
			[DataMember]
			public List<Hl7.Fhir.Model.ImagingStudy.ImagingStudySeriesInstanceComponent> Instance { get; set; }

			public override IDeepCopyable CopyTo(IDeepCopyable other)
			{
				var dest = other as ImagingStudySeriesComponent;

				if (dest != null)
				{
					base.CopyTo(dest);
					if (NumberElement != null) dest.NumberElement = (Hl7.Fhir.Model.Integer)NumberElement.DeepCopy();
					if (ModalityElement != null) dest.ModalityElement = (Code<Hl7.Fhir.Model.ImagingStudy.Modality>)ModalityElement.DeepCopy();
					if (UidElement != null) dest.UidElement = (Hl7.Fhir.Model.Oid)UidElement.DeepCopy();
					if (DescriptionElement != null) dest.DescriptionElement = (Hl7.Fhir.Model.FhirString)DescriptionElement.DeepCopy();
					if (NumberOfInstancesElement != null) dest.NumberOfInstancesElement = (Hl7.Fhir.Model.Integer)NumberOfInstancesElement.DeepCopy();
					if (AvailabilityElement != null) dest.AvailabilityElement = (Code<Hl7.Fhir.Model.ImagingStudy.InstanceAvailability>)AvailabilityElement.DeepCopy();
					if (UrlElement != null) dest.UrlElement = (Hl7.Fhir.Model.FhirUri)UrlElement.DeepCopy();
					if (BodySite != null) dest.BodySite = (Hl7.Fhir.Model.Coding)BodySite.DeepCopy();
					if (DateTimeElement != null) dest.DateTimeElement = (Hl7.Fhir.Model.FhirDateTime)DateTimeElement.DeepCopy();
					if (Instance != null) dest.Instance = new List<Hl7.Fhir.Model.ImagingStudy.ImagingStudySeriesInstanceComponent>(Instance.DeepCopy());
					return dest;
				}
				else
					throw Error.Argument(nameof(other), "Can only copy to an object of the same type");
			}

			public override IDeepCopyable DeepCopy()
			{
				return CopyTo(new ImagingStudySeriesComponent());
			}

			public override bool Matches(IDeepComparable other)
			{
				var otherT = other as ImagingStudySeriesComponent;
				if (otherT is null) return false;

				if (!base.Matches(otherT)) return false;
				if (!DeepComparable.Matches(NumberElement, otherT.NumberElement)) return false;
				if (!DeepComparable.Matches(ModalityElement, otherT.ModalityElement)) return false;
				if (!DeepComparable.Matches(UidElement, otherT.UidElement)) return false;
				if (!DeepComparable.Matches(DescriptionElement, otherT.DescriptionElement)) return false;
				if (!DeepComparable.Matches(NumberOfInstancesElement, otherT.NumberOfInstancesElement)) return false;
				if (!DeepComparable.Matches(AvailabilityElement, otherT.AvailabilityElement)) return false;
				if (!DeepComparable.Matches(UrlElement, otherT.UrlElement)) return false;
				if (!DeepComparable.Matches(BodySite, otherT.BodySite)) return false;
				if (!DeepComparable.Matches(DateTimeElement, otherT.DateTimeElement)) return false;
				if (!DeepComparable.Matches(Instance, otherT.Instance)) return false;

				return true;
			}

			public override bool IsExactly(IDeepComparable other)
			{
				var otherT = other as ImagingStudySeriesComponent;
				if (otherT is null) return false;

				if (!base.IsExactly(otherT)) return false;
				if (!DeepComparable.IsExactly(NumberElement, otherT.NumberElement)) return false;
				if (!DeepComparable.IsExactly(ModalityElement, otherT.ModalityElement)) return false;
				if (!DeepComparable.IsExactly(UidElement, otherT.UidElement)) return false;
				if (!DeepComparable.IsExactly(DescriptionElement, otherT.DescriptionElement)) return false;
				if (!DeepComparable.IsExactly(NumberOfInstancesElement, otherT.NumberOfInstancesElement)) return false;
				if (!DeepComparable.IsExactly(AvailabilityElement, otherT.AvailabilityElement)) return false;
				if (!DeepComparable.IsExactly(UrlElement, otherT.UrlElement)) return false;
				if (!DeepComparable.IsExactly(BodySite, otherT.BodySite)) return false;
				if (!DeepComparable.IsExactly(DateTimeElement, otherT.DateTimeElement)) return false;
				if (!DeepComparable.IsExactly(Instance, otherT.Instance)) return false;

				return true;
			}

		}


		[FhirType("ImagingStudySeriesInstanceComponent")]
		[DataContract]
		public partial class ImagingStudySeriesInstanceComponent : Hl7.Fhir.Model.Element
		{
			/// <summary>
			/// The number of this instance in the series (0020,0013)
			/// </summary>
			[FhirElement("number", InSummary = true, Order = 40)]
			[DataMember]
			public Hl7.Fhir.Model.Integer NumberElement { get; set; }

			/// <summary>
			/// The number of this instance in the series (0020,0013)
			/// </summary>
			/// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
			[NotMapped]
			[IgnoreDataMember]
			public int? Number
			{
				get { return NumberElement?.Value; }
				set
				{
					if (value is null)
						NumberElement = null;
					else
						NumberElement = new Hl7.Fhir.Model.Integer(value);
				}
			}

			/// <summary>
			/// Formal identifier for this instance (0008,0018)
			/// </summary>
			[FhirElement("uid", InSummary = true, Order = 50)]
			[Cardinality(Min = 1, Max = 1)]
			[DataMember]
			public Hl7.Fhir.Model.Oid UidElement { get; set; }

			/// <summary>
			/// Formal identifier for this instance (0008,0018)
			/// </summary>
			/// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
			[NotMapped]
			[IgnoreDataMember]
			public string Uid
			{
				get { return UidElement?.Value; }
				set
				{
					if (value is null)
						UidElement = null;
					else
						UidElement = new Hl7.Fhir.Model.Oid(value);
				}
			}

			/// <summary>
			/// DICOM class type (0008,0016)
			/// </summary>
			[FhirElement("sopclass", InSummary = true, Order = 60)]
			[Cardinality(Min = 1, Max = 1)]
			[DataMember]
			public Hl7.Fhir.Model.Oid SopclassElement { get; set; }

			/// <summary>
			/// DICOM class type (0008,0016)
			/// </summary>
			/// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
			[NotMapped]
			[IgnoreDataMember]
			public string Sopclass
			{
				get { return SopclassElement?.Value; }
				set
				{
					if (value is null)
						SopclassElement = null;
					else
						SopclassElement = new Hl7.Fhir.Model.Oid(value);
				}
			}

			/// <summary>
			/// Type of instance (image etc) (0004,1430)
			/// </summary>
			[FhirElement("type", InSummary = true, Order = 70)]
			[DataMember]
			public Hl7.Fhir.Model.FhirString TypeElement { get; set; }

			/// <summary>
			/// Type of instance (image etc) (0004,1430)
			/// </summary>
			/// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
			[NotMapped]
			[IgnoreDataMember]
			public string Type
			{
				get { return TypeElement?.Value; }
				set
				{
					if (value is null)
						TypeElement = null;
					else
						TypeElement = new Hl7.Fhir.Model.FhirString(value);
				}
			}

			/// <summary>
			/// Description (0070,0080 | 0040,A043 &gt; 0008,0104 | 0042,0010 | 0008,0008)
			/// </summary>
			[FhirElement("title", InSummary = true, Order = 80)]
			[DataMember]
			public Hl7.Fhir.Model.FhirString TitleElement { get; set; }

			/// <summary>
			/// Description (0070,0080 | 0040,A043 &gt; 0008,0104 | 0042,0010 | 0008,0008)
			/// </summary>
			/// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
			[NotMapped]
			[IgnoreDataMember]
			public string Title
			{
				get { return TitleElement?.Value; }
				set
				{
					if (value is null)
						TitleElement = null;
					else
						TitleElement = new Hl7.Fhir.Model.FhirString(value);
				}
			}

			/// <summary>
			/// WADO-RS service where instance is available  (0008,1199 &gt; 0008,1190)
			/// </summary>
			[FhirElement("url", InSummary = true, Order = 90)]
			[DataMember]
			public Hl7.Fhir.Model.FhirUri UrlElement { get; set; }

			/// <summary>
			/// WADO-RS service where instance is available  (0008,1199 &gt; 0008,1190)
			/// </summary>
			/// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
			[NotMapped]
			[IgnoreDataMember]
			public string Url
			{
				get { return UrlElement?.Value; }
				set
				{
					if (value is null)
						UrlElement = null;
					else
						UrlElement = new Hl7.Fhir.Model.FhirUri(value);
				}
			}

			/// <summary>
			/// A FHIR resource with content for this instance
			/// </summary>
			[FhirElement("attachment", InSummary = true, Order = 100)]
			[References()]
			[DataMember]
			public Hl7.Fhir.Model.ResourceReference Attachment { get; set; }

			public override IDeepCopyable CopyTo(IDeepCopyable other)
			{
				var dest = other as ImagingStudySeriesInstanceComponent;

				if (dest != null)
				{
					base.CopyTo(dest);
					if (NumberElement != null) dest.NumberElement = (Hl7.Fhir.Model.Integer)NumberElement.DeepCopy();
					if (UidElement != null) dest.UidElement = (Hl7.Fhir.Model.Oid)UidElement.DeepCopy();
					if (SopclassElement != null) dest.SopclassElement = (Hl7.Fhir.Model.Oid)SopclassElement.DeepCopy();
					if (TypeElement != null) dest.TypeElement = (Hl7.Fhir.Model.FhirString)TypeElement.DeepCopy();
					if (TitleElement != null) dest.TitleElement = (Hl7.Fhir.Model.FhirString)TitleElement.DeepCopy();
					if (UrlElement != null) dest.UrlElement = (Hl7.Fhir.Model.FhirUri)UrlElement.DeepCopy();
					if (Attachment != null) dest.Attachment = (Hl7.Fhir.Model.ResourceReference)Attachment.DeepCopy();
					return dest;
				}
				else
					throw Error.Argument(nameof(other), "Can only copy to an object of the same type");
			}

			public override IDeepCopyable DeepCopy()
			{
				return CopyTo(new ImagingStudySeriesInstanceComponent());
			}

			public override bool Matches(IDeepComparable other)
			{
				var otherT = other as ImagingStudySeriesInstanceComponent;
				if (otherT is null) return false;

				if (!base.Matches(otherT)) return false;
				if (!DeepComparable.Matches(NumberElement, otherT.NumberElement)) return false;
				if (!DeepComparable.Matches(UidElement, otherT.UidElement)) return false;
				if (!DeepComparable.Matches(SopclassElement, otherT.SopclassElement)) return false;
				if (!DeepComparable.Matches(TypeElement, otherT.TypeElement)) return false;
				if (!DeepComparable.Matches(TitleElement, otherT.TitleElement)) return false;
				if (!DeepComparable.Matches(UrlElement, otherT.UrlElement)) return false;
				if (!DeepComparable.Matches(Attachment, otherT.Attachment)) return false;

				return true;
			}

			public override bool IsExactly(IDeepComparable other)
			{
				var otherT = other as ImagingStudySeriesInstanceComponent;
				if (otherT is null) return false;

				if (!base.IsExactly(otherT)) return false;
				if (!DeepComparable.IsExactly(NumberElement, otherT.NumberElement)) return false;
				if (!DeepComparable.IsExactly(UidElement, otherT.UidElement)) return false;
				if (!DeepComparable.IsExactly(SopclassElement, otherT.SopclassElement)) return false;
				if (!DeepComparable.IsExactly(TypeElement, otherT.TypeElement)) return false;
				if (!DeepComparable.IsExactly(TitleElement, otherT.TitleElement)) return false;
				if (!DeepComparable.IsExactly(UrlElement, otherT.UrlElement)) return false;
				if (!DeepComparable.IsExactly(Attachment, otherT.Attachment)) return false;

				return true;
			}

		}


		/// <summary>
		/// When the study was performed
		/// </summary>
		[FhirElement("dateTime", Order = 70)]
		[DataMember]
		public Hl7.Fhir.Model.FhirDateTime DateTimeElement { get; set; }

		/// <summary>
		/// When the study was performed
		/// </summary>
		/// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
		[NotMapped]
		[IgnoreDataMember]
		public string DateTime
		{
			get { return DateTimeElement?.Value; }
			set
			{
				if (value is null)
					DateTimeElement = null;
				else
					DateTimeElement = new Hl7.Fhir.Model.FhirDateTime(value);
			}
		}

		/// <summary>
		/// Who the images are of
		/// </summary>
		[FhirElement("subject", Order = 80)]
		[References("Patient")]
		[Cardinality(Min = 1, Max = 1)]
		[DataMember]
		public Hl7.Fhir.Model.ResourceReference Subject { get; set; }

		/// <summary>
		/// Formal identifier for the study (0020,000D)
		/// </summary>
		[FhirElement("uid", Order = 90)]
		[Cardinality(Min = 1, Max = 1)]
		[DataMember]
		public Hl7.Fhir.Model.Oid UidElement { get; set; }

		/// <summary>
		/// Formal identifier for the study (0020,000D)
		/// </summary>
		/// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
		[NotMapped]
		[IgnoreDataMember]
		public string Uid
		{
			get { return UidElement?.Value; }
			set
			{
				if (value is null)
					UidElement = null;
				else
					UidElement = new Hl7.Fhir.Model.Oid(value);
			}
		}

		/// <summary>
		/// Accession Number (0008,0050)
		/// </summary>
		[FhirElement("accessionNo", Order = 100)]
		[DataMember]
		public Hl7.Fhir.Model.Identifier AccessionNo { get; set; }

		/// <summary>
		/// Other identifiers for the study (0020,0010)
		/// </summary>
		[FhirElement("identifier", Order = 110)]
		[Cardinality(Min = 0, Max = -1)]
		[DataMember]
		public List<Hl7.Fhir.Model.Identifier> Identifier { get; set; }

		/// <summary>
		/// Order(s) that caused this study to be performed
		/// </summary>
		[FhirElement("order", Order = 120)]
		[References("DiagnosticOrder")]
		[Cardinality(Min = 0, Max = -1)]
		[DataMember]
		public List<Hl7.Fhir.Model.ResourceReference> Order { get; set; }

		/// <summary>
		/// All series.modality if actual acquisition modalities
		/// </summary>
		[FhirElement("modality", Order = 130)]
		[Cardinality(Min = 0, Max = -1)]
		[DataMember]
		public List<Code<Hl7.Fhir.Model.ImagingStudy.ImagingModality>> Modality_Element { get; set; }

		/// <summary>
		/// All series.modality if actual acquisition modalities
		/// </summary>
		/// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
		[NotMapped]
		[IgnoreDataMember]
		public IEnumerable<Hl7.Fhir.Model.ImagingStudy.ImagingModality?> Modality_
		{
			get { return Modality_Element?.Select(elem => elem.Value); }
			set
			{
				if (value is null)
					Modality_Element = null;
				else
					Modality_Element = new List<Code<Hl7.Fhir.Model.ImagingStudy.ImagingModality>>(value.Select(elem => new Code<Hl7.Fhir.Model.ImagingStudy.ImagingModality>(elem)));
			}
		}

		/// <summary>
		/// Referring physician (0008,0090)
		/// </summary>
		[FhirElement("referrer", Order = 140)]
		[References("Practitioner")]
		[DataMember]
		public Hl7.Fhir.Model.ResourceReference Referrer { get; set; }

		/// <summary>
		/// ONLINE | OFFLINE | NEARLINE | UNAVAILABLE (0008,0056)
		/// </summary>
		[FhirElement("availability", Order = 150)]
		[DataMember]
		public Code<Hl7.Fhir.Model.ImagingStudy.InstanceAvailability> AvailabilityElement { get; set; }

		/// <summary>
		/// ONLINE | OFFLINE | NEARLINE | UNAVAILABLE (0008,0056)
		/// </summary>
		/// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
		[NotMapped]
		[IgnoreDataMember]
		public Hl7.Fhir.Model.ImagingStudy.InstanceAvailability? Availability
		{
			get { return AvailabilityElement?.Value; }
			set
			{
				if (value is null)
					AvailabilityElement = null;
				else
					AvailabilityElement = new Code<Hl7.Fhir.Model.ImagingStudy.InstanceAvailability>(value);
			}
		}

		/// <summary>
		/// Retrieve URI (0008,1190)
		/// </summary>
		[FhirElement("url", Order = 160)]
		[DataMember]
		public Hl7.Fhir.Model.FhirUri UrlElement { get; set; }

		/// <summary>
		/// Retrieve URI (0008,1190)
		/// </summary>
		/// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
		[NotMapped]
		[IgnoreDataMember]
		public string Url
		{
			get { return UrlElement?.Value; }
			set
			{
				if (value is null)
					UrlElement = null;
				else
					UrlElement = new Hl7.Fhir.Model.FhirUri(value);
			}
		}

		/// <summary>
		/// Number of Study Related Series (0020,1206)
		/// </summary>
		[FhirElement("numberOfSeries", Order = 170)]
		[Cardinality(Min = 1, Max = 1)]
		[DataMember]
		public Hl7.Fhir.Model.Integer NumberOfSeriesElement { get; set; }

		/// <summary>
		/// Number of Study Related Series (0020,1206)
		/// </summary>
		/// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
		[NotMapped]
		[IgnoreDataMember]
		public int? NumberOfSeries
		{
			get { return NumberOfSeriesElement?.Value; }
			set
			{
				if (value is null)
					NumberOfSeriesElement = null;
				else
					NumberOfSeriesElement = new Hl7.Fhir.Model.Integer(value);
			}
		}

		/// <summary>
		/// Number of Study Related Instances (0020,1208)
		/// </summary>
		[FhirElement("numberOfInstances", Order = 180)]
		[Cardinality(Min = 1, Max = 1)]
		[DataMember]
		public Hl7.Fhir.Model.Integer NumberOfInstancesElement { get; set; }

		/// <summary>
		/// Number of Study Related Instances (0020,1208)
		/// </summary>
		/// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
		[NotMapped]
		[IgnoreDataMember]
		public int? NumberOfInstances
		{
			get { return NumberOfInstancesElement?.Value; }
			set
			{
				if (value is null)
					NumberOfInstancesElement = null;
				else
					NumberOfInstancesElement = new Hl7.Fhir.Model.Integer(value);
			}
		}

		/// <summary>
		/// Diagnoses etc with request (0040,1002)
		/// </summary>
		[FhirElement("clinicalInformation", Order = 190)]
		[DataMember]
		public Hl7.Fhir.Model.FhirString ClinicalInformationElement { get; set; }

		/// <summary>
		/// Diagnoses etc with request (0040,1002)
		/// </summary>
		/// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
		[NotMapped]
		[IgnoreDataMember]
		public string ClinicalInformation
		{
			get { return ClinicalInformationElement?.Value; }
			set
			{
				if (value is null)
					ClinicalInformationElement = null;
				else
					ClinicalInformationElement = new Hl7.Fhir.Model.FhirString(value);
			}
		}

		/// <summary>
		/// Type of procedure performed (0008,1032)
		/// </summary>
		[FhirElement("procedure", Order = 200)]
		[Cardinality(Min = 0, Max = -1)]
		[DataMember]
		public List<Hl7.Fhir.Model.Coding> Procedure { get; set; }

		/// <summary>
		/// Who interpreted images (0008,1060)
		/// </summary>
		[FhirElement("interpreter", Order = 210)]
		[References("Practitioner")]
		[DataMember]
		public Hl7.Fhir.Model.ResourceReference Interpreter { get; set; }

		/// <summary>
		/// Institution-generated description (0008,1030)
		/// </summary>
		[FhirElement("description", Order = 220)]
		[DataMember]
		public Hl7.Fhir.Model.FhirString DescriptionElement { get; set; }

		/// <summary>
		/// Institution-generated description (0008,1030)
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
		/// Each study has one or more series of instances
		/// </summary>
		[FhirElement("series", Order = 230)]
		[Cardinality(Min = 0, Max = -1)]
		[DataMember]
		public List<Hl7.Fhir.Model.ImagingStudy.ImagingStudySeriesComponent> Series { get; set; }

		public override IDeepCopyable CopyTo(IDeepCopyable other)
		{
			var dest = other as ImagingStudy;

			if (dest != null)
			{
				base.CopyTo(dest);
				if (DateTimeElement != null) dest.DateTimeElement = (Hl7.Fhir.Model.FhirDateTime)DateTimeElement.DeepCopy();
				if (Subject != null) dest.Subject = (Hl7.Fhir.Model.ResourceReference)Subject.DeepCopy();
				if (UidElement != null) dest.UidElement = (Hl7.Fhir.Model.Oid)UidElement.DeepCopy();
				if (AccessionNo != null) dest.AccessionNo = (Hl7.Fhir.Model.Identifier)AccessionNo.DeepCopy();
				if (Identifier != null) dest.Identifier = new List<Hl7.Fhir.Model.Identifier>(Identifier.DeepCopy());
				if (Order != null) dest.Order = new List<Hl7.Fhir.Model.ResourceReference>(Order.DeepCopy());
				if (Modality_Element != null) dest.Modality_Element = new List<Code<Hl7.Fhir.Model.ImagingStudy.ImagingModality>>(Modality_Element.DeepCopy());
				if (Referrer != null) dest.Referrer = (Hl7.Fhir.Model.ResourceReference)Referrer.DeepCopy();
				if (AvailabilityElement != null) dest.AvailabilityElement = (Code<Hl7.Fhir.Model.ImagingStudy.InstanceAvailability>)AvailabilityElement.DeepCopy();
				if (UrlElement != null) dest.UrlElement = (Hl7.Fhir.Model.FhirUri)UrlElement.DeepCopy();
				if (NumberOfSeriesElement != null) dest.NumberOfSeriesElement = (Hl7.Fhir.Model.Integer)NumberOfSeriesElement.DeepCopy();
				if (NumberOfInstancesElement != null) dest.NumberOfInstancesElement = (Hl7.Fhir.Model.Integer)NumberOfInstancesElement.DeepCopy();
				if (ClinicalInformationElement != null) dest.ClinicalInformationElement = (Hl7.Fhir.Model.FhirString)ClinicalInformationElement.DeepCopy();
				if (Procedure != null) dest.Procedure = new List<Hl7.Fhir.Model.Coding>(Procedure.DeepCopy());
				if (Interpreter != null) dest.Interpreter = (Hl7.Fhir.Model.ResourceReference)Interpreter.DeepCopy();
				if (DescriptionElement != null) dest.DescriptionElement = (Hl7.Fhir.Model.FhirString)DescriptionElement.DeepCopy();
				if (Series != null) dest.Series = new List<Hl7.Fhir.Model.ImagingStudy.ImagingStudySeriesComponent>(Series.DeepCopy());
				return dest;
			}
			else
				throw Error.Argument(nameof(other), "Can only copy to an object of the same type");
		}

		public override IDeepCopyable DeepCopy()
		{
			return CopyTo(new ImagingStudy());
		}

		public override bool Matches(IDeepComparable other)
		{
			var otherT = other as ImagingStudy;
			if (otherT is null) return false;

			if (!base.Matches(otherT)) return false;
			if (!DeepComparable.Matches(DateTimeElement, otherT.DateTimeElement)) return false;
			if (!DeepComparable.Matches(Subject, otherT.Subject)) return false;
			if (!DeepComparable.Matches(UidElement, otherT.UidElement)) return false;
			if (!DeepComparable.Matches(AccessionNo, otherT.AccessionNo)) return false;
			if (!DeepComparable.Matches(Identifier, otherT.Identifier)) return false;
			if (!DeepComparable.Matches(Order, otherT.Order)) return false;
			if (!DeepComparable.Matches(Modality_Element, otherT.Modality_Element)) return false;
			if (!DeepComparable.Matches(Referrer, otherT.Referrer)) return false;
			if (!DeepComparable.Matches(AvailabilityElement, otherT.AvailabilityElement)) return false;
			if (!DeepComparable.Matches(UrlElement, otherT.UrlElement)) return false;
			if (!DeepComparable.Matches(NumberOfSeriesElement, otherT.NumberOfSeriesElement)) return false;
			if (!DeepComparable.Matches(NumberOfInstancesElement, otherT.NumberOfInstancesElement)) return false;
			if (!DeepComparable.Matches(ClinicalInformationElement, otherT.ClinicalInformationElement)) return false;
			if (!DeepComparable.Matches(Procedure, otherT.Procedure)) return false;
			if (!DeepComparable.Matches(Interpreter, otherT.Interpreter)) return false;
			if (!DeepComparable.Matches(DescriptionElement, otherT.DescriptionElement)) return false;
			if (!DeepComparable.Matches(Series, otherT.Series)) return false;

			return true;
		}

		public override bool IsExactly(IDeepComparable other)
		{
			var otherT = other as ImagingStudy;
			if (otherT is null) return false;

			if (!base.IsExactly(otherT)) return false;
			if (!DeepComparable.IsExactly(DateTimeElement, otherT.DateTimeElement)) return false;
			if (!DeepComparable.IsExactly(Subject, otherT.Subject)) return false;
			if (!DeepComparable.IsExactly(UidElement, otherT.UidElement)) return false;
			if (!DeepComparable.IsExactly(AccessionNo, otherT.AccessionNo)) return false;
			if (!DeepComparable.IsExactly(Identifier, otherT.Identifier)) return false;
			if (!DeepComparable.IsExactly(Order, otherT.Order)) return false;
			if (!DeepComparable.IsExactly(Modality_Element, otherT.Modality_Element)) return false;
			if (!DeepComparable.IsExactly(Referrer, otherT.Referrer)) return false;
			if (!DeepComparable.IsExactly(AvailabilityElement, otherT.AvailabilityElement)) return false;
			if (!DeepComparable.IsExactly(UrlElement, otherT.UrlElement)) return false;
			if (!DeepComparable.IsExactly(NumberOfSeriesElement, otherT.NumberOfSeriesElement)) return false;
			if (!DeepComparable.IsExactly(NumberOfInstancesElement, otherT.NumberOfInstancesElement)) return false;
			if (!DeepComparable.IsExactly(ClinicalInformationElement, otherT.ClinicalInformationElement)) return false;
			if (!DeepComparable.IsExactly(Procedure, otherT.Procedure)) return false;
			if (!DeepComparable.IsExactly(Interpreter, otherT.Interpreter)) return false;
			if (!DeepComparable.IsExactly(DescriptionElement, otherT.DescriptionElement)) return false;
			if (!DeepComparable.IsExactly(Series, otherT.Series)) return false;

			return true;
		}

	}

}
