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
	/// A set of codes drawn from one or more code systems
	/// </summary>
	[FhirType("ValueSet", IsResource = true)]
	[DataContract]
	public partial class ValueSet : Hl7.Fhir.Model.Resource
	{
		/// <summary>
		/// The lifecycle status of a Value Set or Concept Map
		/// </summary>
		[FhirEnumeration("ValueSetStatus")]
		public enum ValueSetStatus
		{
			/// <summary>
			/// This valueset is still under development.
			/// </summary>
			[EnumLiteral("draft")]
			Draft,
			/// <summary>
			/// This valueset is ready for normal use.
			/// </summary>
			[EnumLiteral("active")]
			Active,
			/// <summary>
			/// This valueset has been withdrawn or superceded and should no longer be used.
			/// </summary>
			[EnumLiteral("retired")]
			Retired,
		}

		/// <summary>
		/// The kind of operation to perform as a part of a property based filter
		/// </summary>
		[FhirEnumeration("FilterOperator")]
		public enum FilterOperator
		{
			/// <summary>
			/// The property value has the concept specified by the value.
			/// </summary>
			[EnumLiteral("=")]
			Equal,
			/// <summary>
			/// The property value has a concept that has an is-a relationship with the value.
			/// </summary>
			[EnumLiteral("is-a")]
			IsA,
			/// <summary>
			/// The property value has a concept that does not have an is-a relationship with the value.
			/// </summary>
			[EnumLiteral("is-not-a")]
			IsNotA,
			/// <summary>
			/// The property value representation matches the regex specified in the value.
			/// </summary>
			[EnumLiteral("regex")]
			Regex,
			/// <summary>
			/// The property value is in the set of codes or concepts identified by the value.
			/// </summary>
			[EnumLiteral("in")]
			In,
			/// <summary>
			/// The property value is not in the set of codes or concepts identified by the value.
			/// </summary>
			[EnumLiteral("not in")]
			NotIn,
		}

		[FhirType("ValueSetDefineComponent")]
		[DataContract]
		public partial class ValueSetDefineComponent : Hl7.Fhir.Model.Element
		{
			/// <summary>
			/// URI to identify the code system
			/// </summary>
			[FhirElement("system", InSummary = true, Order = 40)]
			[Cardinality(Min = 1, Max = 1)]
			[DataMember]
			public Hl7.Fhir.Model.FhirUri SystemElement { get; set; }

			/// <summary>
			/// URI to identify the code system
			/// </summary>
			/// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
			[NotMapped]
			[IgnoreDataMember]
			public string System
			{
				get { return SystemElement != null ? SystemElement.Value : null; }
				set
				{
					if (value == null)
						SystemElement = null;
					else
						SystemElement = new Hl7.Fhir.Model.FhirUri(value);
				}
			}

			/// <summary>
			/// Version of this system
			/// </summary>
			[FhirElement("version", InSummary = true, Order = 50)]
			[DataMember]
			public Hl7.Fhir.Model.FhirString VersionElement { get; set; }

			/// <summary>
			/// Version of this system
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
			/// If code comparison is case sensitive
			/// </summary>
			[FhirElement("caseSensitive", InSummary = true, Order = 60)]
			[DataMember]
			public Hl7.Fhir.Model.FhirBoolean CaseSensitiveElement { get; set; }

			/// <summary>
			/// If code comparison is case sensitive
			/// </summary>
			/// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
			[NotMapped]
			[IgnoreDataMember]
			public bool? CaseSensitive
			{
				get { return CaseSensitiveElement != null ? CaseSensitiveElement.Value : null; }
				set
				{
					if (value == null)
						CaseSensitiveElement = null;
					else
						CaseSensitiveElement = new Hl7.Fhir.Model.FhirBoolean(value);
				}
			}

			/// <summary>
			/// Concepts in the code system
			/// </summary>
			[FhirElement("concept", InSummary = true, Order = 70)]
			[Cardinality(Min = 0, Max = -1)]
			[DataMember]
			public List<Hl7.Fhir.Model.ValueSet.ValueSetDefineConceptComponent> Concept { get; set; }

			public override IDeepCopyable CopyTo(IDeepCopyable other)
			{
				var dest = other as ValueSetDefineComponent;

				if (dest != null)
				{
					base.CopyTo(dest);
					if (SystemElement != null) dest.SystemElement = (Hl7.Fhir.Model.FhirUri)SystemElement.DeepCopy();
					if (VersionElement != null) dest.VersionElement = (Hl7.Fhir.Model.FhirString)VersionElement.DeepCopy();
					if (CaseSensitiveElement != null) dest.CaseSensitiveElement = (Hl7.Fhir.Model.FhirBoolean)CaseSensitiveElement.DeepCopy();
					if (Concept != null) dest.Concept = new List<Hl7.Fhir.Model.ValueSet.ValueSetDefineConceptComponent>(Concept.DeepCopy());
					return dest;
				}
				else
					throw Error.Argument(nameof(other), "Can only copy to an object of the same type");
			}

			public override IDeepCopyable DeepCopy()
			{
				return CopyTo(new ValueSetDefineComponent());
			}

			public override bool Matches(IDeepComparable other)
			{
				var otherT = other as ValueSetDefineComponent;
				if (otherT == null) return false;

				if (!base.Matches(otherT)) return false;
				if (!DeepComparable.Matches(SystemElement, otherT.SystemElement)) return false;
				if (!DeepComparable.Matches(VersionElement, otherT.VersionElement)) return false;
				if (!DeepComparable.Matches(CaseSensitiveElement, otherT.CaseSensitiveElement)) return false;
				if (!DeepComparable.Matches(Concept, otherT.Concept)) return false;

				return true;
			}

			public override bool IsExactly(IDeepComparable other)
			{
				var otherT = other as ValueSetDefineComponent;
				if (otherT == null) return false;

				if (!base.IsExactly(otherT)) return false;
				if (!DeepComparable.IsExactly(SystemElement, otherT.SystemElement)) return false;
				if (!DeepComparable.IsExactly(VersionElement, otherT.VersionElement)) return false;
				if (!DeepComparable.IsExactly(CaseSensitiveElement, otherT.CaseSensitiveElement)) return false;
				if (!DeepComparable.IsExactly(Concept, otherT.Concept)) return false;

				return true;
			}

		}


		[FhirType("ValueSetExpansionContainsComponent")]
		[DataContract]
		public partial class ValueSetExpansionContainsComponent : Hl7.Fhir.Model.Element
		{
			/// <summary>
			/// System value for the code
			/// </summary>
			[FhirElement("system", InSummary = true, Order = 40)]
			[DataMember]
			public Hl7.Fhir.Model.FhirUri SystemElement { get; set; }

			/// <summary>
			/// System value for the code
			/// </summary>
			/// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
			[NotMapped]
			[IgnoreDataMember]
			public string System
			{
				get { return SystemElement != null ? SystemElement.Value : null; }
				set
				{
					if (value == null)
						SystemElement = null;
					else
						SystemElement = new Hl7.Fhir.Model.FhirUri(value);
				}
			}

			/// <summary>
			/// Code - if blank, this is not a choosable code
			/// </summary>
			[FhirElement("code", InSummary = true, Order = 50)]
			[DataMember]
			public Hl7.Fhir.Model.Code CodeElement { get; set; }

			/// <summary>
			/// Code - if blank, this is not a choosable code
			/// </summary>
			/// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
			[NotMapped]
			[IgnoreDataMember]
			public string Code
			{
				get { return CodeElement != null ? CodeElement.Value : null; }
				set
				{
					if (value == null)
						CodeElement = null;
					else
						CodeElement = new Hl7.Fhir.Model.Code(value);
				}
			}

			/// <summary>
			/// User display for the concept
			/// </summary>
			[FhirElement("display", InSummary = true, Order = 60)]
			[DataMember]
			public Hl7.Fhir.Model.FhirString DisplayElement { get; set; }

			/// <summary>
			/// User display for the concept
			/// </summary>
			/// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
			[NotMapped]
			[IgnoreDataMember]
			public string Display
			{
				get { return DisplayElement != null ? DisplayElement.Value : null; }
				set
				{
					if (value == null)
						DisplayElement = null;
					else
						DisplayElement = new Hl7.Fhir.Model.FhirString(value);
				}
			}

			/// <summary>
			/// Codes contained in this concept
			/// </summary>
			[FhirElement("contains", InSummary = true, Order = 70)]
			[Cardinality(Min = 0, Max = -1)]
			[DataMember]
			public List<Hl7.Fhir.Model.ValueSet.ValueSetExpansionContainsComponent> Contains { get; set; }

			public override IDeepCopyable CopyTo(IDeepCopyable other)
			{
				var dest = other as ValueSetExpansionContainsComponent;

				if (dest != null)
				{
					base.CopyTo(dest);
					if (SystemElement != null) dest.SystemElement = (Hl7.Fhir.Model.FhirUri)SystemElement.DeepCopy();
					if (CodeElement != null) dest.CodeElement = (Hl7.Fhir.Model.Code)CodeElement.DeepCopy();
					if (DisplayElement != null) dest.DisplayElement = (Hl7.Fhir.Model.FhirString)DisplayElement.DeepCopy();
					if (Contains != null) dest.Contains = new List<Hl7.Fhir.Model.ValueSet.ValueSetExpansionContainsComponent>(Contains.DeepCopy());
					return dest;
				}
				else
					throw Error.Argument(nameof(other), "Can only copy to an object of the same type");
			}

			public override IDeepCopyable DeepCopy()
			{
				return CopyTo(new ValueSetExpansionContainsComponent());
			}

			public override bool Matches(IDeepComparable other)
			{
				var otherT = other as ValueSetExpansionContainsComponent;
				if (otherT == null) return false;

				if (!base.Matches(otherT)) return false;
				if (!DeepComparable.Matches(SystemElement, otherT.SystemElement)) return false;
				if (!DeepComparable.Matches(CodeElement, otherT.CodeElement)) return false;
				if (!DeepComparable.Matches(DisplayElement, otherT.DisplayElement)) return false;
				if (!DeepComparable.Matches(Contains, otherT.Contains)) return false;

				return true;
			}

			public override bool IsExactly(IDeepComparable other)
			{
				var otherT = other as ValueSetExpansionContainsComponent;
				if (otherT == null) return false;

				if (!base.IsExactly(otherT)) return false;
				if (!DeepComparable.IsExactly(SystemElement, otherT.SystemElement)) return false;
				if (!DeepComparable.IsExactly(CodeElement, otherT.CodeElement)) return false;
				if (!DeepComparable.IsExactly(DisplayElement, otherT.DisplayElement)) return false;
				if (!DeepComparable.IsExactly(Contains, otherT.Contains)) return false;

				return true;
			}

		}


		[FhirType("ConceptSetComponent")]
		[DataContract]
		public partial class ConceptSetComponent : Hl7.Fhir.Model.Element
		{
			/// <summary>
			/// The system the codes come from
			/// </summary>
			[FhirElement("system", InSummary = true, Order = 40)]
			[Cardinality(Min = 1, Max = 1)]
			[DataMember]
			public Hl7.Fhir.Model.FhirUri SystemElement { get; set; }

			/// <summary>
			/// The system the codes come from
			/// </summary>
			/// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
			[NotMapped]
			[IgnoreDataMember]
			public string System
			{
				get { return SystemElement != null ? SystemElement.Value : null; }
				set
				{
					if (value == null)
						SystemElement = null;
					else
						SystemElement = new Hl7.Fhir.Model.FhirUri(value);
				}
			}

			/// <summary>
			/// Specific version of the code system referred to
			/// </summary>
			[FhirElement("version", InSummary = true, Order = 50)]
			[DataMember]
			public Hl7.Fhir.Model.FhirString VersionElement { get; set; }

			/// <summary>
			/// Specific version of the code system referred to
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
			/// Code or concept from system
			/// </summary>
			[FhirElement("code", InSummary = true, Order = 60)]
			[Cardinality(Min = 0, Max = -1)]
			[DataMember]
			public List<Hl7.Fhir.Model.Code> CodeElement { get; set; }

			/// <summary>
			/// Code or concept from system
			/// </summary>
			/// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
			[NotMapped]
			[IgnoreDataMember]
			public IEnumerable<string> Code
			{
				get { return CodeElement != null ? CodeElement.Select(elem => elem.Value) : null; }
				set
				{
					if (value == null)
						CodeElement = null;
					else
						CodeElement = new List<Hl7.Fhir.Model.Code>(value.Select(elem => new Hl7.Fhir.Model.Code(elem)));
				}
			}

			/// <summary>
			/// Select codes/concepts by their properties (including relationships)
			/// </summary>
			[FhirElement("filter", InSummary = true, Order = 70)]
			[Cardinality(Min = 0, Max = -1)]
			[DataMember]
			public List<Hl7.Fhir.Model.ValueSet.ConceptSetFilterComponent> Filter { get; set; }

			public override IDeepCopyable CopyTo(IDeepCopyable other)
			{
				var dest = other as ConceptSetComponent;

				if (dest != null)
				{
					base.CopyTo(dest);
					if (SystemElement != null) dest.SystemElement = (Hl7.Fhir.Model.FhirUri)SystemElement.DeepCopy();
					if (VersionElement != null) dest.VersionElement = (Hl7.Fhir.Model.FhirString)VersionElement.DeepCopy();
					if (CodeElement != null) dest.CodeElement = new List<Hl7.Fhir.Model.Code>(CodeElement.DeepCopy());
					if (Filter != null) dest.Filter = new List<Hl7.Fhir.Model.ValueSet.ConceptSetFilterComponent>(Filter.DeepCopy());
					return dest;
				}
				else
					throw Error.Argument(nameof(other), "Can only copy to an object of the same type");
			}

			public override IDeepCopyable DeepCopy()
			{
				return CopyTo(new ConceptSetComponent());
			}

			public override bool Matches(IDeepComparable other)
			{
				var otherT = other as ConceptSetComponent;
				if (otherT == null) return false;

				if (!base.Matches(otherT)) return false;
				if (!DeepComparable.Matches(SystemElement, otherT.SystemElement)) return false;
				if (!DeepComparable.Matches(VersionElement, otherT.VersionElement)) return false;
				if (!DeepComparable.Matches(CodeElement, otherT.CodeElement)) return false;
				if (!DeepComparable.Matches(Filter, otherT.Filter)) return false;

				return true;
			}

			public override bool IsExactly(IDeepComparable other)
			{
				var otherT = other as ConceptSetComponent;
				if (otherT == null) return false;

				if (!base.IsExactly(otherT)) return false;
				if (!DeepComparable.IsExactly(SystemElement, otherT.SystemElement)) return false;
				if (!DeepComparable.IsExactly(VersionElement, otherT.VersionElement)) return false;
				if (!DeepComparable.IsExactly(CodeElement, otherT.CodeElement)) return false;
				if (!DeepComparable.IsExactly(Filter, otherT.Filter)) return false;

				return true;
			}

		}


		[FhirType("ConceptSetFilterComponent")]
		[DataContract]
		public partial class ConceptSetFilterComponent : Hl7.Fhir.Model.Element
		{
			/// <summary>
			/// A property defined by the code system
			/// </summary>
			[FhirElement("property", InSummary = true, Order = 40)]
			[Cardinality(Min = 1, Max = 1)]
			[DataMember]
			public Hl7.Fhir.Model.Code PropertyElement { get; set; }

			/// <summary>
			/// A property defined by the code system
			/// </summary>
			/// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
			[NotMapped]
			[IgnoreDataMember]
			public string Property
			{
				get { return PropertyElement != null ? PropertyElement.Value : null; }
				set
				{
					if (value == null)
						PropertyElement = null;
					else
						PropertyElement = new Hl7.Fhir.Model.Code(value);
				}
			}

			/// <summary>
			/// = | is-a | is-not-a | regex | in | not in
			/// </summary>
			[FhirElement("op", InSummary = true, Order = 50)]
			[Cardinality(Min = 1, Max = 1)]
			[DataMember]
			public Code<Hl7.Fhir.Model.ValueSet.FilterOperator> OpElement { get; set; }

			/// <summary>
			/// = | is-a | is-not-a | regex | in | not in
			/// </summary>
			/// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
			[NotMapped]
			[IgnoreDataMember]
			public Hl7.Fhir.Model.ValueSet.FilterOperator? Op
			{
				get { return OpElement != null ? OpElement.Value : null; }
				set
				{
					if (value == null)
						OpElement = null;
					else
						OpElement = new Code<Hl7.Fhir.Model.ValueSet.FilterOperator>(value);
				}
			}

			/// <summary>
			/// Code from the system, or regex criteria
			/// </summary>
			[FhirElement("value", InSummary = true, Order = 60)]
			[Cardinality(Min = 1, Max = 1)]
			[DataMember]
			public Hl7.Fhir.Model.Code ValueElement { get; set; }

			/// <summary>
			/// Code from the system, or regex criteria
			/// </summary>
			/// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
			[NotMapped]
			[IgnoreDataMember]
			public string Value
			{
				get { return ValueElement != null ? ValueElement.Value : null; }
				set
				{
					if (value == null)
						ValueElement = null;
					else
						ValueElement = new Hl7.Fhir.Model.Code(value);
				}
			}

			public override IDeepCopyable CopyTo(IDeepCopyable other)
			{
				var dest = other as ConceptSetFilterComponent;

				if (dest != null)
				{
					base.CopyTo(dest);
					if (PropertyElement != null) dest.PropertyElement = (Hl7.Fhir.Model.Code)PropertyElement.DeepCopy();
					if (OpElement != null) dest.OpElement = (Code<Hl7.Fhir.Model.ValueSet.FilterOperator>)OpElement.DeepCopy();
					if (ValueElement != null) dest.ValueElement = (Hl7.Fhir.Model.Code)ValueElement.DeepCopy();
					return dest;
				}
				else
					throw Error.Argument(nameof(other), "Can only copy to an object of the same type");
			}

			public override IDeepCopyable DeepCopy()
			{
				return CopyTo(new ConceptSetFilterComponent());
			}

			public override bool Matches(IDeepComparable other)
			{
				var otherT = other as ConceptSetFilterComponent;
				if (otherT == null) return false;

				if (!base.Matches(otherT)) return false;
				if (!DeepComparable.Matches(PropertyElement, otherT.PropertyElement)) return false;
				if (!DeepComparable.Matches(OpElement, otherT.OpElement)) return false;
				if (!DeepComparable.Matches(ValueElement, otherT.ValueElement)) return false;

				return true;
			}

			public override bool IsExactly(IDeepComparable other)
			{
				var otherT = other as ConceptSetFilterComponent;
				if (otherT == null) return false;

				if (!base.IsExactly(otherT)) return false;
				if (!DeepComparable.IsExactly(PropertyElement, otherT.PropertyElement)) return false;
				if (!DeepComparable.IsExactly(OpElement, otherT.OpElement)) return false;
				if (!DeepComparable.IsExactly(ValueElement, otherT.ValueElement)) return false;

				return true;
			}

		}


		[FhirType("ValueSetComposeComponent")]
		[DataContract]
		public partial class ValueSetComposeComponent : Hl7.Fhir.Model.Element
		{
			/// <summary>
			/// Import the contents of another value set
			/// </summary>
			[FhirElement("import", InSummary = true, Order = 40)]
			[Cardinality(Min = 0, Max = -1)]
			[DataMember]
			public List<Hl7.Fhir.Model.FhirUri> ImportElement { get; set; }

			/// <summary>
			/// Import the contents of another value set
			/// </summary>
			/// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
			[NotMapped]
			[IgnoreDataMember]
			public IEnumerable<string> Import
			{
				get { return ImportElement != null ? ImportElement.Select(elem => elem.Value) : null; }
				set
				{
					if (value == null)
						ImportElement = null;
					else
						ImportElement = new List<Hl7.Fhir.Model.FhirUri>(value.Select(elem => new Hl7.Fhir.Model.FhirUri(elem)));
				}
			}

			/// <summary>
			/// Include one or more codes from a code system
			/// </summary>
			[FhirElement("include", InSummary = true, Order = 50)]
			[Cardinality(Min = 0, Max = -1)]
			[DataMember]
			public List<Hl7.Fhir.Model.ValueSet.ConceptSetComponent> Include { get; set; }

			/// <summary>
			/// Explicitly exclude codes
			/// </summary>
			[FhirElement("exclude", InSummary = true, Order = 60)]
			[Cardinality(Min = 0, Max = -1)]
			[DataMember]
			public List<Hl7.Fhir.Model.ValueSet.ConceptSetComponent> Exclude { get; set; }

			public override IDeepCopyable CopyTo(IDeepCopyable other)
			{
				var dest = other as ValueSetComposeComponent;

				if (dest != null)
				{
					base.CopyTo(dest);
					if (ImportElement != null) dest.ImportElement = new List<Hl7.Fhir.Model.FhirUri>(ImportElement.DeepCopy());
					if (Include != null) dest.Include = new List<Hl7.Fhir.Model.ValueSet.ConceptSetComponent>(Include.DeepCopy());
					if (Exclude != null) dest.Exclude = new List<Hl7.Fhir.Model.ValueSet.ConceptSetComponent>(Exclude.DeepCopy());
					return dest;
				}
				else
					throw Error.Argument(nameof(other), "Can only copy to an object of the same type");
			}

			public override IDeepCopyable DeepCopy()
			{
				return CopyTo(new ValueSetComposeComponent());
			}

			public override bool Matches(IDeepComparable other)
			{
				var otherT = other as ValueSetComposeComponent;
				if (otherT == null) return false;

				if (!base.Matches(otherT)) return false;
				if (!DeepComparable.Matches(ImportElement, otherT.ImportElement)) return false;
				if (!DeepComparable.Matches(Include, otherT.Include)) return false;
				if (!DeepComparable.Matches(Exclude, otherT.Exclude)) return false;

				return true;
			}

			public override bool IsExactly(IDeepComparable other)
			{
				var otherT = other as ValueSetComposeComponent;
				if (otherT == null) return false;

				if (!base.IsExactly(otherT)) return false;
				if (!DeepComparable.IsExactly(ImportElement, otherT.ImportElement)) return false;
				if (!DeepComparable.IsExactly(Include, otherT.Include)) return false;
				if (!DeepComparable.IsExactly(Exclude, otherT.Exclude)) return false;

				return true;
			}

		}


		[FhirType("ValueSetDefineConceptComponent")]
		[DataContract]
		public partial class ValueSetDefineConceptComponent : Hl7.Fhir.Model.Element
		{
			/// <summary>
			/// Code that identifies concept
			/// </summary>
			[FhirElement("code", InSummary = true, Order = 40)]
			[Cardinality(Min = 1, Max = 1)]
			[DataMember]
			public Hl7.Fhir.Model.Code CodeElement { get; set; }

			/// <summary>
			/// Code that identifies concept
			/// </summary>
			/// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
			[NotMapped]
			[IgnoreDataMember]
			public string Code
			{
				get { return CodeElement != null ? CodeElement.Value : null; }
				set
				{
					if (value == null)
						CodeElement = null;
					else
						CodeElement = new Hl7.Fhir.Model.Code(value);
				}
			}

			/// <summary>
			/// If this code is not for use as a real concept
			/// </summary>
			[FhirElement("abstract", InSummary = true, Order = 50)]
			[DataMember]
			public Hl7.Fhir.Model.FhirBoolean AbstractElement { get; set; }

			/// <summary>
			/// If this code is not for use as a real concept
			/// </summary>
			/// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
			[NotMapped]
			[IgnoreDataMember]
			public bool? Abstract
			{
				get { return AbstractElement != null ? AbstractElement.Value : null; }
				set
				{
					if (value == null)
						AbstractElement = null;
					else
						AbstractElement = new Hl7.Fhir.Model.FhirBoolean(value);
				}
			}

			/// <summary>
			/// Text to Display to the user
			/// </summary>
			[FhirElement("display", InSummary = true, Order = 60)]
			[DataMember]
			public Hl7.Fhir.Model.FhirString DisplayElement { get; set; }

			/// <summary>
			/// Text to Display to the user
			/// </summary>
			/// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
			[NotMapped]
			[IgnoreDataMember]
			public string Display
			{
				get { return DisplayElement != null ? DisplayElement.Value : null; }
				set
				{
					if (value == null)
						DisplayElement = null;
					else
						DisplayElement = new Hl7.Fhir.Model.FhirString(value);
				}
			}

			/// <summary>
			/// Formal Definition
			/// </summary>
			[FhirElement("definition", InSummary = true, Order = 70)]
			[DataMember]
			public Hl7.Fhir.Model.FhirString DefinitionElement { get; set; }

			/// <summary>
			/// Formal Definition
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
						DefinitionElement = new Hl7.Fhir.Model.FhirString(value);
				}
			}

			/// <summary>
			/// Child Concepts (is-a / contains)
			/// </summary>
			[FhirElement("concept", InSummary = true, Order = 80)]
			[Cardinality(Min = 0, Max = -1)]
			[DataMember]
			public List<Hl7.Fhir.Model.ValueSet.ValueSetDefineConceptComponent> Concept { get; set; }

			public override IDeepCopyable CopyTo(IDeepCopyable other)
			{
				var dest = other as ValueSetDefineConceptComponent;

				if (dest != null)
				{
					base.CopyTo(dest);
					if (CodeElement != null) dest.CodeElement = (Hl7.Fhir.Model.Code)CodeElement.DeepCopy();
					if (AbstractElement != null) dest.AbstractElement = (Hl7.Fhir.Model.FhirBoolean)AbstractElement.DeepCopy();
					if (DisplayElement != null) dest.DisplayElement = (Hl7.Fhir.Model.FhirString)DisplayElement.DeepCopy();
					if (DefinitionElement != null) dest.DefinitionElement = (Hl7.Fhir.Model.FhirString)DefinitionElement.DeepCopy();
					if (Concept != null) dest.Concept = new List<Hl7.Fhir.Model.ValueSet.ValueSetDefineConceptComponent>(Concept.DeepCopy());
					return dest;
				}
				else
					throw Error.Argument(nameof(other), "Can only copy to an object of the same type");
			}

			public override IDeepCopyable DeepCopy()
			{
				return CopyTo(new ValueSetDefineConceptComponent());
			}

			public override bool Matches(IDeepComparable other)
			{
				var otherT = other as ValueSetDefineConceptComponent;
				if (otherT == null) return false;

				if (!base.Matches(otherT)) return false;
				if (!DeepComparable.Matches(CodeElement, otherT.CodeElement)) return false;
				if (!DeepComparable.Matches(AbstractElement, otherT.AbstractElement)) return false;
				if (!DeepComparable.Matches(DisplayElement, otherT.DisplayElement)) return false;
				if (!DeepComparable.Matches(DefinitionElement, otherT.DefinitionElement)) return false;
				if (!DeepComparable.Matches(Concept, otherT.Concept)) return false;

				return true;
			}

			public override bool IsExactly(IDeepComparable other)
			{
				var otherT = other as ValueSetDefineConceptComponent;
				if (otherT == null) return false;

				if (!base.IsExactly(otherT)) return false;
				if (!DeepComparable.IsExactly(CodeElement, otherT.CodeElement)) return false;
				if (!DeepComparable.IsExactly(AbstractElement, otherT.AbstractElement)) return false;
				if (!DeepComparable.IsExactly(DisplayElement, otherT.DisplayElement)) return false;
				if (!DeepComparable.IsExactly(DefinitionElement, otherT.DefinitionElement)) return false;
				if (!DeepComparable.IsExactly(Concept, otherT.Concept)) return false;

				return true;
			}

		}


		[FhirType("ValueSetExpansionComponent")]
		[DataContract]
		public partial class ValueSetExpansionComponent : Hl7.Fhir.Model.Element
		{
			/// <summary>
			/// Uniquely identifies this expansion
			/// </summary>
			[FhirElement("identifier", InSummary = true, Order = 40)]
			[DataMember]
			public Hl7.Fhir.Model.Identifier Identifier { get; set; }

			/// <summary>
			/// Time valueset expansion happened
			/// </summary>
			[FhirElement("timestamp", InSummary = true, Order = 50)]
			[Cardinality(Min = 1, Max = 1)]
			[DataMember]
			public Hl7.Fhir.Model.Instant TimestampElement { get; set; }

			/// <summary>
			/// Time valueset expansion happened
			/// </summary>
			/// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
			[NotMapped]
			[IgnoreDataMember]
			public DateTimeOffset? Timestamp
			{
				get { return TimestampElement != null ? TimestampElement.Value : null; }
				set
				{
					if (value == null)
						TimestampElement = null;
					else
						TimestampElement = new Hl7.Fhir.Model.Instant(value);
				}
			}

			/// <summary>
			/// Codes in the value set
			/// </summary>
			[FhirElement("contains", InSummary = true, Order = 60)]
			[Cardinality(Min = 0, Max = -1)]
			[DataMember]
			public List<Hl7.Fhir.Model.ValueSet.ValueSetExpansionContainsComponent> Contains { get; set; }

			public override IDeepCopyable CopyTo(IDeepCopyable other)
			{
				var dest = other as ValueSetExpansionComponent;

				if (dest != null)
				{
					base.CopyTo(dest);
					if (Identifier != null) dest.Identifier = (Hl7.Fhir.Model.Identifier)Identifier.DeepCopy();
					if (TimestampElement != null) dest.TimestampElement = (Hl7.Fhir.Model.Instant)TimestampElement.DeepCopy();
					if (Contains != null) dest.Contains = new List<Hl7.Fhir.Model.ValueSet.ValueSetExpansionContainsComponent>(Contains.DeepCopy());
					return dest;
				}
				else
					throw Error.Argument(nameof(other), "Can only copy to an object of the same type");
			}

			public override IDeepCopyable DeepCopy()
			{
				return CopyTo(new ValueSetExpansionComponent());
			}

			public override bool Matches(IDeepComparable other)
			{
				var otherT = other as ValueSetExpansionComponent;
				if (otherT == null) return false;

				if (!base.Matches(otherT)) return false;
				if (!DeepComparable.Matches(Identifier, otherT.Identifier)) return false;
				if (!DeepComparable.Matches(TimestampElement, otherT.TimestampElement)) return false;
				if (!DeepComparable.Matches(Contains, otherT.Contains)) return false;

				return true;
			}

			public override bool IsExactly(IDeepComparable other)
			{
				var otherT = other as ValueSetExpansionComponent;
				if (otherT == null) return false;

				if (!base.IsExactly(otherT)) return false;
				if (!DeepComparable.IsExactly(Identifier, otherT.Identifier)) return false;
				if (!DeepComparable.IsExactly(TimestampElement, otherT.TimestampElement)) return false;
				if (!DeepComparable.IsExactly(Contains, otherT.Contains)) return false;

				return true;
			}

		}


		/// <summary>
		/// Logical id to reference this value set
		/// </summary>
		[FhirElement("identifier", InSummary = true, Order = 70)]
		[DataMember]
		public Hl7.Fhir.Model.FhirString IdentifierElement { get; set; }

		/// <summary>
		/// Logical id to reference this value set
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
		/// Logical id for this version of the value set
		/// </summary>
		[FhirElement("version", InSummary = true, Order = 80)]
		[DataMember]
		public Hl7.Fhir.Model.FhirString VersionElement { get; set; }

		/// <summary>
		/// Logical id for this version of the value set
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
		/// Informal name for this value set
		/// </summary>
		[FhirElement("name", InSummary = true, Order = 90)]
		[Cardinality(Min = 1, Max = 1)]
		[DataMember]
		public Hl7.Fhir.Model.FhirString NameElement { get; set; }

		/// <summary>
		/// Informal name for this value set
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
				if (value == null)
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
		/// Human language description of the value set
		/// </summary>
		[FhirElement("description", InSummary = true, Order = 120)]
		[Cardinality(Min = 1, Max = 1)]
		[DataMember]
		public Hl7.Fhir.Model.FhirString DescriptionElement { get; set; }

		/// <summary>
		/// Human language description of the value set
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
		/// About the value set or its content
		/// </summary>
		[FhirElement("copyright", Order = 130)]
		[DataMember]
		public Hl7.Fhir.Model.FhirString CopyrightElement { get; set; }

		/// <summary>
		/// About the value set or its content
		/// </summary>
		/// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
		[NotMapped]
		[IgnoreDataMember]
		public string Copyright
		{
			get { return CopyrightElement != null ? CopyrightElement.Value : null; }
			set
			{
				if (value == null)
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
		public Code<Hl7.Fhir.Model.ValueSet.ValueSetStatus> StatusElement { get; set; }

		/// <summary>
		/// draft | active | retired
		/// </summary>
		/// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
		[NotMapped]
		[IgnoreDataMember]
		public Hl7.Fhir.Model.ValueSet.ValueSetStatus? Status
		{
			get { return StatusElement != null ? StatusElement.Value : null; }
			set
			{
				if (value == null)
					StatusElement = null;
				else
					StatusElement = new Code<Hl7.Fhir.Model.ValueSet.ValueSetStatus>(value);
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
				if (value == null)
					ExperimentalElement = null;
				else
					ExperimentalElement = new Hl7.Fhir.Model.FhirBoolean(value);
			}
		}

		/// <summary>
		/// Whether this is intended to be used with an extensible binding
		/// </summary>
		[FhirElement("extensible", Order = 160)]
		[DataMember]
		public Hl7.Fhir.Model.FhirBoolean ExtensibleElement { get; set; }

		/// <summary>
		/// Whether this is intended to be used with an extensible binding
		/// </summary>
		/// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
		[NotMapped]
		[IgnoreDataMember]
		public bool? Extensible
		{
			get { return ExtensibleElement != null ? ExtensibleElement.Value : null; }
			set
			{
				if (value == null)
					ExtensibleElement = null;
				else
					ExtensibleElement = new Hl7.Fhir.Model.FhirBoolean(value);
			}
		}

		/// <summary>
		/// Date for given status
		/// </summary>
		[FhirElement("date", InSummary = true, Order = 170)]
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
		/// When value set defines its own codes
		/// </summary>
		[FhirElement("define", InSummary = true, Order = 180)]
		[DataMember]
		public Hl7.Fhir.Model.ValueSet.ValueSetDefineComponent Define { get; set; }

		/// <summary>
		/// When value set includes codes from elsewhere
		/// </summary>
		[FhirElement("compose", Order = 190)]
		[DataMember]
		public Hl7.Fhir.Model.ValueSet.ValueSetComposeComponent Compose { get; set; }

		/// <summary>
		/// When value set is an expansion
		/// </summary>
		[FhirElement("expansion", Order = 200)]
		[DataMember]
		public Hl7.Fhir.Model.ValueSet.ValueSetExpansionComponent Expansion { get; set; }

		public override IDeepCopyable CopyTo(IDeepCopyable other)
		{
			var dest = other as ValueSet;

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
				if (StatusElement != null) dest.StatusElement = (Code<Hl7.Fhir.Model.ValueSet.ValueSetStatus>)StatusElement.DeepCopy();
				if (ExperimentalElement != null) dest.ExperimentalElement = (Hl7.Fhir.Model.FhirBoolean)ExperimentalElement.DeepCopy();
				if (ExtensibleElement != null) dest.ExtensibleElement = (Hl7.Fhir.Model.FhirBoolean)ExtensibleElement.DeepCopy();
				if (DateElement != null) dest.DateElement = (Hl7.Fhir.Model.FhirDateTime)DateElement.DeepCopy();
				if (Define != null) dest.Define = (Hl7.Fhir.Model.ValueSet.ValueSetDefineComponent)Define.DeepCopy();
				if (Compose != null) dest.Compose = (Hl7.Fhir.Model.ValueSet.ValueSetComposeComponent)Compose.DeepCopy();
				if (Expansion != null) dest.Expansion = (Hl7.Fhir.Model.ValueSet.ValueSetExpansionComponent)Expansion.DeepCopy();
				return dest;
			}
			else
				throw Error.Argument(nameof(other), "Can only copy to an object of the same type");
		}

		public override IDeepCopyable DeepCopy()
		{
			return CopyTo(new ValueSet());
		}

		public override bool Matches(IDeepComparable other)
		{
			var otherT = other as ValueSet;
			if (otherT == null) return false;

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
			if (!DeepComparable.Matches(ExtensibleElement, otherT.ExtensibleElement)) return false;
			if (!DeepComparable.Matches(DateElement, otherT.DateElement)) return false;
			if (!DeepComparable.Matches(Define, otherT.Define)) return false;
			if (!DeepComparable.Matches(Compose, otherT.Compose)) return false;
			if (!DeepComparable.Matches(Expansion, otherT.Expansion)) return false;

			return true;
		}

		public override bool IsExactly(IDeepComparable other)
		{
			var otherT = other as ValueSet;
			if (otherT == null) return false;

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
			if (!DeepComparable.IsExactly(ExtensibleElement, otherT.ExtensibleElement)) return false;
			if (!DeepComparable.IsExactly(DateElement, otherT.DateElement)) return false;
			if (!DeepComparable.IsExactly(Define, otherT.Define)) return false;
			if (!DeepComparable.IsExactly(Compose, otherT.Compose)) return false;
			if (!DeepComparable.IsExactly(Expansion, otherT.Expansion)) return false;

			return true;
		}

	}

}
