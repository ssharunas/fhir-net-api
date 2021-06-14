/* 
 * Copyright (c) 2014, Furore (info@furore.com) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using Hl7.Fhir.Model;
using Hl7.Fhir.Support;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace Hl7.Fhir.Introspection
{
	internal class ClassMapping
	{
		private static IDictionary<Type, bool> _definedTypes = new ConcurrentDictionary<Type, bool>();

		/// <summary>
		/// Name of the FHIR datatype/resource this class represents
		/// </summary>
		public string Name { get; private set; }

		/// <summary>
		/// Profile scope within this Name and mapping are applicable
		/// </summary>
		public string Profile { get; private set; }

		/// <summary>
		/// The .NET class that implements the FHIR datatype/resource
		/// </summary>
		public Type NativeType { get; private set; }

		/// <summary>
		/// Is True when this class represents a Resource datatype and False if it 
		/// represents a normal complex or primitive Fhir Datatype
		/// </summary>
		public bool IsResource { get; private set; }

		/// <summary>
		/// PropertyMappings indexed by uppercase name for access speed
		/// </summary>
		private Dictionary<string, PropertyMapping> _propMappings = new Dictionary<string, PropertyMapping>();

		/// <summary>
		/// Collection of PropertyMappings that capture information about this classes
		/// properties
		/// </summary>
		public ICollection<PropertyMapping> PropertyMappings { get; private set; }

		/// <summary>
		/// Holds a reference to a property that represents a primitive FHIR value. This
		/// property will also be present in the PropertyMappings collection. If this class has 
		/// no such property, it is null. 
		/// </summary>
		public PropertyMapping PrimitiveValueProperty { get; private set; }

		public bool HasPrimitiveValueMember => PrimitiveValueProperty != null;

		public PropertyMapping FindMappedElementByName(string name)
		{
			if (name is null) throw Error.ArgumentNull(nameof(name));

			var normalizedName = name.ToUpperInvariant();

			// Direct success
			if (_propMappings.TryGetValue(normalizedName, out PropertyMapping prop))
				return prop;

			// Not found, maybe a polymorphic name
			// TODO: specify possible polymorhpic variations using attributes
			// to speedup look up & aid validation
			return PropertyMappings.SingleOrDefault(p => p.MatchesSuffixedName(name));
		}

		public static ClassMapping Create(Type type)
		{
			// checkMutualExclusiveAttributes(type);

			var result = new ClassMapping();
			result.NativeType = type;

			if (IsMappableType(type))
			{
				result.Name = collectTypeName(type);
				result.Profile = getProfile(type);
				result.IsResource = IsFhirResource(type);

				if (!result.IsResource && !string.IsNullOrEmpty(result.Profile))
					throw Error.Argument(nameof(type), $"Type {type.Name} is not a resource, so its FhirType attribute may not specify a profile");

				inspectProperties(result);

				return result;
			}
			else
				throw Error.Argument(nameof(type), $"Type {type.Name} is not marked as a Fhir Resource or datatype using [FhirType]");
		}

		/// <summary>
		/// Enumerate this class' properties using reflection, create PropertyMappings
		/// for them and add them to the PropertyMappings.
		/// </summary>
		private static void inspectProperties(ClassMapping me)
		{
			foreach (var property in ReflectionHelper.FindPublicProperties(me.NativeType))
			{
				// Skip properties that are marked as NotMapped
				if (ReflectionHelper.HasAttribute<NotMappedAttribute>(property)) continue;

				var propMapping = PropertyMapping.Create(property);
				var propKey = propMapping.Name.ToUpperInvariant();

				if (me._propMappings.ContainsKey(propKey))
					throw Error.InvalidOperation($"Class has multiple properties that are named '{propKey}'. The property name must be unique");

				me._propMappings.Add(propKey, propMapping);

				// Keep a pointer to this property if this is a primitive value element ("Value" in primitive types)
				if (propMapping.RepresentsValueElement)
					me.PrimitiveValueProperty = propMapping;
			}

			me.PropertyMappings = me._propMappings.Values.OrderBy(prop => prop.Order).ToList();
		}

		private static string getProfile(Type type)
		{
			return ReflectionHelper.GetAttribute<FhirTypeAttribute>(type)?.Profile;
		}

		private static string collectTypeName(Type type)
		{
			string name = ReflectionHelper.GetAttribute<FhirTypeAttribute>(type)?.Name ?? type.Name;

			if (ReflectionHelper.IsClosedGenericType(type))
			{
				name += "<";
				name += string.Join(",", type.GetGenericArguments().Select(arg => arg.FullName));
				name += ">";
			}

			return name;
		}

		public static bool IsFhirResource(Type type)
		{
			if (typeof(Resource).IsAssignableFrom(type))
				return true;

			return ReflectionHelper.GetAttribute<FhirTypeAttribute>(type)?.IsResource ?? false;
		}

		public static bool IsMappableType(Type type)
		{
			if (!_definedTypes.TryGetValue(type, out bool result))
			{
				if (!type.IsDefined(typeof(FhirTypeAttribute), false))
					result = false;
				else if (type.IsAbstract)
					throw Error.Argument(nameof(type), $"Type {type.Name} is marked as a mappable type, but is abstract so cannot be used directly to represent a FHIR datatype");
				else if (ReflectionHelper.IsOpenGenericTypeDefinition(type))
				{
					// Open generic type definitions can never appear as roots of objects
					// to parse. In instances, they will either have been used in closed type definitions
					// or as the closed type of a property. However, the FhirType attribute propagates to
					// these closed definitions, so we will allow having this attribute on an open generic,
					// it's not going to be directly mappable however.

					Message.Debug("Type {0} is marked as a FhirType and is an open generic type, which cannot be used directly to represent a FHIR datatype", type.Name);
					result = false;
				}
				else
					result = true;

				_definedTypes[type] = result;
			}

			return result;
		}
	}
}
