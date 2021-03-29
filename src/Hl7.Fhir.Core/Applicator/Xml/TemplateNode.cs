using Hl7.Fhir.Applicator.XPath.Navigation;
using Hl7.Fhir.Serialization;
using Hl7.Fhir.Serialization.Xml;
using Hl7.Fhir.Support;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Xml;

namespace Hl7.Fhir.Applicator.Xml
{
	internal partial class TemplateNode : IPositional
	{
		private const string X_IF = "x-if";
		private const string X_ID = "x-id";
		private const string X_READ = "x-read";
		private const string X_WRITE = "x-write";
		private const string X_FOREACH = "x-foreach";

		private const string X_INCLUDE = "x-include";
		private const string X_INCLUDE_SOURCE = "source";
		private const string X_INCLUDE_ROOT = "root";
		private const string X_INCLUDE_SCOPE = "scope";

		private const string REGEX_KEY = "{([^}]+)}";

		public const string ATOM_MODIFIED_AT = "ModifiedAt";
		public const string ATOM_ID = "ID";
		public const string ATOM_ENTRY = "Entry";
		public const string ATOM_TOTAL_RESULTS = "TotalResults";
		public const string ATOM_TOTAL_PAGES = "TotalPages";

		private bool? _hasAnyData = null;
		private bool? _hasDataInAttributes = null;
		private bool? _hasDataInText = null;
		private bool? _hasDataInChildren = null;
		private bool _isIncludesProcessed;
		private string _if;
		private string _foreach;
		private readonly List<Attribute> _attributes;
		private List<TemplateNode> _children;
		private TemplateNode _include;

		private TemplateNode(string name, string @namespace, string text, IPositionInfo pos, List<TemplateNode> children, List<FhirXmlReader.Attr> attributes)
		{
			IsRead = true;
			IsWrite = true;
			IsWriteAlways = false;
			Name = name;
			Namespace = @namespace;
			Text = text;
			Position = pos;
			_children = children;

			if (attributes?.Count > 0)
			{
				foreach (var attribute in attributes)
				{
					switch (attribute.Key)
					{
						case X_ID: Id = XPath.XPath.Parse(attribute.Value, pos); break;
						case X_IF: _if = attribute.Value; break;
						case X_FOREACH: _foreach = attribute.Value; break;
						case X_READ: IsRead = attribute.Value?.ToLower() != "false"; break;
						case X_WRITE:
							IsWrite = attribute.Value?.ToLower() != "false";
							IsWriteAlways = attribute.Value?.ToLower() == "always";
							break;
						default:
							if (_attributes is null)
								_attributes = new List<Attribute>();
							_attributes.Add(new Attribute(attribute));
							break;
					}
				}
			}

			if (Id is null)
				Id = TemplateNodeAutoIdGenerator.Generate(Name, attributes);
		}

		/// <summary>
		/// x-id property
		/// </summary>
		private IXPathNavigator Id { get; set; }

		/// <summary>
		/// x-read property.
		/// If true - node can be read by SetData into IDataSetterContext.
		/// Otherwise node is skipped for reading.
		/// </summary>
		private bool IsRead { get; }

		/// <summary>
		/// x-write property.
		/// If true - node will be used in create operations.
		/// Otherwise node is skipped for writing.
		/// </summary>
		private bool IsWrite { get; }

		/// <summary>
		/// x-write property.
		/// If true - node will be used in create and update operations.
		/// </summary>
		private bool IsWriteAlways { get; }

		/// <summary>
		/// x-if property value
		/// </summary>
		private string If => string.IsNullOrEmpty(_if) ? null : Scope + _if;

		/// <summary>
		/// x-foreach property value
		/// </summary>
		private string Foreach => string.IsNullOrEmpty(_foreach) ? null : Scope + _foreach;

		private string Text { get; }
		public string Name { get; private set; }
		public string Namespace { get; }
		public IPositionInfo Position { get; }

		private string Scope { get; set; }

		private bool HasAnyData
		{
			get
			{
				if (_hasAnyData is null)
					_hasAnyData = HasDataInText || HasDataInAttributes || HasDataInChildren || Name == X_INCLUDE;

				return _hasAnyData.Value;
			}
		}

