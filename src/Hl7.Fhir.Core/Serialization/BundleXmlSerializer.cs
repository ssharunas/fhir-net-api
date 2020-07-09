/* 
 * Copyright (c) 2014, Furore (info@furore.com) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using Hl7.Fhir.Model;
using Hl7.Fhir.Support;
using System;
using System.Xml;
using System.Xml.Linq;

namespace Hl7.Fhir.Serialization
{
	internal static class BundleXmlSerializer
	{
		public static void WriteTo(Bundle bundle, XmlWriter writer, bool summary = false)
		{
			if (bundle is null)
				Error.ArgumentNull(nameof(bundle), "Bundle cannot be null");

			var root = new XElement(XmlNs.XATOM + BundleXmlParser.XATOM_FEED);

			if (!string.IsNullOrWhiteSpace(bundle.Title))
				root.Add(xmlCreateTitle(bundle.Title));

			if (SerializationUtil.HasUriValue(bundle.Id))
				root.Add(xmlCreateId(bundle.Id));

			if (bundle.LastUpdated != null)
				root.Add(new XElement(XmlNs.XATOM + BundleXmlParser.XATOM_UPDATED, bundle.LastUpdated));

			if (!string.IsNullOrWhiteSpace(bundle.AuthorName))
				root.Add(xmlCreateAuthor(bundle.AuthorName, bundle.AuthorUri));

			if (bundle.TotalResults != null)
				root.Add(new XElement(XmlNs.XOPENSEARCH + BundleXmlParser.XATOM_TOTALRESULTS, bundle.TotalResults));

			if (bundle.Links?.Count > 0)
			{
				foreach (var l in bundle.Links)
					root.Add(xmlCreateLink(l.Rel, l.Uri));
			}

			if (bundle.Tags?.Count > 0)
			{
				foreach (var tag in bundle.Tags)
					root.Add(TagListSerializer.CreateTagCategoryPropertyXml(tag));
			}

			foreach (var entry in bundle.Entries)
				root.Add(createEntry(entry, summary));

			root.WriteTo(writer);
		}

		public static void WriteTo(BundleEntry entry, XmlWriter writer, bool summary = false)
		{
			if (entry is null)
				Error.ArgumentNull(nameof(entry), "Entry cannot be null");

			new XDocument(createEntry(entry, summary)).WriteTo(writer);
		}

		private static XElement createEntry(BundleEntry entry, bool summary)
		{
			XElement result;

			if (entry is ResourceEntry)
			{
				ResourceEntry re = (ResourceEntry)entry;
				result = new XElement(XmlNs.XATOM + BundleXmlParser.XATOM_ENTRY);

				if (!string.IsNullOrEmpty(re.Title))
					result.Add(xmlCreateTitle(re.Title));

				if (SerializationUtil.HasUriValue(entry.Id))
					result.Add(xmlCreateId(entry.Id));

				if (re.LastUpdated != null)
					result.Add(new XElement(XmlNs.XATOM + BundleXmlParser.XATOM_UPDATED, re.LastUpdated.Value));

				if (re.Published != null)
					result.Add(new XElement(XmlNs.XATOM + BundleXmlParser.XATOM_PUBLISHED, re.Published.Value));

				if (!string.IsNullOrWhiteSpace(re.AuthorName))
					result.Add(xmlCreateAuthor(re.AuthorName, re.AuthorUri));
			}
			else
			{
				result = new XElement(XmlNs.XATOMPUB_TOMBSTONES + BundleXmlParser.XATOM_DELETED_ENTRY);

				if (SerializationUtil.HasUriValue(entry.Id))
					result.Add(new XAttribute(BundleXmlParser.XATOM_DELETED_REF, entry.Id.ToString()));

				if (((DeletedEntry)entry).When != null)
					result.Add(new XAttribute(BundleXmlParser.XATOM_DELETED_WHEN, ((DeletedEntry)entry).When));
			}

			if (entry.Links != null)
			{
				foreach (var link in entry.Links)
				{
					if (link.Uri != null)
						result.Add(xmlCreateLink(link.Rel, link.Uri));
				}
			}

			if (entry.Tags != null)
			{
				foreach (var tag in entry.Tags)
					result.Add(TagListSerializer.CreateTagCategoryPropertyXml(tag));
			}

			if (entry is ResourceEntry)
			{
				ResourceEntry re = (ResourceEntry)entry;
				if (re.Resource != null)
				{
					result.Add(new XElement(XmlNs.XATOM + BundleXmlParser.XATOM_CONTENT,
						new XAttribute(BundleXmlParser.XATOM_CONTENT_TYPE, "text/xml"),
						getContentAsXElement(re.Resource, summary)));
				}

				// Note: this is a read-only property, so it is serialized but never parsed
				if (entry.Summary != null)
				{
					var xelem = XElement.Parse(entry.Summary);
					xelem.Name = XmlNs.XHTMLNS + xelem.Name.LocalName;

					result.Add(new XElement(XmlNs.XATOM + BundleXmlParser.XATOM_SUMMARY,
							new XAttribute(BundleXmlParser.XATOM_CONTENT_TYPE, "xhtml"), xelem));
				}
			}

			return result;
		}

		private static object getContentAsXElement(Resource resource, bool summary)
		{
			return XElement.Parse(FhirSerializer.SerializeResourceToXml(resource, summary));
		}

		private static XElement xmlCreateId(Uri id)
		{
			return new XElement(XmlNs.XATOM + BundleXmlParser.XATOM_ID, id.ToString());
		}

		private static XElement xmlCreateLink(string rel, Uri uri)
		{
			return new XElement(XmlNs.XATOM + BundleXmlParser.XATOM_LINK, new XAttribute(BundleXmlParser.XATOM_LINK_REL, rel), new XAttribute(BundleXmlParser.XATOM_LINK_HREF, uri.ToString()));
		}

		private static XElement xmlCreateTitle(string title)
		{
			return new XElement(XmlNs.XATOM + BundleXmlParser.XATOM_TITLE, new XAttribute(BundleXmlParser.XATOM_CONTENT_TYPE, "text"), title);
		}

		private static XElement xmlCreateAuthor(string name, string uri)
		{
			var result = new XElement(XmlNs.XATOM + BundleXmlParser.XATOM_AUTHOR);

			if (!string.IsNullOrEmpty(name))
				result.Add(new XElement(XmlNs.XATOM + BundleXmlParser.XATOM_AUTH_NAME, name));

			if (!string.IsNullOrEmpty(uri))
				result.Add(new XElement(XmlNs.XATOM + BundleXmlParser.XATOM_AUTH_URI, uri));

			return result;
		}
	}
}
