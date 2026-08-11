using System;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace Innovation.Script
{
	public class ScriptEngine : IDisposable
	{
		private ScriptOptions m_options;

		private ScriptExecutor m_executor;

		public ScriptOptions Options => m_options;

		public ScriptEngine(ScriptOptions options)
		{
			m_options = options;
			m_executor = new ScriptExecutor(m_options);
		}

		~ScriptEngine()
		{
			Dispose();
		}

		public ParseResult Parse(string sourceCode)
		{
			try
			{
				SourceCodeKind sourceCodeKind = m_options.SourceCodeKind;
				CSharpParseOptions options = new CSharpParseOptions(m_options.LanguageVersion, DocumentationMode.Parse, sourceCodeKind);
				return new ParseResult(SyntaxFactory.ParseSyntaxTree(sourceCode, options), null);
			}
			catch (Exception exception)
			{
				return new ParseResult(exception);
			}
		}

		public ExecutionResult Run(SyntaxTree syntaxTree)
		{
			try
			{
				return m_executor.Execute((CSharpSyntaxTree)syntaxTree);
			}
			catch (Exception exception)
			{
				return new ExecutionResult(exception);
			}
		}

		public ExecutionResult Run(string sourceCode)
		{
			ParseResult parseResult = Parse(sourceCode);
			if (parseResult.Exception != null)
			{
				return new ExecutionResult(parseResult.Exception);
			}
			return Run(parseResult.SyntaxTree);
		}

		public void Dispose()
		{
			m_executor.Dispose();
		}
	}
}