		private bool HasDataInText
		{
			get => _hasDataInText ?? (_hasDataInText = HasDataInString(Text)).Value;
		}

		private bool HasDataInAttributes
		{
			get
			{
				if (_hasDataInAttributes is null)
				{
					_hasDataInAttributes = false;

					if (_attributes?.Count > 0)
					{
						foreach (var attribute in _attributes)
						{
							if (attribute.HasData)
							{
								_hasDataInAttributes = true;
								break;
							}
						}
					}
				}

				return _hasDataInAttributes.Value;
			}
		}

		private bool IsArray => !string.IsNullOrEmpty(Foreach);

		private bool HasDataInChildren
		{
			get
			{
				if (_hasDataInChildren is null)
				{
					_hasDataInChildren = false;

					if (_children?.Count > 0)
					{
						foreach (var child in _children)
						{
							if (child.HasAnyData)
							{
								_hasDataInChildren = true;
								break;
							}
						}
					}
				}

				return _hasDataInChildren.Value;
			}
		}

		private void SetScope(string scope)
		{
			Scope = scope;

			if (_children?.Count > 0 && string.IsNullOrEmpty(Foreach))
			{
				foreach (var child in _children)
					child.SetScope(scope);
			}
		}

		private void Validate(TemplateNode parent, TemplateNodeValidationResult context, string path, string indent, bool isWrite, bool isRead)
		{
			var line = context.AddLine(Position, indent + "<" + Name + " ",
				string.Join(". ", GetErrors(parent) ?? new string[0]),
				string.Join(". ", GetWarnings(parent) ?? new string[0])
			);

			isRead = isRead & IsRead;
			isWrite = isWrite && IsWrite;

			if (Name == X_INCLUDE)
				line.LineText = "<!-- " + line.LineText;

			if (Id != null)
				line.LineText += $"{X_ID}=\"{Id}\" ";

			if (!IsRead)
				line.LineText += $"{X_READ}=\"false\" ";

			if (!IsWrite)
				line.LineText += $"{X_WRITE}=\"false\" ";

			if (IsWriteAlways)
				line.LineText += $"{X_WRITE}=\"always\" ";

			if (!string.IsNullOrEmpty(Foreach))
			{
				line.LineText += $"{X_FOREACH}=\"{_foreach}\" ";

				if (Name != X_INCLUDE)
					context.AddData(path, Foreach, true, isWrite, isRead);

				if (!string.IsNullOrEmpty(path))
					path += ".";

				path += Foreach;
			}

			if (!string.IsNullOrEmpty(If))
			{
				line.LineText += $"{X_IF}=\"{_if}\" ";

				if (Name != X_INCLUDE)
					context.AddData(path, If, false, true, false);
			}

			if (_attributes?.Count > 0)
			{
				foreach (var attribute in _attributes)
				{
					line.LineText += $"{attribute.Key}=\"{attribute.Value}\" ";
					context.AddDataList(path, GetDataKeys(attribute.Value), isWrite, isRead);
				}
			}

			if (HasDataInText)
				context.AddDataList(path, GetDataKeys(Text), isWrite, isRead);

			if (_children?.Count > 0 && Name != X_INCLUDE)
			{
				line.LineText += ">";
				foreach (var child in _children)
					child.Validate(this, context, path, indent + "\t", isWrite, isRead);
				context.AddLine(null, indent + "</" + Name + ">", null, null);
			}
			else if (!string.IsNullOrEmpty(Text))
			{
				line.LineText += ">" + Text + "</" + Name + ">";
			}
			else
			{
				line.LineText += "/>";
			}

			if (Name == X_INCLUDE)
			{
				line.LineText += " -->";

				if (context.GetInclude(GetAttributeValue(X_INCLUDE_SOURCE)) != null)
				{
					var include = GetInclude(context);

					if (string.IsNullOrEmpty(include.Namespace))
						line.Error += " Included node must contain a namespace!";

					include.Validate(this, context, path, indent, isWrite, isRead);
				}
			}
		}

