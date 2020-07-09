/* 
 * Copyright (c) 2014, Furore (info@furore.com) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using Hl7.Fhir.Support;
using System;

namespace Hl7.Fhir.Serialization
{
	internal static class DefaultModelFactory
	{
		public static object Create(Type type)
		{
			if (type is null)
				throw Error.ArgumentNull(nameof(type));

			// For nullable types, create an instance of the type that
			// is made nullable, since otherwise you'll create an (untyped) null
			if (ReflectionHelper.IsNullableType(type))
				type = ReflectionHelper.GetNullableArgument(type);

			// If type is a typed collection (but not an array), and the type
			// is not a concrete collection type, but an interface, create a new List of
			// the given type.
			if (!type.IsArray && type.IsInterface && ReflectionHelper.IsTypedCollection(type))
			{
				var elementType = ReflectionHelper.GetCollectionItemType(type);
				type = ReflectionHelper.GetGenericListType(elementType);
			}

			return ReflectionHelper.CreateInstance(type);
		}
	}
}
