using Hl7.Fhir.Model;
using System;
using System.Collections.Generic;
using System.Net;
using System.Reflection;
using System.Collections;

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

		/// <summary>
		/// Get Resource by ID
		/// </summary>
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

		public string SearchRaw(string resource, IList<string> criteria)
		{
			var query = toQuery(resource, criteria, null, null);

			RestUrl url = new RestUrl(Endpoint);
			url = url.Search(query);

			FhirRequest req = createFhirRequest(makeAbsolute(url.Uri), "GET");

			return doRequest(req, HttpStatusCode.OK, resp => resp.BodyAsString(), ResourceFormat.Unknown);
		}

		/// <summary>
		/// Confirm document signing. Use only if using not RC component.
		/// </summary>
		/// <param name="id">Composition ID</param>
		/// <param name="pdf">Signed PDF data</param>
		public void ConfirmSigned(ulong id, byte[] pdf = null)
		{
			ConfirmSigned("Documents/" + id + "/confirm", pdf);
		}

		/// <summary>
		/// Confirm document signing. Use only if using not RC component.
		/// </summary>
		/// <param name="id">Confirmation Url</param>
		/// <param name="pdf">Signed PDF data</param>
		public void ConfirmSigned(string location, byte[] pdf = null)
		{
			ConfirmSigned(new Uri(location, UriKind.Relative), pdf);
		}

		/// <summary>
		/// Confirm document signing. Use only if using not RC component.
		/// </summary>
		/// <param name="location">Confirmation Url</param>
		/// <param name="pdf">Signed PDF data</param>
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

		/// <summary>
		/// Confirm all documents in transaction.
		/// </summary>
		/// <param name="id">Transaction ID</param
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

		/// <summary>
		/// Returns Sign URL for RC component
		/// </summary>
		/// <param name="id">Composition ID</param>
		/// <param name="returnURL">Where to redirect after signing</param>
		public string GetSignUrl(ulong id, string returnURL)
		{
			Uri location = makeAbsolute(new Uri(string.Format("Documents/{0}/sign", id), UriKind.Relative));
			location = new Uri(location.ToString() + "?id=" + string.Join(",", id) + "&returnUrl=" + (returnURL ?? "https://google.lt"));

			var req = createFhirRequest(location, "GET");

			return doRequest(req, new[] { HttpStatusCode.OK, HttpStatusCode.NoContent }, resp => resp.AsLocation());
		}

		/// <summary>
		/// Returns Sign URL for bundle for RC component
		/// </summary>
		/// <param name="id">Compositions IDs</param>
		/// <param name="returnURL">Where to redirect after signing</param>
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

		/// <summary>
		/// Get Document. DOES NOT WORK AS OF 2018-07-23.
		/// But it should work
		/// </summary>
		/// <param name="id">Composition ID</param>
		public Bundle GetDocumentBundle(ulong id)
		{
			Uri location = makeAbsolute(new Uri(string.Format("Documents/{0}", id), UriKind.Relative));
			var req = createFhirRequest(location, "GET");
			return doRequest(req, new[] { HttpStatusCode.OK }, resp => resp.BodyAsBundle(), ResourceFormat.Xml);
		}

		/// <summary>
		/// Get full composition with references. Not all resources are returned.
		/// </summary>
		/// <param name="id">Composition ESPBI ID</param>
		public Composition GetDocument(long id)
		{
			Bundle bundle = SearchById("Composition", id.ToString(), new[] { "Composition.section.*" }, 100);

			Composition result = null;
			if (bundle?.TotalResults > 0)
			{
				IList<Resource> visited = new List<Resource>();

				for (int i = 0; i < bundle.Entries.Count; i++)
				{
					ResourceEntry entry = bundle.Entries[i] as ResourceEntry;
					if (entry != null)
					{
						if (result == null && entry.Resource is Composition)
						{
							result = entry.Resource as Composition;
							result.ID = entry.GetEntryID();
						}

						CheckProperties(entry.Resource, bundle, visited);
					}
				}
			}

			return result;
		}

		private void CheckProperties(object root, Bundle bundle, IList<Resource> visited)
		{
			if (root != null && !root.GetType().Namespace.StartsWith("System") && !(root is Patient) && !(root is Organization) && !(root is Practitioner))
			{
				if (root is Resource)
				{
					if (visited.Contains(root as Resource)) return;
					else visited.Add(root as Resource);
				}

				Type type = root.GetType();
				IList<PropertyInfo> properties = type.GetProperties();

				if (properties?.Count > 0)
				{
					foreach (PropertyInfo property in properties)
					{
						object value = property.GetValue(root, null);

						if (value != null)
						{
							if (value is ResourceReference)
							{
								ResourceReference reference = value as ResourceReference;
								if (reference.ReferenceResource == null)
								{
									reference.ReferenceResource = GetResource(reference, bundle.Entries);
									CheckProperties(reference.ReferenceResource, bundle, visited);
								}
							}
							else if (value is IList<ResourceReference>)
							{
								IList<ResourceReference> list = value as IList<ResourceReference>;
								foreach (var listItem in list)
								{
									if (listItem.ReferenceResource == null)
									{
										listItem.ReferenceResource = GetResource(listItem, bundle.Entries);
										CheckProperties(listItem.ReferenceResource, bundle, visited);
									}
								}
							}
							else if (typeof(IEnumerable).IsAssignableFrom(property.PropertyType) && property.PropertyType != typeof(string))
							{
								IEnumerable list = value as IEnumerable;
								foreach (var listItem in list)
								{
									CheckProperties(listItem, bundle, visited);
								}
							}
							else
							{
								CheckProperties(value, bundle, visited);
							}
						}
					}
				}
			}
		}

		private Resource GetResource(ResourceReference value, IList<BundleEntry> entries)
		{
			Resource result = null;

			if (value != null && value.Reference?.Contains("#") == false)
			{
				foreach (var item in entries)
				{
					string id = value.Reference;
					string itemID = item.GetEntryID();

					if (!id.Contains("history") || !itemID.Contains("history") || id.Contains("Composition"))
					{
						id = string.Format("{0}/{1}", id.Split('/'));
						itemID = string.Format("{0}/{1}", itemID.Split('/'));
					}

					if (itemID == id)
					{
						result = (item as ResourceEntry)?.Resource;
						result.Id = item.GetEntryID();
						break;
					}
				}

				if (result == null)
				{
					try
					{
						ResourceEntry entry = Read(value.Reference);

						if (entry != null)
						{
							entries.Add(entry);
							result = entry.Resource;
							result.Id = entry.GetEntryID();
						}
					}
					catch
					{
						try
						{
							string[] parts = value.Reference.Trim('/').Split('/');

							Bundle found = SearchById(parts[0], parts[1]);
							if (found?.TotalResults > 0)
							{
								foreach (var item in found.Entries)
								{
									ResourceEntry entry = item as ResourceEntry;
									if (entry != null)
									{
										entries.Add(entry);
										result = entry.Resource;
										result.Id = entry.GetEntryID();
									}
								}
							}
						}
						catch
						{
						}
					}
				}
			}

			return result;
		}
	}
}