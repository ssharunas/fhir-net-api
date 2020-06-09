using System;
using System.Text;

namespace CodePlex.XPathParser
{
	internal class XPathParserException : Exception
	{
		private enum TrimType
		{
			Left,
			Right,
			Middle,
		}

		public string _queryString;
		public int _startChar;
		public int _endChar;

		public XPathParserException(string queryString, int startChar, int endChar, string message) : base(message)
		{
			_queryString = queryString;
			_startChar = startChar;
			_endChar = endChar;
		}

		// This function is used to prevent long quotations in error messages
		private static void AppendTrimmed(StringBuilder sb, string value, int startIndex, int count, TrimType trimType)
		{
			const int TRIM_SIZE = 32;
			const string TRIM_MARKER = "...";

			if (count <= TRIM_SIZE)
			{
				sb.Append(value, startIndex, count);
			}
			else
			{
				switch (trimType)
				{
					case TrimType.Left:
						sb.Append(TRIM_MARKER);
						sb.Append(value, startIndex + count - TRIM_SIZE, TRIM_SIZE);
						break;
					case TrimType.Right:
						sb.Append(value, startIndex, TRIM_SIZE);
						sb.Append(TRIM_MARKER);
						break;
					case TrimType.Middle:
						sb.Append(value, startIndex, TRIM_SIZE / 2);
						sb.Append(TRIM_MARKER);
						sb.Append(value, startIndex + count - TRIM_SIZE / 2, TRIM_SIZE / 2);
						break;
				}
			}
		}

		internal string MarkOutError()
		{
			if (string.IsNullOrWhiteSpace(_queryString))
				return null;

			int len = _endChar - _startChar;
			StringBuilder sb = new StringBuilder();

			AppendTrimmed(sb, _queryString, 0, _startChar, TrimType.Left);
			if (len > 0)
			{
				sb.Append(" -->");
				AppendTrimmed(sb, _queryString, _startChar, len, TrimType.Middle);
			}

			sb.Append("<-- ");
			AppendTrimmed(sb, _queryString, _endChar, _queryString.Length - _endChar, TrimType.Right);

			return sb.ToString();
		}


		private string FormatDetailedMessage()
		{
			string message = Message;
			string error = MarkOutError();

			if (error?.Length > 0)
			{
				if (message.Length > 0)
					message += Environment.NewLine;

				message += error;
			}
			return message;
		}

		public override string ToString()
		{
			string result = GetType().FullName;
			string info = FormatDetailedMessage();

			if (info?.Length > 0)
				result += ": " + info;

			if (StackTrace != null)
				result += Environment.NewLine + StackTrace;

			return result;
		}

	}
}