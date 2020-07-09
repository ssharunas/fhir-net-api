﻿using Hl7.Fhir.Introspection;
using Hl7.Fhir.Support;
using System;
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
	/// Time range defined by start and end date/time
	/// </summary>
	[FhirType("Period")]
	[DataContract]
	public partial class Period : Hl7.Fhir.Model.Element
	{
		public Period()
		{
		}

		public Period(DateTime? start, DateTime? end)
		{
			if (start != null && start != DateTime.MinValue && start != DateTime.MaxValue)
				StartElement = new FhirDateTime(start.Value);

			if (end != null && end != DateTime.MinValue && end != DateTime.MaxValue)
				EndElement = new FhirDateTime(end.Value);
		}

		/// <summary>
		/// Starting time with inclusive boundary
		/// </summary>
		[FhirElement("start", InSummary = true, Order = 40)]
		[DataMember]
		public Hl7.Fhir.Model.FhirDateTime StartElement { get; set; }

		/// <summary>
		/// Starting time with inclusive boundary
		/// </summary>
		/// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
		[NotMapped]
		[IgnoreDataMember]
		public string Start
		{
			get { return StartElement != null ? StartElement.Value : null; }
			set
			{
				if (value is null)
					StartElement = null;
				else
					StartElement = new Hl7.Fhir.Model.FhirDateTime(value);
			}
		}

		/// <summary>
		/// End time with inclusive boundary, if not ongoing
		/// </summary>
		[FhirElement("end", InSummary = true, Order = 50)]
		[DataMember]
		public Hl7.Fhir.Model.FhirDateTime EndElement { get; set; }

		/// <summary>
		/// End time with inclusive boundary, if not ongoing
		/// </summary>
		/// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
		[NotMapped]
		[IgnoreDataMember]
		public string End
		{
			get { return EndElement != null ? EndElement.Value : null; }
			set
			{
				if (value is null)
					EndElement = null;
				else
					EndElement = new Hl7.Fhir.Model.FhirDateTime(value);
			}
		}

		public override IDeepCopyable CopyTo(IDeepCopyable other)
		{
			var dest = other as Period;

			if (dest != null)
			{
				base.CopyTo(dest);
				if (StartElement != null) dest.StartElement = (Hl7.Fhir.Model.FhirDateTime)StartElement.DeepCopy();
				if (EndElement != null) dest.EndElement = (Hl7.Fhir.Model.FhirDateTime)EndElement.DeepCopy();
				return dest;
			}
			else
				throw Error.Argument(nameof(other), "Can only copy to an object of the same type");
		}

		public override IDeepCopyable DeepCopy()
		{
			return CopyTo(new Period());
		}

		public override bool Matches(IDeepComparable other)
		{
			var otherT = other as Period;
			if (otherT is null) return false;

			if (!base.Matches(otherT)) return false;
			if (!DeepComparable.Matches(StartElement, otherT.StartElement)) return false;
			if (!DeepComparable.Matches(EndElement, otherT.EndElement)) return false;

			return true;
		}

		public override bool IsExactly(IDeepComparable other)
		{
			var otherT = other as Period;
			if (otherT is null) return false;

			if (!base.IsExactly(otherT)) return false;
			if (!DeepComparable.IsExactly(StartElement, otherT.StartElement)) return false;
			if (!DeepComparable.IsExactly(EndElement, otherT.EndElement)) return false;

			return true;
		}

	}

}
