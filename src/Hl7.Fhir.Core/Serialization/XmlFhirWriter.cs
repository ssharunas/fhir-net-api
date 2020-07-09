/* 
 * Copyright (c) 2014, Furore (info@furore.com) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using Hl7.Fhir.Introspection;
using Hl7.Fhir.Support;
using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;

namespace Hl7.Fhir.Serialization
{
	internal class XmlFhirWriter : IFhirWriter
	{
		private readonly XmlWriter _xw;
		private readonly Stack<string> _memberNameStack = new Stack<string>();
		private string _currentMemberName = null;

		public XmlFhirWriter(XmlWriter xwriter)
		{
			_xw = xwriter;
		}

		public void WriteStartRootObject(string name, bool contained = false)
		{
			if (contained)
				WriteStartComplexContent();

			WriteStartProperty(name);
		}

		public void WriteEndRootObject(bool contained = false)
		{
			if (contained)
				WriteEndComplexContent();
		}

		public void WriteStartProperty(string name)
		{
			_currentMemberName = name;
		}

		public void WriteEndProperty()
		{

		}


		public void WriteStartComplexContent()
		{
			if (_currentMemberName is null)
				throw Error.InvalidOperation("There is no current member name set while starting complex content");

			_xw.WriteStartElement(_currentMemberName, XmlNs.FHIR);

			// A new complex element starts a new scope with its own members and member names
			_memberNameStack.Push(_currentMemberName);
			_currentMemberName = null;
		}

		public void WriteEndComplexContent()
		{
			_currentMemberName = _memberNameStack.Pop();
			_xw.WriteEndElement();
		}

		public void WritePrimitiveContents(object value, XmlSerializationHint xmlFormatHint)
		{
			if (value is null) throw Error.ArgumentNull(nameof(value), "There's no support for null values in Xml Fhir serialization");

			if (xmlFormatHint == XmlSerializationHint.None) xmlFormatHint = XmlSerializationHint.Attribute;

			var valueAsString = PrimitiveTypeConverter.ConvertTo<string>(value);

			if (xmlFormatHint == XmlSerializationHint.Attribute)
				_xw.WriteAttributeString(_currentMemberName, valueAsString);
			else if (xmlFormatHint == XmlSerializationHint.TextNode)
				_xw.WriteString(valueAsString);
			else if (xmlFormatHint == XmlSerializationHint.XhtmlElement)
			{
				XElement xe = XElement.Parse(valueAsString);
				xe.Name = XmlNs.XHTMLNS + xe.Name.LocalName;

				// Write xhtml directly into the output stream,
				// the xhtml <div> becomes part of the elements
				// of the type, just like the other FHIR elements
				_xw.WriteRaw(xe.ToString());
			}
			else
				throw Error.Argument(nameof(xmlFormatHint), "Unsupported xmlFormatHint " + xmlFormatHint);
		}

		public void WriteStartArray()
		{
			//nothing
		}

		public void WriteEndArray()
		{
			//nothing
		}

		public bool HasValueElementSupport
		{
			get { return false; }
		}

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		private void Dispose(bool disposing)
		{
			if (disposing && _xw != null) ((IDisposable)_xw).Dispose();
		}
	}
}
