using Hl7.Fhir.Model;
using Hl7.Fhir.Serialization;
using Hl7.Fhir.Serialization.Xml;
using System;
using System.Collections.Generic;

namespace Hl7.Fhir.Applicator.Navigation
{
	internal class BundleNavigator : HardcodedNavigator<Bundle>
	{
		private static readonly IDictionary<string, Func<Creator, Bundle, IList<IFhirXmlNode>>> _availableElements;

		static BundleNavigator()
		{
			_availableElements = new Dictionary<string, Func<Creator, Bundle, IList<IFhirXmlNode>>>
			{
				{ BundleXmlParser.XATOM_LINK, (c, o) => c.CreateNodes(o.Links) },
				{ BundleXmlParser.XATOM_ID, (c, o) => c.CreateNodes(o.Id) },
				{ BundleXmlParser.XATOM_TITLE, (c, o) => c.CreateNodes(o.Title) },
				{ BundleXmlParser.XATOM_UPDATED, (c, o) => c.CreateNodes(o.LastUpdated) },
				//{ BundleXmlParser.XATOM_AUTHOR, (o) => o },
				{ BundleXmlParser.XATOM_AUTH_URI, (c, o) => c.CreateNodes(o.AuthorUri) },
				{ BundleXmlParser.XATOM_AUTH_NAME, (c, o) => c.CreateNodes(o.AuthorName) },
				{ BundleXmlParser.XATOM_TOTALRESULTS, (c, o) => c.CreateNodes(o.TotalResults) },
				{ BundleXmlParser.XATOM_ENTRY, (c, o) => CreateNodes(c.Wrapper, o.Entries) }
			};
		}

		private static IList<IFhirXmlNode> CreateNodes(IFhirXmlNode parent, IList<BundleEntry> etries)
		{
			IList<IFhirXmlNode> result = null;

			if (etries?.Count > 0)
			{
				result = new List<IFhirXmlNode>(etries.Count);

				foreach (var entry in etries)
				{
					if (entry is ResourceEntry re)
						result.Add(new ResourceEntryNavigator(parent, re));
				}
			}

			return result;
		}

		public BundleNavigator(Bundle bundle)
			: base(null, bundle)
		{
			Name = BundleXmlParser.XATOM_FEED;
		}

		protected override IDictionary<string, Func<Creator, Bundle, IList<IFhirXmlNode>>> AvailableElements => _availableElements;
	}
}
