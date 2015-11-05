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
					throw new ArgumentNullException("value", "Context must not be null!");

				_context = value;
			}
		}

		internal FhirClientWithContext(Uri endpoint) : base(endpoint)
		{
		}

		internal FhirClientWithContext(string endpoint) : base(endpoint)
		{
		}

		public Bundle Search<TResource>(string criteria = null) where TResource : Resource, new()
		{
			string[] search = null;
			if (criteria != null)
				search = new[] { criteria };

			return Search(typeof(TResource).GetCollectionName(), search);
		}

		protected override FhirRequest createFhirRequest(Uri location, string method = "GET")
		{
			var req = new OAuthFhirRequest(location, method, BeforeRequest, AfterResponse);

			if (Timeout != null) req.Timeout = Timeout.Value;

			return req;
		}

		protected override T doRequest<T>(FhirRequest request, HttpStatusCode[] success, Func<FhirResponse, T> onSuccess)
		{
			request.UseFormatParameter = this.UseFormatParam;

			OAuthFhirRequest oAuthRequest = request as OAuthFhirRequest;

			if (oAuthRequest == null)
				throw new ArgumentException("FhirClientWithContext can only work with OAuthFhirRequest's");

			FhirResponse response = oAuthRequest.GetResponse(PreferredFormat, Context);

			return HandleResponse(response, success, onSuccess);
		}
	}
}
