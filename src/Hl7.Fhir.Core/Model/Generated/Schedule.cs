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
	/// A schedule that specifies an event that may occur multiple times
	/// </summary>
	[FhirType("Schedule")]
	[DataContract]
	public partial class Schedule : Hl7.Fhir.Model.Element
	{
		/// <summary>
		/// A unit of time (units from UCUM)
		/// </summary>
		[FhirEnumeration("UnitsOfTime")]
		public enum UnitsOfTime
		{
			/// <summary>
			/// second.
			/// </summary>
			[EnumLiteral("s")]
			S,
			/// <summary>
			/// minute.
			/// </summary>
			[EnumLiteral("min")]
			Min,
			/// <summary>
			/// hour.
			/// </summary>
			[EnumLiteral("h")]
			H,
			/// <summary>
			/// day.
			/// </summary>
			[EnumLiteral("d")]
			D,
			/// <summary>
			/// week.
			/// </summary>
			[EnumLiteral("wk")]
			Wk,
			/// <summary>
			/// month.
			/// </summary>
			[EnumLiteral("mo")]
			Mo,
			/// <summary>
			/// year.
			/// </summary>
			[EnumLiteral("a")]
			A,
		}

		/// <summary>
		/// Real world event that the schedule relates to
		/// </summary>
		[FhirEnumeration("EventTiming")]
		public enum EventTiming
		{
			/// <summary>
			/// event occurs [duration] before the hour of sleep (or trying to).
			/// </summary>
			[EnumLiteral("HS")]
			HS,
			/// <summary>
			/// event occurs [duration] after waking.
			/// </summary>
			[EnumLiteral("WAKE")]
			WAKE,
			/// <summary>
			/// event occurs [duration] before a meal (from the Latin ante cibus).
			/// </summary>
			[EnumLiteral("AC")]
			AC,
			/// <summary>
			/// event occurs [duration] before breakfast (from the Latin ante cibus matutinus).
			/// </summary>
			[EnumLiteral("ACM")]
			ACM,
			/// <summary>
			/// event occurs [duration] before lunch (from the Latin ante cibus diurnus).
			/// </summary>
			[EnumLiteral("ACD")]
			ACD,
			/// <summary>
			/// event occurs [duration] before dinner (from the Latin ante cibus vespertinus).
			/// </summary>
			[EnumLiteral("ACV")]
			ACV,
			/// <summary>
			/// event occurs [duration] after a meal (from the Latin post cibus).
			/// </summary>
			[EnumLiteral("PC")]
			PC,
			/// <summary>
			/// event occurs [duration] after breakfast (from the Latin post cibus matutinus).
			/// </summary>
			[EnumLiteral("PCM")]
			PCM,
			/// <summary>
			/// event occurs [duration] after lunch (from the Latin post cibus diurnus).
			/// </summary>
			[EnumLiteral("PCD")]
			PCD,
			/// <summary>
			/// event occurs [duration] after dinner (from the Latin post cibus vespertinus).
			/// </summary>
			[EnumLiteral("PCV")]
			PCV,
		}

		[FhirType("ScheduleRepeatComponent")]
		[DataContract]
		public partial class ScheduleRepeatComponent : Hl7.Fhir.Model.Element
		{
			/// <summary>
			/// Event occurs frequency times per duration
			/// </summary>
			[FhirElement("frequency", InSummary = true, Order = 40)]
			[DataMember]
			public Hl7.Fhir.Model.Integer FrequencyElement { get; set; }

			/// <summary>
			/// Event occurs frequency times per duration
			/// </summary>
			/// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
			[NotMapped]
			[IgnoreDataMember]
			public int? Frequency
			{
				get { return FrequencyElement?.Value; }
				set
				{
					if (value is null)
						FrequencyElement = null;
					else
						FrequencyElement = new Hl7.Fhir.Model.Integer(value);
				}
			}

			/// <summary>
			/// HS | WAKE | AC | ACM | ACD | ACV | PC | PCM | PCD | PCV - common life events
			/// </summary>
			[FhirElement("when", InSummary = true, Order = 50)]
			[DataMember]
			public Code<Hl7.Fhir.Model.Schedule.EventTiming> WhenElement { get; set; }

			/// <summary>
			/// HS | WAKE | AC | ACM | ACD | ACV | PC | PCM | PCD | PCV - common life events
			/// </summary>
			/// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
			[NotMapped]
			[IgnoreDataMember]
			public Hl7.Fhir.Model.Schedule.EventTiming? When
			{
				get { return WhenElement?.Value; }
				set
				{
					if (value is null)
						WhenElement = null;
					else
						WhenElement = new Code<Hl7.Fhir.Model.Schedule.EventTiming>(value);
				}
			}

			/// <summary>
			/// Repeating or event-related duration
			/// </summary>
			[FhirElement("duration", InSummary = true, Order = 60)]
			[Cardinality(Min = 1, Max = 1)]
			[DataMember]
			public Hl7.Fhir.Model.FhirDecimal DurationElement { get; set; }

			/// <summary>
			/// Repeating or event-related duration
			/// </summary>
			/// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
			[NotMapped]
			[IgnoreDataMember]
			public decimal? Duration
			{
				get { return DurationElement?.Value; }
				set
				{
					if (value is null)
						DurationElement = null;
					else
						DurationElement = new Hl7.Fhir.Model.FhirDecimal(value);
				}
			}

			/// <summary>
			/// s | min | h | d | wk | mo | a - unit of time (UCUM)
			/// </summary>
			[FhirElement("units", InSummary = true, Order = 70)]
			[Cardinality(Min = 1, Max = 1)]
			[DataMember]
			public Code<Hl7.Fhir.Model.Schedule.UnitsOfTime> UnitsElement { get; set; }

			/// <summary>
			/// s | min | h | d | wk | mo | a - unit of time (UCUM)
			/// </summary>
			/// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
			[NotMapped]
			[IgnoreDataMember]
			public Hl7.Fhir.Model.Schedule.UnitsOfTime? Units
			{
				get { return UnitsElement?.Value; }
				set
				{
					if (value is null)
						UnitsElement = null;
					else
						UnitsElement = new Code<Hl7.Fhir.Model.Schedule.UnitsOfTime>(value);
				}
			}

			/// <summary>
			/// Number of times to repeat
			/// </summary>
			[FhirElement("count", InSummary = true, Order = 80)]
			[DataMember]
			public Hl7.Fhir.Model.Integer CountElement { get; set; }

			/// <summary>
			/// Number of times to repeat
			/// </summary>
			/// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
			[NotMapped]
			[IgnoreDataMember]
			public int? Count
			{
				get { return CountElement?.Value; }
				set
				{
					if (value is null)
						CountElement = null;
					else
						CountElement = new Hl7.Fhir.Model.Integer(value);
				}
			}

			/// <summary>
			/// When to stop repeats
			/// </summary>
			[FhirElement("end", InSummary = true, Order = 90)]
			[DataMember]
			public Hl7.Fhir.Model.FhirDateTime EndElement { get; set; }

			/// <summary>
			/// When to stop repeats
			/// </summary>
			/// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
			[NotMapped]
			[IgnoreDataMember]
			public string End
			{
				get { return EndElement?.Value; }
				set
				{
					if (value is null)
						EndElement = null;
					else
						EndElement = new Hl7.Fhir.Model.FhirDateTime(value);
				}
			}

			public override IDeepCopyable CopyTo(IDeepCopyable other)
			{
				var dest = other as ScheduleRepeatComponent;

				if (dest != null)
				{
					base.CopyTo(dest);
					if (FrequencyElement != null) dest.FrequencyElement = (Hl7.Fhir.Model.Integer)FrequencyElement.DeepCopy();
					if (WhenElement != null) dest.WhenElement = (Code<Hl7.Fhir.Model.Schedule.EventTiming>)WhenElement.DeepCopy();
					if (DurationElement != null) dest.DurationElement = (Hl7.Fhir.Model.FhirDecimal)DurationElement.DeepCopy();
					if (UnitsElement != null) dest.UnitsElement = (Code<Hl7.Fhir.Model.Schedule.UnitsOfTime>)UnitsElement.DeepCopy();
					if (CountElement != null) dest.CountElement = (Hl7.Fhir.Model.Integer)CountElement.DeepCopy();
					if (EndElement != null) dest.EndElement = (Hl7.Fhir.Model.FhirDateTime)EndElement.DeepCopy();
					return dest;
				}
				else
					throw Error.Argument(nameof(other), "Can only copy to an object of the same type");
			}

			public override IDeepCopyable DeepCopy()
			{
				return CopyTo(new ScheduleRepeatComponent());
			}

			public override bool Matches(IDeepComparable other)
			{
				var otherT = other as ScheduleRepeatComponent;
				if (otherT is null) return false;

				if (!base.Matches(otherT)) return false;
				if (!DeepComparable.Matches(FrequencyElement, otherT.FrequencyElement)) return false;
				if (!DeepComparable.Matches(WhenElement, otherT.WhenElement)) return false;
				if (!DeepComparable.Matches(DurationElement, otherT.DurationElement)) return false;
				if (!DeepComparable.Matches(UnitsElement, otherT.UnitsElement)) return false;
				if (!DeepComparable.Matches(CountElement, otherT.CountElement)) return false;
				if (!DeepComparable.Matches(EndElement, otherT.EndElement)) return false;

				return true;
			}

			public override bool IsExactly(IDeepComparable other)
			{
				var otherT = other as ScheduleRepeatComponent;
				if (otherT is null) return false;

				if (!base.IsExactly(otherT)) return false;
				if (!DeepComparable.IsExactly(FrequencyElement, otherT.FrequencyElement)) return false;
				if (!DeepComparable.IsExactly(WhenElement, otherT.WhenElement)) return false;
				if (!DeepComparable.IsExactly(DurationElement, otherT.DurationElement)) return false;
				if (!DeepComparable.IsExactly(UnitsElement, otherT.UnitsElement)) return false;
				if (!DeepComparable.IsExactly(CountElement, otherT.CountElement)) return false;
				if (!DeepComparable.IsExactly(EndElement, otherT.EndElement)) return false;

				return true;
			}

		}


		/// <summary>
		/// When the event occurs
		/// </summary>
		[FhirElement("event", InSummary = true, Order = 40)]
		[Cardinality(Min = 0, Max = -1)]
		[DataMember]
		public List<Hl7.Fhir.Model.Period> Event { get; set; }

		/// <summary>
		/// Only if there is none or one event
		/// </summary>
		[FhirElement("repeat", InSummary = true, Order = 50)]
		[DataMember]
		public Hl7.Fhir.Model.Schedule.ScheduleRepeatComponent Repeat { get; set; }

		public override IDeepCopyable CopyTo(IDeepCopyable other)
		{
			var dest = other as Schedule;

			if (dest != null)
			{
				base.CopyTo(dest);
				if (Event != null) dest.Event = new List<Hl7.Fhir.Model.Period>(Event.DeepCopy());
				if (Repeat != null) dest.Repeat = (Hl7.Fhir.Model.Schedule.ScheduleRepeatComponent)Repeat.DeepCopy();
				return dest;
			}
			else
				throw Error.Argument(nameof(other), "Can only copy to an object of the same type");
		}

		public override IDeepCopyable DeepCopy()
		{
			return CopyTo(new Schedule());
		}

		public override bool Matches(IDeepComparable other)
		{
			var otherT = other as Schedule;
			if (otherT is null) return false;

			if (!base.Matches(otherT)) return false;
			if (!DeepComparable.Matches(Event, otherT.Event)) return false;
			if (!DeepComparable.Matches(Repeat, otherT.Repeat)) return false;

			return true;
		}

		public override bool IsExactly(IDeepComparable other)
		{
			var otherT = other as Schedule;
			if (otherT is null) return false;

			if (!base.IsExactly(otherT)) return false;
			if (!DeepComparable.IsExactly(Event, otherT.Event)) return false;
			if (!DeepComparable.IsExactly(Repeat, otherT.Repeat)) return false;

			return true;
		}

	}

}
