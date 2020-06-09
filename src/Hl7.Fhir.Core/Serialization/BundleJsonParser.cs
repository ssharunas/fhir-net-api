﻿/* 
 * Copyright (c) 2014, Furore (info@furore.com) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using Hl7.Fhir.Model;
using Hl7.Fhir.Support;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Hl7.Fhir.Serialization
{
	internal static class BundleJsonParser
	{
		internal const string JATOM_VERSION = "version";
		internal const string JATOM_DELETED = "deleted";

		private static int? intValueOrNull(JToken attr)
		{
			if (attr == null) return null;

			return attr.Value<int?>();
		}

		private static DateTimeOffset? instantOrNull(JToken attr)
		{
			if (attr == null) return null;

			return PrimitiveTypeConverter.ConvertTo<DateTimeOffset>(attr.Value<string>());
		}

		internal static Bundle Load(JsonReader reader)
		{
			JObject feed;

			try
			{
				reader.DateParseHandling = DateParseHandling.None;
				reader.FloatParseHandling = FloatParseHandling.Decimal;
				feed = JObject.Load(reader);

				if (feed.Value<string>(JsonDomFhirReader.RESOURCETYPE_MEMBER_NAME) != "Bundle")
					throw Error.Format("Input data is not an json FHIR bundle");
			}
			catch (Exception exc)
			{
				throw Error.Format("Exception while parsing feed: " + exc.Message);
			}

			Bundle result;

			try
			{
				result = new Bundle()
				{
					Title = feed.Value<string>(BundleXmlParser.XATOM_TITLE),
					LastUpdated = instantOrNull(feed[BundleXmlParser.XATOM_UPDATED]),
					Id = SerializationUtil.UriValueOrNull(feed[BundleXmlParser.XATOM_ID]),
					Links = getLinks(feed[BundleXmlParser.XATOM_LINK]),
					Tags = TagListParser.ParseTags(feed[BundleXmlParser.XATOM_CATEGORY]),
					AuthorName = (feed[BundleXmlParser.XATOM_AUTHOR] as JArray)
									?.Select(auth => auth.Value<string>(BundleXmlParser.XATOM_AUTH_NAME))
									?.FirstOrDefault(),
					AuthorUri = (feed[BundleXmlParser.XATOM_AUTHOR] as JArray)
									?.Select(auth => auth.Value<string>(BundleXmlParser.XATOM_AUTH_URI))
									?.FirstOrDefault(),
					TotalResults = intValueOrNull(feed[BundleXmlParser.XATOM_TOTALRESULTS])
				};
			}
			catch (Exception exc)
			{
				throw Error.Format("Exception while parsing json feed attributes: " + exc.Message);
			}

			var entries = feed[BundleXmlParser.XATOM_ENTRY];
			if (entries != null)
			{
				if (!(entries is JArray))
					throw Error.Format("The json feed contains a single entry, instead of an array");

				result.Entries = loadEntries((JArray)entries);
			}

			return result;
		}

		private static IList<BundleEntry> loadEntries(JArray entries)
		{
			var result = new List<BundleEntry>();

			foreach (var entry in entries)
			{
				if (entry.Type != JTokenType.Object)
					throw Error.Format("Expected a complex object when reading an entry", JsonDomFhirReader.GetLineInfo(entries));

				var loaded = loadEntry((JObject)entry);
				if (entry != null) result.Add(loaded);
			}

			return result;
		}

		internal static BundleEntry LoadEntry(JsonReader reader)
		{
			JObject entry;
			reader.DateParseHandling = DateParseHandling.None;
			reader.FloatParseHandling = FloatParseHandling.Decimal;

			try
			{
				entry = JObject.Load(reader);
			}
			catch (Exception exc)
			{
				throw Error.Format("Exception while parsing json for entry: " + exc.Message);
			}

			return loadEntry(entry);
		}

		private static BundleEntry loadEntry(JObject entry)
		{
			BundleEntry result;

			try
			{
				if (entry[JATOM_DELETED] != null)
				{
					result = new DeletedEntry
					{
						Id = SerializationUtil.UriValueOrNull(entry[BundleXmlParser.XATOM_ID])
					};
				}
				else
				{
					var content = entry[BundleXmlParser.XATOM_CONTENT];

					var id = SerializationUtil.UriValueOrNull(entry[BundleXmlParser.XATOM_ID]);
					if (id == null) throw Error.Format("BundleEntry found without an id");

					if (content != null)
					{
						var parsed = getContents(content);
						if (parsed != null)
							result = ResourceEntry.Create(parsed);
						else
							throw Error.Format($"BundleEntry {id} has a content element without content");
					}
					else
					{
						result = SerializationUtil.CreateResourceEntryFromId(id);
					}

					result.Id = id;
				}

				result.Links = getLinks(entry[BundleXmlParser.XATOM_LINK]);
				result.Tags = TagListParser.ParseTags(entry[BundleXmlParser.XATOM_CATEGORY]);

				if (result is DeletedEntry)
					((DeletedEntry)result).When = instantOrNull(entry[JATOM_DELETED]);
				else
				{
					var re = (ResourceEntry)result;
					re.Title = entry.Value<string>(BundleXmlParser.XATOM_TITLE);
					re.LastUpdated = instantOrNull(entry[BundleXmlParser.XATOM_UPDATED]);
					re.Published = instantOrNull(entry[BundleXmlParser.XATOM_PUBLISHED]);
					re.AuthorName = (entry[BundleXmlParser.XATOM_AUTHOR] as JArray)
							?.Select(auth => auth.Value<string>(BundleXmlParser.XATOM_AUTH_NAME))
							?.FirstOrDefault();
					re.AuthorUri = (entry[BundleXmlParser.XATOM_AUTHOR] as JArray)
							?.Select(auth => auth.Value<string>(BundleXmlParser.XATOM_AUTH_URI))
							?.FirstOrDefault();
				}
			}
			catch (Exception exc)
			{
				throw Error.Format("Exception while reading entry: " + exc.Message);
			}

			return result;
		}

		private static UriLinkList getLinks(JToken token)
		{
			var result = new UriLinkList();

			if (token is JArray links)
			{
				foreach (var link in links)
				{
					var uri = SerializationUtil.UriValueOrNull(link[BundleXmlParser.XATOM_LINK_HREF]);

					if (uri != null)
						result.Add(new UriLinkEntry
						{
							Rel = link.Value<string>(BundleXmlParser.XATOM_LINK_REL),
							Uri = uri
						});
				}
			}

			return result;
		}

		private static Resource getContents(JToken token)
		{
			return (Resource)(ResourceReader.Deserialize(new JsonDomFhirReader(token)));
		}
	}
}
