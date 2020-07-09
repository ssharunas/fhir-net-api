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
	/// A statement of relationships from one set of concepts to one or more other concept systems
	/// </summary>
	[FhirType("ConceptMap", IsResource = true)]
	[DataContract]
	public partial class ConceptMap : Hl7.Fhir.Model.Resource
	{
		/// <summary>
		/// The degree of equivalence between concepts
		/// </summary>
		[FhirEnumeration("ConceptMapEquivalence")]
		public enum ConceptMapEquivalence
		{
			/// <summary>
			/// The definitions of the concepts are exactly the same (i.e. only grammatical differences) and structural implications of meaning are identifical or irrelevant (i.e. intensionally identical).
			/// </summary>
			[EnumLiteral("equal")]
			Equal,
			/// <summary>
			/// The definitions of the concepts mean the same thing (including when structural implications of meaning are considered) (i.e. extensionally identical).
			/// </summary>
			[EnumLiteral("equivalent")]
			Equivalent,
			/// <summary>
			/// The target mapping is wider in meaning than the source concept.
			/// </summary>
			[EnumLiteral("wider")]
			Wider,
			/// <summary>
			/// The target mapping subsumes the meaning of the source concept (e.g. the source is-a target).
			/// </summary>
			[EnumLiteral("subsumes")]
			Subsumes,
			/// <summary>
			/// The target mapping is narrower in meaning that the source concept. The sense in which the mapping is narrower SHALL be described in the comments in this case, and applications should be careful when atempting to use these mappings operationally.
			/// </summary>
			[EnumLiteral("narrower")]
			Narrower,
			/// <summary>
			/// The target mapping specialises the meaning of the source concept (e.g. the target is-a source).
			/// </summary>
			[EnumLiteral("specialises")]
			Specialises,
			/// <summary>
			/// The target mapping overlaps with the source concept, but both source and target cover additional meaning. The sense in which the mapping is narrower SHALL be described in the comments in this case, and applications should be careful when atempting to use these mappings operationally.
			/// </summary>
			[EnumLiteral("inexact")]
			Inexact,
			/// <summary>
			/// There is no match for this concept in the destination concept system.
			/// </summary>
			[EnumLiteral("unmatched")]
			Unmatched,
			/// <summary>
			/// This is an explicit assertion that there is no mapping between the source and target concept.
			/// </summary>
			[EnumLiteral("disjoint")]
			Disjoint,
		}

		[FhirType("ConceptMapConceptMapComponent")]
		[DataContract]
		public partial class ConceptMapConceptMapComponent : Hl7.Fhir.Model.Element
		{
			/// <summary>
			/// System of the target
			/// </summary>
			[FhirElement("system", InSummary = true, Order = 40)]
			[DataMember]
			public Hl7.Fhir.Model.FhirUri SystemElement { get; set; }

			/// <summary>
			/// System of the target
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
			/// Code that identifies the target concept
			/// </summary>
			[FhirElement("code", InSummary = true, Order = 50)]
			[DataMember]
			public Hl7.Fhir.Model.Code CodeElement { get; set; }

			/// <summary>
			/// Code that identifies the target concept
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

			/// <summary>
			/// equal | equivalent | wider | subsumes | narrower | specialises | inexact | unmatched | disjoint
			/// </summary>
			[FhirElement("equivalence", InSummary = true, Order = 60)]
			[Cardinality(Min = 1, Max = 1)]
			[DataMember]
			public Code<Hl7.Fhir.Model.ConceptMap.ConceptMapEquivalence> EquivalenceElement { get; set; }

			/// <summary>
			/// equal | equivalent | wider | subsumes | narrower | specialises | inexact | unmatched | disjoint
			/// </summary>
			/// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
			[NotMapped]
			[IgnoreDataMember]
			public Hl7.Fhir.Model.ConceptMap.ConceptMapEquivalence? Equivalence
			{
				get { return EquivalenceElement?.Value; }
				set
				{
					if (value is null)
						EquivalenceElement = null;
					else
						EquivalenceElement = new Code<Hl7.Fhir.Model.ConceptMap.ConceptMapEquivalence>(value);
				}
			}

			/// <summary>
			/// Description of status/issues in mapping
			/// </summary>
			[FhirElement("comments", InSummary = true, Order = 70)]
			[DataMember]
			public Hl7.Fhir.Model.FhirString CommentsElement { get; set; }

			/// <summary>
			/// Description of status/issues in mapping
			/// </summary>
			/// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
			[NotMapped]
			[IgnoreDataMember]
			public string Comments
			{
				get { return CommentsElement?.Value; }
				set
				{
					if (value is null)
						CommentsElement = null;
					else
						CommentsElement = new Hl7.Fhir.Model.FhirString(value);
				}
			}

			/// <summary>
			/// Other concepts that this mapping also produces
			/// </summary>
			[FhirElement("product", InSummary = true, Order = 80)]
			[Cardinality(Min = 0, Max = -1)]
			[DataMember]
			public List<Hl7.Fhir.Model.ConceptMap.OtherConceptComponent> Product { get; set; }

			public override IDeepCopyable CopyTo(IDeepCopyable other)
			{
				var dest = other as ConceptMapConceptMapComponent;

				if (dest != null)
				{
					base.CopyTo(dest);
					if (SystemElement != null) dest.SystemElement = (Hl7.Fhir.Model.FhirUri)SystemElement.DeepCopy();
					if (CodeElement != null) dest.CodeElement = (Hl7.Fhir.Model.Code)CodeElement.DeepCopy();
					if (EquivalenceElement != null) dest.EquivalenceElement = (Code<Hl7.Fhir.Model.ConceptMap.ConceptMapEquivalence>)EquivalenceElement.DeepCopy();
					if (CommentsElement != null) dest.CommentsElement = (Hl7.Fhir.Model.FhirString)CommentsElement.DeepCopy();
					if (Product != null) dest.Product = new List<Hl7.Fhir.Model.ConceptMap.OtherConceptComponent>(Product.DeepCopy());
					return dest;
				}
				else
					throw Error.Argument(nameof(other), "Can only copy to an object of the same type");
			}

			public override IDeepCopyable DeepCopy()
			{
				return CopyTo(new ConceptMapConceptMapComponent());
			}

			public override bool Matches(IDeepComparable other)
			{
				var otherT = other as ConceptMapConceptMapComponent;
				if (otherT is null) return false;

				if (!base.Matches(otherT)) return false;
				if (!DeepComparable.Matches(SystemElement, otherT.SystemElement)) return false;
				if (!DeepComparable.Matches(CodeElement, otherT.CodeElement)) return false;
				if (!DeepComparable.Matches(EquivalenceElement, otherT.EquivalenceElement)) return false;
				if (!DeepComparable.Matches(CommentsElement, otherT.CommentsElement)) return false;
				if (!DeepComparable.Matches(Product, otherT.Product)) return false;

				return true;
			}

			public override bool IsExactly(IDeepComparable other)
			{
				var otherT = other as ConceptMapConceptMapComponent;
				if (otherT is null) return false;

				if (!base.IsExactly(otherT)) return false;
				if (!DeepComparable.IsExactly(SystemElement, otherT.SystemElement)) return false;
				if (!DeepComparable.IsExactly(CodeElement, otherT.CodeElement)) return false;
				if (!DeepComparable.IsExactly(EquivalenceElement, otherT.EquivalenceElement)) return false;
				if (!DeepComparable.IsExactly(CommentsElement, otherT.CommentsElement)) return false;
				if (!DeepComparable.IsExactly(Product, otherT.Product)) return false;

				return true;
			}

		}


		[FhirType("OtherConceptComponent")]
		[DataContract]
		public partial class OtherConceptComponent : Hl7.Fhir.Model.Element
		{
			/// <summary>
			/// Reference to element/field/valueset provides the context
			/// </summary>
			[FhirElement("concept", InSummary = true, Order = 40)]
			[Cardinality(Min = 1, Max = 1)]
			[DataMember]
			public Hl7.Fhir.Model.FhirUri ConceptElement { get; set; }

			/// <summary>
			/// Reference to element/field/valueset provides the context
			/// </summary>
			/// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
			[NotMapped]
			[IgnoreDataMember]
			public string Concept
			{
				get { return ConceptElement?.Value; }
				set
				{
					if (value is null)
						ConceptElement = null;
					else
						ConceptElement = new Hl7.Fhir.Model.FhirUri(value);
				}
			}

			/// <summary>
			/// System for a concept in the referenced concept
			/// </summary>
			[FhirElement("system", InSummary = true, Order = 50)]
			[Cardinality(Min = 1, Max = 1)]
			[DataMember]
			public Hl7.Fhir.Model.FhirUri SystemElement { get; set; }

			/// <summary>
			/// System for a concept in the referenced concept
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
			/// Code for a concept in the referenced concept
			/// </summary>
			[FhirElement("code", InSummary = true, Order = 60)]
			[Cardinality(Min = 1, Max = 1)]
			[DataMember]
			public Hl7.Fhir.Model.Code CodeElement { get; set; }

			/// <summary>
			/// Code for a concept in the referenced concept
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
				var dest = other as OtherConceptComponent;

				if (dest != null)
				{
					base.CopyTo(dest);
					if (ConceptElement != null) dest.ConceptElement = (Hl7.Fhir.Model.FhirUri)ConceptElement.DeepCopy();
					if (SystemElement != null) dest.SystemElement = (Hl7.Fhir.Model.FhirUri)SystemElement.DeepCopy();
					if (CodeElement != null) dest.CodeElement = (Hl7.Fhir.Model.Code)CodeElement.DeepCopy();
					return dest;
				}
				else
					throw Error.Argument(nameof(other), "Can only copy to an object of the same type");
			}

			public override IDeepCopyable DeepCopy()
			{
				return CopyTo(new OtherConceptComponent());
			}

			public override bool Matches(IDeepComparable other)
			{
				var otherT = other as OtherConceptComponent;
				if (otherT is null) return false;

				if (!base.Matches(otherT)) return false;
				if (!DeepComparable.Matches(ConceptElement, otherT.ConceptElement)) return false;
				if (!DeepComparable.Matches(SystemElement, otherT.SystemElement)) return false;
				if (!DeepComparable.Matches(CodeElement, otherT.CodeElement)) return false;

				return true;
			}

			public override bool IsExactly(IDeepComparable other)
			{
				var otherT = other as OtherConceptComponent;
				if (otherT is null) return false;

				if (!base.IsExactly(otherT)) return false;
				if (!DeepComparable.IsExactly(ConceptElement, otherT.ConceptElement)) return false;
				if (!DeepComparable.IsExactly(SystemElement, otherT.SystemElement)) return false;
				if (!DeepComparable.IsExactly(CodeElement, otherT.CodeElement)) return false;

				return true;
			}

		}


		[FhirType("ConceptMapConceptComponent")]
		[DataContract]
		public partial class ConceptMapConceptComponent : Hl7.Fhir.Model.Element
		{
			/// <summary>
			/// System that defines the concept being mapped
			/// </summary>
			[FhirElement("system", InSummary = true, Order = 40)]
			[Cardinality(Min = 1, Max = 1)]
			[DataMember]
			public Hl7.Fhir.Model.FhirUri SystemElement { get; set; }

			/// <summary>
			/// System that defines the concept being mapped
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
			/// Identifies concept being mapped
			/// </summary>
			[FhirElement("code", InSummary = true, Order = 50)]
			[DataMember]
			public Hl7.Fhir.Model.Code CodeElement { get; set; }

			/// <summary>
			/// Identifies concept being mapped
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

			/// <summary>
			/// Other concepts required for this mapping (from context)
			/// </summary>
			[FhirElement("dependsOn", InSummary = true, Order = 60)]
			[Cardinality(Min = 0, Max = -1)]
			[DataMember]
			public List<Hl7.Fhir.Model.ConceptMap.OtherConceptComponent> DependsOn { get; set; }

			/// <summary>
			/// A concept from the target value set that this concept maps to
			/// </summary>
			[FhirElement("map", InSummary = true, Order = 70)]
			[Cardinality(Min = 0, Max = -1)]
			[DataMember]
			public List<Hl7.Fhir.Model.ConceptMap.ConceptMapConceptMapComponent> Map { get; set; }

			public override IDeepCopyable CopyTo(IDeepCopyable other)
			{
				var dest = other as ConceptMapConceptComponent;

				if (dest != null)
				{
					base.CopyTo(dest);
					if (SystemElement != null) dest.SystemElement = (Hl7.Fhir.Model.FhirUri)SystemElement.DeepCopy();
					if (CodeElement != null) dest.CodeElement = (Hl7.Fhir.Model.Code)CodeElement.DeepCopy();
					if (DependsOn != null) dest.DependsOn = new List<Hl7.Fhir.Model.ConceptMap.OtherConceptComponent>(DependsOn.DeepCopy());
					if (Map != null) dest.Map = new List<Hl7.Fhir.Model.ConceptMap.ConceptMapConceptMapComponent>(Map.DeepCopy());
					return dest;
				}
				else
					throw Error.Argument(nameof(other), "Can only copy to an object of the same type");
			}

			public override IDeepCopyable DeepCopy()
			{
				return CopyTo(new ConceptMapConceptComponent());
			}

			public override bool Matches(IDeepComparable other)
			{
				var otherT = other as ConceptMapConceptComponent;
				if (otherT is null) return false;

				if (!base.Matches(otherT)) return false;
				if (!DeepComparable.Matches(SystemElement, otherT.SystemElement)) return false;
				if (!DeepComparable.Matches(CodeElement, otherT.CodeElement)) return false;
				if (!DeepComparable.Matches(DependsOn, otherT.DependsOn)) return false;
				if (!DeepComparable.Matches(Map, otherT.Map)) return false;

				return true;
			}

			public override bool IsExactly(IDeepComparable other)
			{
				var otherT = other as ConceptMapConceptComponent;
				if (otherT is null) return false;

				if (!base.IsExactly(otherT)) return false;
				if (!DeepComparable.IsExactly(SystemElement, otherT.SystemElement)) return false;
				if (!DeepComparable.IsExactly(CodeElement, otherT.CodeElement)) return false;
				if (!DeepComparable.IsExactly(DependsOn, otherT.DependsOn)) return false;
				if (!DeepComparable.IsExactly(Map, otherT.Map)) return false;

				return true;
			}

		}


		/// <summary>
		/// Logical id to reference this concept map
		/// </summary>
		[FhirElement("identifier", InSummary = true, Order = 70)]
		[DataMember]
		public Hl7.Fhir.Model.FhirString IdentifierElement { get; set; }

		/// <summary>
		/// Logical id to reference this concept map
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
					IdentifierElement = new Hl7.Fhir.Model.FhirString(value);
			}
		}

		/// <summary>
		/// Logical id for this version of the concept map
		/// </summary>
		[FhirElement("version", InSummary = true, Order = 80)]
		[DataMember]
		public Hl7.Fhir.Model.FhirString VersionElement { get; set; }

		/// <summary>
		/// Logical id for this version of the concept map
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
		/// Informal name for this concept map
		/// </summary>
		[FhirElement("name", InSummary = true, Order = 90)]
		[Cardinality(Min = 1, Max = 1)]
		[DataMember]
		public Hl7.Fhir.Model.FhirString NameElement { get; set; }

		/// <summary>
		/// Informal name for this concept map
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
			get { return PublisherElement?.Value; }
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
		/// Human language description of the concept map
		/// </summary>
		[FhirElement("description", InSummary = true, Order = 120)]
		[DataMember]
		public Hl7.Fhir.Model.FhirString DescriptionElement { get; set; }

		/// <summary>
		/// Human language description of the concept map
		/// </summary>
		/// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
		[NotMapped]
		[IgnoreDataMember]
		public string Description
		{
			get { return DescriptionElement?.Value; }
			set
			{
				if (value is null)
					DescriptionElement = null;
				else
					DescriptionElement = new Hl7.Fhir.Model.FhirString(value);
			}
		}

		/// <summary>
		/// About the concept map or its content
		/// </summary>
		[FhirElement("copyright", Order = 130)]
		[DataMember]
		public Hl7.Fhir.Model.FhirString CopyrightElement { get; set; }

		/// <summary>
		/// About the concept map or its content
		/// </summary>
		/// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
		[NotMapped]
		[IgnoreDataMember]
		public string Copyright
		{
			get { return CopyrightElement?.Value; }
			set
			{
				if (value is null)
					CopyrightElement = null;
				else
					CopyrightElement = new Hl7.Fhir.Model.FhirString(value);
			}
		}

		/// <summary>
		/// draft | active | retired
		/// </summary>
		[FhirElement("status", InSummary = true, Order = 140)]
		[Cardinality(Min = 1, Max = 1)]
		[DataMember]
		public Hl7.Fhir.Model.Code StatusElement { get; set; }

		/// <summary>
		/// draft | active | retired
		/// </summary>
		/// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
		[NotMapped]
		[IgnoreDataMember]
		public string Status
		{
			get { return StatusElement?.Value; }
			set
			{
				if (value is null)
					StatusElement = null;
				else
					StatusElement = new Hl7.Fhir.Model.Code(value);
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
			get { return ExperimentalElement?.Value; }
			set
			{
				if (value is null)
					ExperimentalElement = null;
				else
					ExperimentalElement = new Hl7.Fhir.Model.FhirBoolean(value);
			}
		}

		/// <summary>
		/// Date for given status
		/// </summary>
		[FhirElement("date", InSummary = true, Order = 160)]
		[DataMember]
		public Hl7.Fhir.Model.FhirDateTime DateElement { get; set; }

		/// <summary>
		/// Date for given status
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
		/// Identifies the source value set which is being mapped
		/// </summary>
		[FhirElement("source", InSummary = true, Order = 170)]
		[References("ValueSet")]
		[Cardinality(Min = 1, Max = 1)]
		[DataMember]
		public Hl7.Fhir.Model.ResourceReference Source { get; set; }

		/// <summary>
		/// Provides context to the mappings
		/// </summary>
		[FhirElement("target", InSummary = true, Order = 180)]
		[References("ValueSet")]
		[Cardinality(Min = 1, Max = 1)]
		[DataMember]
		public Hl7.Fhir.Model.ResourceReference Target { get; set; }

		/// <summary>
		/// Mappings for a concept from the source valueset
		/// </summary>
		[FhirElement("concept", Order = 190)]
		[Cardinality(Min = 0, Max = -1)]
		[DataMember]
		public List<Hl7.Fhir.Model.ConceptMap.ConceptMapConceptComponent> Concept { get; set; }

		public override IDeepCopyable CopyTo(IDeepCopyable other)
		{
			var dest = other as ConceptMap;

			if (dest != null)
			{
				base.CopyTo(dest);
				if (IdentifierElement != null) dest.IdentifierElement = (Hl7.Fhir.Model.FhirString)IdentifierElement.DeepCopy();
				if (VersionElement != null) dest.VersionElement = (Hl7.Fhir.Model.FhirString)VersionElement.DeepCopy();
				if (NameElement != null) dest.NameElement = (Hl7.Fhir.Model.FhirString)NameElement.DeepCopy();
				if (PublisherElement != null) dest.PublisherElement = (Hl7.Fhir.Model.FhirString)PublisherElement.DeepCopy();
				if (Telecom != null) dest.Telecom = new List<Hl7.Fhir.Model.Contact>(Telecom.DeepCopy());
				if (DescriptionElement != null) dest.DescriptionElement = (Hl7.Fhir.Model.FhirString)DescriptionElement.DeepCopy();
				if (CopyrightElement != null) dest.CopyrightElement = (Hl7.Fhir.Model.FhirString)CopyrightElement.DeepCopy();
				if (StatusElement != null) dest.StatusElement = (Hl7.Fhir.Model.Code)StatusElement.DeepCopy();
				if (ExperimentalElement != null) dest.ExperimentalElement = (Hl7.Fhir.Model.FhirBoolean)ExperimentalElement.DeepCopy();
				if (DateElement != null) dest.DateElement = (Hl7.Fhir.Model.FhirDateTime)DateElement.DeepCopy();
				if (Source != null) dest.Source = (Hl7.Fhir.Model.ResourceReference)Source.DeepCopy();
				if (Target != null) dest.Target = (Hl7.Fhir.Model.ResourceReference)Target.DeepCopy();
				if (Concept != null) dest.Concept = new List<Hl7.Fhir.Model.ConceptMap.ConceptMapConceptComponent>(Concept.DeepCopy());
				return dest;
			}
			else
				throw Error.Argument(nameof(other), "Can only copy to an object of the same type");
		}

		public override IDeepCopyable DeepCopy()
		{
			return CopyTo(new ConceptMap());
		}

		public override bool Matches(IDeepComparable other)
		{
			var otherT = other as ConceptMap;
			if (otherT is null) return false;

			if (!base.Matches(otherT)) return false;
			if (!DeepComparable.Matches(IdentifierElement, otherT.IdentifierElement)) return false;
			if (!DeepComparable.Matches(VersionElement, otherT.VersionElement)) return false;
			if (!DeepComparable.Matches(NameElement, otherT.NameElement)) return false;
			if (!DeepComparable.Matches(PublisherElement, otherT.PublisherElement)) return false;
			if (!DeepComparable.Matches(Telecom, otherT.Telecom)) return false;
			if (!DeepComparable.Matches(DescriptionElement, otherT.DescriptionElement)) return false;
			if (!DeepComparable.Matches(CopyrightElement, otherT.CopyrightElement)) return false;
			if (!DeepComparable.Matches(StatusElement, otherT.StatusElement)) return false;
			if (!DeepComparable.Matches(ExperimentalElement, otherT.ExperimentalElement)) return false;
			if (!DeepComparable.Matches(DateElement, otherT.DateElement)) return false;
			if (!DeepComparable.Matches(Source, otherT.Source)) return false;
			if (!DeepComparable.Matches(Target, otherT.Target)) return false;
			if (!DeepComparable.Matches(Concept, otherT.Concept)) return false;

			return true;
		}

		public override bool IsExactly(IDeepComparable other)
		{
			var otherT = other as ConceptMap;
			if (otherT is null) return false;

			if (!base.IsExactly(otherT)) return false;
			if (!DeepComparable.IsExactly(IdentifierElement, otherT.IdentifierElement)) return false;
			if (!DeepComparable.IsExactly(VersionElement, otherT.VersionElement)) return false;
			if (!DeepComparable.IsExactly(NameElement, otherT.NameElement)) return false;
			if (!DeepComparable.IsExactly(PublisherElement, otherT.PublisherElement)) return false;
			if (!DeepComparable.IsExactly(Telecom, otherT.Telecom)) return false;
			if (!DeepComparable.IsExactly(DescriptionElement, otherT.DescriptionElement)) return false;
			if (!DeepComparable.IsExactly(CopyrightElement, otherT.CopyrightElement)) return false;
			if (!DeepComparable.IsExactly(StatusElement, otherT.StatusElement)) return false;
			if (!DeepComparable.IsExactly(ExperimentalElement, otherT.ExperimentalElement)) return false;
			if (!DeepComparable.IsExactly(DateElement, otherT.DateElement)) return false;
			if (!DeepComparable.IsExactly(Source, otherT.Source)) return false;
			if (!DeepComparable.IsExactly(Target, otherT.Target)) return false;
			if (!DeepComparable.IsExactly(Concept, otherT.Concept)) return false;

			return true;
		}

	}

}
