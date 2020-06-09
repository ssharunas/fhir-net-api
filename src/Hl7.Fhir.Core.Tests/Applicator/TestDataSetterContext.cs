using System;
using System.Collections.Generic;

namespace Hl7.Fhir.Applicator
{
	internal class TestDataSetterContext : TestDataContextBase, IDataSetterContext
	{
		public Dictionary<string, IList<TestDataSetterContext>> Arrays { get; } = new Dictionary<string, IList<TestDataSetterContext>>();
		public Dictionary<string, string> Values { get; } = new Dictionary<string, string>();
		public WeakReference<IPathable> Node { get; set; }

		public IDataSetterContext GetArrayElementSetter(IPathable node, string key)
		{
			if (!Arrays.ContainsKey(key))
				Arrays[key] = new List<TestDataSetterContext>();

			var result = new TestDataSetterContext() { Node = new WeakReference<IPathable>(node) };
			Arrays[key].Add(result);

			return result;
		}

		public void SetData(IPathable node, string key, string value)
		{
			if (Values.ContainsKey(key) && Values[key] != value)
				throw new ArgumentException($"Value for key {key} was already set! And it is not equal to current value: '{Values[key]}' != '{value}'");

			Values[key] = value;
		}

	}
}
