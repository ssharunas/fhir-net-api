using System;

namespace Hl7.Fhir.Model
{
	public class FhirIdentifier
	{
		private ResourceType _type = ResourceType.Resource;

		public FhirIdentifier()
		{
		}

		public FhirIdentifier(long id)
		{
			ID = id;
		}

		public FhirIdentifier(long id, ResourceType type)
		{
			ID = id;
			Type = type;
		}

		public FhirIdentifier(long id, bool isCid)
		{
			ID = id;
			IsCid = isCid;
		}

		public FhirIdentifier(long id, long version)
		{
			ID = id;
			Version = version;
		}

		public FhirIdentifier(long id, long version, ResourceType type)
		{
			ID = id;
			Version = version;
			Type = type;
		}

		public long ID { get; private set; }
		public long Version { get; private set; }

		public bool IsCid { get; private set; }

		/// <summary>
		/// True if ID is local (starts with #)
		/// </summary>
		public bool IsLocal { get; private set; }

		/// <summary>
		/// Type of a HL7 object this identifier is referring to.
		/// </summary>
		public ResourceType Type
		{
			get { return _type; }
			internal set { _type = value; }
		}

		public FhirIdentifier RemoveVersion()
		{
			Version = -1;
			return this;
		}

		/// <summary>
		/// Returns true if ID has version extension.
		/// </summary>
		public bool HasVersion()
		{
			return Version > 0;
		}

		public FhirIdentifier Clone()
		{
			return new FhirIdentifier(ID, Version)
			{
				Type = Type,
				IsCid = IsCid,
				IsLocal = IsLocal,
			};
		}

		public void EnsureType<T>()
		{
			if (Type == ResourceType.Resource)
			{
				ResourceType _type;
				if (Enum.TryParse(typeof(T).Name, out _type))
				{
					Type = _type;
				}
				else throw new Exception($"Type {typeof(T)} is not supported by the ResourceType");
			}
		}

		public bool Is<T>()
		{
			ResourceType _type;
			if (Enum.TryParse(typeof(T).Name, out _type))
				return Is(_type);

			return false;
		}

		public bool Is(ResourceType type)
		{
			return type == Type;
		}

		public override string ToString()
		{
			return ToString(ResourceType.Resource);
		}

		public string ToString<T>()
		{
			ResourceType _type;

			if (Enum.TryParse(typeof(T).Name, out _type))
			{
				if (_type != Type)
					throw new Exception($"Invalid type '{Type}'. Expected type: '{typeof(T)}'.");

				return ToString(_type);
			}

			throw new Exception($"Type {typeof(T)} is not supported by the ResourceType");
		}

		/// <summary>
		/// Same as constructor with 'long' parameter.
		/// </summary>
		public static implicit operator FhirIdentifier(long value)
		{
			return new FhirIdentifier(value);
		}

		public static implicit operator FhirIdentifier(long? value)
		{
			if (value.HasValue)
				return new FhirIdentifier(value.Value);

			return null;
		}

		public static implicit operator FhirIdentifier(Uri value)
		{
			return Parse(value?.ToString());
		}

		public static implicit operator FhirIdentifier(ResourceReference value)
		{
			return Parse(value);
		}

		public static implicit operator FhirIdentifier(string value)
		{
			return Parse(value);
		}

		public static implicit operator FhirIdentifier(Identifier value)
		{
			return Parse(value?.Value);
		}

		public static implicit operator ulong(FhirIdentifier value)
		{
			return (ulong)value.ID;
		}

		public static implicit operator long(FhirIdentifier value)
		{
			return value?.ID ?? -1;
		}

		public static implicit operator string(FhirIdentifier value)
		{
			return value?.ToString();
		}

		public static implicit operator FhirIdentifier(Resource value)
		{
			if (value != null)
				return Parse(value.Id);

			return null;
		}

		public static implicit operator ResourceReference(FhirIdentifier value)
		{
			if (value != null)
				return new ResourceReference { Reference = value.ToString() };

			return null;
		}

		/// <summary>
		/// Returns true, if ID has version extension
		/// </summary>
		public static bool HasVersion(string id)
		{
			return !string.IsNullOrEmpty(id) && id.Contains("_history");
		}

		private static bool IsNumeric(string value)
		{
			if (!string.IsNullOrEmpty(value))
			{
				foreach (char c in value)
				{
					if (c < '0' || c > '9')
						return false;
				}

				return true;
			}

			return false;
		}

		public static FhirIdentifier Parse(ResourceReference value)
		{
			return Parse(value?.Reference);
		}

		/// <returns>Parsed HL7 ID instance</returns>
		public static FhirIdentifier Parse(string value)
		{
			FhirIdentifier result = null;

			if (!string.IsNullOrEmpty(value))
			{
				long id = -1;

				if (value.StartsWith("#"))
				{
					long.TryParse(value.Substring(1), out id);

					if (id > 0)
						result = new FhirIdentifier(id) { IsLocal = true };
				}
				else if (value.StartsWith("cid:"))
				{
					long.TryParse(value.Substring(4), out id);

					if (id > 0)
						result = new FhirIdentifier(id) { IsCid = true };
				}
				else
				{
					string[] splited = value.Trim('/').Split('/');
					bool isHistory = false;
					long version = -1;
					string type = null;

					foreach (var item in splited)
					{
						if (IsNumeric(item))
						{
							if (isHistory)
								long.TryParse(item, out version);
							else
								long.TryParse(item, out id);
						}
						else if (item == "_history")
							isHistory = true;
						else
							type = item;
					}

					ResourceType _type = ResourceType.Resource;
					if (id > 0 && Enum.TryParse(type, out _type))
						result = new FhirIdentifier(id, version) { Type = _type };
				}
			}

			return result;
		}

		private string ToString(ResourceType type)
		{
			string result;

			if (IsLocal)
				result = "#" + ID;
			else if (IsCid)
				result = "cid:" + ID;
			else
			{
				if (Type != ResourceType.Resource)
					type = Type;

				if (Type != ResourceType.Resource)
				{
					result = type + "/" + ID;

					if (Version > 0)
						result += "/_history/" + Version;
				}
				else
					result = ID.ToString();
			}

			return result;
		}
	}
}