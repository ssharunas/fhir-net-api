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
	/// An instance of a manufactured thing that is used in the provision of healthcare
	/// </summary>
	[FhirType("Device", IsResource = true)]
	[DataContract]
	public partial class Device : Hl7.Fhir.Model.Resource
	{
		/// <summary>
		/// Instance id from manufacturer, owner and others
		/// </summary>
		[FhirElement("identifier", Order = 70)]
		[Cardinality(Min = 0, Max = -1)]
		[DataMember]
		public List<Hl7.Fhir.Model.Identifier> Identifier { get; set; }

		/// <summary>
		/// What kind of device this is
		/// </summary>
		[FhirElement("type", Order = 80)]
		[Cardinality(Min = 1, Max = 1)]
		[DataMember]
		public Hl7.Fhir.Model.CodeableConcept Type { get; set; }

		/// <summary>
		/// Name of device manufacturer
		/// </summary>
		[FhirElement("manufacturer", Order = 90)]
		[DataMember]
		public Hl7.Fhir.Model.FhirString ManufacturerElement { get; set; }

		/// <summary>
		/// Name of device manufacturer
		/// </summary>
		/// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
		[NotMapped]
		[IgnoreDataMember]
		public string Manufacturer
		{
			get { return ManufacturerElement != null ? ManufacturerElement.Value : null; }
			set
			{
				if (value == null)
					ManufacturerElement = null;
				else
					ManufacturerElement = new Hl7.Fhir.Model.FhirString(value);
			}
		}

		/// <summary>
		/// Model id assigned by the manufacturer
		/// </summary>
		[FhirElement("model", Order = 100)]
		[DataMember]
		public Hl7.Fhir.Model.FhirString ModelElement { get; set; }

		/// <summary>
		/// Model id assigned by the manufacturer
		/// </summary>
		/// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
		[NotMapped]
		[IgnoreDataMember]
		public string Model
		{
			get { return ModelElement != null ? ModelElement.Value : null; }
			set
			{
				if (value == null)
					ModelElement = null;
				else
					ModelElement = new Hl7.Fhir.Model.FhirString(value);
			}
		}

		/// <summary>
		/// Version number (i.e. software)
		/// </summary>
		[FhirElement("version", Order = 110)]
		[DataMember]
		public Hl7.Fhir.Model.FhirString VersionElement { get; set; }

		/// <summary>
		/// Version number (i.e. software)
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
		/// Date of expiry of this device (if applicable)
		/// </summary>
		[FhirElement("expiry", Order = 120)]
		[DataMember]
		public Hl7.Fhir.Model.Date ExpiryElement { get; set; }

		/// <summary>
		/// Date of expiry of this device (if applicable)
		/// </summary>
		/// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
		[NotMapped]
		[IgnoreDataMember]
		public string Expiry
		{
			get { return ExpiryElement != null ? ExpiryElement.Value : null; }
			set
			{
				if (value == null)
					ExpiryElement = null;
				else
					ExpiryElement = new Hl7.Fhir.Model.Date(value);
			}
		}

		/// <summary>
		/// FDA Mandated Unique Device Identifier
		/// </summary>
		[FhirElement("udi", Order = 130)]
		[DataMember]
		public Hl7.Fhir.Model.FhirString UdiElement { get; set; }

		/// <summary>
		/// FDA Mandated Unique Device Identifier
		/// </summary>
		/// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
		[NotMapped]
		[IgnoreDataMember]
		public string Udi
		{
			get { return UdiElement != null ? UdiElement.Value : null; }
			set
			{
				if (value == null)
					UdiElement = null;
				else
					UdiElement = new Hl7.Fhir.Model.FhirString(value);
			}
		}

		/// <summary>
		/// Lot number of manufacture
		/// </summary>
		[FhirElement("lotNumber", Order = 140)]
		[DataMember]
		public Hl7.Fhir.Model.FhirString LotNumberElement { get; set; }

		/// <summary>
		/// Lot number of manufacture
		/// </summary>
		/// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
		[NotMapped]
		[IgnoreDataMember]
		public string LotNumber
		{
			get { return LotNumberElement != null ? LotNumberElement.Value : null; }
			set
			{
				if (value == null)
					LotNumberElement = null;
				else
					LotNumberElement = new Hl7.Fhir.Model.FhirString(value);
			}
		}

		/// <summary>
		/// Organization responsible for device
		/// </summary>
		[FhirElement("owner", Order = 150)]
		[References("Organization")]
		[DataMember]
		public Hl7.Fhir.Model.ResourceReference Owner { get; set; }

		/// <summary>
		/// Where the resource is found
		/// </summary>
		[FhirElement("location", Order = 160)]
		[References("Location")]
		[DataMember]
		public Hl7.Fhir.Model.ResourceReference Location { get; set; }

		/// <summary>
		/// If the resource is affixed to a person
		/// </summary>
		[FhirElement("patient", Order = 170)]
		[References("Patient")]
		[DataMember]
		public Hl7.Fhir.Model.ResourceReference Patient { get; set; }

		/// <summary>
		/// Details for human/organization for support
		/// </summary>
		[FhirElement("contact", Order = 180)]
		[Cardinality(Min = 0, Max = -1)]
		[DataMember]
		public List<Hl7.Fhir.Model.Contact> Contact { get; set; }

		/// <summary>
		/// Network address to contact device
		/// </summary>
		[FhirElement("url", Order = 190)]
		[DataMember]
		public Hl7.Fhir.Model.FhirUri UrlElement { get; set; }

		/// <summary>
		/// Network address to contact device
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
			var dest = other as Device;

			if (dest != null)
			{
				base.CopyTo(dest);
				if (Identifier != null) dest.Identifier = new List<Hl7.Fhir.Model.Identifier>(Identifier.DeepCopy());
				if (Type != null) dest.Type = (Hl7.Fhir.Model.CodeableConcept)Type.DeepCopy();
				if (ManufacturerElement != null) dest.ManufacturerElement = (Hl7.Fhir.Model.FhirString)ManufacturerElement.DeepCopy();
				if (ModelElement != null) dest.ModelElement = (Hl7.Fhir.Model.FhirString)ModelElement.DeepCopy();
				if (VersionElement != null) dest.VersionElement = (Hl7.Fhir.Model.FhirString)VersionElement.DeepCopy();
				if (ExpiryElement != null) dest.ExpiryElement = (Hl7.Fhir.Model.Date)ExpiryElement.DeepCopy();
				if (UdiElement != null) dest.UdiElement = (Hl7.Fhir.Model.FhirString)UdiElement.DeepCopy();
				if (LotNumberElement != null) dest.LotNumberElement = (Hl7.Fhir.Model.FhirString)LotNumberElement.DeepCopy();
				if (Owner != null) dest.Owner = (Hl7.Fhir.Model.ResourceReference)Owner.DeepCopy();
				if (Location != null) dest.Location = (Hl7.Fhir.Model.ResourceReference)Location.DeepCopy();
				if (Patient != null) dest.Patient = (Hl7.Fhir.Model.ResourceReference)Patient.DeepCopy();
				if (Contact != null) dest.Contact = new List<Hl7.Fhir.Model.Contact>(Contact.DeepCopy());
				if (UrlElement != null) dest.UrlElement = (Hl7.Fhir.Model.FhirUri)UrlElement.DeepCopy();
				return dest;
			}
			else
				throw Error.Argument(nameof(other), "Can only copy to an object of the same type");
		}

		public override IDeepCopyable DeepCopy()
		{
			return CopyTo(new Device());
		}

		public override bool Matches(IDeepComparable other)
		{
			var otherT = other as Device;
			if (otherT == null) return false;

			if (!base.Matches(otherT)) return false;
			if (!DeepComparable.Matches(Identifier, otherT.Identifier)) return false;
			if (!DeepComparable.Matches(Type, otherT.Type)) return false;
			if (!DeepComparable.Matches(ManufacturerElement, otherT.ManufacturerElement)) return false;
			if (!DeepComparable.Matches(ModelElement, otherT.ModelElement)) return false;
			if (!DeepComparable.Matches(VersionElement, otherT.VersionElement)) return false;
			if (!DeepComparable.Matches(ExpiryElement, otherT.ExpiryElement)) return false;
			if (!DeepComparable.Matches(UdiElement, otherT.UdiElement)) return false;
			if (!DeepComparable.Matches(LotNumberElement, otherT.LotNumberElement)) return false;
			if (!DeepComparable.Matches(Owner, otherT.Owner)) return false;
			if (!DeepComparable.Matches(Location, otherT.Location)) return false;
			if (!DeepComparable.Matches(Patient, otherT.Patient)) return false;
			if (!DeepComparable.Matches(Contact, otherT.Contact)) return false;
			if (!DeepComparable.Matches(UrlElement, otherT.UrlElement)) return false;

			return true;
		}

		public override bool IsExactly(IDeepComparable other)
		{
			var otherT = other as Device;
			if (otherT == null) return false;

			if (!base.IsExactly(otherT)) return false;
			if (!DeepComparable.IsExactly(Identifier, otherT.Identifier)) return false;
			if (!DeepComparable.IsExactly(Type, otherT.Type)) return false;
			if (!DeepComparable.IsExactly(ManufacturerElement, otherT.ManufacturerElement)) return false;
			if (!DeepComparable.IsExactly(ModelElement, otherT.ModelElement)) return false;
			if (!DeepComparable.IsExactly(VersionElement, otherT.VersionElement)) return false;
			if (!DeepComparable.IsExactly(ExpiryElement, otherT.ExpiryElement)) return false;
			if (!DeepComparable.IsExactly(UdiElement, otherT.UdiElement)) return false;
			if (!DeepComparable.IsExactly(LotNumberElement, otherT.LotNumberElement)) return false;
			if (!DeepComparable.IsExactly(Owner, otherT.Owner)) return false;
			if (!DeepComparable.IsExactly(Location, otherT.Location)) return false;
			if (!DeepComparable.IsExactly(Patient, otherT.Patient)) return false;
			if (!DeepComparable.IsExactly(Contact, otherT.Contact)) return false;
			if (!DeepComparable.IsExactly(UrlElement, otherT.UrlElement)) return false;

			return true;
		}

	}

}
