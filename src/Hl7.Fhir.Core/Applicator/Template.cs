using Hl7.Fhir.Applicator.Xml;
using Hl7.Fhir.Model;
using Hl7.Fhir.Rest;
using Hl7.Fhir.Serialization;
using Hl7.Fhir.Serialization.Xml;
using Hl7.Fhir.Support;
using System;
using System.Xml;

namespace Hl7.Fhir.Applicator
{
	public abstract class Template<TDto> : ITemplate
	{
		private TemplateNode _template;

		internal TemplateNode TemplateNode
		{
			get
			{
				if (_template == null)
				{
					using (var reader = GetTemplate())
					{
						if (reader == null)
							throw Error.InvalidOperation("GetTemplate() returned null! Template was not found?");

						_template = TemplateNode.Create(reader);
					}
				}

				return _template;
			}
		}

		public abstract Uri GetLocation(ulong id);

		public virtual TemplateResponseReader GetReaderForCreate(string response, IDataGetterContext context)
		{
			return new TemplateResponseReader(response, context);
		}

		public virtual Bundle Create(IDataGetterContext context)
		{
			return BundleXmlParser.Load(TemplateNode.CreateData(context));
		}

		public virtual UpdateData<TDto> Update(FhirResponse response, Func<TDto, TDto> modify)
		{
			if (modify == null)
				throw Error.ArgumentNull(nameof(modify));

			var data = response?.GetBodyAsString();

			if (!string.IsNullOrEmpty(data))
			{
				var xml = FhirXmlNode.Create(FhirParser.XmlReaderFromString(data));
				var context = GetSetter(true);

				if (context == null)
					throw Error.InvalidOperation("GetSetter() returned null!");

				TemplateNode.ReadData(xml, context);
				var dto = ToDto(context);

				if (dto != null)
				{
					dto = modify(dto);

					if (dto != null)
					{
						var getterContext = GetGetter(dto, context);

						TemplateNode.UpdateData(xml, getterContext);
						return new UpdateData<TDto>(xml, getterContext, dto);
					}
				}
			}

			return null;
		}

		public virtual TDto Read(FhirResponse response)
		{
			var data = response?.GetBodyAsString();

			if (!string.IsNullOrEmpty(data))
			{
				var xml = FhirXmlNode.Create(FhirParser.XmlReaderFromString(data));
				var context = GetSetter(false);

				if (context == null)
					throw Error.InvalidOperation("GetSetter() returned null!");

				TemplateNode.ReadData(xml, context);
				return ToDto(context);
			}

			return default;
		}

		public virtual TemplateNodeValidationResult Validate(IDataContext includes)
		{
			TemplateNodeValidationResult context = new TemplateNodeValidationResult(includes ?? throw Error.ArgumentNull(nameof(includes)));
			TemplateNode.Validate(context);
			return context;
		}

		public abstract IDataGetterContext GetGetter(TDto dto, IDataSetterContext context);
		protected abstract IDataSetterContext GetSetter(bool isForUpdate);
		protected abstract TDto ToDto(IDataSetterContext context);
		protected abstract XmlReader GetTemplate();
	}
}
