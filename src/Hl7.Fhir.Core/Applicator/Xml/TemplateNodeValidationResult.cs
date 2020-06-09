using Hl7.Fhir.Support;
using System;
using System.Collections.Generic;
using System.Xml;

namespace Hl7.Fhir.Applicator.Xml
{
	public class TemplateNodeValidationResult : IDataContext
	{
		public class Line
		{
			internal Line(string text, string error, string warning, IPositionInfo position)
			{
				LineText = text;
				Error = error;
				Warning = warning;
				Position = position;
			}

			public IPositionInfo Position { get; internal set; }
			public string LineText { get; internal set; }
			public string Error { get; internal set; }
			public string Warning { get; internal set; }
		}

		public class Data
		{
			internal Data(string key, bool isArray, bool isCanWrite, bool isCanRead)
			{
				Key = key;
				IsArray = isArray;

				if (isCanWrite) WriteCount++;
				if (isCanRead) ReadCount++;
			}

			public bool IsCanWrite => WriteCount > 0;
			public bool IsCanRead => ReadCount > 0;

			public int WriteCount { get; internal set; }
			public int ReadCount { get; internal set; }

			public string Key { get; internal set; }
			public bool IsArray { get; internal set; }
		}

		public TemplateNodeValidationResult(IDataContext context)
		{
			Template = new List<Line>();
			TemplateData = new List<Data>();
			IncludeErrors = new List<string>();
			Context = context;
		}

		private IDataContext Context { get; }
		public IList<Line> Template { get; }
		public IList<Data> TemplateData { get; }
		public IList<string> IncludeErrors { get; }

		public bool HasErros()
		{
			if (Template?.Count > 0)
			{
				foreach (var line in Template)
				{
					if (!string.IsNullOrWhiteSpace(line.Error))
						return true;
				}
			}

			return false;
		}

		internal Line AddLine(IPositionInfo position, string text, string error, string warning)
		{
			Line line = new Line(text, error, warning, position);
			Template.Add(line);

			return line;
		}

		internal TemplateNodeValidationResult AddDataList(string path, IList<string> keys, bool isCanWrite, bool isCanRead)
		{
			if (keys?.Count > 0)
			{
				foreach (var key in keys)
					AddData(path, key, false, isCanWrite, isCanRead);
			}

			return this;
		}

		internal TemplateNodeValidationResult AddData(string path, string key, bool isArray, bool isCanWrite, bool isCanRead)
		{
			if (!string.IsNullOrEmpty(key))
			{
				if (!string.IsNullOrEmpty(path))
					key = path + "." + key;

				bool contains = false;

				foreach (var item in TemplateData)
				{
					if (item.Key == key && item.IsArray == isArray)
					{
						if (isCanWrite)
							item.WriteCount++;

						if (isCanRead)
							item.ReadCount++;

						contains = true;
						break;
					}
				}

				if (!contains)
					TemplateData.Add(new Data(key, isArray, isCanWrite, isCanRead));
			}

			return this;
		}

		internal TemplateNodeValidationResult AddIncludeError(string source)
		{
			if (!string.IsNullOrEmpty(source))
				IncludeErrors.Add(source);

			return this;
		}

		public XmlReader GetInclude(string source)
		{
			try
			{
				if (string.IsNullOrEmpty(source))
					AddIncludeError($"Source attribute 'source=' is missing for x-include!");

				var include = Context.GetInclude(source);

				if (include == null)
					AddIncludeError($"Failed to find include for source '{source}'.");

				return include;
			}
			catch (Exception ex)
			{
				AddIncludeError($"Exception while getting include source '{source}': {ex.Message}");
			}

			return null;
		}
	}
}
