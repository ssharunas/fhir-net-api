/* 
 * Copyright (c) 2014, Furore (info@furore.com) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using Hl7.Fhir.Model;
using Hl7.Fhir.Serialization.Xml;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Xml.Linq;

namespace Hl7.Fhir.Serialization
{
	internal static class TagListParser
	{
		internal static IList<Tag> ParseTags(IList<IFhirXmlNode> tags)
		{
			var result = new List<Tag>();

			if (tags?.Count > 0)
			{
				foreach (var tag in tags)
				{
					var scheme = tag.Attribute(BundleXmlParser.XATOM_CAT_SCHEME)?.ValueAsString;
					var term = tag.Attribute(BundleXmlParser.XATOM_CAT_TERM)?.ValueAsString;
					var label = tag.Attribute(BundleXmlParser.XATOM_CAT_LABEL)?.ValueAsString;

					result.Add(new Tag(term, scheme, label));
				}
			}

			return result;
		}

		internal static IList<Tag> ParseTags(JToken token)
		{
			var result = new List<Tag>();
			var tags = token as JArray;

			if (tags != null)
			{
				foreach (var tag in tags)
				{
					var scheme = tag.Value<string>(BundleXmlParser.XATOM_CAT_SCHEME);
					var term = tag.Value<string>(BundleXmlParser.XATOM_CAT_TERM);
					var label = tag.Value<string>(BundleXmlParser.XATOM_CAT_LABEL);

					result.Add(new Tag(term, scheme, label));
				}
			}

			return result;
		}
	}
}
