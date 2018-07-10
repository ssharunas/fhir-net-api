using System;
using System.Collections.Generic;
using static Hl7.Fhir.Model.Composition;

namespace Hl7.Fhir.Model
{
	public static class ElementExtension
	{
		public static FhirDateTime ToFhirDateTime(this DateTime? value)
		{
			return ToFhirDateTime(value ?? DateTime.MinValue);
		}

		public static FhirDateTime ToFhirDateTime(this DateTime value)
		{
			if (value != DateTime.MinValue && value != DateTime.MaxValue)
				return new FhirDateTime(value.ToString("yyyy-MM-ddTHH:mm:ss"));

			return null;
		}

		/// <summary>
		/// Recursively search for Section with given Code
		/// </summary
		public static SectionComponent FindSection(this IList<SectionComponent> sections, string code)
		{
			SectionComponent result = null;

			if (sections?.Count > 0)
			{
				foreach (var item in sections)
				{
					if (item.Code.GetCode() == code)
						result = item;
					else if (item.Section?.Count > 0)
						result = FindSection(item.Section, code);

					if (result != null)
						break;
				}
			}

			return result;
		}
	}
}