/* 
 * Copyright (c) 2014, Furore (info@furore.com) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using Hl7.Fhir.Model;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Text;
using System.Xml;

namespace Hl7.Fhir.Serialization
{
	internal static class FhirSerializer
	{
		private static void Serialize(object instance, IFhirWriter writer, bool summary = false)
		{
			new ResourceWriter(writer).Serialize(instance, summary);
		}

		internal static string SerializeToXml(object instance, bool summary = false)
		{
			return xmlWriterToString(xw => Serialize(instance, new XmlFhirWriter(xw), summary));
		}

		private static string SerializeToJson(object instance, bool summary = false)
		{
			return jsonWriterToString(jw => Serialize(instance, new JsonDomFhirWriter(jw), summary));
		}

		private static byte[] SerializeToXmlBytes(object instance, bool summary = false)
		{
			return xmlWriterToBytes(xw => Serialize(instance, new XmlFhirWriter(xw), summary));
		}

		private static byte[] SerializeToJsonBytes(object instance, bool summary = false)
		{
			return jsonWriterToBytes(jw => Serialize(instance, new JsonDomFhirWriter(jw), summary));
		}

		public static string SerializeResourceToXml(Resource resource, bool summary = false)
		{
			return SerializeToXml(resource, summary);
		}

		public static string SerializeTagListToXml(TagList list)
		{
			return SerializeToXml(list, false);
		}

		public static byte[] SerializeResourceToXmlBytes(Resource resource, bool summary = false)
		{
			return SerializeToXmlBytes(resource, summary);
		}

		public static byte[] SerializeTagListToXmlBytes(TagList list)
		{
			return SerializeToXmlBytes(list, false);
		}

		public static string SerializeResourceToJson(Resource resource, bool summary = false)
		{
			return SerializeToJson(resource, summary);
		}

		public static string SerializeTagListToJson(TagList list)
		{
			return SerializeToJson(list, false);
		}

		public static byte[] SerializeResourceToJsonBytes(Resource resource, bool summary = false)
		{
			return SerializeToJsonBytes(resource, summary);
		}

		public static byte[] SerializeTagListToJsonBytes(TagList list)
		{
			return SerializeToJsonBytes(list, false);
		}

		internal static string SerializeBundleToJson(Bundle bundle, bool summary = false)
		{
			return jsonWriterToString(jw => BundleJsonSerializer.WriteTo(bundle, jw, summary));
		}

		internal static string SerializeBundleToXml(Bundle bundle, bool summary = false)
		{
			return xmlWriterToString(xw => BundleXmlSerializer.WriteTo(bundle, xw, summary));
		}

		internal static byte[] SerializeBundleToJsonBytes(Bundle bundle, bool summary = false)
		{
			return jsonWriterToBytes(jw => BundleJsonSerializer.WriteTo(bundle, jw, summary));
		}

		internal static byte[] SerializeBundleToXmlBytes(Bundle bundle, bool summary = false)
		{
			return xmlWriterToBytes(xw => BundleXmlSerializer.WriteTo(bundle, xw, summary));
		}

		internal static string SerializeBundleEntryToJson(BundleEntry entry, bool summary = false)
		{
			return jsonWriterToString(jw => BundleJsonSerializer.WriteTo(entry, jw, summary));
		}

		internal static string SerializeBundleEntryToXml(BundleEntry entry, bool summary = false)
		{
			return xmlWriterToString(xw => BundleXmlSerializer.WriteTo(entry, xw, summary));
		}

		private static byte[] xmlWriterToBytes(Action<XmlWriter> serializer)
		{
			MemoryStream stream = new MemoryStream();
			XmlWriterSettings settings = new XmlWriterSettings { Encoding = new UTF8Encoding(false), OmitXmlDeclaration = true };
			XmlWriter xw = XmlWriter.Create(stream, settings);

			serializer(xw);

			xw.Flush();

			return stream.ToArray();
		}

		private static byte[] jsonWriterToBytes(Action<JsonWriter> serializer)
		{
			MemoryStream stream = new MemoryStream();

			var sw = new StreamWriter(stream, new UTF8Encoding(false));
			JsonWriter jw = new JsonTextWriter(sw);

			serializer(jw);

			jw.Flush();

			return stream.ToArray();

		}

		private static string jsonWriterToString(Action<JsonWriter> serializer)
		{
			StringBuilder resultBuilder = new StringBuilder();
			StringWriter sw = new StringWriter(resultBuilder);
			JsonWriter jw = new JsonTextWriter(sw);

			serializer(jw);
			jw.Flush();
			jw.Close();

			return resultBuilder.ToString();
		}

		private static string xmlWriterToString(Action<XmlWriter> serializer)
		{
			StringBuilder sb = new StringBuilder();
			XmlWriter xw = XmlWriter.Create(sb, new XmlWriterSettings { OmitXmlDeclaration = true });

			serializer(xw);

			xw.Flush();


			return sb.ToString();
		}
	}
}
