using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using DevDefined.OAuth.Consumer;

namespace Hl7.Fhir.Rest
{
	public class OAuthFhirRequest : FhirRequest
	{
		public OAuthFhirRequest(Uri location, string method = "GET",
			Action<FhirRequest, HttpWebRequest> beforeRequest = null, Action<FhirResponse, WebResponse> afterRequest = null)
			: base(location, method, beforeRequest, afterRequest)
		{
		}

		public FhirResponse GetResponse(ResourceFormat? acceptFormat, IContext context)
		{
			if (context == null)
				return GetResponse(acceptFormat);

			FhirResponse fhirResponse = null;
			bool needsFormatParam = UseFormatParameter && acceptFormat.HasValue;

			var location = new RestUrl(Location);

			if (needsFormatParam)
				location.AddParam(HttpUtil.RESTPARAM_FORMAT, Rest.ContentType.BuildFormatParam(acceptFormat.Value));

			var request = context.GetRequest();
			if (request != null)
			{
				IDictionary headers = new Dictionary<string, string>();

				//System.Diagnostics.Debug.WriteLine("{0}: {1}", Method, location.Uri.OriginalString);
				request.ForMethod(Method).ForUri(location.Uri);

				if (acceptFormat != null && acceptFormat.Value != ResourceFormat.Unknown && !UseFormatParameter)
					request.WithAcceptHeader(Rest.ContentType.BuildContentType(acceptFormat.Value, ForBundle));

				if (Body != null)
				{
					request
						.WithRawContentType(ContentType)
						.WithRawContent(Body);

					if (ContentLocation != null)
						headers[HttpRequestHeader.ContentLocation.ToString()] = ContentLocation.ToString();
				}

				if (!string.IsNullOrEmpty(CategoryHeader))
					headers[HttpUtil.CATEGORY] = CategoryHeader;

				if (headers.Count > 0)
					request.WithHeaders(headers);

#if !PORTABLE45
				request.Timeout = Timeout;
#endif

				HttpWebRequest webRequest = request.ToWebRequest();

				if (_beforeRequest != null) _beforeRequest(this, webRequest);

				// Make sure the HttpResponse gets disposed!
				using (HttpWebResponse webResponse = (HttpWebResponse)webRequest.GetResponseNoEx())
				{
					fhirResponse = FhirResponse.FromHttpWebResponse(webResponse);
					if (_afterRequest != null) _afterRequest(fhirResponse, webResponse);
				}
			}
			else
			{
				System.Diagnostics.Debug.WriteLine("Ignored request, because context.GetRequest() did not return it: {0}: {1}", Method, location.Uri.OriginalString);
			}

			return fhirResponse;
		}
	}
}
