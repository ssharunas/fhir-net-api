using DevDefined.OAuth.Consumer;
using System;

namespace Hl7.Fhir.Rest
{
	/// <summary>
	/// Should be implemented depending on key type (RSA-sha1, HMAC-SHA1, etc)
	/// </summary>
	public abstract class OAuthContext : IContext, IDisposable
	{
		public bool IsDisposed { get; private set; }

		protected abstract OAuthConsumerContext GetContext();

		protected virtual IOAuthSession GetSession()
		{
			IOAuthConsumerContext consumerContext = GetContext();
			return new OAuthSession(consumerContext).EnableOAuthRequestBodyHashes();
		}

		public virtual IConsumerRequest GetRequest()
		{
			return GetSession().Request();
		}

		protected virtual void Dispose(bool disposing)
		{
			if (!IsDisposed)
			{
				IsDisposed = true;
			}
		}

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		~OAuthContext()
		{
			Dispose(false);
		}

	}
}
