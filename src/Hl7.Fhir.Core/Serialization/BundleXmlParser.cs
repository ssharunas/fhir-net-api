/* 
 * Copyright (c) 2014, Furore (info@furore.com) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using Hl7.Fhir.Model;
using Hl7.Fhir.Serialization.Xml;
using Hl7.Fhir.Support;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Xml.Linq;

namespace Hl7.Fhir.Serialization
{
	internal static class BundleXmlParser
	{
		public const string XATOM_FEED = "feed";
		public const string XATOM_DELETED_ENTRY = "deleted-entry";
		public const string XATOM_DELETED_WHEN = "when";
		public const string XATOM_DELETED_REF = "ref";
		public const string XATOM_LINK = "link";
		public const string XATOM_LINK_REL = "rel";
		public const string XATOM_LINK_HREF = "href";
		//public const string XATOM_CONTENT_BINARY = "Binary";
		public const string XATOM_CONTENT_TYPE = "type";
		//public const string XATOM_CONTENT_BINARY_TYPE = "contentType";
		public const string XATOM_TITLE = "title";
		public const string XATOM_UPDATED = "updated";
		public const string XATOM_ID = "id";
		public const string XATOM_ENTRY = "entry";
		public const string XATOM_PUBLISHED = "published";
		public const string XATOM_AUTHOR = "author";
		public const string XATOM_AUTH_NAME = "name";
		public const string XATOM_AUTH_URI = "uri";
		public const string XATOM_CATEGORY = "category";
		public const string XATOM_CAT_TERM = "term";
		public const string XATOM_CAT_SCHEME = "scheme";
		public const string XATOM_CAT_LABEL = "label";
		public const string XATOM_CONTENT = "content";
		public const string XATOM_SUMMARY = "summary";
		public const string XATOM_TOTALRESULTS = "totalResults";

		internal static Bundle Load(XmlReader reader)
		{
			IFhirXmlNode feed;
			var settings = new XmlReaderSettings()
			{
				IgnoreComments = true,
				IgnoreProcessingInstructions = true,
				IgnoreWhitespace = true,
			};

			try
			{
				feed = FhirXmlNode.Create(XmlReader.Create(reader, settings));  // new FhirXmlNode(XmlReader.Create(reader, settings));
				if (!feed.Is(XmlNs.ATOM, "feed"))
					throw Error.Format("Input data is not an Atom feed");
			}
			catch (Exception ex)
			{
				throw Error.Format("Exception while loading feed: " + ex.Message, null, ex);
			}

			return Load(feed);
		}

		internal static Bundle Load(IFhirXmlNode feed)
		{
			Bundle result;

			try
			{
				result = new Bundle()
				{
					Links = getLinks(feed.Elements(XmlNs.ATOM, XATOM_LINK)),
					Id = feed.Element(XmlNs.ATOM, XATOM_ID)?.ValueAsUri,
					Title = feed.Element(XmlNs.ATOM, XATOM_TITLE)?.ValueAsString,
					LastUpdated = feed.Element(XmlNs.ATOM, XATOM_UPDATED)?.ValueAsInstant,
					AuthorUri = feed.Element(XmlNs.ATOM, XATOM_AUTHOR)?.Element(XmlNs.ATOM, XATOM_AUTH_URI)?.ValueAsString,
					AuthorName = feed.Element(XmlNs.ATOM, XATOM_AUTHOR)?.Element(XmlNs.ATOM, XATOM_AUTH_NAME)?.ValueAsString,
					TotalResults = feed.Element(XmlNs.OPENSEARCH, XATOM_TOTALRESULTS)?.ValueAsInt,
					Tags = TagListParser.ParseTags(feed.Elements(XmlNs.ATOM, XATOM_CATEGORY)),
				};
			}
			catch (Exception exc)
			{
				throw Error.Format("Exception while parsing xml feed attributes: " + exc.Message);
			}

			result.Entries = loadEntries(feed.Elements().Where(elem => elem.Is(XmlNs.ATOM, XATOM_ENTRY) || elem.Is(XmlNs.ATOMPUB_TOMBSTONES, XATOM_DELETED_ENTRY)));

			return result;
		}

		private static IList<BundleEntry> loadEntries(IEnumerable<IFhirXmlNode> entries)
		{
			var result = new List<BundleEntry>();

			foreach (var entry in entries)
			{
				if (entry != null)
					result.Add(loadEntry(entry));
			}

			return result;
		}

		internal static BundleEntry LoadEntry(string xml)
		{
			return LoadEntry(FhirParser.XmlReaderFromString(xml));
		}

		internal static BundleEntry LoadEntry(XmlReader reader)
		{
			IFhirXmlNode entry;

			try
			{
				entry = FhirXmlNode.Create(reader);
			}
			catch (Exception ex)
			{
				throw Error.Format("Exception while loading entry: " + ex.Message, innerException: ex);
			}

			return loadEntry(entry);
		}

		private static BundleEntry loadEntry(IFhirXmlNode entry)
		{
			BundleEntry result;

			//try
			{
				if (entry.Is(XmlNs.ATOMPUB_TOMBSTONES, XATOM_DELETED_ENTRY))
				{
					result = new DeletedEntry()
					{
						Id = entry.Attribute(XATOM_DELETED_REF)?.ValueAsUri,
					};
				}
				else
				{
					var content = entry.Element(XmlNs.ATOM, XATOM_CONTENT);
					var id = entry.Element(XmlNs.ATOM, XATOM_ID)?.ValueAsUri;

					if (content != null)
					{
						var parsed = getContents(content);
						if (parsed != null)
							result = ResourceEntry.Create(parsed);
						else
							throw Error.Format("BundleEntry has a content element without content", content.Position);
					}
					else
					{
						result = SerializationUtil.CreateResourceEntryFromId(id);
					}

					result.Id = id;
				}

				result.Links = getLinks(entry.Elements(XmlNs.ATOM, XATOM_LINK));
				result.Tags = TagListParser.ParseTags(entry.Elements(XmlNs.ATOM, XATOM_CATEGORY));

				if (result is DeletedEntry)
				{
					((DeletedEntry)result).When = entry.Attribute(XATOM_DELETED_WHEN)?.ValueAsInstant;
				}
				else
				{
					ResourceEntry re = (ResourceEntry)result;
					re.Title = entry.Element(XmlNs.ATOM, XATOM_TITLE)?.ValueAsString;
					re.LastUpdated = entry.Element(XmlNs.ATOM, XATOM_UPDATED)?.ValueAsInstant;
					re.Published = entry.Element(XmlNs.ATOM, XATOM_PUBLISHED)?.ValueAsInstant;
					re.AuthorName = entry.Element(XmlNs.ATOM, XATOM_AUTHOR)?.Element(XmlNs.ATOM, XATOM_AUTH_NAME)?.ValueAsString;
					re.AuthorUri = entry.Element(XmlNs.ATOM, XATOM_AUTHOR)?.Element(XmlNs.ATOM, XATOM_AUTH_URI)?.ValueAsString;
				}
			}
			//catch (Exception exc)
			//{
			//	throw Error.Format("Exception while reading entry: " + exc.Message, entry.Position);
			//}

			return result;
		}

		private static UriLinkList getLinks(IEnumerable<IFhirXmlNode> links)
		{
			return new UriLinkList(
				links.Select(el => new UriLinkEntry
				{
					Rel = el.Attribute(XATOM_LINK_REL)?.ValueAsString,
					Uri = el.Attribute(XATOM_LINK_HREF)?.ValueAsUri,
				}));
		}

		private static Resource getContents(IFhirXmlNode content)
		{
			string contentType = content.Attribute(XATOM_CONTENT_TYPE)?.ValueAsString;

			if (contentType != "text/xml" && contentType != "application/xml+fhir")
			{
				throw Error.Format("Bundle Entry should have contents of type 'text/xml'", content.Position);
			}

#if DEBUG
			if (contentType == "application/xml+fhir")
			{
				Message.Debug("Bundle Entry should have contents of type 'text/xml'", content.Position);
			}
#endif

			IFhirXmlNode resource = null;

			try
			{
				resource = content.Elements().Single();
			}
			catch (Exception ex)
			{
				throw Error.Format("Entry <content> node should have a single child: the resource", content.Position, ex);
			}

			return (Resource)(ResourceReader.Deserialize(new XmlDomFhirReader(resource)));
		}
	}
}
