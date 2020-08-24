using Hl7.Fhir.Applicator.Xml;
using Hl7.Fhir.Model;
using Hl7.Fhir.Rest;
using Hl7.Fhir.Serialization;
using Hl7.Fhir.Serialization.Xml;
using Hl7.Fhir.Support;
using System;
using System.Collections.Generic;
using System.Xml;

namespace Hl7.Fhir.Applicator
{
	internal interface ISearchableTemplate<TDto>
	{
		IList<TDto> ReadAtomSearch(FhirResponse response, out int totalPages, out int totalResults);
	}

	public abstract class SearchableTemplate<TDto, TCriteria> : Template<TDto>, ISearchableTemplate<TDto>
	{
		private class SearchAtomDataSetterElement : IDataSetterContext
		{
			private SearchableTemplate<TDto, TCriteria> _template;

			public SearchAtomDataSetterElement(SearchableTemplate<TDto, TCriteria> template)
			{
				_template = template;
			}

			public DateTime LastModified { get; private set; }
			public Uri Id { get; private set; }
			public IDataSetterContext Content { get; private set; }

			public IDataSetterContext GetArrayElementSetter(IPathable node, string key)
			{
				if (key == TemplateNode.ATOM_ENTRY)
				{
					if (Content != null)
						throw Error.InvalidOperation("Setting multiple contents! Expected only one!");

					Content = _template.GetSetter(false);
					return Content;
				}

				throw Error.InvalidOperation($"Invalid search operation: tried to set an unknown list '{key}' in a result list");
			}

			public XmlReader GetInclude(string source)
			{
				throw new NotSupportedException();
			}

			public void SetData(IPathable node, string key, string value)
			{
				switch (key)
				{
					case TemplateNode.ATOM_ID:
						Id = new Uri(value, UriKind.Relative);
						break;

					case TemplateNode.ATOM_MODIFIED_AT:
						LastModified = DateTime.Parse(value);
						break;

					default:
						throw Error.InvalidOperation($"Invalid search operation: tried to set an attribute '{key}' to the resulting DTO");
				}
			}
		}

		private class SearchAtomDataSetter : IDataSetterContext
		{
			private SearchableTemplate<TDto, TCriteria> _template;

			public SearchAtomDataSetter(SearchableTemplate<TDto, TCriteria> template)
			{
				_template = template;
			}

			public List<SearchAtomDataSetterElement> Result { get; } = new List<SearchAtomDataSetterElement>();
			public int TotalResults { get; private set; }
			public int TotalPages { get; private set; }

			public IDataSetterContext GetArrayElementSetter(IPathable node, string key)
			{
				if (key == TemplateNode.ATOM_ENTRY)
				{
					var result = new SearchAtomDataSetterElement(_template);
					Result.Add(result);
					return result;
				}

				throw Error.InvalidOperation($"Invalid search operation: tried to set an unknown list '{key}' in a result list.");
			}

			public XmlReader GetInclude(string source)
			{
				throw new NotSupportedException();
			}

			public void SetData(IPathable node, string key, string value)
			{
				if (key == TemplateNode.ATOM_TOTAL_RESULTS)
					TotalResults = int.Parse(value);
				else if (key == TemplateNode.ATOM_TOTAL_PAGES)
					TotalPages = int.Parse(value);
				else if (key == ".")
					; //Ignored
				else
					throw Error.InvalidOperation("Invalid search operation: tried to set an attribute to the returned list");
			}
		}

		private TemplateNode _searchTemplateNode;

		internal TemplateNode SearchTemplateNode
		{
			get
			{
				if (_searchTemplateNode is null)
					_searchTemplateNode = TemplateNode.CreateForAtomSearch(TemplateNode);

				return _searchTemplateNode;
			}
		}

		public abstract Query GetSearcQuery(TCriteria criteria);

		public override TDto Read(FhirResponse response)
		{
			var dto = base.Read(response);

			if (dto != null)
			{
				Uri id = null;
				DateTime lastModified = DateTime.MinValue;
				var location = response.Location ?? response.ContentLocation ?? response.ResponseUri.OriginalString;

				if (!string.IsNullOrEmpty(location))
				{
					id = new ResourceIdentity(location);
				}

				if (!string.IsNullOrEmpty(response.LastModified))
					lastModified = DateTime.Parse(response.LastModified);

				SetDtoData(ref dto, id, lastModified);
			}

			return dto;
		}

		public virtual IList<TDto> ReadAtomSearch(FhirResponse response, out int totalPages, out int totalResults)
		{
			var data = response?.GetBodyAsString();

			if (!string.IsNullOrEmpty(data))
			{
				var xml = FhirXmlNode.Create(FhirParser.XmlReaderFromString(data));
				var context = GetSetterForAtomSearch();

				if (context is null)
					throw Error.InvalidOperation("GetSetterForAtomSearch() returned null!");

				SearchTemplateNode.ReadData(xml, context);
				return ToDtoList(context, xml, out totalPages, out totalResults);
			}

			totalResults = 0;
			totalPages = 0;
			return default;
		}

		protected virtual IDataSetterContext GetSetterForAtomSearch()
		{
			return new SearchAtomDataSetter(this);
		}

		protected virtual IList<TDto> ToDtoList(IDataSetterContext context, IPathable xml, out int totalPages, out int totalResults)
		{
			IList<TDto> result = null;

			if (context is SearchAtomDataSetter setter)
			{
				totalPages = setter.TotalPages;
				totalResults = setter.TotalResults;

				if (setter.Result?.Count > 0)
				{
					result = new List<TDto>(setter.Result.Count);

					foreach (var item in setter.Result)
					{
						TDto dto = ToDto(item.Content);
						SetDtoData(ref dto, item.Id, item.LastModified);
						result.Add(dto);
					}
				}
			}
			else
			{
				throw Error.NotSupported($"Expected setter to be '{nameof(SearchAtomDataSetter)}' but it was '{context?.GetType()}'.");
			}

			return result;
		}

		protected abstract void SetDtoData(ref TDto dto, Uri id, DateTime lastModified);
	}
}
