﻿using Hl7.Fhir.Introspection;
using Hl7.Fhir.Support;
using Hl7.Fhir.Validation;
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
	/// Content in a format defined elsewhere
	/// </summary>
	[FhirType("Attachment")]
	[DataContract]
	public partial class Attachment : Hl7.Fhir.Model.Element
	{
		/// <summary>
		/// Mime type of the content, with charset etc.
		/// </summary>
		[FhirElement("contentType", InSummary = true, Order = 40)]
		[Cardinality(Min = 1, Max = 1)]
		[DataMember]
		public Hl7.Fhir.Model.Code ContentTypeElement { get; set; }

		/// <summary>
		/// Mime type of the content, with charset etc.
		/// </summary>
		/// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
		[NotMapped]
		[IgnoreDataMember]
		public string ContentType
		{
			get { return ContentTypeElement?.Value; }
			set
			{
				if (value is null)
					ContentTypeElement = null;
				else
					ContentTypeElement = new Hl7.Fhir.Model.Code(value);
			}
		}

		/// <summary>
		/// Human language of the content (BCP-47)
		/// </summary>
		[FhirElement("language", InSummary = true, Order = 50)]
		[DataMember]
		public Hl7.Fhir.Model.Code LanguageElement { get; set; }

		/// <summary>
		/// Human language of the content (BCP-47)
		/// </summary>
		/// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
		[NotMapped]
		[IgnoreDataMember]
		public string Language
		{
			get { return LanguageElement?.Value; }
			set
			{
				if (value is null)
					LanguageElement = null;
				else
					LanguageElement = new Hl7.Fhir.Model.Code(value);
			}
		}

		/// <summary>
		/// Data inline, base64ed
		/// </summary>
		[FhirElement("data", InSummary = true, Order = 60)]
		[DataMember]
		public Hl7.Fhir.Model.Base64Binary DataElement { get; set; }

		/// <summary>
		/// Data inline, base64ed
		/// </summary>
		/// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
		[NotMapped]
		[IgnoreDataMember]
		public byte[] Data
		{
			get { return DataElement?.Value; }
			set
			{
				if (value is null)
					DataElement = null;
				else
					DataElement = new Hl7.Fhir.Model.Base64Binary(value);
			}
		}

		/// <summary>
		/// Uri where the data can be found
		/// </summary>
		[FhirElement("url", InSummary = true, Order = 70)]
		[DataMember]
		public Hl7.Fhir.Model.FhirUri UrlElement { get; set; }

		/// <summary>
		/// Uri where the data can be found
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
		/// Number of bytes of content (if url provided)
		/// </summary>
		[FhirElement("size", InSummary = true, Order = 80)]
		[DataMember]
		public Hl7.Fhir.Model.Integer SizeElement { get; set; }

		/// <summary>
		/// Number of bytes of content (if url provided)
		/// </summary>
		/// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
		[NotMapped]
		[IgnoreDataMember]
		public int? Size
		{
			get { return SizeElement?.Value; }
			set
			{
				if (value is null)
					SizeElement = null;
				else
					SizeElement = new Hl7.Fhir.Model.Integer(value);
			}
		}

		/// <summary>
		/// Hash of the data (sha-1, base64ed )
		/// </summary>
		[FhirElement("hash", InSummary = true, Order = 90)]
		[DataMember]
		public Hl7.Fhir.Model.Base64Binary HashElement { get; set; }

		/// <summary>
		/// Hash of the data (sha-1, base64ed )
		/// </summary>
		/// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
		[NotMapped]
		[IgnoreDataMember]
		public byte[] Hash
		{
			get { return HashElement?.Value; }
			set
			{
				if (value is null)
					HashElement = null;
				else
					HashElement = new Hl7.Fhir.Model.Base64Binary(value);
			}
		}

		/// <summary>
		/// Label to display in place of the data
		/// </summary>
		[FhirElement("title", InSummary = true, Order = 100)]
		[DataMember]
		public Hl7.Fhir.Model.FhirString TitleElement { get; set; }

		/// <summary>
		/// Label to display in place of the data
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

		public override IDeepCopyable CopyTo(IDeepCopyable other)
		{
			var dest = other as Attachment;

			if (dest != null)
			{
				base.CopyTo(dest);
				if (ContentTypeElement != null) dest.ContentTypeElement = (Hl7.Fhir.Model.Code)ContentTypeElement.DeepCopy();
				if (LanguageElement != null) dest.LanguageElement = (Hl7.Fhir.Model.Code)LanguageElement.DeepCopy();
				if (DataElement != null) dest.DataElement = (Hl7.Fhir.Model.Base64Binary)DataElement.DeepCopy();
				if (UrlElement != null) dest.UrlElement = (Hl7.Fhir.Model.FhirUri)UrlElement.DeepCopy();
				if (SizeElement != null) dest.SizeElement = (Hl7.Fhir.Model.Integer)SizeElement.DeepCopy();
				if (HashElement != null) dest.HashElement = (Hl7.Fhir.Model.Base64Binary)HashElement.DeepCopy();
				if (TitleElement != null) dest.TitleElement = (Hl7.Fhir.Model.FhirString)TitleElement.DeepCopy();
				return dest;
			}
			else
				throw Error.Argument(nameof(other), "Can only copy to an object of the same type");
		}

		public override IDeepCopyable DeepCopy()
		{
			return CopyTo(new Attachment());
		}

		public override bool Matches(IDeepComparable other)
		{
			var otherT = other as Attachment;
			if (otherT is null) return false;

			if (!base.Matches(otherT)) return false;
			if (!DeepComparable.Matches(ContentTypeElement, otherT.ContentTypeElement)) return false;
			if (!DeepComparable.Matches(LanguageElement, otherT.LanguageElement)) return false;
			if (!DeepComparable.Matches(DataElement, otherT.DataElement)) return false;
			if (!DeepComparable.Matches(UrlElement, otherT.UrlElement)) return false;
			if (!DeepComparable.Matches(SizeElement, otherT.SizeElement)) return false;
			if (!DeepComparable.Matches(HashElement, otherT.HashElement)) return false;
			if (!DeepComparable.Matches(TitleElement, otherT.TitleElement)) return false;

			return true;
		}

		public override bool IsExactly(IDeepComparable other)
		{
			var otherT = other as Attachment;
			if (otherT is null) return false;

			if (!base.IsExactly(otherT)) return false;
			if (!DeepComparable.IsExactly(ContentTypeElement, otherT.ContentTypeElement)) return false;
			if (!DeepComparable.IsExactly(LanguageElement, otherT.LanguageElement)) return false;
			if (!DeepComparable.IsExactly(DataElement, otherT.DataElement)) return false;
			if (!DeepComparable.IsExactly(UrlElement, otherT.UrlElement)) return false;
			if (!DeepComparable.IsExactly(SizeElement, otherT.SizeElement)) return false;
			if (!DeepComparable.IsExactly(HashElement, otherT.HashElement)) return false;
			if (!DeepComparable.IsExactly(TitleElement, otherT.TitleElement)) return false;

			return true;
		}

	}

}
