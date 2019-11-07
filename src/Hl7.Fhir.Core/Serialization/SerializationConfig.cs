/* 
 * Copyright (c) 2014, Furore (info@furore.com) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using Hl7.Fhir.Introspection;
using Hl7.Fhir.Model;

namespace Hl7.Fhir.Serialization
{
	internal static class SerializationConfig
	{
		private static ModelInspector _inspector;

		public const string BINARY_CONTENT_MEMBER_NAME = "content";

		public static bool AcceptUnknownMembers { get; set; }

		public static bool EnforceNoXsiAttributesOnRoot { get; set; }

		internal static ModelInspector Inspector
		{
			get
			{
				if (_inspector == null)
				{
					_inspector = new ModelInspector();
					_inspector.Import(typeof(Resource).Assembly);
				}

				return _inspector;
			}
		}
	}
}
