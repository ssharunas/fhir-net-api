/* 
 * Copyright (c) 2014, Furore (info@furore.com) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using Hl7.Fhir.Serialization;
using Hl7.Fhir.Support;
using System;
using System.Linq.Expressions;
using System.Reflection;


namespace Hl7.Fhir.Introspection
{
	internal class PropertyMapping
	{
		private PropertyInfo _prop;
		private FhirElementAttribute _attribute;

		public PropertyMapping(PropertyInfo prop)
		{
			if (prop is null)
				throw Error.ArgumentNull(nameof(prop));

			_prop = prop;
			Name = Attribute?.Name ?? lowerCamel(_prop.Name);

			InSummary = Attribute?.InSummary ?? false;
			Choice = Attribute?.Choice ?? ChoiceType.None;
			SerializationHint = Attribute?.XmlSerialization ?? XmlSerializationHint.None;
			Order = Attribute?.Order ?? 0;

			IsCollection = !_prop.PropertyType.IsArray && ReflectionHelper.IsTypedCollection(prop.PropertyType);

			if (IsCollection)
				ElementType = ReflectionHelper.GetCollectionItemType(_prop.PropertyType);
			else if (ReflectionHelper.IsNullableType(_prop.PropertyType))
				ElementType = ReflectionHelper.GetNullableArgument(_prop.PropertyType);
			else
				ElementType = _prop.PropertyType;

			if (ElementType.IsEnum)
				IsPrimitive = true;
			else
			{
				IsPrimitive = PrimitiveTypeConverter.CanConvert(ElementType);
			}

			if (IsPrimitive)
				RepresentsValueElement = Attribute?.IsPrimitiveValue ?? false;
		}

		public string Name { get; }

		public bool IsCollection { get; }

		public bool IsPrimitive { get; }

		public Type ElementType { get; }

		public bool InSummary { get; }

		public ChoiceType Choice { get; }

		public XmlSerializationHint SerializationHint { get; }

		public int Order { get; }

		public bool RepresentsValueElement { get; }

		private FhirElementAttribute Attribute => _attribute ?? (_attribute = _prop.GetCustomAttribute<FhirElementAttribute>());

		internal static PropertyMapping Create(PropertyInfo prop)
		{
			return new PropertyMapping(prop);
		}

		private static string lowerCamel(string p)
		{
			if (string.IsNullOrEmpty(p)) return p;

			return char.ToLowerInvariant(p[0]) + p.Remove(0, 1);
		}

		private static string buildQualifiedPropName(PropertyInfo prop)
		{
			return prop.DeclaringType.Name + "." + prop.Name;
		}

		public bool MatchesSuffixedName(string suffixedName)
		{
			if (suffixedName is null) throw Error.ArgumentNull(nameof(suffixedName));

			return Choice == ChoiceType.DatatypeChoice && suffixedName.ToUpperInvariant().StartsWith(Name.ToUpperInvariant());
		}

		public string GetChoiceSuffixFromName(string suffixedName)
		{
			if (suffixedName is null) throw Error.ArgumentNull((suffixedName));

			if (MatchesSuffixedName(suffixedName))
				return suffixedName.Remove(0, Name.Length);
			else
				throw Error.Argument(nameof(suffixedName), $"The given suffixed name {suffixedName} does not match this property's name {Name}");
		}


		// May need to generate getters/setters using pre-compiled expression trees for performance.
		// See http://weblogs.asp.net/marianor/archive/2009/04/10/using-expression-trees-to-get-property-getter-and-setters.aspx
		//_getter = instance => prop.GetValue(instance, null);
		//_setter = (instance, value) => prop.SetValue(instance, value, null);

		//private Func<object, object> _getter;
		//private Action<object, object> _setter;

		private Func<object, object> _getter;
		private Action<object, object> _setter;

		public object GetValue(object instance)
		{
			if (_getter is null)
			{
				var input = Expression.Parameter(typeof(object));
				var casted = Expression.Convert(input, _prop.DeclaringType);
				var body = Expression.Call(casted, _prop.GetGetMethod());
				var castedToObj = Expression.Convert(body, typeof(object));

				LambdaExpression lambda = Expression.Lambda(typeof(Func<object,object>), castedToObj, input);
				_getter = (Func <object, object>) lambda.Compile();
			}
			
			return _getter(instance);
		}

		public void SetValue(object instance, object value)
		{
			if (_setter is null)
			{
				var valueInput = Expression.Parameter(typeof(object));
				var valueCasted = Expression.Convert(valueInput, _prop.PropertyType);

				var input = Expression.Parameter(typeof(object));
				var casted = Expression.Convert(input, _prop.DeclaringType);
				var body = Expression.Call(casted, _prop.GetSetMethod(true), valueCasted);

				LambdaExpression lambda = Expression.Lambda(typeof(Action<object, object>), body, input, valueInput);
				_setter = (Action<object, object>)lambda.Compile();
			}

			_setter(instance, value);

			//_prop.SetValue(instance, value, null);
			//_setter(instance, value);
		}
	}
}
