using Hl7.Fhir.Model;
using System;
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

		public void ConfirmSigned(ulong id)
		{
			ConfirmSigned("Documents/" + id + "/confirm");
		}

		public void ConfirmSigned(string location)
		{
			ConfirmSigned(new Uri(location, UriKind.Relative));
		}

		public void ConfirmSigned(Uri location)
		{
			location = makeAbsolute(location);
			var req = createFhirRequest(location, "POST");
			req.SetBody("☺", ResourceFormat.Octet);

			doRequest<Binary>(req, new[] { HttpStatusCode.OK, HttpStatusCode.NoContent }, resp => null, ResourceFormat.Unknown);
		}

		public string GetSignUrl(ulong id)
		{
			Uri location = makeAbsolute(new Uri(string.Format("Documents/{0}/sign", id), UriKind.Relative));
			var req = createFhirRequest(location, "GET");

			return doRequest(req, new[] { HttpStatusCode.OK, HttpStatusCode.NoContent }, resp => resp.AsLocation());
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
