/* 
 * Copyright (c) 2014, Furore (info@furore.com) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using Hl7.Fhir.Model;
using Hl7.Fhir.Rest;
using Hl7.Fhir.Support;
using Newtonsoft.Json.Linq;
using System;
using System.Xml;

namespace Hl7.Fhir.Serialization
{
	internal static class SerializationUtil
	{
		public static ResourceEntry CreateResourceEntryFromId(Uri id)
		{
			// Figure out the resource type from the id
			ResourceIdentity rid = new ResourceIdentity(id);

			if (rid.Collection != null)
			{
				var classMapping = SerializationConfig.Inspector.FindClassMappingForResource(rid.Collection);
				return ResourceEntry.Create(classMapping.NativeType);
			}

			throw Error.Format($"BundleEntry's id '{id}' does not specify the type of resource: cannot determine Resource type in parser.");
		}

		public static bool HasUriValue(Uri uri)
		{
			return !string.IsNullOrEmpty(uri?.ToString());
		}

		public static Uri UriValueOrNull(JToken attr)
		{
			if (attr == null)
				return null;

			var value = attr.Value<string>();

			return string.IsNullOrEmpty(value) ? null : new Uri(value, UriKind.RelativeOrAbsolute);
		}

		public static IPositionInfo GetLineInfo(this XmlReader reader)
		{
			var lineInfo = (IXmlLineInfo)reader;
			if (lineInfo?.HasLineInfo() ?? false)
				return new PostitionInfo(lineInfo.LineNumber, lineInfo.LinePosition);

			return null;
		}
	}
}
