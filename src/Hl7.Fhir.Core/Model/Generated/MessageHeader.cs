using Hl7.Fhir.Introspection;
using Hl7.Fhir.Support;
using Hl7.Fhir.Validation;
using System;
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
	/// A resource that describes a message that is exchanged between systems
	/// </summary>
	[FhirType("MessageHeader", IsResource = true)]
	[DataContract]
	public partial class MessageHeader : Hl7.Fhir.Model.Resource
	{
		/// <summary>
		/// The kind of response to a message
		/// </summary>
		[FhirEnumeration("ResponseType")]
		public enum ResponseType
		{
			/// <summary>
			/// The message was accepted and processed without error.
			/// </summary>
			[EnumLiteral("ok")]
			Ok,
			/// <summary>
			/// Some internal unexpected error occurred - wait and try again. Note - this is usually used for things like database unavailable, which may be expected to resolve, though human intervention may be required.
			/// </summary>
			[EnumLiteral("transient-error")]
			TransientError,
			/// <summary>
			/// The message was rejected because of some content in it. There is no point in re-sending without change. The response narrative SHALL describe what the issue is.
			/// </summary>
			[EnumLiteral("fatal-error")]
			FatalError,
		}

		[FhirType("MessageDestinationComponent")]
		[DataContract]
		public partial class MessageDestinationComponent : Hl7.Fhir.Model.Element
		{
			/// <summary>
			/// Name of system
			/// </summary>
			[FhirElement("name", InSummary = true, Order = 40)]
			[DataMember]
			public Hl7.Fhir.Model.FhirString NameElement { get; set; }

			/// <summary>
			/// Name of system
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
			/// Particular delivery destination within the destination
			/// </summary>
			[FhirElement("target", InSummary = true, Order = 50)]
			[References("Device")]
			[DataMember]
			public Hl7.Fhir.Model.ResourceReference Target { get; set; }

			/// <summary>
			/// Actual destination address or id
			/// </summary>
			[FhirElement("endpoint", InSummary = true, Order = 60)]
			[Cardinality(Min = 1, Max = 1)]
			[DataMember]
			public Hl7.Fhir.Model.FhirUri EndpointElement { get; set; }

			/// <summary>
			/// Actual destination address or id
			/// </summary>
			/// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
			[NotMapped]
			[IgnoreDataMember]
			public string Endpoint
			{
				get { return EndpointElement?.Value; }
				set
				{
					if (value is null)
						EndpointElement = null;
					else
						EndpointElement = new Hl7.Fhir.Model.FhirUri(value);
				}
			}

			public override IDeepCopyable CopyTo(IDeepCopyable other)
			{
				var dest = other as MessageDestinationComponent;

				if (dest != null)
				{
					base.CopyTo(dest);
					if (NameElement != null) dest.NameElement = (Hl7.Fhir.Model.FhirString)NameElement.DeepCopy();
					if (Target != null) dest.Target = (Hl7.Fhir.Model.ResourceReference)Target.DeepCopy();
					if (EndpointElement != null) dest.EndpointElement = (Hl7.Fhir.Model.FhirUri)EndpointElement.DeepCopy();
					return dest;
				}
				else
					throw Error.Argument(nameof(other), "Can only copy to an object of the same type");
			}

			public override IDeepCopyable DeepCopy()
			{
				return CopyTo(new MessageDestinationComponent());
			}

			public override bool Matches(IDeepComparable other)
			{
				var otherT = other as MessageDestinationComponent;
				if (otherT is null) return false;

				if (!base.Matches(otherT)) return false;
				if (!DeepComparable.Matches(NameElement, otherT.NameElement)) return false;
				if (!DeepComparable.Matches(Target, otherT.Target)) return false;
				if (!DeepComparable.Matches(EndpointElement, otherT.EndpointElement)) return false;

				return true;
			}

			public override bool IsExactly(IDeepComparable other)
			{
				var otherT = other as MessageDestinationComponent;
				if (otherT is null) return false;

				if (!base.IsExactly(otherT)) return false;
				if (!DeepComparable.IsExactly(NameElement, otherT.NameElement)) return false;
				if (!DeepComparable.IsExactly(Target, otherT.Target)) return false;
				if (!DeepComparable.IsExactly(EndpointElement, otherT.EndpointElement)) return false;

				return true;
			}

		}


		[FhirType("MessageSourceComponent")]
		[DataContract]
		public partial class MessageSourceComponent : Hl7.Fhir.Model.Element
		{
			/// <summary>
			/// Name of system
			/// </summary>
			[FhirElement("name", InSummary = true, Order = 40)]
			[DataMember]
			public Hl7.Fhir.Model.FhirString NameElement { get; set; }

			/// <summary>
			/// Name of system
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
			/// Name of software running the system
			/// </summary>
			[FhirElement("software", InSummary = true, Order = 50)]
			[Cardinality(Min = 1, Max = 1)]
			[DataMember]
			public Hl7.Fhir.Model.FhirString SoftwareElement { get; set; }

			/// <summary>
			/// Name of software running the system
			/// </summary>
			/// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
			[NotMapped]
			[IgnoreDataMember]
			public string Software
			{
				get { return SoftwareElement?.Value; }
				set
				{
					if (value is null)
						SoftwareElement = null;
					else
						SoftwareElement = new Hl7.Fhir.Model.FhirString(value);
				}
			}

			/// <summary>
			/// Version of software running
			/// </summary>
			[FhirElement("version", InSummary = true, Order = 60)]
			[DataMember]
			public Hl7.Fhir.Model.FhirString VersionElement { get; set; }

			/// <summary>
			/// Version of software running
			/// </summary>
			/// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
			[NotMapped]
			[IgnoreDataMember]
			public string Version
			{
				get { return VersionElement?.Value; }
				set
				{
					if (value is null)
						VersionElement = null;
					else
						VersionElement = new Hl7.Fhir.Model.FhirString(value);
				}
			}

			/// <summary>
			/// Human contact for problems
			/// </summary>
			[FhirElement("contact", InSummary = true, Order = 70)]
			[DataMember]
			public Hl7.Fhir.Model.Contact Contact { get; set; }

			/// <summary>
			/// Actual message source address or id
			/// </summary>
			[FhirElement("endpoint", InSummary = true, Order = 80)]
			[Cardinality(Min = 1, Max = 1)]
			[DataMember]
			public Hl7.Fhir.Model.FhirUri EndpointElement { get; set; }

			/// <summary>
			/// Actual message source address or id
			/// </summary>
			/// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
			[NotMapped]
			[IgnoreDataMember]
			public string Endpoint
			{
				get { return EndpointElement?.Value; }
				set
				{
					if (value is null)
						EndpointElement = null;
					else
						EndpointElement = new Hl7.Fhir.Model.FhirUri(value);
				}
			}

			public override IDeepCopyable CopyTo(IDeepCopyable other)
			{
				var dest = other as MessageSourceComponent;

				if (dest != null)
				{
					base.CopyTo(dest);
					if (NameElement != null) dest.NameElement = (Hl7.Fhir.Model.FhirString)NameElement.DeepCopy();
					if (SoftwareElement != null) dest.SoftwareElement = (Hl7.Fhir.Model.FhirString)SoftwareElement.DeepCopy();
					if (VersionElement != null) dest.VersionElement = (Hl7.Fhir.Model.FhirString)VersionElement.DeepCopy();
					if (Contact != null) dest.Contact = (Hl7.Fhir.Model.Contact)Contact.DeepCopy();
					if (EndpointElement != null) dest.EndpointElement = (Hl7.Fhir.Model.FhirUri)EndpointElement.DeepCopy();
					return dest;
				}
				else
					throw Error.Argument(nameof(other), "Can only copy to an object of the same type");
			}

			public override IDeepCopyable DeepCopy()
			{
				return CopyTo(new MessageSourceComponent());
			}

			public override bool Matches(IDeepComparable other)
			{
				var otherT = other as MessageSourceComponent;
				if (otherT is null) return false;

				if (!base.Matches(otherT)) return false;
				if (!DeepComparable.Matches(NameElement, otherT.NameElement)) return false;
				if (!DeepComparable.Matches(SoftwareElement, otherT.SoftwareElement)) return false;
				if (!DeepComparable.Matches(VersionElement, otherT.VersionElement)) return false;
				if (!DeepComparable.Matches(Contact, otherT.Contact)) return false;
				if (!DeepComparable.Matches(EndpointElement, otherT.EndpointElement)) return false;

				return true;
			}

			public override bool IsExactly(IDeepComparable other)
			{
				var otherT = other as MessageSourceComponent;
				if (otherT is null) return false;

				if (!base.IsExactly(otherT)) return false;
				if (!DeepComparable.IsExactly(NameElement, otherT.NameElement)) return false;
				if (!DeepComparable.IsExactly(SoftwareElement, otherT.SoftwareElement)) return false;
				if (!DeepComparable.IsExactly(VersionElement, otherT.VersionElement)) return false;
				if (!DeepComparable.IsExactly(Contact, otherT.Contact)) return false;
				if (!DeepComparable.IsExactly(EndpointElement, otherT.EndpointElement)) return false;

				return true;
			}

		}


		[FhirType("MessageHeaderResponseComponent")]
		[DataContract]
		public partial class MessageHeaderResponseComponent : Hl7.Fhir.Model.Element
		{
			/// <summary>
			/// Id of original message
			/// </summary>
			[FhirElement("identifier", InSummary = true, Order = 40)]
			[Cardinality(Min = 1, Max = 1)]
			[DataMember]
			public Hl7.Fhir.Model.Id IdentifierElement { get; set; }

			/// <summary>
			/// Id of original message
			/// </summary>
			/// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
			[NotMapped]
			[IgnoreDataMember]
			public string Identifier
			{
				get { return IdentifierElement?.Value; }
				set
				{
					if (value is null)
						IdentifierElement = null;
					else
						IdentifierElement = new Hl7.Fhir.Model.Id(value);
				}
			}

			/// <summary>
			/// ok | transient-error | fatal-error
			/// </summary>
			[FhirElement("code", InSummary = true, Order = 50)]
			[Cardinality(Min = 1, Max = 1)]
			[DataMember]
			public Code<Hl7.Fhir.Model.MessageHeader.ResponseType> CodeElement { get; set; }

			/// <summary>
			/// ok | transient-error | fatal-error
			/// </summary>
			/// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
			[NotMapped]
			[IgnoreDataMember]
			public Hl7.Fhir.Model.MessageHeader.ResponseType? Code
			{
				get { return CodeElement?.Value; }
				set
				{
					if (value is null)
						CodeElement = null;
					else
						CodeElement = new Code<Hl7.Fhir.Model.MessageHeader.ResponseType>(value);
				}
			}

			/// <summary>
			/// Specific list of hints/warnings/errors
			/// </summary>
			[FhirElement("details", InSummary = true, Order = 60)]
			[References("OperationOutcome")]
			[DataMember]
			public Hl7.Fhir.Model.ResourceReference Details { get; set; }

			public override IDeepCopyable CopyTo(IDeepCopyable other)
			{
				var dest = other as MessageHeaderResponseComponent;

				if (dest != null)
				{
					base.CopyTo(dest);
					if (IdentifierElement != null) dest.IdentifierElement = (Hl7.Fhir.Model.Id)IdentifierElement.DeepCopy();
					if (CodeElement != null) dest.CodeElement = (Code<Hl7.Fhir.Model.MessageHeader.ResponseType>)CodeElement.DeepCopy();
					if (Details != null) dest.Details = (Hl7.Fhir.Model.ResourceReference)Details.DeepCopy();
					return dest;
				}
				else
					throw Error.Argument(nameof(other), "Can only copy to an object of the same type");
			}

			public override IDeepCopyable DeepCopy()
			{
				return CopyTo(new MessageHeaderResponseComponent());
			}

			public override bool Matches(IDeepComparable other)
			{
				var otherT = other as MessageHeaderResponseComponent;
				if (otherT is null) return false;

				if (!base.Matches(otherT)) return false;
				if (!DeepComparable.Matches(IdentifierElement, otherT.IdentifierElement)) return false;
				if (!DeepComparable.Matches(CodeElement, otherT.CodeElement)) return false;
				if (!DeepComparable.Matches(Details, otherT.Details)) return false;

				return true;
			}

			public override bool IsExactly(IDeepComparable other)
			{
				var otherT = other as MessageHeaderResponseComponent;
				if (otherT is null) return false;

				if (!base.IsExactly(otherT)) return false;
				if (!DeepComparable.IsExactly(IdentifierElement, otherT.IdentifierElement)) return false;
				if (!DeepComparable.IsExactly(CodeElement, otherT.CodeElement)) return false;
				if (!DeepComparable.IsExactly(Details, otherT.Details)) return false;

				return true;
			}

		}


		/// <summary>
		/// Id of this message
		/// </summary>
		[FhirElement("identifier", Order = 70)]
		[Cardinality(Min = 1, Max = 1)]
		[DataMember]
		public Hl7.Fhir.Model.Id IdentifierElement { get; set; }

		/// <summary>
		/// Id of this message
		/// </summary>
		/// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
		[NotMapped]
		[IgnoreDataMember]
		public string Identifier
		{
			get { return IdentifierElement?.Value; }
			set
			{
				if (value is null)
					IdentifierElement = null;
				else
					IdentifierElement = new Hl7.Fhir.Model.Id(value);
			}
		}

		/// <summary>
		/// Time that the message was sent
		/// </summary>
		[FhirElement("timestamp", Order = 80)]
		[Cardinality(Min = 1, Max = 1)]
		[DataMember]
		public Hl7.Fhir.Model.Instant TimestampElement { get; set; }

		/// <summary>
		/// Time that the message was sent
		/// </summary>
		/// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
		[NotMapped]
		[IgnoreDataMember]
		public DateTimeOffset? Timestamp
		{
			get { return TimestampElement?.Value; }
			set
			{
				if (value is null)
					TimestampElement = null;
				else
					TimestampElement = new Hl7.Fhir.Model.Instant(value);
			}
		}

		/// <summary>
		/// Code for the event this message represents
		/// </summary>
		[FhirElement("event", Order = 90)]
		[Cardinality(Min = 1, Max = 1)]
		[DataMember]
		public Hl7.Fhir.Model.Coding Event { get; set; }

		/// <summary>
		/// If this is a reply to prior message
		/// </summary>
		[FhirElement("response", Order = 100)]
		[DataMember]
		public Hl7.Fhir.Model.MessageHeader.MessageHeaderResponseComponent Response { get; set; }

		/// <summary>
		/// Message Source Application
		/// </summary>
		[FhirElement("source", Order = 110)]
		[Cardinality(Min = 1, Max = 1)]
		[DataMember]
		public Hl7.Fhir.Model.MessageHeader.MessageSourceComponent Source { get; set; }

		/// <summary>
		/// Message Destination Application(s)
		/// </summary>
		[FhirElement("destination", Order = 120)]
		[Cardinality(Min = 0, Max = -1)]
		[DataMember]
		public List<Hl7.Fhir.Model.MessageHeader.MessageDestinationComponent> Destination { get; set; }

		/// <summary>
		/// The source of the data entry
		/// </summary>
		[FhirElement("enterer", Order = 130)]
		[References("Practitioner")]
		[DataMember]
		public Hl7.Fhir.Model.ResourceReference Enterer { get; set; }

		/// <summary>
		/// The source of the decision
		/// </summary>
		[FhirElement("author", Order = 140)]
		[References("Practitioner")]
		[DataMember]
		public Hl7.Fhir.Model.ResourceReference Author { get; set; }

		/// <summary>
		/// Intended "real-world" recipient for the data
		/// </summary>
		[FhirElement("receiver", Order = 150)]
		[References("Practitioner", "Organization")]
		[DataMember]
		public Hl7.Fhir.Model.ResourceReference Receiver { get; set; }

		/// <summary>
		/// Final responsibility for event
		/// </summary>
		[FhirElement("responsible", Order = 160)]
		[References("Practitioner", "Organization")]
		[DataMember]
		public Hl7.Fhir.Model.ResourceReference Responsible { get; set; }

		/// <summary>
		/// Cause of event
		/// </summary>
		[FhirElement("reason", Order = 170)]
		[DataMember]
		public Hl7.Fhir.Model.CodeableConcept Reason { get; set; }

		/// <summary>
		/// The actual content of the message
		/// </summary>
		[FhirElement("data", Order = 180)]
		[References()]
		[Cardinality(Min = 0, Max = -1)]
		[DataMember]
		public List<Hl7.Fhir.Model.ResourceReference> Data { get; set; }

		public override IDeepCopyable CopyTo(IDeepCopyable other)
		{
			var dest = other as MessageHeader;

			if (dest != null)
			{
				base.CopyTo(dest);
				if (IdentifierElement != null) dest.IdentifierElement = (Hl7.Fhir.Model.Id)IdentifierElement.DeepCopy();
				if (TimestampElement != null) dest.TimestampElement = (Hl7.Fhir.Model.Instant)TimestampElement.DeepCopy();
				if (Event != null) dest.Event = (Hl7.Fhir.Model.Coding)Event.DeepCopy();
				if (Response != null) dest.Response = (Hl7.Fhir.Model.MessageHeader.MessageHeaderResponseComponent)Response.DeepCopy();
				if (Source != null) dest.Source = (Hl7.Fhir.Model.MessageHeader.MessageSourceComponent)Source.DeepCopy();
				if (Destination != null) dest.Destination = new List<Hl7.Fhir.Model.MessageHeader.MessageDestinationComponent>(Destination.DeepCopy());
				if (Enterer != null) dest.Enterer = (Hl7.Fhir.Model.ResourceReference)Enterer.DeepCopy();
				if (Author != null) dest.Author = (Hl7.Fhir.Model.ResourceReference)Author.DeepCopy();
				if (Receiver != null) dest.Receiver = (Hl7.Fhir.Model.ResourceReference)Receiver.DeepCopy();
				if (Responsible != null) dest.Responsible = (Hl7.Fhir.Model.ResourceReference)Responsible.DeepCopy();
				if (Reason != null) dest.Reason = (Hl7.Fhir.Model.CodeableConcept)Reason.DeepCopy();
				if (Data != null) dest.Data = new List<Hl7.Fhir.Model.ResourceReference>(Data.DeepCopy());
				return dest;
			}
			else
				throw Error.Argument(nameof(other), "Can only copy to an object of the same type");
		}

		public override IDeepCopyable DeepCopy()
		{
			return CopyTo(new MessageHeader());
		}

		public override bool Matches(IDeepComparable other)
		{
			var otherT = other as MessageHeader;
			if (otherT is null) return false;

			if (!base.Matches(otherT)) return false;
			if (!DeepComparable.Matches(IdentifierElement, otherT.IdentifierElement)) return false;
			if (!DeepComparable.Matches(TimestampElement, otherT.TimestampElement)) return false;
			if (!DeepComparable.Matches(Event, otherT.Event)) return false;
			if (!DeepComparable.Matches(Response, otherT.Response)) return false;
			if (!DeepComparable.Matches(Source, otherT.Source)) return false;
			if (!DeepComparable.Matches(Destination, otherT.Destination)) return false;
			if (!DeepComparable.Matches(Enterer, otherT.Enterer)) return false;
			if (!DeepComparable.Matches(Author, otherT.Author)) return false;
			if (!DeepComparable.Matches(Receiver, otherT.Receiver)) return false;
			if (!DeepComparable.Matches(Responsible, otherT.Responsible)) return false;
			if (!DeepComparable.Matches(Reason, otherT.Reason)) return false;
			if (!DeepComparable.Matches(Data, otherT.Data)) return false;

			return true;
		}

		public override bool IsExactly(IDeepComparable other)
		{
			var otherT = other as MessageHeader;
			if (otherT is null) return false;

			if (!base.IsExactly(otherT)) return false;
			if (!DeepComparable.IsExactly(IdentifierElement, otherT.IdentifierElement)) return false;
			if (!DeepComparable.IsExactly(TimestampElement, otherT.TimestampElement)) return false;
			if (!DeepComparable.IsExactly(Event, otherT.Event)) return false;
			if (!DeepComparable.IsExactly(Response, otherT.Response)) return false;
			if (!DeepComparable.IsExactly(Source, otherT.Source)) return false;
			if (!DeepComparable.IsExactly(Destination, otherT.Destination)) return false;
			if (!DeepComparable.IsExactly(Enterer, otherT.Enterer)) return false;
			if (!DeepComparable.IsExactly(Author, otherT.Author)) return false;
			if (!DeepComparable.IsExactly(Receiver, otherT.Receiver)) return false;
			if (!DeepComparable.IsExactly(Responsible, otherT.Responsible)) return false;
			if (!DeepComparable.IsExactly(Reason, otherT.Reason)) return false;
			if (!DeepComparable.IsExactly(Data, otherT.Data)) return false;

			return true;
		}

	}

}
