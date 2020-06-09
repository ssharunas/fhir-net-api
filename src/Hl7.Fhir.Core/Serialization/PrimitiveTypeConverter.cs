/* 
 * Copyright (c) 2014, Furore (info@furore.com) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using Hl7.Fhir.Introspection;
using Hl7.Fhir.Support;
using System;
using System.Xml;

namespace Hl7.Fhir.Serialization
{
	public static class PrimitiveTypeConverter
	{
		public static object Convert(object value, Type to)
		{
			if (to == null)
				throw Error.ArgumentNull(nameof(to));

			if (value == null)
				throw Error.ArgumentNull(nameof(value));

			// No conversion necessary...
			if (value.GetType() == to)
				return value;

			// Convert TO string (mostly Xml serialization, some additional schemes used)
			if (to == typeof(string))
			{
				return convertToXmlString(value);
				// Include enum serialization here
			}

			// Convert FROM string
			else if (value is string)
			{
				return convertXmlStringToPrimitive(to, (string)value);
				// Include enum parsing here
			}
			else
				// For non-string primitives use the .NET conversions to convert
				// to the desired type in the class model. Note that the xml/json readers
				// will either produce strings or primitives as values, so no other
				// conversion should be necessary, however this .NET conversion supports
				// conversion from any type implementing IConvertable
				return System.Convert.ChangeType(value, to, null);
		}

		public const string FMT_FULL = "yyyy-MM-dd'T'HH:mm:ssK";
		//private const string FMT_YEAR = "{0:D4}";
		//private const string FMT_YEARMONTH = "{0:D4}-{1:D2}";
		//private const string FMT_YEARMONTHDAY = "{0:D4}-{1:D2}-{2:D2}";

		private static string convertToXmlString(object value)
		{
			if (value is bool castedBool)
				return XmlConvert.ToString(castedBool);
			if (value is byte castedByte)
				return XmlConvert.ToString(castedByte);        // Not used in FHIR serialization
			if (value is char castedChar)
				return XmlConvert.ToString(castedChar);        // Not used in FHIR serialization
			if (value is DateTime castedDateTime)
				return XmlConvert.ToString(castedDateTime, FMT_FULL);    // TODO: validate format, not used in model
			if (value is decimal castedDecimal)
				return XmlConvert.ToString(castedDecimal);
			if (value is double castedDouble)
				return XmlConvert.ToString(castedDouble);
			if (value is short castedShort)
				return XmlConvert.ToString(castedShort);
			if (value is int castedInt)
				return XmlConvert.ToString(castedInt);
			if (value is long castedLong)
				return XmlConvert.ToString(castedLong);       // Not used in FHIR serialization
			if (value is sbyte castedSbyte)
				return XmlConvert.ToString(castedSbyte);       // Not used in FHIR serialization
			if (value is float castedFloat)
				return XmlConvert.ToString(castedFloat);      // Not used in FHIR serialization
			if (value is ushort castedUshort)
				return XmlConvert.ToString(castedUshort);      // Not used in FHIR serialization
			if (value is uint castedUint)
				return XmlConvert.ToString(castedUint);      // Not used in FHIR serialization
			if (value is ulong castedUlong)
				return XmlConvert.ToString(castedUlong);      // Not used in FHIR serialization
			if (value is byte[] castedByteArray)
				return System.Convert.ToBase64String(castedByteArray);
			if (value is DateTimeOffset castedDateTimeOffset)
				return XmlConvert.ToString(castedDateTimeOffset, FMT_FULL);
			if (value is Uri castedUri)
				return castedUri.ToString();
			if (value is Enum castedEnum)
			{
				var attr = castedEnum.GetAttributeOnEnum<EnumLiteralAttribute>();
				if (attr != null)
					return attr.Literal;
			}

			throw Error.NotSupported($"Cannot convert {value.GetType().Name} value '{value}' to string");
		}

		private static object convertXmlStringToPrimitive(Type to, string value)
		{
			if (typeof(DateTimeOffset).IsAssignableFrom(to))
				return XmlConvert.ToDateTimeOffset(value);
			if (typeof(byte[]).IsAssignableFrom(to))
				return System.Convert.FromBase64String(value);
			if (typeof(DateTime).IsAssignableFrom(to))
				return XmlConvert.ToDateTimeOffset(value); // TODO: should handle FHIR's "instant" datatype
			if (typeof(Uri).IsAssignableFrom(to))
				return new Uri(value, UriKind.RelativeOrAbsolute);
			if (typeof(bool).IsAssignableFrom(to))
				return XmlConvert.ToBoolean(value);
			if (typeof(decimal).IsAssignableFrom(to))
				return XmlConvert.ToDecimal(value);
			if (typeof(int).IsAssignableFrom(to))
				return XmlConvert.ToInt32(value);
			if (typeof(double).IsAssignableFrom(to))
				return XmlConvert.ToDouble(value);      // Could lead to loss in precision
			if (typeof(byte).IsAssignableFrom(to))
				return XmlConvert.ToByte(value);        // Not used in FHIR serialization
			if (typeof(char).IsAssignableFrom(to))
				return XmlConvert.ToChar(value);        // Not used in FHIR serialization
			if (typeof(short).IsAssignableFrom(to))
				return XmlConvert.ToInt16(value);       // Could lead to loss in precision
			if (typeof(long).IsAssignableFrom(to))
				return XmlConvert.ToInt64(value);       // Not used in FHIR serialization
			if (typeof(sbyte).IsAssignableFrom(to))
				return XmlConvert.ToSByte(value);       // Not used in FHIR serialization
			if (typeof(float).IsAssignableFrom(to))
				return XmlConvert.ToSingle(value);      // Not used in FHIR serialization
			if (typeof(ushort).IsAssignableFrom(to))
				return XmlConvert.ToUInt16(value);      // Not used in FHIR serialization
			if (typeof(uint).IsAssignableFrom(to))
				return XmlConvert.ToUInt32(value);      // Not used in FHIR serialization
			if (typeof(ulong).IsAssignableFrom(to))
				return XmlConvert.ToUInt64(value);      // Not used in FHIR serialization

			throw Error.NotSupported($"Cannot convert string value '{value}' to a {to.Name}");
		}

		public static T ConvertTo<T>(this object value)
		{
			return (T)Convert(value, typeof(T));
		}

		public static bool CanConvert(Type type)
		{
			if (Type.GetTypeCode(type) != TypeCode.Object)
				return true;

			if (type.Namespace != "System")
				return false;

			// And some specific complex native types
			return type == typeof(byte[]) ||
				type == typeof(DateTimeOffset) ||
				type == typeof(Uri) ||
				type == typeof(string)
			;
		}
	}
}
