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
	/// A response to an order
	/// </summary>
	[FhirType("OrderResponse", IsResource = true)]
	[DataContract]
	public partial class OrderResponse : Hl7.Fhir.Model.Resource
	{
		/// <summary>
		/// The status of the response to an order
		/// </summary>
		[FhirEnumeration("OrderOutcomeStatus")]
		public enum OrderOutcomeStatus
		{
			/// <summary>
			/// The order is known, but no processing has occurred at this time.
			/// </summary>
			[EnumLiteral("pending")]
			Pending,
			/// <summary>
			/// The order is undergoing initial processing to determine whether it will be accepted (usually this involves human review).
			/// </summary>
			[EnumLiteral("review")]
			Review,
			/// <summary>
			/// The order was rejected because of a workflow/business logic reason.
			/// </summary>
			[EnumLiteral("rejected")]
			Rejected,
			/// <summary>
			/// The order was unable to be processed because of a technical error (i.e. unexpected error).
			/// </summary>
			[EnumLiteral("error")]
			Error,
			/// <summary>
			/// The order has been accepted, and work is in progress.
			/// </summary>
			[EnumLiteral("accepted")]
			Accepted,
			/// <summary>
			/// Processing the order was halted at the initiators request.
			/// </summary>
			[EnumLiteral("cancelled")]
			Cancelled,
			/// <summary>
			/// The order has been cancelled and replaced by another.
			/// </summary>
			[EnumLiteral("replaced")]
			Replaced,
			/// <summary>
			/// Processing the order was stopped because of some workflow/business logic reason.
			/// </summary>
			[EnumLiteral("aborted")]
			Aborted,
			/// <summary>
			/// The order has been completed.
			/// </summary>
			[EnumLiteral("complete")]
			Complete,
		}

		/// <summary>
		/// Identifiers assigned to this order by the orderer or by the receiver
		/// </summary>
		[FhirElement("identifier", Order = 70)]
		[Cardinality(Min = 0, Max = -1)]
		[DataMember]
		public List<Hl7.Fhir.Model.Identifier> Identifier { get; set; }

		/// <summary>
		/// The order that this is a response to
		/// </summary>
		[FhirElement("request", Order = 80)]
		[References("Order")]
		[Cardinality(Min = 1, Max = 1)]
		[DataMember]
		public Hl7.Fhir.Model.ResourceReference Request { get; set; }

		/// <summary>
		/// When the response was made
		/// </summary>
		[FhirElement("date", Order = 90)]
		[DataMember]
		public Hl7.Fhir.Model.FhirDateTime DateElement { get; set; }

		/// <summary>
		/// When the response was made
		/// </summary>
		/// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
		[NotMapped]
		[IgnoreDataMember]
		public string Date
		{
			get { return DateElement != null ? DateElement.Value : null; }
			set
			{
				if (value == null)
					DateElement = null;
				else
					DateElement = new Hl7.Fhir.Model.FhirDateTime(value);
			}
		}

		/// <summary>
		/// Who made the response
		/// </summary>
		[FhirElement("who", Order = 100)]
		[References("Practitioner", "Organization", "Device")]
		[DataMember]
		public Hl7.Fhir.Model.ResourceReference Who { get; set; }

		/// <summary>
		/// If required by policy
		/// </summary>
		[FhirElement("authority", Order = 110, Choice = ChoiceType.DatatypeChoice)]
		[AllowedTypes(typeof(Hl7.Fhir.Model.CodeableConcept), typeof(Hl7.Fhir.Model.ResourceReference))]
		[DataMember]
		public Hl7.Fhir.Model.Element Authority { get; set; }

		/// <summary>
		/// pending | review | rejected | error | accepted | cancelled | replaced | aborted | complete
		/// </summary>
		[FhirElement("code", Order = 120)]
		[Cardinality(Min = 1, Max = 1)]
		[DataMember]
		public Code<Hl7.Fhir.Model.OrderResponse.OrderOutcomeStatus> CodeElement { get; set; }

		/// <summary>
		/// pending | review | rejected | error | accepted | cancelled | replaced | aborted | complete
		/// </summary>
		/// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
		[NotMapped]
		[IgnoreDataMember]
		public Hl7.Fhir.Model.OrderResponse.OrderOutcomeStatus? Code
		{
			get { return CodeElement != null ? CodeElement.Value : null; }
			set
			{
				if (value == null)
					CodeElement = null;
				else
					CodeElement = new Code<Hl7.Fhir.Model.OrderResponse.OrderOutcomeStatus>(value);
			}
		}

		/// <summary>
		/// Additional description of the response
		/// </summary>
		[FhirElement("description", Order = 130)]
		[DataMember]
		public Hl7.Fhir.Model.FhirString DescriptionElement { get; set; }

		/// <summary>
		/// Additional description of the response
		/// </summary>
		/// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
		[NotMapped]
		[IgnoreDataMember]
		public string Description
		{
			get { return DescriptionElement != null ? DescriptionElement.Value : null; }
			set
			{
				if (value == null)
					DescriptionElement = null;
				else
					DescriptionElement = new Hl7.Fhir.Model.FhirString(value);
			}
		}

		/// <summary>
		/// Details of the outcome of performing the order
		/// </summary>
		[FhirElement("fulfillment", Order = 140)]
		[References()]
		[Cardinality(Min = 0, Max = -1)]
		[DataMember]
		public List<Hl7.Fhir.Model.ResourceReference> Fulfillment { get; set; }

		public override IDeepCopyable CopyTo(IDeepCopyable other)
		{
			var dest = other as OrderResponse;

			if (dest != null)
			{
				base.CopyTo(dest);
				if (Identifier != null) dest.Identifier = new List<Hl7.Fhir.Model.Identifier>(Identifier.DeepCopy());
				if (Request != null) dest.Request = (Hl7.Fhir.Model.ResourceReference)Request.DeepCopy();
				if (DateElement != null) dest.DateElement = (Hl7.Fhir.Model.FhirDateTime)DateElement.DeepCopy();
				if (Who != null) dest.Who = (Hl7.Fhir.Model.ResourceReference)Who.DeepCopy();
				if (Authority != null) dest.Authority = (Hl7.Fhir.Model.Element)Authority.DeepCopy();
				if (CodeElement != null) dest.CodeElement = (Code<Hl7.Fhir.Model.OrderResponse.OrderOutcomeStatus>)CodeElement.DeepCopy();
				if (DescriptionElement != null) dest.DescriptionElement = (Hl7.Fhir.Model.FhirString)DescriptionElement.DeepCopy();
				if (Fulfillment != null) dest.Fulfillment = new List<Hl7.Fhir.Model.ResourceReference>(Fulfillment.DeepCopy());
				return dest;
			}
			else
				throw Error.Argument(nameof(other), "Can only copy to an object of the same type");
		}

		public override IDeepCopyable DeepCopy()
		{
			return CopyTo(new OrderResponse());
		}

		public override bool Matches(IDeepComparable other)
		{
			var otherT = other as OrderResponse;
			if (otherT == null) return false;

			if (!base.Matches(otherT)) return false;
			if (!DeepComparable.Matches(Identifier, otherT.Identifier)) return false;
			if (!DeepComparable.Matches(Request, otherT.Request)) return false;
			if (!DeepComparable.Matches(DateElement, otherT.DateElement)) return false;
			if (!DeepComparable.Matches(Who, otherT.Who)) return false;
			if (!DeepComparable.Matches(Authority, otherT.Authority)) return false;
			if (!DeepComparable.Matches(CodeElement, otherT.CodeElement)) return false;
			if (!DeepComparable.Matches(DescriptionElement, otherT.DescriptionElement)) return false;
			if (!DeepComparable.Matches(Fulfillment, otherT.Fulfillment)) return false;

			return true;
		}

		public override bool IsExactly(IDeepComparable other)
		{
			var otherT = other as OrderResponse;
			if (otherT == null) return false;

			if (!base.IsExactly(otherT)) return false;
			if (!DeepComparable.IsExactly(Identifier, otherT.Identifier)) return false;
			if (!DeepComparable.IsExactly(Request, otherT.Request)) return false;
			if (!DeepComparable.IsExactly(DateElement, otherT.DateElement)) return false;
			if (!DeepComparable.IsExactly(Who, otherT.Who)) return false;
			if (!DeepComparable.IsExactly(Authority, otherT.Authority)) return false;
			if (!DeepComparable.IsExactly(CodeElement, otherT.CodeElement)) return false;
			if (!DeepComparable.IsExactly(DescriptionElement, otherT.DescriptionElement)) return false;
			if (!DeepComparable.IsExactly(Fulfillment, otherT.Fulfillment)) return false;

			return true;
		}

	}

}
