using System;
using Microsoft.CodeAnalysis;

namespace Innovation.Script
{
	public class ParseResult
	{
		public SyntaxTree SyntaxTree { get; private set; }

		public Exception Exception { get; private set; }

		public ParseResult(Exception exception)
			: this(null, exception)
		{
		}

		public ParseResult(SyntaxTree syntaxTree, Exception exception)
		{
			SyntaxTree = syntaxTree;
			Exception = exception;
		}
	}
}
