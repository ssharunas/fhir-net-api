/* 
 * Copyright (c) 2014, Furore (info@furore.com) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using Hl7.Fhir.Support;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace Hl7.Fhir.Rest
{
	/// <summary>
	/// The ResourceIdentity Class can be used to describe the location of an actual instance of a fhir resource.
	/// It is not designed to be able to handle all URLs for bundles, such as searching, or history retrieval.
	/// (If this class is used with the resource history, then the results will not be as expected)
	/// </summary>
	[Serializable]
	internal class ResourceIdentity : Uri
	{
		private List<string> _components;
		private Uri _endpoint;
		private string _collection;
		private string _id;
		private string _versionId;
		private Uri _operationPath;

		/// <summary>
		/// Creates an Resource Identity instance for a Resource given a resource's location.
		/// </summary>
		/// <param name="uri">Relative or absolute location of a Resource</param>
		/// <returns></returns>
		public ResourceIdentity(string uri) : base(uri, UriKind.RelativeOrAbsolute) { }

		/// <summary>
		/// Creates an Resource Identity instance for a Resource given a resource's location.
		/// </summary>
		/// <param name="uri">Relative or absolute location of a Resource</param>
		/// <returns></returns>
		public ResourceIdentity(Uri uri) : base(uri.ToString(), UriKind.RelativeOrAbsolute) { }

		internal ResourceIdentity(string uri, UriKind kind) : base(uri, kind) { }

		#region << Serialization Implementation >>
		// The default serialization is all that is required as this class does
		// not contain any of it's own properties that are not contained in the actual Uri
		protected ResourceIdentity(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		protected virtual new void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}

		#endregion

		/// <summary>
		/// Creates an absolute Uri representing a Resource identitity for a given resource type, id and optional version.
		/// </summary>
		/// <param name="endpoint">Absolute path giving the FHIR service endpoint</param>
		/// <param name="collection">Name of the collection (resource type)</param>
		/// <param name="id">The resource's logical id</param>
		/// <param name="vid">The resource's version id</param>
		/// <returns></returns>
		public static ResourceIdentity Build(Uri endpoint, string collection, string id, string vid = null)
		{
			if (collection is null) throw Error.ArgumentNull(nameof(collection));
			if (id is null) throw Error.ArgumentNull(nameof(id));
			if (!endpoint.IsAbsoluteUri) throw Error.Argument(nameof(endpoint), "endpoint must be an absolute path");

			if (vid != null)
				return new ResourceIdentity(construct(endpoint, collection, id, RestOperation.HISTORY, vid));
			else
				return new ResourceIdentity(construct(endpoint, collection, id));
		}

		/// <summary>
		/// Creates a relative Uri representing a Resource identitity for a given resource type, id and optional version.
		/// </summary>
		/// <param name="collection">Name of the collection (resource type)</param>
		/// <param name="id">The resource's logical id</param>
		/// <param name="vid">The resource's version id</param>
		/// <returns></returns>
		public static ResourceIdentity Build(string collection, string id, string vid = null)
		{
			if (collection is null) throw Error.ArgumentNull(nameof(collection));
			if (id is null) throw Error.ArgumentNull(nameof(id));

			string url = vid != null ?
				string.Format("{0}/{1}/{2}/{3}", collection, id, RestOperation.HISTORY, vid) :
				string.Format("{0}/{1}", collection, id);

			return new ResourceIdentity(url, UriKind.Relative);
		}

		private List<string> Components
		{
			get
			{
				if (_components is null)
				{
					string path = IsAbsoluteUri ? LocalPath : ToString();
					_components = path.Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries).ToList();
				}

				return _components;
			}
		}

		/// <summary>
		/// This is the FHIR service endpoint where the resource is located.
		/// </summary>
		public Uri Endpoint
		{
			get
			{
				if (_endpoint is null)
				{
					int count = Components.Count;

					if (count < 2)
						return null;

					int index = Components.IndexOf(RestOperation.HISTORY);
					int n = (index > 0) ? index - 2 : count - 2;
					IEnumerable<string> _components = Components.Skip(n);
					string path = string.Join("/", _components).Trim('/');
					string s = ToString();
					string endpoint = s.Remove(s.LastIndexOf(path));

					_endpoint = (endpoint.Length > 0) ? new Uri(endpoint) : null;
				}

				return _endpoint;
			}
		}

		/// <summary>
		/// The name of the resource as it occurs in the Resource url
		/// </summary>
		public string Collection
		{
			get
			{
				if (_collection is null)
				{
					int index = Components.IndexOf(RestOperation.HISTORY);
					if (index > -1 && index == Components.Count - 1) return null; // illegal use, there's just a _history component, but no version id

					if (index >= 2)
					{
						_collection = Components[index - 2];
					}
					else if (Components.Count > 2)
					{
						_collection = Components[Components.Count - 2];
					}
					else if (Components.Count == 2 && index == -1)
					{
						_collection = Components[0];
					}

					if (!Model.ModelInfo.IsKnownResource(_collection))
						Message.Warn($"Unknown resource identity {_collection} in {nameof(ResourceIdentity)}");
				}

				return _collection;
			}
		}

		/// <summary>
		/// The logical id of the resource as it occurs in the Resource url
		/// </summary>
		public string Id
		{
			get
			{
				if (_id is null)
				{
					int index = Components.IndexOf(RestOperation.HISTORY);

					if (index > -1 && index == Components.Count - 1)
						_id = null; // illegal use, there's just a _history component, but no version id
					else if (index >= 2)
						_id = Components[index - 1];
					else if (index == -1 && Components.Count >= 2)
						_id = Components[Components.Count - 1];
					else
						_id = null;
				}

				return _id;
			}
		}

		/// <summary>
		/// The version id of the resource as it occurs in the Resource url
		/// </summary>
		public string VersionId
		{
			get
			{
				if (_versionId is null)
				{
					int index = Components.IndexOf(RestOperation.HISTORY);
					if (index > -1 && index == Components.Count - 1)
						_versionId = null; // illegal use, there's just a _history component, but no version id

					if (index >= 2 && Components.Count >= 4 && index < Components.Count - 1)
						_versionId = Components[index + 1];
				}

				return _versionId;
			}
		}

		/// <summary>
		/// Returns a Uri that is a relative version of the ResourceIdentity
		/// </summary>
		public Uri OperationPath
		{
			get
			{
				if (_operationPath is null)
					_operationPath = Build(Collection, Id, VersionId);//this always makes the uri relative

				return _operationPath;
			}
		}

		/// <summary>
		/// Indicates whether this ResourceIdentity is version-specific (has a _history part)
		/// </summary>
		public bool HasVersion => VersionId != null;

		/// <summary>
		/// Returns a new ResourceIdentity made specific for the given version
		/// </summary>
		/// <param name="version">The version to add to the ResourceIdentity (part after the _history/)</param>
		/// <returns></returns>
		public ResourceIdentity WithVersion(string version)
		{
			if (Endpoint is null)
				return Build(Collection, Id, version);
			else
				return Build(Endpoint, Collection, Id, version);
		}

		/// <summary>
		/// Turns a version-specific ResourceIdentity into a non-version-specific ResourceIdentity
		/// </summary>
		/// <returns></returns>
		public ResourceIdentity WithoutVersion()
		{
			if (Endpoint is null)
				return Build(Collection, Id);
			else
				return Build(Endpoint, Collection, Id);
		}

		private static Uri construct(Uri endpoint, IEnumerable<string> components)
		{
			UriBuilder builder = new UriBuilder(endpoint);
			string _path = builder.Path;

			if (!_path.EndsWith("/"))
				_path += "/";

			string _components = string.Join("/", components).Trim('/');
			builder.Path = _path + _components;

			return builder.Uri;
		}

		private static Uri construct(Uri endpoint, params string[] components)
		{
			return construct(endpoint, (IEnumerable<string>)components);
		}

	}
}
