using System;

namespace Hl7.Fhir.Serialization.Xml
{
	internal abstract class FhirXmlValue : IFhirXmlValue
	{
		public string Name { get; protected set; }

		public abstract string ValueAsString { get; set; }

		public virtual object Value => ValueAsString;

		public DateTimeOffset? ValueAsInstant
		{
			get
			{
				if (ValueAsString != null)
					return PrimitiveTypeConverter.ConvertTo<DateTimeOffset>(ValueAsString);

				return null;
			}
		}

		public int? ValueAsInt
		{
			get
			{
				if (ValueAsString != null)
					return int.Parse(ValueAsString);

				return null;
			}
		}

		public Uri ValueAsUri
		{
			get
			{
				if (ValueAsString != null)
					return new Uri(ValueAsString, UriKind.RelativeOrAbsolute);

				return null;
			}
		}

	}
}
