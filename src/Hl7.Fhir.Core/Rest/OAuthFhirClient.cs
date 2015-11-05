using System;

namespace Hl7.Fhir.Rest
{
	/// <summary>
	/// This class is not thread safe!
	/// </summary>
	public class OAuthFhirClient : IDisposable
	{
		private FhirClientWithContext _client;

		public OAuthFhirClient(Uri endpoint)
		{
			_client = new FhirClientWithContext(endpoint);
		}

		public OAuthFhirClient(string endpoint)
		{
			_client = new FhirClientWithContext(endpoint);
		}

		public FhirClientWithContext For(IContext context)
		{
			_client.Context = context;
			return _client;
		}

		public bool IsDisposed { get; private set; }

		protected virtual void Dispose(bool disposing)
		{
			if (!IsDisposed)
			{
				if (disposing)
				{
					_client = null;
				}

				IsDisposed = true;
			}
		}

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		~OAuthFhirClient()
		{
			Dispose(false);
		}
	}
}
