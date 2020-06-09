using System;
using System.Collections.Generic;

namespace Hl7.Fhir.Applicator
{
	internal class TestDataGetterContext : TestDataContextBase, IDataGetterContext
	{
		private Dictionary<string, IList<IDataGetterContext>> Arrays { get; } = new Dictionary<string, IList<IDataGetterContext>>();
		private Dictionary<string, string> Values { get; } = new Dictionary<string, string>();
		private WeakReference<IPathable> Node { get; set; }

		public TestDataGetterContext()
		{

		}

		public TestDataGetterContext(TestDataSetterContext copy)
		{
			if (copy != null)
			{
				Node = copy.Node;

				foreach (var item in copy.Values)
					AddValue(item.Key, item.Value);

				foreach (var item in copy.Arrays)
				{
					foreach (var value in item.Value)
						AddArray(item.Key, new TestDataGetterContext(value));
				}
			}
		}

		public IList<IDataGetterContext> GetArray(string key)
		{
			if (Arrays.ContainsKey(key))
				return Arrays[key];

			return null;
		}

		public bool GetBoolean(string key)
		{
			if (Values.ContainsKey(key))
			{
				return !string.IsNullOrEmpty(Values[key]) || Arrays.ContainsKey(key) && Arrays[key].Count > 0;
			}
			else if (key.EndsWith(".."))
			{
				key = key.Substring(0, key.Length - 1);

				foreach (var item in Values.Keys)
				{
					if (item.StartsWith(key) && !string.IsNullOrEmpty(Values[item]))
						return true;
				}
			}

			return false;
		}

		public string GetString(string key)
		{
			if (Values.ContainsKey(key))
				return Values[key];

			throw new Exception($"Key {key} was not found in getter context!");
		}

		public bool IsForNode(IPathable node)
		{
			if (Node.TryGetTarget(out IPathable target))
				return ReferenceEquals(node, target);

			return false;
		}

		internal TestDataGetterContext AddValue(string key, string value)
		{
			Values.Add(key, value);
			return this;
		}

		internal TestDataGetterContext AddArray(string key, TestDataGetterContext value)
		{
			if (!Arrays.ContainsKey(key))
				Arrays[key] = new List<IDataGetterContext>();

			Arrays[key].Add(value);

			return this;
		}

		public string this[string key]
		{
			set => Values[key] = value;
		}

	}
}
