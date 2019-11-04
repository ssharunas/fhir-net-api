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

namespace Hl7.Fhir.Rest
{
	public static class ResourceIdentityExtensions
    {
        // todo: dit is losgetrokken uit ResourceLocation. Maar nog niet af.

        public static string GetCollectionName(this Resource resource)
        {
            return (resource != null) 
                ? GetCollectionName(resource.GetType())
                : null;
        }

        public static string GetCollectionName(this Type type)
        {
            if (typeof(Resource).IsAssignableFrom(type))
                return ModelInfo.GetResourceNameForType(type);
            else
                throw Error.Argument($"Cannot determine collection name, type {type.Name} is not a resource type");
        }
    }
}