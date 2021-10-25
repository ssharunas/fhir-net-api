using Hl7.Fhir.Core.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace Hl7.Fhir.Validation
{
	public static class Validator
	{
		private static ValidationAttributeStore _store = ValidationAttributeStore.Instance;

		/// <summary>
		/// Tests whether the given object instance is valid.
		/// </summary>
		/// <remarks>
		/// This method evaluates all <see cref="ValidationAttribute"/>s attached to the object instance's type.  It also
		/// checks to ensure all properties marked with <see cref="RequiredAttribute"/> are set.  If <paramref name="validateAllProperties"/>
		/// is <c>true</c>, this method will also evaluate the <see cref="ValidationAttribute"/>s for all the immediate properties
		/// of this object.  This process is not recursive.
		/// <para>
		/// If <paramref name="validationResults"/> is null, then execution will abort upon the first validation
		/// failure.  If <paramref name="validationResults"/> is non-null, then all validation attributes will be
		/// evaluated.
		/// </para>
		/// <para>
		/// For any given property, if it has a <see cref="RequiredAttribute"/> that fails validation, no other validators
		/// will be evaluated for that property.
		/// </para>
		/// </remarks>
		/// <param name="instance">The object instance to test.  It cannot be null.</param>
		/// <param name="validationContext">Describes the object to validate and provides services and context for the validators.</param>
		/// <param name="validationResults">Optional collection to receive <see cref="ValidationResult"/>s for the failures.</param>
		/// <param name="validateAllProperties">If <c>true</c>, also evaluates all properties of the object (this process is not
		/// recursive over properties of the properties).</param>
		/// <returns><c>true</c> if the object is valid, <c>false</c> if any validation errors are encountered.</returns>
		/// <exception cref="ArgumentNullException">When <paramref name="instance"/> is null.</exception>
		/// <exception cref="ArgumentException">When <paramref name="instance"/> doesn't match the
		/// <see cref="ValidationContext.ObjectInstance"/>on <paramref name="validationContext"/>.</exception>
		public static bool TryValidateObject(object instance, ValidationContext validationContext, ICollection<ValidationResult> validationResults, bool validateAllProperties)
		{
			if (instance == null)
			{
				throw new ArgumentNullException("instance");
			}

			if (validationContext != null && instance != validationContext.ObjectInstance)
			{
				throw new ArgumentException(DataAnnotationsResources.Validator_InstanceMustMatchValidationContextInstance, nameof(instance));
			}

			bool result = true;
			bool breakOnFirstError = (validationResults == null);

			foreach (ValidationError err in GetObjectValidationErrors(instance, validationContext, validateAllProperties, breakOnFirstError))
			{
				result = false;

				if (validationResults != null)
				{
					validationResults.Add(err.ValidationResult);
				}
			}

			return result;
		}

		/// <summary>
		/// Internal iterator to enumerate all validation errors for the given object instance.
		/// </summary>
		/// <param name="instance">Object instance to test.</param>
		/// <param name="validationContext">Describes the object type.</param>
		/// <param name="validateAllProperties">if <c>true</c> also validates all properties.</param>
		/// <param name="breakOnFirstError">Whether to break on the first error or validate everything.</param>
		/// <returns>A collection of validation errors that result from validating the <paramref name="instance"/> with
		/// the given <paramref name="validationContext"/>.</returns>
		/// <exception cref="ArgumentNullException">When <paramref name="instance"/> is null.</exception>
		/// <exception cref="ArgumentNullException">When <paramref name="validationContext"/> is null.</exception>
		/// <exception cref="ArgumentException">When <paramref name="instance"/> doesn't match the
		/// <see cref="ValidationContext.ObjectInstance"/> on <paramref name="validationContext"/>.</exception>
		private static IEnumerable<ValidationError> GetObjectValidationErrors(object instance, ValidationContext validationContext, bool validateAllProperties, bool breakOnFirstError)
		{
			if (instance == null)
				throw new ArgumentNullException(nameof(instance));

			if (validationContext == null)
				throw new ArgumentNullException(nameof(validationContext));

			// Step 1: Validate the object properties' validation attributes
			List<ValidationError> errors = new List<ValidationError>();
			errors.AddRange(GetObjectPropertyValidationErrors(instance, validationContext, validateAllProperties, breakOnFirstError));

			// We only proceed to Step 2 if there are no errors
			if (errors?.Count > 0)
			{
				return errors;
			}

			// Step 2: Validate the object's validation attributes
			IEnumerable<ValidationAttribute> attributes = _store.GetTypeValidationAttributes(validationContext);
			errors.AddRange(GetValidationErrors(instance, validationContext, attributes, breakOnFirstError));

#if !SILVERLIGHT
			// We only proceed to Step 3 if there are no errors
			if (errors.Any())
			{
				return errors;
			}

			// Step 3: Test for IValidatableObject implementation
			IValidatableObject validatable = instance as IValidatableObject;
			if (validatable != null)
			{
				IEnumerable<ValidationResult> results = validatable.Validate(validationContext);

				foreach (ValidationResult result in results.Where(r => r != ValidationResult.Success))
				{
					errors.Add(new ValidationError(null, instance, result));
				}
			}
#endif

			return errors;
		}

		/// <summary>
		/// Internal iterator to enumerate all the validation errors for all properties of the given object instance.
		/// </summary>
		/// <param name="instance">Object instance to test.</param>
		/// <param name="validationContext">Describes the object type.</param>
		/// <param name="validateAllProperties">If <c>true</c>, evaluates all the properties, otherwise just checks that
		/// ones marked with <see cref="RequiredAttribute"/> are not null.</param>
		/// <param name="breakOnFirstError">Whether to break on the first error or validate everything.</param>
		/// <returns>A list of <see cref="ValidationError"/> instances.</returns>
		private static IEnumerable<ValidationError> GetObjectPropertyValidationErrors(object instance, ValidationContext validationContext, bool validateAllProperties, bool breakOnFirstError)
		{
			ICollection<KeyValuePair<ValidationContext, object>> properties = GetPropertyValues(instance, validationContext);
			List<ValidationError> errors = new List<ValidationError>();

			foreach (KeyValuePair<ValidationContext, object> property in properties)
			{
				// get list of all validation attributes for this property
				IEnumerable<ValidationAttribute> attributes = _store.GetPropertyValidationAttributes(property.Key);

				if (validateAllProperties)
				{
					// validate all validation attributes on this property
					errors.AddRange(GetValidationErrors(property.Value, property.Key, attributes, breakOnFirstError));
				}
				else
				{
					// only validate the Required attributes
					RequiredAttribute reqAttr = attributes.FirstOrDefault(a => a is RequiredAttribute) as RequiredAttribute;
					if (reqAttr != null)
					{
						// Note: we let the [Required] attribute do its own null testing,
						// since the user may have subclassed it and have a deeper meaning to what 'required' means
						ValidationResult validationResult = reqAttr.GetValidationResult(property.Value, property.Key);
						if (validationResult != ValidationResult.Success)
						{
							errors.Add(new ValidationError(reqAttr, property.Value, validationResult));
						}
					}
				}

				if (breakOnFirstError && errors.Any())
				{
					break;
				}
			}

			return errors;
		}

		/// <summary>
		/// Retrieves the property values for the given instance.
		/// </summary>
		/// <param name="instance">Instance from which to fetch the properties.</param>
		/// <param name="validationContext">Describes the entity being validated.</param>
		/// <returns>A set of key value pairs, where the key is a validation context for the property and the value is its current
		/// value.</returns>
		/// <remarks>Ignores indexed properties.</remarks>
		private static ICollection<KeyValuePair<ValidationContext, object>> GetPropertyValues(object instance, ValidationContext validationContext)
		{
			PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(instance);
			List<KeyValuePair<ValidationContext, object>> items = new List<KeyValuePair<ValidationContext, object>>(properties.Count);
			foreach (PropertyDescriptor property in properties)
			{
				ValidationContext context = CreateValidationContext(instance, validationContext);
				context.MemberName = property.Name;

				if (_store.GetPropertyValidationAttributes(context).Any())
				{
					items.Add(new KeyValuePair<ValidationContext, object>(context, property.GetValue(instance)));
				}
			}
			return items;
		}

		/// <summary>
		/// Creates a new <see cref="ValidationContext"/> to use to validate the type or a member of
		/// the given object instance.
		/// </summary>
		/// <param name="instance">The object instance to use for the context.</param>
		/// <param name="validationContext">An parent validation context that supplies an <see cref="IServiceProvider"/>
		/// and <see cref="ValidationContext.Items"/>.</param>
		/// <returns>A new <see cref="ValidationContext"/> for the <paramref name="instance"/> provided.</returns>
		/// <exception cref="ArgumentNullException">When <paramref name="validationContext"/> is null.</exception>
		internal static ValidationContext CreateValidationContext(object instance, ValidationContext validationContext)
		{
			if (validationContext == null)
			{
				throw new ArgumentNullException("validationContext");
			}

			// Create a new context using the existing ValidationContext that acts as an IServiceProvider and contains our existing items.
			ValidationContext context = new ValidationContext(instance, validationContext, validationContext.Items);
			return context;
		}

		/// <summary>
		/// Tests whether a value is valid against a single <see cref="ValidationAttribute"/> using the <see cref="ValidationContext"/>.
		/// </summary>
		/// <param name="value">The value to be tested for validity.</param>
		/// <param name="validationContext">Describes the property member to validate.</param>
		/// <param name="attribute">The validation attribute to test.</param>
		/// <param name="validationError">The validation error that occurs during validation.  Will be <c>null</c> when the return value is <c>true</c>.</param>
		/// <returns><c>true</c> if the value is valid.</returns>
		/// <exception cref="ArgumentNullException">When <paramref name="validationContext"/> is null.</exception>
		private static bool TryValidate(object value, ValidationContext validationContext, ValidationAttribute attribute, out ValidationError validationError)
		{
			if (validationContext == null)
			{
				throw new ArgumentNullException("validationContext");
			}

			ValidationResult validationResult = attribute.GetValidationResult(value, validationContext);
			if (validationResult != ValidationResult.Success)
			{
				validationError = new ValidationError(attribute, value, validationResult);
				return false;
			}

			validationError = null;
			return true;
		}

		/// <summary>
		/// Internal iterator to enumerate all validation errors for an value.
		/// </summary>
		/// <remarks>
		/// If a <see cref="RequiredAttribute"/> is found, it will be evaluated first, and if that fails,
		/// validation will abort, regardless of the <paramref name="breakOnFirstError"/> parameter value.
		/// </remarks>
		/// <param name="value">The value to pass to the validation attributes.</param>
		/// <param name="validationContext">Describes the type/member being evaluated.</param>
		/// <param name="attributes">The validation attributes to evaluate.</param>
		/// <param name="breakOnFirstError">Whether or not to break on the first validation failure.  A
		/// <see cref="RequiredAttribute"/> failure will always abort with that sole failure.</param>
		/// <returns>The collection of validation errors.</returns>
		/// <exception cref="ArgumentNullException">When <paramref name="validationContext"/> is null.</exception>
		private static IEnumerable<ValidationError> GetValidationErrors(object value, ValidationContext validationContext, IEnumerable<ValidationAttribute> attributes, bool breakOnFirstError)
		{
			if (validationContext == null)
			{
				throw new ArgumentNullException("validationContext");
			}

			List<ValidationError> errors = new List<ValidationError>();
			ValidationError validationError;

			// Get the required validator if there is one and test it first, aborting on failure
			RequiredAttribute required = attributes.FirstOrDefault(a => a is RequiredAttribute) as RequiredAttribute;
			if (required != null)
			{
				if (!TryValidate(value, validationContext, required, out validationError))
				{
					errors.Add(validationError);
					return errors;
				}
			}

			// Iterate through the rest of the validators, skipping the required validator
			foreach (ValidationAttribute attr in attributes)
			{
				if (attr != required)
				{
					if (!TryValidate(value, validationContext, attr, out validationError))
					{
						errors.Add(validationError);

						if (breakOnFirstError)
						{
							break;
						}
					}
				}
			}

			return errors;
		}

		/// <summary>
		/// Private helper class to encapsulate a ValidationAttribute with the failed value and the user-visible
		/// target name against which it was validated.
		/// </summary>
		private class ValidationError
		{
			internal ValidationError(ValidationAttribute validationAttribute, object value, ValidationResult validationResult)
			{
				this.ValidationAttribute = validationAttribute;
				this.ValidationResult = validationResult;
				this.Value = value;
			}

			internal object Value { get; set; }

			internal ValidationAttribute ValidationAttribute { get; set; }

			internal ValidationResult ValidationResult { get; set; }

			internal void ThrowValidationException()
			{
				throw new ValidationException(this.ValidationResult, this.ValidationAttribute, this.Value);
			}
		}
	}
}