		private IList<string> GetErrors(TemplateNode parent)
		{
			var errors = new List<string>();

			if (parent?._children?.Count > 0 && HasAnyData && Name != X_INCLUDE && IsRead)
			{
				foreach (var child in parent._children)
				{
					if (child == this || !child.IsRead)
						continue;

					if (child.Name == Name)
					{
						if (Id is null)
							errors.Add($"Element is missing 'x-id'! Another element is found with same name '{Name}' at {child.Position}");
						else if (child.Id?.ToString() == Id.ToString())
							errors.Add($"Element's 'x-id' is not unique! Another element is found with same name '{Name}' and same x-id='{Id}' at {child.Position}");
					}
				}
			}

			if (parent is null && Name == X_INCLUDE && string.IsNullOrEmpty(Namespace))
				errors.Add("Missing a namespace! Root element must contain a namespace (xmlns='xxx')");

			return errors;
		}

		private IList<string> GetWarnings(TemplateNode parent)
		{
			if (Name == X_INCLUDE)
				return null;

			var warnings = new List<string>();

			if ((_children is null || _children.Count == 0) && !HasAnyData && Name != "system")
				warnings.Add("Unmapped node.");

			return warnings;
		}

		private IList<IFhirXmlNode> FilterElements(IList<IFhirXmlNode> all)
		{
			List<IFhirXmlNode> result = null;

			if (all?.Count > 0)
			{
				try
				{
					foreach (var element in all)
					{
						if (element.Name == Name)
						{
							if (Id?.IsMatch(element) ?? true)
							{
								if (result is null)
									result = new List<IFhirXmlNode>();

								result.Add(element);
							}
						}
					}
				}
				catch (Exception ex)
				{
					throw Error.InvalidOperation($"Failed to filter template elements as {Position}. Error: " + ex.Message, ex);
				}
			}

			return result;
		}

		private static bool HasDataInString(string str)
		{
			if (string.IsNullOrEmpty(str) || str.IndexOf('{') == -1)
				return false;

			return Regex.IsMatch(str, REGEX_KEY);
		}

		private string ExtractData(string str, IDataGetterContext context)
		{
			if (string.IsNullOrEmpty(str) || str.IndexOf('{') == -1)
				return str;

			return Regex.Replace(str, REGEX_KEY, (Match match) =>
			{
				return context.GetString(Scope + match.Groups[1].Value);
			}, RegexOptions.Multiline | RegexOptions.Compiled);
		}

		private IList<string> GetDataKeys(string str)
		{
			List<string> result = null;

			if (!string.IsNullOrEmpty(str) && str.IndexOf('{') != -1)
			{
				var matches = Regex.Matches(str, REGEX_KEY);
				if (matches?.Count > 0)
				{
					result = new List<string>(matches.Count);

					foreach (Match match in matches)
					{
						result.Add(Scope + match.Groups[1].Value);
					}
				}
			}

			return result;
		}

		private void ReadContextValue(IDataSetterContext context, IPathable node, string value, string pattern)
		{
			if (!string.IsNullOrEmpty(pattern) && !string.IsNullOrEmpty(value) && pattern.Length > 2)
			{
				//Support for: Encounter/{ID}/history/{VersionID}
				for (int p = 0, v = 0; p < pattern.Length && v < value.Length; p++, v++)
				{
					if (pattern[p] == '{')
					{
						int start = p;
						while (++p < pattern.Length && pattern[p] != '}') ;

						string key = Scope + pattern.Substring(start + 1, p - start - 1);

						var nextP = pattern.IndexOf('{', p);
						if (nextP == -1)
							nextP = pattern.Length;

						string itemValue;

						if (nextP - p > 1)
						{
							string endPattern = pattern.Substring(p + 1, nextP - p - 1);
							int end = value.IndexOf(endPattern, v);

							if (end == -1) //Values does not match the pattern
								throw Error.Format($"Value '{value}' does not mathc the pattern '{pattern}'", Position);

							itemValue = value.Substring(v, end - v);
							v = end - 1 + endPattern.Length;
							p += endPattern.Length;
						}
						else
						{
							itemValue = value.Substring(v);
							v = value.Length;
						}

						context.SetData(node, key, itemValue);
					}
					else if (value[v] != pattern[p])
					{
						throw Error.InvalidOperation($"Failed to parse value '{value}' to pattern '{pattern}'");
					}
				}
			}
		}

