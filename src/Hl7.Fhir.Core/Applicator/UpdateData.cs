using Hl7.Fhir.Model;
using Hl7.Fhir.Serialization;
using Hl7.Fhir.Serialization.Xml;

namespace Hl7.Fhir.Applicator
{
	public class UpdateData
	{
		internal UpdateData(IFhirXmlNode xml, IDataGetterContext context)
		{
			Context = context;
			Bundle = BundleXmlParser.Load(xml);
		}

		public IDataGetterContext Context { get; private set; }
		public Bundle Bundle { get; set; }
	}

	public class UpdateData<TDto> : UpdateData
	{
		internal UpdateData(IFhirXmlNode xml, IDataGetterContext context, TDto dto)
			: base(xml, context)
		{
			Dto = dto;
		}

		public TDto Dto { get; set; }
	}
}
