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
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace Hl7.Fhir.Rest
{
	public class FhirRequest
	{
		private Action<FhirRequest, HttpWebRequest> _beforeRequest;
		private Action<FhirResponse, FhirRequest, WebResponse, HttpWebRequest> _afterRequest;
		private HttpWebRequest _request;

		public FhirRequest(Uri location, string method, Action<FhirRequest, HttpWebRequest> beforeRequest, Action<FhirResponse, FhirRequest, WebResponse, HttpWebRequest> afterRequest)
		{
			if (method == null) throw Error.ArgumentNull(nameof(method));
			if (location == null) throw Error.ArgumentNull(nameof(location));
			if (!location.IsAbsoluteUri) throw Error.Argument(nameof(location), "Must be absolute uri");

			Method = method;
			Location = location;

			_beforeRequest = beforeRequest;
			_afterRequest = afterRequest;
		}

		public Guid ID { get; } = Guid.NewGuid();
		public Uri Location { get; }
		public Uri ContentLocation { get; internal set; }
		public string Method { get; }
		public int Timeout { get; set; } = 100000; // Default timeout is 100 seconds
		public byte[] Body { get; private set; }
		public string ContentType { get; /*private*/ set; }
		public string CategoryHeader { get; private set; }
		public bool IsForBundle { get; internal set; }

		internal void SetBody(string body, ResourceFormat format)
		{
			Body = Encoding.UTF8.GetBytes(body);
			ContentType = Rest.ContentType.BuildContentType(format, forBundle: false);
		}

		internal void SetBody(Resource resource, ResourceFormat format)
		{
			if (resource == null) throw Error.ArgumentNull(nameof(resource));

			if (resource is Binary binary)
			{
				Body = binary.Content;
				ContentType = binary.ContentType;
			}
			else
			{
				Body = format == ResourceFormat.Xml ?
					FhirSerializer.SerializeResourceToXmlBytes(resource, summary: false) :
					FhirSerializer.SerializeResourceToJsonBytes(resource, summary: false);

				ContentType = Rest.ContentType.BuildContentType(format, forBundle: false);
			}
		}

		internal void SetBody(Bundle bundle, ResourceFormat format)
		{
			if (bundle == null) throw Error.ArgumentNull(nameof(bundle));

			Body = format == ResourceFormat.Xml ?
				FhirSerializer.SerializeBundleToXmlBytes(bundle, summary: false) :
				FhirSerializer.SerializeBundleToJsonBytes(bundle, summary: false);

			ContentType = Hl7.Fhir.Rest.ContentType.BuildContentType(format, forBundle: true);
		}

		internal void SetBody(TagList tagList, ResourceFormat format)
		{
			if (tagList == null) throw Error.ArgumentNull(nameof(tagList));

			Body = format == ResourceFormat.Xml ?
				FhirSerializer.SerializeTagListToXmlBytes(tagList) :
				FhirSerializer.SerializeTagListToJsonBytes(tagList);

			ContentType = Rest.ContentType.BuildContentType(format, forBundle: false);
		}

		internal void SetTagsInHeader(IEnumerable<Tag> tags)
		{
			if (tags == null) throw Error.ArgumentNull(nameof(tags));

			CategoryHeader = HttpUtil.BuildCategoryHeader(tags);
		}

		/// <summary>
		/// Encodes property 'Body' to utf-8.
		/// </summary>
		public string GetBodyAsString()
		{
			return Body == null ? null : Encoding.UTF8.GetString(Body);
		}

		/// <summary>
		/// Returns HTTP request and headers.
		/// </summary>
		/// <returns></returns>
		public string GetHeadersAsString()
		{
			StringBuilder result = new StringBuilder();
			result.AppendLine($"{_request.Method} {Uri.UnescapeDataString(_request.RequestUri.ToString())} HTTP/{_request.ProtocolVersion}");

			foreach (var header in _request.Headers.AllKeys)
			{
				result.AppendLine($"{header}: {_request.Headers[header]}");
			}

			return result.ToString();
		}

		internal FhirResponse GetResponse(ResourceFormat? acceptFormat)
		{
			if (_request != null)
				throw new FhirOperationException("Reuse of FhirRequest! One request - one use, reusing is forbidden!");

			var location = new RestUrl(Location);

			_request = (HttpWebRequest)WebRequest.Create(location.Uri);
			_request.Timeout = Timeout;
			_request.Method = Method;
			_request.UserAgent = ".NET FhirClient for FHIR " + ModelInfo.Version;

			if (acceptFormat != null)
				_request.Accept = Rest.ContentType.BuildContentType(acceptFormat.Value, IsForBundle);

			if (CategoryHeader != null)
				_request.Headers[HttpUtil.CATEGORY] = CategoryHeader;

			if (Body != null)
			{
				_request.ContentType = ContentType;

				if (ContentLocation != null)
					_request.Headers[HttpRequestHeader.ContentLocation] = ContentLocation.ToString();
			}

			_beforeRequest?.Invoke(this, _request);

			if (Body != null)
			{
				Stream stream = _request.GetRequestStream();
				stream.Write(Body, 0, Body.Length);
				stream.Flush();
			}

			// Make sure the HttpResponse gets disposed!
			using (HttpWebResponse webResponse = GetResponseNoEx(_request))
			{
				var fhirResponse = new FhirResponse(webResponse);
				_afterRequest?.Invoke(fhirResponse, this, webResponse, _request);
				return fhirResponse;
			}
		}

		private HttpWebResponse GetResponseNoEx(WebRequest req)
		{
			try
			{
				return (HttpWebResponse)req.GetResponse();
			}
			catch (WebException ex)
			{
				if (ex.Response is HttpWebResponse resp)
					return resp;

				throw;
			}
		}
	}
}
