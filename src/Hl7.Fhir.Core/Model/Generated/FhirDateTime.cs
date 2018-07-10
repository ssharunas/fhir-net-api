﻿using System;
using System.Runtime.Serialization;
using Hl7.Fhir.Introspection;
using Hl7.Fhir.Validation;

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
	/// Typed element containing the primitive dateTime
	/// </summary>
	[FhirType("dateTime")]
	[DataContract]
	public partial class FhirDateTime : Hl7.Fhir.Model.Element, System.ComponentModel.INotifyPropertyChanged
	{
		// Must conform to the pattern "-?([1-9][0-9]{3}|0[0-9]{3})(-(0[1-9]|1[0-2])(-(0[1-9]|[12][0-9]|3[01])(T(([01][0-9]|2[0-3]):[0-5][0-9]:[0-5][0-9](\.[0-9]+)?|(24:00:00(\.0+)?))(Z|(\+|-)((0[0-9]|1[0-3]):[0-5][0-9]|14:00))?)?)?)?"
		public const string PATTERN = @"-?([1-9][0-9]{3}|0[0-9]{3})(-(0[1-9]|1[0-2])(-(0[1-9]|[12][0-9]|3[01])(T(([01][0-9]|2[0-3]):[0-5][0-9]:[0-5][0-9](\.[0-9]+)?|(24:00:00(\.0+)?))(Z|(\+|-)((0[0-9]|1[0-3]):[0-5][0-9]|14:00))?)?)?)?";

		public FhirDateTime(string value)
		{
			Value = value;
		}

		public FhirDateTime() : this((string)null) { }

		/// <summary>
		/// Primitive value of the element
		/// </summary>
		[FhirElement("value", IsPrimitiveValue = true, XmlSerialization = XmlSerializationHint.Attribute, InSummary = true, Order = 40)]
		[DateTimePattern]
		[DataMember]
		public string Value
		{
			get { return _Value; }
			set { _Value = value; OnPropertyChanged("Value"); }
		}
		private string _Value;

		public override IDeepCopyable CopyTo(IDeepCopyable other)
		{
			var dest = other as FhirDateTime;

			if (dest != null)
			{
				base.CopyTo(dest);
				if (Value != null) dest.Value = Value;
				return dest;
			}
			else
				throw new ArgumentException("Can only copy to an object of the same type", "other");
		}

		public override IDeepCopyable DeepCopy()
		{
			return CopyTo(new FhirDateTime());
		}

		public override bool Matches(IDeepComparable other)
		{
			var otherT = other as FhirDateTime;
			if (otherT == null) return false;

			if (!base.Matches(otherT)) return false;
			if (Value != otherT.Value) return false;

			return true;
		}

		public override bool IsExactly(IDeepComparable other)
		{
			var otherT = other as FhirDateTime;
			if (otherT == null) return false;

			if (!base.IsExactly(otherT)) return false;
			if (Value != otherT.Value) return false;

			return true;
		}

		public static implicit operator DateTime(FhirDateTime value)
		{
			DateTime result = DateTime.MinValue;

			if (value != null && !string.IsNullOrEmpty(value.Value))
				DateTime.TryParse(value.Value, out result);

			return result;
		}

		public static implicit operator FhirDateTime(DateTime? value)
		{
			return (value ?? DateTime.MinValue);
		}

		public static implicit operator FhirDateTime(DateTime value)
		{
			FhirDateTime result = null;

			if (value != DateTime.MinValue && value != DateTime.MaxValue)
				result = new FhirDateTime(value.ToString("yyyy-MM-ddTHH:mm:ss"));

			return result;
		}
	}
}