// Copyright (c) Microsoft Open Technologies, Inc. All rights reserved. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using Hl7.Fhir.Serialization;


namespace Hl7.Fhir.Support
{
	internal static class Message
	{
		internal static void Info(string format, params object[] args)
		{
#if DEBUG
			Debug.WriteLine(format, args);
#endif
		}
	}


	/// <summary>
	/// Utility class for creating and unwrapping <see cref="Exception"/> instances.
	/// </summary>
	internal static class Error
	{
		/// <summary>
		/// Creates an <see cref="ArgumentException"/> with the provided properties.
		/// </summary>
		/// <param name="message">A composite format string explaining the reason for the exception.</param>
		/// <param name="messageArgs">An object array that contains zero or more objects to format.</param>
		/// <returns>The logged <see cref="Exception"/>.</returns>
		internal static ArgumentException Argument(string message)
		{
			return new ArgumentException(message);
		}

		/// <summary>
		/// Creates an <see cref="ArgumentException"/> with the provided properties.
		/// </summary>
		/// <param name="parameterName">The name of the parameter that caused the current exception.</param>
		/// <param name="message">A composite format string explaining the reason for the exception.</param>
		/// <param name="messageArgs">An object array that contains zero or more objects to format.</param>
		/// <returns>The logged <see cref="Exception"/>.</returns>
		internal static ArgumentException Argument(string parameterName, string message)
		{
			return new ArgumentException(message, parameterName);
		}

		/// <summary>
		/// Creates an <see cref="ArgumentNullException"/> with the provided properties.
		/// </summary>
		/// <param name="parameterName">The name of the parameter that caused the current exception.</param>
		/// <returns>The logged <see cref="Exception"/>.</returns>
		internal static ArgumentException ArgumentNull(string parameterName)
		{
			return new ArgumentNullException(parameterName);
		}

		/// <summary>
		/// Creates an <see cref="ArgumentNullException"/> with the provided properties.
		/// </summary>
		/// <param name="parameterName">The name of the parameter that caused the current exception.</param>
		/// <param name="message">A composite format string explaining the reason for the exception.</param>
		/// <param name="messageArgs">An object array that contains zero or more objects to format.</param>
		/// <returns>The logged <see cref="Exception"/>.</returns>
		internal static ArgumentNullException ArgumentNull(string parameterName, string message)
		{
			return new ArgumentNullException(parameterName, message);
		}

		/// <summary>
		/// Creates an <see cref="InvalidOperationException"/>.
		/// </summary>
		/// <param name="message">A composite format string explaining the reason for the exception.</param>
		/// <param name="messageArgs">An object array that contains zero or more objects to format.</param>
		/// <returns>The logged <see cref="Exception"/>.</returns>
		internal static InvalidOperationException InvalidOperation(string message)
		{
			return new InvalidOperationException(message);
		}

		/// <summary>
		/// Creates an <see cref="NotSupportedException"/>.
		/// </summary>
		/// <param name="message">A composite format string explaining the reason for the exception.</param>
		/// <param name="messageArgs">An object array that contains zero or more objects to format.</param>
		/// <returns>The logged <see cref="Exception"/>.</returns>
		internal static NotSupportedException NotSupported(string message, Exception innerException = null)
		{
			return new NotSupportedException(message, innerException);
		}

		/// <summary>
		/// Creates an <see cref="FormatException"/> with the provided properties.
		/// </summary>
		/// <param name="message">A composite format string explaining the reason for the exception.</param>
		/// <param name="pos">Optional line position information for the message</param>
		/// <param name="messageArgs">An object array that contains zero or more objects to format.</param>
		/// <returns>The logged <see cref="Exception"/>.</returns>
		internal static FormatException Format(string message, IPostitionInfo pos = null)
		{
			if (pos != null)
				message = $"At line {pos.LineNumber}, pos {pos.LinePosition}: {message}";

			return new FormatException(message);
		}

		/// <summary>
		/// Creates an <see cref="NotImplementedException"/>.
		/// </summary>
		/// <param name="messageFormat">A composite format string explaining the reason for the exception.</param>
		/// <param name="messageArgs">An object array that contains zero or more objects to format.</param>
		/// <returns>The logged <see cref="Exception"/>.</returns>
		internal static NotImplementedException NotImplemented(string message)
		{
			return new NotImplementedException(message);
		}

	}
}
