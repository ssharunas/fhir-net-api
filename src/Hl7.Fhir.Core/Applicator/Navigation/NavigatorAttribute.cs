using Hl7.Fhir.Serialization.Xml;
using System;

namespace Hl7.Fhir.Applicator.Navigation
{
	internal class NavigatorAttribute : IFhirXmlAttribute
	{
		public NavigatorAttribute(string name, object value)
		{
			Name = name;
			Value = value;
		}

		public string Name { get; protected set; }

		public object Value { get; }

		public string ValueAsString
		{
			get => NavigatorBase.ToString(Value);
			set => throw new NotSupportedException();
		}

		public Uri ValueAsUri => Value as Uri;
		public int? ValueAsInt => Value as int?;
		public DateTimeOffset? ValueAsInstant => Value as DateTimeOffset?;
	}
}
