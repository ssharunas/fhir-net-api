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
	/// A manifest that defines a set of documents
	/// </summary>
	[FhirType("DocumentManifest", IsResource = true)]
	[DataContract]
	public partial class DocumentManifest : Hl7.Fhir.Model.Resource
	{
		/// <summary>
		/// Unique Identifier for the set of documents
		/// </summary>
		[FhirElement("masterIdentifier", Order = 70)]
		[Cardinality(Min = 1, Max = 1)]
		[DataMember]
		public Hl7.Fhir.Model.Identifier MasterIdentifier { get; set; }

		/// <summary>
		/// Other identifiers for the manifest
		/// </summary>
		[FhirElement("identifier", Order = 80)]
		[Cardinality(Min = 0, Max = -1)]
		[DataMember]
		public List<Hl7.Fhir.Model.Identifier> Identifier { get; set; }

		/// <summary>
		/// The subject of the set of documents
		/// </summary>
		[FhirElement("subject", Order = 90)]
		[References("Patient", "Practitioner", "Group", "Device")]
		[Cardinality(Min = 1, Max = -1)]
		[DataMember]
		public List<Hl7.Fhir.Model.ResourceReference> Subject { get; set; }

		/// <summary>
		/// Intended to get notified about this set of documents
		/// </summary>
		[FhirElement("recipient", Order = 100)]
		[References("Patient", "Practitioner", "Organization")]
		[Cardinality(Min = 0, Max = -1)]
		[DataMember]
		public List<Hl7.Fhir.Model.ResourceReference> Recipient { get; set; }

		/// <summary>
		/// What kind of document set this is
		/// </summary>
		[FhirElement("type", Order = 110)]
		[DataMember]
		public Hl7.Fhir.Model.CodeableConcept Type { get; set; }

		/// <summary>
		/// Who and/or what authored the document
		/// </summary>
		[FhirElement("author", Order = 120)]
		[References("Practitioner", "Device", "Patient", "RelatedPerson")]
		[Cardinality(Min = 0, Max = -1)]
		[DataMember]
		public List<Hl7.Fhir.Model.ResourceReference> Author { get; set; }

		/// <summary>
		/// When this document manifest created
		/// </summary>
		[FhirElement("created", Order = 130)]
		[DataMember]
		public Hl7.Fhir.Model.FhirDateTime CreatedElement { get; set; }

		/// <summary>
		/// When this document manifest created
		/// </summary>
		/// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
		[NotMapped]
		[IgnoreDataMember]
		public string Created
		{
			get { return CreatedElement?.Value; }
			set
			{
				if (value is null)
					CreatedElement = null;
				else
					CreatedElement = new Hl7.Fhir.Model.FhirDateTime(value);
			}
		}

		/// <summary>
		/// The source system/application/software
		/// </summary>
		[FhirElement("source", Order = 140)]
		[DataMember]
		public Hl7.Fhir.Model.FhirUri SourceElement { get; set; }

		/// <summary>
		/// The source system/application/software
		/// </summary>
		/// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
		[NotMapped]
		[IgnoreDataMember]
		public string Source
		{
			get { return SourceElement?.Value; }
			set
			{
				if (value is null)
					SourceElement = null;
				else
					SourceElement = new Hl7.Fhir.Model.FhirUri(value);
			}
		}

		/// <summary>
		/// current | superceded | entered in error
		/// </summary>
		[FhirElement("status", Order = 150)]
		[Cardinality(Min = 1, Max = 1)]
		[DataMember]
		public Hl7.Fhir.Model.Code StatusElement { get; set; }

		/// <summary>
		/// current | superceded | entered in error
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
		/// If this document manifest replaces another
		/// </summary>
		[FhirElement("supercedes", Order = 160)]
		[References("DocumentManifest")]
		[DataMember]
		public Hl7.Fhir.Model.ResourceReference Supercedes { get; set; }

		/// <summary>
		/// Human-readable description (title)
		/// </summary>
		[FhirElement("description", Order = 170)]
		[DataMember]
		public Hl7.Fhir.Model.FhirString DescriptionElement { get; set; }

		/// <summary>
		/// Human-readable description (title)
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
		/// Sensitivity of set of documents
		/// </summary>
		[FhirElement("confidentiality", Order = 180)]
		[DataMember]
		public Hl7.Fhir.Model.CodeableConcept Confidentiality { get; set; }

		/// <summary>
		/// Contents of this set of documents
		/// </summary>
		[FhirElement("content", Order = 190)]
		[References("DocumentReference", "Binary", "Media")]
		[Cardinality(Min = 1, Max = -1)]
		[DataMember]
		public List<Hl7.Fhir.Model.ResourceReference> Content { get; set; }

		public override IDeepCopyable CopyTo(IDeepCopyable other)
		{
			var dest = other as DocumentManifest;

			if (dest != null)
			{
				base.CopyTo(dest);
				if (MasterIdentifier != null) dest.MasterIdentifier = (Hl7.Fhir.Model.Identifier)MasterIdentifier.DeepCopy();
				if (Identifier != null) dest.Identifier = new List<Hl7.Fhir.Model.Identifier>(Identifier.DeepCopy());
				if (Subject != null) dest.Subject = new List<Hl7.Fhir.Model.ResourceReference>(Subject.DeepCopy());
				if (Recipient != null) dest.Recipient = new List<Hl7.Fhir.Model.ResourceReference>(Recipient.DeepCopy());
				if (Type != null) dest.Type = (Hl7.Fhir.Model.CodeableConcept)Type.DeepCopy();
				if (Author != null) dest.Author = new List<Hl7.Fhir.Model.ResourceReference>(Author.DeepCopy());
				if (CreatedElement != null) dest.CreatedElement = (Hl7.Fhir.Model.FhirDateTime)CreatedElement.DeepCopy();
				if (SourceElement != null) dest.SourceElement = (Hl7.Fhir.Model.FhirUri)SourceElement.DeepCopy();
				if (StatusElement != null) dest.StatusElement = (Hl7.Fhir.Model.Code)StatusElement.DeepCopy();
				if (Supercedes != null) dest.Supercedes = (Hl7.Fhir.Model.ResourceReference)Supercedes.DeepCopy();
				if (DescriptionElement != null) dest.DescriptionElement = (Hl7.Fhir.Model.FhirString)DescriptionElement.DeepCopy();
				if (Confidentiality != null) dest.Confidentiality = (Hl7.Fhir.Model.CodeableConcept)Confidentiality.DeepCopy();
				if (Content != null) dest.Content = new List<Hl7.Fhir.Model.ResourceReference>(Content.DeepCopy());
				return dest;
			}
			else
				throw Error.Argument(nameof(other), "Can only copy to an object of the same type");
		}

		public override IDeepCopyable DeepCopy()
		{
			return CopyTo(new DocumentManifest());
		}

		public override bool Matches(IDeepComparable other)
		{
			var otherT = other as DocumentManifest;
			if (otherT is null) return false;

			if (!base.Matches(otherT)) return false;
			if (!DeepComparable.Matches(MasterIdentifier, otherT.MasterIdentifier)) return false;
			if (!DeepComparable.Matches(Identifier, otherT.Identifier)) return false;
			if (!DeepComparable.Matches(Subject, otherT.Subject)) return false;
			if (!DeepComparable.Matches(Recipient, otherT.Recipient)) return false;
			if (!DeepComparable.Matches(Type, otherT.Type)) return false;
			if (!DeepComparable.Matches(Author, otherT.Author)) return false;
			if (!DeepComparable.Matches(CreatedElement, otherT.CreatedElement)) return false;
			if (!DeepComparable.Matches(SourceElement, otherT.SourceElement)) return false;
			if (!DeepComparable.Matches(StatusElement, otherT.StatusElement)) return false;
			if (!DeepComparable.Matches(Supercedes, otherT.Supercedes)) return false;
			if (!DeepComparable.Matches(DescriptionElement, otherT.DescriptionElement)) return false;
			if (!DeepComparable.Matches(Confidentiality, otherT.Confidentiality)) return false;
			if (!DeepComparable.Matches(Content, otherT.Content)) return false;

			return true;
		}

		public override bool IsExactly(IDeepComparable other)
		{
			var otherT = other as DocumentManifest;
			if (otherT is null) return false;

			if (!base.IsExactly(otherT)) return false;
			if (!DeepComparable.IsExactly(MasterIdentifier, otherT.MasterIdentifier)) return false;
			if (!DeepComparable.IsExactly(Identifier, otherT.Identifier)) return false;
			if (!DeepComparable.IsExactly(Subject, otherT.Subject)) return false;
			if (!DeepComparable.IsExactly(Recipient, otherT.Recipient)) return false;
			if (!DeepComparable.IsExactly(Type, otherT.Type)) return false;
			if (!DeepComparable.IsExactly(Author, otherT.Author)) return false;
			if (!DeepComparable.IsExactly(CreatedElement, otherT.CreatedElement)) return false;
			if (!DeepComparable.IsExactly(SourceElement, otherT.SourceElement)) return false;
			if (!DeepComparable.IsExactly(StatusElement, otherT.StatusElement)) return false;
			if (!DeepComparable.IsExactly(Supercedes, otherT.Supercedes)) return false;
			if (!DeepComparable.IsExactly(DescriptionElement, otherT.DescriptionElement)) return false;
			if (!DeepComparable.IsExactly(Confidentiality, otherT.Confidentiality)) return false;
			if (!DeepComparable.IsExactly(Content, otherT.Content)) return false;

			return true;
		}

	}

}
