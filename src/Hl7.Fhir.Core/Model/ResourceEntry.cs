/*
  Copyright (c) 2011-2013, HL7, Inc.
  All rights reserved.
  
  Redistribution and use in source and binary forms, with or without modification, 
  are permitted provided that the following conditions are met:
  
   * Redistributions of source code must retain the above copyright notice, this 
     list of conditions and the following disclaimer.
   * Redistributions in binary form must reproduce the above copyright notice, 
     this list of conditions and the following disclaimer in the documentation 
     and/or other materials provided with the distribution.
   * Neither the name of HL7 nor the names of its contributors may be used to 
     endorse or promote products derived from this software without specific 
     prior written permission.
  
  THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND 
  ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED 
  WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED. 
  IN NO EVENT SHALL THE COPYRIGHT HOLDER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, 
  INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT 
  NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR 
  PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, 
  WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) 
  ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE 
  POSSIBILITY OF SUCH DAMAGE.
*/

using Hl7.Fhir.Support;
using Hl7.Fhir.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace Hl7.Fhir.Model
{
	[InvokeIValidatableObject]
	public class ResourceEntry<T> : ResourceEntry, Validation.IValidatableObject where T : Resource, new()
	{
		public ResourceEntry(Uri id, string title, DateTimeOffset lastUpdated, T resource) : this()
		{
			Id = id;
			Title = title;
			LastUpdated = lastUpdated;
			Resource = resource;
		}

		public ResourceEntry(Uri id, DateTimeOffset lastUpdated, T resource)
			: this(id, $"{typeof(T).Name} resource with id {id}", lastUpdated, resource)
		{
		}


		public ResourceEntry() : base()
		{
		}

		public new T Resource
		{
			get { return (T)((ResourceEntry)this).Resource; }
			set { ((ResourceEntry)this).Resource = value; }
		}

		public static ResourceEntry<T> Create(T resource)
		{
			return new ResourceEntry<T>() { Resource = resource };
		}
	}

	[InvokeIValidatableObject]
	public abstract class ResourceEntry : BundleEntry, Validation.IValidatableObject
	{
		public Resource Resource { get; set; }

		[Required(AllowEmptyStrings = false)]
		public string Title { get; set; }

		[Required]
		public DateTimeOffset? LastUpdated { get; set; }
		public DateTimeOffset? Published { get; set; }
		public string AuthorName { get; set; }
		public string AuthorUri { get; set; }


		/// <summary>
		/// Creates an instance of a typed ResourceEntry&lt;T&gt;, based on the actual type of the passed resource parameter
		/// </summary>
		/// <param name="resource"></param>
		/// <returns></returns>
		public static ResourceEntry Create(Resource resource)
		{
			if (resource == null) throw Error.ArgumentNull(nameof(resource));

			var result = Create(resource.GetType());
			result.Resource = resource;

			return result;
		}

		/// <summary>
		/// Creates an instance of a typed ResourceEntry&lt;T&gt;, based on the actual type of the passed resource parameter
		/// </summary>
		/// <param name="type">The type of entry (T in ResourceEntry&lt;T&gt;) to create</param>
		/// <returns></returns>
		public static ResourceEntry Create(Type type)
		{
			if (type == null) throw Error.ArgumentNull(nameof(type));
			var isResource = typeof(Resource).GetTypeInfo().IsAssignableFrom(type.GetTypeInfo());

			if (!isResource) throw Error.Argument(nameof(type), "Must be a subtype of Resource");

			return (ResourceEntry)ReflectionHelper.CreateInstance(ReflectionHelper.GetGenericResourceType(type));
		}

		/// <summary>
		/// Read-only. Returns the summary text from a Resource.
		/// </summary>
		public override string Summary
		{
			get
			{
				if (Resource is Binary binary)
					return $"<div xmlns='http://www.w3.org/1999/xhtml'>Binary content (mediatype {binary.ContentType})</div>";

				return Resource?.Text?.Div;
			}
		}

		public override IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
		{
			var result = new List<ValidationResult>(base.Validate(validationContext));

			//if (Resource == null)
			//    result.Add(new ValidationResult("Entry must contain Resource data, Content may not be null"));
			if (Resource != null && validationContext.ValidateRecursively())
				DotNetAttributeValidation.TryValidate(Resource, result, validationContext.ValidateRecursively());

			return result;
		}

	}
}
