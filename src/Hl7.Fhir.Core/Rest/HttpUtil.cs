/* 
 * Copyright (c) 2014, Furore (info@furore.com) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using Hl7.Fhir.Model;
using Hl7.Fhir.Support;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Hl7.Fhir.Rest
{
	internal static class HttpUtil
	{
		public const string CONTENTLOCATION = "Content-Location";
		public const string LOCATION = "Location";
		public const string LASTMODIFIED = "Last-Modified";
		public const string CATEGORY = "Category";

		public const string RESTPARAM_FORMAT = "_format";

		public const string HISTORY_PARAM_SINCE = "_since";
		public const string HISTORY_PARAM_COUNT = Query.SEARCH_PARAM_COUNT;

		public static ICollection<Tag> ParseCategoryHeader(string value)
		{
			var result = new List<Tag>();

			if (!string.IsNullOrEmpty(value))
			{
				var categories = value.SplitNotInQuotes(',', StringSplitOptions.RemoveEmptyEntries);

				foreach (var category in categories)
				{
					var values = category.SplitNotInQuotes(';', StringSplitOptions.RemoveEmptyEntries);

					if (values.Count() >= 1)
					{
						var term = values.First();

						var pars = values.Skip(1).Select(v =>
						{
							var vsplit = v.Split('=');
							var item1 = vsplit[0].Trim();
							var item2 = vsplit.Length > 1 ? vsplit[1].Trim() : null;
							return new Tuple<string, string>(item1, item2);
						});

						var scheme = new Uri(pars.Where(t => t.Item1 == "scheme").Select(t => t.Item2.Trim('\"')).FirstOrDefault(), UriKind.RelativeOrAbsolute);
						var label = pars.Where(t => t.Item1 == "label").Select(t => t.Item2.Trim('\"')).FirstOrDefault();

						result.Add(new Tag(term, scheme, label));
					}
				}
			}

			return result;
		}

		public static string BuildCategoryHeader(IEnumerable<Tag> tags)
		{
			var result = new List<string>();

			foreach (var tag in tags)
			{
				StringBuilder sb = new StringBuilder();

				if (!string.IsNullOrEmpty(tag.Term))
				{
					if (tag.Term.Contains(",") || tag.Term.Contains(";"))
						throw Error.Argument(nameof(tags), "Found tag containing ',' or ';' - this will produce an inparsable Category header");

					sb.Append(tag.Term);
				}

				if (!string.IsNullOrEmpty(tag.Label))
					sb.AppendFormat("; label=\"{0}\"", tag.Label);

				sb.AppendFormat("; scheme=\"{0}\"", tag.Scheme.ToString());
				result.Add(sb.ToString());
			}

			return string.Join(", ", result);
		}

		private static Tuple<string, string> SplitParam(string param)
		{
			if (param is null) throw Error.ArgumentNull(nameof(param));

			string[] pair = param.Split('=');

			var key = Uri.UnescapeDataString(pair[0]);
			var value = pair.Length >= 2 ? string.Join("?", pair.Skip(1)) : null;
			if (value != null) value = Uri.UnescapeDataString(value);

			return new Tuple<string, string>(key, value);
		}

		/// <summary>
		/// Parses the possibly escaped key=value query parameter into a (key,value) Tuple
		/// </summary>
		/// <param name="query"></param>
		/// <returns>A Tuple&lt;string,string&gt; containing the key and value. Value maybe null if
		/// only the key was specified as a query parameter.</returns>
		public static List<Tuple<string, string>> SplitParams(string query)
		{
			var result = new List<Tuple<string, string>>();

			if (!string.IsNullOrEmpty(query))
			{
				query = query.TrimStart('?');

				var querySegments = query.Split(new[] { "&" }, StringSplitOptions.RemoveEmptyEntries);

				foreach (var segment in querySegments)
				{
					result.Add(SplitParam(segment));
				}
			}

			return result;
		}

		/// <summary>
		/// Converts a key,value pair into a query parameters, escaping the key and value
		/// of necessary.
		/// </summary>
		/// <param name="key"></param>
		/// <param name="value"></param>
		/// <returns></returns>
		private static string MakeParam(string key, string value = null)
		{
			if (key is null) throw Error.ArgumentNull(nameof(key));

			var result = Uri.EscapeDataString(key);

			if (value != null)
				result += "=" + Uri.EscapeDataString(value);

			return result;
		}

		private static string JoinParam(Tuple<string, string> kv)
		{
			if (kv is null) throw Error.ArgumentNull(nameof(kv));
			if (kv.Item1 is null) throw Error.Argument(nameof(kv), "Key in tuple may not be null");

			return MakeParam(kv.Item1, kv.Item2);
		}

		/// <summary>
		/// Builds a query string based on a set of key,value pairs
		/// </summary>
		/// <param name="pars"></param>
		/// <returns></returns>
		public static string JoinParams(IEnumerable<Tuple<string, string>> pars)
		{
			List<string> result = new List<string>();

			foreach (var kv in pars)
				result.Add(JoinParam(kv));

			return string.Join("&", result);
		}
	}
}
