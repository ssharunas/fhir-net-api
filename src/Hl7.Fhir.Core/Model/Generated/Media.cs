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
	/// A photo, video, or audio recording acquired or used in healthcare. The actual content may be inline or provided by direct reference
	/// </summary>
	[FhirType("Media", IsResource = true)]
	[DataContract]
	public partial class Media : Hl7.Fhir.Model.Resource
	{
		/// <summary>
		/// Whether the Media is a photo, video, or audio
		/// </summary>
		[FhirEnumeration("MediaType")]
		public enum MediaType
		{
			/// <summary>
			/// The media consists of one or more unmoving images, including photographs, computer-generated graphs and charts, and scanned documents.
			/// </summary>
			[EnumLiteral("photo")]
			Photo,
			/// <summary>
			/// The media consists of a series of frames that capture a moving image.
			/// </summary>
			[EnumLiteral("video")]
			Video,
			/// <summary>
			/// The media consists of a sound recording.
			/// </summary>
			[EnumLiteral("audio")]
			Audio,
		}

		/// <summary>
		/// photo | video | audio
		/// </summary>
		[FhirElement("type", InSummary = true, Order = 70)]
		[Cardinality(Min = 1, Max = 1)]
		[DataMember]
		public Code<Hl7.Fhir.Model.Media.MediaType> TypeElement { get; set; }

		/// <summary>
		/// photo | video | audio
		/// </summary>
		/// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
		[NotMapped]
		[IgnoreDataMember]
		public Hl7.Fhir.Model.Media.MediaType? Type
		{
			get { return TypeElement?.Value; }
			set
			{
				if (value is null)
					TypeElement = null;
				else
					TypeElement = new Code<Hl7.Fhir.Model.Media.MediaType>(value);
			}
		}

		/// <summary>
		/// The type of acquisition equipment/process
		/// </summary>
		[FhirElement("subtype", InSummary = true, Order = 80)]
		[DataMember]
		public Hl7.Fhir.Model.CodeableConcept Subtype { get; set; }

		/// <summary>
		/// Identifier(s) for the image
		/// </summary>
		[FhirElement("identifier", InSummary = true, Order = 90)]
		[Cardinality(Min = 0, Max = -1)]
		[DataMember]
		public List<Hl7.Fhir.Model.Identifier> Identifier { get; set; }

		/// <summary>
		/// When the media was taken/recorded (end)
		/// </summary>
		[FhirElement("dateTime", InSummary = true, Order = 100)]
		[DataMember]
		public Hl7.Fhir.Model.FhirDateTime DateTimeElement { get; set; }

		/// <summary>
		/// When the media was taken/recorded (end)
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
		/// Who/What this Media is a record of
		/// </summary>
		[FhirElement("subject", InSummary = true, Order = 110)]
		[References("Patient", "Practitioner", "Group", "Device", "Specimen")]
		[DataMember]
		public Hl7.Fhir.Model.ResourceReference Subject { get; set; }

		/// <summary>
		/// The person who generated the image
		/// </summary>
		[FhirElement("operator", InSummary = true, Order = 120)]
		[References("Practitioner")]
		[DataMember]
		public Hl7.Fhir.Model.ResourceReference Operator { get; set; }

		/// <summary>
		/// Imaging view e.g Lateral or Antero-posterior
		/// </summary>
		[FhirElement("view", InSummary = true, Order = 130)]
		[DataMember]
		public Hl7.Fhir.Model.CodeableConcept View { get; set; }

		/// <summary>
		/// Name of the device/manufacturer
		/// </summary>
		[FhirElement("deviceName", InSummary = true, Order = 140)]
		[DataMember]
		public Hl7.Fhir.Model.FhirString DeviceNameElement { get; set; }

		/// <summary>
		/// Name of the device/manufacturer
		/// </summary>
		/// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
		[NotMapped]
		[IgnoreDataMember]
		public string DeviceName
		{
			get { return DeviceNameElement?.Value; }
			set
			{
				if (value is null)
					DeviceNameElement = null;
				else
					DeviceNameElement = new Hl7.Fhir.Model.FhirString(value);
			}
		}

		/// <summary>
		/// Height of the image in pixels(photo/video)
		/// </summary>
		[FhirElement("height", InSummary = true, Order = 150)]
		[DataMember]
		public Hl7.Fhir.Model.Integer HeightElement { get; set; }

		/// <summary>
		/// Height of the image in pixels(photo/video)
		/// </summary>
		/// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
		[NotMapped]
		[IgnoreDataMember]
		public int? Height
		{
			get { return HeightElement?.Value; }
			set
			{
				if (value is null)
					HeightElement = null;
				else
					HeightElement = new Hl7.Fhir.Model.Integer(value);
			}
		}

		/// <summary>
		/// Width of the image in pixels (photo/video)
		/// </summary>
		[FhirElement("width", InSummary = true, Order = 160)]
		[DataMember]
		public Hl7.Fhir.Model.Integer WidthElement { get; set; }

		/// <summary>
		/// Width of the image in pixels (photo/video)
		/// </summary>
		/// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
		[NotMapped]
		[IgnoreDataMember]
		public int? Width
		{
			get { return WidthElement?.Value; }
			set
			{
				if (value is null)
					WidthElement = null;
				else
					WidthElement = new Hl7.Fhir.Model.Integer(value);
			}
		}

		/// <summary>
		/// Number of frames if &gt; 1 (photo)
		/// </summary>
		[FhirElement("frames", InSummary = true, Order = 170)]
		[DataMember]
		public Hl7.Fhir.Model.Integer FramesElement { get; set; }

		/// <summary>
		/// Number of frames if &gt; 1 (photo)
		/// </summary>
		/// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
		[NotMapped]
		[IgnoreDataMember]
		public int? Frames
		{
			get { return FramesElement?.Value; }
			set
			{
				if (value is null)
					FramesElement = null;
				else
					FramesElement = new Hl7.Fhir.Model.Integer(value);
			}
		}

		/// <summary>
		/// Length in seconds (audio / video)
		/// </summary>
		[FhirElement("length", InSummary = true, Order = 180)]
		[DataMember]
		public Hl7.Fhir.Model.Integer LengthElement { get; set; }

		/// <summary>
		/// Length in seconds (audio / video)
		/// </summary>
		/// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
		[NotMapped]
		[IgnoreDataMember]
		public int? Length
		{
			get { return LengthElement?.Value; }
			set
			{
				if (value is null)
					LengthElement = null;
				else
					LengthElement = new Hl7.Fhir.Model.Integer(value);
			}
		}

		/// <summary>
		/// Actual Media - reference or data
		/// </summary>
		[FhirElement("content", Order = 190)]
		[Cardinality(Min = 1, Max = 1)]
		[DataMember]
		public Hl7.Fhir.Model.Attachment Content { get; set; }

		public override IDeepCopyable CopyTo(IDeepCopyable other)
		{
			var dest = other as Media;

			if (dest != null)
			{
				base.CopyTo(dest);
				if (TypeElement != null) dest.TypeElement = (Code<Hl7.Fhir.Model.Media.MediaType>)TypeElement.DeepCopy();
				if (Subtype != null) dest.Subtype = (Hl7.Fhir.Model.CodeableConcept)Subtype.DeepCopy();
				if (Identifier != null) dest.Identifier = new List<Hl7.Fhir.Model.Identifier>(Identifier.DeepCopy());
				if (DateTimeElement != null) dest.DateTimeElement = (Hl7.Fhir.Model.FhirDateTime)DateTimeElement.DeepCopy();
				if (Subject != null) dest.Subject = (Hl7.Fhir.Model.ResourceReference)Subject.DeepCopy();
				if (Operator != null) dest.Operator = (Hl7.Fhir.Model.ResourceReference)Operator.DeepCopy();
				if (View != null) dest.View = (Hl7.Fhir.Model.CodeableConcept)View.DeepCopy();
				if (DeviceNameElement != null) dest.DeviceNameElement = (Hl7.Fhir.Model.FhirString)DeviceNameElement.DeepCopy();
				if (HeightElement != null) dest.HeightElement = (Hl7.Fhir.Model.Integer)HeightElement.DeepCopy();
				if (WidthElement != null) dest.WidthElement = (Hl7.Fhir.Model.Integer)WidthElement.DeepCopy();
				if (FramesElement != null) dest.FramesElement = (Hl7.Fhir.Model.Integer)FramesElement.DeepCopy();
				if (LengthElement != null) dest.LengthElement = (Hl7.Fhir.Model.Integer)LengthElement.DeepCopy();
				if (Content != null) dest.Content = (Hl7.Fhir.Model.Attachment)Content.DeepCopy();
				return dest;
			}
			else
				throw Error.Argument(nameof(other), "Can only copy to an object of the same type");
		}

		public override IDeepCopyable DeepCopy()
		{
			return CopyTo(new Media());
		}

		public override bool Matches(IDeepComparable other)
		{
			var otherT = other as Media;
			if (otherT is null) return false;

			if (!base.Matches(otherT)) return false;
			if (!DeepComparable.Matches(TypeElement, otherT.TypeElement)) return false;
			if (!DeepComparable.Matches(Subtype, otherT.Subtype)) return false;
			if (!DeepComparable.Matches(Identifier, otherT.Identifier)) return false;
			if (!DeepComparable.Matches(DateTimeElement, otherT.DateTimeElement)) return false;
			if (!DeepComparable.Matches(Subject, otherT.Subject)) return false;
			if (!DeepComparable.Matches(Operator, otherT.Operator)) return false;
			if (!DeepComparable.Matches(View, otherT.View)) return false;
			if (!DeepComparable.Matches(DeviceNameElement, otherT.DeviceNameElement)) return false;
			if (!DeepComparable.Matches(HeightElement, otherT.HeightElement)) return false;
			if (!DeepComparable.Matches(WidthElement, otherT.WidthElement)) return false;
			if (!DeepComparable.Matches(FramesElement, otherT.FramesElement)) return false;
			if (!DeepComparable.Matches(LengthElement, otherT.LengthElement)) return false;
			if (!DeepComparable.Matches(Content, otherT.Content)) return false;

			return true;
		}

		public override bool IsExactly(IDeepComparable other)
		{
			var otherT = other as Media;
			if (otherT is null) return false;

			if (!base.IsExactly(otherT)) return false;
			if (!DeepComparable.IsExactly(TypeElement, otherT.TypeElement)) return false;
			if (!DeepComparable.IsExactly(Subtype, otherT.Subtype)) return false;
			if (!DeepComparable.IsExactly(Identifier, otherT.Identifier)) return false;
			if (!DeepComparable.IsExactly(DateTimeElement, otherT.DateTimeElement)) return false;
			if (!DeepComparable.IsExactly(Subject, otherT.Subject)) return false;
			if (!DeepComparable.IsExactly(Operator, otherT.Operator)) return false;
			if (!DeepComparable.IsExactly(View, otherT.View)) return false;
			if (!DeepComparable.IsExactly(DeviceNameElement, otherT.DeviceNameElement)) return false;
			if (!DeepComparable.IsExactly(HeightElement, otherT.HeightElement)) return false;
			if (!DeepComparable.IsExactly(WidthElement, otherT.WidthElement)) return false;
			if (!DeepComparable.IsExactly(FramesElement, otherT.FramesElement)) return false;
			if (!DeepComparable.IsExactly(LengthElement, otherT.LengthElement)) return false;
			if (!DeepComparable.IsExactly(Content, otherT.Content)) return false;

			return true;
		}

	}

}
