using Hl7.Fhir.Introspection;
using Hl7.Fhir.Support;
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
	/// A measured or measurable amount
	/// </summary>
	[FhirType("Quantity")]
	[DataContract]
	public partial class Quantity : Hl7.Fhir.Model.Element
	{
		/// <summary>
		/// How the Quantity should be understood and represented
		/// </summary>
		[FhirEnumeration("QuantityCompararator")]
		public enum QuantityCompararator
		{
			/// <summary>
			/// The actual value is less than the given value.
			/// </summary>
			[EnumLiteral("<")]
			LessThan,
			/// <summary>
			/// The actual value is less than or equal to the given value.
			/// </summary>
			[EnumLiteral("<=")]
			LessOrEqual,
			/// <summary>
			/// The actual value is greater than or equal to the given value.
			/// </summary>
			[EnumLiteral(">=")]
			GreaterOrEqual,
			/// <summary>
			/// The actual value is greater than the given value.
			/// </summary>
			[EnumLiteral(">")]
			GreaterThan,
		}

		/// <summary>
		/// Numerical value (with implicit precision)
		/// </summary>
		[FhirElement("value", InSummary = true, Order = 40)]
		[DataMember]
		public Hl7.Fhir.Model.FhirDecimal ValueElement { get; set; }

		/// <summary>
		/// Numerical value (with implicit precision)
		/// </summary>
		/// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
		[NotMapped]
		[IgnoreDataMember]
		public decimal? Value
		{
			get { return ValueElement?.Value; }
			set
			{
				if (value is null)
					ValueElement = null;
				else
					ValueElement = new Hl7.Fhir.Model.FhirDecimal(value);
			}
		}

		/// <summary>
		/// &lt; | &lt;= | &gt;= | &gt; - how to understand the value
		/// </summary>
		[FhirElement("comparator", InSummary = true, Order = 50)]
		[DataMember]
		public Code<Hl7.Fhir.Model.Quantity.QuantityCompararator> ComparatorElement { get; set; }

		/// <summary>
		/// &lt; | &lt;= | &gt;= | &gt; - how to understand the value
		/// </summary>
		/// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
		[NotMapped]
		[IgnoreDataMember]
		public Hl7.Fhir.Model.Quantity.QuantityCompararator? Comparator
		{
			get { return ComparatorElement?.Value; }
			set
			{
				if (value is null)
					ComparatorElement = null;
				else
					ComparatorElement = new Code<Hl7.Fhir.Model.Quantity.QuantityCompararator>(value);
			}
		}

		/// <summary>
		/// Unit representation
		/// </summary>
		[FhirElement("units", InSummary = true, Order = 60)]
		[DataMember]
		public Hl7.Fhir.Model.FhirString UnitsElement { get; set; }

		/// <summary>
		/// Unit representation
		/// </summary>
		/// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
		[NotMapped]
		[IgnoreDataMember]
		public string Units
		{
			get { return UnitsElement?.Value; }
			set
			{
				if (value is null)
					UnitsElement = null;
				else
					UnitsElement = new Hl7.Fhir.Model.FhirString(value);
			}
		}

		/// <summary>
		/// System that defines coded unit form
		/// </summary>
		[FhirElement("system", InSummary = true, Order = 70)]
		[DataMember]
		public Hl7.Fhir.Model.FhirUri SystemElement { get; set; }

		/// <summary>
		/// System that defines coded unit form
		/// </summary>
		/// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
		[NotMapped]
		[IgnoreDataMember]
		public string System
		{
			get { return SystemElement?.Value; }
			set
			{
				if (value is null)
					SystemElement = null;
				else
					SystemElement = new Hl7.Fhir.Model.FhirUri(value);
			}
		}

		/// <summary>
		/// Coded form of the unit
		/// </summary>
		[FhirElement("code", InSummary = true, Order = 80)]
		[DataMember]
		public Hl7.Fhir.Model.Code CodeElement { get; set; }

		/// <summary>
		/// Coded form of the unit
		/// </summary>
		/// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
		[NotMapped]
		[IgnoreDataMember]
		public string Code
		{
			get { return CodeElement?.Value; }
			set
			{
				if (value is null)
					CodeElement = null;
				else
					CodeElement = new Hl7.Fhir.Model.Code(value);
			}
		}

		public override IDeepCopyable CopyTo(IDeepCopyable other)
		{
			var dest = other as Quantity;

			if (dest != null)
			{
				base.CopyTo(dest);
				if (ValueElement != null) dest.ValueElement = (Hl7.Fhir.Model.FhirDecimal)ValueElement.DeepCopy();
				if (ComparatorElement != null) dest.ComparatorElement = (Code<Hl7.Fhir.Model.Quantity.QuantityCompararator>)ComparatorElement.DeepCopy();
				if (UnitsElement != null) dest.UnitsElement = (Hl7.Fhir.Model.FhirString)UnitsElement.DeepCopy();
				if (SystemElement != null) dest.SystemElement = (Hl7.Fhir.Model.FhirUri)SystemElement.DeepCopy();
				if (CodeElement != null) dest.CodeElement = (Hl7.Fhir.Model.Code)CodeElement.DeepCopy();
				return dest;
			}
			else
				throw Error.Argument(nameof(other), "Can only copy to an object of the same type");
		}

		public override IDeepCopyable DeepCopy()
		{
			return CopyTo(new Quantity());
		}

		public override bool Matches(IDeepComparable other)
		{
			var otherT = other as Quantity;
			if (otherT is null) return false;

			if (!base.Matches(otherT)) return false;
			if (!DeepComparable.Matches(ValueElement, otherT.ValueElement)) return false;
			if (!DeepComparable.Matches(ComparatorElement, otherT.ComparatorElement)) return false;
			if (!DeepComparable.Matches(UnitsElement, otherT.UnitsElement)) return false;
			if (!DeepComparable.Matches(SystemElement, otherT.SystemElement)) return false;
			if (!DeepComparable.Matches(CodeElement, otherT.CodeElement)) return false;

			return true;
		}

		public override bool IsExactly(IDeepComparable other)
		{
			var otherT = other as Quantity;
			if (otherT is null) return false;

			if (!base.IsExactly(otherT)) return false;
			if (!DeepComparable.IsExactly(ValueElement, otherT.ValueElement)) return false;
			if (!DeepComparable.IsExactly(ComparatorElement, otherT.ComparatorElement)) return false;
			if (!DeepComparable.IsExactly(UnitsElement, otherT.UnitsElement)) return false;
			if (!DeepComparable.IsExactly(SystemElement, otherT.SystemElement)) return false;
			if (!DeepComparable.IsExactly(CodeElement, otherT.CodeElement)) return false;

			return true;
		}

	}

}
