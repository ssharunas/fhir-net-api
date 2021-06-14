/* 
 * Copyright (c) 2014, Furore (info@furore.com) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using Hl7.Fhir.Model;
using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Hl7.Fhir.Support
{
	internal static class ReflectionHelper
	{
		private static readonly IDictionary<Type, Type> _listTypes = new ConcurrentDictionary<Type, Type>();
		private static readonly IDictionary<Type, Type> _resourceTypes = new ConcurrentDictionary<Type, Type>();
		private static readonly IDictionary<Type, Func<object>> _cachedTypes = new ConcurrentDictionary<Type, Func<object>>();

		/// <summary>
		/// Gets an attribute on an enum field value
		/// </summary>
		/// <typeparam name="T">The type of the attribute you want to retrieve</typeparam>
		/// <param name="enumVal">The enum value</param>
		/// <returns>The attribute of type T that exists on the enum value</returns>
		public static T GetAttributeOnEnum<T>(this Enum enumVal) where T : Attribute
		{
			var memInfo = enumVal.GetType().GetMember(enumVal.ToString())[0];
			var attributes = memInfo.GetCustomAttributes(typeof(T), false);
			return (attributes.Count() > 0) ? (T)attributes.First() : null;
		}

		public static IEnumerable<PropertyInfo> FindPublicProperties(Type t)
		{
			if (t is null)
				throw Error.ArgumentNull(nameof(t));

			return t.GetProperties(BindingFlags.Instance | BindingFlags.Public);
		}

		public static bool IsNullableType(Type type)
		{
			if (type is null)
				throw Error.ArgumentNull(nameof(type));

			return (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>));
		}

		public static Type GetNullableArgument(Type type)
		{
			if (IsNullableType(type))
				return type.GetGenericArguments()[0];

			throw Error.Argument(nameof(type), $"Type {type.Name} is not a Nullable<T>");
		}

		public static bool IsTypedCollection(Type type)
		{
			return type.IsArray || ImplementsGenericDefinition(type, typeof(ICollection<>));
		}


		public static IList CreateGenericList(Type elementType)
		{
			return (IList)CreateInstance(GetGenericListType(elementType));
		}

		public static Type GetGenericListType(Type elementType)
		{
			if (!_listTypes.TryGetValue(elementType, out Type result))
			{
				result = typeof(List<>).MakeGenericType(elementType);
				_listTypes[elementType] = result;
			}

			return result;
		}

		public static Type GetGenericResourceType(Type elementType)
		{
			if (!_resourceTypes.TryGetValue(elementType, out Type result))
			{
				result = typeof(ResourceEntry<>).MakeGenericType(elementType);
				_resourceTypes[elementType] = result;
			}

			return result;
		}

		public static bool IsClosedGenericType(Type type)
		{
			return type.IsGenericType && !type.ContainsGenericParameters;
		}

		public static bool IsOpenGenericTypeDefinition(Type type)
		{
			return type.IsGenericTypeDefinition;
		}

		/// <summary>
		/// Gets the type of the typed collection's items.
		/// </summary>
		/// <param name="type">The type.</param>
		/// <returns>The type of the typed collection's items.</returns>
		public static Type GetCollectionItemType(Type type)
		{
			if (type is null) throw Error.ArgumentNull(nameof(type));


			if (type.IsArray)
			{
				return type.GetElementType();
			}
			else if (ImplementsGenericDefinition(type, typeof(ICollection<>), out Type genericListType))
			{
				//EK: If I look at ImplementsGenericDefinition, I don't think this can actually occur.
				//if (genericListType.IsGenericTypeDefinition)
				//throw Error.Argument(nameof(type), "Type {0} is not a collection.", type.Name);

				return genericListType.GetGenericArguments()[0];
			}
			else if (typeof(IEnumerable).IsAssignableFrom(type))
			{
				return null;
			}
			else
			{
				throw Error.Argument(nameof(type), $"Type {type.Name} is not a collection.");
			}
		}

		public static bool ImplementsGenericDefinition(Type type, Type genericInterfaceDefinition)
		{
			return ImplementsGenericDefinition(type, genericInterfaceDefinition, out _);
		}

		public static bool ImplementsGenericDefinition(Type type, Type genericInterfaceDefinition, out Type implementingType)
		{
			if (type is null)
				throw Error.ArgumentNull(nameof(type));

			if (genericInterfaceDefinition is null)
				throw Error.ArgumentNull(nameof(genericInterfaceDefinition));

			if (!genericInterfaceDefinition.IsInterface || !genericInterfaceDefinition.IsGenericTypeDefinition)
				throw Error.Argument(nameof(genericInterfaceDefinition), $"'{genericInterfaceDefinition.Name}' is not a generic interface definition.");

			if (type.IsInterface && type.IsGenericType)
			{
				Type interfaceDefinition = type.GetGenericTypeDefinition();

				if (genericInterfaceDefinition == interfaceDefinition)
				{
					implementingType = type;
					return true;
				}
			}

			foreach (Type i in type.GetInterfaces())
			{
				if (i.IsGenericType)
				{
					Type interfaceDefinition = i.GetGenericTypeDefinition();

					if (genericInterfaceDefinition == interfaceDefinition)
					{
						implementingType = i;
						return true;
					}
				}
			}

			implementingType = null;
			return false;
		}

		internal static T GetAttribute<T>(MemberInfo member) where T : Attribute
		{
			return (T)Attribute.GetCustomAttribute(member, typeof(T));
		}

		internal static bool HasAttribute<T>(MemberInfo member) where T : Attribute
		{
			return Attribute.IsDefined(member, typeof(T));
		}

		internal static IEnumerable<FieldInfo> FindEnumFields(Type t)
		{
			if (t is null)
				throw Error.ArgumentNull(nameof(t));

			return t.GetFields(BindingFlags.Public | BindingFlags.Static);
		}

		internal static bool IsArray(object value)
		{
			if (value is null)
				throw Error.ArgumentNull(nameof(value));

			return value.GetType().IsArray;
		}

		internal static object CreateInstance(Type type)
		{
			//Activator.CreateInstance(type);

			if (!_cachedTypes.TryGetValue(type, out Func<object> creator))
			{
				var constructor = type.GetConstructor(Array.Empty<Type>());

				if (constructor is null)
				{
					creator = () => Activator.CreateInstance(type);
				}
				else
				{
					Expression expr = Expression.New(constructor);
					LambdaExpression lambda = Expression.Lambda(typeof(Func<object>), expr);
					creator = (Func<object>)lambda.Compile();
				}

				_cachedTypes[type] = creator;
			}

			return creator();
		}
	}
}
