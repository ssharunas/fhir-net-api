/* 
 * Copyright (c) 2014, Furore (info@furore.com) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using Hl7.Fhir.Model;
using Hl7.Fhir.Serialization;
using Hl7.Fhir.Support;
using System;
using System.IO;
using System.Net;
using System.Text;

namespace Hl7.Fhir.Rest
{
	public class FhirResponse
	{
		private string _headers;

		public HttpStatusCode Result { get; }
		public string ContentType { get; }
		public Encoding CharacterEncoding { get; }

		public string ContentLocation { get; }
		public string Location { get; }
		public string LastModified { get; }
		public string Category { get; }

		public Uri ResponseUri { get; }

		public byte[] Body { get; }

		// Can't hold onto this as it gets disposed pretty quick.
		//public HttpWebResponse Response { get; set; }

		internal FhirResponse(HttpWebResponse response)
		{
			System.Net.Mime.ContentType contentType = null;
			if (!string.IsNullOrEmpty(response.ContentType))
				contentType = new System.Net.Mime.ContentType(response.ContentType);

			ResponseUri = response.ResponseUri;
			Result = response.StatusCode;
			ContentType = contentType?.MediaType;
			ContentLocation = response.Headers[HttpUtil.CONTENTLOCATION];
			LastModified = response.Headers[HttpUtil.LASTMODIFIED];
			Location = response.Headers[HttpUtil.LOCATION];
			Category = response.Headers[HttpUtil.CATEGORY];

			_headers = ExtractHeaders(response);
			CharacterEncoding = GetContentEncoding(contentType?.CharSet);
			Body = ReadAllFromStream(response.GetResponseStream(), response.ContentLength); //Read response body
		}

		/// <summary>
		/// Encodes property 'Body' to utf-8.
		/// </summary>
		public string GetBodyAsString()
		{
			if (Body != null)
			{
				// If no encoding is specified, default to utf8
				return (CharacterEncoding ?? Encoding.UTF8).GetString(Body);
			}

			return null;
		}

		/// <summary>
		/// Parses body as Tag list.
		/// </summary>
		public TagList GetBodyAsTagList()
		{
			return GetParsedBody(FhirParser.ParseTagListFromXml, FhirParser.ParseTagListFromJson);
		}

		/// <summary>
		/// Returns parsed body.
		/// </summary>
		public ResourceEntry<T> GetBodyAsEntry<T>(FhirRequest request) where T : Resource, new()
		{
			var result = GetBodyAsEntry(ModelInfo.GetCollectionName<T>());

			if (result.Resource is T)
				return (ResourceEntry<T>)result;

			throw new FhirOperationException($"Received a resource of type {result.Resource.GetType().Name}, expected a {typeof(T).Name} resource", request, this);
		}

		/// <summary>
		/// Returns parsed body.
		/// </summary>
		public ResourceEntry GetBodyAsEntry(string collection)
		{
			Resource resource = null;

			if (collection == nameof(Binary))
			{
				resource = new Binary
				{
					Content = Body,
					ContentType = ContentType
				};
			}
			else
			{
				resource = GetParsedBody(FhirParser.ParseResourceFromXml, FhirParser.ParseResourceFromJson);
			}

			ResourceEntry result = ResourceEntry.Create(resource);

			var location = Location ?? ContentLocation ?? ResponseUri.OriginalString;

			if (!string.IsNullOrEmpty(location))
			{
				ResourceIdentity reqId = new ResourceIdentity(location);

				// Set the id to the location, without the version specific part
				if (!string.IsNullOrEmpty(reqId.Collection) && !string.IsNullOrEmpty(reqId.Id))//Outcome does not have collection nor ID
					result.Id = reqId.WithoutVersion();

				// If the content location has version information, set to SelfLink to it
				if (reqId.VersionId != null)
					result.SelfLink = reqId;
			}

			if (!string.IsNullOrEmpty(LastModified))
				result.LastUpdated = DateTimeOffset.Parse(LastModified);

			if (!string.IsNullOrEmpty(Category))
				result.Tags = HttpUtil.ParseCategoryHeader(Category);

			result.Title = "A " + resource.GetType().Name + " resource";

			return result;
		}

		/// <summary>
		/// Returns parsed Bundle body.
		/// </summary>
		public Bundle GetBodyAsBundle()
		{
			return GetParsedBody(FhirParser.ParseBundleFromXml, FhirParser.ParseBundleFromJson);
		}

		/// <summary>
		/// Returns HTTP response headers.
		/// </summary>
		/// <returns></returns>
		public string GetHeadersAsString()
		{
			return _headers;
		}

		private T GetParsedBody<T>(Func<string, T> xmlParser, Func<string, T> jsonParser) where T : class
		{
			ResourceFormat format = Rest.ContentType.GetResourceFormatFromContentType(ContentType);

			switch (format)
			{
				case ResourceFormat.Json:
					return jsonParser(GetBodyAsString());
				case ResourceFormat.Xml:
					return xmlParser(GetBodyAsString());
			}

			throw Error.Format("Cannot decode body: unrecognized content type " + ContentType);
		}

		private Encoding GetContentEncoding(string charSet)
		{
			if (!string.IsNullOrEmpty(charSet))
				return Encoding.GetEncoding(charSet);

			return null;
		}

		private string ExtractHeaders(HttpWebResponse response)
		{
			StringBuilder result = new StringBuilder();

			if (response != null)
			{
				result.AppendLine($"HTTP/{response.ProtocolVersion} {(int)response.StatusCode} {response.StatusDescription}");
				foreach (var header in response.Headers.AllKeys)
				{
					result.AppendLine($"{header}: {response.Headers[header]}");
				}
			}

			return result.ToString();
		}

		private byte[] ReadAllFromStream(Stream stream, long contentLength)
		{
			if (contentLength == 0)
				return null;

			var memory = new MemoryStream(Math.Max(0, (int)contentLength));
			stream.CopyTo(memory);
			return memory.ToArray();
		}
	}
}
