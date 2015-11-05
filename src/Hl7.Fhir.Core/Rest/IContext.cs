using DevDefined.OAuth.Consumer;

namespace Hl7.Fhir.Rest
{
	public interface IContext
	{
		IConsumerRequest GetRequest();
	}
}
