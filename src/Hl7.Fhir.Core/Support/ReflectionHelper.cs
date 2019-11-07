/* 
 * Copyright (c) 2014, Furore (info@furore.com) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Hl7.Fhir.Support
{
	internal static class ReflectionHelper
	{
		/// <summary>
		/// Gets an attribute on an enum field value
		/// </summary>
		/// <typeparam name="T">The type of the attribute you want to retrieve</typeparam>
		/// <param name="enumVal">The enum value</param>
		/// <returns>The attribute of type T that exists on the enum value</returns>
		public static T GetAttributeOnEnum<T>(this Enum enumVal) where T : System.Attribute
		{
			var type = enumVal.GetType();
			var memInfo = type.GetMember(enumVal.ToString())[0];
			var attributes = memInfo.GetCustomAttributes(typeof(T), false);
			return (attributes.Count() > 0) ? (T)attributes.First() : null;
		}


		public static IEnumerable<PropertyInfo> FindPublicProperties(Type t)
		{
			if (t == null) throw Error.ArgumentNull(nameof(t));
			return t.GetProperties(BindingFlags.Instance | BindingFlags.Public);
		}

		public static PropertyInfo FindPublicProperty(Type t, string name)
		{
			if (t == null) throw Error.ArgumentNull(nameof(t));
			if (name == null) throw Error.ArgumentNull(nameof(name));

			return t.GetProperty(name, BindingFlags.Instance | BindingFlags.Public);
		}

		internal static MethodInfo FindPublicStaticMethod(Type t, string name, params Type[] arguments)
		{
			if (t == null) throw Error.ArgumentNull(nameof(t));
			if (name == null) throw Error.ArgumentNull(nameof(name));

			return t.GetMethod(name, arguments);
		}

		internal static bool HasDefaultPublicConstructor(Type t)
		{
			if (t == null) throw Error.ArgumentNull(nameof(t));

			if (t.IsValueType)
				return true;

			return (GetDefaultPublicConstructor(t) != null);
		}

		internal static ConstructorInfo GetDefaultPublicConstructor(Type t)
		{
			BindingFlags bindingFlags = BindingFlags.Instance | BindingFlags.Public;

			return t.GetConstructors(bindingFlags).SingleOrDefault(c => !c.GetParameters().Any());
		}

		public static bool IsNullableType(Type type)
		{
			if (type == null) throw Error.ArgumentNull(nameof(type));

			return (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>));
		}

		public static Type GetNullableArgument(Type type)
		{
			if (type == null) throw Error.ArgumentNull(nameof(type));

			if (IsNullableType(type))
				return type.GetGenericArguments()[0];

			throw Error.Argument(nameof(type), $"Type {type.Name} is not a Nullable<T>");
		}

		public static bool IsTypedCollection(Type type)
		{
			return type.IsArray || ImplementsGenericDefinition(type, typeof(ICollection<>));
		}


		public static IList CreateGenericList(Type itemType)
		{
			return (IList)Activator.CreateInstance(typeof(List<>).MakeGenericType(itemType));
		}


		public static bool IsClosedGenericType(Type type)
		{
			return type.IsGenericType && !type.ContainsGenericParameters;
		}


		public static bool IsOpenGenericTypeDefinition(Type type)
		{
			return type.IsGenericTypeDefinition;
		}

		public static bool IsConstructedFromGenericTypeDefinition(Type type, Type genericBase)
		{
			return type.GetGenericTypeDefinition() == genericBase;
		}

		/// <summary>
		/// Gets the type of the typed collection's items.
		/// </summary>
		/// <param name="type">The type.</param>
		/// <returns>The type of the typed collection's items.</returns>
		public static Type GetCollectionItemType(Type type)
		{
			if (type == null) throw Error.ArgumentNull(nameof(type));

			Type genericListType;

			if (type.IsArray)
			{
				return type.GetElementType();
			}
			else if (ImplementsGenericDefinition(type, typeof(ICollection<>), out genericListType))
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
			if (type == null) throw Error.ArgumentNull(nameof(type));
			if (genericInterfaceDefinition == null) throw Error.ArgumentNull(nameof(genericInterfaceDefinition));

			if (!genericInterfaceDefinition.IsInterface || !genericInterfaceDefinition.IsGenericTypeDefinition)
				throw Error.Argument(nameof(genericInterfaceDefinition), $"'{genericInterfaceDefinition.Name}' is not a generic interface definition.");

			if (type.IsInterface)
			{

				if (type.IsGenericType)
				{
					Type interfaceDefinition = type.GetGenericTypeDefinition();

					if (genericInterfaceDefinition == interfaceDefinition)
					{
						implementingType = type;
						return true;
					}
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

		#region << Extension methods to make the handling of PCL easier >>

		internal static bool IsEnum(this Type t)
		{
			return t.IsEnum;
		}

		#endregion

		internal static T GetAttribute<T>(MemberInfo member) where T : Attribute
		{
			return (T)Attribute.GetCustomAttribute(member, typeof(T));
		}

		internal static ICollection<T> GetAttributes<T>(MemberInfo member) where T : Attribute
		{
			return (ICollection<T>)Attribute.GetCustomAttributes(member, typeof(T)).Select(a => (T)a);
		}


		internal static IEnumerable<FieldInfo> FindEnumFields(Type t)
		{
			if (t == null) throw Error.ArgumentNull(nameof(t));

			return t.GetFields(BindingFlags.Public | BindingFlags.Static);
		}

		internal static bool IsArray(object value)
		{
			if (value == null) throw Error.ArgumentNull(nameof(value));

			return value.GetType().IsArray;
		}
	}
}
