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
	/// Details and position information for a physical place
	/// </summary>
	[FhirType("Location", IsResource = true)]
	[DataContract]
	public partial class Location : Hl7.Fhir.Model.Resource
	{
		/// <summary>
		/// Indicates whether the location is still in use
		/// </summary>
		[FhirEnumeration("LocationStatus")]
		public enum LocationStatus
		{
			/// <summary>
			/// The location is operational.
			/// </summary>
			[EnumLiteral("active")]
			Active,
			/// <summary>
			/// The location is temporarily closed.
			/// </summary>
			[EnumLiteral("suspended")]
			Suspended,
			/// <summary>
			/// The location is no longer used.
			/// </summary>
			[EnumLiteral("inactive")]
			Inactive,
		}

		/// <summary>
		/// Indicates whether a resource instance represents a specific location or a class of locations
		/// </summary>
		[FhirEnumeration("LocationMode")]
		public enum LocationMode
		{
			/// <summary>
			/// The Location resource represents a specific instance of a Location.
			/// </summary>
			[EnumLiteral("instance")]
			Instance,
			/// <summary>
			/// The Location represents a class of Locations.
			/// </summary>
			[EnumLiteral("kind")]
			Kind,
		}

		[FhirType("LocationPositionComponent")]
		[DataContract]
		public partial class LocationPositionComponent : Hl7.Fhir.Model.Element
		{
			/// <summary>
			/// Longitude as expressed in KML
			/// </summary>
			[FhirElement("longitude", InSummary = true, Order = 40)]
			[Cardinality(Min = 1, Max = 1)]
			[DataMember]
			public Hl7.Fhir.Model.FhirDecimal LongitudeElement { get; set; }

			/// <summary>
			/// Longitude as expressed in KML
			/// </summary>
			/// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
			[NotMapped]
			[IgnoreDataMember]
			public decimal? Longitude
			{
				get { return LongitudeElement?.Value; }
				set
				{
					if (value is null)
						LongitudeElement = null;
					else
						LongitudeElement = new Hl7.Fhir.Model.FhirDecimal(value);
				}
			}

			/// <summary>
			/// Latitude as expressed in KML
			/// </summary>
			[FhirElement("latitude", InSummary = true, Order = 50)]
			[Cardinality(Min = 1, Max = 1)]
			[DataMember]
			public Hl7.Fhir.Model.FhirDecimal LatitudeElement { get; set; }

			/// <summary>
			/// Latitude as expressed in KML
			/// </summary>
			/// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
			[NotMapped]
			[IgnoreDataMember]
			public decimal? Latitude
			{
				get { return LatitudeElement?.Value; }
				set
				{
					if (value is null)
						LatitudeElement = null;
					else
						LatitudeElement = new Hl7.Fhir.Model.FhirDecimal(value);
				}
			}

			/// <summary>
			/// Altitude as expressed in KML
			/// </summary>
			[FhirElement("altitude", InSummary = true, Order = 60)]
			[DataMember]
			public Hl7.Fhir.Model.FhirDecimal AltitudeElement { get; set; }

			/// <summary>
			/// Altitude as expressed in KML
			/// </summary>
			/// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
			[NotMapped]
			[IgnoreDataMember]
			public decimal? Altitude
			{
				get { return AltitudeElement?.Value; }
				set
				{
					if (value is null)
						AltitudeElement = null;
					else
						AltitudeElement = new Hl7.Fhir.Model.FhirDecimal(value);
				}
			}

			public override IDeepCopyable CopyTo(IDeepCopyable other)
			{
				var dest = other as LocationPositionComponent;

				if (dest != null)
				{
					base.CopyTo(dest);
					if (LongitudeElement != null) dest.LongitudeElement = (Hl7.Fhir.Model.FhirDecimal)LongitudeElement.DeepCopy();
					if (LatitudeElement != null) dest.LatitudeElement = (Hl7.Fhir.Model.FhirDecimal)LatitudeElement.DeepCopy();
					if (AltitudeElement != null) dest.AltitudeElement = (Hl7.Fhir.Model.FhirDecimal)AltitudeElement.DeepCopy();
					return dest;
				}
				else
					throw Error.Argument(nameof(other), "Can only copy to an object of the same type");
			}

			public override IDeepCopyable DeepCopy()
			{
				return CopyTo(new LocationPositionComponent());
			}

			public override bool Matches(IDeepComparable other)
			{
				var otherT = other as LocationPositionComponent;
				if (otherT is null) return false;

				if (!base.Matches(otherT)) return false;
				if (!DeepComparable.Matches(LongitudeElement, otherT.LongitudeElement)) return false;
				if (!DeepComparable.Matches(LatitudeElement, otherT.LatitudeElement)) return false;
				if (!DeepComparable.Matches(AltitudeElement, otherT.AltitudeElement)) return false;

				return true;
			}

			public override bool IsExactly(IDeepComparable other)
			{
				var otherT = other as LocationPositionComponent;
				if (otherT is null) return false;

				if (!base.IsExactly(otherT)) return false;
				if (!DeepComparable.IsExactly(LongitudeElement, otherT.LongitudeElement)) return false;
				if (!DeepComparable.IsExactly(LatitudeElement, otherT.LatitudeElement)) return false;
				if (!DeepComparable.IsExactly(AltitudeElement, otherT.AltitudeElement)) return false;

				return true;
			}

		}


		/// <summary>
		/// Unique code or number identifying the location to its users
		/// </summary>
		[FhirElement("identifier", Order = 70)]
		[DataMember]
		public Hl7.Fhir.Model.Identifier Identifier { get; set; }

		/// <summary>
		/// Name of the location as used by humans
		/// </summary>
		[FhirElement("name", Order = 80)]
		[DataMember]
		public Hl7.Fhir.Model.FhirString NameElement { get; set; }

		/// <summary>
		/// Name of the location as used by humans
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
		/// Description of the Location, which helps in finding or referencing the place
		/// </summary>
		[FhirElement("description", Order = 90)]
		[DataMember]
		public Hl7.Fhir.Model.FhirString DescriptionElement { get; set; }

		/// <summary>
		/// Description of the Location, which helps in finding or referencing the place
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
		/// Indicates the type of function performed at the location
		/// </summary>
		[FhirElement("type", Order = 100)]
		[DataMember]
		public Hl7.Fhir.Model.CodeableConcept Type { get; set; }

		/// <summary>
		/// Contact details of the location
		/// </summary>
		[FhirElement("telecom", Order = 110)]
		[Cardinality(Min = 0, Max = -1)]
		[DataMember]
		public List<Hl7.Fhir.Model.Contact> Telecom { get; set; }

		/// <summary>
		/// Physical location
		/// </summary>
		[FhirElement("address", Order = 120)]
		[DataMember]
		public Hl7.Fhir.Model.Address Address { get; set; }

		/// <summary>
		/// Physical form of the location
		/// </summary>
		[FhirElement("physicalType", Order = 130)]
		[DataMember]
		public Hl7.Fhir.Model.CodeableConcept PhysicalType { get; set; }

		/// <summary>
		/// The absolute geographic location
		/// </summary>
		[FhirElement("position", Order = 140)]
		[DataMember]
		public Hl7.Fhir.Model.Location.LocationPositionComponent Position { get; set; }

		/// <summary>
		/// The organization that is responsible for the provisioning and upkeep of the location
		/// </summary>
		[FhirElement("managingOrganization", Order = 150)]
		[References("Organization")]
		[DataMember]
		public Hl7.Fhir.Model.ResourceReference ManagingOrganization { get; set; }

		/// <summary>
		/// active | suspended | inactive
		/// </summary>
		[FhirElement("status", Order = 160)]
		[DataMember]
		public Code<Hl7.Fhir.Model.Location.LocationStatus> StatusElement { get; set; }

		/// <summary>
		/// active | suspended | inactive
		/// </summary>
		/// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
		[NotMapped]
		[IgnoreDataMember]
		public Hl7.Fhir.Model.Location.LocationStatus? Status
		{
			get { return StatusElement?.Value; }
			set
			{
				if (value is null)
					StatusElement = null;
				else
					StatusElement = new Code<Hl7.Fhir.Model.Location.LocationStatus>(value);
			}
		}

		/// <summary>
		/// Another Location which this Location is physically part of
		/// </summary>
		[FhirElement("partOf", Order = 170)]
		[References("Location")]
		[DataMember]
		public Hl7.Fhir.Model.ResourceReference PartOf { get; set; }

		/// <summary>
		/// instance | kind
		/// </summary>
		[FhirElement("mode", Order = 180)]
		[DataMember]
		public Code<Hl7.Fhir.Model.Location.LocationMode> ModeElement { get; set; }

		/// <summary>
		/// instance | kind
		/// </summary>
		/// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
		[NotMapped]
		[IgnoreDataMember]
		public Hl7.Fhir.Model.Location.LocationMode? Mode
		{
			get { return ModeElement?.Value; }
			set
			{
				if (value is null)
					ModeElement = null;
				else
					ModeElement = new Code<Hl7.Fhir.Model.Location.LocationMode>(value);
			}
		}

		public override IDeepCopyable CopyTo(IDeepCopyable other)
		{
			var dest = other as Location;

			if (dest != null)
			{
				base.CopyTo(dest);
				if (Identifier != null) dest.Identifier = (Hl7.Fhir.Model.Identifier)Identifier.DeepCopy();
				if (NameElement != null) dest.NameElement = (Hl7.Fhir.Model.FhirString)NameElement.DeepCopy();
				if (DescriptionElement != null) dest.DescriptionElement = (Hl7.Fhir.Model.FhirString)DescriptionElement.DeepCopy();
				if (Type != null) dest.Type = (Hl7.Fhir.Model.CodeableConcept)Type.DeepCopy();
				if (Telecom != null) dest.Telecom = new List<Hl7.Fhir.Model.Contact>(Telecom.DeepCopy());
				if (Address != null) dest.Address = (Hl7.Fhir.Model.Address)Address.DeepCopy();
				if (PhysicalType != null) dest.PhysicalType = (Hl7.Fhir.Model.CodeableConcept)PhysicalType.DeepCopy();
				if (Position != null) dest.Position = (Hl7.Fhir.Model.Location.LocationPositionComponent)Position.DeepCopy();
				if (ManagingOrganization != null) dest.ManagingOrganization = (Hl7.Fhir.Model.ResourceReference)ManagingOrganization.DeepCopy();
				if (StatusElement != null) dest.StatusElement = (Code<Hl7.Fhir.Model.Location.LocationStatus>)StatusElement.DeepCopy();
				if (PartOf != null) dest.PartOf = (Hl7.Fhir.Model.ResourceReference)PartOf.DeepCopy();
				if (ModeElement != null) dest.ModeElement = (Code<Hl7.Fhir.Model.Location.LocationMode>)ModeElement.DeepCopy();
				return dest;
			}
			else
				throw Error.Argument(nameof(other), "Can only copy to an object of the same type");
		}

		public override IDeepCopyable DeepCopy()
		{
			return CopyTo(new Location());
		}

		public override bool Matches(IDeepComparable other)
		{
			var otherT = other as Location;
			if (otherT is null) return false;

			if (!base.Matches(otherT)) return false;
			if (!DeepComparable.Matches(Identifier, otherT.Identifier)) return false;
			if (!DeepComparable.Matches(NameElement, otherT.NameElement)) return false;
			if (!DeepComparable.Matches(DescriptionElement, otherT.DescriptionElement)) return false;
			if (!DeepComparable.Matches(Type, otherT.Type)) return false;
			if (!DeepComparable.Matches(Telecom, otherT.Telecom)) return false;
			if (!DeepComparable.Matches(Address, otherT.Address)) return false;
			if (!DeepComparable.Matches(PhysicalType, otherT.PhysicalType)) return false;
			if (!DeepComparable.Matches(Position, otherT.Position)) return false;
			if (!DeepComparable.Matches(ManagingOrganization, otherT.ManagingOrganization)) return false;
			if (!DeepComparable.Matches(StatusElement, otherT.StatusElement)) return false;
			if (!DeepComparable.Matches(PartOf, otherT.PartOf)) return false;
			if (!DeepComparable.Matches(ModeElement, otherT.ModeElement)) return false;

			return true;
		}

		public override bool IsExactly(IDeepComparable other)
		{
			var otherT = other as Location;
			if (otherT is null) return false;

			if (!base.IsExactly(otherT)) return false;
			if (!DeepComparable.IsExactly(Identifier, otherT.Identifier)) return false;
			if (!DeepComparable.IsExactly(NameElement, otherT.NameElement)) return false;
			if (!DeepComparable.IsExactly(DescriptionElement, otherT.DescriptionElement)) return false;
			if (!DeepComparable.IsExactly(Type, otherT.Type)) return false;
			if (!DeepComparable.IsExactly(Telecom, otherT.Telecom)) return false;
			if (!DeepComparable.IsExactly(Address, otherT.Address)) return false;
			if (!DeepComparable.IsExactly(PhysicalType, otherT.PhysicalType)) return false;
			if (!DeepComparable.IsExactly(Position, otherT.Position)) return false;
			if (!DeepComparable.IsExactly(ManagingOrganization, otherT.ManagingOrganization)) return false;
			if (!DeepComparable.IsExactly(StatusElement, otherT.StatusElement)) return false;
			if (!DeepComparable.IsExactly(PartOf, otherT.PartOf)) return false;
			if (!DeepComparable.IsExactly(ModeElement, otherT.ModeElement)) return false;

			return true;
		}

	}

}
