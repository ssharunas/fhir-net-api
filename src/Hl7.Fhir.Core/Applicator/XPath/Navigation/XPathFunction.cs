using Hl7.Fhir.Serialization.Xml;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Hl7.Fhir.Applicator.XPath.Navigation
{
	internal class XPathFunction : IXPathNavigator
	{
		private abstract class XFunction
		{
			public int ParamCount { get; }

			public XFunction(int paramCount)
			{
				ParamCount = paramCount;
			}

			protected abstract object Invoke(IFhirXmlValue node, IList<IXPathNavigator> args);

			public object Value(IFhirXmlValue node, IList<IXPathNavigator> args)
			{
				return Invoke(node, args);
			}
		}

		private class XFunction0 : XFunction
		{
			private Func<object> _func;
			public XFunction0(Func<object> func) : base(0) { _func = func; }
			protected override object Invoke(IFhirXmlValue node, IList<IXPathNavigator> args)
			{
				return _func();
			}
		}

		private class XFunction1 : XFunction
		{
			Func<IFhirXmlValue, IXPathNavigator, object> _func;
			public XFunction1(Func<IFhirXmlValue, IXPathNavigator, object> func) : base(1) { _func = func; }
			protected override object Invoke(IFhirXmlValue node, IList<IXPathNavigator> args)
			{
				return _func(node, args[0]);
			}
		}

		private class XFunction2 : XFunction
		{
			private Func<IFhirXmlValue, IXPathNavigator, IXPathNavigator, object> _func;
			public XFunction2(Func<IFhirXmlValue, IXPathNavigator, IXPathNavigator, object> func) : base(2) { _func = func; }
			protected override object Invoke(IFhirXmlValue node, IList<IXPathNavigator> args)
			{
				return _func(node, args[0], args[1]);
			}
		}

		private class XFunction3 : XFunction
		{
			Func<IFhirXmlValue, IXPathNavigator, IXPathNavigator, IXPathNavigator, object> _func;
			public XFunction3(Func<IFhirXmlValue, IXPathNavigator, IXPathNavigator, IXPathNavigator, object> func) : base(3) { _func = func; }
			protected override object Invoke(IFhirXmlValue node, IList<IXPathNavigator> args)
			{
				return _func(node, args[0], args[1], args[2]);
			}
		}

		private class XFunctionN : XFunction
		{
			private Func<IFhirXmlValue, IList<IXPathNavigator>, object> _func;
			public XFunctionN(Func<IFhirXmlValue, IList<IXPathNavigator>, object> func) : base(-1) { _func = func; }
			protected override object Invoke(IFhirXmlValue node, IList<IXPathNavigator> args)
			{
				return _func(node, args);
			}
		}

		private static IDictionary<string, XFunction> _functions = new ConcurrentDictionary<string, XFunction>();

		static XPathFunction()
		{
			_functions.Add("boolean", new XFunction1((value, xpath) => xpath.IsMatch(value)));
			_functions.Add("ceiling", new XFunction1((value, xpath) => Math.Ceiling(ToNumber(xpath.RawValue(value)))));
			_functions.Add("choose", new XFunction3((value, xpath1, xpath2, xpath3) => xpath1.IsMatch(value) ? xpath2.Value(value) : xpath3.Value(value)));
			_functions.Add("concat", new XFunctionN((value, xpaths) => ToString(xpaths.Select(path => path.Value(value)).Where(item => item != null))));
			_functions.Add("contains", new XFunction2((value, xpath1, xpath2) => ToString(xpath1.Value(value)).Contains(ToString(xpath2.Value(value)))));
			_functions.Add("count", new XFunction1((value, xpath) => (decimal)(xpath.Values(value)?.Count ?? 0)));
			_functions.Add("false", new XFunction0(() => false));
			_functions.Add("floor", new XFunction1((value, xpath) => Math.Floor(ToNumber(xpath.RawValue(value)))));
			_functions.Add("not", new XFunction1((value, xpath) => !xpath.IsMatch(value)));
			_functions.Add("number", new XFunction1((value, xpath) => ToNumber(xpath.RawValue(value))));
			_functions.Add("round", new XFunction1((value, xpath) => Math.Round(ToNumber(xpath.RawValue(value)))));
			_functions.Add("starts-with", new XFunction2((value, xpath1, xpath2) => ToString(xpath1.Value(value)).StartsWith(ToString(xpath2.Value(value)))));
			_functions.Add("ends-with", new XFunction2((value, xpath1, xpath2) => ToString(xpath1.Value(value)).EndsWith(ToString(xpath2.Value(value)))));
			_functions.Add("string", new XFunction1((value, xpath) => ToString(xpath.Value(value))));
			_functions.Add("string-length", new XFunction1((value, xpath) => (decimal)ToString(xpath.Value(value)).Length));
			_functions.Add("true", new XFunction0(() => true));
			_functions.Add("function-available", new XFunction1((value, xpath) => _functions.ContainsKey(ToString(xpath.Value(value)))));
			_functions.Add("is-first", new XFunction1((value, xpath) => ReferenceEquals(xpath.Value(value), value)));
			_functions.Add("name", new XFunction1((value, xpath) => (xpath.Value(value) as IFhirXmlNode)?.Name));
			_functions.Add("index-of", new XFunction2((value, xpath1, xpath2) => IndexOf(xpath1.Values(value), xpath2.Value(value))));
		}

		private static decimal? IndexOf(IList<object> list, object value)
		{
			if (list?.Count > 0)
			{
				var index = list.IndexOf(value);
				if (index >= 0)
					return index;
			}

			return null;
		}

		private static decimal ToNumber(object obj)
		{
			if (obj is null)
				return 0;

			if (obj is decimal dec)
				return dec;
			if (obj is long lng)
				return lng;
			if (obj is int integer)
				return integer;
			if (obj is bool b)
				return b ? 1 : 0;
			if (obj is string str)
			{
				if (string.IsNullOrEmpty(str))
					return 0;

				if (decimal.TryParse(str, NumberStyles.Any, CultureInfo.InvariantCulture, out dec))
					return dec;
			}

			throw new FormatException($"Failed to convert value '{obj}' to number.");
		}

		private static string ToString(object obj)
		{
			if (obj is null)
				return string.Empty;

			return obj.ToString();
		}

		private static string ToString(IEnumerable<object> obj)
		{
			if (obj is null)
				return string.Empty;

			string result = null;

			foreach (var item in obj)
				result += ToString(item);

			return result;
		}

		private static bool ToBool(object obj)
		{
			if (obj is bool b)
				return b;
			if (obj is string str)
				return str.ToLower() == "true";
			if (obj is decimal?)
				return true; // obj != null; Jeigu bus null - net neįeis
			if (obj is decimal dec)
				return dec != 0;
			if (obj is long lng)
				return lng != 0;
			if (obj is int integer)
				return integer != 0;

			return false;
		}

		private string _functionName;
		private XFunction _function;
		private IList<IXPathNavigator> _arguments;

		public XPathFunction(string function, IList<IXPathNavigator> args)
		{
			if (!_functions.ContainsKey(function))
				throw new NotSupportedException($"XPath function '{function}' is not supported!");

			_function = _functions[function];

			if (_function.ParamCount != (args?.Count ?? 0) && _function.ParamCount != -1)
				throw new InvalidOperationException($"Invalid XPath function '{function}' invocation: expected {_function.ParamCount} parameters, but got {args?.Count ?? 0}.");

			_functionName = function;
			_arguments = args;
		}

		public bool IsMatch(IFhirXmlValue root)
		{
			return ToBool(RawValue(root));
		}

		public object RawValue(IFhirXmlValue node)
		{
			return _function.Value(node, _arguments);
		}

		public object Value(IFhirXmlValue node)
		{
			return ToString(RawValue(node));
		}

		public List<object> Values(IFhirXmlValue node)
		{
			var result = Value(node);

			if (result != null)
				return new List<object> { result };

			return null;
		}

		public override string ToString()
		{
			return $"{_functionName}({(_arguments?.Count > 0 ? string.Join(", ", _arguments) : "")})";
		}
	}
}
