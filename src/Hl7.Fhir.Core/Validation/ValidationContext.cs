using System;
using System.Collections.Generic;
using System.Text;

namespace Hl7.Fhir.Validation
{
	public class ValidationContext : IServiceProvider
	{
		private Func<Type, object> _serviceProvider;
		private string _displayName;

		/// <summary>
		/// Construct a <see cref="ValidationContext"/> for a given object instance being validated.
		/// </summary>
		/// <param name="instance">The object instance being validated.  It cannot be <c>null</c>.</param>
		/// <exception cref="ArgumentNullException">When <paramref name="instance"/> is <c>null</c></exception>
		public ValidationContext(object instance) : this(instance, null, null) { }

		/// <summary>
		/// Construct a <see cref="ValidationContext"/> for a given object instance and an optional
		/// property bag of <paramref name="items"/>.
		/// </summary>
		/// <param name="instance">The object instance being validated.  It cannot be null.</param>
		/// <param name="items">Optional set of key/value pairs to make available to consumers via <see cref="Items"/>.
		/// If null, an empty dictionary will be created.  If not null, the set of key/value pairs will be copied into a
		/// new dictionary, preventing consumers from modifying the original dictionary.
		/// </param>
		/// <exception cref="ArgumentNullException">When <paramref name="instance"/> is <c>null</c></exception>
		public ValidationContext(object instance, IDictionary<object, object> items) : this(instance, null, items) { }

		/// <summary>
		/// Construct a <see cref="ValidationContext"/> for a given object instance, an optional <paramref name="serviceProvider"/>, and an optional
		/// property bag of <paramref name="items"/>.
		/// </summary>
		/// <param name="instance">The object instance being validated.  It cannot be null.</param>
		/// <param name="serviceProvider">
		/// Optional <see cref="IServiceProvider"/> to use when <see cref="GetService"/> is called.
		/// <para>
		/// If the <paramref name="serviceProvider"/> specified implements <see cref="Design.IServiceContainer"/>,
		/// then it will be used as the <see cref="ServiceContainer"/> but its services can still be retrieved
		/// through <see cref="GetService"/> as well.
		/// </para>
		/// </param>
		/// <param name="items">Optional set of key/value pairs to make available to consumers via <see cref="Items"/>.
		/// If null, an empty dictionary will be created.  If not null, the set of key/value pairs will be copied into a
		/// new dictionary, preventing consumers from modifying the original dictionary.
		/// </param>
		/// <exception cref="ArgumentNullException">When <paramref name="instance"/> is <c>null</c></exception>
		public ValidationContext(object instance, IServiceProvider serviceProvider, IDictionary<object, object> items)
		{
			if (instance is null)
				throw new ArgumentNullException("instance");

			if (serviceProvider != null)
				_serviceProvider = serviceType => serviceProvider.GetService(serviceType);

			if (items != null)
				Items = new Dictionary<object, object>(items);
			else
				Items = new Dictionary<object, object>();

			ObjectInstance = instance;
		}


		/// <summary>
		/// Gets the object instance being validated.  While it will not be null, the state of the instance is indeterminate
		/// as it might only be partially initialized during validation.
		/// <para>Consume this instance with caution!</para>
		/// </summary>
		/// <remarks>
		/// During validation, especially property-level validation, the object instance might be in an indeterminate state.
		/// For example, the property being validated, as well as other properties on the instance might not have been
		/// updated to their new values.
		/// </remarks>
		public object ObjectInstance { get; }

		/// <summary>
		/// Gets the type of the object being validated.  It will not be null.
		/// </summary>
		public Type ObjectType => ObjectInstance?.GetType();

		/// <summary>
		/// Gets or sets the user-visible name of the type or property being validated.
		/// </summary>
		/// <value>If this name was not explicitly set, this property will consult an associated <see cref="DisplayAttribute"/>
		/// to see if can use that instead.  Lacking that, it returns <see cref="MemberName"/>.  The <see cref="ObjectInstance"/>
		/// type name will be used if MemberName has not been set.
		/// </value>
		public string DisplayName
		{
			get
			{
				if (string.IsNullOrEmpty(_displayName))
				{
					_displayName = GetDisplayName();

					if (string.IsNullOrEmpty(_displayName))
					{
						_displayName = MemberName;

						if (string.IsNullOrEmpty(_displayName))
							_displayName = ObjectType.Name;
					}
				}

				return _displayName;
			}
			set
			{
				if (string.IsNullOrEmpty(value))
					throw new ArgumentNullException("value");

				_displayName = value;
			}
		}

		/// <summary>
		/// Gets or sets the name of the type or property being validated.
		/// </summary>
		/// <value>This name reflects the API name of the member being validated, not a localized name.  It should be set
		/// only for property or parameter contexts.</value>
		public string MemberName { get; set; }

		/// <summary>
		/// Gets the dictionary of key/value pairs associated with this context.
		/// </summary>
		/// <value>This property will never be null, but the dictionary may be empty.  Changes made
		/// to items in this dictionary will never affect the original dictionary specified in the constructor.</value>
		public IDictionary<object, object> Items { get; }

		/// <summary>
		/// Looks up the display name using the DisplayAttribute attached to the respective type or property.
		/// </summary>
		/// <returns>A display-friendly name of the member represented by the <see cref="MemberName"/>.</returns>
		private string GetDisplayName()
		{
			string displayName = null;
			/*
			ValidationAttributeStore store = ValidationAttributeStore.Instance;
			DisplayAttribute displayAttribute = null;

			if (string.IsNullOrEmpty(MemberName))
			{
				displayAttribute = store.GetTypeDisplayAttribute(this);
			}
			else if (store.IsPropertyContext(this))
			{
				displayAttribute = store.GetPropertyDisplayAttribute(this);
			}

			if (displayAttribute != null)
			{
				displayName = displayAttribute.GetName();
			}
			*/

			return displayName ?? MemberName;
		}

		/// <summary>
		/// See <see cref="IServiceProvider.GetService(Type)"/>.
		/// When the <see cref="ServiceContainer"/> is in use, it will be used
		/// first to retrieve the requested service.  If the <see cref="ServiceContainer"/>
		/// is not being used or it cannot resolve the service, then the
		/// <see cref="IServiceProvider"/> provided to this <see cref="ValidationContext"/>
		/// will be queried for the service type.
		/// </summary>
		/// <param name="serviceType">The type of the service needed.</param>
		/// <returns>An instance of that service or null if it is not available.</returns>
		public object GetService(Type serviceType)
		{
			object service = null;
			/*
			if (this._serviceContainer != null)
			{
				service = this._serviceContainer.GetService(serviceType);
			}

			if (service == null && _serviceProvider != null)
			{
				service = _serviceProvider(serviceType);
			}
			*/
			return service;
		}
	}
}
