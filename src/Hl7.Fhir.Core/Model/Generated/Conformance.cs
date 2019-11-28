﻿using Hl7.Fhir.Introspection;
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
	/// A conformance statement
	/// </summary>
	[FhirType("Conformance", IsResource = true)]
	[DataContract]
	public partial class Conformance : Hl7.Fhir.Model.Resource
	{
		/// <summary>
		/// Whether the application produces or consumes documents
		/// </summary>
		[FhirEnumeration("DocumentMode")]
		public enum DocumentMode
		{
			/// <summary>
			/// The application produces documents of the specified type.
			/// </summary>
			[EnumLiteral("producer")]
			Producer,
			/// <summary>
			/// The application consumes documents of the specified type.
			/// </summary>
			[EnumLiteral("consumer")]
			Consumer,
		}

		/// <summary>
		/// The mode of a RESTful conformance statement
		/// </summary>
		[FhirEnumeration("RestfulConformanceMode")]
		public enum RestfulConformanceMode
		{
			/// <summary>
			/// The application acts as a server for this resource.
			/// </summary>
			[EnumLiteral("client")]
			Client,
			/// <summary>
			/// The application acts as a client for this resource.
			/// </summary>
			[EnumLiteral("server")]
			Server,
		}

		/// <summary>
		/// The protocol used for message transport
		/// </summary>
		[FhirEnumeration("MessageTransport")]
		public enum MessageTransport
		{
			/// <summary>
			/// The application sends or receives messages using HTTP POST (may be over http or https).
			/// </summary>
			[EnumLiteral("http")]
			Http,
			/// <summary>
			/// The application sends or receives messages using File Transfer Protocol.
			/// </summary>
			[EnumLiteral("ftp")]
			Ftp,
			/// <summary>
			/// The application sends or receivers messages using HL7's Minimal Lower Level Protocol.
			/// </summary>
			[EnumLiteral("mllp")]
			Mllp,
		}

		/// <summary>
		/// The mode of a message conformance statement
		/// </summary>
		[FhirEnumeration("ConformanceEventMode")]
		public enum ConformanceEventMode
		{
			/// <summary>
			/// The application sends requests and receives responses.
			/// </summary>
			[EnumLiteral("sender")]
			Sender,
			/// <summary>
			/// The application receives requests and sends responses.
			/// </summary>
			[EnumLiteral("receiver")]
			Receiver,
		}

		/// <summary>
		/// The impact of the content of a message
		/// </summary>
		[FhirEnumeration("MessageSignificanceCategory")]
		public enum MessageSignificanceCategory
		{
			/// <summary>
			/// The message represents/requests a change that should not be processed more than once. E.g. Making a booking for an appointment.
			/// </summary>
			[EnumLiteral("Consequence")]
			Consequence,
			/// <summary>
			/// The message represents a response to query for current information. Retrospective processing is wrong and/or wasteful.
			/// </summary>
			[EnumLiteral("Currency")]
			Currency,
			/// <summary>
			/// The content is not necessarily intended to be current, and it can be reprocessed, though there may be version issues created by processing old notifications.
			/// </summary>
			[EnumLiteral("Notification")]
			Notification,
		}

		/// <summary>
		/// Operations supported by REST at the type or instance level
		/// </summary>
		[FhirEnumeration("RestfulOperationType")]
		public enum RestfulOperationType
		{
			[EnumLiteral("read")]
			Read,
			[EnumLiteral("vread")]
			Vread,
			[EnumLiteral("update")]
			Update,
			[EnumLiteral("delete")]
			Delete,
			[EnumLiteral("history-instance")]
			HistoryInstance,
			[EnumLiteral("validate")]
			Validate,
			[EnumLiteral("history-type")]
			HistoryType,
			[EnumLiteral("create")]
			Create,
			[EnumLiteral("search-type")]
			SearchType,
		}

		/// <summary>
		/// The status of this conformance statement
		/// </summary>
		[FhirEnumeration("ConformanceStatementStatus")]
		public enum ConformanceStatementStatus
		{
			/// <summary>
			/// This conformance statement is still under development.
			/// </summary>
			[EnumLiteral("draft")]
			Draft,
			/// <summary>
			/// This conformance statement is ready for use in production systems.
			/// </summary>
			[EnumLiteral("active")]
			Active,
			/// <summary>
			/// This conformance statement has been withdrawn or superceded and should no longer be used.
			/// </summary>
			[EnumLiteral("retired")]
			Retired,
		}

		/// <summary>
		/// Operations supported by REST at the system level
		/// </summary>
		[FhirEnumeration("RestfulOperationSystem")]
		public enum RestfulOperationSystem
		{
			[EnumLiteral("transaction")]
			Transaction,
			[EnumLiteral("search-system")]
			SearchSystem,
			[EnumLiteral("history-system")]
			HistorySystem,
		}

		/// <summary>
		/// Data types allowed to be used for search parameters
		/// </summary>
		[FhirEnumeration("SearchParamType")]
		public enum SearchParamType
		{
			/// <summary>
			/// Search parameter SHALL be a number (a whole number, or a decimal).
			/// </summary>
			[EnumLiteral("number")]
			Number,
			/// <summary>
			/// Search parameter is on a date/time. The date format is the standard XML format, though other formats may be supported.
			/// </summary>
			[EnumLiteral("date")]
			Date,
			/// <summary>
			/// Search parameter is a simple string, like a name part. Search is case-insensitive and accent-insensitive. May match just the start of a string. String parameters may contain spaces.
			/// </summary>
			[EnumLiteral("string")]
			String,
			/// <summary>
			/// Search parameter on a coded element or identifier. May be used to search through the text, displayname, code and code/codesystem (for codes) and label, system and key (for identifier). Its value is either a string or a pair of namespace and value, separated by a "|", depending on the modifier used.
			/// </summary>
			[EnumLiteral("token")]
			Token,
			/// <summary>
			/// A reference to another resource.
			/// </summary>
			[EnumLiteral("reference")]
			Reference,
			/// <summary>
			/// A composite search parameter that combines a search on two values together.
			/// </summary>
			[EnumLiteral("composite")]
			Composite,
			/// <summary>
			/// A search parameter that searches on a quantity.
			/// </summary>
			[EnumLiteral("quantity")]
			Quantity,
		}

		/// <summary>
		/// Types of security services used with FHIR
		/// </summary>
		[FhirEnumeration("RestfulSecurityService")]
		public enum RestfulSecurityService
		{
			/// <summary>
			/// OAuth (see oauth.net).
			/// </summary>
			[EnumLiteral("OAuth")]
			OAuth,
			/// <summary>
			/// OAuth version 2 (see oauth.net).
			/// </summary>
			[EnumLiteral("OAuth2")]
			OAuth2,
			/// <summary>
			/// Microsoft NTLM Authentication.
			/// </summary>
			[EnumLiteral("NTLM")]
			NTLM,
			/// <summary>
			/// Basic authentication defined in HTTP specification.
			/// </summary>
			[EnumLiteral("Basic")]
			Basic,
			/// <summary>
			/// see http://www.ietf.org/rfc/rfc4120.txt.
			/// </summary>
			[EnumLiteral("Kerberos")]
			Kerberos,
		}

		[FhirType("ConformanceRestQueryComponent")]
		[DataContract]
		public partial class ConformanceRestQueryComponent : Hl7.Fhir.Model.Element
		{
			/// <summary>
			/// Special named queries (_query=)
			/// </summary>
			[FhirElement("name", InSummary = true, Order = 40)]
			[Cardinality(Min = 1, Max = 1)]
			[DataMember]
			public Hl7.Fhir.Model.FhirString NameElement { get; set; }

			/// <summary>
			/// Special named queries (_query=)
			/// </summary>
			/// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
			[NotMapped]
			[IgnoreDataMember]
			public string Name
			{
				get { return NameElement != null ? NameElement.Value : null; }
				set
				{
					if (value == null)
						NameElement = null;
					else
						NameElement = new Hl7.Fhir.Model.FhirString(value);
				}
			}

			/// <summary>
			/// Where query is defined
			/// </summary>
			[FhirElement("definition", InSummary = true, Order = 50)]
			[Cardinality(Min = 1, Max = 1)]
			[DataMember]
			public Hl7.Fhir.Model.FhirUri DefinitionElement { get; set; }

			/// <summary>
			/// Where query is defined
			/// </summary>
			/// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
			[NotMapped]
			[IgnoreDataMember]
			public string Definition
			{
				get { return DefinitionElement != null ? DefinitionElement.Value : null; }
				set
				{
					if (value == null)
						DefinitionElement = null;
					else
						DefinitionElement = new Hl7.Fhir.Model.FhirUri(value);
				}
			}

			/// <summary>
			/// Additional usage guidance
			/// </summary>
			[FhirElement("documentation", InSummary = true, Order = 60)]
			[DataMember]
			public Hl7.Fhir.Model.FhirString DocumentationElement { get; set; }

			/// <summary>
			/// Additional usage guidance
			/// </summary>
			/// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
			[NotMapped]
			[IgnoreDataMember]
			public string Documentation
			{
				get { return DocumentationElement != null ? DocumentationElement.Value : null; }
				set
				{
					if (value == null)
						DocumentationElement = null;
					else
						DocumentationElement = new Hl7.Fhir.Model.FhirString(value);
				}
			}

			/// <summary>
			/// Parameter for the named query
			/// </summary>
			[FhirElement("parameter", InSummary = true, Order = 70)]
			[Cardinality(Min = 0, Max = -1)]
			[DataMember]
			public List<Hl7.Fhir.Model.Conformance.ConformanceRestResourceSearchParamComponent> Parameter { get; set; }

			public override IDeepCopyable CopyTo(IDeepCopyable other)
			{
				var dest = other as ConformanceRestQueryComponent;

				if (dest != null)
				{
					base.CopyTo(dest);
					if (NameElement != null) dest.NameElement = (Hl7.Fhir.Model.FhirString)NameElement.DeepCopy();
					if (DefinitionElement != null) dest.DefinitionElement = (Hl7.Fhir.Model.FhirUri)DefinitionElement.DeepCopy();
					if (DocumentationElement != null) dest.DocumentationElement = (Hl7.Fhir.Model.FhirString)DocumentationElement.DeepCopy();
					if (Parameter != null) dest.Parameter = new List<Hl7.Fhir.Model.Conformance.ConformanceRestResourceSearchParamComponent>(Parameter.DeepCopy());
					return dest;
				}
				else
					throw Error.Argument(nameof(other), "Can only copy to an object of the same type");
			}

			public override IDeepCopyable DeepCopy()
			{
				return CopyTo(new ConformanceRestQueryComponent());
			}

			public override bool Matches(IDeepComparable other)
			{
				var otherT = other as ConformanceRestQueryComponent;
				if (otherT == null) return false;

				if (!base.Matches(otherT)) return false;
				if (!DeepComparable.Matches(NameElement, otherT.NameElement)) return false;
				if (!DeepComparable.Matches(DefinitionElement, otherT.DefinitionElement)) return false;
				if (!DeepComparable.Matches(DocumentationElement, otherT.DocumentationElement)) return false;
				if (!DeepComparable.Matches(Parameter, otherT.Parameter)) return false;

				return true;
			}

			public override bool IsExactly(IDeepComparable other)
			{
				var otherT = other as ConformanceRestQueryComponent;
				if (otherT == null) return false;

				if (!base.IsExactly(otherT)) return false;
				if (!DeepComparable.IsExactly(NameElement, otherT.NameElement)) return false;
				if (!DeepComparable.IsExactly(DefinitionElement, otherT.DefinitionElement)) return false;
				if (!DeepComparable.IsExactly(DocumentationElement, otherT.DocumentationElement)) return false;
				if (!DeepComparable.IsExactly(Parameter, otherT.Parameter)) return false;

				return true;
			}

		}


		[FhirType("ConformanceRestComponent")]
		[DataContract]
		public partial class ConformanceRestComponent : Hl7.Fhir.Model.Element
		{
			/// <summary>
			/// client | server
			/// </summary>
			[FhirElement("mode", InSummary = true, Order = 40)]
			[Cardinality(Min = 1, Max = 1)]
			[DataMember]
			public Code<Hl7.Fhir.Model.Conformance.RestfulConformanceMode> ModeElement { get; set; }

			/// <summary>
			/// client | server
			/// </summary>
			/// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
			[NotMapped]
			[IgnoreDataMember]
			public Hl7.Fhir.Model.Conformance.RestfulConformanceMode? Mode
			{
				get { return ModeElement != null ? ModeElement.Value : null; }
				set
				{
					if (value == null)
						ModeElement = null;
					else
						ModeElement = new Code<Hl7.Fhir.Model.Conformance.RestfulConformanceMode>(value);
				}
			}

			/// <summary>
			/// General description of implementation
			/// </summary>
			[FhirElement("documentation", InSummary = true, Order = 50)]
			[DataMember]
			public Hl7.Fhir.Model.FhirString DocumentationElement { get; set; }

			/// <summary>
			/// General description of implementation
			/// </summary>
			/// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
			[NotMapped]
			[IgnoreDataMember]
			public string Documentation
			{
				get { return DocumentationElement != null ? DocumentationElement.Value : null; }
				set
				{
					if (value == null)
						DocumentationElement = null;
					else
						DocumentationElement = new Hl7.Fhir.Model.FhirString(value);
				}
			}

			/// <summary>
			/// Information about security of implementation
			/// </summary>
			[FhirElement("security", InSummary = true, Order = 60)]
			[DataMember]
			public Hl7.Fhir.Model.Conformance.ConformanceRestSecurityComponent Security { get; set; }

			/// <summary>
			/// Resource served on the REST interface
			/// </summary>
			[FhirElement("resource", InSummary = true, Order = 70)]
			[Cardinality(Min = 1, Max = -1)]
			[DataMember]
			public List<Hl7.Fhir.Model.Conformance.ConformanceRestResourceComponent> Resource { get; set; }

			/// <summary>
			/// What operations are supported?
			/// </summary>
			[FhirElement("operation", InSummary = true, Order = 80)]
			[Cardinality(Min = 0, Max = -1)]
			[DataMember]
			public List<Hl7.Fhir.Model.Conformance.ConformanceRestOperationComponent> Operation { get; set; }

			/// <summary>
			/// Definition of a named query
			/// </summary>
			[FhirElement("query", InSummary = true, Order = 90)]
			[Cardinality(Min = 0, Max = -1)]
			[DataMember]
			public List<Hl7.Fhir.Model.Conformance.ConformanceRestQueryComponent> Query { get; set; }

			/// <summary>
			/// How documents are accepted in /Mailbox
			/// </summary>
			[FhirElement("documentMailbox", InSummary = true, Order = 100)]
			[Cardinality(Min = 0, Max = -1)]
			[DataMember]
			public List<Hl7.Fhir.Model.FhirUri> DocumentMailboxElement { get; set; }

			/// <summary>
			/// How documents are accepted in /Mailbox
			/// </summary>
			/// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
			[NotMapped]
			[IgnoreDataMember]
			public IEnumerable<string> DocumentMailbox
			{
				get { return DocumentMailboxElement != null ? DocumentMailboxElement.Select(elem => elem.Value) : null; }
				set
				{
					if (value == null)
						DocumentMailboxElement = null;
					else
						DocumentMailboxElement = new List<Hl7.Fhir.Model.FhirUri>(value.Select(elem => new Hl7.Fhir.Model.FhirUri(elem)));
				}
			}

			public override IDeepCopyable CopyTo(IDeepCopyable other)
			{
				var dest = other as ConformanceRestComponent;

				if (dest != null)
				{
					base.CopyTo(dest);
					if (ModeElement != null) dest.ModeElement = (Code<Hl7.Fhir.Model.Conformance.RestfulConformanceMode>)ModeElement.DeepCopy();
					if (DocumentationElement != null) dest.DocumentationElement = (Hl7.Fhir.Model.FhirString)DocumentationElement.DeepCopy();
					if (Security != null) dest.Security = (Hl7.Fhir.Model.Conformance.ConformanceRestSecurityComponent)Security.DeepCopy();
					if (Resource != null) dest.Resource = new List<Hl7.Fhir.Model.Conformance.ConformanceRestResourceComponent>(Resource.DeepCopy());
					if (Operation != null) dest.Operation = new List<Hl7.Fhir.Model.Conformance.ConformanceRestOperationComponent>(Operation.DeepCopy());
					if (Query != null) dest.Query = new List<Hl7.Fhir.Model.Conformance.ConformanceRestQueryComponent>(Query.DeepCopy());
					if (DocumentMailboxElement != null) dest.DocumentMailboxElement = new List<Hl7.Fhir.Model.FhirUri>(DocumentMailboxElement.DeepCopy());
					return dest;
				}
				else
					throw Error.Argument(nameof(other), "Can only copy to an object of the same type");
			}

			public override IDeepCopyable DeepCopy()
			{
				return CopyTo(new ConformanceRestComponent());
			}

			public override bool Matches(IDeepComparable other)
			{
				var otherT = other as ConformanceRestComponent;
				if (otherT == null) return false;

				if (!base.Matches(otherT)) return false;
				if (!DeepComparable.Matches(ModeElement, otherT.ModeElement)) return false;
				if (!DeepComparable.Matches(DocumentationElement, otherT.DocumentationElement)) return false;
				if (!DeepComparable.Matches(Security, otherT.Security)) return false;
				if (!DeepComparable.Matches(Resource, otherT.Resource)) return false;
				if (!DeepComparable.Matches(Operation, otherT.Operation)) return false;
				if (!DeepComparable.Matches(Query, otherT.Query)) return false;
				if (!DeepComparable.Matches(DocumentMailboxElement, otherT.DocumentMailboxElement)) return false;

				return true;
			}

			public override bool IsExactly(IDeepComparable other)
			{
				var otherT = other as ConformanceRestComponent;
				if (otherT == null) return false;

				if (!base.IsExactly(otherT)) return false;
				if (!DeepComparable.IsExactly(ModeElement, otherT.ModeElement)) return false;
				if (!DeepComparable.IsExactly(DocumentationElement, otherT.DocumentationElement)) return false;
				if (!DeepComparable.IsExactly(Security, otherT.Security)) return false;
				if (!DeepComparable.IsExactly(Resource, otherT.Resource)) return false;
				if (!DeepComparable.IsExactly(Operation, otherT.Operation)) return false;
				if (!DeepComparable.IsExactly(Query, otherT.Query)) return false;
				if (!DeepComparable.IsExactly(DocumentMailboxElement, otherT.DocumentMailboxElement)) return false;

				return true;
			}

		}


		[FhirType("ConformanceSoftwareComponent")]
		[DataContract]
		public partial class ConformanceSoftwareComponent : Hl7.Fhir.Model.Element
		{
			/// <summary>
			/// A name the software is known by
			/// </summary>
			[FhirElement("name", InSummary = true, Order = 40)]
			[Cardinality(Min = 1, Max = 1)]
			[DataMember]
			public Hl7.Fhir.Model.FhirString NameElement { get; set; }

			/// <summary>
			/// A name the software is known by
			/// </summary>
			/// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
			[NotMapped]
			[IgnoreDataMember]
			public string Name
			{
				get { return NameElement != null ? NameElement.Value : null; }
				set
				{
					if (value == null)
						NameElement = null;
					else
						NameElement = new Hl7.Fhir.Model.FhirString(value);
				}
			}

			/// <summary>
			/// Version covered by this statement
			/// </summary>
			[FhirElement("version", InSummary = true, Order = 50)]
			[DataMember]
			public Hl7.Fhir.Model.FhirString VersionElement { get; set; }

			/// <summary>
			/// Version covered by this statement
			/// </summary>
			/// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
			[NotMapped]
			[IgnoreDataMember]
			public string Version
			{
				get { return VersionElement != null ? VersionElement.Value : null; }
				set
				{
					if (value == null)
						VersionElement = null;
					else
						VersionElement = new Hl7.Fhir.Model.FhirString(value);
				}
			}

			/// <summary>
			/// Date this version released
			/// </summary>
			[FhirElement("releaseDate", InSummary = true, Order = 60)]
			[DataMember]
			public Hl7.Fhir.Model.FhirDateTime ReleaseDateElement { get; set; }

			/// <summary>
			/// Date this version released
			/// </summary>
			/// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
			[NotMapped]
			[IgnoreDataMember]
			public string ReleaseDate
			{
				get { return ReleaseDateElement != null ? ReleaseDateElement.Value : null; }
				set
				{
					if (value == null)
						ReleaseDateElement = null;
					else
						ReleaseDateElement = new Hl7.Fhir.Model.FhirDateTime(value);
				}
			}

			public override IDeepCopyable CopyTo(IDeepCopyable other)
			{
				var dest = other as ConformanceSoftwareComponent;

				if (dest != null)
				{
					base.CopyTo(dest);
					if (NameElement != null) dest.NameElement = (Hl7.Fhir.Model.FhirString)NameElement.DeepCopy();
					if (VersionElement != null) dest.VersionElement = (Hl7.Fhir.Model.FhirString)VersionElement.DeepCopy();
					if (ReleaseDateElement != null) dest.ReleaseDateElement = (Hl7.Fhir.Model.FhirDateTime)ReleaseDateElement.DeepCopy();
					return dest;
				}
				else
					throw Error.Argument(nameof(other), "Can only copy to an object of the same type");
			}

			public override IDeepCopyable DeepCopy()
			{
				return CopyTo(new ConformanceSoftwareComponent());
			}

			public override bool Matches(IDeepComparable other)
			{
				var otherT = other as ConformanceSoftwareComponent;
				if (otherT == null) return false;

				if (!base.Matches(otherT)) return false;
				if (!DeepComparable.Matches(NameElement, otherT.NameElement)) return false;
				if (!DeepComparable.Matches(VersionElement, otherT.VersionElement)) return false;
				if (!DeepComparable.Matches(ReleaseDateElement, otherT.ReleaseDateElement)) return false;

				return true;
			}

			public override bool IsExactly(IDeepComparable other)
			{
				var otherT = other as ConformanceSoftwareComponent;
				if (otherT == null) return false;

				if (!base.IsExactly(otherT)) return false;
				if (!DeepComparable.IsExactly(NameElement, otherT.NameElement)) return false;
				if (!DeepComparable.IsExactly(VersionElement, otherT.VersionElement)) return false;
				if (!DeepComparable.IsExactly(ReleaseDateElement, otherT.ReleaseDateElement)) return false;

				return true;
			}

		}


		[FhirType("ConformanceMessagingComponent")]
		[DataContract]
		public partial class ConformanceMessagingComponent : Hl7.Fhir.Model.Element
		{
			/// <summary>
			/// Actual endpoint being described
			/// </summary>
			[FhirElement("endpoint", InSummary = true, Order = 40)]
			[DataMember]
			public Hl7.Fhir.Model.FhirUri EndpointElement { get; set; }

			/// <summary>
			/// Actual endpoint being described
			/// </summary>
			/// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
			[NotMapped]
			[IgnoreDataMember]
			public string Endpoint
			{
				get { return EndpointElement != null ? EndpointElement.Value : null; }
				set
				{
					if (value == null)
						EndpointElement = null;
					else
						EndpointElement = new Hl7.Fhir.Model.FhirUri(value);
				}
			}

			/// <summary>
			/// Reliable Message Cache Length
			/// </summary>
			[FhirElement("reliableCache", InSummary = true, Order = 50)]
			[DataMember]
			public Hl7.Fhir.Model.Integer ReliableCacheElement { get; set; }

			/// <summary>
			/// Reliable Message Cache Length
			/// </summary>
			/// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
			[NotMapped]
			[IgnoreDataMember]
			public int? ReliableCache
			{
				get { return ReliableCacheElement != null ? ReliableCacheElement.Value : null; }
				set
				{
					if (value == null)
						ReliableCacheElement = null;
					else
						ReliableCacheElement = new Hl7.Fhir.Model.Integer(value);
				}
			}

			/// <summary>
			/// Messaging interface behavior details
			/// </summary>
			[FhirElement("documentation", InSummary = true, Order = 60)]
			[DataMember]
			public Hl7.Fhir.Model.FhirString DocumentationElement { get; set; }

			/// <summary>
			/// Messaging interface behavior details
			/// </summary>
			/// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
			[NotMapped]
			[IgnoreDataMember]
			public string Documentation
			{
				get { return DocumentationElement != null ? DocumentationElement.Value : null; }
				set
				{
					if (value == null)
						DocumentationElement = null;
					else
						DocumentationElement = new Hl7.Fhir.Model.FhirString(value);
				}
			}

			/// <summary>
			/// Declare support for this event
			/// </summary>
			[FhirElement("event", InSummary = true, Order = 70)]
			[Cardinality(Min = 1, Max = -1)]
			[DataMember]
			public List<Hl7.Fhir.Model.Conformance.ConformanceMessagingEventComponent> Event { get; set; }

			public override IDeepCopyable CopyTo(IDeepCopyable other)
			{
				var dest = other as ConformanceMessagingComponent;

				if (dest != null)
				{
					base.CopyTo(dest);
					if (EndpointElement != null) dest.EndpointElement = (Hl7.Fhir.Model.FhirUri)EndpointElement.DeepCopy();
					if (ReliableCacheElement != null) dest.ReliableCacheElement = (Hl7.Fhir.Model.Integer)ReliableCacheElement.DeepCopy();
					if (DocumentationElement != null) dest.DocumentationElement = (Hl7.Fhir.Model.FhirString)DocumentationElement.DeepCopy();
					if (Event != null) dest.Event = new List<Hl7.Fhir.Model.Conformance.ConformanceMessagingEventComponent>(Event.DeepCopy());
					return dest;
				}
				else
					throw Error.Argument(nameof(other), "Can only copy to an object of the same type");
			}

			public override IDeepCopyable DeepCopy()
			{
				return CopyTo(new ConformanceMessagingComponent());
			}

			public override bool Matches(IDeepComparable other)
			{
				var otherT = other as ConformanceMessagingComponent;
				if (otherT == null) return false;

				if (!base.Matches(otherT)) return false;
				if (!DeepComparable.Matches(EndpointElement, otherT.EndpointElement)) return false;
				if (!DeepComparable.Matches(ReliableCacheElement, otherT.ReliableCacheElement)) return false;
				if (!DeepComparable.Matches(DocumentationElement, otherT.DocumentationElement)) return false;
				if (!DeepComparable.Matches(Event, otherT.Event)) return false;

				return true;
			}

			public override bool IsExactly(IDeepComparable other)
			{
				var otherT = other as ConformanceMessagingComponent;
				if (otherT == null) return false;

				if (!base.IsExactly(otherT)) return false;
				if (!DeepComparable.IsExactly(EndpointElement, otherT.EndpointElement)) return false;
				if (!DeepComparable.IsExactly(ReliableCacheElement, otherT.ReliableCacheElement)) return false;
				if (!DeepComparable.IsExactly(DocumentationElement, otherT.DocumentationElement)) return false;
				if (!DeepComparable.IsExactly(Event, otherT.Event)) return false;

				return true;
			}

		}


		[FhirType("ConformanceDocumentComponent")]
		[DataContract]
		public partial class ConformanceDocumentComponent : Hl7.Fhir.Model.Element
		{
			/// <summary>
			/// producer | consumer
			/// </summary>
			[FhirElement("mode", InSummary = true, Order = 40)]
			[Cardinality(Min = 1, Max = 1)]
			[DataMember]
			public Code<Hl7.Fhir.Model.Conformance.DocumentMode> ModeElement { get; set; }

			/// <summary>
			/// producer | consumer
			/// </summary>
			/// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
			[NotMapped]
			[IgnoreDataMember]
			public Hl7.Fhir.Model.Conformance.DocumentMode? Mode
			{
				get { return ModeElement != null ? ModeElement.Value : null; }
				set
				{
					if (value == null)
						ModeElement = null;
					else
						ModeElement = new Code<Hl7.Fhir.Model.Conformance.DocumentMode>(value);
				}
			}

			/// <summary>
			/// Description of document support
			/// </summary>
			[FhirElement("documentation", InSummary = true, Order = 50)]
			[DataMember]
			public Hl7.Fhir.Model.FhirString DocumentationElement { get; set; }

			/// <summary>
			/// Description of document support
			/// </summary>
			/// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
			[NotMapped]
			[IgnoreDataMember]
			public string Documentation
			{
				get { return DocumentationElement != null ? DocumentationElement.Value : null; }
				set
				{
					if (value == null)
						DocumentationElement = null;
					else
						DocumentationElement = new Hl7.Fhir.Model.FhirString(value);
				}
			}

			/// <summary>
			/// Constraint on a resource used in the document
			/// </summary>
			[FhirElement("profile", InSummary = true, Order = 60)]
			[References("Profile")]
			[Cardinality(Min = 1, Max = 1)]
			[DataMember]
			public Hl7.Fhir.Model.ResourceReference Profile { get; set; }

			public override IDeepCopyable CopyTo(IDeepCopyable other)
			{
				var dest = other as ConformanceDocumentComponent;

				if (dest != null)
				{
					base.CopyTo(dest);
					if (ModeElement != null) dest.ModeElement = (Code<Hl7.Fhir.Model.Conformance.DocumentMode>)ModeElement.DeepCopy();
					if (DocumentationElement != null) dest.DocumentationElement = (Hl7.Fhir.Model.FhirString)DocumentationElement.DeepCopy();
					if (Profile != null) dest.Profile = (Hl7.Fhir.Model.ResourceReference)Profile.DeepCopy();
					return dest;
				}
				else
					throw Error.Argument(nameof(other), "Can only copy to an object of the same type");
			}

			public override IDeepCopyable DeepCopy()
			{
				return CopyTo(new ConformanceDocumentComponent());
			}

			public override bool Matches(IDeepComparable other)
			{
				var otherT = other as ConformanceDocumentComponent;
				if (otherT == null) return false;

				if (!base.Matches(otherT)) return false;
				if (!DeepComparable.Matches(ModeElement, otherT.ModeElement)) return false;
				if (!DeepComparable.Matches(DocumentationElement, otherT.DocumentationElement)) return false;
				if (!DeepComparable.Matches(Profile, otherT.Profile)) return false;

				return true;
			}

			public override bool IsExactly(IDeepComparable other)
			{
				var otherT = other as ConformanceDocumentComponent;
				if (otherT == null) return false;

				if (!base.IsExactly(otherT)) return false;
				if (!DeepComparable.IsExactly(ModeElement, otherT.ModeElement)) return false;
				if (!DeepComparable.IsExactly(DocumentationElement, otherT.DocumentationElement)) return false;
				if (!DeepComparable.IsExactly(Profile, otherT.Profile)) return false;

				return true;
			}

		}


		[FhirType("ConformanceRestResourceComponent")]
		[DataContract]
		public partial class ConformanceRestResourceComponent : Hl7.Fhir.Model.Element
		{
			/// <summary>
			/// A resource type that is supported
			/// </summary>
			[FhirElement("type", InSummary = true, Order = 40)]
			[Cardinality(Min = 1, Max = 1)]
			[DataMember]
			public Hl7.Fhir.Model.Code TypeElement { get; set; }

			/// <summary>
			/// A resource type that is supported
			/// </summary>
			/// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
			[NotMapped]
			[IgnoreDataMember]
			public string Type
			{
				get { return TypeElement != null ? TypeElement.Value : null; }
				set
				{
					if (value == null)
						TypeElement = null;
					else
						TypeElement = new Hl7.Fhir.Model.Code(value);
				}
			}

			/// <summary>
			/// What structural features are supported
			/// </summary>
			[FhirElement("profile", InSummary = true, Order = 50)]
			[References("Profile")]
			[DataMember]
			public Hl7.Fhir.Model.ResourceReference Profile { get; set; }

			/// <summary>
			/// What operations are supported?
			/// </summary>
			[FhirElement("operation", InSummary = true, Order = 60)]
			[Cardinality(Min = 1, Max = -1)]
			[DataMember]
			public List<Hl7.Fhir.Model.Conformance.ConformanceRestResourceOperationComponent> Operation { get; set; }

			/// <summary>
			/// Whether vRead can return past versions
			/// </summary>
			[FhirElement("readHistory", InSummary = true, Order = 70)]
			[DataMember]
			public Hl7.Fhir.Model.FhirBoolean ReadHistoryElement { get; set; }

			/// <summary>
			/// Whether vRead can return past versions
			/// </summary>
			/// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
			[NotMapped]
			[IgnoreDataMember]
			public bool? ReadHistory
			{
				get { return ReadHistoryElement != null ? ReadHistoryElement.Value : null; }
				set
				{
					if (value == null)
						ReadHistoryElement = null;
					else
						ReadHistoryElement = new Hl7.Fhir.Model.FhirBoolean(value);
				}
			}

			/// <summary>
			/// If allows/uses update to a new location
			/// </summary>
			[FhirElement("updateCreate", InSummary = true, Order = 80)]
			[DataMember]
			public Hl7.Fhir.Model.FhirBoolean UpdateCreateElement { get; set; }

			/// <summary>
			/// If allows/uses update to a new location
			/// </summary>
			/// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
			[NotMapped]
			[IgnoreDataMember]
			public bool? UpdateCreate
			{
				get { return UpdateCreateElement != null ? UpdateCreateElement.Value : null; }
				set
				{
					if (value == null)
						UpdateCreateElement = null;
					else
						UpdateCreateElement = new Hl7.Fhir.Model.FhirBoolean(value);
				}
			}

			/// <summary>
			/// _include values supported by the server
			/// </summary>
			[FhirElement("searchInclude", InSummary = true, Order = 90)]
			[Cardinality(Min = 0, Max = -1)]
			[DataMember]
			public List<Hl7.Fhir.Model.FhirString> SearchIncludeElement { get; set; }

			/// <summary>
			/// _include values supported by the server
			/// </summary>
			/// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
			[NotMapped]
			[IgnoreDataMember]
			public IEnumerable<string> SearchInclude
			{
				get { return SearchIncludeElement != null ? SearchIncludeElement.Select(elem => elem.Value) : null; }
				set
				{
					if (value == null)
						SearchIncludeElement = null;
					else
						SearchIncludeElement = new List<Hl7.Fhir.Model.FhirString>(value.Select(elem => new Hl7.Fhir.Model.FhirString(elem)));
				}
			}

			/// <summary>
			/// Additional search params defined
			/// </summary>
			[FhirElement("searchParam", InSummary = true, Order = 100)]
			[Cardinality(Min = 0, Max = -1)]
			[DataMember]
			public List<Hl7.Fhir.Model.Conformance.ConformanceRestResourceSearchParamComponent> SearchParam { get; set; }

			public override IDeepCopyable CopyTo(IDeepCopyable other)
			{
				var dest = other as ConformanceRestResourceComponent;

				if (dest != null)
				{
					base.CopyTo(dest);
					if (TypeElement != null) dest.TypeElement = (Hl7.Fhir.Model.Code)TypeElement.DeepCopy();
					if (Profile != null) dest.Profile = (Hl7.Fhir.Model.ResourceReference)Profile.DeepCopy();
					if (Operation != null) dest.Operation = new List<Hl7.Fhir.Model.Conformance.ConformanceRestResourceOperationComponent>(Operation.DeepCopy());
					if (ReadHistoryElement != null) dest.ReadHistoryElement = (Hl7.Fhir.Model.FhirBoolean)ReadHistoryElement.DeepCopy();
					if (UpdateCreateElement != null) dest.UpdateCreateElement = (Hl7.Fhir.Model.FhirBoolean)UpdateCreateElement.DeepCopy();
					if (SearchIncludeElement != null) dest.SearchIncludeElement = new List<Hl7.Fhir.Model.FhirString>(SearchIncludeElement.DeepCopy());
					if (SearchParam != null) dest.SearchParam = new List<Hl7.Fhir.Model.Conformance.ConformanceRestResourceSearchParamComponent>(SearchParam.DeepCopy());
					return dest;
				}
				else
					throw Error.Argument(nameof(other), "Can only copy to an object of the same type");
			}

			public override IDeepCopyable DeepCopy()
			{
				return CopyTo(new ConformanceRestResourceComponent());
			}

			public override bool Matches(IDeepComparable other)
			{
				var otherT = other as ConformanceRestResourceComponent;
				if (otherT == null) return false;

				if (!base.Matches(otherT)) return false;
				if (!DeepComparable.Matches(TypeElement, otherT.TypeElement)) return false;
				if (!DeepComparable.Matches(Profile, otherT.Profile)) return false;
				if (!DeepComparable.Matches(Operation, otherT.Operation)) return false;
				if (!DeepComparable.Matches(ReadHistoryElement, otherT.ReadHistoryElement)) return false;
				if (!DeepComparable.Matches(UpdateCreateElement, otherT.UpdateCreateElement)) return false;
				if (!DeepComparable.Matches(SearchIncludeElement, otherT.SearchIncludeElement)) return false;
				if (!DeepComparable.Matches(SearchParam, otherT.SearchParam)) return false;

				return true;
			}

			public override bool IsExactly(IDeepComparable other)
			{
				var otherT = other as ConformanceRestResourceComponent;
				if (otherT == null) return false;

				if (!base.IsExactly(otherT)) return false;
				if (!DeepComparable.IsExactly(TypeElement, otherT.TypeElement)) return false;
				if (!DeepComparable.IsExactly(Profile, otherT.Profile)) return false;
				if (!DeepComparable.IsExactly(Operation, otherT.Operation)) return false;
				if (!DeepComparable.IsExactly(ReadHistoryElement, otherT.ReadHistoryElement)) return false;
				if (!DeepComparable.IsExactly(UpdateCreateElement, otherT.UpdateCreateElement)) return false;
				if (!DeepComparable.IsExactly(SearchIncludeElement, otherT.SearchIncludeElement)) return false;
				if (!DeepComparable.IsExactly(SearchParam, otherT.SearchParam)) return false;

				return true;
			}

		}


		[FhirType("ConformanceImplementationComponent")]
		[DataContract]
		public partial class ConformanceImplementationComponent : Hl7.Fhir.Model.Element
		{
			/// <summary>
			/// Describes this specific instance
			/// </summary>
			[FhirElement("description", InSummary = true, Order = 40)]
			[Cardinality(Min = 1, Max = 1)]
			[DataMember]
			public Hl7.Fhir.Model.FhirString DescriptionElement { get; set; }

			/// <summary>
			/// Describes this specific instance
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
			/// Base URL for the installation
			/// </summary>
			[FhirElement("url", InSummary = true, Order = 50)]
			[DataMember]
			public Hl7.Fhir.Model.FhirUri UrlElement { get; set; }

			/// <summary>
			/// Base URL for the installation
			/// </summary>
			/// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
			[NotMapped]
			[IgnoreDataMember]
			public string Url
			{
				get { return UrlElement != null ? UrlElement.Value : null; }
				set
				{
					if (value == null)
						UrlElement = null;
					else
						UrlElement = new Hl7.Fhir.Model.FhirUri(value);
				}
			}

			public override IDeepCopyable CopyTo(IDeepCopyable other)
			{
				var dest = other as ConformanceImplementationComponent;

				if (dest != null)
				{
					base.CopyTo(dest);
					if (DescriptionElement != null) dest.DescriptionElement = (Hl7.Fhir.Model.FhirString)DescriptionElement.DeepCopy();
					if (UrlElement != null) dest.UrlElement = (Hl7.Fhir.Model.FhirUri)UrlElement.DeepCopy();
					return dest;
				}
				else
					throw Error.Argument(nameof(other), "Can only copy to an object of the same type");
			}

			public override IDeepCopyable DeepCopy()
			{
				return CopyTo(new ConformanceImplementationComponent());
			}

			public override bool Matches(IDeepComparable other)
			{
				var otherT = other as ConformanceImplementationComponent;
				if (otherT == null) return false;

				if (!base.Matches(otherT)) return false;
				if (!DeepComparable.Matches(DescriptionElement, otherT.DescriptionElement)) return false;
				if (!DeepComparable.Matches(UrlElement, otherT.UrlElement)) return false;

				return true;
			}

			public override bool IsExactly(IDeepComparable other)
			{
				var otherT = other as ConformanceImplementationComponent;
				if (otherT == null) return false;

				if (!base.IsExactly(otherT)) return false;
				if (!DeepComparable.IsExactly(DescriptionElement, otherT.DescriptionElement)) return false;
				if (!DeepComparable.IsExactly(UrlElement, otherT.UrlElement)) return false;

				return true;
			}

		}


		[FhirType("ConformanceRestResourceOperationComponent")]
		[DataContract]
		public partial class ConformanceRestResourceOperationComponent : Hl7.Fhir.Model.Element
		{
			/// <summary>
			/// read | vread | update | delete | history-instance | validate | history-type | create | search-type
			/// </summary>
			[FhirElement("code", InSummary = true, Order = 40)]
			[Cardinality(Min = 1, Max = 1)]
			[DataMember]
			public Code<Hl7.Fhir.Model.Conformance.RestfulOperationType> CodeElement { get; set; }
			/// read | vread | update | delete | history-instance | validate | history-type | create | search-type
			/// </summary>
			/// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
			[NotMapped]
			[IgnoreDataMember]
			public Hl7.Fhir.Model.Conformance.RestfulOperationType? Code
			{
				get { return CodeElement != null ? CodeElement.Value : null; }
				set
				{
					if (value == null)
						CodeElement = null;
					else
						CodeElement = new Code<Hl7.Fhir.Model.Conformance.RestfulOperationType>(value);
				}
			}

			/// <summary>
			/// Anything special about operation behavior
			/// </summary>
			[FhirElement("documentation", InSummary = true, Order = 50)]
			[DataMember]
			public Hl7.Fhir.Model.FhirString DocumentationElement { get; set; }
			/// Anything special about operation behavior
			/// </summary>
			/// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
			[NotMapped]
			[IgnoreDataMember]
			public string Documentation
			{
				get { return DocumentationElement != null ? DocumentationElement.Value : null; }
				set
				{
					if (value == null)
						DocumentationElement = null;
					else
						DocumentationElement = new Hl7.Fhir.Model.FhirString(value);
				}
			}

			public override IDeepCopyable CopyTo(IDeepCopyable other)
			{
				var dest = other as ConformanceRestResourceOperationComponent;

				if (dest != null)
				{
					base.CopyTo(dest);
					if (CodeElement != null) dest.CodeElement = (Code<Hl7.Fhir.Model.Conformance.RestfulOperationType>)CodeElement.DeepCopy();
					if (DocumentationElement != null) dest.DocumentationElement = (Hl7.Fhir.Model.FhirString)DocumentationElement.DeepCopy();
					return dest;
				}
				else
					throw Error.Argument(nameof(other), "Can only copy to an object of the same type");
			}

			public override IDeepCopyable DeepCopy()
			{
				return CopyTo(new ConformanceRestResourceOperationComponent());
			}

			public override bool Matches(IDeepComparable other)
			{
				var otherT = other as ConformanceRestResourceOperationComponent;
				if (otherT == null) return false;

				if (!base.Matches(otherT)) return false;
				if (!DeepComparable.Matches(CodeElement, otherT.CodeElement)) return false;
				if (!DeepComparable.Matches(DocumentationElement, otherT.DocumentationElement)) return false;

				return true;
			}

			public override bool IsExactly(IDeepComparable other)
			{
				var otherT = other as ConformanceRestResourceOperationComponent;
				if (otherT == null) return false;

				if (!base.IsExactly(otherT)) return false;
				if (!DeepComparable.IsExactly(CodeElement, otherT.CodeElement)) return false;
				if (!DeepComparable.IsExactly(DocumentationElement, otherT.DocumentationElement)) return false;

				return true;
			}

		}


		[FhirType("ConformanceMessagingEventComponent")]
		[DataContract]
		public partial class ConformanceMessagingEventComponent : Hl7.Fhir.Model.Element
		{
			/// <summary>
			/// Event type
			/// </summary>
			[FhirElement("code", InSummary = true, Order = 40)]
			[Cardinality(Min = 1, Max = 1)]
			[DataMember]
			public Hl7.Fhir.Model.Coding Code { get; set; }

			/// <summary>
			/// Consequence | Currency | Notification
			/// </summary>
			[FhirElement("category", InSummary = true, Order = 50)]
			[DataMember]
			public Code<Hl7.Fhir.Model.Conformance.MessageSignificanceCategory> CategoryElement { get; set; }

			/// <summary>
			/// Consequence | Currency | Notification
			/// </summary>
			/// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
			[NotMapped]
			[IgnoreDataMember]
			public Hl7.Fhir.Model.Conformance.MessageSignificanceCategory? Category
			{
				get { return CategoryElement != null ? CategoryElement.Value : null; }
				set
				{
					if (value == null)
						CategoryElement = null;
					else
						CategoryElement = new Code<Hl7.Fhir.Model.Conformance.MessageSignificanceCategory>(value);
				}
			}

			/// <summary>
			/// sender | receiver
			/// </summary>
			[FhirElement("mode", InSummary = true, Order = 60)]
			[Cardinality(Min = 1, Max = 1)]
			[DataMember]
			public Code<Hl7.Fhir.Model.Conformance.ConformanceEventMode> ModeElement { get; set; }

			/// <summary>
			/// sender | receiver
			/// </summary>
			/// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
			[NotMapped]
			[IgnoreDataMember]
			public Hl7.Fhir.Model.Conformance.ConformanceEventMode? Mode
			{
				get { return ModeElement != null ? ModeElement.Value : null; }
				set
				{
					if (value == null)
						ModeElement = null;
					else
						ModeElement = new Code<Hl7.Fhir.Model.Conformance.ConformanceEventMode>(value);
				}
			}

			/// <summary>
			/// http | ftp | mllp +
			/// </summary>
			[FhirElement("protocol", InSummary = true, Order = 70)]
			[Cardinality(Min = 0, Max = -1)]
			[DataMember]
			public List<Hl7.Fhir.Model.Coding> Protocol { get; set; }

			/// <summary>
			/// Resource that's focus of message
			/// </summary>
			[FhirElement("focus", InSummary = true, Order = 80)]
			[Cardinality(Min = 1, Max = 1)]
			[DataMember]
			public Hl7.Fhir.Model.Code FocusElement { get; set; }

			/// <summary>
			/// Resource that's focus of message
			/// </summary>
			/// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
			[NotMapped]
			[IgnoreDataMember]
			public string Focus
			{
				get { return FocusElement != null ? FocusElement.Value : null; }
				set
				{
					if (value == null)
						FocusElement = null;
					else
						FocusElement = new Hl7.Fhir.Model.Code(value);
				}
			}

			/// <summary>
			/// Profile that describes the request
			/// </summary>
			[FhirElement("request", InSummary = true, Order = 90)]
			[References("Profile")]
			[Cardinality(Min = 1, Max = 1)]
			[DataMember]
			public Hl7.Fhir.Model.ResourceReference Request { get; set; }

			/// <summary>
			/// Profile that describes the response
			/// </summary>
			[FhirElement("response", InSummary = true, Order = 100)]
			[References("Profile")]
			[Cardinality(Min = 1, Max = 1)]
			[DataMember]
			public Hl7.Fhir.Model.ResourceReference Response { get; set; }

			/// <summary>
			/// Endpoint-specific event documentation
			/// </summary>
			[FhirElement("documentation", InSummary = true, Order = 110)]
			[DataMember]
			public Hl7.Fhir.Model.FhirString DocumentationElement { get; set; }

			/// <summary>
			/// Endpoint-specific event documentation
			/// </summary>
			/// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
			[NotMapped]
			[IgnoreDataMember]
			public string Documentation
			{
				get { return DocumentationElement != null ? DocumentationElement.Value : null; }
				set
				{
					if (value == null)
						DocumentationElement = null;
					else
						DocumentationElement = new Hl7.Fhir.Model.FhirString(value);
				}
			}

			public override IDeepCopyable CopyTo(IDeepCopyable other)
			{
				var dest = other as ConformanceMessagingEventComponent;

				if (dest != null)
				{
					base.CopyTo(dest);
					if (Code != null) dest.Code = (Hl7.Fhir.Model.Coding)Code.DeepCopy();
					if (CategoryElement != null) dest.CategoryElement = (Code<Hl7.Fhir.Model.Conformance.MessageSignificanceCategory>)CategoryElement.DeepCopy();
					if (ModeElement != null) dest.ModeElement = (Code<Hl7.Fhir.Model.Conformance.ConformanceEventMode>)ModeElement.DeepCopy();
					if (Protocol != null) dest.Protocol = new List<Hl7.Fhir.Model.Coding>(Protocol.DeepCopy());
					if (FocusElement != null) dest.FocusElement = (Hl7.Fhir.Model.Code)FocusElement.DeepCopy();
					if (Request != null) dest.Request = (Hl7.Fhir.Model.ResourceReference)Request.DeepCopy();
					if (Response != null) dest.Response = (Hl7.Fhir.Model.ResourceReference)Response.DeepCopy();
					if (DocumentationElement != null) dest.DocumentationElement = (Hl7.Fhir.Model.FhirString)DocumentationElement.DeepCopy();
					return dest;
				}
				else
					throw Error.Argument(nameof(other), "Can only copy to an object of the same type");
			}

			public override IDeepCopyable DeepCopy()
			{
				return CopyTo(new ConformanceMessagingEventComponent());
			}

			public override bool Matches(IDeepComparable other)
			{
				var otherT = other as ConformanceMessagingEventComponent;
				if (otherT == null) return false;

				if (!base.Matches(otherT)) return false;
				if (!DeepComparable.Matches(Code, otherT.Code)) return false;
				if (!DeepComparable.Matches(CategoryElement, otherT.CategoryElement)) return false;
				if (!DeepComparable.Matches(ModeElement, otherT.ModeElement)) return false;
				if (!DeepComparable.Matches(Protocol, otherT.Protocol)) return false;
				if (!DeepComparable.Matches(FocusElement, otherT.FocusElement)) return false;
				if (!DeepComparable.Matches(Request, otherT.Request)) return false;
				if (!DeepComparable.Matches(Response, otherT.Response)) return false;
				if (!DeepComparable.Matches(DocumentationElement, otherT.DocumentationElement)) return false;

				return true;
			}

			public override bool IsExactly(IDeepComparable other)
			{
				var otherT = other as ConformanceMessagingEventComponent;
				if (otherT == null) return false;

				if (!base.IsExactly(otherT)) return false;
				if (!DeepComparable.IsExactly(Code, otherT.Code)) return false;
				if (!DeepComparable.IsExactly(CategoryElement, otherT.CategoryElement)) return false;
				if (!DeepComparable.IsExactly(ModeElement, otherT.ModeElement)) return false;
				if (!DeepComparable.IsExactly(Protocol, otherT.Protocol)) return false;
				if (!DeepComparable.IsExactly(FocusElement, otherT.FocusElement)) return false;
				if (!DeepComparable.IsExactly(Request, otherT.Request)) return false;
				if (!DeepComparable.IsExactly(Response, otherT.Response)) return false;
				if (!DeepComparable.IsExactly(DocumentationElement, otherT.DocumentationElement)) return false;

				return true;
			}

		}


		[FhirType("ConformanceRestSecurityComponent")]
		[DataContract]
		public partial class ConformanceRestSecurityComponent : Hl7.Fhir.Model.Element
		{
			/// <summary>
			/// Adds CORS Headers (http://enable-cors.org/)
			/// </summary>
			[FhirElement("cors", InSummary = true, Order = 40)]
			[DataMember]
			public Hl7.Fhir.Model.FhirBoolean CorsElement { get; set; }

			/// <summary>
			/// Adds CORS Headers (http://enable-cors.org/)
			/// </summary>
			/// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
			[NotMapped]
			[IgnoreDataMember]
			public bool? Cors
			{
				get { return CorsElement != null ? CorsElement.Value : null; }
				set
				{
					if (value == null)
						CorsElement = null;
					else
						CorsElement = new Hl7.Fhir.Model.FhirBoolean(value);
				}
			}

			/// <summary>
			/// OAuth | OAuth2 | NTLM | Basic | Kerberos
			/// </summary>
			[FhirElement("service", InSummary = true, Order = 50)]
			[Cardinality(Min = 0, Max = -1)]
			[DataMember]
			public List<Hl7.Fhir.Model.CodeableConcept> Service { get; set; }

			/// <summary>
			/// General description of how security works
			/// </summary>
			[FhirElement("description", InSummary = true, Order = 60)]
			[DataMember]
			public Hl7.Fhir.Model.FhirString DescriptionElement { get; set; }

			/// <summary>
			/// General description of how security works
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
			/// Certificates associated with security profiles
			/// </summary>
			[FhirElement("certificate", InSummary = true, Order = 70)]
			[Cardinality(Min = 0, Max = -1)]
			[DataMember]
			public List<Hl7.Fhir.Model.Conformance.ConformanceRestSecurityCertificateComponent> Certificate { get; set; }

			public override IDeepCopyable CopyTo(IDeepCopyable other)
			{
				var dest = other as ConformanceRestSecurityComponent;

				if (dest != null)
				{
					base.CopyTo(dest);
					if (CorsElement != null) dest.CorsElement = (Hl7.Fhir.Model.FhirBoolean)CorsElement.DeepCopy();
					if (Service != null) dest.Service = new List<Hl7.Fhir.Model.CodeableConcept>(Service.DeepCopy());
					if (DescriptionElement != null) dest.DescriptionElement = (Hl7.Fhir.Model.FhirString)DescriptionElement.DeepCopy();
					if (Certificate != null) dest.Certificate = new List<Hl7.Fhir.Model.Conformance.ConformanceRestSecurityCertificateComponent>(Certificate.DeepCopy());
					return dest;
				}
				else
					throw Error.Argument(nameof(other), "Can only copy to an object of the same type");
			}

			public override IDeepCopyable DeepCopy()
			{
				return CopyTo(new ConformanceRestSecurityComponent());
			}

			public override bool Matches(IDeepComparable other)
			{
				var otherT = other as ConformanceRestSecurityComponent;
				if (otherT == null) return false;

				if (!base.Matches(otherT)) return false;
				if (!DeepComparable.Matches(CorsElement, otherT.CorsElement)) return false;
				if (!DeepComparable.Matches(Service, otherT.Service)) return false;
				if (!DeepComparable.Matches(DescriptionElement, otherT.DescriptionElement)) return false;
				if (!DeepComparable.Matches(Certificate, otherT.Certificate)) return false;

				return true;
			}

			public override bool IsExactly(IDeepComparable other)
			{
				var otherT = other as ConformanceRestSecurityComponent;
				if (otherT == null) return false;

				if (!base.IsExactly(otherT)) return false;
				if (!DeepComparable.IsExactly(CorsElement, otherT.CorsElement)) return false;
				if (!DeepComparable.IsExactly(Service, otherT.Service)) return false;
				if (!DeepComparable.IsExactly(DescriptionElement, otherT.DescriptionElement)) return false;
				if (!DeepComparable.IsExactly(Certificate, otherT.Certificate)) return false;

				return true;
			}

		}


		[FhirType("ConformanceRestSecurityCertificateComponent")]
		[DataContract]
		public partial class ConformanceRestSecurityCertificateComponent : Hl7.Fhir.Model.Element
		{
			/// <summary>
			/// Mime type for certificate
			/// </summary>
			[FhirElement("type", InSummary = true, Order = 40)]
			[DataMember]
			public Hl7.Fhir.Model.Code TypeElement { get; set; }

			/// <summary>
			/// Mime type for certificate
			/// </summary>
			/// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
			[NotMapped]
			[IgnoreDataMember]
			public string Type
			{
				get { return TypeElement != null ? TypeElement.Value : null; }
				set
				{
					if (value == null)
						TypeElement = null;
					else
						TypeElement = new Hl7.Fhir.Model.Code(value);
				}
			}

			/// <summary>
			/// Actual certificate
			/// </summary>
			[FhirElement("blob", InSummary = true, Order = 50)]
			[DataMember]
			public Hl7.Fhir.Model.Base64Binary BlobElement { get; set; }

			/// <summary>
			/// Actual certificate
			/// </summary>
			/// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
			[NotMapped]
			[IgnoreDataMember]
			public byte[] Blob
			{
				get { return BlobElement != null ? BlobElement.Value : null; }
				set
				{
					if (value == null)
						BlobElement = null;
					else
						BlobElement = new Hl7.Fhir.Model.Base64Binary(value);
				}
			}

			public override IDeepCopyable CopyTo(IDeepCopyable other)
			{
				var dest = other as ConformanceRestSecurityCertificateComponent;

				if (dest != null)
				{
					base.CopyTo(dest);
					if (TypeElement != null) dest.TypeElement = (Hl7.Fhir.Model.Code)TypeElement.DeepCopy();
					if (BlobElement != null) dest.BlobElement = (Hl7.Fhir.Model.Base64Binary)BlobElement.DeepCopy();
					return dest;
				}
				else
					throw Error.Argument(nameof(other), "Can only copy to an object of the same type");
			}

			public override IDeepCopyable DeepCopy()
			{
				return CopyTo(new ConformanceRestSecurityCertificateComponent());
			}

			public override bool Matches(IDeepComparable other)
			{
				var otherT = other as ConformanceRestSecurityCertificateComponent;
				if (otherT == null) return false;

				if (!base.Matches(otherT)) return false;
				if (!DeepComparable.Matches(TypeElement, otherT.TypeElement)) return false;
				if (!DeepComparable.Matches(BlobElement, otherT.BlobElement)) return false;

				return true;
			}

			public override bool IsExactly(IDeepComparable other)
			{
				var otherT = other as ConformanceRestSecurityCertificateComponent;
				if (otherT == null) return false;

				if (!base.IsExactly(otherT)) return false;
				if (!DeepComparable.IsExactly(TypeElement, otherT.TypeElement)) return false;
				if (!DeepComparable.IsExactly(BlobElement, otherT.BlobElement)) return false;

				return true;
			}

		}


		[FhirType("ConformanceRestOperationComponent")]
		[DataContract]
		public partial class ConformanceRestOperationComponent : Hl7.Fhir.Model.Element
		{
			/// <summary>
			/// transaction | search-system | history-system
			/// </summary>
			[FhirElement("code", InSummary = true, Order = 40)]
			[Cardinality(Min = 1, Max = 1)]
			[DataMember]
			public Code<Hl7.Fhir.Model.Conformance.RestfulOperationSystem> CodeElement { get; set; }

			/// <summary>
			/// transaction | search-system | history-system
			/// </summary>
			/// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
			[NotMapped]
			[IgnoreDataMember]
			public Hl7.Fhir.Model.Conformance.RestfulOperationSystem? Code
			{
				get { return CodeElement != null ? CodeElement.Value : null; }
				set
				{
					if (value == null)
						CodeElement = null;
					else
						CodeElement = new Code<Hl7.Fhir.Model.Conformance.RestfulOperationSystem>(value);
				}
			}

			/// <summary>
			/// Anything special about operation behavior
			/// </summary>
			[FhirElement("documentation", InSummary = true, Order = 50)]
			[DataMember]
			public Hl7.Fhir.Model.FhirString DocumentationElement { get; set; }

			/// <summary>
			/// Anything special about operation behavior
			/// </summary>
			/// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
			[NotMapped]
			[IgnoreDataMember]
			public string Documentation
			{
				get { return DocumentationElement != null ? DocumentationElement.Value : null; }
				set
				{
					if (value == null)
						DocumentationElement = null;
					else
						DocumentationElement = new Hl7.Fhir.Model.FhirString(value);
				}
			}

			public override IDeepCopyable CopyTo(IDeepCopyable other)
			{
				var dest = other as ConformanceRestOperationComponent;

				if (dest != null)
				{
					base.CopyTo(dest);
					if (CodeElement != null) dest.CodeElement = (Code<Hl7.Fhir.Model.Conformance.RestfulOperationSystem>)CodeElement.DeepCopy();
					if (DocumentationElement != null) dest.DocumentationElement = (Hl7.Fhir.Model.FhirString)DocumentationElement.DeepCopy();
					return dest;
				}
				else
					throw Error.Argument(nameof(other), "Can only copy to an object of the same type");
			}

			public override IDeepCopyable DeepCopy()
			{
				return CopyTo(new ConformanceRestOperationComponent());
			}

			public override bool Matches(IDeepComparable other)
			{
				var otherT = other as ConformanceRestOperationComponent;
				if (otherT == null) return false;

				if (!base.Matches(otherT)) return false;
				if (!DeepComparable.Matches(CodeElement, otherT.CodeElement)) return false;
				if (!DeepComparable.Matches(DocumentationElement, otherT.DocumentationElement)) return false;

				return true;
			}

			public override bool IsExactly(IDeepComparable other)
			{
				var otherT = other as ConformanceRestOperationComponent;
				if (otherT == null) return false;

				if (!base.IsExactly(otherT)) return false;
				if (!DeepComparable.IsExactly(CodeElement, otherT.CodeElement)) return false;
				if (!DeepComparable.IsExactly(DocumentationElement, otherT.DocumentationElement)) return false;

				return true;
			}

		}


		[FhirType("ConformanceRestResourceSearchParamComponent")]
		[DataContract]
		public partial class ConformanceRestResourceSearchParamComponent : Hl7.Fhir.Model.Element
		{
			/// <summary>
			/// Name of search parameter
			/// </summary>
			[FhirElement("name", InSummary = true, Order = 40)]
			[Cardinality(Min = 1, Max = 1)]
			[DataMember]
			public Hl7.Fhir.Model.FhirString NameElement { get; set; }

			/// <summary>
			/// Name of search parameter
			/// </summary>
			/// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
			[NotMapped]
			[IgnoreDataMember]
			public string Name
			{
				get { return NameElement != null ? NameElement.Value : null; }
				set
				{
					if (value == null)
						NameElement = null;
					else
						NameElement = new Hl7.Fhir.Model.FhirString(value);
				}
			}

			/// <summary>
			/// Source of definition for parameter
			/// </summary>
			[FhirElement("definition", InSummary = true, Order = 50)]
			[DataMember]
			public Hl7.Fhir.Model.FhirUri DefinitionElement { get; set; }

			/// <summary>
			/// Source of definition for parameter
			/// </summary>
			/// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
			[NotMapped]
			[IgnoreDataMember]
			public string Definition
			{
				get { return DefinitionElement != null ? DefinitionElement.Value : null; }
				set
				{
					if (value == null)
						DefinitionElement = null;
					else
						DefinitionElement = new Hl7.Fhir.Model.FhirUri(value);
				}
			}

			/// <summary>
			/// number | date | string | token | reference | composite | quantity
			/// </summary>
			[FhirElement("type", InSummary = true, Order = 60)]
			[Cardinality(Min = 1, Max = 1)]
			[DataMember]
			public Code<Hl7.Fhir.Model.Conformance.SearchParamType> TypeElement { get; set; }

			/// <summary>
			/// number | date | string | token | reference | composite | quantity
			/// </summary>
			/// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
			[NotMapped]
			[IgnoreDataMember]
			public Hl7.Fhir.Model.Conformance.SearchParamType? Type
			{
				get { return TypeElement != null ? TypeElement.Value : null; }
				set
				{
					if (value == null)
						TypeElement = null;
					else
						TypeElement = new Code<Hl7.Fhir.Model.Conformance.SearchParamType>(value);
				}
			}

			/// <summary>
			/// Server-specific usage
			/// </summary>
			[FhirElement("documentation", InSummary = true, Order = 70)]
			[DataMember]
			public Hl7.Fhir.Model.FhirString DocumentationElement { get; set; }

			/// <summary>
			/// Server-specific usage
			/// </summary>
			/// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
			[NotMapped]
			[IgnoreDataMember]
			public string Documentation
			{
				get { return DocumentationElement != null ? DocumentationElement.Value : null; }
				set
				{
					if (value == null)
						DocumentationElement = null;
					else
						DocumentationElement = new Hl7.Fhir.Model.FhirString(value);
				}
			}

			/// <summary>
			/// Types of resource (if a resource reference)
			/// </summary>
			[FhirElement("target", InSummary = true, Order = 80)]
			[Cardinality(Min = 0, Max = -1)]
			[DataMember]
			public List<Hl7.Fhir.Model.Code> TargetElement { get; set; }

			/// <summary>
			/// Types of resource (if a resource reference)
			/// </summary>
			/// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
			[NotMapped]
			[IgnoreDataMember]
			public IEnumerable<string> Target
			{
				get { return TargetElement != null ? TargetElement.Select(elem => elem.Value) : null; }
				set
				{
					if (value == null)
						TargetElement = null;
					else
						TargetElement = new List<Hl7.Fhir.Model.Code>(value.Select(elem => new Hl7.Fhir.Model.Code(elem)));
				}
			}

			/// <summary>
			/// Chained names supported
			/// </summary>
			[FhirElement("chain", InSummary = true, Order = 90)]
			[Cardinality(Min = 0, Max = -1)]
			[DataMember]
			public List<Hl7.Fhir.Model.FhirString> ChainElement { get; set; }

			/// <summary>
			/// Chained names supported
			/// </summary>
			/// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
			[NotMapped]
			[IgnoreDataMember]
			public IEnumerable<string> Chain
			{
				get { return ChainElement != null ? ChainElement.Select(elem => elem.Value) : null; }
				set
				{
					if (value == null)
						ChainElement = null;
					else
						ChainElement = new List<Hl7.Fhir.Model.FhirString>(value.Select(elem => new Hl7.Fhir.Model.FhirString(elem)));
				}
			}

			public override IDeepCopyable CopyTo(IDeepCopyable other)
			{
				var dest = other as ConformanceRestResourceSearchParamComponent;

				if (dest != null)
				{
					base.CopyTo(dest);
					if (NameElement != null) dest.NameElement = (Hl7.Fhir.Model.FhirString)NameElement.DeepCopy();
					if (DefinitionElement != null) dest.DefinitionElement = (Hl7.Fhir.Model.FhirUri)DefinitionElement.DeepCopy();
					if (TypeElement != null) dest.TypeElement = (Code<Hl7.Fhir.Model.Conformance.SearchParamType>)TypeElement.DeepCopy();
					if (DocumentationElement != null) dest.DocumentationElement = (Hl7.Fhir.Model.FhirString)DocumentationElement.DeepCopy();
					if (TargetElement != null) dest.TargetElement = new List<Hl7.Fhir.Model.Code>(TargetElement.DeepCopy());
					if (ChainElement != null) dest.ChainElement = new List<Hl7.Fhir.Model.FhirString>(ChainElement.DeepCopy());
					return dest;
				}
				else
					throw Error.Argument(nameof(other), "Can only copy to an object of the same type");
			}

			public override IDeepCopyable DeepCopy()
			{
				return CopyTo(new ConformanceRestResourceSearchParamComponent());
			}

			public override bool Matches(IDeepComparable other)
			{
				var otherT = other as ConformanceRestResourceSearchParamComponent;
				if (otherT == null) return false;

				if (!base.Matches(otherT)) return false;
				if (!DeepComparable.Matches(NameElement, otherT.NameElement)) return false;
				if (!DeepComparable.Matches(DefinitionElement, otherT.DefinitionElement)) return false;
				if (!DeepComparable.Matches(TypeElement, otherT.TypeElement)) return false;
				if (!DeepComparable.Matches(DocumentationElement, otherT.DocumentationElement)) return false;
				if (!DeepComparable.Matches(TargetElement, otherT.TargetElement)) return false;
				if (!DeepComparable.Matches(ChainElement, otherT.ChainElement)) return false;

				return true;
			}

			public override bool IsExactly(IDeepComparable other)
			{
				var otherT = other as ConformanceRestResourceSearchParamComponent;
				if (otherT == null) return false;

				if (!base.IsExactly(otherT)) return false;
				if (!DeepComparable.IsExactly(NameElement, otherT.NameElement)) return false;
				if (!DeepComparable.IsExactly(DefinitionElement, otherT.DefinitionElement)) return false;
				if (!DeepComparable.IsExactly(TypeElement, otherT.TypeElement)) return false;
				if (!DeepComparable.IsExactly(DocumentationElement, otherT.DocumentationElement)) return false;
				if (!DeepComparable.IsExactly(TargetElement, otherT.TargetElement)) return false;
				if (!DeepComparable.IsExactly(ChainElement, otherT.ChainElement)) return false;

				return true;
			}

		}


		/// <summary>
		/// Logical id to reference this statement
		/// </summary>
		[FhirElement("identifier", InSummary = true, Order = 70)]
		[DataMember]
		public Hl7.Fhir.Model.FhirString IdentifierElement { get; set; }

		/// <summary>
		/// Logical id to reference this statement
		/// </summary>
		/// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
		[NotMapped]
		[IgnoreDataMember]
		public string Identifier
		{
			get { return IdentifierElement != null ? IdentifierElement.Value : null; }
			set
			{
				if (value == null)
					IdentifierElement = null;
				else
					IdentifierElement = new Hl7.Fhir.Model.FhirString(value);
			}
		}

		/// <summary>
		/// Logical id for this version of the statement
		/// </summary>
		[FhirElement("version", InSummary = true, Order = 80)]
		[DataMember]
		public Hl7.Fhir.Model.FhirString VersionElement { get; set; }

		/// <summary>
		/// Logical id for this version of the statement
		/// </summary>
		/// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
		[NotMapped]
		[IgnoreDataMember]
		public string Version
		{
			get { return VersionElement != null ? VersionElement.Value : null; }
			set
			{
				if (value == null)
					VersionElement = null;
				else
					VersionElement = new Hl7.Fhir.Model.FhirString(value);
			}
		}

		/// <summary>
		/// Informal name for this conformance statement
		/// </summary>
		[FhirElement("name", InSummary = true, Order = 90)]
		[DataMember]
		public Hl7.Fhir.Model.FhirString NameElement { get; set; }

		/// <summary>
		/// Informal name for this conformance statement
		/// </summary>
		/// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
		[NotMapped]
		[IgnoreDataMember]
		public string Name
		{
			get { return NameElement != null ? NameElement.Value : null; }
			set
			{
				if (value == null)
					NameElement = null;
				else
					NameElement = new Hl7.Fhir.Model.FhirString(value);
			}
		}

		/// <summary>
		/// Publishing Organization
		/// </summary>
		[FhirElement("publisher", InSummary = true, Order = 100)]
		[Cardinality(Min = 1, Max = 1)]
		[DataMember]
		public Hl7.Fhir.Model.FhirString PublisherElement { get; set; }

		/// <summary>
		/// Publishing Organization
		/// </summary>
		/// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
		[NotMapped]
		[IgnoreDataMember]
		public string Publisher
		{
			get { return PublisherElement != null ? PublisherElement.Value : null; }
			set
			{
				if (value == null)
					PublisherElement = null;
				else
					PublisherElement = new Hl7.Fhir.Model.FhirString(value);
			}
		}

		/// <summary>
		/// Contacts for Organization
		/// </summary>
		[FhirElement("telecom", InSummary = true, Order = 110)]
		[Cardinality(Min = 0, Max = -1)]
		[DataMember]
		public List<Hl7.Fhir.Model.Contact> Telecom { get; set; }

		/// <summary>
		/// Human description of the conformance statement
		/// </summary>
		[FhirElement("description", InSummary = true, Order = 120)]
		[DataMember]
		public Hl7.Fhir.Model.FhirString DescriptionElement { get; set; }

		/// <summary>
		/// Human description of the conformance statement
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
		/// draft | active | retired
		/// </summary>
		[FhirElement("status", InSummary = true, Order = 130)]
		[DataMember]
		public Code<Hl7.Fhir.Model.Conformance.ConformanceStatementStatus> StatusElement { get; set; }

		/// <summary>
		/// draft | active | retired
		/// </summary>
		/// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
		[NotMapped]
		[IgnoreDataMember]
		public Hl7.Fhir.Model.Conformance.ConformanceStatementStatus? Status
		{
			get { return StatusElement != null ? StatusElement.Value : null; }
			set
			{
				if (value == null)
					StatusElement = null;
				else
					StatusElement = new Code<Hl7.Fhir.Model.Conformance.ConformanceStatementStatus>(value);
			}
		}

		/// <summary>
		/// If for testing purposes, not real usage
		/// </summary>
		[FhirElement("experimental", InSummary = true, Order = 140)]
		[DataMember]
		public Hl7.Fhir.Model.FhirBoolean ExperimentalElement { get; set; }

		/// <summary>
		/// If for testing purposes, not real usage
		/// </summary>
		/// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
		[NotMapped]
		[IgnoreDataMember]
		public bool? Experimental
		{
			get { return ExperimentalElement != null ? ExperimentalElement.Value : null; }
			set
			{
				if (value == null)
					ExperimentalElement = null;
				else
					ExperimentalElement = new Hl7.Fhir.Model.FhirBoolean(value);
			}
		}

		/// <summary>
		/// Publication Date
		/// </summary>
		[FhirElement("date", InSummary = true, Order = 150)]
		[Cardinality(Min = 1, Max = 1)]
		[DataMember]
		public Hl7.Fhir.Model.FhirDateTime DateElement { get; set; }

		/// <summary>
		/// Publication Date
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
		/// Software that is covered by this conformance statement
		/// </summary>
		[FhirElement("software", InSummary = true, Order = 160)]
		[DataMember]
		public Hl7.Fhir.Model.Conformance.ConformanceSoftwareComponent Software { get; set; }

		/// <summary>
		/// If this describes a specific instance
		/// </summary>
		[FhirElement("implementation", InSummary = true, Order = 170)]
		[DataMember]
		public Hl7.Fhir.Model.Conformance.ConformanceImplementationComponent Implementation { get; set; }

		/// <summary>
		/// FHIR Version
		/// </summary>
		[FhirElement("fhirVersion", InSummary = true, Order = 180)]
		[Cardinality(Min = 1, Max = 1)]
		[DataMember]
		public Hl7.Fhir.Model.Id FhirVersionElement { get; set; }

		/// <summary>
		/// FHIR Version
		/// </summary>
		/// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
		[NotMapped]
		[IgnoreDataMember]
		public string FhirVersion
		{
			get { return FhirVersionElement != null ? FhirVersionElement.Value : null; }
			set
			{
				if (value == null)
					FhirVersionElement = null;
				else
					FhirVersionElement = new Hl7.Fhir.Model.Id(value);
			}
		}

		/// <summary>
		/// True if application accepts unknown elements
		/// </summary>
		[FhirElement("acceptUnknown", InSummary = true, Order = 190)]
		[Cardinality(Min = 1, Max = 1)]
		[DataMember]
		public Hl7.Fhir.Model.FhirBoolean AcceptUnknownElement { get; set; }

		/// <summary>
		/// True if application accepts unknown elements
		/// </summary>
		/// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
		[NotMapped]
		[IgnoreDataMember]
		public bool? AcceptUnknown
		{
			get { return AcceptUnknownElement != null ? AcceptUnknownElement.Value : null; }
			set
			{
				if (value == null)
					AcceptUnknownElement = null;
				else
					AcceptUnknownElement = new Hl7.Fhir.Model.FhirBoolean(value);
			}
		}

		/// <summary>
		/// formats supported (xml | json | mime type)
		/// </summary>
		[FhirElement("format", Order = 200)]
		[Cardinality(Min = 1, Max = -1)]
		[DataMember]
		public List<Hl7.Fhir.Model.Code> FormatElement { get; set; }

		/// <summary>
		/// formats supported (xml | json | mime type)
		/// </summary>
		/// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
		[NotMapped]
		[IgnoreDataMember]
		public IEnumerable<string> Format
		{
			get { return FormatElement != null ? FormatElement.Select(elem => elem.Value) : null; }
			set
			{
				if (value == null)
					FormatElement = null;
				else
					FormatElement = new List<Hl7.Fhir.Model.Code>(value.Select(elem => new Hl7.Fhir.Model.Code(elem)));
			}
		}

		/// <summary>
		/// Profiles supported by the system
		/// </summary>
		[FhirElement("profile", Order = 210)]
		[References("Profile")]
		[Cardinality(Min = 0, Max = -1)]
		[DataMember]
		public List<Hl7.Fhir.Model.ResourceReference> Profile { get; set; }

		/// <summary>
		/// If the endpoint is a RESTful one
		/// </summary>
		[FhirElement("rest", Order = 220)]
		[Cardinality(Min = 0, Max = -1)]
		[DataMember]
		public List<Hl7.Fhir.Model.Conformance.ConformanceRestComponent> Rest { get; set; }

		/// <summary>
		/// If messaging is supported
		/// </summary>
		[FhirElement("messaging", Order = 230)]
		[Cardinality(Min = 0, Max = -1)]
		[DataMember]
		public List<Hl7.Fhir.Model.Conformance.ConformanceMessagingComponent> Messaging { get; set; }

		/// <summary>
		/// Document definition
		/// </summary>
		[FhirElement("document", Order = 240)]
		[Cardinality(Min = 0, Max = -1)]
		[DataMember]
		public List<Hl7.Fhir.Model.Conformance.ConformanceDocumentComponent> Document { get; set; }

		public override IDeepCopyable CopyTo(IDeepCopyable other)
		{
			var dest = other as Conformance;

			if (dest != null)
			{
				base.CopyTo(dest);
				if (IdentifierElement != null) dest.IdentifierElement = (Hl7.Fhir.Model.FhirString)IdentifierElement.DeepCopy();
				if (VersionElement != null) dest.VersionElement = (Hl7.Fhir.Model.FhirString)VersionElement.DeepCopy();
				if (NameElement != null) dest.NameElement = (Hl7.Fhir.Model.FhirString)NameElement.DeepCopy();
				if (PublisherElement != null) dest.PublisherElement = (Hl7.Fhir.Model.FhirString)PublisherElement.DeepCopy();
				if (Telecom != null) dest.Telecom = new List<Hl7.Fhir.Model.Contact>(Telecom.DeepCopy());
				if (DescriptionElement != null) dest.DescriptionElement = (Hl7.Fhir.Model.FhirString)DescriptionElement.DeepCopy();
				if (StatusElement != null) dest.StatusElement = (Code<Hl7.Fhir.Model.Conformance.ConformanceStatementStatus>)StatusElement.DeepCopy();
				if (ExperimentalElement != null) dest.ExperimentalElement = (Hl7.Fhir.Model.FhirBoolean)ExperimentalElement.DeepCopy();
				if (DateElement != null) dest.DateElement = (Hl7.Fhir.Model.FhirDateTime)DateElement.DeepCopy();
				if (Software != null) dest.Software = (Hl7.Fhir.Model.Conformance.ConformanceSoftwareComponent)Software.DeepCopy();
				if (Implementation != null) dest.Implementation = (Hl7.Fhir.Model.Conformance.ConformanceImplementationComponent)Implementation.DeepCopy();
				if (FhirVersionElement != null) dest.FhirVersionElement = (Hl7.Fhir.Model.Id)FhirVersionElement.DeepCopy();
				if (AcceptUnknownElement != null) dest.AcceptUnknownElement = (Hl7.Fhir.Model.FhirBoolean)AcceptUnknownElement.DeepCopy();
				if (FormatElement != null) dest.FormatElement = new List<Hl7.Fhir.Model.Code>(FormatElement.DeepCopy());
				if (Profile != null) dest.Profile = new List<Hl7.Fhir.Model.ResourceReference>(Profile.DeepCopy());
				if (Rest != null) dest.Rest = new List<Hl7.Fhir.Model.Conformance.ConformanceRestComponent>(Rest.DeepCopy());
				if (Messaging != null) dest.Messaging = new List<Hl7.Fhir.Model.Conformance.ConformanceMessagingComponent>(Messaging.DeepCopy());
				if (Document != null) dest.Document = new List<Hl7.Fhir.Model.Conformance.ConformanceDocumentComponent>(Document.DeepCopy());
				return dest;
			}
			else
				throw Error.Argument(nameof(other), "Can only copy to an object of the same type");
		}

		public override IDeepCopyable DeepCopy()
		{
			return CopyTo(new Conformance());
		}

		public override bool Matches(IDeepComparable other)
		{
			var otherT = other as Conformance;
			if (otherT == null) return false;

			if (!base.Matches(otherT)) return false;
			if (!DeepComparable.Matches(IdentifierElement, otherT.IdentifierElement)) return false;
			if (!DeepComparable.Matches(VersionElement, otherT.VersionElement)) return false;
			if (!DeepComparable.Matches(NameElement, otherT.NameElement)) return false;
			if (!DeepComparable.Matches(PublisherElement, otherT.PublisherElement)) return false;
			if (!DeepComparable.Matches(Telecom, otherT.Telecom)) return false;
			if (!DeepComparable.Matches(DescriptionElement, otherT.DescriptionElement)) return false;
			if (!DeepComparable.Matches(StatusElement, otherT.StatusElement)) return false;
			if (!DeepComparable.Matches(ExperimentalElement, otherT.ExperimentalElement)) return false;
			if (!DeepComparable.Matches(DateElement, otherT.DateElement)) return false;
			if (!DeepComparable.Matches(Software, otherT.Software)) return false;
			if (!DeepComparable.Matches(Implementation, otherT.Implementation)) return false;
			if (!DeepComparable.Matches(FhirVersionElement, otherT.FhirVersionElement)) return false;
			if (!DeepComparable.Matches(AcceptUnknownElement, otherT.AcceptUnknownElement)) return false;
			if (!DeepComparable.Matches(FormatElement, otherT.FormatElement)) return false;
			if (!DeepComparable.Matches(Profile, otherT.Profile)) return false;
			if (!DeepComparable.Matches(Rest, otherT.Rest)) return false;
			if (!DeepComparable.Matches(Messaging, otherT.Messaging)) return false;
			if (!DeepComparable.Matches(Document, otherT.Document)) return false;

			return true;
		}

		public override bool IsExactly(IDeepComparable other)
		{
			var otherT = other as Conformance;
			if (otherT == null) return false;

			if (!base.IsExactly(otherT)) return false;
			if (!DeepComparable.IsExactly(IdentifierElement, otherT.IdentifierElement)) return false;
			if (!DeepComparable.IsExactly(VersionElement, otherT.VersionElement)) return false;
			if (!DeepComparable.IsExactly(NameElement, otherT.NameElement)) return false;
			if (!DeepComparable.IsExactly(PublisherElement, otherT.PublisherElement)) return false;
			if (!DeepComparable.IsExactly(Telecom, otherT.Telecom)) return false;
			if (!DeepComparable.IsExactly(DescriptionElement, otherT.DescriptionElement)) return false;
			if (!DeepComparable.IsExactly(StatusElement, otherT.StatusElement)) return false;
			if (!DeepComparable.IsExactly(ExperimentalElement, otherT.ExperimentalElement)) return false;
			if (!DeepComparable.IsExactly(DateElement, otherT.DateElement)) return false;
			if (!DeepComparable.IsExactly(Software, otherT.Software)) return false;
			if (!DeepComparable.IsExactly(Implementation, otherT.Implementation)) return false;
			if (!DeepComparable.IsExactly(FhirVersionElement, otherT.FhirVersionElement)) return false;
			if (!DeepComparable.IsExactly(AcceptUnknownElement, otherT.AcceptUnknownElement)) return false;
			if (!DeepComparable.IsExactly(FormatElement, otherT.FormatElement)) return false;
			if (!DeepComparable.IsExactly(Profile, otherT.Profile)) return false;
			if (!DeepComparable.IsExactly(Rest, otherT.Rest)) return false;
			if (!DeepComparable.IsExactly(Messaging, otherT.Messaging)) return false;
			if (!DeepComparable.IsExactly(Document, otherT.Document)) return false;

			return true;
		}

	}

}
