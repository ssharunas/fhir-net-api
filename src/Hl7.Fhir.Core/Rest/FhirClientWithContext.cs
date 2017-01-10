using Hl7.Fhir.Model;
using System;
using System.Collections.Generic;
using System.Net;

namespace Hl7.Fhir.Rest
{
	/// <summary>
	/// Internally used clas for FHIR requests. Must be created with OAuthFhirClient
	/// </summary>
	public class FhirClientWithContext : FhirClient
	{
		private IContext _context;

		public IContext Context
		{
			get { return _context; }
			internal set
			{
				if (value == null)
					throw new ArgumentNullException(nameof(value), "Context must not be null!");

				_context = value;
			}
		}

		internal FhirClientWithContext(Uri endpoint) : base(endpoint)
		{
		}

		internal FhirClientWithContext(string endpoint) : base(endpoint)
		{
		}

		public ResourceEntry<TResource> Get<TResource>(ulong id) where TResource : Resource, new()
		{
			return Read<TResource>(typeof(TResource).Name + "/" + id);
		}

		public Bundle Search<TResource>(string criteria = null) where TResource : Resource, new()
		{
			string[] search = null;
			if (criteria != null)
				search = new[] { criteria };

			return Search(typeof(TResource).GetCollectionName(), search);
		}

		public ResourceEntry<Binary> Pdf(Uri location)
		{
			location = makeAbsolute(location);
			var req = createFhirRequest(location, "GET");

			return doRequest(req, HttpStatusCode.OK, resp => resp.BodyAsEntry<Binary>(), ResourceFormat.Pdf);
		}

		public ResourceEntry<Binary> Pdf(string location)
		{
			return Pdf(new Uri(location, UriKind.Relative));
		}

		public ResourceEntry<Binary> Pdf(ulong id)
		{
			return Pdf("Documents/" + id);
		}

		public void ConfirmSigned(ulong id, byte[] pdf = null)
		{
			ConfirmSigned("Documents/" + id + "/confirm", pdf);
		}

		public void ConfirmSigned(string location, byte[] pdf = null)
		{
			ConfirmSigned(new Uri(location, UriKind.Relative), pdf);
		}

		public IList<string> ConfirmSignedTransaction(ulong id)
		{
			Uri location = makeAbsolute(new Uri("Documents/confirm", UriKind.Relative));
			location = new Uri(location.ToString() + "?transaction=" + id);

			var req = createFhirRequest(location, "POST");
			req.SetBody("☺", ResourceFormat.Octet);

			return doRequest<IList<string>>(req, new[] { HttpStatusCode.OK, HttpStatusCode.NoContent }, resp =>
			{
				string body = resp.BodyAsString();
				if (!string.IsNullOrEmpty(body))
					return body.Trim('[', ']').Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

				return null;
			}, ResourceFormat.Unknown);
		}

		public void ConfirmSigned(Uri location, byte[] pdf = null)
		{
			location = makeAbsolute(location);
			var req = createFhirRequest(location, "POST");

			ResourceFormat format = ResourceFormat.Octet;
			if (pdf?.Length > 0)
			{
				format = ResourceFormat.Pdf;
				Binary file = new Binary();
				file.ContentType = "application/pdf";
				file.Content = pdf;
				req.SetBody(file, format);
			}
			else req.SetBody("☺", format);

			doRequest<Binary>(req, new[] { HttpStatusCode.OK, HttpStatusCode.NoContent }, resp => null, format);
		}

		public string GetSignUrl(ulong id, string returnURL)
		{
			Uri location = makeAbsolute(new Uri(string.Format("Documents/{0}/sign", id), UriKind.Relative));
			location = new Uri(location.ToString() + "?id=" + string.Join(",", id) + "&returnUrl=" + (returnURL ?? "https://google.lt"));

			var req = createFhirRequest(location, "GET");

			return doRequest(req, new[] { HttpStatusCode.OK, HttpStatusCode.NoContent }, resp => resp.AsLocation());
		}

		public string GetSignUrl(IList<ulong> id, string returnURL)
		{
			if (id != null)
			{
				Uri location = makeAbsolute(new Uri(string.Format("Documents/sign"), UriKind.Relative));
				location = new Uri(location.ToString() + "?id=" + string.Join(",", id) + "&returnUrl=" + (returnURL ?? "https://google.lt"));

				var req = createFhirRequest(location, "GET");
				return doRequest(req, new[] { HttpStatusCode.OK, HttpStatusCode.NoContent }, resp => resp.AsLocation());
			}

			return null;
		}

		protected override FhirRequest createFhirRequest(Uri location, string method)
		{
			var req = new OAuthFhirRequest(location, method, BeforeRequest, AfterResponse);

			if (Timeout != null) req.Timeout = Timeout.Value;

			return req;
		}

		protected override T doRequest<T>(FhirRequest request, HttpStatusCode[] success, Func<FhirResponse, T> onSuccess, ResourceFormat? format = null)
		{
			request.UseFormatParameter = UseFormatParam;

			OAuthFhirRequest oAuthRequest = request as OAuthFhirRequest;

			if (oAuthRequest == null)
				throw new ArgumentException("FhirClientWithContext can only work with OAuthFhirRequest's");

			FhirResponse response = oAuthRequest.GetResponse(format ?? PreferredFormat, Context);

			return HandleResponse(response, request, success, onSuccess);
		}
	}
}
