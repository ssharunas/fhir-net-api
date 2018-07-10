using System;
using System.Collections.Generic;

namespace Hl7.Fhir.Model
{
	public static class CodeableConceptExtensions
	{
		public static Coding GetCoding(this CodeableConcept concept, string system)
		{
			if (concept != null)
				return GetCoding(concept.Coding, system);

			return null;
		}

		public static CodeableConcept GetFirst(this IList<CodeableConcept> concept, string system)
		{
			if (concept?.Count > 0)
			{
				foreach (var item in concept)
				{
					var coding = GetCoding(item.Coding, system);

					if (coding != null)
						return item;
				}
			}

			return null;
		}

		public static Coding GetCoding(this IList<CodeableConcept> concept, string system)
		{
			if (concept?.Count > 0)
			{
				foreach (var item in concept)
				{
					var coding = GetCoding(item.Coding, system);

					if (coding != null)
						return coding;
				}
			}

			return null;
		}

		public static Coding GetCoding(this IList<Coding> codings, string system = null)
		{
			if (codings != null && codings.Count > 0)
			{
				if (string.IsNullOrEmpty(system))
					return codings[0];

				foreach (Coding item in codings)
				{
					if (string.Equals(item.System, system, StringComparison.InvariantCultureIgnoreCase))
						return item;
				}
			}

			return null;
		}

		public static IList<Coding> GetValues(this IList<Coding> codings, string system)
		{
			IList<Coding> result = null;

			if (codings != null && codings.Count > 0)
			{
				foreach (Coding item in codings)
				{
					if (string.Equals(item.System, system, StringComparison.InvariantCultureIgnoreCase))
					{
						if (result == null)
							result = new List<Coding>();

						result.Add(item);
					}
				}
			}

			return result;
		}

		public static string GetCode(this CodeableConcept concept, string system = null)
		{
			if (concept != null)
				return GetCoding(concept.Coding, system)?.Code;

			return null;
		}

		public static string GetDisplay(this CodeableConcept concept, string system = null)
		{
			if (concept != null)
				return GetCoding(concept.Coding, system)?.Display;

			return null;
		}
	}
}