		private string GetAttributeValue(string key)
		{
			if (_attributes?.Count > 0)
			{
				foreach (var attr in _attributes)
					if (attr.Key == key)
						return string.IsNullOrEmpty(attr.Value) ? null : attr.Value;
			}

			return null;
		}

		private TemplateNode GetInclude(IDataContext context)
		{
			if (context is null)
				throw Error.ArgumentNull(nameof(context));

			if (Name != X_INCLUDE)
				throw Error.InvalidOperation($"Failed to include non-include node. Node has to be a '{X_INCLUDE}', but was '{Name}'.");

			if (_include is null)
			{
				string source = GetAttributeValue(X_INCLUDE_SOURCE) ?? throw Error.Format("There is no source for template include!", Position);
				using (var reader = context.GetInclude(source))
				{
					_include = Create(reader, Position.LineNumber - 1);

					string root = GetAttributeValue(X_INCLUDE_ROOT);
					string scope = Scope + GetAttributeValue(X_INCLUDE_SCOPE);

					if (!string.IsNullOrEmpty(root))
						_include.Name = root;

					if (Id != null)
						_include.Id = Id;

					if (If != null)
						_include._if = _if;

					if (Foreach != null)
						_include._foreach = _foreach;

					if (_children?.Count > 0)
					{
						if (_include._children is null || _include._children.Count == 0)
						{
							_include._children = new List<TemplateNode>(_children);
						}
						else
						{
							foreach (var child in _children)
							{
								for (var i = _include._children.Count - 1; i >= 0; i--)
								{
									if (_include._children[i].Name == child.Name &&
										_include._children[i].Id?.ToString() == child.Id?.ToString())
									{
										_include._children.RemoveAt(i);
									}
								}
							}
							_include._children.AddRange(_children);
						}
					}

					if (!string.IsNullOrEmpty(scope))
						_include.SetScope(scope);
				}
			}

			return _include;
		}

		private bool IsIfOk(IDataGetterContext context)
		{
			return string.IsNullOrEmpty(If) || context.GetBoolean(If);
		}

		private List<TemplateNode> GetTemplateChildren(IDataContext context)
		{
			if (_children?.Count > 0 && !_isIncludesProcessed)
			{
				lock (this)
				{
					for (var i = 0; i < _children.Count; i++)
					{
						if (_children[i].Name == X_INCLUDE)
							_children[i] = _children[i].GetInclude(context);
					}

					_isIncludesProcessed = true;
				}
			}

			return _children;
		}

		private void UpdateList(IFhirXmlNode root, IList<IFhirXmlNode> existingNodes, TemplateNode child, IList<IDataGetterContext> allContexts)
		{
			if (allContexts is null || allContexts.Count == 0)
			{
				if (existingNodes?.Count > 0)
				{
					foreach (var node in existingNodes)
						root.DeleteElement(node);
				}
			}
			else
			{
				var contexts = new List<IDataGetterContext>(allContexts.Count);

				//filter non-ok contexts, so that we wouldn't need to iterate over them while
				//matching nodes. (Performance optimization)
				foreach (var context in allContexts)
				{
					if (child.IsIfOk(context))
						contexts.Add(context);
				}

				if (existingNodes?.Count > 0)
				{
					foreach (var node in existingNodes)
					{
						IDataGetterContext matchingContext = null;

						for (var k = 0; k < contexts.Count; k++)
						{
							if (contexts[k].IsForNode(node))
							{
								matchingContext = contexts[k];
								contexts.RemoveAt(k);
								break;
							}
						}

						if (matchingContext != null /*&& child.IsIfOk(matchingContext)*/) //we dont need to check "ok" for filtered contexts
							child.UpdateData(node, matchingContext);
						else
							root.DeleteElement(node);
					}
				}

				if (contexts.Count > 0)
				{
					foreach (var context in contexts)
					{
						//if (child.IsIfOk(context)) ///we dont need to check "ok" for filtered contexts
						child.CreateElement(root, context);
					}
				}
			}
		}

