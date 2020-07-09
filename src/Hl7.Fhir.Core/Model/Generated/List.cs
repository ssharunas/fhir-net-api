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
	/// Information summarized from a list of other resources
	/// </summary>
	[FhirType("List", IsResource = true)]
	[DataContract]
	public partial class List : Hl7.Fhir.Model.Resource
	{
		/// <summary>
		/// The processing mode that applies to this list
		/// </summary>
		[FhirEnumeration("ListMode")]
		public enum ListMode
		{
			/// <summary>
			/// This list is the master list, maintained in an ongoing fashion with regular updates as the real world list it is tracking changes.
			/// </summary>
			[EnumLiteral("working")]
			Working,
			/// <summary>
			/// This list was prepared as a snapshot. It should not be assumed to be current.
			/// </summary>
			[EnumLiteral("snapshot")]
			Snapshot,
			/// <summary>
			/// The list is prepared as a statement of changes that have been made or recommended.
			/// </summary>
			[EnumLiteral("changes")]
			Changes,
		}

		[FhirType("ListEntryComponent")]
		[DataContract]
		public partial class ListEntryComponent : Hl7.Fhir.Model.Element
		{
			/// <summary>
			/// Workflow information about this item
			/// </summary>
			[FhirElement("flag", InSummary = true, Order = 40)]
			[Cardinality(Min = 0, Max = -1)]
			[DataMember]
			public List<Hl7.Fhir.Model.CodeableConcept> Flag { get; set; }

			/// <summary>
			/// If this item is actually marked as deleted
			/// </summary>
			[FhirElement("deleted", InSummary = true, Order = 50)]
			[DataMember]
			public Hl7.Fhir.Model.FhirBoolean DeletedElement { get; set; }

			/// <summary>
			/// If this item is actually marked as deleted
			/// </summary>
			/// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
			[NotMapped]
			[IgnoreDataMember]
			public bool? Deleted
			{
				get { return DeletedElement != null ? DeletedElement.Value : null; }
				set
				{
					if (value is null)
						DeletedElement = null;
					else
						DeletedElement = new Hl7.Fhir.Model.FhirBoolean(value);
				}
			}

			/// <summary>
			/// When item added to list
			/// </summary>
			[FhirElement("date", InSummary = true, Order = 60)]
			[DataMember]
			public Hl7.Fhir.Model.FhirDateTime DateElement { get; set; }

			/// <summary>
			/// When item added to list
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
			/// Actual entry
			/// </summary>
			[FhirElement("item", InSummary = true, Order = 70)]
			[References()]
			[Cardinality(Min = 1, Max = 1)]
			[DataMember]
			public Hl7.Fhir.Model.ResourceReference Item { get; set; }

			public override IDeepCopyable CopyTo(IDeepCopyable other)
			{
				var dest = other as ListEntryComponent;

				if (dest != null)
				{
					base.CopyTo(dest);
					if (Flag != null) dest.Flag = new List<Hl7.Fhir.Model.CodeableConcept>(Flag.DeepCopy());
					if (DeletedElement != null) dest.DeletedElement = (Hl7.Fhir.Model.FhirBoolean)DeletedElement.DeepCopy();
					if (DateElement != null) dest.DateElement = (Hl7.Fhir.Model.FhirDateTime)DateElement.DeepCopy();
					if (Item != null) dest.Item = (Hl7.Fhir.Model.ResourceReference)Item.DeepCopy();
					return dest;
				}
				else
					throw Error.Argument(nameof(other), "Can only copy to an object of the same type");
			}

			public override IDeepCopyable DeepCopy()
			{
				return CopyTo(new ListEntryComponent());
			}

			public override bool Matches(IDeepComparable other)
			{
				var otherT = other as ListEntryComponent;
				if (otherT is null) return false;

				if (!base.Matches(otherT)) return false;
				if (!DeepComparable.Matches(Flag, otherT.Flag)) return false;
				if (!DeepComparable.Matches(DeletedElement, otherT.DeletedElement)) return false;
				if (!DeepComparable.Matches(DateElement, otherT.DateElement)) return false;
				if (!DeepComparable.Matches(Item, otherT.Item)) return false;

				return true;
			}

			public override bool IsExactly(IDeepComparable other)
			{
				var otherT = other as ListEntryComponent;
				if (otherT is null) return false;

				if (!base.IsExactly(otherT)) return false;
				if (!DeepComparable.IsExactly(Flag, otherT.Flag)) return false;
				if (!DeepComparable.IsExactly(DeletedElement, otherT.DeletedElement)) return false;
				if (!DeepComparable.IsExactly(DateElement, otherT.DateElement)) return false;
				if (!DeepComparable.IsExactly(Item, otherT.Item)) return false;

				return true;
			}

		}


		/// <summary>
		/// Business identifier
		/// </summary>
		[FhirElement("identifier", Order = 70)]
		[Cardinality(Min = 0, Max = -1)]
		[DataMember]
		public List<Hl7.Fhir.Model.Identifier> Identifier { get; set; }

		/// <summary>
		/// What the purpose of this list is
		/// </summary>
		[FhirElement("code", Order = 80)]
		[DataMember]
		public Hl7.Fhir.Model.CodeableConcept Code { get; set; }

		/// <summary>
		/// If all resources have the same subject
		/// </summary>
		[FhirElement("subject", Order = 90)]
		[References("Patient", "Group", "Device", "Location")]
		[DataMember]
		public Hl7.Fhir.Model.ResourceReference Subject { get; set; }

		/// <summary>
		/// Who and/or what defined the list contents
		/// </summary>
		[FhirElement("source", Order = 100)]
		[References("Practitioner", "Patient", "Device")]
		[DataMember]
		public Hl7.Fhir.Model.ResourceReference Source { get; set; }

		/// <summary>
		/// When the list was prepared
		/// </summary>
		[FhirElement("date", Order = 110)]
		[DataMember]
		public Hl7.Fhir.Model.FhirDateTime DateElement { get; set; }

		/// <summary>
		/// When the list was prepared
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
		/// Whether items in the list have a meaningful order
		/// </summary>
		[FhirElement("ordered", Order = 120)]
		[DataMember]
		public Hl7.Fhir.Model.FhirBoolean OrderedElement { get; set; }

		/// <summary>
		/// Whether items in the list have a meaningful order
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
		/// working | snapshot | changes
		/// </summary>
		[FhirElement("mode", Order = 130)]
		[Cardinality(Min = 1, Max = 1)]
		[DataMember]
		public Code<Hl7.Fhir.Model.List.ListMode> ModeElement { get; set; }

		/// <summary>
		/// working | snapshot | changes
		/// </summary>
		/// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
		[NotMapped]
		[IgnoreDataMember]
		public Hl7.Fhir.Model.List.ListMode? Mode
		{
			get { return ModeElement != null ? ModeElement.Value : null; }
			set
			{
				if (value is null)
					ModeElement = null;
				else
					ModeElement = new Code<Hl7.Fhir.Model.List.ListMode>(value);
			}
		}

		/// <summary>
		/// Entries in the list
		/// </summary>
		[FhirElement("entry", Order = 140)]
		[Cardinality(Min = 0, Max = -1)]
		[DataMember]
		public List<Hl7.Fhir.Model.List.ListEntryComponent> Entry { get; set; }

		/// <summary>
		/// Why list is empty
		/// </summary>
		[FhirElement("emptyReason", Order = 150)]
		[DataMember]
		public Hl7.Fhir.Model.CodeableConcept EmptyReason { get; set; }

		public override IDeepCopyable CopyTo(IDeepCopyable other)
		{
			var dest = other as List;

			if (dest != null)
			{
				base.CopyTo(dest);
				if (Identifier != null) dest.Identifier = new List<Hl7.Fhir.Model.Identifier>(Identifier.DeepCopy());
				if (Code != null) dest.Code = (Hl7.Fhir.Model.CodeableConcept)Code.DeepCopy();
				if (Subject != null) dest.Subject = (Hl7.Fhir.Model.ResourceReference)Subject.DeepCopy();
				if (Source != null) dest.Source = (Hl7.Fhir.Model.ResourceReference)Source.DeepCopy();
				if (DateElement != null) dest.DateElement = (Hl7.Fhir.Model.FhirDateTime)DateElement.DeepCopy();
				if (OrderedElement != null) dest.OrderedElement = (Hl7.Fhir.Model.FhirBoolean)OrderedElement.DeepCopy();
				if (ModeElement != null) dest.ModeElement = (Code<Hl7.Fhir.Model.List.ListMode>)ModeElement.DeepCopy();
				if (Entry != null) dest.Entry = new List<Hl7.Fhir.Model.List.ListEntryComponent>(Entry.DeepCopy());
				if (EmptyReason != null) dest.EmptyReason = (Hl7.Fhir.Model.CodeableConcept)EmptyReason.DeepCopy();
				return dest;
			}
			else
				throw Error.Argument(nameof(other), "Can only copy to an object of the same type");
		}

		public override IDeepCopyable DeepCopy()
		{
			return CopyTo(new List());
		}

		public override bool Matches(IDeepComparable other)
		{
			var otherT = other as List;
			if (otherT is null) return false;

			if (!base.Matches(otherT)) return false;
			if (!DeepComparable.Matches(Identifier, otherT.Identifier)) return false;
			if (!DeepComparable.Matches(Code, otherT.Code)) return false;
			if (!DeepComparable.Matches(Subject, otherT.Subject)) return false;
			if (!DeepComparable.Matches(Source, otherT.Source)) return false;
			if (!DeepComparable.Matches(DateElement, otherT.DateElement)) return false;
			if (!DeepComparable.Matches(OrderedElement, otherT.OrderedElement)) return false;
			if (!DeepComparable.Matches(ModeElement, otherT.ModeElement)) return false;
			if (!DeepComparable.Matches(Entry, otherT.Entry)) return false;
			if (!DeepComparable.Matches(EmptyReason, otherT.EmptyReason)) return false;

			return true;
		}

		public override bool IsExactly(IDeepComparable other)
		{
			var otherT = other as List;
			if (otherT is null) return false;

			if (!base.IsExactly(otherT)) return false;
			if (!DeepComparable.IsExactly(Identifier, otherT.Identifier)) return false;
			if (!DeepComparable.IsExactly(Code, otherT.Code)) return false;
			if (!DeepComparable.IsExactly(Subject, otherT.Subject)) return false;
			if (!DeepComparable.IsExactly(Source, otherT.Source)) return false;
			if (!DeepComparable.IsExactly(DateElement, otherT.DateElement)) return false;
			if (!DeepComparable.IsExactly(OrderedElement, otherT.OrderedElement)) return false;
			if (!DeepComparable.IsExactly(ModeElement, otherT.ModeElement)) return false;
			if (!DeepComparable.IsExactly(Entry, otherT.Entry)) return false;
			if (!DeepComparable.IsExactly(EmptyReason, otherT.EmptyReason)) return false;

			return true;
		}

	}

}
