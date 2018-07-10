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



using System;
using System.Collections.Generic;
using System.Linq;

namespace Hl7.Fhir.Model
{
	public interface IExtendable
	{
		List<Extension> Extension { get; set; }
	}

	public static class ExtensionExtensions
	{
		/// <summary>
		/// Return the first extension with the given uri, or null if none was found
		/// </summary>
		/// <param name="extendable"></param>
		/// <param name="uri"></param>
		/// <returns>The first uri, or null if no extension with the given uri was found.</returns>
		public static Extension GetExtension(this IExtendable extendable, string uri)
		{
			if (extendable.Extension != null)
			{
				return GetExtensions(extendable, uri).FirstOrDefault();
			}

			return null;
		}

		/// <summary>
		/// Return the first extension with the given uri, or null if none was found
		/// </summary>
		/// <param name="extendable"></param>
		/// <param name="uri"></param>
		/// <returns>The first uri, or null if no extension with the given uri was found.</returns>
		public static Element GetExtensionValue(this IExtendable extendable, string uri)
		{
			if (extendable.Extension != null)
			{
				var ext = GetExtensions(extendable, uri).FirstOrDefault();
				if (ext != null)
					return ext.Value;
			}

			return null;
		}

		/// <summary>
		/// Find all extensions with the given uri.
		/// </summary>
		/// <param name="extendable"></param>
		/// <param name="uri"></param>
		/// <returns>The list of extensions with a matching uri, or empty list if none were found.</returns>
		public static IEnumerable<Extension> GetExtensions(this IExtendable extendable, string uri)
		{
			if (extendable.Extension != null)
			{
				return extendable.Extension.Where(ext => ext.Url == uri);
			}

			return null;
		}


		/// <summary>
		/// Add an extension with the given uri and value
		/// </summary>
		/// <param name="extendable"></param>
		/// <param name="uri"></param>
		/// <param name="value"></param>
		/// <returns>The newly added Extension</returns>
		public static Extension AddExtension(this IExtendable extendable, string uri, Element value)
		{
			if (extendable.Extension == null)
				extendable.Extension = new List<Extension>();

			var newExtension = new Extension() { Url = uri, Value = value };
			extendable.Extension.Add(newExtension);

			return newExtension;
		}


		/// <summary>
		/// Remove all extensions with the current uri, if any.
		/// </summary>
		/// <param name="extendable"></param>
		/// <param name="uri"></param>
		public static void RemoveExtension(this IExtendable extendable, string uri)
		{
			if (extendable.Extension == null) return;

			var remove = extendable.Extension.Where(ext => ext.Url == uri).ToList();

			foreach (var ext in remove)
				extendable.Extension.Remove(ext);
		}

		/// <summary>
		/// Add an extension with the given uri and value, removing any pre-existsing extensions
		/// with the same uri.
		/// </summary>
		/// <param name="extendable"></param>
		/// <param name="uri"></param>
		/// <param name="value"></param>
		/// <returns>The newly added extension</returns>
		public static Extension SetExtension(this IExtendable extendable, string uri, Element value)
		{
			if (extendable.Extension == null)
				extendable.Extension = new List<Extension>();

			RemoveExtension(extendable, uri);

			return AddExtension(extendable, uri, value);
		}

		public static T SimpleAdapterHelper<T>(this Extension value)
			where T : Element
		{
			T item = value?.Value as T;

			if (item != null)
				return item;

			return null;
		}

		public static Extension ExtensionAdapter(Extension value)
		{
			return value;
		}

		public static string CodingCodeAdapter(Extension value)
		{
			return SimpleAdapterHelper<Coding>(value)?.Code;
		}

		public static string CodingDisplayAdapter(Extension value)
		{
			return SimpleAdapterHelper<Coding>(value)?.Display;
		}

		public static string ToString(this Extension value)
		{
			return SimpleAdapterHelper<FhirString>(value)?.Value;
		}

