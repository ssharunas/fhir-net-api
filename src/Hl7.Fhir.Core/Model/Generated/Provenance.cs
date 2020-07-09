using Hl7.Fhir.Introspection;
using Hl7.Fhir.Support;
using Hl7.Fhir.Validation;
using System;
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
	/// Who, What, When for a set of resources
	/// </summary>
	[FhirType("Provenance", IsResource = true)]
	[DataContract]
	public partial class Provenance : Hl7.Fhir.Model.Resource
	{
		/// <summary>
		/// How an entity was used in an activity
		/// </summary>
		[FhirEnumeration("ProvenanceEntityRole")]
		public enum ProvenanceEntityRole
		{
			/// <summary>
			/// A transformation of an entity into another, an update of an entity resulting in a new one, or the construction of a new entity based on a preexisting entity.
			/// </summary>
			[EnumLiteral("derivation")]
			Derivation,
			/// <summary>
			/// A derivation for which the resulting entity is a revised version of some original.
			/// </summary>
			[EnumLiteral("revision")]
			Revision,
			/// <summary>
			/// The repeat of (some or all of) an entity, such as text or image, by someone who may or may not be its original author.
			/// </summary>
			[EnumLiteral("quotation")]
			Quotation,
			/// <summary>
			/// A primary source for a topic refers to something produced by some agent with direct experience and knowledge about the topic, at the time of the topic's study, without benefit from hindsight.
			/// </summary>
			[EnumLiteral("source")]
			Source,
		}

		[FhirType("ProvenanceAgentComponent")]
		[DataContract]
		public partial class ProvenanceAgentComponent : Hl7.Fhir.Model.Element
		{
			/// <summary>
			/// e.g. author | overseer | enterer | attester | source | cc: +
			/// </summary>
			[FhirElement("role", InSummary = true, Order = 40)]
			[Cardinality(Min = 1, Max = 1)]
			[DataMember]
			public Hl7.Fhir.Model.Coding Role { get; set; }

			/// <summary>
			/// e.g. Resource | Person | Application | Record | Document +
			/// </summary>
			[FhirElement("type", InSummary = true, Order = 50)]
			[Cardinality(Min = 1, Max = 1)]
			[DataMember]
			public Hl7.Fhir.Model.Coding Type { get; set; }

			/// <summary>
			/// Identity of agent (urn or url)
			/// </summary>
			[FhirElement("reference", InSummary = true, Order = 60)]
			[Cardinality(Min = 1, Max = 1)]
			[DataMember]
			public Hl7.Fhir.Model.FhirUri ReferenceElement { get; set; }

			/// <summary>
			/// Identity of agent (urn or url)
			/// </summary>
			/// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
			[NotMapped]
			[IgnoreDataMember]
			public string Reference
			{
				get { return ReferenceElement?.Value; }
				set
				{
					if (value is null)
						ReferenceElement = null;
					else
						ReferenceElement = new Hl7.Fhir.Model.FhirUri(value);
				}
			}

			/// <summary>
			/// Human description of participant
			/// </summary>
			[FhirElement("display", InSummary = true, Order = 70)]
			[DataMember]
			public Hl7.Fhir.Model.FhirString DisplayElement { get; set; }

			/// <summary>
			/// Human description of participant
			/// </summary>
			/// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
			[NotMapped]
			[IgnoreDataMember]
			public string Display
			{
				get { return DisplayElement?.Value; }
				set
				{
					if (value is null)
						DisplayElement = null;
					else
						DisplayElement = new Hl7.Fhir.Model.FhirString(value);
				}
			}

			public override IDeepCopyable CopyTo(IDeepCopyable other)
			{
				var dest = other as ProvenanceAgentComponent;

				if (dest != null)
				{
					base.CopyTo(dest);
					if (Role != null) dest.Role = (Hl7.Fhir.Model.Coding)Role.DeepCopy();
					if (Type != null) dest.Type = (Hl7.Fhir.Model.Coding)Type.DeepCopy();
					if (ReferenceElement != null) dest.ReferenceElement = (Hl7.Fhir.Model.FhirUri)ReferenceElement.DeepCopy();
					if (DisplayElement != null) dest.DisplayElement = (Hl7.Fhir.Model.FhirString)DisplayElement.DeepCopy();
					return dest;
				}
				else
					throw Error.Argument(nameof(other), "Can only copy to an object of the same type");
			}

			public override IDeepCopyable DeepCopy()
			{
				return CopyTo(new ProvenanceAgentComponent());
			}

			public override bool Matches(IDeepComparable other)
			{
				var otherT = other as ProvenanceAgentComponent;
				if (otherT is null) return false;

				if (!base.Matches(otherT)) return false;
				if (!DeepComparable.Matches(Role, otherT.Role)) return false;
				if (!DeepComparable.Matches(Type, otherT.Type)) return false;
				if (!DeepComparable.Matches(ReferenceElement, otherT.ReferenceElement)) return false;
				if (!DeepComparable.Matches(DisplayElement, otherT.DisplayElement)) return false;

				return true;
			}

			public override bool IsExactly(IDeepComparable other)
			{
				var otherT = other as ProvenanceAgentComponent;
				if (otherT is null) return false;

				if (!base.IsExactly(otherT)) return false;
				if (!DeepComparable.IsExactly(Role, otherT.Role)) return false;
				if (!DeepComparable.IsExactly(Type, otherT.Type)) return false;
				if (!DeepComparable.IsExactly(ReferenceElement, otherT.ReferenceElement)) return false;
				if (!DeepComparable.IsExactly(DisplayElement, otherT.DisplayElement)) return false;

				return true;
			}

		}


		[FhirType("ProvenanceEntityComponent")]
		[DataContract]
		public partial class ProvenanceEntityComponent : Hl7.Fhir.Model.Element
		{
			/// <summary>
			/// derivation | revision | quotation | source
			/// </summary>
			[FhirElement("role", InSummary = true, Order = 40)]
			[Cardinality(Min = 1, Max = 1)]
			[DataMember]
			public Code<Hl7.Fhir.Model.Provenance.ProvenanceEntityRole> RoleElement { get; set; }

			/// <summary>
			/// derivation | revision | quotation | source
			/// </summary>
			/// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
			[NotMapped]
			[IgnoreDataMember]
			public Hl7.Fhir.Model.Provenance.ProvenanceEntityRole? Role
			{
				get { return RoleElement?.Value; }
				set
				{
					if (value is null)
						RoleElement = null;
					else
						RoleElement = new Code<Hl7.Fhir.Model.Provenance.ProvenanceEntityRole>(value);
				}
			}

			/// <summary>
			/// Resource Type, or something else
			/// </summary>
			[FhirElement("type", InSummary = true, Order = 50)]
			[Cardinality(Min = 1, Max = 1)]
			[DataMember]
			public Hl7.Fhir.Model.Coding Type { get; set; }

			/// <summary>
			/// Identity of participant (urn or url)
			/// </summary>
			[FhirElement("reference", InSummary = true, Order = 60)]
			[Cardinality(Min = 1, Max = 1)]
			[DataMember]
			public Hl7.Fhir.Model.FhirUri ReferenceElement { get; set; }

			/// <summary>
			/// Identity of participant (urn or url)
			/// </summary>
			/// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
			[NotMapped]
			[IgnoreDataMember]
			public string Reference
			{
				get { return ReferenceElement?.Value; }
				set
				{
					if (value is null)
						ReferenceElement = null;
					else
						ReferenceElement = new Hl7.Fhir.Model.FhirUri(value);
				}
			}

			/// <summary>
			/// Human description of participant
			/// </summary>
			[FhirElement("display", InSummary = true, Order = 70)]
			[DataMember]
			public Hl7.Fhir.Model.FhirString DisplayElement { get; set; }

			/// <summary>
			/// Human description of participant
			/// </summary>
			/// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
			[NotMapped]
			[IgnoreDataMember]
			public string Display
			{
				get { return DisplayElement?.Value; }
				set
				{
					if (value is null)
						DisplayElement = null;
					else
						DisplayElement = new Hl7.Fhir.Model.FhirString(value);
				}
			}

			/// <summary>
			/// Entity is attributed to this agent
			/// </summary>
			[FhirElement("agent", InSummary = true, Order = 80)]
			[DataMember]
			public Hl7.Fhir.Model.Provenance.ProvenanceAgentComponent Agent { get; set; }

			public override IDeepCopyable CopyTo(IDeepCopyable other)
			{
				var dest = other as ProvenanceEntityComponent;

				if (dest != null)
				{
					base.CopyTo(dest);
					if (RoleElement != null) dest.RoleElement = (Code<Hl7.Fhir.Model.Provenance.ProvenanceEntityRole>)RoleElement.DeepCopy();
					if (Type != null) dest.Type = (Hl7.Fhir.Model.Coding)Type.DeepCopy();
					if (ReferenceElement != null) dest.ReferenceElement = (Hl7.Fhir.Model.FhirUri)ReferenceElement.DeepCopy();
					if (DisplayElement != null) dest.DisplayElement = (Hl7.Fhir.Model.FhirString)DisplayElement.DeepCopy();
					if (Agent != null) dest.Agent = (Hl7.Fhir.Model.Provenance.ProvenanceAgentComponent)Agent.DeepCopy();
					return dest;
				}
				else
					throw Error.Argument(nameof(other), "Can only copy to an object of the same type");
			}

			public override IDeepCopyable DeepCopy()
			{
				return CopyTo(new ProvenanceEntityComponent());
			}

			public override bool Matches(IDeepComparable other)
			{
				var otherT = other as ProvenanceEntityComponent;
				if (otherT is null) return false;

				if (!base.Matches(otherT)) return false;
				if (!DeepComparable.Matches(RoleElement, otherT.RoleElement)) return false;
				if (!DeepComparable.Matches(Type, otherT.Type)) return false;
				if (!DeepComparable.Matches(ReferenceElement, otherT.ReferenceElement)) return false;
				if (!DeepComparable.Matches(DisplayElement, otherT.DisplayElement)) return false;
				if (!DeepComparable.Matches(Agent, otherT.Agent)) return false;

				return true;
			}

			public override bool IsExactly(IDeepComparable other)
			{
				var otherT = other as ProvenanceEntityComponent;
				if (otherT is null) return false;

				if (!base.IsExactly(otherT)) return false;
				if (!DeepComparable.IsExactly(RoleElement, otherT.RoleElement)) return false;
				if (!DeepComparable.IsExactly(Type, otherT.Type)) return false;
				if (!DeepComparable.IsExactly(ReferenceElement, otherT.ReferenceElement)) return false;
				if (!DeepComparable.IsExactly(DisplayElement, otherT.DisplayElement)) return false;
				if (!DeepComparable.IsExactly(Agent, otherT.Agent)) return false;

				return true;
			}

		}


		/// <summary>
		/// Target resource(s) (usually version specific)
		/// </summary>
		[FhirElement("target", Order = 70)]
		[References()]
		[Cardinality(Min = 1, Max = -1)]
		[DataMember]
		public List<Hl7.Fhir.Model.ResourceReference> Target { get; set; }

		/// <summary>
		/// When the activity occurred
		/// </summary>
		[FhirElement("period", Order = 80)]
		[DataMember]
		public Hl7.Fhir.Model.Period Period { get; set; }

		/// <summary>
		/// When the activity was recorded / updated
		/// </summary>
		[FhirElement("recorded", Order = 90)]
		[Cardinality(Min = 1, Max = 1)]
		[DataMember]
		public Hl7.Fhir.Model.Instant RecordedElement { get; set; }

		/// <summary>
		/// When the activity was recorded / updated
		/// </summary>
		/// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
		[NotMapped]
		[IgnoreDataMember]
		public DateTimeOffset? Recorded
		{
			get { return RecordedElement?.Value; }
			set
			{
				if (value is null)
					RecordedElement = null;
				else
					RecordedElement = new Hl7.Fhir.Model.Instant(value);
			}
		}

		/// <summary>
		/// Reason the activity is occurring
		/// </summary>
		[FhirElement("reason", Order = 100)]
		[DataMember]
		public Hl7.Fhir.Model.CodeableConcept Reason { get; set; }

		/// <summary>
		/// Where the activity occurred, if relevant
		/// </summary>
		[FhirElement("location", Order = 110)]
		[References("Location")]
		[DataMember]
		public Hl7.Fhir.Model.ResourceReference Location { get; set; }

		/// <summary>
		/// Policy or plan the activity was defined by
		/// </summary>
		[FhirElement("policy", Order = 120)]
		[Cardinality(Min = 0, Max = -1)]
		[DataMember]
		public List<Hl7.Fhir.Model.FhirUri> PolicyElement { get; set; }

		/// <summary>
		/// Policy or plan the activity was defined by
		/// </summary>
		/// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
		[NotMapped]
		[IgnoreDataMember]
		public IEnumerable<string> Policy
		{
			get { return PolicyElement?.Select(elem => elem.Value); }
			set
			{
				if (value is null)
					PolicyElement = null;
				else
					PolicyElement = new List<Hl7.Fhir.Model.FhirUri>(value.Select(elem => new Hl7.Fhir.Model.FhirUri(elem)));
			}
		}

		/// <summary>
		/// Person, organization, records, etc. involved in creating resource
		/// </summary>
		[FhirElement("agent", Order = 130)]
		[Cardinality(Min = 0, Max = -1)]
		[DataMember]
		public List<Hl7.Fhir.Model.Provenance.ProvenanceAgentComponent> Agent { get; set; }

		/// <summary>
		/// An entity used in this activity
		/// </summary>
		[FhirElement("entity", Order = 140)]
		[Cardinality(Min = 0, Max = -1)]
		[DataMember]
		public List<Hl7.Fhir.Model.Provenance.ProvenanceEntityComponent> Entity { get; set; }

		/// <summary>
		/// Base64 signature (DigSig) - integrity check
		/// </summary>
		[FhirElement("integritySignature", Order = 150)]
		[DataMember]
		public Hl7.Fhir.Model.FhirString IntegritySignatureElement { get; set; }

		/// <summary>
		/// Base64 signature (DigSig) - integrity check
		/// </summary>
		/// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
		[NotMapped]
		[IgnoreDataMember]
		public string IntegritySignature
		{
			get { return IntegritySignatureElement?.Value; }
			set
			{
				if (value is null)
					IntegritySignatureElement = null;
				else
					IntegritySignatureElement = new Hl7.Fhir.Model.FhirString(value);
			}
		}

		public override IDeepCopyable CopyTo(IDeepCopyable other)
		{
			var dest = other as Provenance;

			if (dest != null)
			{
				base.CopyTo(dest);
				if (Target != null) dest.Target = new List<Hl7.Fhir.Model.ResourceReference>(Target.DeepCopy());
				if (Period != null) dest.Period = (Hl7.Fhir.Model.Period)Period.DeepCopy();
				if (RecordedElement != null) dest.RecordedElement = (Hl7.Fhir.Model.Instant)RecordedElement.DeepCopy();
				if (Reason != null) dest.Reason = (Hl7.Fhir.Model.CodeableConcept)Reason.DeepCopy();
				if (Location != null) dest.Location = (Hl7.Fhir.Model.ResourceReference)Location.DeepCopy();
				if (PolicyElement != null) dest.PolicyElement = new List<Hl7.Fhir.Model.FhirUri>(PolicyElement.DeepCopy());
				if (Agent != null) dest.Agent = new List<Hl7.Fhir.Model.Provenance.ProvenanceAgentComponent>(Agent.DeepCopy());
				if (Entity != null) dest.Entity = new List<Hl7.Fhir.Model.Provenance.ProvenanceEntityComponent>(Entity.DeepCopy());
				if (IntegritySignatureElement != null) dest.IntegritySignatureElement = (Hl7.Fhir.Model.FhirString)IntegritySignatureElement.DeepCopy();
				return dest;
			}
			else
				throw Error.Argument(nameof(other), "Can only copy to an object of the same type");
		}

		public override IDeepCopyable DeepCopy()
		{
			return CopyTo(new Provenance());
		}

		public override bool Matches(IDeepComparable other)
		{
			var otherT = other as Provenance;
			if (otherT is null) return false;

			if (!base.Matches(otherT)) return false;
			if (!DeepComparable.Matches(Target, otherT.Target)) return false;
			if (!DeepComparable.Matches(Period, otherT.Period)) return false;
			if (!DeepComparable.Matches(RecordedElement, otherT.RecordedElement)) return false;
			if (!DeepComparable.Matches(Reason, otherT.Reason)) return false;
			if (!DeepComparable.Matches(Location, otherT.Location)) return false;
			if (!DeepComparable.Matches(PolicyElement, otherT.PolicyElement)) return false;
			if (!DeepComparable.Matches(Agent, otherT.Agent)) return false;
			if (!DeepComparable.Matches(Entity, otherT.Entity)) return false;
			if (!DeepComparable.Matches(IntegritySignatureElement, otherT.IntegritySignatureElement)) return false;

			return true;
		}

		public override bool IsExactly(IDeepComparable other)
		{
			var otherT = other as Provenance;
			if (otherT is null) return false;

			if (!base.IsExactly(otherT)) return false;
			if (!DeepComparable.IsExactly(Target, otherT.Target)) return false;
			if (!DeepComparable.IsExactly(Period, otherT.Period)) return false;
			if (!DeepComparable.IsExactly(RecordedElement, otherT.RecordedElement)) return false;
			if (!DeepComparable.IsExactly(Reason, otherT.Reason)) return false;
			if (!DeepComparable.IsExactly(Location, otherT.Location)) return false;
			if (!DeepComparable.IsExactly(PolicyElement, otherT.PolicyElement)) return false;
			if (!DeepComparable.IsExactly(Agent, otherT.Agent)) return false;
			if (!DeepComparable.IsExactly(Entity, otherT.Entity)) return false;
			if (!DeepComparable.IsExactly(IntegritySignatureElement, otherT.IntegritySignatureElement)) return false;

			return true;
		}

	}

}
