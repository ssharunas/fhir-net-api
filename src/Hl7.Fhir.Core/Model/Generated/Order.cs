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
	/// A request to perform an action
	/// </summary>
	[FhirType("Order", IsResource = true)]
	[DataContract]
	public partial class Order : Hl7.Fhir.Model.Resource
	{
		[FhirType("OrderWhenComponent")]
		[DataContract]
		public partial class OrderWhenComponent : Hl7.Fhir.Model.Element
		{
			/// <summary>
			/// Code specifies when request should be done. The code may simply be a priority code
			/// </summary>
			[FhirElement("code", InSummary = true, Order = 40)]
			[DataMember]
			public Hl7.Fhir.Model.CodeableConcept Code { get; set; }

			/// <summary>
			/// A formal schedule
			/// </summary>
			[FhirElement("schedule", InSummary = true, Order = 50)]
			[DataMember]
			public Hl7.Fhir.Model.Schedule Schedule { get; set; }

			public override IDeepCopyable CopyTo(IDeepCopyable other)
			{
				var dest = other as OrderWhenComponent;

				if (dest != null)
				{
					base.CopyTo(dest);
					if (Code != null) dest.Code = (Hl7.Fhir.Model.CodeableConcept)Code.DeepCopy();
					if (Schedule != null) dest.Schedule = (Hl7.Fhir.Model.Schedule)Schedule.DeepCopy();
					return dest;
				}
				else
					throw Error.Argument(nameof(other), "Can only copy to an object of the same type");
			}

			public override IDeepCopyable DeepCopy()
			{
				return CopyTo(new OrderWhenComponent());
			}

			public override bool Matches(IDeepComparable other)
			{
				var otherT = other as OrderWhenComponent;
				if (otherT is null) return false;

				if (!base.Matches(otherT)) return false;
				if (!DeepComparable.Matches(Code, otherT.Code)) return false;
				if (!DeepComparable.Matches(Schedule, otherT.Schedule)) return false;

				return true;
			}

			public override bool IsExactly(IDeepComparable other)
			{
				var otherT = other as OrderWhenComponent;
				if (otherT is null) return false;

				if (!base.IsExactly(otherT)) return false;
				if (!DeepComparable.IsExactly(Code, otherT.Code)) return false;
				if (!DeepComparable.IsExactly(Schedule, otherT.Schedule)) return false;

				return true;
			}

		}


		/// <summary>
		/// Identifiers assigned to this order by the orderer or by the receiver
		/// </summary>
		[FhirElement("identifier", Order = 70)]
		[Cardinality(Min = 0, Max = -1)]
		[DataMember]
		public List<Hl7.Fhir.Model.Identifier> Identifier { get; set; }

		/// <summary>
		/// When the order was made
		/// </summary>
		[FhirElement("date", Order = 80)]
		[DataMember]
		public Hl7.Fhir.Model.FhirDateTime DateElement { get; set; }

		/// <summary>
		/// When the order was made
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
		/// Patient this order is about
		/// </summary>
		[FhirElement("subject", Order = 90)]
		[References("Patient")]
		[DataMember]
		public Hl7.Fhir.Model.ResourceReference Subject { get; set; }

		/// <summary>
		/// Who initiated the order
		/// </summary>
		[FhirElement("source", Order = 100)]
		[References("Practitioner")]
		[DataMember]
		public Hl7.Fhir.Model.ResourceReference Source { get; set; }

		/// <summary>
		/// Who is intended to fulfill the order
		/// </summary>
		[FhirElement("target", Order = 110)]
		[References("Organization", "Device", "Practitioner")]
		[DataMember]
		public Hl7.Fhir.Model.ResourceReference Target { get; set; }

		/// <summary>
		/// Text - why the order was made
		/// </summary>
		[FhirElement("reason", Order = 120, Choice = ChoiceType.DatatypeChoice)]
		[AllowedTypes(typeof(Hl7.Fhir.Model.CodeableConcept), typeof(Hl7.Fhir.Model.ResourceReference))]
		[DataMember]
		public Hl7.Fhir.Model.Element Reason { get; set; }

		/// <summary>
		/// If required by policy
		/// </summary>
		[FhirElement("authority", Order = 130)]
		[References()]
		[DataMember]
		public Hl7.Fhir.Model.ResourceReference Authority { get; set; }

		/// <summary>
		/// When order should be fulfilled
		/// </summary>
		[FhirElement("when", Order = 140)]
		[DataMember]
		public Hl7.Fhir.Model.Order.OrderWhenComponent When { get; set; }

		/// <summary>
		/// What action is being ordered
		/// </summary>
		[FhirElement("detail", Order = 150)]
		[References()]
		[Cardinality(Min = 1, Max = -1)]
		[DataMember]
		public List<Hl7.Fhir.Model.ResourceReference> Detail { get; set; }

		public override IDeepCopyable CopyTo(IDeepCopyable other)
		{
			var dest = other as Order;

			if (dest != null)
			{
				base.CopyTo(dest);
				if (Identifier != null) dest.Identifier = new List<Hl7.Fhir.Model.Identifier>(Identifier.DeepCopy());
				if (DateElement != null) dest.DateElement = (Hl7.Fhir.Model.FhirDateTime)DateElement.DeepCopy();
				if (Subject != null) dest.Subject = (Hl7.Fhir.Model.ResourceReference)Subject.DeepCopy();
				if (Source != null) dest.Source = (Hl7.Fhir.Model.ResourceReference)Source.DeepCopy();
				if (Target != null) dest.Target = (Hl7.Fhir.Model.ResourceReference)Target.DeepCopy();
				if (Reason != null) dest.Reason = (Hl7.Fhir.Model.Element)Reason.DeepCopy();
				if (Authority != null) dest.Authority = (Hl7.Fhir.Model.ResourceReference)Authority.DeepCopy();
				if (When != null) dest.When = (Hl7.Fhir.Model.Order.OrderWhenComponent)When.DeepCopy();
				if (Detail != null) dest.Detail = new List<Hl7.Fhir.Model.ResourceReference>(Detail.DeepCopy());
				return dest;
			}
			else
				throw Error.Argument(nameof(other), "Can only copy to an object of the same type");
		}

		public override IDeepCopyable DeepCopy()
		{
			return CopyTo(new Order());
		}

		public override bool Matches(IDeepComparable other)
		{
			var otherT = other as Order;
			if (otherT is null) return false;

			if (!base.Matches(otherT)) return false;
			if (!DeepComparable.Matches(Identifier, otherT.Identifier)) return false;
			if (!DeepComparable.Matches(DateElement, otherT.DateElement)) return false;
			if (!DeepComparable.Matches(Subject, otherT.Subject)) return false;
			if (!DeepComparable.Matches(Source, otherT.Source)) return false;
			if (!DeepComparable.Matches(Target, otherT.Target)) return false;
			if (!DeepComparable.Matches(Reason, otherT.Reason)) return false;
			if (!DeepComparable.Matches(Authority, otherT.Authority)) return false;
			if (!DeepComparable.Matches(When, otherT.When)) return false;
			if (!DeepComparable.Matches(Detail, otherT.Detail)) return false;

			return true;
		}

		public override bool IsExactly(IDeepComparable other)
		{
			var otherT = other as Order;
			if (otherT is null) return false;

			if (!base.IsExactly(otherT)) return false;
			if (!DeepComparable.IsExactly(Identifier, otherT.Identifier)) return false;
			if (!DeepComparable.IsExactly(DateElement, otherT.DateElement)) return false;
			if (!DeepComparable.IsExactly(Subject, otherT.Subject)) return false;
			if (!DeepComparable.IsExactly(Source, otherT.Source)) return false;
			if (!DeepComparable.IsExactly(Target, otherT.Target)) return false;
			if (!DeepComparable.IsExactly(Reason, otherT.Reason)) return false;
			if (!DeepComparable.IsExactly(Authority, otherT.Authority)) return false;
			if (!DeepComparable.IsExactly(When, otherT.When)) return false;
			if (!DeepComparable.IsExactly(Detail, otherT.Detail)) return false;

			return true;
		}

	}

}