		private void CreateElement(IFhirXmlNode root, IDataGetterContext context)
		{
			IFhirXmlNode element = root.AddElement(Name, Namespace, ExtractData(Text, context));

			if (_attributes?.Count > 0)
			{
				foreach (var attribute in _attributes)
				{
					var value = ExtractData(attribute.Value, context);

					if (!string.IsNullOrEmpty(value))
						element.AddAttribute(attribute.Key, value);
				}
			}

			CreateChildrenByTemplate(element, context);
		}

		private void CreateChildrenByTemplate(IFhirXmlNode root, IDataGetterContext context)
		{
			var children = GetTemplateChildren(context);
			if (children?.Count > 0)
			{
				foreach (var child in children)
				{
					if (!child.IsWrite)
						continue;

					if (child.IsArray)
					{
						var childContexts = context.GetArray(child.Foreach);
						if (childContexts?.Count > 0)
						{
							foreach (var item in childContexts)
							{
								if (child.IsIfOk(item))
									child.CreateElement(root, item);
							}
						}
					}
					else if (child.IsIfOk(context))
					{
						child.CreateElement(root, context);
					}
				}
			}
		}

		private static TemplateNode Create(string name, string @namespace, string text, IPositionInfo pos, List<TemplateNode> children, List<FhirXmlReader.Attr> attributes)
		{
			return new TemplateNode(name, @namespace, text, pos, children, attributes);
		}

		public static TemplateNode Create(XmlReader reader, int lineOffset = 0)
		{
			if (lineOffset > 0)
			{
				return FhirXmlReader.Read<TemplateNode>(reader, (name, @namespace, text, pos, children, attributes) =>
				{
					if (pos != null)
						pos = new PostitionInfo(pos.LineNumber + lineOffset, pos.LinePosition);

					return Create(name, @namespace, text, pos, children, attributes);
				}, false);
			}

			return FhirXmlReader.Read<TemplateNode>(reader, Create, false);
		}

		public static TemplateNode CreateForAtomSearch(TemplateNode innerContent)
		{
			var content = new TemplateNode(BundleXmlParser.XATOM_CONTENT, null, null, null, new List<TemplateNode> { innerContent }, new List<FhirXmlReader.Attr> { new FhirXmlReader.Attr(X_FOREACH, ATOM_ENTRY) });
			var updated = new TemplateNode(BundleXmlParser.XATOM_UPDATED, null, $"{{{ATOM_MODIFIED_AT}}}", null, null, null);
			var link = new TemplateNode(BundleXmlParser.XATOM_LINK, null, null, null, null, new List<FhirXmlReader.Attr> { new FhirXmlReader.Attr(BundleXmlParser.XATOM_LINK_HREF, $"{{{ATOM_ID}}}") });
			var entry = new TemplateNode(BundleXmlParser.XATOM_ENTRY, null, null, null, new List<TemplateNode> { link, updated, content }, new List<FhirXmlReader.Attr> { new FhirXmlReader.Attr(X_FOREACH, ATOM_ENTRY) });
			var totalResults = new TemplateNode(BundleXmlParser.XATOM_TOTALRESULTS, null, $"{{{ATOM_TOTAL_RESULTS}}}", null, null, null);
			var lastPageLink = new TemplateNode(BundleXmlParser.XATOM_LINK, null, null, null, null, new List<FhirXmlReader.Attr> {
				new FhirXmlReader.Attr(BundleXmlParser.XATOM_LINK_HREF, $"{{.}}page={{{ATOM_TOTAL_PAGES}}}"),
				new FhirXmlReader.Attr(X_ID, "@rel = 'last'")
			});

			return new TemplateNode(BundleXmlParser.XATOM_FEED, XmlNs.ATOM, null, null, new List<TemplateNode> { lastPageLink, totalResults, entry }, null);
		}

		public void Validate(TemplateNodeValidationResult context)
		{
			if (context is null)
				throw Error.ArgumentNull(nameof(context));

			Validate(null, context, string.Empty, string.Empty, IsWrite, IsRead);
		}

