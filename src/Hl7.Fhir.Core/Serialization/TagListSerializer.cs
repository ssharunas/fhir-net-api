/* 
 * Copyright (c) 2014, Furore (info@furore.com) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using Hl7.Fhir.Model;
using Hl7.Fhir.Support;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Xml.Linq;

namespace Hl7.Fhir.Serialization
{
	internal static class TagListSerializer
	{
		public const string TAGLIST_TYPE = "taglist";

		internal static JProperty CreateTagCategoryPropertyJson(IEnumerable<Tag> tagList)
		{
			JArray jTags = new JArray();
			JProperty result = new JProperty(BundleXmlParser.XATOM_CATEGORY, jTags);

			foreach (Tag tag in tagList)
			{
				JObject jTag = new JObject();
				if (!string.IsNullOrEmpty(tag.Term))
					jTag.Add(new JProperty(BundleXmlParser.XATOM_CAT_TERM, tag.Term));
				if (!string.IsNullOrEmpty(tag.Label))
					jTag.Add(new JProperty(BundleXmlParser.XATOM_CAT_LABEL, tag.Label));
				jTag.Add(new JProperty(BundleXmlParser.XATOM_CAT_SCHEME, tag.Scheme.ToString()));
				jTags.Add(jTag);
			}

			return result;
		}

		internal static XElement CreateTagCategoryPropertyXml(Tag tag, bool useAtomNs = true)
		{
			XElement result = useAtomNs ?
				new XElement(XmlNs.XATOM + BundleXmlParser.XATOM_CATEGORY) :
				new XElement(XmlNs.XFHIR + BundleXmlParser.XATOM_CATEGORY);

			if (!string.IsNullOrEmpty(tag.Term))
				result.Add(new XAttribute(BundleXmlParser.XATOM_CAT_TERM, tag.Term));

			if (!string.IsNullOrEmpty(tag.Label))
				result.Add(new XAttribute(BundleXmlParser.XATOM_CAT_LABEL, tag.Label));

			result.Add(new XAttribute(BundleXmlParser.XATOM_CAT_SCHEME, tag.Scheme.ToString()));

			return result;
		}


	}
}
