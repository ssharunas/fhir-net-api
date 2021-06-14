/* 
 * Copyright (c) 2014, Furore (info@furore.com) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using Hl7.Fhir.Support;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Reflection;

namespace Hl7.Fhir.Introspection
{
	//TODO: Find out the right way to handle named resource-local component types (i.e. Patient.AnimalComponent)
	internal class ModelInspector
	{
		// Index for easy lookup of resources, key is Tuple<upper resourcename, upper profile>
		private ConcurrentDictionary<string, ClassMapping> _resourceClasses = new ConcurrentDictionary<string, ClassMapping>();

		// Index for easy lookup of datatypes, key is upper typenanme
		private IDictionary<string, ClassMapping> _dataTypeClasses = new ConcurrentDictionary<string, ClassMapping>();

		// Index for easy lookup of classmappings, key is Type
		private IDictionary<Type, ClassMapping> _classMappingsByType = new ConcurrentDictionary<Type, ClassMapping>();

		// Index for easy lookup of enummappings, key is Type
		private IDictionary<Type, EnumMapping> _enumMappingsByType = new ConcurrentDictionary<Type, EnumMapping>();

		private object lockObject = new object();

		public void Import(Assembly assembly)
		{
			if (assembly is null)
				throw Error.ArgumentNull(nameof(assembly));

			if (Attribute.GetCustomAttribute(assembly, typeof(NotMappedAttribute)) != null)
				return;

			Type[] exportedTypes = assembly.GetExportedTypes();

			foreach (Type type in exportedTypes)
			{
				// Don't import types marked with [NotMapped]
				if (Attribute.GetCustomAttribute(type, typeof(NotMappedAttribute)) != null)
					continue;

				if (type.IsEnum)
				{
					// Map an enumeration
					if (EnumMapping.IsMappableEnum(type))
						ImportEnum(type);
					else
						Message.Debug("Skipped enum {0} while doing inspection: not recognized as representing a FHIR enumeration", type.Name);
				}
				else
				{
					// Map a Fhir Datatype
					if (ClassMapping.IsMappableType(type))
						ImportType(type);
					else
						Message.Debug("Skipped type {0} while doing inspection: not recognized as representing a FHIR type", type.Name);
				}
			}
		}

		internal EnumMapping ImportEnum(Type type)
		{
			EnumMapping mapping = null;

			if (!EnumMapping.IsMappableEnum(type))
				throw Error.Argument(nameof(type), $"Type {type.Name} is not a mappable enumeration");

			lock (lockObject)
			{
				mapping = FindEnumMappingByType(type);

				if (mapping is null)
				{
					mapping = EnumMapping.Create(type);
					_enumMappingsByType[type] = mapping;
				}
			}

			return mapping;
		}

		internal ClassMapping ImportType(Type type)
		{
			ClassMapping mapping = null;

			if (!ClassMapping.IsMappableType(type))
				throw Error.Argument(nameof(type), $"Type {type.Name} is not a mappable Fhir datatype or resource");

			lock (lockObject)
			{
				mapping = FindClassMappingByType(type);
				if (mapping is null)
				{
					mapping = ClassMapping.Create(type);
					_classMappingsByType[type] = mapping;

					if (mapping.IsResource)
					{
						var key = buildResourceKey(mapping.Name, mapping.Profile);
						_resourceClasses[key] = mapping;
					}
					else
					{
						var key = mapping.Name.ToUpperInvariant();
						_dataTypeClasses[key] = mapping;
					}
				}
			}

			return mapping;
		}

		private static string buildResourceKey(string name, string profile)
		{
			name = name.ToUpperInvariant();

			if (!string.IsNullOrEmpty(profile))
				name = string.Concat(",", profile.ToUpperInvariant());

			return name;
		}

		public EnumMapping FindEnumMappingByType(Type type)
		{
			if (type is null)
				throw Error.ArgumentNull(nameof(type));

			if (!type.IsEnum)
				throw Error.Argument(nameof(type), $"Type {type.Name} is not an enumeration");

			// Try finding a resource with the specified profile first
			if (_enumMappingsByType.TryGetValue(type, out EnumMapping entry))
				return entry;

			return null;
		}

		public ClassMapping FindClassMappingForResource(string name, string profile = null)
		{
			var key = buildResourceKey(name, profile);

			if (_resourceClasses.TryGetValue(key, out ClassMapping entry))
				return entry;

			if (!string.IsNullOrEmpty(profile))
			{
				key = buildResourceKey(name, null);
				if (_resourceClasses.TryGetValue(key, out entry))
					return entry;
			}

			return null;
		}

		public ClassMapping FindClassMappingForFhirDataType(string name)
		{
			var key = name.ToUpperInvariant();

			if (_dataTypeClasses.TryGetValue(key, out ClassMapping entry))
				return entry;

			return null;
		}

		public ClassMapping FindClassMappingByType(Type type)
		{
			if (!_classMappingsByType.TryGetValue(type, out ClassMapping entry))
				return null;

			// Do an extra lookup via this mapping's name when this is a Resource. This will find possible
			// replacement mappings, when a later import for the same Fhir typename
			// was found.
			if (entry.IsResource)
				return FindClassMappingForResource(entry.Name, entry.Profile);

			return entry;   // NB: no extra lookup for non-resource types
		}
	}

}
