namespace Hl7.Fhir.Applicator
{
	public interface IDataSetterContext : IDataContext
	{
		void SetData(IPathable node, string key, string value);
		IDataSetterContext GetArrayElementSetter(IPathable node, string key);
	}
}