		public static decimal ToDecimal(this Extension value)
		{
			return SimpleAdapterHelper<FhirDecimal>(value)?.Value ?? 0;
		}

		public static DateTime ToDateTime(this string date)
		{
			DateTime result = DateTime.MinValue;

			if (!string.IsNullOrEmpty(date))
				DateTime.TryParse(date, out result);

			return result;
		}

		public static DateTime? ToNullableDateTime(this string date)
		{
			DateTime result = DateTime.MinValue;

			if (!string.IsNullOrEmpty(date) && DateTime.TryParse(date, out result))
				return result;

			return null;
		}

		public static ResourceReference ToResourceReference(this Extension value)
		{
			return SimpleAdapterHelper<ResourceReference>(value);
		}

		public static DateTime ToDateTime(this Extension value)
		{
			string date = SimpleAdapterHelper<Date>(value)?.Value;

			if (string.IsNullOrEmpty(date))
				date = SimpleAdapterHelper<FhirDateTime>(value)?.Value;

			return date.ToDateTime();
		}

		public static DateTime? ToNullableDateTime(this Extension value)
		{
			string date = SimpleAdapterHelper<Date>(value)?.Value;

			if (string.IsNullOrEmpty(date))
				date = SimpleAdapterHelper<FhirDateTime>(value)?.Value;

			return date.ToNullableDateTime();
		}

		public static string ToIdentifierValue(this Extension value)
		{
			return SimpleAdapterHelper<Identifier>(value)?.Value;
		}

		public static Identifier ToIdentifier(this Extension value)
		{
			return SimpleAdapterHelper<Identifier>(value);
		}

		public static CodeableConcept ToCodableConcept(this Extension value)
		{
			return SimpleAdapterHelper<CodeableConcept>(value);
		}

		public static string ToCodingCode(this Extension value)
		{
			return SimpleAdapterHelper<Coding>(value)?.Code;
		}

		public static string ToCodingDisplay(this Extension value)
		{
			return SimpleAdapterHelper<Coding>(value)?.Display;
		}

		public static string ToCodeValue(this Extension value)
		{
			return SimpleAdapterHelper<Code>(value)?.Value;
		}

		public static bool? ToBoolean(this Extension value)
		{
			return SimpleAdapterHelper<FhirBoolean>(value)?.Value;
		}

		public static int ToInteger(this Extension value)
		{
			return SimpleAdapterHelper<Integer>(value).Value ?? 0;
		}

		public static string ToAttachmentUrl(this Extension value)
		{
			return SimpleAdapterHelper<Attachment>(value)?.Url;
		}

		public static T GetValue<T>(this IList<Extension> extensions, string url, Func<Extension, T> adapter)
		{
			if (extensions != null && adapter != null)
			{
				foreach (Extension item in extensions)
				{
					if ((url?.StartsWith("#") == true && item.Url.EndsWith(url, StringComparison.InvariantCultureIgnoreCase)) ||
						string.Equals(item.Url, url, StringComparison.InvariantCultureIgnoreCase))
					{
						var result = adapter(item);
						if (result != null)
							return result;
					}
					else if (item.Extension != null)
					{
						var result = GetValue(item.Extension, url, adapter);
						if (result != null)
							return result;
					}
				}
			}

			return default(T);
		}

		public static IList<T> GetValues<T>(this IList<Extension> extensions, string url, Func<Extension, T> adapter)
		{
			List<T> result = null;
			GetValues(url, extensions, adapter, ref result);
			return result;
		}

		public static void GetValues<T>(string url, IList<Extension> extensions, Func<Extension, T> adapter, ref List<T> result)
		{
			if (extensions != null && adapter != null)
			{
				foreach (Extension item in extensions)
				{
					if (string.Equals(item.Url, url, StringComparison.InvariantCulture))
					{
						T value = adapter(item);

						if (value != null)
						{
							if (result == null)
								result = new List<T>();

							result.Add(value);
						}
					}
					else if (item.Extension != null)
						GetValues(url, item.Extension, adapter, ref result);
				}
			}
		}
	}
}