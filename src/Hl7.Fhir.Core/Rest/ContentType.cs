/* 
 * Copyright (c) 2014, Furore (info@furore.com) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using Hl7.Fhir.Support;
using System.Linq;
using System.Text;


namespace Hl7.Fhir.Rest
{
	/// <summary>
	/// The supported formats for Fhir Resources
	/// </summary>
	public enum ResourceFormat
	{
		Xml = 1,
		Json = 2,
		Unknown = 3,
		Pdf = 4,
		Octet = 5
	}

	internal static class ContentType
	{
		public const string JSON_CONTENT_HEADER = "application/json+fhir";  // The formal FHIR mime type (still to be registered).
		public static readonly string[] JSON_CONTENT_HEADERS =
		{
			JSON_CONTENT_HEADER,
			"application/fhir+json",
			"application/json"
		};

		public const string XML_FHIR_CONTENT_HEADER = "application/xml+fhir";   // The formal FHIR mime type (still to be registered)
		public const string XML_CONTENT_HEADER = "application/xml";

		public static readonly string[] XML_CONTENT_HEADERS =
		{
			XML_FHIR_CONTENT_HEADER,
			"application/fhir+xml",
			"text/xml",
			XML_CONTENT_HEADER,
			"text/xml+fhir",
			ATOM_CONTENT_HEADER
		};

		public const string PDF_CONTENT_HEADER = "application/pdf";

		public static readonly string[] PDF_CONTENT_HEADERS = { PDF_CONTENT_HEADER };

		public const string ATOM_CONTENT_HEADER = "application/atom+xml";
		public const string OCTET_CONTENT_HEADER = "application/octet-stream";

		public const string FORMAT_PARAM_XML = "xml";
		public const string FORMAT_PARAM_JSON = "json";
		public const string FORMAT_PARAM_PDF = "pdf";


		/// <summary>
		/// Converts a format string to a ResourceFormat
		/// </summary>
		/// <param name="format">A format string, as used by the _format Url parameter</param>
		/// <returns>The Resource format or the special value Unknow if the format was unrecognized</returns>
		public static ResourceFormat GetResourceFormatFromFormatParam(string format)
		{
			if (string.IsNullOrEmpty(format)) return ResourceFormat.Unknown;

			var f = format.ToLowerInvariant();

			if (f == FORMAT_PARAM_JSON || JSON_CONTENT_HEADERS.Contains(f))
				return ResourceFormat.Json;
			if (f == FORMAT_PARAM_XML || XML_CONTENT_HEADERS.Contains(f))
				return ResourceFormat.Xml;

			if (f == FORMAT_PARAM_PDF || XML_CONTENT_HEADERS.Contains(f))
				return ResourceFormat.Pdf;

			return ResourceFormat.Unknown;
		}


		/// <summary>
		/// Converts a content type to a ResourceFormat
		/// </summary>
		/// <param name="contentType">The content type, as it appears on e.g. a Http Content-Type header</param>
		/// <returns></returns>
		public static ResourceFormat GetResourceFormatFromContentType(string contentType)
		{
			if (string.IsNullOrEmpty(contentType))
				return ResourceFormat.Unknown;

			var f = contentType.ToLowerInvariant();

			if (JSON_CONTENT_HEADERS.Contains(f))
				return ResourceFormat.Json;

			if (XML_CONTENT_HEADERS.Contains(f))
				return ResourceFormat.Xml;

			return ResourceFormat.Unknown;
		}


		public static string BuildContentType(ResourceFormat format, bool forBundle)
		{
			string contentType;

			if (format == ResourceFormat.Json)
				contentType = JSON_CONTENT_HEADER;
			else if (format == ResourceFormat.Xml && forBundle)
				contentType = ATOM_CONTENT_HEADER;
			else if (format == ResourceFormat.Xml && !forBundle)
				contentType = XML_FHIR_CONTENT_HEADER;
			else if (format == ResourceFormat.Pdf)
				contentType = PDF_CONTENT_HEADER;
			else if (format == ResourceFormat.Octet)
				contentType = OCTET_CONTENT_HEADER;
			else if (format == ResourceFormat.Unknown)
				return null;
			else
				throw Error.Argument(nameof(format), "Cannot determine content type for data format " + format);

			return contentType + ";charset=" + Encoding.UTF8.WebName;
		}

		public static string BuildFormatParam(ResourceFormat format)
		{
			if (format == ResourceFormat.Json)
				return FORMAT_PARAM_JSON;
			if (format == ResourceFormat.Xml)
				return FORMAT_PARAM_XML;
			if (format == ResourceFormat.Pdf)
				return FORMAT_PARAM_PDF;

			throw Error.Argument(nameof(format), "Cannot determine content type for data format " + format);
		}

		/// <summary>
		/// Checks whether a given content type is valid as a content type for resource data
		/// </summary>
		/// <param name="contentType">The content type, as it appears on e.g. a Http Content-Type header</param>
		/// <returns></returns>
		public static bool IsValidResourceContentType(string contentType)
		{
			var f = contentType.ToLowerInvariant();

			return JSON_CONTENT_HEADERS.Contains(f) || XML_CONTENT_HEADERS.Contains(f);
		}


		/// <summary>
		/// Checks whether a given content type is valid as a content type for bundles
		/// </summary>
		/// <param name="contentType">The content type, as it appears on e.g. a Http Content-Type header</param>
		/// <returns></returns>
		public static bool IsValidBundleContentType(string contentType)
		{
			var f = contentType.ToLowerInvariant();

			return (JSON_CONTENT_HEADERS.Contains(f) || f == ATOM_CONTENT_HEADER);
		}


		/// <summary>
		/// Checks whether a given format parameter is a valid as a content type for resource data
		/// </summary>
		/// <param name="paramValue">The content type, as it appears on the URL parameter</param>
		/// <returns></returns>
		public static bool IsValidFormatParam(string paramValue)
		{
			return GetResourceFormatFromFormatParam(paramValue) != ResourceFormat.Unknown;
		}

	}
}
