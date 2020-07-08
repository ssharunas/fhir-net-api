using System.Collections.Generic;

namespace Hl7.Fhir.Applicator.Navigation
{
	internal class NullNavigator : IPathable
	{
		public IPathable GetNode(string path)
		{
			return null;
		}

		public IList<IPathable> GetNodes(string path)
		{
			return null;
		}

		public object GetValue(string path)
		{
			return null;
		}

		public IList<object> GetValues(string path)
		{
			return null;
		}

		public override string ToString()
		{
			return typeof(IPathable) + " <null>";
		}

		public string ToXml()
		{
			return null;
		}
	}
}
