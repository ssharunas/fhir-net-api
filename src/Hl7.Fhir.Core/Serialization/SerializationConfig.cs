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
	public static class SerializationConfig
	{
		private static volatile ModelInspector _inspector;

		static SerializationConfig()
		{
			_inspector = new ModelInspector();
			_inspector.Import(typeof(Resource).Assembly);
		}

		public static bool AcceptUnknownMembers { get; set; }

		public static bool IsIndentOutput { get; set; }

		/// <summary>
		/// Do not allow non-array elements to be mapped to array nodes.
		/// </summary>
		public static bool IsParserStrict { get; set; } = true;

		internal static ModelInspector Inspector => _inspector;
	}
}
