using System.Collections.Generic;

namespace Hl7.Fhir.Applicator
{
	public interface IPathable
	{
		IPathable GetNode(string path);
		IList<IPathable> GetNodes(string path);
		object GetValue(string path);
		IList<object> GetValues(string path);
	}
}
