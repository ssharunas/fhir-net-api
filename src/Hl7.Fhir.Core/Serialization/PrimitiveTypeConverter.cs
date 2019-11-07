﻿/* 
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
	internal static class PrimitiveTypeConverter
	{
		public static object ConvertTo(this object value, Type to)
		{
			return Convert(value, to);
		}

		public static object Convert(object value, Type to)
		{
			if (to == null) throw Error.ArgumentNull(nameof(to));
			if (value == null) throw Error.ArgumentNull(nameof(value));

			// No conversion necessary...
			if (value.GetType() == to) return value;

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
		private const string FMT_YEAR = "{0:D4}";
		private const string FMT_YEARMONTH = "{0:D4}-{1:D2}";
		private const string FMT_YEARMONTHDAY = "{0:D4}-{1:D2}-{2:D2}";

		private static string convertToXmlString(object value)
		{
			if (value is Boolean)
				return XmlConvert.ToString((bool)value);
			if (value is Byte)
				return XmlConvert.ToString((byte)value);        // Not used in FHIR serialization
			if (value is Char)
				return XmlConvert.ToString((char)value);        // Not used in FHIR serialization
			if (value is DateTime)
				return XmlConvert.ToString((DateTime)value, FMT_FULL);    // TODO: validate format, not used in model
			if (value is Decimal)
				return XmlConvert.ToString((decimal)value);
			if (value is Double)
				return XmlConvert.ToString((double)value);
			if (value is Int16)
				return XmlConvert.ToString((short)value);
			if (value is Int32)
				return XmlConvert.ToString((int)value);
			if (value is Int64)
				return XmlConvert.ToString((long)value);       // Not used in FHIR serialization
			if (value is SByte)
				return XmlConvert.ToString((sbyte)value);       // Not used in FHIR serialization
			if (value is Single)
				return XmlConvert.ToString((float)value);      // Not used in FHIR serialization
			if (value is UInt16)
				return XmlConvert.ToString((ushort)value);      // Not used in FHIR serialization
			if (value is UInt32)
				return XmlConvert.ToString((uint)value);      // Not used in FHIR serialization
			if (value is UInt64)
				return XmlConvert.ToString((ulong)value);      // Not used in FHIR serialization
			if (value is byte[])
				return System.Convert.ToBase64String((byte[])value);
			if (value is DateTimeOffset)
				return XmlConvert.ToString((DateTimeOffset)value, FMT_FULL);
			if (value is Uri)
				return ((Uri)value).ToString();
			if (value is Enum)
			{
				var attr = ((Enum)value).GetAttributeOnEnum<EnumLiteralAttribute>();
				if (attr != null) return attr.Literal;
			}

			throw Error.NotSupported($"Cannot convert {value.GetType().Name} value '{value}' to string");
		}

		private static object convertXmlStringToPrimitive(Type to, string value)
		{
			if (typeof(Boolean).IsAssignableFrom(to))
				return XmlConvert.ToBoolean(value);
			if (typeof(Byte).IsAssignableFrom(to))
				return XmlConvert.ToByte(value);        // Not used in FHIR serialization
			if (typeof(Char).IsAssignableFrom(to))
				return XmlConvert.ToChar(value);        // Not used in FHIR serialization
			if (typeof(DateTime).IsAssignableFrom(to))
				return XmlConvert.ToDateTimeOffset(value); // TODO: should handle FHIR's "instant" datatype
			if (typeof(Decimal).IsAssignableFrom(to))
				return XmlConvert.ToDecimal(value);
			if (typeof(Double).IsAssignableFrom(to))
				return XmlConvert.ToDouble(value);      // Could lead to loss in precision
			if (typeof(Int16).IsAssignableFrom(to))
				return XmlConvert.ToInt16(value);       // Could lead to loss in precision
			if (typeof(Int32).IsAssignableFrom(to))
				return XmlConvert.ToInt32(value);
			if (typeof(Int64).IsAssignableFrom(to))
				return XmlConvert.ToInt64(value);       // Not used in FHIR serialization
			if (typeof(SByte).IsAssignableFrom(to))
				return XmlConvert.ToSByte(value);       // Not used in FHIR serialization
			if (typeof(Single).IsAssignableFrom(to))
				return XmlConvert.ToSingle(value);      // Not used in FHIR serialization
			if (typeof(UInt16).IsAssignableFrom(to))
				return XmlConvert.ToUInt16(value);      // Not used in FHIR serialization
			if (typeof(UInt32).IsAssignableFrom(to))
				return XmlConvert.ToUInt32(value);      // Not used in FHIR serialization
			if (typeof(UInt64).IsAssignableFrom(to))
				return XmlConvert.ToUInt64(value);      // Not used in FHIR serialization
			if (typeof(byte[]).IsAssignableFrom(to))
				return System.Convert.FromBase64String(value);
			if (typeof(DateTimeOffset).IsAssignableFrom(to))
				return XmlConvert.ToDateTimeOffset(value);
			if (typeof(System.Uri).IsAssignableFrom(to))
				return new Uri(value, UriKind.RelativeOrAbsolute);

			throw Error.NotSupported($"Cannot convert string value '{value}' to a {to.Name}");
		}

		public static T ConvertTo<T>(this object value)
		{
			return (T)Convert(value, typeof(T));
		}

		public static bool CanConvert(Type type)
		{
			if (Type.GetTypeCode(type) != TypeCode.Object) return true;

			// And some specific complex native types
			if (type == typeof(byte[]) ||
				 type == typeof(string) ||
				 type == typeof(DateTimeOffset) ||
				 type == typeof(Uri))
				return true;

			return false;
		}
	}
}
