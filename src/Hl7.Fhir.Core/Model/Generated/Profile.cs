using Hl7.Fhir.Introspection;
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
	/// Resource Profile
	/// </summary>
	[FhirType("Profile", IsResource = true)]
	[DataContract]
	public partial class Profile : Hl7.Fhir.Model.Resource
	{
		/// <summary>
		/// Binding conformance for applications
		/// </summary>
		[FhirEnumeration("BindingConformance")]
		public enum BindingConformance
		{
			/// <summary>
			/// Only codes in the specified set are allowed.  If the binding is extensible, other codes may be used for concepts not covered by the bound set of codes.
			/// </summary>
			[EnumLiteral("required")]
			Required,
			/// <summary>
			/// For greater interoperability, implementers are strongly encouraged to use the bound set of codes, however alternate codes may be used in derived profiles and implementations if necessary without being considered non-conformant.
			/// </summary>
			[EnumLiteral("preferred")]
			Preferred,
			/// <summary>
			/// The codes in the set are an example to illustrate the meaning of the field. There is no particular preference for its use nor any assertion that the provided values are sufficient to meet implementation needs.
			/// </summary>
			[EnumLiteral("example")]
			Example,
		}

		/// <summary>
		/// SHALL applications comply with this constraint?
		/// </summary>
		[FhirEnumeration("ConstraintSeverity")]
		public enum ConstraintSeverity
		{
			/// <summary>
			/// If the constraint is violated, the resource is not conformant.
			/// </summary>
			[EnumLiteral("error")]
			Error,
			/// <summary>
			/// If the constraint is violated, the resource is conformant, but it is not necessarily following best practice.
			/// </summary>
			[EnumLiteral("warning")]
			Warning,
		}

		/// <summary>
		/// The lifecycle status of a Resource Profile
		/// </summary>
		[FhirEnumeration("ResourceProfileStatus")]
		public enum ResourceProfileStatus
		{
			/// <summary>
			/// This profile is still under development.
			/// </summary>
			[EnumLiteral("draft")]
			Draft,
			/// <summary>
			/// This profile is ready for normal use.
			/// </summary>
			[EnumLiteral("active")]
			Active,
			/// <summary>
			/// This profile has been deprecated, withdrawn or superseded and should no longer be used.
			/// </summary>
			[EnumLiteral("retired")]
			Retired,
		}

		/// <summary>
		/// How a property is represented on the wire
		/// </summary>
		[FhirEnumeration("PropertyRepresentation")]
		public enum PropertyRepresentation
		{
			/// <summary>
			/// In XML, this property is represented as an attribute not an element.
			/// </summary>
			[EnumLiteral("xmlAttr")]
			XmlAttr,
		}

		/// <summary>
		/// How resource references can be aggregated
		/// </summary>
		[FhirEnumeration("AggregationMode")]
		public enum AggregationMode
		{
			/// <summary>
			/// The reference is a local reference to a contained resource.
			/// </summary>
			[EnumLiteral("contained")]
			Contained,
			/// <summary>
			/// The reference to to a resource that has to be resolved externally to the resource that includes the reference.
			/// </summary>
			[EnumLiteral("referenced")]
			Referenced,
			/// <summary>
			/// The resource the reference points to will be found in the same bundle as the resource that includes the reference.
			/// </summary>
			[EnumLiteral("bundled")]
			Bundled,
		}

		/// <summary>
		/// How an extension context is interpreted
		/// </summary>
		[FhirEnumeration("ExtensionContext")]
		public enum ExtensionContext
		{
			/// <summary>
			/// The context is all elements matching a particular resource element path.
			/// </summary>
			[EnumLiteral("resource")]
			Resource,
			/// <summary>
			/// The context is all nodes matching a particular data type element path (root or repeating element) or all elements referencing a particular primitive data type (expressed as the datatype name).
			/// </summary>
			[EnumLiteral("datatype")]
			Datatype,
			/// <summary>
			/// The context is all nodes whose mapping to a specified reference model corresponds to a particular mapping structure.  The context identifies the mapping target. The mapping should clearly identify where such an extension could be used.
			/// </summary>
			[EnumLiteral("mapping")]
			Mapping,
			/// <summary>
			/// The context is a particular extension from a particular profile.  Expressed as uri#name, where uri identifies the profile and #name identifies the extension code.
			/// </summary>
			[EnumLiteral("extension")]
			Extension,
		}

		/// <summary>
		/// How slices are interpreted when evaluating an instance
		/// </summary>
		[FhirEnumeration("SlicingRules")]
		public enum SlicingRules
		{
			/// <summary>
			/// No additional content is allowed other than that described by the slices in this profile.
			/// </summary>
			[EnumLiteral("closed")]
			Closed,
			/// <summary>
			/// Additional content is allowed anywhere in the list.
			/// </summary>
			[EnumLiteral("open")]
			Open,
			/// <summary>
			/// Additional content is allowed, but only at the end of the list.
			/// </summary>
			[EnumLiteral("openAtEnd")]
			OpenAtEnd,
		}

		[FhirType("ElementDefinitionComponent")]
		[DataContract]
		public partial class ElementDefinitionComponent : Hl7.Fhir.Model.Element
		{
			/// <summary>
			/// Concise definition for xml presentation
			/// </summary>
			[FhirElement("short", InSummary = true, Order = 40)]
			[Cardinality(Min = 1, Max = 1)]
			[DataMember]
			public Hl7.Fhir.Model.FhirString ShortElement { get; set; }

			/// <summary>
			/// Concise definition for xml presentation
			/// </summary>
			/// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
			[NotMapped]
			[IgnoreDataMember]
			public string Short
			{
				get { return ShortElement != null ? ShortElement.Value : null; }
				set
				{
					if (value is null)
						ShortElement = null;
					else
						ShortElement = new Hl7.Fhir.Model.FhirString(value);
				}
			}

			/// <summary>
			/// Full formal definition in human language
			/// </summary>
			[FhirElement("formal", InSummary = true, Order = 50)]
			[Cardinality(Min = 1, Max = 1)]
			[DataMember]
			public Hl7.Fhir.Model.FhirString FormalElement { get; set; }

			/// <summary>
			/// Full formal definition in human language
			/// </summary>
			/// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
			[NotMapped]
			[IgnoreDataMember]
			public string Formal
			{
				get { return FormalElement != null ? FormalElement.Value : null; }
				set
				{
					if (value is null)
						FormalElement = null;
					else
						FormalElement = new Hl7.Fhir.Model.FhirString(value);
				}
			}

			/// <summary>
			/// Comments about the use of this element
			/// </summary>
			[FhirElement("comments", InSummary = true, Order = 60)]
			[DataMember]
			public Hl7.Fhir.Model.FhirString CommentsElement { get; set; }

			/// <summary>
			/// Comments about the use of this element
			/// </summary>
			/// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
			[NotMapped]
			[IgnoreDataMember]
			public string Comments
			{
				get { return CommentsElement != null ? CommentsElement.Value : null; }
				set
				{
					if (value is null)
						CommentsElement = null;
					else
						CommentsElement = new Hl7.Fhir.Model.FhirString(value);
				}
			}

			/// <summary>
			/// Why is this needed?
			/// </summary>
			[FhirElement("requirements", InSummary = true, Order = 70)]
			[DataMember]
			public Hl7.Fhir.Model.FhirString RequirementsElement { get; set; }

			/// <summary>
			/// Why is this needed?
			/// </summary>
			/// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
			[NotMapped]
			[IgnoreDataMember]
			public string Requirements
			{
				get { return RequirementsElement != null ? RequirementsElement.Value : null; }
				set
				{
					if (value is null)
						RequirementsElement = null;
					else
						RequirementsElement = new Hl7.Fhir.Model.FhirString(value);
				}
			}

			/// <summary>
			/// Other names
			/// </summary>
			[FhirElement("synonym", InSummary = true, Order = 80)]
			[Cardinality(Min = 0, Max = -1)]
			[DataMember]
			public List<Hl7.Fhir.Model.FhirString> SynonymElement { get; set; }

			/// <summary>
			/// Other names
			/// </summary>
			/// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
			[NotMapped]
			[IgnoreDataMember]
			public IEnumerable<string> Synonym
			{
				get { return SynonymElement != null ? SynonymElement.Select(elem => elem.Value) : null; }
				set
				{
					if (value is null)
						SynonymElement = null;
					else
						SynonymElement = new List<Hl7.Fhir.Model.FhirString>(value.Select(elem => new Hl7.Fhir.Model.FhirString(elem)));
				}
			}

			/// <summary>
			/// Minimum Cardinality
			/// </summary>
			[FhirElement("min", InSummary = true, Order = 90)]
			[Cardinality(Min = 1, Max = 1)]
			[DataMember]
			public Hl7.Fhir.Model.Integer MinElement { get; set; }

			/// <summary>
			/// Minimum Cardinality
			/// </summary>
			/// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
			[NotMapped]
			[IgnoreDataMember]
			public int? Min
			{
				get { return MinElement != null ? MinElement.Value : null; }
				set
				{
					if (value is null)
						MinElement = null;
					else
						MinElement = new Hl7.Fhir.Model.Integer(value);
				}
			}

			/// <summary>
			/// Maximum Cardinality (a number or *)
			/// </summary>
			[FhirElement("max", InSummary = true, Order = 100)]
			[Cardinality(Min = 1, Max = 1)]
			[DataMember]
			public Hl7.Fhir.Model.FhirString MaxElement { get; set; }

			/// <summary>
			/// Maximum Cardinality (a number or *)
			/// </summary>
			/// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
			[NotMapped]
			[IgnoreDataMember]
			public string Max
			{
				get { return MaxElement != null ? MaxElement.Value : null; }
				set
				{
					if (value is null)
						MaxElement = null;
					else
						MaxElement = new Hl7.Fhir.Model.FhirString(value);
				}
			}

			/// <summary>
			/// Data type and Profile for this element
			/// </summary>
			[FhirElement("type", InSummary = true, Order = 110)]
			[Cardinality(Min = 0, Max = -1)]
			[DataMember]
			public List<Hl7.Fhir.Model.Profile.TypeRefComponent> Type { get; set; }

			/// <summary>
			/// To another element constraint (by element.name)
			/// </summary>
			[FhirElement("nameReference", InSummary = true, Order = 120)]
			[DataMember]
			public Hl7.Fhir.Model.FhirString NameReferenceElement { get; set; }

			/// <summary>
			/// To another element constraint (by element.name)
			/// </summary>
			/// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
			[NotMapped]
			[IgnoreDataMember]
			public string NameReference
			{
				get { return NameReferenceElement != null ? NameReferenceElement.Value : null; }
				set
				{
					if (value is null)
						NameReferenceElement = null;
					else
						NameReferenceElement = new Hl7.Fhir.Model.FhirString(value);
				}
			}

			/// <summary>
			/// Fixed value: [as defined for a primitive type]
			/// </summary>
			[FhirElement("value", InSummary = true, Order = 130, Choice = ChoiceType.DatatypeChoice)]
			[AllowedTypes(typeof(Hl7.Fhir.Model.Element))]
			[DataMember]
			public Hl7.Fhir.Model.Element Value { get; set; }

			/// <summary>
			/// Example value: [as defined for type]
			/// </summary>
			[FhirElement("example", InSummary = true, Order = 140, Choice = ChoiceType.DatatypeChoice)]
			[AllowedTypes(typeof(Hl7.Fhir.Model.Element))]
			[DataMember]
			public Hl7.Fhir.Model.Element Example { get; set; }

			/// <summary>
			/// Length for strings
			/// </summary>
			[FhirElement("maxLength", InSummary = true, Order = 150)]
			[DataMember]
			public Hl7.Fhir.Model.Integer MaxLengthElement { get; set; }

			/// <summary>
			/// Length for strings
			/// </summary>
			/// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
			[NotMapped]
			[IgnoreDataMember]
			public int? MaxLength
			{
				get { return MaxLengthElement != null ? MaxLengthElement.Value : null; }
				set
				{
					if (value is null)
						MaxLengthElement = null;
					else
						MaxLengthElement = new Hl7.Fhir.Model.Integer(value);
				}
			}

			/// <summary>
			/// Reference to invariant about presence
			/// </summary>
			[FhirElement("condition", InSummary = true, Order = 160)]
			[Cardinality(Min = 0, Max = -1)]
			[DataMember]
			public List<Hl7.Fhir.Model.Id> ConditionElement { get; set; }

			/// <summary>
			/// Reference to invariant about presence
			/// </summary>
			/// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
			[NotMapped]
			[IgnoreDataMember]
			public IEnumerable<string> Condition
			{
				get { return ConditionElement != null ? ConditionElement.Select(elem => elem.Value) : null; }
				set
				{
					if (value is null)
						ConditionElement = null;
					else
						ConditionElement = new List<Hl7.Fhir.Model.Id>(value.Select(elem => new Hl7.Fhir.Model.Id(elem)));
				}
			}

			/// <summary>
			/// Condition that must evaluate to true
			/// </summary>
			[FhirElement("constraint", InSummary = true, Order = 170)]
			[Cardinality(Min = 0, Max = -1)]
			[DataMember]
			public List<Hl7.Fhir.Model.Profile.ElementDefinitionConstraintComponent> Constraint { get; set; }

			/// <summary>
			/// If the element must supported
			/// </summary>
			[FhirElement("mustSupport", InSummary = true, Order = 180)]
			[DataMember]
			public Hl7.Fhir.Model.FhirBoolean MustSupportElement { get; set; }

			/// <summary>
			/// If the element must supported
			/// </summary>
			/// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
			[NotMapped]
			[IgnoreDataMember]
			public bool? MustSupport
			{
				get { return MustSupportElement != null ? MustSupportElement.Value : null; }
				set
				{
					if (value is null)
						MustSupportElement = null;
					else
						MustSupportElement = new Hl7.Fhir.Model.FhirBoolean(value);
				}
			}

			/// <summary>
			/// If this modifies the meaning of other elements
			/// </summary>
			[FhirElement("isModifier", InSummary = true, Order = 190)]
			[Cardinality(Min = 1, Max = 1)]
			[DataMember]
			public Hl7.Fhir.Model.FhirBoolean IsModifierElement { get; set; }

			/// <summary>
			/// If this modifies the meaning of other elements
			/// </summary>
			/// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
			[NotMapped]
			[IgnoreDataMember]
			public bool? IsModifier
			{
				get { return IsModifierElement != null ? IsModifierElement.Value : null; }
				set
				{
					if (value is null)
						IsModifierElement = null;
					else
						IsModifierElement = new Hl7.Fhir.Model.FhirBoolean(value);
				}
			}

			/// <summary>
			/// ValueSet details if this is coded
			/// </summary>
			[FhirElement("binding", InSummary = true, Order = 200)]
			[DataMember]
			public Hl7.Fhir.Model.Profile.ElementDefinitionBindingComponent Binding { get; set; }

			/// <summary>
			/// Map element to another set of definitions
			/// </summary>
			[FhirElement("mapping", InSummary = true, Order = 210)]
			[Cardinality(Min = 0, Max = -1)]
			[DataMember]
			public List<Hl7.Fhir.Model.Profile.ElementDefinitionMappingComponent> Mapping { get; set; }

			public override IDeepCopyable CopyTo(IDeepCopyable other)
			{
				var dest = other as ElementDefinitionComponent;

				if (dest != null)
				{
					base.CopyTo(dest);
					if (ShortElement != null) dest.ShortElement = (Hl7.Fhir.Model.FhirString)ShortElement.DeepCopy();
					if (FormalElement != null) dest.FormalElement = (Hl7.Fhir.Model.FhirString)FormalElement.DeepCopy();
					if (CommentsElement != null) dest.CommentsElement = (Hl7.Fhir.Model.FhirString)CommentsElement.DeepCopy();
					if (RequirementsElement != null) dest.RequirementsElement = (Hl7.Fhir.Model.FhirString)RequirementsElement.DeepCopy();
					if (SynonymElement != null) dest.SynonymElement = new List<Hl7.Fhir.Model.FhirString>(SynonymElement.DeepCopy());
					if (MinElement != null) dest.MinElement = (Hl7.Fhir.Model.Integer)MinElement.DeepCopy();
					if (MaxElement != null) dest.MaxElement = (Hl7.Fhir.Model.FhirString)MaxElement.DeepCopy();
					if (Type != null) dest.Type = new List<Hl7.Fhir.Model.Profile.TypeRefComponent>(Type.DeepCopy());
					if (NameReferenceElement != null) dest.NameReferenceElement = (Hl7.Fhir.Model.FhirString)NameReferenceElement.DeepCopy();
					if (Value != null) dest.Value = (Hl7.Fhir.Model.Element)Value.DeepCopy();
					if (Example != null) dest.Example = (Hl7.Fhir.Model.Element)Example.DeepCopy();
					if (MaxLengthElement != null) dest.MaxLengthElement = (Hl7.Fhir.Model.Integer)MaxLengthElement.DeepCopy();
					if (ConditionElement != null) dest.ConditionElement = new List<Hl7.Fhir.Model.Id>(ConditionElement.DeepCopy());
					if (Constraint != null) dest.Constraint = new List<Hl7.Fhir.Model.Profile.ElementDefinitionConstraintComponent>(Constraint.DeepCopy());
					if (MustSupportElement != null) dest.MustSupportElement = (Hl7.Fhir.Model.FhirBoolean)MustSupportElement.DeepCopy();
					if (IsModifierElement != null) dest.IsModifierElement = (Hl7.Fhir.Model.FhirBoolean)IsModifierElement.DeepCopy();
					if (Binding != null) dest.Binding = (Hl7.Fhir.Model.Profile.ElementDefinitionBindingComponent)Binding.DeepCopy();
					if (Mapping != null) dest.Mapping = new List<Hl7.Fhir.Model.Profile.ElementDefinitionMappingComponent>(Mapping.DeepCopy());
					return dest;
				}
				else
					throw Error.Argument(nameof(other), "Can only copy to an object of the same type");
			}

			public override IDeepCopyable DeepCopy()
			{
				return CopyTo(new ElementDefinitionComponent());
			}

			public override bool Matches(IDeepComparable other)
			{
				var otherT = other as ElementDefinitionComponent;
				if (otherT is null) return false;

				if (!base.Matches(otherT)) return false;
				if (!DeepComparable.Matches(ShortElement, otherT.ShortElement)) return false;
				if (!DeepComparable.Matches(FormalElement, otherT.FormalElement)) return false;
				if (!DeepComparable.Matches(CommentsElement, otherT.CommentsElement)) return false;
				if (!DeepComparable.Matches(RequirementsElement, otherT.RequirementsElement)) return false;
				if (!DeepComparable.Matches(SynonymElement, otherT.SynonymElement)) return false;
				if (!DeepComparable.Matches(MinElement, otherT.MinElement)) return false;
				if (!DeepComparable.Matches(MaxElement, otherT.MaxElement)) return false;
				if (!DeepComparable.Matches(Type, otherT.Type)) return false;
				if (!DeepComparable.Matches(NameReferenceElement, otherT.NameReferenceElement)) return false;
				if (!DeepComparable.Matches(Value, otherT.Value)) return false;
				if (!DeepComparable.Matches(Example, otherT.Example)) return false;
				if (!DeepComparable.Matches(MaxLengthElement, otherT.MaxLengthElement)) return false;
				if (!DeepComparable.Matches(ConditionElement, otherT.ConditionElement)) return false;
				if (!DeepComparable.Matches(Constraint, otherT.Constraint)) return false;
				if (!DeepComparable.Matches(MustSupportElement, otherT.MustSupportElement)) return false;
				if (!DeepComparable.Matches(IsModifierElement, otherT.IsModifierElement)) return false;
				if (!DeepComparable.Matches(Binding, otherT.Binding)) return false;
				if (!DeepComparable.Matches(Mapping, otherT.Mapping)) return false;

				return true;
			}

			public override bool IsExactly(IDeepComparable other)
			{
				var otherT = other as ElementDefinitionComponent;
				if (otherT is null) return false;

				if (!base.IsExactly(otherT)) return false;
				if (!DeepComparable.IsExactly(ShortElement, otherT.ShortElement)) return false;
				if (!DeepComparable.IsExactly(FormalElement, otherT.FormalElement)) return false;
				if (!DeepComparable.IsExactly(CommentsElement, otherT.CommentsElement)) return false;
				if (!DeepComparable.IsExactly(RequirementsElement, otherT.RequirementsElement)) return false;
				if (!DeepComparable.IsExactly(SynonymElement, otherT.SynonymElement)) return false;
				if (!DeepComparable.IsExactly(MinElement, otherT.MinElement)) return false;
				if (!DeepComparable.IsExactly(MaxElement, otherT.MaxElement)) return false;
				if (!DeepComparable.IsExactly(Type, otherT.Type)) return false;
				if (!DeepComparable.IsExactly(NameReferenceElement, otherT.NameReferenceElement)) return false;
				if (!DeepComparable.IsExactly(Value, otherT.Value)) return false;
				if (!DeepComparable.IsExactly(Example, otherT.Example)) return false;
				if (!DeepComparable.IsExactly(MaxLengthElement, otherT.MaxLengthElement)) return false;
				if (!DeepComparable.IsExactly(ConditionElement, otherT.ConditionElement)) return false;
				if (!DeepComparable.IsExactly(Constraint, otherT.Constraint)) return false;
				if (!DeepComparable.IsExactly(MustSupportElement, otherT.MustSupportElement)) return false;
				if (!DeepComparable.IsExactly(IsModifierElement, otherT.IsModifierElement)) return false;
				if (!DeepComparable.IsExactly(Binding, otherT.Binding)) return false;
				if (!DeepComparable.IsExactly(Mapping, otherT.Mapping)) return false;

				return true;
			}

		}


		[FhirType("ElementSlicingComponent")]
		[DataContract]
		public partial class ElementSlicingComponent : Hl7.Fhir.Model.Element
		{
			/// <summary>
			/// Element that used to distinguish the slices
			/// </summary>
			[FhirElement("discriminator", InSummary = true, Order = 40)]
			[Cardinality(Min = 1, Max = 1)]
			[DataMember]
			public Hl7.Fhir.Model.Id DiscriminatorElement { get; set; }

			/// <summary>
			/// Element that used to distinguish the slices
			/// </summary>
			/// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
			[NotMapped]
			[IgnoreDataMember]
			public string Discriminator
			{
				get { return DiscriminatorElement != null ? DiscriminatorElement.Value : null; }
				set
				{
					if (value is null)
						DiscriminatorElement = null;
					else
						DiscriminatorElement = new Hl7.Fhir.Model.Id(value);
				}
			}

			/// <summary>
			/// If elements must be in same order as slices
			/// </summary>
			[FhirElement("ordered", InSummary = true, Order = 50)]
			[Cardinality(Min = 1, Max = 1)]
			[DataMember]
			public Hl7.Fhir.Model.FhirBoolean OrderedElement { get; set; }

			/// <summary>
			/// If elements must be in same order as slices
			/// </summary>
			/// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
			[NotMapped]
			[IgnoreDataMember]
			public bool? Ordered
			{
				get { return OrderedElement != null ? OrderedElement.Value : null; }
				set
				{
					if (value is null)
						OrderedElement = null;
					else
						OrderedElement = new Hl7.Fhir.Model.FhirBoolean(value);
				}
			}

			/// <summary>
			/// closed | open | openAtEnd
			/// </summary>
			[FhirElement("rules", InSummary = true, Order = 60)]
			[Cardinality(Min = 1, Max = 1)]
			[DataMember]
			public Code<Hl7.Fhir.Model.Profile.SlicingRules> RulesElement { get; set; }

			/// <summary>
			/// closed | open | openAtEnd
			/// </summary>
			/// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
			[NotMapped]
			[IgnoreDataMember]
			public Hl7.Fhir.Model.Profile.SlicingRules? Rules
			{
				get { return RulesElement != null ? RulesElement.Value : null; }
				set
				{
					if (value is null)
						RulesElement = null;
					else
						RulesElement = new Code<Hl7.Fhir.Model.Profile.SlicingRules>(value);
				}
			}

			public override IDeepCopyable CopyTo(IDeepCopyable other)
			{
				var dest = other as ElementSlicingComponent;

				if (dest != null)
				{
					base.CopyTo(dest);
					if (DiscriminatorElement != null) dest.DiscriminatorElement = (Hl7.Fhir.Model.Id)DiscriminatorElement.DeepCopy();
					if (OrderedElement != null) dest.OrderedElement = (Hl7.Fhir.Model.FhirBoolean)OrderedElement.DeepCopy();
					if (RulesElement != null) dest.RulesElement = (Code<Hl7.Fhir.Model.Profile.SlicingRules>)RulesElement.DeepCopy();
					return dest;
				}
				else
					throw Error.Argument(nameof(other), "Can only copy to an object of the same type");
			}

			public override IDeepCopyable DeepCopy()
			{
				return CopyTo(new ElementSlicingComponent());
			}

			public override bool Matches(IDeepComparable other)
			{
				var otherT = other as ElementSlicingComponent;
				if (otherT is null) return false;

				if (!base.Matches(otherT)) return false;
				if (!DeepComparable.Matches(DiscriminatorElement, otherT.DiscriminatorElement)) return false;
				if (!DeepComparable.Matches(OrderedElement, otherT.OrderedElement)) return false;
				if (!DeepComparable.Matches(RulesElement, otherT.RulesElement)) return false;

				return true;
			}

			public override bool IsExactly(IDeepComparable other)
			{
				var otherT = other as ElementSlicingComponent;
				if (otherT is null) return false;

				if (!base.IsExactly(otherT)) return false;
				if (!DeepComparable.IsExactly(DiscriminatorElement, otherT.DiscriminatorElement)) return false;
				if (!DeepComparable.IsExactly(OrderedElement, otherT.OrderedElement)) return false;
				if (!DeepComparable.IsExactly(RulesElement, otherT.RulesElement)) return false;

				return true;
			}

		}


		[FhirType("ProfileStructureComponent")]
		[DataContract]
		public partial class ProfileStructureComponent : Hl7.Fhir.Model.Element
		{
			/// <summary>
			/// The Resource or Data Type being described
			/// </summary>
			[FhirElement("type", InSummary = true, Order = 40)]
			[Cardinality(Min = 1, Max = 1)]
			[DataMember]
			public Hl7.Fhir.Model.Code TypeElement { get; set; }

			/// <summary>
			/// The Resource or Data Type being described
			/// </summary>
			/// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
			[NotMapped]
			[IgnoreDataMember]
			public string Type
			{
				get { return TypeElement != null ? TypeElement.Value : null; }
				set
				{
					if (value is null)
						TypeElement = null;
					else
						TypeElement = new Hl7.Fhir.Model.Code(value);
				}
			}

			/// <summary>
			/// Name for this particular structure (reference target)
			/// </summary>
			[FhirElement("name", InSummary = true, Order = 50)]
			[DataMember]
			public Hl7.Fhir.Model.FhirString NameElement { get; set; }

			/// <summary>
			/// Name for this particular structure (reference target)
			/// </summary>
			/// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
			[NotMapped]
			[IgnoreDataMember]
			public string Name
			{
				get { return NameElement != null ? NameElement.Value : null; }
				set
				{
					if (value is null)
						NameElement = null;
					else
						NameElement = new Hl7.Fhir.Model.FhirString(value);
				}
			}

			/// <summary>
			/// This definition is published (i.e. for validation)
			/// </summary>
			[FhirElement("publish", InSummary = true, Order = 60)]
			[DataMember]
			public Hl7.Fhir.Model.FhirBoolean PublishElement { get; set; }

			/// <summary>
			/// This definition is published (i.e. for validation)
			/// </summary>
			/// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
			[NotMapped]
			[IgnoreDataMember]
			public bool? Publish
			{
				get { return PublishElement != null ? PublishElement.Value : null; }
				set
				{
					if (value is null)
						PublishElement = null;
					else
						PublishElement = new Hl7.Fhir.Model.FhirBoolean(value);
				}
			}

			/// <summary>
			/// Human summary: why describe this resource?
			/// </summary>
			[FhirElement("purpose", InSummary = true, Order = 70)]
			[DataMember]
			public Hl7.Fhir.Model.FhirString PurposeElement { get; set; }

			/// <summary>
			/// Human summary: why describe this resource?
			/// </summary>
			/// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
			[NotMapped]
			[IgnoreDataMember]
			public string Purpose
			{
				get { return PurposeElement != null ? PurposeElement.Value : null; }
				set
				{
					if (value is null)
						PurposeElement = null;
					else
						PurposeElement = new Hl7.Fhir.Model.FhirString(value);
				}
			}

			/// <summary>
			/// Definition of elements in the resource (if no profile)
			/// </summary>
			[FhirElement("element", InSummary = true, Order = 80)]
			[Cardinality(Min = 0, Max = -1)]
			[DataMember]
			public List<Hl7.Fhir.Model.Profile.ElementComponent> Element { get; set; }

			/// <summary>
			/// Search params defined
			/// </summary>
			[FhirElement("searchParam", InSummary = true, Order = 90)]
			[Cardinality(Min = 0, Max = -1)]
			[DataMember]
			public List<Hl7.Fhir.Model.Profile.ProfileStructureSearchParamComponent> SearchParam { get; set; }

			public override IDeepCopyable CopyTo(IDeepCopyable other)
			{
				var dest = other as ProfileStructureComponent;

				if (dest != null)
				{
					base.CopyTo(dest);
					if (TypeElement != null) dest.TypeElement = (Hl7.Fhir.Model.Code)TypeElement.DeepCopy();
					if (NameElement != null) dest.NameElement = (Hl7.Fhir.Model.FhirString)NameElement.DeepCopy();
					if (PublishElement != null) dest.PublishElement = (Hl7.Fhir.Model.FhirBoolean)PublishElement.DeepCopy();
					if (PurposeElement != null) dest.PurposeElement = (Hl7.Fhir.Model.FhirString)PurposeElement.DeepCopy();
					if (Element != null) dest.Element = new List<Hl7.Fhir.Model.Profile.ElementComponent>(Element.DeepCopy());
					if (SearchParam != null) dest.SearchParam = new List<Hl7.Fhir.Model.Profile.ProfileStructureSearchParamComponent>(SearchParam.DeepCopy());
					return dest;
				}
				else
					throw Error.Argument(nameof(other), "Can only copy to an object of the same type");
			}

			public override IDeepCopyable DeepCopy()
			{
				return CopyTo(new ProfileStructureComponent());
			}

			public override bool Matches(IDeepComparable other)
			{
				var otherT = other as ProfileStructureComponent;
				if (otherT is null) return false;

				if (!base.Matches(otherT)) return false;
				if (!DeepComparable.Matches(TypeElement, otherT.TypeElement)) return false;
				if (!DeepComparable.Matches(NameElement, otherT.NameElement)) return false;
				if (!DeepComparable.Matches(PublishElement, otherT.PublishElement)) return false;
				if (!DeepComparable.Matches(PurposeElement, otherT.PurposeElement)) return false;
				if (!DeepComparable.Matches(Element, otherT.Element)) return false;
				if (!DeepComparable.Matches(SearchParam, otherT.SearchParam)) return false;

				return true;
			}

			public override bool IsExactly(IDeepComparable other)
			{
				var otherT = other as ProfileStructureComponent;
				if (otherT is null) return false;

				if (!base.IsExactly(otherT)) return false;
				if (!DeepComparable.IsExactly(TypeElement, otherT.TypeElement)) return false;
				if (!DeepComparable.IsExactly(NameElement, otherT.NameElement)) return false;
				if (!DeepComparable.IsExactly(PublishElement, otherT.PublishElement)) return false;
				if (!DeepComparable.IsExactly(PurposeElement, otherT.PurposeElement)) return false;
				if (!DeepComparable.IsExactly(Element, otherT.Element)) return false;
				if (!DeepComparable.IsExactly(SearchParam, otherT.SearchParam)) return false;

				return true;
			}

		}


		[FhirType("ProfileStructureSearchParamComponent")]
		[DataContract]
		public partial class ProfileStructureSearchParamComponent : Hl7.Fhir.Model.Element
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
					if (value is null)
						NameElement = null;
					else
						NameElement = new Hl7.Fhir.Model.FhirString(value);
				}
			}

			/// <summary>
			/// number | date | string | token | reference | composite | quantity
			/// </summary>
			[FhirElement("type", InSummary = true, Order = 50)]
			[Cardinality(Min = 1, Max = 1)]
			[DataMember]
			public Hl7.Fhir.Model.Code TypeElement { get; set; }

			/// <summary>
			/// number | date | string | token | reference | composite | quantity
			/// </summary>
			/// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
			[NotMapped]
			[IgnoreDataMember]
			public string Type
			{
				get { return TypeElement != null ? TypeElement.Value : null; }
				set
				{
					if (value is null)
						TypeElement = null;
					else
						TypeElement = new Hl7.Fhir.Model.Code(value);
				}
			}

			/// <summary>
			/// Contents and meaning of search parameter
			/// </summary>
			[FhirElement("documentation", InSummary = true, Order = 60)]
			[Cardinality(Min = 1, Max = 1)]
			[DataMember]
			public Hl7.Fhir.Model.FhirString DocumentationElement { get; set; }

			/// <summary>
			/// Contents and meaning of search parameter
			/// </summary>
			/// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
			[NotMapped]
			[IgnoreDataMember]
			public string Documentation
			{
				get { return DocumentationElement != null ? DocumentationElement.Value : null; }
				set
				{
					if (value is null)
						DocumentationElement = null;
					else
						DocumentationElement = new Hl7.Fhir.Model.FhirString(value);
				}
			}

			/// <summary>
			/// XPath that extracts the parameter set
			/// </summary>
			[FhirElement("xpath", InSummary = true, Order = 70)]
			[DataMember]
			public Hl7.Fhir.Model.FhirString XpathElement { get; set; }

			/// <summary>
			/// XPath that extracts the parameter set
			/// </summary>
			/// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
			[NotMapped]
			[IgnoreDataMember]
			public string Xpath
			{
				get { return XpathElement != null ? XpathElement.Value : null; }
				set
				{
					if (value is null)
						XpathElement = null;
					else
						XpathElement = new Hl7.Fhir.Model.FhirString(value);
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
					if (value is null)
						TargetElement = null;
					else
						TargetElement = new List<Hl7.Fhir.Model.Code>(value.Select(elem => new Hl7.Fhir.Model.Code(elem)));
				}
			}

			public override IDeepCopyable CopyTo(IDeepCopyable other)
			{
				var dest = other as ProfileStructureSearchParamComponent;

				if (dest != null)
				{
					base.CopyTo(dest);
					if (NameElement != null) dest.NameElement = (Hl7.Fhir.Model.FhirString)NameElement.DeepCopy();
					if (TypeElement != null) dest.TypeElement = (Hl7.Fhir.Model.Code)TypeElement.DeepCopy();
					if (DocumentationElement != null) dest.DocumentationElement = (Hl7.Fhir.Model.FhirString)DocumentationElement.DeepCopy();
					if (XpathElement != null) dest.XpathElement = (Hl7.Fhir.Model.FhirString)XpathElement.DeepCopy();
					if (TargetElement != null) dest.TargetElement = new List<Hl7.Fhir.Model.Code>(TargetElement.DeepCopy());
					return dest;
				}
				else
					throw Error.Argument(nameof(other), "Can only copy to an object of the same type");
			}

			public override IDeepCopyable DeepCopy()
			{
				return CopyTo(new ProfileStructureSearchParamComponent());
			}

			public override bool Matches(IDeepComparable other)
			{
				var otherT = other as ProfileStructureSearchParamComponent;
				if (otherT is null) return false;

				if (!base.Matches(otherT)) return false;
				if (!DeepComparable.Matches(NameElement, otherT.NameElement)) return false;
				if (!DeepComparable.Matches(TypeElement, otherT.TypeElement)) return false;
				if (!DeepComparable.Matches(DocumentationElement, otherT.DocumentationElement)) return false;
				if (!DeepComparable.Matches(XpathElement, otherT.XpathElement)) return false;
				if (!DeepComparable.Matches(TargetElement, otherT.TargetElement)) return false;

				return true;
			}

			public override bool IsExactly(IDeepComparable other)
			{
				var otherT = other as ProfileStructureSearchParamComponent;
				if (otherT is null) return false;

				if (!base.IsExactly(otherT)) return false;
				if (!DeepComparable.IsExactly(NameElement, otherT.NameElement)) return false;
				if (!DeepComparable.IsExactly(TypeElement, otherT.TypeElement)) return false;
				if (!DeepComparable.IsExactly(DocumentationElement, otherT.DocumentationElement)) return false;
				if (!DeepComparable.IsExactly(XpathElement, otherT.XpathElement)) return false;
				if (!DeepComparable.IsExactly(TargetElement, otherT.TargetElement)) return false;

				return true;
			}

		}


		[FhirType("ProfileQueryComponent")]
		[DataContract]
		public partial class ProfileQueryComponent : Hl7.Fhir.Model.Element
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
					if (value is null)
						NameElement = null;
					else
						NameElement = new Hl7.Fhir.Model.FhirString(value);
				}
			}

			/// <summary>
			/// Describes the named query
			/// </summary>
			[FhirElement("documentation", InSummary = true, Order = 50)]
			[Cardinality(Min = 1, Max = 1)]
			[DataMember]
			public Hl7.Fhir.Model.FhirString DocumentationElement { get; set; }

			/// <summary>
			/// Describes the named query
			/// </summary>
			/// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
			[NotMapped]
			[IgnoreDataMember]
			public string Documentation
			{
				get { return DocumentationElement != null ? DocumentationElement.Value : null; }
				set
				{
					if (value is null)
						DocumentationElement = null;
					else
						DocumentationElement = new Hl7.Fhir.Model.FhirString(value);
				}
			}

			/// <summary>
			/// Parameter for the named query
			/// </summary>
			[FhirElement("parameter", InSummary = true, Order = 60)]
			[Cardinality(Min = 0, Max = -1)]
			[DataMember]
			public List<Hl7.Fhir.Model.Profile.ProfileStructureSearchParamComponent> Parameter { get; set; }

			public override IDeepCopyable CopyTo(IDeepCopyable other)
			{
				var dest = other as ProfileQueryComponent;

				if (dest != null)
				{
					base.CopyTo(dest);
					if (NameElement != null) dest.NameElement = (Hl7.Fhir.Model.FhirString)NameElement.DeepCopy();
					if (DocumentationElement != null) dest.DocumentationElement = (Hl7.Fhir.Model.FhirString)DocumentationElement.DeepCopy();
					if (Parameter != null) dest.Parameter = new List<Hl7.Fhir.Model.Profile.ProfileStructureSearchParamComponent>(Parameter.DeepCopy());
					return dest;
				}
				else
					throw Error.Argument(nameof(other), "Can only copy to an object of the same type");
			}

			public override IDeepCopyable DeepCopy()
			{
				return CopyTo(new ProfileQueryComponent());
			}

			public override bool Matches(IDeepComparable other)
			{
				var otherT = other as ProfileQueryComponent;
				if (otherT is null) return false;

				if (!base.Matches(otherT)) return false;
				if (!DeepComparable.Matches(NameElement, otherT.NameElement)) return false;
				if (!DeepComparable.Matches(DocumentationElement, otherT.DocumentationElement)) return false;
				if (!DeepComparable.Matches(Parameter, otherT.Parameter)) return false;

				return true;
			}

			public override bool IsExactly(IDeepComparable other)
			{
				var otherT = other as ProfileQueryComponent;
				if (otherT is null) return false;

				if (!base.IsExactly(otherT)) return false;
				if (!DeepComparable.IsExactly(NameElement, otherT.NameElement)) return false;
				if (!DeepComparable.IsExactly(DocumentationElement, otherT.DocumentationElement)) return false;
				if (!DeepComparable.IsExactly(Parameter, otherT.Parameter)) return false;

				return true;
			}

		}


		[FhirType("TypeRefComponent")]
		[DataContract]
		public partial class TypeRefComponent : Hl7.Fhir.Model.Element
		{
			/// <summary>
			/// Name of Data type or Resource
			/// </summary>
			[FhirElement("code", InSummary = true, Order = 40)]
			[Cardinality(Min = 1, Max = 1)]
			[DataMember]
			public Hl7.Fhir.Model.Code CodeElement { get; set; }

			/// <summary>
			/// Name of Data type or Resource
			/// </summary>
			/// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
			[NotMapped]
			[IgnoreDataMember]
			public string Code
			{
				get { return CodeElement != null ? CodeElement.Value : null; }
				set
				{
					if (value is null)
						CodeElement = null;
					else
						CodeElement = new Hl7.Fhir.Model.Code(value);
				}
			}

			/// <summary>
			/// Profile.structure to apply
			/// </summary>
			[FhirElement("profile", InSummary = true, Order = 50)]
			[DataMember]
			public Hl7.Fhir.Model.FhirUri ProfileElement { get; set; }

			/// <summary>
			/// Profile.structure to apply
			/// </summary>
			/// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
			[NotMapped]
			[IgnoreDataMember]
			public string Profile
			{
				get { return ProfileElement != null ? ProfileElement.Value : null; }
				set
				{
					if (value is null)
						ProfileElement = null;
					else
						ProfileElement = new Hl7.Fhir.Model.FhirUri(value);
				}
			}

			/// <summary>
			/// contained | referenced | bundled - how aggregated
			/// </summary>
			[FhirElement("aggregation", InSummary = true, Order = 60)]
			[Cardinality(Min = 0, Max = -1)]
			[DataMember]
			public List<Code<Hl7.Fhir.Model.Profile.AggregationMode>> AggregationElement { get; set; }

			/// <summary>
			/// contained | referenced | bundled - how aggregated
			/// </summary>
			/// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
			[NotMapped]
			[IgnoreDataMember]
			public IEnumerable<Hl7.Fhir.Model.Profile.AggregationMode?> Aggregation
			{
				get { return AggregationElement != null ? AggregationElement.Select(elem => elem.Value) : null; }
				set
				{
					if (value is null)
						AggregationElement = null;
					else
						AggregationElement = new List<Code<Hl7.Fhir.Model.Profile.AggregationMode>>(value.Select(elem => new Code<Hl7.Fhir.Model.Profile.AggregationMode>(elem)));
				}
			}

			public override IDeepCopyable CopyTo(IDeepCopyable other)
			{
				var dest = other as TypeRefComponent;

				if (dest != null)
				{
					base.CopyTo(dest);
					if (CodeElement != null) dest.CodeElement = (Hl7.Fhir.Model.Code)CodeElement.DeepCopy();
					if (ProfileElement != null) dest.ProfileElement = (Hl7.Fhir.Model.FhirUri)ProfileElement.DeepCopy();
					if (AggregationElement != null) dest.AggregationElement = new List<Code<Hl7.Fhir.Model.Profile.AggregationMode>>(AggregationElement.DeepCopy());
					return dest;
				}
				else
					throw Error.Argument(nameof(other), "Can only copy to an object of the same type");
			}

			public override IDeepCopyable DeepCopy()
			{
				return CopyTo(new TypeRefComponent());
			}

			public override bool Matches(IDeepComparable other)
			{
				var otherT = other as TypeRefComponent;
				if (otherT is null) return false;

				if (!base.Matches(otherT)) return false;
				if (!DeepComparable.Matches(CodeElement, otherT.CodeElement)) return false;
				if (!DeepComparable.Matches(ProfileElement, otherT.ProfileElement)) return false;
				if (!DeepComparable.Matches(AggregationElement, otherT.AggregationElement)) return false;

				return true;
			}

			public override bool IsExactly(IDeepComparable other)
			{
				var otherT = other as TypeRefComponent;
				if (otherT is null) return false;

				if (!base.IsExactly(otherT)) return false;
				if (!DeepComparable.IsExactly(CodeElement, otherT.CodeElement)) return false;
				if (!DeepComparable.IsExactly(ProfileElement, otherT.ProfileElement)) return false;
				if (!DeepComparable.IsExactly(AggregationElement, otherT.AggregationElement)) return false;

				return true;
			}

		}


		[FhirType("ProfileMappingComponent")]
		[DataContract]
		public partial class ProfileMappingComponent : Hl7.Fhir.Model.Element
		{
			/// <summary>
			/// Internal id when this mapping is used
			/// </summary>
			[FhirElement("identity", InSummary = true, Order = 40)]
			[Cardinality(Min = 1, Max = 1)]
			[DataMember]
			public Hl7.Fhir.Model.Id IdentityElement { get; set; }

			/// <summary>
			/// Internal id when this mapping is used
			/// </summary>
			/// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
			[NotMapped]
			[IgnoreDataMember]
			public string Identity
			{
				get { return IdentityElement != null ? IdentityElement.Value : null; }
				set
				{
					if (value is null)
						IdentityElement = null;
					else
						IdentityElement = new Hl7.Fhir.Model.Id(value);
				}
			}

			/// <summary>
			/// Identifies what this mapping refers to
			/// </summary>
			[FhirElement("uri", InSummary = true, Order = 50)]
			[DataMember]
			public Hl7.Fhir.Model.FhirUri UriElement { get; set; }

			/// <summary>
			/// Identifies what this mapping refers to
			/// </summary>
			/// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
			[NotMapped]
			[IgnoreDataMember]
			public string Uri
			{
				get { return UriElement != null ? UriElement.Value : null; }
				set
				{
					if (value is null)
						UriElement = null;
					else
						UriElement = new Hl7.Fhir.Model.FhirUri(value);
				}
			}

			/// <summary>
			/// Names what this mapping refers to
			/// </summary>
			[FhirElement("name", InSummary = true, Order = 60)]
			[DataMember]
			public Hl7.Fhir.Model.FhirString NameElement { get; set; }

			/// <summary>
			/// Names what this mapping refers to
			/// </summary>
			/// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
			[NotMapped]
			[IgnoreDataMember]
			public string Name
			{
				get { return NameElement != null ? NameElement.Value : null; }
				set
				{
					if (value is null)
						NameElement = null;
					else
						NameElement = new Hl7.Fhir.Model.FhirString(value);
				}
			}

			/// <summary>
			/// Versions, Issues, Scope limitations etc
			/// </summary>
			[FhirElement("comments", InSummary = true, Order = 70)]
			[DataMember]
			public Hl7.Fhir.Model.FhirString CommentsElement { get; set; }

			/// <summary>
			/// Versions, Issues, Scope limitations etc
			/// </summary>
			/// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
			[NotMapped]
			[IgnoreDataMember]
			public string Comments
			{
				get { return CommentsElement != null ? CommentsElement.Value : null; }
				set
				{
					if (value is null)
						CommentsElement = null;
					else
						CommentsElement = new Hl7.Fhir.Model.FhirString(value);
				}
			}

			public override IDeepCopyable CopyTo(IDeepCopyable other)
			{
				var dest = other as ProfileMappingComponent;

				if (dest != null)
				{
					base.CopyTo(dest);
					if (IdentityElement != null) dest.IdentityElement = (Hl7.Fhir.Model.Id)IdentityElement.DeepCopy();
					if (UriElement != null) dest.UriElement = (Hl7.Fhir.Model.FhirUri)UriElement.DeepCopy();
					if (NameElement != null) dest.NameElement = (Hl7.Fhir.Model.FhirString)NameElement.DeepCopy();
					if (CommentsElement != null) dest.CommentsElement = (Hl7.Fhir.Model.FhirString)CommentsElement.DeepCopy();
					return dest;
				}
				else
					throw Error.Argument(nameof(other), "Can only copy to an object of the same type");
			}

			public override IDeepCopyable DeepCopy()
			{
				return CopyTo(new ProfileMappingComponent());
			}

			public override bool Matches(IDeepComparable other)
			{
				var otherT = other as ProfileMappingComponent;
				if (otherT is null) return false;

				if (!base.Matches(otherT)) return false;
				if (!DeepComparable.Matches(IdentityElement, otherT.IdentityElement)) return false;
				if (!DeepComparable.Matches(UriElement, otherT.UriElement)) return false;
				if (!DeepComparable.Matches(NameElement, otherT.NameElement)) return false;
				if (!DeepComparable.Matches(CommentsElement, otherT.CommentsElement)) return false;

				return true;
			}

			public override bool IsExactly(IDeepComparable other)
			{
				var otherT = other as ProfileMappingComponent;
				if (otherT is null) return false;

				if (!base.IsExactly(otherT)) return false;
				if (!DeepComparable.IsExactly(IdentityElement, otherT.IdentityElement)) return false;
				if (!DeepComparable.IsExactly(UriElement, otherT.UriElement)) return false;
				if (!DeepComparable.IsExactly(NameElement, otherT.NameElement)) return false;
				if (!DeepComparable.IsExactly(CommentsElement, otherT.CommentsElement)) return false;

				return true;
			}

		}


		[FhirType("ElementDefinitionMappingComponent")]
		[DataContract]
		public partial class ElementDefinitionMappingComponent : Hl7.Fhir.Model.Element
		{
			/// <summary>
			/// Reference to mapping declaration
			/// </summary>
			[FhirElement("identity", InSummary = true, Order = 40)]
			[Cardinality(Min = 1, Max = 1)]
			[DataMember]
			public Hl7.Fhir.Model.Id IdentityElement { get; set; }

			/// <summary>
			/// Reference to mapping declaration
			/// </summary>
			/// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
			[NotMapped]
			[IgnoreDataMember]
			public string Identity
			{
				get { return IdentityElement != null ? IdentityElement.Value : null; }
				set
				{
					if (value is null)
						IdentityElement = null;
					else
						IdentityElement = new Hl7.Fhir.Model.Id(value);
				}
			}

			/// <summary>
			/// Details of the mapping
			/// </summary>
			[FhirElement("map", InSummary = true, Order = 50)]
			[Cardinality(Min = 1, Max = 1)]
			[DataMember]
			public Hl7.Fhir.Model.FhirString MapElement { get; set; }

			/// <summary>
			/// Details of the mapping
			/// </summary>
			/// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
			[NotMapped]
			[IgnoreDataMember]
			public string Map
			{
				get { return MapElement != null ? MapElement.Value : null; }
				set
				{
					if (value is null)
						MapElement = null;
					else
						MapElement = new Hl7.Fhir.Model.FhirString(value);
				}
			}

			public override IDeepCopyable CopyTo(IDeepCopyable other)
			{
				var dest = other as ElementDefinitionMappingComponent;

				if (dest != null)
				{
					base.CopyTo(dest);
					if (IdentityElement != null) dest.IdentityElement = (Hl7.Fhir.Model.Id)IdentityElement.DeepCopy();
					if (MapElement != null) dest.MapElement = (Hl7.Fhir.Model.FhirString)MapElement.DeepCopy();
					return dest;
				}
				else
					throw Error.Argument(nameof(other), "Can only copy to an object of the same type");
			}

			public override IDeepCopyable DeepCopy()
			{
				return CopyTo(new ElementDefinitionMappingComponent());
			}

			public override bool Matches(IDeepComparable other)
			{
				var otherT = other as ElementDefinitionMappingComponent;
				if (otherT is null) return false;

				if (!base.Matches(otherT)) return false;
				if (!DeepComparable.Matches(IdentityElement, otherT.IdentityElement)) return false;
				if (!DeepComparable.Matches(MapElement, otherT.MapElement)) return false;

				return true;
			}

			public override bool IsExactly(IDeepComparable other)
			{
				var otherT = other as ElementDefinitionMappingComponent;
				if (otherT is null) return false;

				if (!base.IsExactly(otherT)) return false;
				if (!DeepComparable.IsExactly(IdentityElement, otherT.IdentityElement)) return false;
				if (!DeepComparable.IsExactly(MapElement, otherT.MapElement)) return false;

				return true;
			}

		}


		[FhirType("ElementDefinitionBindingComponent")]
		[DataContract]
		public partial class ElementDefinitionBindingComponent : Hl7.Fhir.Model.Element
		{
			/// <summary>
			/// Descriptive Name
			/// </summary>
			[FhirElement("name", InSummary = true, Order = 40)]
			[Cardinality(Min = 1, Max = 1)]
			[DataMember]
			public Hl7.Fhir.Model.FhirString NameElement { get; set; }

			/// <summary>
			/// Descriptive Name
			/// </summary>
			/// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
			[NotMapped]
			[IgnoreDataMember]
			public string Name
			{
				get { return NameElement != null ? NameElement.Value : null; }
				set
				{
					if (value is null)
						NameElement = null;
					else
						NameElement = new Hl7.Fhir.Model.FhirString(value);
				}
			}

			/// <summary>
			/// Can additional codes be used?
			/// </summary>
			[FhirElement("isExtensible", InSummary = true, Order = 50)]
			[Cardinality(Min = 1, Max = 1)]
			[DataMember]
			public Hl7.Fhir.Model.FhirBoolean IsExtensibleElement { get; set; }

			/// <summary>
			/// Can additional codes be used?
			/// </summary>
			/// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
			[NotMapped]
			[IgnoreDataMember]
			public bool? IsExtensible
			{
				get { return IsExtensibleElement != null ? IsExtensibleElement.Value : null; }
				set
				{
					if (value is null)
						IsExtensibleElement = null;
					else
						IsExtensibleElement = new Hl7.Fhir.Model.FhirBoolean(value);
				}
			}

			/// <summary>
			/// required | preferred | example
			/// </summary>
			[FhirElement("conformance", InSummary = true, Order = 60)]
			[DataMember]
			public Code<Hl7.Fhir.Model.Profile.BindingConformance> ConformanceElement { get; set; }

			/// <summary>
			/// required | preferred | example
			/// </summary>
			/// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
			[NotMapped]
			[IgnoreDataMember]
			public Hl7.Fhir.Model.Profile.BindingConformance? Conformance
			{
				get { return ConformanceElement != null ? ConformanceElement.Value : null; }
				set
				{
					if (value is null)
						ConformanceElement = null;
					else
						ConformanceElement = new Code<Hl7.Fhir.Model.Profile.BindingConformance>(value);
				}
			}

			/// <summary>
			/// Human explanation of the value set
			/// </summary>
			[FhirElement("description", InSummary = true, Order = 70)]
			[DataMember]
			public Hl7.Fhir.Model.FhirString DescriptionElement { get; set; }

			/// <summary>
			/// Human explanation of the value set
			/// </summary>
			/// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
			[NotMapped]
			[IgnoreDataMember]
			public string Description
			{
				get { return DescriptionElement != null ? DescriptionElement.Value : null; }
				set
				{
					if (value is null)
						DescriptionElement = null;
					else
						DescriptionElement = new Hl7.Fhir.Model.FhirString(value);
				}
			}

			/// <summary>
			/// Source of value set
			/// </summary>
			[FhirElement("reference", InSummary = true, Order = 80, Choice = ChoiceType.DatatypeChoice)]
			[AllowedTypes(typeof(Hl7.Fhir.Model.FhirUri), typeof(Hl7.Fhir.Model.ResourceReference))]
			[DataMember]
			public Hl7.Fhir.Model.Element Reference { get; set; }

			public override IDeepCopyable CopyTo(IDeepCopyable other)
			{
				var dest = other as ElementDefinitionBindingComponent;

				if (dest != null)
				{
					base.CopyTo(dest);
					if (NameElement != null) dest.NameElement = (Hl7.Fhir.Model.FhirString)NameElement.DeepCopy();
					if (IsExtensibleElement != null) dest.IsExtensibleElement = (Hl7.Fhir.Model.FhirBoolean)IsExtensibleElement.DeepCopy();
					if (ConformanceElement != null) dest.ConformanceElement = (Code<Hl7.Fhir.Model.Profile.BindingConformance>)ConformanceElement.DeepCopy();
					if (DescriptionElement != null) dest.DescriptionElement = (Hl7.Fhir.Model.FhirString)DescriptionElement.DeepCopy();
					if (Reference != null) dest.Reference = (Hl7.Fhir.Model.Element)Reference.DeepCopy();
					return dest;
				}
				else
					throw Error.Argument(nameof(other), "Can only copy to an object of the same type");
			}

			public override IDeepCopyable DeepCopy()
			{
				return CopyTo(new ElementDefinitionBindingComponent());
			}

			public override bool Matches(IDeepComparable other)
			{
				var otherT = other as ElementDefinitionBindingComponent;
				if (otherT is null) return false;

				if (!base.Matches(otherT)) return false;
				if (!DeepComparable.Matches(NameElement, otherT.NameElement)) return false;
				if (!DeepComparable.Matches(IsExtensibleElement, otherT.IsExtensibleElement)) return false;
				if (!DeepComparable.Matches(ConformanceElement, otherT.ConformanceElement)) return false;
				if (!DeepComparable.Matches(DescriptionElement, otherT.DescriptionElement)) return false;
				if (!DeepComparable.Matches(Reference, otherT.Reference)) return false;

				return true;
			}

			public override bool IsExactly(IDeepComparable other)
			{
				var otherT = other as ElementDefinitionBindingComponent;
				if (otherT is null) return false;

				if (!base.IsExactly(otherT)) return false;
				if (!DeepComparable.IsExactly(NameElement, otherT.NameElement)) return false;
				if (!DeepComparable.IsExactly(IsExtensibleElement, otherT.IsExtensibleElement)) return false;
				if (!DeepComparable.IsExactly(ConformanceElement, otherT.ConformanceElement)) return false;
				if (!DeepComparable.IsExactly(DescriptionElement, otherT.DescriptionElement)) return false;
				if (!DeepComparable.IsExactly(Reference, otherT.Reference)) return false;

				return true;
			}

		}


		[FhirType("ElementDefinitionConstraintComponent")]
		[DataContract]
		public partial class ElementDefinitionConstraintComponent : Hl7.Fhir.Model.Element
		{
			/// <summary>
			/// Target of 'condition' reference above
			/// </summary>
			[FhirElement("key", InSummary = true, Order = 40)]
			[Cardinality(Min = 1, Max = 1)]
			[DataMember]
			public Hl7.Fhir.Model.Id KeyElement { get; set; }

			/// <summary>
			/// Target of 'condition' reference above
			/// </summary>
			/// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
			[NotMapped]
			[IgnoreDataMember]
			public string Key
			{
				get { return KeyElement != null ? KeyElement.Value : null; }
				set
				{
					if (value is null)
						KeyElement = null;
					else
						KeyElement = new Hl7.Fhir.Model.Id(value);
				}
			}

			/// <summary>
			/// Short human label
			/// </summary>
			[FhirElement("name", InSummary = true, Order = 50)]
			[DataMember]
			public Hl7.Fhir.Model.FhirString NameElement { get; set; }

			/// <summary>
			/// Short human label
			/// </summary>
			/// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
			[NotMapped]
			[IgnoreDataMember]
			public string Name
			{
				get { return NameElement != null ? NameElement.Value : null; }
				set
				{
					if (value is null)
						NameElement = null;
					else
						NameElement = new Hl7.Fhir.Model.FhirString(value);
				}
			}

			/// <summary>
			/// error | warning
			/// </summary>
			[FhirElement("severity", InSummary = true, Order = 60)]
			[Cardinality(Min = 1, Max = 1)]
			[DataMember]
			public Code<Hl7.Fhir.Model.Profile.ConstraintSeverity> SeverityElement { get; set; }

			/// <summary>
			/// error | warning
			/// </summary>
			/// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
			[NotMapped]
			[IgnoreDataMember]
			public Hl7.Fhir.Model.Profile.ConstraintSeverity? Severity
			{
				get { return SeverityElement != null ? SeverityElement.Value : null; }
				set
				{
					if (value is null)
						SeverityElement = null;
					else
						SeverityElement = new Code<Hl7.Fhir.Model.Profile.ConstraintSeverity>(value);
				}
			}

			/// <summary>
			/// Human description of constraint
			/// </summary>
			[FhirElement("human", InSummary = true, Order = 70)]
			[Cardinality(Min = 1, Max = 1)]
			[DataMember]
			public Hl7.Fhir.Model.FhirString HumanElement { get; set; }

			/// <summary>
			/// Human description of constraint
			/// </summary>
			/// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
			[NotMapped]
			[IgnoreDataMember]
			public string Human
			{
				get { return HumanElement != null ? HumanElement.Value : null; }
				set
				{
					if (value is null)
						HumanElement = null;
					else
						HumanElement = new Hl7.Fhir.Model.FhirString(value);
				}
			}

			/// <summary>
			/// XPath expression of constraint
			/// </summary>
			[FhirElement("xpath", InSummary = true, Order = 80)]
			[Cardinality(Min = 1, Max = 1)]
			[DataMember]
			public Hl7.Fhir.Model.FhirString XpathElement { get; set; }

			/// <summary>
			/// XPath expression of constraint
			/// </summary>
			/// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
			[NotMapped]
			[IgnoreDataMember]
			public string Xpath
			{
				get { return XpathElement != null ? XpathElement.Value : null; }
				set
				{
					if (value is null)
						XpathElement = null;
					else
						XpathElement = new Hl7.Fhir.Model.FhirString(value);
				}
			}

			public override IDeepCopyable CopyTo(IDeepCopyable other)
			{
				var dest = other as ElementDefinitionConstraintComponent;

				if (dest != null)
				{
					base.CopyTo(dest);
					if (KeyElement != null) dest.KeyElement = (Hl7.Fhir.Model.Id)KeyElement.DeepCopy();
					if (NameElement != null) dest.NameElement = (Hl7.Fhir.Model.FhirString)NameElement.DeepCopy();
					if (SeverityElement != null) dest.SeverityElement = (Code<Hl7.Fhir.Model.Profile.ConstraintSeverity>)SeverityElement.DeepCopy();
					if (HumanElement != null) dest.HumanElement = (Hl7.Fhir.Model.FhirString)HumanElement.DeepCopy();
					if (XpathElement != null) dest.XpathElement = (Hl7.Fhir.Model.FhirString)XpathElement.DeepCopy();
					return dest;
				}
				else
					throw Error.Argument(nameof(other), "Can only copy to an object of the same type");
			}

			public override IDeepCopyable DeepCopy()
			{
				return CopyTo(new ElementDefinitionConstraintComponent());
			}

			public override bool Matches(IDeepComparable other)
			{
				var otherT = other as ElementDefinitionConstraintComponent;
				if (otherT is null) return false;

				if (!base.Matches(otherT)) return false;
				if (!DeepComparable.Matches(KeyElement, otherT.KeyElement)) return false;
				if (!DeepComparable.Matches(NameElement, otherT.NameElement)) return false;
				if (!DeepComparable.Matches(SeverityElement, otherT.SeverityElement)) return false;
				if (!DeepComparable.Matches(HumanElement, otherT.HumanElement)) return false;
				if (!DeepComparable.Matches(XpathElement, otherT.XpathElement)) return false;

				return true;
			}

			public override bool IsExactly(IDeepComparable other)
			{
				var otherT = other as ElementDefinitionConstraintComponent;
				if (otherT is null) return false;

				if (!base.IsExactly(otherT)) return false;
				if (!DeepComparable.IsExactly(KeyElement, otherT.KeyElement)) return false;
				if (!DeepComparable.IsExactly(NameElement, otherT.NameElement)) return false;
				if (!DeepComparable.IsExactly(SeverityElement, otherT.SeverityElement)) return false;
				if (!DeepComparable.IsExactly(HumanElement, otherT.HumanElement)) return false;
				if (!DeepComparable.IsExactly(XpathElement, otherT.XpathElement)) return false;

				return true;
			}

		}


		[FhirType("ElementComponent")]
		[DataContract]
		public partial class ElementComponent : Hl7.Fhir.Model.Element
		{
			/// <summary>
			/// The path of the element (see the formal definitions)
			/// </summary>
			[FhirElement("path", InSummary = true, Order = 40)]
			[Cardinality(Min = 1, Max = 1)]
			[DataMember]
			public Hl7.Fhir.Model.FhirString PathElement { get; set; }

			/// <summary>
			/// The path of the element (see the formal definitions)
			/// </summary>
			/// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
			[NotMapped]
			[IgnoreDataMember]
			public string Path
			{
				get { return PathElement != null ? PathElement.Value : null; }
				set
				{
					if (value is null)
						PathElement = null;
					else
						PathElement = new Hl7.Fhir.Model.FhirString(value);
				}
			}

			/// <summary>
			/// How this element is represented in instances
			/// </summary>
			[FhirElement("representation", InSummary = true, Order = 50)]
			[Cardinality(Min = 0, Max = -1)]
			[DataMember]
			public List<Code<Hl7.Fhir.Model.Profile.PropertyRepresentation>> RepresentationElement { get; set; }

			/// <summary>
			/// How this element is represented in instances
			/// </summary>
			/// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
			[NotMapped]
			[IgnoreDataMember]
			public IEnumerable<Hl7.Fhir.Model.Profile.PropertyRepresentation?> Representation
			{
				get { return RepresentationElement != null ? RepresentationElement.Select(elem => elem.Value) : null; }
				set
				{
					if (value is null)
						RepresentationElement = null;
					else
						RepresentationElement = new List<Code<Hl7.Fhir.Model.Profile.PropertyRepresentation>>(value.Select(elem => new Code<Hl7.Fhir.Model.Profile.PropertyRepresentation>(elem)));
				}
			}

			/// <summary>
			/// Name for this particular element definition (reference target)
			/// </summary>
			[FhirElement("name", InSummary = true, Order = 60)]
			[DataMember]
			public Hl7.Fhir.Model.FhirString NameElement { get; set; }

			/// <summary>
			/// Name for this particular element definition (reference target)
			/// </summary>
			/// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
			[NotMapped]
			[IgnoreDataMember]
			public string Name
			{
				get { return NameElement != null ? NameElement.Value : null; }
				set
				{
					if (value is null)
						NameElement = null;
					else
						NameElement = new Hl7.Fhir.Model.FhirString(value);
				}
			}

			/// <summary>
			/// This element is sliced - slices follow
			/// </summary>
			[FhirElement("slicing", InSummary = true, Order = 70)]
			[DataMember]
			public Hl7.Fhir.Model.Profile.ElementSlicingComponent Slicing { get; set; }

			/// <summary>
			/// More specific definition of the element
			/// </summary>
			[FhirElement("definition", InSummary = true, Order = 80)]
			[DataMember]
			public Hl7.Fhir.Model.Profile.ElementDefinitionComponent Definition { get; set; }

			public override IDeepCopyable CopyTo(IDeepCopyable other)
			{
				var dest = other as ElementComponent;

				if (dest != null)
				{
					base.CopyTo(dest);
					if (PathElement != null) dest.PathElement = (Hl7.Fhir.Model.FhirString)PathElement.DeepCopy();
					if (RepresentationElement != null) dest.RepresentationElement = new List<Code<Hl7.Fhir.Model.Profile.PropertyRepresentation>>(RepresentationElement.DeepCopy());
					if (NameElement != null) dest.NameElement = (Hl7.Fhir.Model.FhirString)NameElement.DeepCopy();
					if (Slicing != null) dest.Slicing = (Hl7.Fhir.Model.Profile.ElementSlicingComponent)Slicing.DeepCopy();
					if (Definition != null) dest.Definition = (Hl7.Fhir.Model.Profile.ElementDefinitionComponent)Definition.DeepCopy();
					return dest;
				}
				else
					throw Error.Argument(nameof(other), "Can only copy to an object of the same type");
			}

			public override IDeepCopyable DeepCopy()
			{
				return CopyTo(new ElementComponent());
			}

			public override bool Matches(IDeepComparable other)
			{
				var otherT = other as ElementComponent;
				if (otherT is null) return false;

				if (!base.Matches(otherT)) return false;
				if (!DeepComparable.Matches(PathElement, otherT.PathElement)) return false;
				if (!DeepComparable.Matches(RepresentationElement, otherT.RepresentationElement)) return false;
				if (!DeepComparable.Matches(NameElement, otherT.NameElement)) return false;
				if (!DeepComparable.Matches(Slicing, otherT.Slicing)) return false;
				if (!DeepComparable.Matches(Definition, otherT.Definition)) return false;

				return true;
			}

			public override bool IsExactly(IDeepComparable other)
			{
				var otherT = other as ElementComponent;
				if (otherT is null) return false;

				if (!base.IsExactly(otherT)) return false;
				if (!DeepComparable.IsExactly(PathElement, otherT.PathElement)) return false;
				if (!DeepComparable.IsExactly(RepresentationElement, otherT.RepresentationElement)) return false;
				if (!DeepComparable.IsExactly(NameElement, otherT.NameElement)) return false;
				if (!DeepComparable.IsExactly(Slicing, otherT.Slicing)) return false;
				if (!DeepComparable.IsExactly(Definition, otherT.Definition)) return false;

				return true;
			}

		}


		[FhirType("ProfileExtensionDefnComponent")]
		[DataContract]
		public partial class ProfileExtensionDefnComponent : Hl7.Fhir.Model.Element
		{
			/// <summary>
			/// Identifies the extension in this profile
			/// </summary>
			[FhirElement("code", InSummary = true, Order = 40)]
			[Cardinality(Min = 1, Max = 1)]
			[DataMember]
			public Hl7.Fhir.Model.Code CodeElement { get; set; }

			/// <summary>
			/// Identifies the extension in this profile
			/// </summary>
			/// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
			[NotMapped]
			[IgnoreDataMember]
			public string Code
			{
				get { return CodeElement != null ? CodeElement.Value : null; }
				set
				{
					if (value is null)
						CodeElement = null;
					else
						CodeElement = new Hl7.Fhir.Model.Code(value);
				}
			}

			/// <summary>
			/// Use this name when displaying the value
			/// </summary>
			[FhirElement("display", InSummary = true, Order = 50)]
			[DataMember]
			public Hl7.Fhir.Model.FhirString DisplayElement { get; set; }

			/// <summary>
			/// Use this name when displaying the value
			/// </summary>
			/// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
			[NotMapped]
			[IgnoreDataMember]
			public string Display
			{
				get { return DisplayElement != null ? DisplayElement.Value : null; }
				set
				{
					if (value is null)
						DisplayElement = null;
					else
						DisplayElement = new Hl7.Fhir.Model.FhirString(value);
				}
			}

			/// <summary>
			/// resource | datatype | mapping | extension
			/// </summary>
			[FhirElement("contextType", InSummary = true, Order = 60)]
			[Cardinality(Min = 1, Max = 1)]
			[DataMember]
			public Code<Hl7.Fhir.Model.Profile.ExtensionContext> ContextTypeElement { get; set; }

			/// <summary>
			/// resource | datatype | mapping | extension
			/// </summary>
			/// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
			[NotMapped]
			[IgnoreDataMember]
			public Hl7.Fhir.Model.Profile.ExtensionContext? ContextType
			{
				get { return ContextTypeElement != null ? ContextTypeElement.Value : null; }
				set
				{
					if (value is null)
						ContextTypeElement = null;
					else
						ContextTypeElement = new Code<Hl7.Fhir.Model.Profile.ExtensionContext>(value);
				}
			}

			/// <summary>
			/// Where the extension can be used in instances
			/// </summary>
			[FhirElement("context", InSummary = true, Order = 70)]
			[Cardinality(Min = 1, Max = -1)]
			[DataMember]
			public List<Hl7.Fhir.Model.FhirString> ContextElement { get; set; }

			/// <summary>
			/// Where the extension can be used in instances
			/// </summary>
			/// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
			[NotMapped]
			[IgnoreDataMember]
			public IEnumerable<string> Context
			{
				get { return ContextElement != null ? ContextElement.Select(elem => elem.Value) : null; }
				set
				{
					if (value is null)
						ContextElement = null;
					else
						ContextElement = new List<Hl7.Fhir.Model.FhirString>(value.Select(elem => new Hl7.Fhir.Model.FhirString(elem)));
				}
			}

			/// <summary>
			/// Definition of the extension and its content
			/// </summary>
			[FhirElement("definition", InSummary = true, Order = 80)]
			[Cardinality(Min = 1, Max = 1)]
			[DataMember]
			public Hl7.Fhir.Model.Profile.ElementDefinitionComponent Definition { get; set; }

			public override IDeepCopyable CopyTo(IDeepCopyable other)
			{
				var dest = other as ProfileExtensionDefnComponent;

				if (dest != null)
				{
					base.CopyTo(dest);
					if (CodeElement != null) dest.CodeElement = (Hl7.Fhir.Model.Code)CodeElement.DeepCopy();
					if (DisplayElement != null) dest.DisplayElement = (Hl7.Fhir.Model.FhirString)DisplayElement.DeepCopy();
					if (ContextTypeElement != null) dest.ContextTypeElement = (Code<Hl7.Fhir.Model.Profile.ExtensionContext>)ContextTypeElement.DeepCopy();
					if (ContextElement != null) dest.ContextElement = new List<Hl7.Fhir.Model.FhirString>(ContextElement.DeepCopy());
					if (Definition != null) dest.Definition = (Hl7.Fhir.Model.Profile.ElementDefinitionComponent)Definition.DeepCopy();
					return dest;
				}
				else
					throw Error.Argument(nameof(other), "Can only copy to an object of the same type");
			}

			public override IDeepCopyable DeepCopy()
			{
				return CopyTo(new ProfileExtensionDefnComponent());
			}

			public override bool Matches(IDeepComparable other)
			{
				var otherT = other as ProfileExtensionDefnComponent;
				if (otherT is null) return false;

				if (!base.Matches(otherT)) return false;
				if (!DeepComparable.Matches(CodeElement, otherT.CodeElement)) return false;
				if (!DeepComparable.Matches(DisplayElement, otherT.DisplayElement)) return false;
				if (!DeepComparable.Matches(ContextTypeElement, otherT.ContextTypeElement)) return false;
				if (!DeepComparable.Matches(ContextElement, otherT.ContextElement)) return false;
				if (!DeepComparable.Matches(Definition, otherT.Definition)) return false;

				return true;
			}

			public override bool IsExactly(IDeepComparable other)
			{
				var otherT = other as ProfileExtensionDefnComponent;
				if (otherT is null) return false;

				if (!base.IsExactly(otherT)) return false;
				if (!DeepComparable.IsExactly(CodeElement, otherT.CodeElement)) return false;
				if (!DeepComparable.IsExactly(DisplayElement, otherT.DisplayElement)) return false;
				if (!DeepComparable.IsExactly(ContextTypeElement, otherT.ContextTypeElement)) return false;
				if (!DeepComparable.IsExactly(ContextElement, otherT.ContextElement)) return false;
				if (!DeepComparable.IsExactly(Definition, otherT.Definition)) return false;

				return true;
			}

		}


		/// <summary>
		/// Logical id to reference this profile
		/// </summary>
		[FhirElement("identifier", InSummary = true, Order = 70)]
		[DataMember]
		public Hl7.Fhir.Model.FhirString IdentifierElement { get; set; }

		/// <summary>
		/// Logical id to reference this profile
		/// </summary>
		/// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
		[NotMapped]
		[IgnoreDataMember]
		public string Identifier
		{
			get { return IdentifierElement != null ? IdentifierElement.Value : null; }
			set
			{
				if (value is null)
					IdentifierElement = null;
				else
					IdentifierElement = new Hl7.Fhir.Model.FhirString(value);
			}
		}

		/// <summary>
		/// Logical id for this version of the profile
		/// </summary>
		[FhirElement("version", InSummary = true, Order = 80)]
		[DataMember]
		public Hl7.Fhir.Model.FhirString VersionElement { get; set; }

		/// <summary>
		/// Logical id for this version of the profile
		/// </summary>
		/// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
		[NotMapped]
		[IgnoreDataMember]
		public string Version
		{
			get { return VersionElement != null ? VersionElement.Value : null; }
			set
			{
				if (value is null)
					VersionElement = null;
				else
					VersionElement = new Hl7.Fhir.Model.FhirString(value);
			}
		}

		/// <summary>
		/// Informal name for this profile
		/// </summary>
		[FhirElement("name", InSummary = true, Order = 90)]
		[Cardinality(Min = 1, Max = 1)]
		[DataMember]
		public Hl7.Fhir.Model.FhirString NameElement { get; set; }

		/// <summary>
		/// Informal name for this profile
		/// </summary>
		/// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
		[NotMapped]
		[IgnoreDataMember]
		public string Name
		{
			get { return NameElement != null ? NameElement.Value : null; }
			set
			{
				if (value is null)
					NameElement = null;
				else
					NameElement = new Hl7.Fhir.Model.FhirString(value);
			}
		}

		/// <summary>
		/// Name of the publisher (Organization or individual)
		/// </summary>
		[FhirElement("publisher", InSummary = true, Order = 100)]
		[DataMember]
		public Hl7.Fhir.Model.FhirString PublisherElement { get; set; }

		/// <summary>
		/// Name of the publisher (Organization or individual)
		/// </summary>
		/// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
		[NotMapped]
		[IgnoreDataMember]
		public string Publisher
		{
			get { return PublisherElement != null ? PublisherElement.Value : null; }
			set
			{
				if (value is null)
					PublisherElement = null;
				else
					PublisherElement = new Hl7.Fhir.Model.FhirString(value);
			}
		}

		/// <summary>
		/// Contact information of the publisher
		/// </summary>
		[FhirElement("telecom", InSummary = true, Order = 110)]
		[Cardinality(Min = 0, Max = -1)]
		[DataMember]
		public List<Hl7.Fhir.Model.Contact> Telecom { get; set; }

		/// <summary>
		/// Natural language description of the profile
		/// </summary>
		[FhirElement("description", InSummary = true, Order = 120)]
		[DataMember]
		public Hl7.Fhir.Model.FhirString DescriptionElement { get; set; }

		/// <summary>
		/// Natural language description of the profile
		/// </summary>
		/// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
		[NotMapped]
		[IgnoreDataMember]
		public string Description
		{
			get { return DescriptionElement != null ? DescriptionElement.Value : null; }
			set
			{
				if (value is null)
					DescriptionElement = null;
				else
					DescriptionElement = new Hl7.Fhir.Model.FhirString(value);
			}
		}

		/// <summary>
		/// Assist with indexing and finding
		/// </summary>
		[FhirElement("code", InSummary = true, Order = 130)]
		[Cardinality(Min = 0, Max = -1)]
		[DataMember]
		public List<Hl7.Fhir.Model.Coding> Code { get; set; }

		/// <summary>
		/// draft | active | retired
		/// </summary>
		[FhirElement("status", InSummary = true, Order = 140)]
		[Cardinality(Min = 1, Max = 1)]
		[DataMember]
		public Code<Hl7.Fhir.Model.Profile.ResourceProfileStatus> StatusElement { get; set; }

		/// <summary>
		/// draft | active | retired
		/// </summary>
		/// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
		[NotMapped]
		[IgnoreDataMember]
		public Hl7.Fhir.Model.Profile.ResourceProfileStatus? Status
		{
			get { return StatusElement != null ? StatusElement.Value : null; }
			set
			{
				if (value is null)
					StatusElement = null;
				else
					StatusElement = new Code<Hl7.Fhir.Model.Profile.ResourceProfileStatus>(value);
			}
		}

		/// <summary>
		/// If for testing purposes, not real usage
		/// </summary>
		[FhirElement("experimental", InSummary = true, Order = 150)]
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
				if (value is null)
					ExperimentalElement = null;
				else
					ExperimentalElement = new Hl7.Fhir.Model.FhirBoolean(value);
			}
		}

		/// <summary>
		/// Date for this version of the profile
		/// </summary>
		[FhirElement("date", InSummary = true, Order = 160)]
		[DataMember]
		public Hl7.Fhir.Model.FhirDateTime DateElement { get; set; }

		/// <summary>
		/// Date for this version of the profile
		/// </summary>
		/// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
		[NotMapped]
		[IgnoreDataMember]
		public string Date
		{
			get { return DateElement != null ? DateElement.Value : null; }
			set
			{
				if (value is null)
					DateElement = null;
				else
					DateElement = new Hl7.Fhir.Model.FhirDateTime(value);
			}
		}

		/// <summary>
		/// Scope and Usage this profile is for
		/// </summary>
		[FhirElement("requirements", Order = 170)]
		[DataMember]
		public Hl7.Fhir.Model.FhirString RequirementsElement { get; set; }

		/// <summary>
		/// Scope and Usage this profile is for
		/// </summary>
		/// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
		[NotMapped]
		[IgnoreDataMember]
		public string Requirements
		{
			get { return RequirementsElement != null ? RequirementsElement.Value : null; }
			set
			{
				if (value is null)
					RequirementsElement = null;
				else
					RequirementsElement = new Hl7.Fhir.Model.FhirString(value);
			}
		}

		/// <summary>
		/// FHIR Version this profile targets
		/// </summary>
		[FhirElement("fhirVersion", InSummary = true, Order = 180)]
		[DataMember]
		public Hl7.Fhir.Model.Id FhirVersionElement { get; set; }

		/// <summary>
		/// FHIR Version this profile targets
		/// </summary>
		/// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
		[NotMapped]
		[IgnoreDataMember]
		public string FhirVersion
		{
			get { return FhirVersionElement != null ? FhirVersionElement.Value : null; }
			set
			{
				if (value is null)
					FhirVersionElement = null;
				else
					FhirVersionElement = new Hl7.Fhir.Model.Id(value);
			}
		}

		/// <summary>
		/// External specification that the content is mapped to
		/// </summary>
		[FhirElement("mapping", Order = 190)]
		[Cardinality(Min = 0, Max = -1)]
		[DataMember]
		public List<Hl7.Fhir.Model.Profile.ProfileMappingComponent> Mapping { get; set; }

		/// <summary>
		/// A constraint on a resource or a data type
		/// </summary>
		[FhirElement("structure", Order = 200)]
		[Cardinality(Min = 0, Max = -1)]
		[DataMember]
		public List<Hl7.Fhir.Model.Profile.ProfileStructureComponent> Structure { get; set; }

		/// <summary>
		/// Definition of an extension
		/// </summary>
		[FhirElement("extensionDefn", Order = 210)]
		[Cardinality(Min = 0, Max = -1)]
		[DataMember]
		public List<Hl7.Fhir.Model.Profile.ProfileExtensionDefnComponent> ExtensionDefn { get; set; }

		/// <summary>
		/// Definition of a named query
		/// </summary>
		[FhirElement("query", Order = 220)]
		[Cardinality(Min = 0, Max = -1)]
		[DataMember]
		public List<Hl7.Fhir.Model.Profile.ProfileQueryComponent> Query { get; set; }

		public override IDeepCopyable CopyTo(IDeepCopyable other)
		{
			var dest = other as Profile;

			if (dest != null)
			{
				base.CopyTo(dest);
				if (IdentifierElement != null) dest.IdentifierElement = (Hl7.Fhir.Model.FhirString)IdentifierElement.DeepCopy();
				if (VersionElement != null) dest.VersionElement = (Hl7.Fhir.Model.FhirString)VersionElement.DeepCopy();
				if (NameElement != null) dest.NameElement = (Hl7.Fhir.Model.FhirString)NameElement.DeepCopy();
				if (PublisherElement != null) dest.PublisherElement = (Hl7.Fhir.Model.FhirString)PublisherElement.DeepCopy();
				if (Telecom != null) dest.Telecom = new List<Hl7.Fhir.Model.Contact>(Telecom.DeepCopy());
				if (DescriptionElement != null) dest.DescriptionElement = (Hl7.Fhir.Model.FhirString)DescriptionElement.DeepCopy();
				if (Code != null) dest.Code = new List<Hl7.Fhir.Model.Coding>(Code.DeepCopy());
				if (StatusElement != null) dest.StatusElement = (Code<Hl7.Fhir.Model.Profile.ResourceProfileStatus>)StatusElement.DeepCopy();
				if (ExperimentalElement != null) dest.ExperimentalElement = (Hl7.Fhir.Model.FhirBoolean)ExperimentalElement.DeepCopy();
				if (DateElement != null) dest.DateElement = (Hl7.Fhir.Model.FhirDateTime)DateElement.DeepCopy();
				if (RequirementsElement != null) dest.RequirementsElement = (Hl7.Fhir.Model.FhirString)RequirementsElement.DeepCopy();
				if (FhirVersionElement != null) dest.FhirVersionElement = (Hl7.Fhir.Model.Id)FhirVersionElement.DeepCopy();
				if (Mapping != null) dest.Mapping = new List<Hl7.Fhir.Model.Profile.ProfileMappingComponent>(Mapping.DeepCopy());
				if (Structure != null) dest.Structure = new List<Hl7.Fhir.Model.Profile.ProfileStructureComponent>(Structure.DeepCopy());
				if (ExtensionDefn != null) dest.ExtensionDefn = new List<Hl7.Fhir.Model.Profile.ProfileExtensionDefnComponent>(ExtensionDefn.DeepCopy());
				if (Query != null) dest.Query = new List<Hl7.Fhir.Model.Profile.ProfileQueryComponent>(Query.DeepCopy());
				return dest;
			}
			else
				throw Error.Argument(nameof(other), "Can only copy to an object of the same type");
		}

		public override IDeepCopyable DeepCopy()
		{
			return CopyTo(new Profile());
		}

		public override bool Matches(IDeepComparable other)
		{
			var otherT = other as Profile;
			if (otherT is null) return false;

			if (!base.Matches(otherT)) return false;
			if (!DeepComparable.Matches(IdentifierElement, otherT.IdentifierElement)) return false;
			if (!DeepComparable.Matches(VersionElement, otherT.VersionElement)) return false;
			if (!DeepComparable.Matches(NameElement, otherT.NameElement)) return false;
			if (!DeepComparable.Matches(PublisherElement, otherT.PublisherElement)) return false;
			if (!DeepComparable.Matches(Telecom, otherT.Telecom)) return false;
			if (!DeepComparable.Matches(DescriptionElement, otherT.DescriptionElement)) return false;
			if (!DeepComparable.Matches(Code, otherT.Code)) return false;
			if (!DeepComparable.Matches(StatusElement, otherT.StatusElement)) return false;
			if (!DeepComparable.Matches(ExperimentalElement, otherT.ExperimentalElement)) return false;
			if (!DeepComparable.Matches(DateElement, otherT.DateElement)) return false;
			if (!DeepComparable.Matches(RequirementsElement, otherT.RequirementsElement)) return false;
			if (!DeepComparable.Matches(FhirVersionElement, otherT.FhirVersionElement)) return false;
			if (!DeepComparable.Matches(Mapping, otherT.Mapping)) return false;
			if (!DeepComparable.Matches(Structure, otherT.Structure)) return false;
			if (!DeepComparable.Matches(ExtensionDefn, otherT.ExtensionDefn)) return false;
			if (!DeepComparable.Matches(Query, otherT.Query)) return false;

			return true;
		}

		public override bool IsExactly(IDeepComparable other)
		{
			var otherT = other as Profile;
			if (otherT is null) return false;

			if (!base.IsExactly(otherT)) return false;
			if (!DeepComparable.IsExactly(IdentifierElement, otherT.IdentifierElement)) return false;
			if (!DeepComparable.IsExactly(VersionElement, otherT.VersionElement)) return false;
			if (!DeepComparable.IsExactly(NameElement, otherT.NameElement)) return false;
			if (!DeepComparable.IsExactly(PublisherElement, otherT.PublisherElement)) return false;
			if (!DeepComparable.IsExactly(Telecom, otherT.Telecom)) return false;
			if (!DeepComparable.IsExactly(DescriptionElement, otherT.DescriptionElement)) return false;
			if (!DeepComparable.IsExactly(Code, otherT.Code)) return false;
			if (!DeepComparable.IsExactly(StatusElement, otherT.StatusElement)) return false;
			if (!DeepComparable.IsExactly(ExperimentalElement, otherT.ExperimentalElement)) return false;
			if (!DeepComparable.IsExactly(DateElement, otherT.DateElement)) return false;
			if (!DeepComparable.IsExactly(RequirementsElement, otherT.RequirementsElement)) return false;
			if (!DeepComparable.IsExactly(FhirVersionElement, otherT.FhirVersionElement)) return false;
			if (!DeepComparable.IsExactly(Mapping, otherT.Mapping)) return false;
			if (!DeepComparable.IsExactly(Structure, otherT.Structure)) return false;
			if (!DeepComparable.IsExactly(ExtensionDefn, otherT.ExtensionDefn)) return false;
			if (!DeepComparable.IsExactly(Query, otherT.Query)) return false;

			return true;
		}

	}

}
