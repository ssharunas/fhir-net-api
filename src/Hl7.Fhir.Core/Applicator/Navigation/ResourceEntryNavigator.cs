using Hl7.Fhir.Model;
using Hl7.Fhir.Serialization;
using Hl7.Fhir.Serialization.Xml;
using System;
using System.Collections.Generic;

namespace Hl7.Fhir.Applicator.Navigation
{
	internal class ResourceEntryNavigator : HardcodedNavigator<ResourceEntry>
	{
		private class Content : NavigatorBase
		{
			private static readonly IList<IFhirXmlAttribute> _attributes = new List<IFhirXmlAttribute> { new FhirXmlAttribute(BundleXmlParser.XATOM_CONTENT_TYPE, "text/xml") };
			private readonly IList<IFhirXmlNode> _elements;

			public Content(IFhirXmlNode parent, Resource resource) : base(parent, BundleXmlParser.XATOM_CONTENT, null)
			{
				_elements = new List<IFhirXmlNode> { new ResourceNavigator(this, resource) };
			}

			public override IList<IFhirXmlAttribute> Attributes() => _attributes;
			public override IList<IFhirXmlNode> Elements() => _elements;

			public override object Unwrap()
			{
				return (Parent as ResourceEntryNavigator)?.Node;
			}
		}

		private static readonly Dictionary<string, Func<Creator, ResourceEntry, IList<IFhirXmlNode>>> _availableElements;

		static ResourceEntryNavigator()
		{
			_availableElements = new Dictionary<string, Func<Creator, ResourceEntry, IList<IFhirXmlNode>>>
			{
				{ BundleXmlParser.XATOM_TITLE, (c, o) => c.CreateNodes(o.Title) },
				{ BundleXmlParser.XATOM_ID, (c, o) => c.CreateNodes(o.Id) },
				{ BundleXmlParser.XATOM_UPDATED, (c, o) => c.CreateNodes(o.LastUpdated) },
				{ BundleXmlParser.XATOM_PUBLISHED, (c, o) => c.CreateNodes(o.Published) },
				{ BundleXmlParser.XATOM_AUTH_NAME, (c, o) => c.CreateNodes(o.AuthorName) },
				{ BundleXmlParser.XATOM_AUTH_URI, (c, o) => c.CreateNodes(o.AuthorUri) },
				{ BundleXmlParser.XATOM_LINK, (c, o) => c.CreateNodes(o.Links) },

				{ BundleXmlParser.XATOM_CONTENT, (c, o) => CreateNodes(c.Wrapper, o.Resource) },
			};
		}

		public ResourceEntryNavigator(IFhirXmlNode parent, ResourceEntry node) : base(parent, node)
		{
			Name = BundleXmlParser.XATOM_ENTRY;
		}

		private static IList<IFhirXmlNode> CreateNodes(IFhirXmlNode parent, Resource resource)
		{
			if (resource != null)
				return new[] { new Content(parent, resource) };

			return null;
		}

		protected override IDictionary<string, Func<Creator, ResourceEntry, IList<IFhirXmlNode>>> AvailableElements => _availableElements;
	}
}
