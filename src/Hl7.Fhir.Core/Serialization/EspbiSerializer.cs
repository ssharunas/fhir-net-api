using System.IO;
using System.Text;
using System.Xml.Serialization;

namespace Hl7.Fhir.Serialization
{
	internal static class EspbiSerializer
	{
		private class Utf8StringWriter : StringWriter
		{
			public override Encoding Encoding => Encoding.UTF8;
		}

		public static string Serialize(object instance)
		{
			if (instance != null)
			{
				XmlSerializer serializer = new XmlSerializer(instance.GetType());

				using (var writer = new Utf8StringWriter())
				{
					serializer.Serialize(writer, instance);
					return writer.ToString();
				}
			}

			return null;
		}

		public static T Deserialize<T>(byte[] data)
			where T : class
		{
			XmlSerializer serializer = new XmlSerializer(typeof(T));

			using (var stream = new MemoryStream(data))
			{
				return serializer.Deserialize(stream) as T;
			}
		}

	}
}
