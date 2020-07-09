/*
  Copyright (c) 2011-2012, HL7, Inc
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

using Hl7.Fhir.Introspection;
using Hl7.Fhir.Support;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.Serialization;

namespace Hl7.Fhir.Model
{
	public enum SortOrder
	{
		Ascending,
		Descending
	}

	public partial class Query
	{
		public const string SEARCH_PARAM_ID = "_id";
		public const string SEARCH_PARAM_NARRATIVE = "_text";
		public const string SEARCH_PARAM_CONTENT = "_content";
		public const string SEARCH_PARAM_TAG = "_tag";
		public const string SEARCH_PARAM_PROFILE = "_profile";
		public const string SEARCH_PARAM_SECURITY = "_security";
		public const string SEARCH_PARAM_QUERY = "_query";
		public const string SEARCH_PARAM_TYPE = "_type";

		public const string SEARCH_PARAM_PAGE = "page";
		public const string SEARCH_PARAM_COUNT = "_count";
		public const string SEARCH_PARAM_INCLUDE = "_include";
		public const string SEARCH_PARAM_SORT = "_sort";
		public const string SEARCH_PARAM_SUMMARY = "_summary";

		/// <summary>
		/// List of all the search parameter that have some special meaning.
		/// Primarily used to filter to the non-special parameters.
		/// Notice that _id, _text, _content, _tag, _profile and _security are predefined in the standard,
		/// but not can still be parsed as regular criteria. So they are not in the RESERVED_PARAMETERS.
		/// </summary>
		internal static readonly string[] RESERVED_PARAMETERS = new[]
		{
			SEARCH_PARAM_QUERY,
			SEARCH_PARAM_TYPE,

			SEARCH_PARAM_COUNT,
			SEARCH_PARAM_INCLUDE,
			SEARCH_PARAM_SORT,
			SEARCH_PARAM_SUMMARY
		};

		/// <summary>
		/// List of additional core search criteria that are
		/// resource-independent
		/// </summary>
		private static readonly string[] CORE_SEARCH_CRITERIA = new[]
		{
			SEARCH_PARAM_ID,
			SEARCH_PARAM_NARRATIVE, SEARCH_PARAM_CONTENT,
			SEARCH_PARAM_TAG, SEARCH_PARAM_PROFILE, SEARCH_PARAM_SECURITY,
			SEARCH_PARAM_TYPE
		};

		private const string SEARCH_MODIF_ASCENDING = "asc";
		private const string SEARCH_MODIF_DESCENDING = "desc";

		internal const char SEARCH_CHAINSEPARATOR = '.';
		internal const char SEARCH_MODIFIERSEPARATOR = ':';

		public Query()
		{
			Identifier = "urn:uuid:" + Guid.NewGuid();
			Parameter = new List<Extension>();
		}

		/// <summary>
		/// Constructs a query object.
		/// </summary>
		/// <param name="collection">Type of resources to return (Patient, Compositsion, Observation, etc)</param>
		/// <param name="criteria">List of search parameters.</param>
		/// <param name="includes">These are used to include resources in the search result that the matched resources refer to</param>
		/// <param name="count">Limits the number of mathes returned per page in the pages search result.</param>
		public Query(string collection, IList<string> criteria, IList<string> includes = null, int? count = null)
			: this()
		{
			ResourceType = collection;

			if (count != null)
				Count = count;

			if (includes?.Count > 0)
				Include(includes);

			if (criteria?.Count > 0)
			{
				foreach (var crit in criteria)
					Where(crit);
			}
		}

		/// <summary>
		/// Gets or sets the special _query search parameter which asks the server to run a 
		/// specific named query instead of the standard FHIR search.
		/// </summary>
		[NotMapped]
		[IgnoreDataMember]
		public string QueryName
		{
			get { return GetSingleValue(SEARCH_PARAM_QUERY); }
			set { SetParameter(SEARCH_PARAM_QUERY, value); }
		}

		/// <summary>
		/// Gets or sets the special _type parameter, which limits the search to resources
		/// of a specific type. 
		/// </summary>
		/// <remarks>If this parameter is null, the search will be a non-restricted search
		/// across all resources.</remarks>
		[NotMapped]
		[IgnoreDataMember]
		public string ResourceType
		{
			get { return GetSingleValue(Query.SEARCH_PARAM_TYPE); }
			set { SetParameter(Query.SEARCH_PARAM_TYPE, value); }
		}

		/// <summary>
		/// Gets or sets the special _count search parameter, which limits the number
		/// of mathes returned per page in the pages search result
		/// </summary>
		/// <remark>The number of resources returned from the search may exceed this
		/// parameter, since additional _included resources for the matches are returned
		/// as well</remark>
		[NotMapped]
		[IgnoreDataMember]
		public int? Count
		{
			get
			{
				var count = GetSingleValue(Query.SEARCH_PARAM_COUNT);
				return count != null ? Int32.Parse(count) : (int?)null;
			}
			set
			{
				RemoveParameter(Query.SEARCH_PARAM_COUNT);
				if (value.HasValue)
					AddParameter(Query.SEARCH_PARAM_COUNT, value.ToString());
			}
		}

		/// <summary>
		/// Gets or sets the special page search parameter, which specifies starting
		/// page for returned results
		/// </summary>
		[NotMapped]
		[IgnoreDataMember]
		public int? Page
		{
			get
			{
				var page = GetSingleValue(Query.SEARCH_PARAM_PAGE);
				return page != null ? int.Parse(page) : (int?)null;
			}
			set
			{
				RemoveParameter(Query.SEARCH_PARAM_PAGE);
				if (value.HasValue)
					AddParameter(Query.SEARCH_PARAM_PAGE, value.ToString());
			}
		}

		/// <summary>
		/// Gets or sets the special _summary search parameter. If set to true,
		/// the server will not return all elements in each matching resource, but just
		/// the most important ones.
		/// </summary>
		[NotMapped]
		[IgnoreDataMember]
		public bool Summary
		{
			get { return GetSingleValue(Query.SEARCH_PARAM_SUMMARY) == "true"; }
			set { SetParameter(Query.SEARCH_PARAM_SUMMARY, value ? "true" : "false"); }
		}

		/// <summary>
		/// Gets or sets the _sort parameter, to modify the sort order of the search result.
		/// Uses a tuple (name, sortorder).
		/// </summary>
		[NotMapped]
		[IgnoreDataMember]
		public List<Tuple<string, SortOrder>> Sort
		{
			get
			{
				var ext = Parameter.WithName(SEARCH_PARAM_SORT);
				if (ext is null) return null;

				var result = new List<Tuple<string, SortOrder>>();

				foreach (var extension in ext)
				{
					var key = ExtractParamKey(extension);
					var sort = key.EndsWith(SEARCH_MODIF_DESCENDING) ? SortOrder.Descending : SortOrder.Ascending;
					var name = ExtractParamValue(extension);

					result.Add(Tuple.Create(name, sort));
				}

				return result;
			}
			set
			{
				RemoveParameter(SEARCH_PARAM_SORT);

				foreach (var sort in value)
				{
					var modif = sort.Item2 == SortOrder.Ascending ? SEARCH_MODIF_ASCENDING : SEARCH_MODIF_DESCENDING;
					var name = sort.Item1;

					AddParameter(SEARCH_PARAM_SORT + SEARCH_MODIFIERSEPARATOR + modif, name);
				}
			}
		}

		/// <summary>
		/// Returns a modifiable collection of _include parameters. These are used to include
		/// resources in the search result that the matched resources refer to.
		/// </summary>
		[NotMapped]
		[IgnoreDataMember]
		public ICollection<string> Includes
		{
			get { return new IncludeCollection(Parameter); }
		}

		/// <summary>
		/// Returns a modifiable collection of all the parameters that are not reserved parameters.
		/// These include the resource-independent parameters _id, _text, _content, _tag, _profile and _security.
		/// </summary>
		[NotMapped]
		[IgnoreDataMember]
		public ICollection<Extension> Criteria
		{
			get { return new List<Extension>(Parameter.Where(p => !p.IsReserved())); }
		}

		/// <summary>
		/// Add a parameter with a given key and value.
		/// </summary>
		/// <param name="key">The name of the parameter, possibly including the modifier</param>
		/// <param name="value">The string representation of the parameter value</param>
		/// <returns>this (Query), so you can chain AddParameter calls</returns>

		public Query AddParameter(string key, long value)
		{
			return AddParameter(key, value.ToString(CultureInfo.InvariantCulture));
		}

		public Query AddParameter(string key, string value)
		{
			if (key is null) throw Error.ArgumentNull(nameof(key));
			if (value is null) throw Error.ArgumentNull(nameof(value));

			if (Parameter is null) Parameter = new List<Extension>();

			Parameter.Add(BuildParamExtension(key, value));

			return this;
		}

		/// <summary>
		/// Remove a parameter with a given name.
		/// </summary>
		/// <param name="key">The name of the parameter, possibly including the modifier</param>
		/// <remarks><para>If the key includes a modifier, only that parameter will be removed. If
		/// the key is just a parameter name, all parameters with that name will be removed, regardless
		/// of modifiers attached to it.</para><para>No exception is thrown when the parameters were not found and nothing was removed.</para></remarks>
		internal void RemoveParameter(string key)
		{
			if (key is null) throw Error.ArgumentNull(nameof(key));

			if (Parameter is null) return;
			Parameter.RemoveAll(ParamsExtensions.MatchParam(key));
		}

		private void SetParameter(string key, string value)
		{
			RemoveParameter(key);
			AddParameter(key, value);
		}

		/// <summary>
		/// Searches for a parameter with the given name, and returns the
		/// value of the parameter, if a single result was found.
		/// </summary>
		/// <param name="key">The name of the parameter, possibly including the modifier</param>
		/// <returns>The value of the parameter with the given name. Will throw an 
		/// exception if multiple parameters with the given name exist.</returns>
		/// <remarks>If the key includes a modifier, the search will be for a parameter with the
		/// given modifier, otherwise any parameter with the given name will be matched.</remarks>
		public string GetSingleValue(string key)
		{
			if (key is null) throw Error.ArgumentNull(nameof(key));
			if (Parameter is null) return null;

			var extension = Parameter.SingleWithName(key);
			return ExtractParamValue(extension);
		}

		/// <summary>
		/// Searches for all parameter with the given name, and returns a list
		/// with the values of those parameters.
		/// </summary>
		/// <param name="key">The name of the parameter, possibly including the modifier</param>
		/// <returns>A list of values of the parameters with the given name.</returns>
		/// <remarks>If the key includes a modifier, the search will be for parameters with the
		/// given modifier, otherwise any parameter with the given name will be matched.</remarks>
		public IEnumerable<string> GetValues(string key)
		{
			if (key is null) throw Error.ArgumentNull(nameof(key));
			if (Parameter is null) return null;

			var extension = Parameter.WithName(key);
			return extension.Select(ext => ExtractParamValue(ext));
		}


		/// <summary>
		/// Build an Extension instance with an Url indicating a
		/// FHIR search parameter and a ValueString set to the given value.
		/// </summary>
		/// <param name="key"></param>
		/// <param name="value"></param>
		/// <returns></returns>
		public static Extension BuildParamExtension(string key, string value)
		{
			if (key is null) throw Error.ArgumentNull(nameof(key));
			if (value is null) throw Error.ArgumentNull(nameof(value));

			return new Extension(BuildParamUri(key), new FhirString(value));
		}

		public static string ExtractParamValue(Extension extension)
		{
			var element = extension != null ? extension.Value as FhirString : null;
			var value = element != null ? element.Value : null;
			return value;
		}

		/// <summary>
		/// Constructs an Url indicating a FHIR search parameter
		/// </summary>
		/// <param name="paramKey"></param>
		/// <returns></returns>
		public static string BuildParamUri(string paramKey)
		{
			if (paramKey is null) throw Error.ArgumentNull(nameof(paramKey));

			return XmlNs.FHIR_URL_SEARCHPARAM + "#" + paramKey;
		}

		private const string PARAMETER_URL_AND_FRAGMENT = XmlNs.FHIR_URL_SEARCHPARAM + "#";

		/// <summary>
		/// Given a Extension containing a FHIR search parameter, returns the
		/// name (and possibly modifier) of the parameter
		/// </summary>
		/// <param name="paramExt">An Extension containing a FHIR search parameter</param>
		/// <returns>The name of the parameter, possibly including a modifier</returns>
		public static string ExtractParamKey(Extension paramExt)
		{
			if (paramExt is null) throw Error.ArgumentNull(nameof(paramExt));
			if (paramExt.Url is null) throw Error.Argument(nameof(paramExt), "Extension.url cannot be null");

			var uriString = paramExt.Url.ToString();

			if (uriString.StartsWith(PARAMETER_URL_AND_FRAGMENT))
				return uriString.Remove(0, PARAMETER_URL_AND_FRAGMENT.Length);
			else
				return null;
		}

		public static Query For<TResource>()
			where TResource : Resource
		{
			return new Query(ModelInfo.GetCollectionName<TResource>(), null);
		}

		public Query Include(string path)
		{
			Includes.Add(path);
			return this;
		}

		public Query Include(ICollection<string> paths)
		{
			if (paths?.Count > 0)
			{
				var includes = Includes;

				foreach (var path in paths)
					includes.Add(path);
			}

			return this;
		}

		public Query Where(string criterium)
		{
			var keyVal = criterium.SplitLeft('=');
			AddParameter(keyVal.Item1, keyVal.Item2);

			return this;
		}

		public Query Where(string key, string value)
		{
			AddParameter(key, value);
			return this;
		}

		public Query WithName(string customQueryName)
		{
			if (customQueryName is null) throw Error.ArgumentNull(nameof(customQueryName));

			QueryName = customQueryName;
			return this;
		}

		public Query OrderBy(string paramName, SortOrder order = SortOrder.Ascending)
		{
			if (paramName is null) throw Error.ArgumentNull(nameof(paramName));

			var sort = Sort ?? new List<Tuple<string, SortOrder>>();
			sort.Add(Tuple.Create(paramName, order));
			Sort = sort;

			return this;
		}

		/// <summary>
		/// Results per page
		/// </summary>
		public Query SetCount(int count)
		{
			Count = count;
			return this;
		}

		public Query SummaryOnly(bool summaryOnly = true)
		{
			Summary = summaryOnly;
			return this;
		}

		/// <summary>
		/// Page index
		/// </summary>
		public Query SetPage(int page, bool isZeroBased = false)
		{
			Page = page + (isZeroBased ? 0 : 1);
			return this;
		}

		private class IncludeCollection : ICollection<string>
		{
			public IncludeCollection(List<Extension> wrapped)
			{
				Wrapped = wrapped;
				_matcher = ParamsExtensions.MatchParam(SEARCH_PARAM_INCLUDE);
			}

			public List<Extension> Wrapped { get; private set; }
			private Predicate<Extension> _matcher;

			public void Add(string item)
			{
				Wrapped.Add(BuildParamExtension(SEARCH_PARAM_INCLUDE, item));
			}

			public void Clear()
			{
				Wrapped.RemoveAll(_matcher);
			}

			public bool Contains(string item)
			{
				return Wrapped.Any(ext => _matcher(ext) && ExtractParamValue(ext) == item);
			}

			public void CopyTo(string[] array, int arrayIndex)
			{
				Wrapped.FindAll(_matcher).Select(ext => ExtractParamValue(ext))
					.ToArray<string>().CopyTo(array, arrayIndex);
			}

			public int Count
			{
				get { return Wrapped.FindAll(_matcher).Count; }
			}

			public bool IsReadOnly
			{
				get { return false; }
			}

			public bool Remove(string item)
			{
				var found = Wrapped.FirstOrDefault(ext => _matcher(ext) && ExtractParamValue(ext) == item);
				if (found is null) return false;

				return Wrapped.Remove(found);
			}

			public IEnumerator<string> GetEnumerator()
			{
				return Wrapped.FindAll(_matcher).Select(ext => ExtractParamValue(ext)).GetEnumerator();
			}

			IEnumerator IEnumerable.GetEnumerator()
			{
				return GetEnumerator();
			}
		}
	}

	internal static class ParamsExtensions
	{
		public static IEnumerable<Extension> WithName(this IEnumerable<Extension> pars, string key)
		{
			var match = MatchParam(key);
			return pars.Where(par => match(par));
		}

		public static Extension SingleWithName(this IEnumerable<Extension> pars, string key)
		{
			var match = MatchParam(key);
			return pars.SingleOrDefault(par => match(par));
		}

		internal static Predicate<Extension> MatchParam(string key)
		{
			var param = Query.BuildParamUri(key).ToString();

			// PCL does not have an overload on this routine that takes a char, only string
			if (key.Contains(Query.SEARCH_MODIFIERSEPARATOR.ToString()))
			{
				return (Extension ext) => ext.Url.ToString() == param;
			}
			else
			{
				// Add a modifier separator to the end if there's no modifier,
				// this way we can assure we don't match just a prefix 
				// (e.g. a param _querySpecial when looking for_query)
				var paramWithSep = Query.BuildParamUri(key + Query.SEARCH_MODIFIERSEPARATOR).ToString();
				return (Extension ext) => ext.Url.ToString().StartsWith(paramWithSep) ||
								(ext.Url.ToString() == param);
			}
		}

		internal static bool IsReserved(this Extension parameter)
		{
			string key = Query.ExtractParamKey(parameter).Split(new[] { Query.SEARCH_MODIFIERSEPARATOR }).First();
			return Query.RESERVED_PARAMETERS.Contains(key);
		}
	}
}
