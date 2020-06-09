using System.Collections.Generic;
using System.Xml.XPath;

namespace CodePlex.XPathParser
{
	internal interface IXPathBuilder<TNode>
	{
		// Should be called once per build
		void StartBuild();

		// Should be called after build for result tree post-processing
		TNode EndBuild(TNode result);

		TNode String(string value);

		TNode Number(string value);

		TNode Operator(XPathOperator op, TNode left, TNode right);

		TNode Axis(XPathAxis xpathAxis, XPathNodeType nodeType, string prefix, string name);

		TNode JoinStep(TNode left, TNode right);

		// http://www.w3.org/TR/xquery-semantics/#id-axis-steps
		// reverseStep is how parser comunicates to builder diference between "ansestor[1]" and "(ansestor)[1]" 
		TNode Predicate(TNode node, TNode condition, bool reverseStep);

		TNode Variable(string prefix, string name);

		TNode Function(string prefix, string name, IList<TNode> args);
	}
}