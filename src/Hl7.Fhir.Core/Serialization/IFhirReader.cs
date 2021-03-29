/* 
 * Copyright (c) 2014, Furore (info@furore.com) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using Hl7.Fhir.Support;
using System.Collections.Generic;

namespace Hl7.Fhir.Serialization
{
	internal interface IFhirReader : IPositionInfo
	{
		string GetResourceTypeName(bool nested);

		IEnumerable<MemberInfo> GetMembers();

		IEnumerable<IFhirReader> GetArrayElements();

		object GetPrimitiveValue();

		TokenType CurrentToken { get; }
	}

	internal enum TokenType
	{
		Object,
		Array,
		String,
		Boolean,
		Number,
		Null,
	}

	internal static class FhirReaderExtensions
	{
		public static bool IsPrimitive(this IFhirReader reader)
		{
			return reader.CurrentToken == TokenType.String ||
					reader.CurrentToken == TokenType.Number ||
					reader.CurrentToken == TokenType.Boolean;
		}
	}

	internal class MemberInfo
	{
		public MemberInfo(string memberName, IFhirReader reader)
		{
			MemberName = memberName;
			Reader = reader;
		}

		public string MemberName { get; }
		public IFhirReader Reader { get; }
	}

}