		public void ReadData(IFhirXmlNode node, IDataSetterContext context)
		{
			if (Name == X_INCLUDE)
			{
				GetInclude(context).ReadData(node, context);
				return;
			}

			if (node is null || !HasAnyData || !IsRead)
				return;

			if (node.Name != Name)
				throw Error.InvalidOperation($"Could not map template to data: template root does not match data root. Template element: '{Name}', data element: '{node.Name}'.");

			if (HasDataInChildren)
			{
				var existingElements = node.Elements();
				var children = GetTemplateChildren(context);

				foreach (var child in children)
				{
					if (!child.IsRead || !child.HasAnyData)
						continue;

					var filtered = child.FilterElements(existingElements);

					if (filtered is null || filtered.Count == 0)
						continue;

					if (child.IsArray)
					{
						foreach (var item in filtered)
							child.ReadData(item, context.GetArrayElementSetter(item, child.Foreach));
					}
					else if (filtered.Count == 1)
						child.ReadData(filtered[0], context);
					else
						throw Error.InvalidOperation($"Could not read data: non-array element '{Name}.{child.Name}' (id: '{child.Id}') at template {child.Position}, xml {filtered[0].Position} has multiple values.");
				}
			}

			if (HasDataInAttributes)
			{
				foreach (var attibute in _attributes)
				{
					if (attibute.HasData)
						ReadContextValue(context, node, node.Attribute(attibute.Key)?.ValueAsString, attibute.Value);
				}
			}

			if (HasDataInText)
			{
				ReadContextValue(context, node, node.ValueAsString, Text);
			}
		}

		public IFhirXmlNode CreateData(IDataGetterContext context)
		{
			if (Name == X_INCLUDE)
				return GetInclude(context).CreateData(context);

			var root = new FhirXmlNodeWrapper();
			CreateElement(root, context);
			return root.UnWrap();
		}

		public void UpdateData(IFhirXmlNode node, IDataGetterContext context)
		{
			if (Name == X_INCLUDE)
			{
				GetInclude(context).UpdateData(node, context);
				return;
			}

			if (node is null || !HasAnyData || !IsWrite)
				return;

			if (node.Name != Name)
				throw Error.InvalidOperation($"Could not map template to data: template root does not match data root. Template element: '{Name}', data element: '{node.Name}'.");

			if (HasDataInChildren)
			{
				var existingElements = node.Elements();
				var children = GetTemplateChildren(context);

				foreach (var child in children)
				{
					if (!child.IsWrite || !child.HasAnyData && string.IsNullOrEmpty(child.If) && string.IsNullOrEmpty(Foreach))
						continue;

					var filtered = child.FilterElements(existingElements);

					if (child.IsArray)
					{
						UpdateList(node, filtered, child, context.GetArray(child.Foreach));
					}
					else if (filtered is null || filtered.Count == 0)
					{
						if (child.IsIfOk(context))
							child.CreateElement(node, context);
					}
					else if (filtered.Count == 1)
					{
						if (child.IsIfOk(context))
							child.UpdateData(filtered[0], context);
						else
							node.DeleteElement(filtered[0]);
					}
					else
					{
						throw Error.InvalidOperation($"Could not set data: non-array element '{Name}.{child.Name}' at {child.Position} has multiple values.");
					}
				}
			}

			if (HasDataInAttributes)
			{
				foreach (var attibute in _attributes)
				{
					if (attibute.HasData)
					{
						var existing = node.Attribute(attibute.Key);

						if (existing is null)
						{
							var value = ExtractData(attibute.Value, context);
							if (!string.IsNullOrEmpty(value))
								node.AddAttribute(attibute.Key, value);
						}
						else if (IsWriteAlways || IsRead) //We can not update value if !IsRead, because it was not read. We can only create it. Unless IsWriteAlways
						{
							var value = ExtractData(attibute.Value, context);
							existing.ValueAsString = value;
						}
					}
				}
			}

			if (HasDataInText)
			{
				node.ValueAsString = ExtractData(Text, context);
			}
		}

		public override string ToString()
		{
			string id = null;

			if (Id != null)
				id = $"x-id=\"{Id}\"";

			if (_children?.Count > 0)
				return $"<{Name} {id}></{Name}>";

			return $"<{Name} {id}/>";
		}

	}
}
