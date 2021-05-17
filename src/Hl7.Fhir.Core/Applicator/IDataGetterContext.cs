using System.Collections.Generic;

namespace Hl7.Fhir.Applicator
{
	public interface IDataGetterContext : IDataContext
	{
		string GetString(string key);
		bool GetBoolean(string key);
		IList<IDataGetterContext> GetArray(string key);
		bool IsForNode(IPathable node);
	}
}
