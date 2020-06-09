/* 
 * Copyright (c) 2014, Furore (info@furore.com) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using Hl7.Fhir.Applicator;
using Hl7.Fhir.Model;
using Hl7.Fhir.Model.ESPBI;
using Hl7.Fhir.Serialization;
using Hl7.Fhir.Support;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace Hl7.Fhir.Rest
{
	public enum PageDirection
	{
		First,
		Previous,
		Next,
		Last
	}

	/// <summary>
	/// Event arguments for HTTP request.
	/// </summary>
	public class BeforeRequestEventArgs : EventArgs
	{
		public BeforeRequestEventArgs(FhirRequest request, HttpWebRequest rawRequest)
		{
			Request = request;
			RawRequest = rawRequest;
		}

		public FhirRequest Request { get; internal set; }
		public HttpWebRequest RawRequest { get; internal set; }
		public string Body => Request.GetBodyAsString();
	}

	/// <summary>
	/// Event arguments for HTTP response.
	/// </summary>
	public class AfterResponseEventArgs : EventArgs
	{
		public AfterResponseEventArgs(FhirResponse fhirResponse, FhirRequest request, WebResponse webResponse, WebRequest webRequest)
		{
			Response = fhirResponse;
			Request = request;
			RawResponse = webResponse;
			RawRequest = webRequest;
		}

		public FhirResponse Response { get; }
		public FhirRequest Request { get; }
		public WebResponse RawResponse { get; }
		public WebRequest RawRequest { get; }
		public string Body => Response.GetBodyAsString();
	}

	public class FhirClient : IDisposable
	{
		public delegate void BeforeRequestEventHandler(object sender, BeforeRequestEventArgs e);
		public delegate void AfterResponseEventHandler(object sender, AfterResponseEventArgs e);

		/// <summary>
		/// Fires just before HTTP request. You can still modify headers of RawRequest, but body is already written.
		/// Headers in Request.GetHeadersAsString() are not complete until OnAfterResponse (thanks, .NET).
		/// </summary>
		public event BeforeRequestEventHandler OnBeforeRequest;

		/// <summary>
		/// Fires just after the HTTP response. Response contains full information about headers and body, but none of them should are modifyable.
		/// Headers in Request.GetHeadersAsString() are now fully populated.
		/// Usefull for logging.
		/// </summary>
		public event AfterResponseEventHandler OnAfterResponse;

		/// <summary>
		/// Creates a new client using a default endpoint
		/// If the endpoint does not end with a slash (/), it will be added.
		/// </summary>
		public FhirClient(Uri endpoint)
		{
			if (endpoint == null)
				throw Error.ArgumentNull(nameof(endpoint));

			if (!endpoint.OriginalString.EndsWith("/"))
				endpoint = new Uri(endpoint.OriginalString + "/");

			if (!endpoint.IsAbsoluteUri)
				throw Error.Argument(nameof(endpoint), "Endpoint must be absolute");

			Endpoint = endpoint;
			PreferredFormat = ResourceFormat.Xml;
		}

		public FhirClient(string endpoint) : this(new Uri(endpoint)) { }

		public ResourceFormat PreferredFormat { get; set; }

		public int? Timeout { get; set; }

		/// <summary>
		/// The default endpoint for use with operations that use discrete id/version parameters
		/// instead of explicit uri endpoints.
		/// </summary>
		public Uri Endpoint { get; }

		public bool IsDisposed { get; private set; }

		private Uri makeAbsolute(Uri location = null)
		{
			// If called without a location, just return the base endpoint
			if (location == null) return Endpoint;

			// If the location is absolute, verify whether it is within the endpoint
			if (location.IsAbsoluteUri)
			{
				if (!new RestUrl(Endpoint).IsEndpointFor(location))
					throw Error.Argument(nameof(location), "Url is not located on this FhirClient's endpoint");
			}
			else
			{
				// Else, make location absolute within the endpoint
				//location = new Uri(Endpoint, location);
				location = new RestUrl(Endpoint).AddPath(location).Uri;
			}

			return location;
		}

		private FhirRequest createFhirRequest(Uri location, string method)
		{
			var req = new FhirRequest(location, method, BeforeRequest, AfterResponse);

			if (Timeout != null)
				req.Timeout = Timeout.Value;

			return req;
		}

		private ResourceEntry<TResource> internalCreate<TResource>(TResource resource, IEnumerable<Tag> tags, string id, bool refresh) where TResource : Resource, new()
		{
			var collection = ModelInfo.GetCollectionName<TResource>();
			FhirRequest req;

			if (id == null)
			{
				// A normal create
				var rl = new RestUrl(Endpoint).ForCollection(collection);
				req = createFhirRequest(rl.Uri, "POST");
			}
			else
			{
				// A create at a specific id => PUT to that address
				var ri = ResourceIdentity.Build(Endpoint, collection, id);
				req = createFhirRequest(ri, "PUT");
			}

			req.SetBody(resource, PreferredFormat);
			if (tags != null) req.SetTagsInHeader(tags);
			FhirResponse response = doRequest(req, new[] { HttpStatusCode.Created, HttpStatusCode.OK }, r => r);

			ResourceEntry<TResource> entry = (ResourceEntry<TResource>)ResourceEntry.Create(resource);
			if (!string.IsNullOrEmpty(response.Location)) //ESPBI Query gražinami resursai būna be Location
			{
				entry.Links.SelfLink = new ResourceIdentity(response.Location);
				entry.Id = new ResourceIdentity(response.Location).WithoutVersion();
			}

			if (!string.IsNullOrEmpty(response.LastModified))
				entry.LastUpdated = DateTimeOffset.Parse(response.LastModified);

			if (!string.IsNullOrEmpty(response.Category))
				entry.Tags = HttpUtil.ParseCategoryHeader(response.Category);

			// If asked for it, immediately get the contents *we just posted*, so use the actually created version
			if (refresh)
				entry = Refresh(entry, versionSpecific: true);

			return entry;
		}

		private static string getCollectionFromLocation(Uri location)
		{
			var collection = new ResourceIdentity(location).Collection;
			if (collection == null) throw Error.Argument(nameof(location), "Must be a FHIR REST url containing the resource type in its path");

			return collection;
		}

		private static string getIdFromLocation(Uri location)
		{
			var id = new ResourceIdentity(location).Id;
			if (id == null) throw Error.Argument(nameof(location), "Must be a FHIR REST url containing the logical id in its path");

			return id;
		}

		private Bundle internalHistory(string collection = null, string id = null, DateTimeOffset? since = null, int? pageSize = null)
		{
			RestUrl location;

			if (collection == null)
				location = new RestUrl(Endpoint).ServerHistory();
			else
			{
				location = (id == null) ?
					new RestUrl(Endpoint).CollectionHistory(collection) :
					new RestUrl(Endpoint).ResourceHistory(collection, id);
			}

			if (since != null) location = location.AddParam(HttpUtil.HISTORY_PARAM_SINCE, PrimitiveTypeConverter.ConvertTo<string>(since.Value));
			if (pageSize != null) location = location.AddParam(HttpUtil.HISTORY_PARAM_COUNT, pageSize.ToString());

			return fetchBundle(location.Uri);
		}

		/// <summary>
		/// Fetches a bundle from a FHIR resource endpoint. 
		/// </summary>
		/// <param name="location">The url of the endpoint which returns a Bundle</param>
		/// <returns>The Bundle as received by performing a GET on the endpoint. This operation will throw an exception
		/// if the operation does not result in a HttpStatus OK.</returns>
		private Bundle fetchBundle(Uri location)
		{
			FhirRequest req = createFhirRequest(makeAbsolute(location), "GET");
			req.IsForBundle = true;

			return doRequest(req, HttpStatusCode.OK, resp => resp.GetBodyAsBundle());
		}

		private OperationOutcome doValidate(Uri url, Resource data, IEnumerable<Tag> tags)
		{
			var req = createFhirRequest(url, "POST");

			req.SetBody(data, PreferredFormat);
			if (tags != null) req.SetTagsInHeader(tags);

			try
			{
				doRequest(req, HttpStatusCode.OK, resp => true);
				return null;
			}
			catch (FhirOperationException foe)
			{
				if (foe.Outcome != null)
					return foe.Outcome;
				else
					throw; // no need to include foe, framework does this and preserves the stack location (CA2200)
			}
		}

		private IEnumerable<Tag> internalGetTags(string collection, string id, string version)
		{
			RestUrl location = new RestUrl(Endpoint);

			if (collection == null)
				location = location.ServerTags();
			else
			{
				if (id == null)
					location = location.CollectionTags(collection);
				else
					location = location.ResourceTags(collection, id, version);
			}

			var req = createFhirRequest(location.Uri, "GET");
			var result = doRequest(req, HttpStatusCode.OK, resp => resp.GetBodyAsTagList());
			return result.Category;
		}

		private T doRequest<T>(FhirRequest request, HttpStatusCode success, Func<FhirResponse, T> onSuccess, ResourceFormat? format = null)
		{
			return doRequest<T>(request, new[] { success }, onSuccess, format);
		}

		private T doRequest<T>(FhirRequest request, HttpStatusCode[] success, Func<FhirResponse, T> onSuccess, ResourceFormat? format = null)
		{
			FhirResponse response = request.GetResponse(format ?? PreferredFormat);

			if (success.Contains(response.Result))
				return onSuccess(response);

			throw new FhirOperationException("Operation failed with status code " + response.Result, request, response);
		}

		/// <summary>
		/// Get a conformance statement for the system
		/// </summary>
		/// <param name="useOptionsVerb">If true, uses the Http OPTIONS verb to get the conformance, otherwise uses the /metadata endpoint</param>
		/// <returns>A Conformance resource. Throws an exception if the operation failed.</returns>
		public ResourceEntry<Conformance> Conformance(bool useOptionsVerb = false)
		{
			RestUrl url = useOptionsVerb ? new RestUrl(Endpoint) : new RestUrl(Endpoint).WithMetadata();

			var request = createFhirRequest(url.Uri, useOptionsVerb ? "OPTIONS" : "GET");
			return doRequest(request, HttpStatusCode.OK, resp => resp.GetBodyAsEntry<Conformance>(request));
		}

		/// <summary>
		/// Create a resource on a FHIR endpoint
		/// </summary>
		/// <param name="resource">The resource instance to create</param>
		/// <param name="tags">Optional. List of Tags to add to the created instance.</param>
		/// <param name="refresh">Optional. When true, fetches the newly created resource from the server.</param>
		/// <returns>A ResourceEntry containing the metadata (id, selflink) associated with the resource as created on the server, or an exception if the create failed.</returns>
		/// <typeparam name="TResource">The type of resource to create</typeparam>
		/// <remarks>The Create operation normally does not return the posted resource, but just its metadata. Specifying
		/// refresh=true ensures the return value contains the Resource as stored by the server.
		/// </remarks>
		public ResourceEntry<TResource> Create<TResource>(TResource resource, IEnumerable<Tag> tags = null, bool refresh = false) where TResource : Resource, new()
		{
			if (resource == null) throw Error.ArgumentNull(nameof(resource));

			return internalCreate<TResource>(resource, tags, null, refresh);
		}

		/// <summary>
		/// Create a resource with a given id on the FHIR endpoint
		/// </summary>
		/// <param name="resource">The resource instance to create</param>
		/// <param name="id">Optional. A client-assigned logical id for the newly created resource.</param>
		/// <param name="tags">Optional. List of Tags to add to the created instance.</param>
		/// <param name="refresh">Optional. When true, fetches the newly created resource from the server.</param>
		/// <returns>A ResourceEntry containing the metadata (id, selflink) associated with the resource as created on the server, or an exception if the create failed.</returns>
		/// <typeparam name="TResource">The type of resource to create</typeparam>
		/// <remarks>The Create operation normally does not return the posted resource, but just its metadata. Specifying
		/// refresh=true ensures the return value contains the Resource as stored by the server.
		/// </remarks>
		public ResourceEntry<TResource> Create<TResource>(TResource resource, string id, IEnumerable<Tag> tags = null, bool refresh = false) where TResource : Resource, new()
		{
			if (resource == null) throw Error.ArgumentNull(nameof(resource));
			if (id == null) throw Error.ArgumentNull(nameof(id), "Must supply a client-assigned id");

			return internalCreate<TResource>(resource, tags, id, refresh);
		}


		/// <summary>
		/// Refreshes the data and metadata for a given ResourceEntry.
		/// </summary>
		/// <param name="entry">The entry to refresh. It's id property will be used to fetch the latest version of the Resource.</param>
		/// <param name="versionSpecific"></param>
		/// <typeparam name="TResource">The type of resource to refresh</typeparam>
		/// <returns>A resource entry containing up-to-date data and metadata.</returns>
		internal ResourceEntry<TResource> Refresh<TResource>(ResourceEntry<TResource> entry, bool versionSpecific = false) where TResource : Resource, new()
		{
			if (entry == null) throw Error.ArgumentNull(nameof(entry));

			if (!versionSpecific)
				return Read<TResource>(entry.Id);
			else
				return Read<TResource>(entry.SelfLink);
		}

		/// <summary>
		/// Fetches a typed resource from a FHIR resource endpoint.
		/// </summary>
		/// <param name="location">The url of the Resource to fetch. This can be a Resource id url or a version-specific
		/// Resource url.</param>
		/// <typeparam name="TResource">The type of resource to read</typeparam>
		/// <returns>The requested resource as a ResourceEntry&lt;T&gt;. This operation will throw an exception
		/// if the resource has been deleted or does not exist. The specified may be relative or absolute, if it is an abolute
		/// url, it must reference an address within the endpoint.</returns>
		public ResourceEntry<TResource> Read<TResource>(Uri location) where TResource : Resource, new()
		{
			if (location == null) throw Error.ArgumentNull(nameof(location));

			var request = createFhirRequest(makeAbsolute(location), "GET");

			return doRequest(request, HttpStatusCode.OK, resp => resp.GetBodyAsEntry<TResource>(request));
		}

		/// <summary>
		/// Fetches a typed resource from a FHIR resource endpoint.
		/// </summary>
		/// <param name="location">The url of the Resource to fetch as a string. This can be a Resource id url or a version-specific
		/// Resource url.</param>
		/// <typeparam name="TResource">The type of resource to read</typeparam>
		/// <returns>The requested resource as a ResourceEntry&lt;T&gt;. This operation will throw an exception
		/// if the resource has been deleted or does not exist. The specified may be relative or absolute, if it is an abolute
		/// url, it must reference an address within the endpoint.</returns>
		public ResourceEntry<TResource> Read<TResource>(string location) where TResource : Resource, new()
		{
			if (location == null) throw Error.ArgumentNull(nameof(location));
			return Read<TResource>(new Uri(location, UriKind.RelativeOrAbsolute));
		}

		/// <summary>
		/// Reads a resource from a FHIR resource endpoint.
		/// </summary>
		/// <param name="location">The url of the Resource to fetch. This can be a Resource id url or a version-specific
		/// Resource url.</param>
		/// <returns>The requested resource as an untyped ResourceEntry. The ResourceEntry.Resource, which is of type
		/// object, must be cast to the correct Resource type to access its properties.
		/// The specified may be relative or absolute, if it is an abolute
		/// url, it must reference an address within the endpoint.</returns>
		public ResourceEntry Read(Uri location)
		{
			if (location == null) throw Error.ArgumentNull(nameof(location));

			var collection = getCollectionFromLocation(location);

			var req = createFhirRequest(makeAbsolute(location), "GET");
			return doRequest(req, HttpStatusCode.OK, resp => resp.GetBodyAsEntry(collection));
		}

		/// <summary>
		/// Reads a resource from a FHIR resource endpoint.
		/// </summary>
		/// <param name="location">The url of the Resource to fetch as a string. This can be a Resource id url or a version-specific
		/// Resource url.</param>
		/// <returns>The requested resource as an untyped ResourceEntry. The ResourceEntry.Resource, which is of type
		/// object, must be cast to the correct Resource type to access its properties.
		/// The specified may be relative or absolute, if it is an abolute
		/// url, it must reference an address within the endpoint.</returns>
		public ResourceEntry Read(string location)
		{
			if (location == null) throw Error.ArgumentNull(nameof(location));
			return Read(new Uri(location, UriKind.RelativeOrAbsolute));
		}

		/// <summary>
		/// Update (or create) a resource at a given endpoint
		/// </summary>
		/// <param name="entry">A ResourceEntry containing the resource to update</param>
		/// <param name="refresh">Optional. When true, fetches the newly updated resource from the server.</param>
		/// <typeparam name="TResource">The type of resource that is being updated</typeparam>
		/// <returns>If refresh=true, 
		/// this function will return a ResourceEntry with all newly created data from the server. Otherwise
		/// the returned result will only contain a SelfLink if the update was actually a create.
		/// Throws an exception when the update failed,
		/// in particular when an update conflict is detected and the server returns a HTTP 409. When the ResourceEntry
		/// passed as the argument does not have a SelfLink, the server may return a HTTP 412 to indicate it
		/// requires version-aware updates.</returns>
		public ResourceEntry<TResource> Update<TResource>(ResourceEntry<TResource> entry, bool refresh = false)
			where TResource : Resource, new()
		{
			if (entry == null) throw Error.ArgumentNull(nameof(entry));
			if (entry.Resource == null) throw Error.Argument(nameof(entry), "Entry does not contain a Resource to update");
			if (entry.Id == null) throw Error.Argument(nameof(entry), "Entry needs a non-null entry.id to send the update to");

			var req = createFhirRequest(entry.Id, "PUT");
			req.SetBody(entry.Resource, PreferredFormat);

			if (entry.Tags != null)
				req.SetTagsInHeader(entry.Tags);

			// Always supply the version we are updating if we have one. Servers may require this.
			if (entry.SelfLink != null)
				req.ContentLocation = entry.SelfLink;

			// This might be an update of a resource that doesn't yet exist, so accept a status Created too
			FhirResponse response = doRequest(req, new[] { HttpStatusCode.Created, HttpStatusCode.OK }, r => r);
			var updated = new ResourceEntry<TResource>();
			var location = response.Location ?? response.ContentLocation ?? response.ResponseUri.OriginalString;

			if (!string.IsNullOrEmpty(location))
			{
				ResourceIdentity reqId = new ResourceIdentity(location);

				// Set the id to the location, without the version specific part
				updated.Id = reqId.WithoutVersion();

				// If the content location has version information, set to SelfLink to it
				if (reqId.VersionId != null) updated.SelfLink = reqId;
			}

			if (!string.IsNullOrEmpty(response.LastModified))
				updated.LastUpdated = DateTimeOffset.Parse(response.LastModified);

			if (!string.IsNullOrEmpty(response.Category))
				updated.Tags = HttpUtil.ParseCategoryHeader(response.Category);

			updated.Title = entry.Title;

			// If asked for it, immediately get the contents *we just posted* if we have the version-specific url, or
			// otherwise the most recent data on the server.
			if (refresh)
				updated = Refresh(updated, updated.SelfLink != null);

			return updated;
		}

		/// <summary>
		/// Update (or create) a resource at a given endpoint
		/// </summary>
		/// <param name="location">The location where the resource must be posted</param>
		/// <param name="data">The resource to send as an update</param>
		/// <param name="refresh">Optional. When true, fetches the newly updated resource from the server.</param>
		/// <typeparam name="TResource">The type of resource that is being updated</typeparam>
		/// <returns>If refresh=true, this function will return a ResourceEntry with all newly created data from the server. 
		/// Otherwise
		/// the returned result will only contain a SelfLink if the update was actually a create.
		/// Throws an exception when the update failed,
		/// in particular when an update conflict is detected and the server returns a HTTP 409. When the ResourceEntry
		/// passed as the argument does not have a SelfLink, the server may return a HTTP 412 to indicate it
		/// requires version-aware updates.</returns>
		public ResourceEntry<TResource> Update<TResource>(Uri location, TResource data, bool refresh = false)
			where TResource : Resource, new()
		{
			if (location == null) throw Error.ArgumentNull(nameof(location));
			if (data == null) throw Error.ArgumentNull(nameof(data));

			ResourceEntry<TResource> entry = new ResourceEntry<TResource>(makeAbsolute(location), DateTimeOffset.Now, data);
			return Update(entry, refresh);
		}

		/// <summary>
		/// Update (or create) a resource at a given endpoint
		/// </summary>
		/// <param name="location">The location where the resource must be posted</param>
		/// <param name="data">The resource to send as an update</param>
		/// <param name="refresh">Optional. When true, fetches the newly updated resource from the server.</param>
		/// <typeparam name="TResource">The type of resource that is being updated</typeparam>
		/// <returns>If refresh=true, this function will return a ResourceEntry with all newly created data from the server. 
		/// Otherwise
		/// the returned result will only contain a SelfLink if the update was actually a create.
		/// Throws an exception when the update failed,
		/// in particular when an update conflict is detected and the server returns a HTTP 409. When the ResourceEntry
		/// passed as the argument does not have a SelfLink, the server may return a HTTP 412 to indicate it
		/// requires version-aware updates.</returns>
		public ResourceEntry<TResource> Update<TResource>(string location, TResource data, bool refresh = false)
			where TResource : Resource, new()
		{
			if (location == null) throw Error.ArgumentNull(nameof(location));
			if (data == null) throw Error.ArgumentNull(nameof(data));

			return Update<TResource>(new Uri(location, UriKind.RelativeOrAbsolute), data, refresh);
		}

		/// <summary>
		/// Delete a resource at the given endpoint.
		/// </summary>
		/// <param name="location">endpoint of the resource to delete</param>
		/// <returns>Throws an exception when the delete failed, though this might
		/// just mean the server returned 404 (the resource didn't exist before) or 410 (the resource was
		/// already deleted).</returns>
		public void Delete(Uri location)
		{
			if (location == null) throw Error.ArgumentNull(nameof(location));

			var req = createFhirRequest(makeAbsolute(location), "DELETE");
			doRequest(req, HttpStatusCode.NoContent, resp => true);
		}

		public void Delete(string location)
		{
			Uri uri = new Uri(location, UriKind.Relative);
			Delete(uri);
		}

		/// <summary>
		/// Delete a resource represented by the entry
		/// </summary>
		/// <param name="entry">Entry containing the id of the resource to delete</param>
		/// <returns>Throws an exception when the delete failed, though this might
		/// just mean the server returned 404 (the resource didn't exist before) or 410 (the resource was
		/// already deleted).</returns>
		public void Delete(ResourceEntry entry)
		{
			if (entry == null) throw Error.ArgumentNull(nameof(entry));
			if (entry.Id == null) throw Error.Argument(nameof(entry), "Entry must have an id");

			Delete(entry.Id);
		}

		/// <summary>
		/// Retrieve the version history for a specific resource type
		/// </summary>
		/// <param name="since">Optional. Returns only changes after the given date</param>
		/// <param name="pageSize">Optional. Asks server to limit the number of entries per page returned</param>
		/// <typeparam name="TResource">The type of Resource to get the history for</typeparam>
		/// <returns>A bundle with the history for the indicated instance, may contain both 
		/// ResourceEntries and DeletedEntries.</returns>
		public Bundle TypeHistory<TResource>(DateTimeOffset? since = null, int? pageSize = null) where TResource : Resource, new()
		{
			var collection = ModelInfo.GetCollectionName<TResource>();

			return internalHistory(collection, null, since, pageSize);
		}

		/// <summary>
		/// Retrieve the version history for a resource at a given location
		/// </summary>
		/// <param name="location">The address of the resource to get the history for</param>
		/// <param name="since">Optional. Returns only changes after the given date</param>
		/// <param name="pageSize">Optional. Asks server to limit the number of entries per page returned</param>
		/// <returns>A bundle with the history for the indicated instance, may contain both 
		/// ResourceEntries and DeletedEntries.</returns>
		public Bundle History(Uri location, DateTimeOffset? since = null, int? pageSize = null)
		{
			if (location == null) throw Error.ArgumentNull(nameof(location));

			var collection = getCollectionFromLocation(location);
			var id = getIdFromLocation(location);

			return internalHistory(collection, id, since, pageSize);
		}

		public Bundle History(string location, DateTimeOffset? since = null, int? pageSize = null)
		{
			if (location == null) throw Error.ArgumentNull(nameof(location));
			Uri uri = new Uri(location, UriKind.Relative);

			return History(uri, since, pageSize);
		}

		/// <summary>
		/// Retrieve the version history for a resource in a ResourceEntry
		/// </summary>
		/// <param name="entry">The ResourceEntry representing the Resource to get the history for</param>
		/// <param name="since">Optional. Returns only changes after the given date</param>
		/// <param name="pageSize">Optional. Asks server to limit the number of entries per page returned</param>
		/// <returns>A bundle with the history for the indicated instance, may contain both 
		/// ResourceEntries and DeletedEntries.</returns>
		public Bundle History(BundleEntry entry, DateTimeOffset? since = null, int? pageSize = null)
		{
			if (entry == null) throw Error.ArgumentNull(nameof(entry));

			return History(entry.Id, since, pageSize);
		}

		/// <summary>
		/// Retrieve the full version history of the server
		/// </summary>
		/// <param name="since">Optional. Returns only changes after the given date</param>
		/// <param name="pageSize">Optional. Asks server to limit the number of entries per page returned</param>
		/// <returns>A bundle with the history for the indicated instance, may contain both 
		/// ResourceEntries and DeletedEntries.</returns>
		public Bundle WholeSystemHistory(DateTimeOffset? since = null, int? pageSize = null)
		{
			return internalHistory(null, null, since, pageSize);
		}

		/// <summary>
		/// Validates whether the contents of the resource would be acceptable as an update
		/// </summary>
		/// <param name="entry">The entry containing the updated Resource to validate</param>
		/// <param name="result">Contains the OperationOutcome detailing why validation failed, or null if validation succeeded</param>
		/// <returns>True when validation was successful, false otherwise. Note that this function may still throw exceptions if non-validation related
		/// failures occur.</returns>
		public bool TryValidateUpdate<TResource>(ResourceEntry<TResource> entry, out OperationOutcome result) where TResource : Resource, new()
		{
			if (entry == null) throw Error.ArgumentNull(nameof(entry));
			if (entry.Resource == null) throw Error.Argument(nameof(entry), "Entry does not contain a Resource to validate");
			if (entry.Id == null) throw Error.Argument(nameof(entry), "Entry needs a non-null entry.id to use for validation");

			var id = new ResourceIdentity(entry.Id);
			var url = new RestUrl(Endpoint).Validate(id.Collection, id.Id);
			result = doValidate(url.Uri, entry.Resource, entry.Tags);

			return result == null || !result.Success();
		}

		/// <summary>
		/// Validates whether the contents of the resource would be acceptable as a create
		/// </summary>
		/// <typeparam name="TResource"></typeparam>
		/// <param name="resource">The entry containing the Resource data to use for the validation</param>
		/// <param name="result">Contains the OperationOutcome detailing why validation failed, or null if validation succeeded</param>
		/// <param name="tags">Optional list of tags to attach to the resource</param>
		/// <returns>True when validation was successful, false otherwise. Note that this function may still throw exceptions if non-validation related
		/// failures occur.</returns>
		public bool TryValidateCreate<TResource>(TResource resource, out OperationOutcome result, IEnumerable<Tag> tags = null) where TResource : Resource, new()
		{
			if (resource == null) throw Error.ArgumentNull(nameof(resource));

			var collection = ModelInfo.GetCollectionName<TResource>();
			var url = new RestUrl(Endpoint).Validate(collection);

			result = doValidate(url.Uri, resource, tags);
			return result == null || !result.Success();
		}

		/// <summary>
		/// Search for Resources based on criteria specified in a Query resource
		/// </summary>
		/// <param name="q">The Query resource containing the search parameters</param>
		/// <returns>A Bundle with all resources found by the search, or an empty Bundle if none were found.</returns>
		public Bundle Search(Query q)
		{
			return fetchBundle(new RestUrl(Endpoint).Search(q).Uri);
		}

		/// <summary>
		/// Search for Resources of a certain type that match the given criteria
		/// </summary>
		/// <param name="criteria">Optional. The search parameters to filter the resources on. Each
		/// given string is a combined key/value pair (separated by '=')</param>
		/// <param name="includes">Optional. A list of include paths</param>
		/// <param name="pageSize">Optional. Asks server to limit the number of entries per page returned</param>
		/// <typeparam name="TResource">The type of resource to list</typeparam>
		/// <returns>A Bundle with all resources found by the search, or an empty Bundle if none were found.</returns>
		/// <remarks>All parameters are optional, leaving all parameters empty will return an unfiltered list 
		/// of all resources of the given Resource type</remarks>
		public Bundle Search<TResource>(IList<string> criteria, IList<string> includes = null, int? pageSize = null) where TResource : Resource, new()
		{
			return Search(ModelInfo.GetCollectionName<TResource>(), criteria, includes, pageSize);
		}

		/// <summary>
		/// Search for Resources of a certain type that match the given criteria
		/// </summary>
		/// <param name="resource">The type of resource to search for</param>
		/// <param name="criteria">Optional. The search parameters to filter the resources on. Each
		/// given string is a combined key/value pair (separated by '=')</param>
		/// <param name="includes">Optional. A list of include paths</param>
		/// <param name="pageSize">Optional. Asks server to limit the number of entries per page returned</param>
		/// <returns>A Bundle with all resources found by the search, or an empty Bundle if none were found.</returns>
		/// <remarks>All parameters are optional, leaving all parameters empty will return an unfiltered list 
		/// of all resources of the given Resource type</remarks>
		public Bundle Search(string resource, IList<string> criteria = null, IList<string> includes = null, int? pageSize = null)
		{
			if (resource == null) throw Error.ArgumentNull(nameof(resource));

			return Search(new Query(resource, criteria, includes, pageSize));
		}

		/// <summary>
		/// Search for Resources across the whol server that match the given criteria
		/// </summary>
		/// <param name="criteria">Optional. The search parameters to filter the resources on. Each
		/// given string is a combined key/value pair (separated by '=')</param>
		/// <param name="includes">Optional. A list of include paths</param>
		/// <param name="pageSize">Optional. Asks server to limit the number of entries per page returned</param>
		/// <returns>A Bundle with all resources found by the search, or an empty Bundle if none were found.</returns>
		/// <remarks>All parameters are optional, leaving all parameters empty will return an unfiltered list 
		/// of all resources of the given Resource type</remarks>
		public Bundle WholeSystemSearch(IList<string> criteria = null, IList<string> includes = null, int? pageSize = null)
		{
			return Search(new Query(null, criteria, includes, pageSize));
		}

		/// <summary>
		/// Search for resources based on a resource's id.
		/// </summary>
		/// <param name="id">The id of the resource to search for</param>
		/// <param name="includes">Zero or more include paths</param>
		/// <param name="pageSize">Optional. Asks server to limit the number of entries per page returned</param>
		/// <typeparam name="TResource">The type of resource to search for</typeparam>
		/// <returns>A Bundle with the BundleEntry as identified by the id parameter or an empty
		/// Bundle if the resource wasn't found.</returns>
		/// <remarks>This operation is similar to Read, but additionally,
		/// it is possible to specify include parameters to include resources in the bundle that the
		/// returned resource refers to.</remarks>
		public Bundle SearchById<TResource>(string id, IList<string> includes = null, int? pageSize = null) where TResource : Resource, new()
		{
			if (id == null) throw Error.ArgumentNull(nameof(id));

			return SearchById(ModelInfo.GetCollectionName<TResource>(), id, includes, pageSize);
		}

		/// <summary>
		/// Search for resources based on a resource's id.
		/// </summary>
		/// <param name="resource">The type of resource to search for</param>
		/// <param name="id">The id of the resource to search for</param>
		/// <param name="includes">Zero or more include paths</param>
		/// <param name="pageSize">Optional. Asks server to limit the number of entries per page returned</param>
		/// <returns>A Bundle with the BundleEntry as identified by the id parameter or an empty
		/// Bundle if the resource wasn't found.</returns>
		/// <remarks>This operation is similar to Read, but additionally,
		/// it is possible to specify include parameters to include resources in the bundle that the
		/// returned resource refers to.</remarks>
		public Bundle SearchById(string resource, string id, IList<string> includes = null, int? pageSize = null)
		{
			if (resource == null) throw Error.ArgumentNull(nameof(resource));
			if (id == null) throw Error.ArgumentNull(nameof(id));

			return Search(new Query(resource, null, includes, pageSize).Where(Model.Query.SEARCH_PARAM_ID, id));
		}

		/// <summary>
		/// Queries ESPBI data via their Query API.
		/// </summary>
		/// <param name="query">Query information</param>
		/// <returns></returns>
		public Bundle Query(Query query)
		{
			FhirRequest req = createFhirRequest(makeAbsolute(new RestUrl(Endpoint).ForCollection(ModelInfo.GetCollectionName<Query>()).Uri), "POST");
			req.SetBody(query, PreferredFormat);
			req.IsForBundle = true;

			return doRequest(req, HttpStatusCode.OK, resp => resp.GetBodyAsBundle());
		}

		/// <summary>
		/// Uses the FHIR paging mechanism to go navigate around a series of paged result Bundles
		/// </summary>
		/// <param name="current">The bundle as received from the last response</param>
		/// <param name="direction">Optional. Direction to browse to, default is the next page of results.</param>
		/// <returns>A bundle containing a new page of results based on the browse direction, or null if
		/// the server did not have more results in that direction.</returns>
		public Bundle Continue(Bundle current, PageDirection direction = PageDirection.Next)
		{
			if (current == null) throw Error.ArgumentNull(nameof(current));
			if (current.Links == null) return null;

			Uri continueAt = null;

			switch (direction)
			{
				case PageDirection.First:
					continueAt = current.Links.FirstLink; break;
				case PageDirection.Previous:
					continueAt = current.Links.PreviousLink; break;
				case PageDirection.Next:
					continueAt = current.Links.NextLink; break;
				case PageDirection.Last:
					continueAt = current.Links.LastLink; break;
			}

			if (continueAt != null)
				return fetchBundle(continueAt);
			else
				return null;
		}

		/// <summary>
		/// Send a set of creates, updates and deletes to the server to be processed in one transaction
		/// </summary>
		/// <param name="bundle">The bundled creates, updates and delted</param>
		/// <returns>A bundle as returned by the server after it has processed the transaction, or null
		/// if an error occurred.</returns>
		public Bundle Transaction(Bundle bundle)
		{
			if (bundle == null) throw Error.ArgumentNull(nameof(bundle));

			var req = createFhirRequest(Endpoint, "POST");
			req.SetBody(bundle, PreferredFormat);
			return doRequest(req, HttpStatusCode.OK, resp => resp.GetBodyAsBundle());
		}

		/// <summary>
		/// Send a document bundle
		/// </summary>
		/// <param name="bundle">A bundle containing a Document</param>
		/// <remarks>The bundle must declare it is a Document, use Bundle.SetBundleType() to do so.</remarks>
		public void Document(Bundle bundle)
		{
			if (bundle == null) throw Error.ArgumentNull(nameof(bundle));
			if (bundle.GetBundleType() != BundleType.Document)
				throw Error.Argument(nameof(bundle), "The bundle passed to the Document endpoint needs to be a document (use SetBundleType to do so)");

			var url = new RestUrl(Endpoint).ToDocument();

			// Documents are merely "accepted"
			var req = createFhirRequest(url.Uri, "POST");
			req.SetBody(bundle, PreferredFormat);
			doRequest(req, HttpStatusCode.NoContent, resp => true);
		}

		/// <summary>
		/// Send a Document or Message bundle to a server's Mailbox
		/// </summary>
		/// <param name="bundle">The Document or Message be sent</param>
		/// <returns>A return message as a Bundle</returns>
		/// <remarks>The bundle must declare it is a Document or Message, use Bundle.SetBundleType() to do so.</remarks>       
		public Bundle DeliverToMailbox(Bundle bundle)
		{
			if (bundle == null) throw Error.ArgumentNull(nameof(bundle));
			if (bundle.GetBundleType() != BundleType.Document && bundle.GetBundleType() != BundleType.Message)
				throw Error.Argument(nameof(bundle), "The bundle passed to the Mailbox endpoint needs to be a document or message (use SetBundleType to do so)");

			var url = new RestUrl(Endpoint).ToMailbox();

			var req = createFhirRequest(url.Uri, "POST");
			req.SetBody(bundle, PreferredFormat);

			return doRequest(req, HttpStatusCode.OK, resp => resp.GetBodyAsBundle());
		}

		/// <summary>
		/// Get all tags known by the FHIR server
		/// </summary>
		/// <returns>A list of Tags</returns>
		public IEnumerable<Tag> WholeSystemTags()
		{
			return internalGetTags(null, null, null);
		}

		/// <summary>
		/// Get all tags known by the FHIR server for a given resource type
		/// </summary>
		/// <returns>A list of all Tags present on the server</returns>
		public IEnumerable<Tag> TypeTags<TResource>() where TResource : Resource, new()
		{
			return internalGetTags(ModelInfo.GetCollectionName<TResource>(), null, null);
		}

		/// <summary>
		/// Get all tags known by the FHIR server for a given resource type
		/// </summary>
		/// <returns>A list of Tags occuring for the given resource type</returns>
		public IEnumerable<Tag> TypeTags(string type)
		{
			if (type == null) throw Error.ArgumentNull(nameof(type));

			return internalGetTags(type, null, null);
		}

		/// <summary>
		/// Get the tags for a resource (or resource version) at a given location
		/// </summary>
		/// <param name="location">The url of the Resource to get the tags for. This can be a Resource id url or a version-specific
		/// Resource url, and may be relative.</param>
		/// <returns>A list of Tags for the resource instance</returns>
		public IEnumerable<Tag> Tags(Uri location)
		{
			if (location == null) throw Error.ArgumentNull(nameof(location));

			var collection = getCollectionFromLocation(location);
			var id = getIdFromLocation(location);
			var version = new ResourceIdentity(location).VersionId;

			return internalGetTags(collection, id, version);
		}

		/// <summary>
		/// Get the tags for a resource (or resource version) at a given location
		/// </summary>
		/// <param name="location">The location the Resource to get the tags for. 
		/// This can be a Resource id url or a version-specific Resource url, and may be relative</param>
		/// <returns>A list of Tags for the resource instance</returns>
		public IEnumerable<Tag> Tags(string location)
		{
			var identity = new ResourceIdentity(location);
			return internalGetTags(identity.Collection, identity.Id, identity.VersionId);
		}

		/// <summary>
		/// Get the tags for a resource (or resource version) at a given location
		/// </summary>
		/// <param name="id">The logical id for the resource</param>
		/// <param name="vid">The version identifier for the resrouce</param>
		/// <returns>A list of Tags for the resource instance</returns>
		public IEnumerable<Tag> Tags<TResource>(string id, string vid = null)
		{
			string collection = ModelInfo.GetResourceNameForType(typeof(TResource));
			return internalGetTags(collection, id, vid);
		}

		/// <summary>
		/// Add one or more tags to a resource at a given location
		/// </summary>
		/// <param name="location">The url of the Resource to affix the tags to. This can be a Resource id url or a version-specific</param>
		/// <param name="tags">List of tags to add to the resource</param>
		/// <remarks>Affixing tags to a resource (or version of the resource) is not considered an update, so does 
		/// not create a new version.</remarks>
		public void AffixTags(Uri location, IEnumerable<Tag> tags)
		{
			if (location == null) throw Error.ArgumentNull("location");
			if (tags == null) throw Error.ArgumentNull("tags");

			var collection = getCollectionFromLocation(location);
			var id = getIdFromLocation(location);
			var version = new ResourceIdentity(location).VersionId;

			var rl = new RestUrl(Endpoint).ResourceTags(collection, id, version);

			var req = createFhirRequest(rl.Uri, "POST");
			req.SetBody(new TagList(tags), PreferredFormat);

			doRequest(req, HttpStatusCode.OK, resp => true);
		}

		/// <summary>
		/// Remove one or more tags from a resource at a given location
		/// </summary>
		/// <param name="location">The url of the Resource to remove the tags from. This can be a Resource id url or a version-specific</param>
		/// <param name="tags">List of tags to delete</param>
		/// <remarks>Removing tags to a resource (or version of the resource) is not considered an update, 
		/// so does not create a new version.</remarks>
		public void DeleteTags(Uri location, IEnumerable<Tag> tags)
		{
			if (location == null) throw Error.ArgumentNull("location");
			if (tags == null) throw Error.ArgumentNull("tags");

			var collection = getCollectionFromLocation(location);
			var id = getIdFromLocation(location);
			var version = new ResourceIdentity(location).VersionId;

			var rl = new RestUrl(Endpoint).DeleteResourceTags(collection, id, version);

			var req = createFhirRequest(rl.Uri, "POST");
			req.SetBody(new TagList(tags), PreferredFormat);

			doRequest(req, new[] { HttpStatusCode.OK, HttpStatusCode.NoContent }, resp => true);
		}

		/// <summary>
		/// Inspect or modify the HttpWebRequest just before the FhirClient issues a call to the server
		/// </summary>
		/// <param name="request">The request as it is about to be sent to the server</param>
		/// <param name="rawRequest"></param>
		protected virtual void BeforeRequest(FhirRequest request, HttpWebRequest rawRequest)
		{
			// Default implementation: call event
			OnBeforeRequest?.Invoke(this, new BeforeRequestEventArgs(request, rawRequest));
		}

		/// <summary>
		/// Inspect the HttpWebResponse as it came back from the server 
		/// </summary>
		/// <param name="fhirResponse"></param>
		/// <param name="request"></param>
		/// <param name="webResponse"></param>
		/// <param name="rawRequest"></param>
		protected virtual void AfterResponse(FhirResponse fhirResponse, FhirRequest request, WebResponse webResponse, HttpWebRequest rawRequest)
		{
			// Default implementation: call event
			OnAfterResponse?.Invoke(this, new AfterResponseEventArgs(fhirResponse, request, webResponse, rawRequest));
		}

		//-------------

		//TODO: Delete
		public string Raw(Uri uri, string method, string body, ResourceFormat? format, string contentType = null)
		{
			FhirRequest req = createFhirRequest(uri, method);

			if (!string.IsNullOrEmpty(body))
				req.SetBody(body, ResourceFormat.Xml);

			if (!string.IsNullOrEmpty(contentType))
				req.ContentType = contentType;

			FhirResponse response = req.GetResponse(format);

			if (response.Result == HttpStatusCode.OK)
				return response.GetBodyAsString();

			throw new FhirOperationException("Operation failed with status code " + response.Result, req, response);
		}


		/// <summary>
		/// Returns FHIR resource by id.
		/// </summary>
		public ResourceEntry<TResource> Read<TResource>(ulong id) where TResource : Resource, new()
		{
			return Read<TResource>(ModelInfo.GetCollectionName<TResource>() + "/" + id);
		}

		/// <summary>
		/// Override for Search(IList&lt;string&gt;), but with single criteria.
		/// </summary>
		public Bundle Search<TResource>(string criteria = null) where TResource : Resource, new()
		{
			string[] search = null;
			if (criteria != null)
				search = new[] { criteria };

			return Search<TResource>(search);
		}

		/// <summary>
		/// Searches for the resource, but does not parse the response.
		/// </summary>
		public string SearchRaw(string resource, IList<string> criteria)
		{
			var query = new Query(resource, criteria, null, null);

			RestUrl url = new RestUrl(Endpoint);
			url = url.Search(query);

			FhirRequest req = createFhirRequest(makeAbsolute(url.Uri), "GET");

			return doRequest(req, HttpStatusCode.OK, resp => resp.GetBodyAsString(), ResourceFormat.Unknown);
		}

		/// <summary>
		/// Returns signed or not signed PDF.
		/// </summary>
		/// <param name="location">Location of the document (i.e. Documents/1). id is Composition id.</param>
		/// <returns>PDF document</returns>
		public ResourceEntry<Binary> Pdf(Uri location)
		{
			location = makeAbsolute(location);
			var request = createFhirRequest(location, "GET");

			return doRequest(request, HttpStatusCode.OK, resp => resp.GetBodyAsEntry<Binary>(request), ResourceFormat.Pdf);
		}

		/// <summary>
		/// Returns signed or not PDF.
		/// </summary>
		/// <param name="location">Location of the document (i.e. Documents/1). id is Composition id.</param>
		public ResourceEntry<Binary> Pdf(string location)
		{
			return Pdf(new Uri(location, UriKind.Relative));
		}

		/// <summary>
		/// Returns signed or not PDF.
		/// </summary>
		/// <param name="id">id of the Composition</param>
		public ResourceEntry<Binary> Pdf(ulong id)
		{
			return Pdf("Documents/" + id);
		}

		/// <summary>
		/// Confirms document signing. You must call this function or else signing will not be visible in ESPBI.
		/// </summary>
		/// <param name="id">ID of the Composition</param>
		/// <param name="pdf">Optional PDF document (if it was signed locally, not through gosign)</param>
		public void ConfirmSigned(ulong id, byte[] pdf = null)
		{
			ConfirmSigned("Documents/" + id + "/confirm", pdf);
		}

		/// <summary>
		/// Confirms document signing. You must call this function or else signing will not be visible in ESPBI.
		/// </summary>
		/// <param name="location">Signing URL (something like Documents/id/confirm)</param>
		/// <param name="pdf">Optional PDF document (if it was signed locally, not through gosign)</param>
		public void ConfirmSigned(string location, byte[] pdf = null)
		{
			ConfirmSigned(new Uri(location, UriKind.Relative), pdf);
		}

		/// <summary>
		/// Confirms document signing. You must call this function or else signing will not be visible in ESPBI.
		/// </summary>
		/// <param name="location">Signing URL (something like Documents/id/confirm)</param>
		/// <param name="pdf">Optional PDF document (if it was signed locally, not through gosign)</param>
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
		/// Confirms whole transaction instead of single composition.
		/// </summary>
		/// <param name="id">Transaction id</param>
		/// <returns>List of confirmed compositions.</returns>
		public IList<string> ConfirmSignedTransaction(ulong id)
		{
			Uri location = makeAbsolute(new Uri("Documents/confirm", UriKind.Relative));
			location = new Uri(location.ToString() + "?transaction=" + id);

			var req = createFhirRequest(location, "POST");
			req.SetBody("☺", ResourceFormat.Octet);

			return doRequest<IList<string>>(req, new[] { HttpStatusCode.OK, HttpStatusCode.NoContent }, resp =>
			{
				string body = resp.GetBodyAsString();
				if (!string.IsNullOrEmpty(body))
					return body.Trim('[', ']').Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

				return null;
			}, ResourceFormat.Unknown);
		}

		/// <summary>
		/// Generates URL for signing single Composition in gosign.lt
		/// </summary>
		/// <param name="id">Composition ID</param>
		/// <param name="returnURL">URL to redirect to after signing was sucessfully completed.</param>
		/// <returns></returns>
		public string GetSignUrl(ulong id, string returnURL)
		{
			Uri location = makeAbsolute(new Uri(string.Format("Documents/{0}/sign", id), UriKind.Relative));
			location = new Uri(location.ToString() + "?id=" + string.Join(",", id) + "&returnUrl=" + (returnURL ?? "https://google.lt"));

			var req = createFhirRequest(location, "GET");

			return doRequest(req, new[] { HttpStatusCode.OK, HttpStatusCode.NoContent }, resp => resp.Location);
		}

		/// <summary>
		/// Generates URL for signing multiple Compositions in gosign.lt. Url contains transaction ID, that later can be used in ConfirmSignedTransaction(ulong);
		/// </summary>
		/// <param name="ids">List of Composition id</param>
		/// /// <param name="returnURL">URL to redirect to after signing was sucessfully completed.</param>
		public string GetSignUrl(IList<ulong> ids, string returnURL)
		{
			if (ids?.Count > 0)
			{
				Uri location = makeAbsolute(new Uri(string.Format("Documents/sign"), UriKind.Relative));
				location = new Uri(location.ToString() + "?id=" + string.Join(",", ids) + "&returnUrl=" + (returnURL ?? "https://google.lt"));

				var req = createFhirRequest(location, "GET");
				return doRequest(req, new[] { HttpStatusCode.OK, HttpStatusCode.NoContent }, resp => resp.Location);
			}

			return null;
		}

		/// <summary>
		/// SSO authentication token.
		/// </summary>
		/// <param name="endpoint">Something like https://sso-mokymai.esveikata.lt</param>
		/// <returns></returns>
		public string GetSsoAuthToken(string endpoint)
		{
			var uri = new RestUrl(endpoint).AddPath("espbi-sso/api/auth/login");
			FhirRequest req = createFhirRequest(uri.Uri, "GET");

			return doRequest(req, HttpStatusCode.OK, resp => resp.GetBodyAsString());
		}

		/// <summary>
		/// Returns full composition and it's related documents in one query,
		/// </summary>
		/// <param name="id">ID of composition</param>
		public Bundle Document(ulong id)
		{
			return Document(new Uri("Documents/" + id, UriKind.Relative));
		}

		/// <summary>
		/// Returns full composition and it's related documents in one query.
		/// </summary>
		/// <param name="location">Location of composition document (ie. Documents/1)</param>
		public Bundle Document(Uri location)
		{
			location = makeAbsolute(location);
			var request = createFhirRequest(location, "GET");
			request.IsForBundle = true;

			return doRequest(request, HttpStatusCode.OK, resp => resp.GetBodyAsBundle());
		}

		/// <summary>
		/// Retrievs information about drug interactions.
		/// </summary>
		/// <param name="query"></param>
		/// <returns></returns>
		public DrugInteraction GetDrugInteraction(DrugInteractionQuery query)
		{
			var uri = new RestUrl(Endpoint).AddPath("ERXCustom", "MedicationPrescription", "drugInteraction");
			var request = createFhirRequest(uri.Uri, "POST");
			request.SetBody(EspbiSerializer.Serialize(query), ResourceFormat.Xml);
			request.IsForBundle = true;
			request.ContentType = ContentType.XML_CONTENT_HEADER;

			return doRequest(request, HttpStatusCode.OK, resp => EspbiSerializer.Deserialize<DrugInteraction>(resp.Body), ResourceFormat.Unknown);
		}

		public bool IsFirstPrescription(ulong patientID, string form, string subtance = null, string strength = null)
		{
			var query = new Query("MedicationPrescription/prescriptionInfo", null);

			if (patientID != 0)
				query.AddParameter("patientId", patientID.ToString());

			if (!string.IsNullOrEmpty(form))
				query.AddParameter("pharmFormCode", form);

			if (!string.IsNullOrEmpty(subtance))
				query.AddParameter("activeSubstances", subtance);

			if (!string.IsNullOrEmpty(strength))
				query.AddParameter("strength", strength);

			RestUrl url = new RestUrl(Endpoint).Query(query);

			FhirRequest req = createFhirRequest(makeAbsolute(url.Uri), "GET");

			return doRequest(req, HttpStatusCode.OK, resp => EspbiSerializer.Deserialize<PrescriptionInfo>(resp.Body)?.IsFirstPrescribing ?? true, ResourceFormat.Unknown);
		}

		#region Template API

		public TDto Document<TDto>(Template<TDto> template, ulong id)
		{
			var request = createFhirRequest(makeAbsolute(new Uri("Documents/" + id, UriKind.Relative)), "GET");
			request.IsForBundle = true;

			return doRequest(request, HttpStatusCode.OK, resp => template.Read(resp));
		}

		/// <summary>
		/// Reads data based on a template.
		/// </summary>
		/// <typeparam name="TDto">Type of DTO object</typeparam>
		/// <param name="template">Template for deserializing received data</param>
		/// <param name="id">ID of resource to fetch</param>
		/// <returns>Transforms response into DTO using template.</returns>
		public TDto Read<TDto>(Template<TDto> template, ulong id)
		{
			if (template == null) throw Error.ArgumentNull(nameof(template));

			var request = createFhirRequest(makeAbsolute(template.GetLocation(id)), "GET");
			return doRequest(request, HttpStatusCode.OK, resp => template.Read(resp));
		}

		/// <summary>
		/// Reads data based on a template.
		/// </summary>
		/// <typeparam name="TDto">Type of DTO object</typeparam>
		/// <param name="template">Template for deserializing received data</param>
		/// <param name="query">Query to fetch the resource. Query must return only one result.</param>
		/// <returns>Transforms response into DTO using template.</returns>
		public TDto Read<TDto>(Template<TDto> template, Query query)
		{
			if (template == null) throw Error.ArgumentNull(nameof(template));
			if (query == null) throw Error.ArgumentNull(nameof(query));

			var request = createFhirRequest(makeAbsolute(new RestUrl(Endpoint).Search(query).Uri), "GET");
			request.IsForBundle = true;

			return doRequest(request, HttpStatusCode.OK, resp => template.Read(resp));
		}

		/// <summary>
		/// Reads data based on a template.
		/// </summary>
		/// <typeparam name="TDto">Type of DTO object</typeparam>
		/// <typeparam name="TCriteria">Type of Search query</typeparam>
		/// <param name="template">Template for deserializing received data</param>
		/// <param name="query">ID of resource to fetch</param>
		/// <param name="pageCount">Count of total pages</param>
		/// <returns>Transforms response into DTO using template.</returns>
		public IList<TDto> Search<TDto, TCriteria>(SearchableTemplate<TDto, TCriteria> template, TCriteria query, out int pageCount)
		{
			if (template == null) throw Error.ArgumentNull(nameof(template));

			pageCount = 0;
			IList<TDto> result = null;

			var fhirQuery = template.GetSearcQuery(query);

			if (fhirQuery != null)
			{
				var request = createFhirRequest(makeAbsolute(new RestUrl(Endpoint).Search(fhirQuery).Uri), "GET");
				request.IsForBundle = true;

				int totalPages = 0;
				int totalResults = 0;
				result = doRequest(request, HttpStatusCode.OK, resp => template.ReadAtomSearch(resp, out totalPages, out totalResults));

				if (fhirQuery.Count == 0)
					pageCount = totalResults;
				else
					pageCount = totalPages;
			}

			return result;
		}

		/// <summary>
		/// Creates a new resource based on data and the template. Create is executed in a transaction.
		/// </summary>
		/// <typeparam name="TDto">Type of DTO object</typeparam>
		/// <param name="template">Template to transform data into a transaction</param>
		/// <param name="data">Data to sent to the server</param>
		/// <returns>TemplateResponseReader to read ids from response</returns>
		public TemplateResponseReader Create<TDto>(Template<TDto> template, TDto data)
		{
			if (data == null) throw Error.ArgumentNull(nameof(data));
			if (template == null) throw Error.ArgumentNull(nameof(template));

			var context = template.GetGetter(data, null);
			var req = createFhirRequest(Endpoint, "POST");
			req.SetBody(template.Create(context), PreferredFormat);

			return doRequest(req, HttpStatusCode.OK, resp => template.GetReaderForCreate(resp.GetBodyAsString(), context));
		}

		public TemplateResponseReader Transaction(UpdateData data)
		{
			if (data == null) throw Error.ArgumentNull(nameof(data));
			return Transaction(data.Bundle, data.Context);
		}

		public TemplateResponseReader Transaction(Bundle bundle, IDataGetterContext context)
		{
			if (bundle == null) throw Error.ArgumentNull(nameof(bundle));

			var req = createFhirRequest(Endpoint, "POST");
			req.SetBody(bundle, PreferredFormat);

			return doRequest(req, HttpStatusCode.OK, resp => new TemplateResponseReader(resp.GetBodyAsString(), context));
		}

		public UpdateData GetForUpdate<TDto>(Template<TDto> template, Query query, Func<TDto, TDto> update)
		{
			if (template == null) throw Error.ArgumentNull(nameof(template));

			if (query != null)
			{
				var request = createFhirRequest(makeAbsolute(new RestUrl(Endpoint).Search(query).Uri), "GET");
				request.IsForBundle = true;

				return doRequest(request, HttpStatusCode.OK, resp => template.Update(resp, update));
			}

			return null;
		}

		public UpdateData GetForUpdate<TDto>(Template<TDto> template, long id, Func<TDto, TDto> update)
		{
			if (template == null) throw Error.ArgumentNull(nameof(template));

			var request = createFhirRequest(makeAbsolute(new Uri("Documents/" + id, UriKind.Relative)), "GET");
			request.IsForBundle = true;

			return doRequest(request, HttpStatusCode.OK, resp => template.Update(resp, update));
		}

		#endregion




		protected virtual void Dispose(bool disposing)
		{
			if (!IsDisposed)
			{
				if (disposing)
				{
					var beforeRequest = OnBeforeRequest?.GetInvocationList();
					if (beforeRequest?.Length > 0)
					{
						foreach (BeforeRequestEventHandler item in beforeRequest)
							OnBeforeRequest -= item;
					}

					var afterRequest = OnAfterResponse?.GetInvocationList();
					if (afterRequest?.Length > 0)
					{
						foreach (AfterResponseEventHandler item in afterRequest)
							OnAfterResponse -= item;
					}
				}

				IsDisposed = true;
			}
		}

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		~FhirClient()
		{
			Dispose(false);
		}


	}
}
