using System.Collections.Generic;
using System.Collections.Immutable;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace Innovation.Script
{
	public class ScriptOptions
	{
		public SourceCodeKind SourceCodeKind { get; private set; }

		public LanguageVersion LanguageVersion { get; private set; }

		public OptimizationLevel OptimizationLevel { get; private set; }

		public ImmutableArray<AssemblyReference> References { get; private set; }

		public ImmutableArray<string> Usings { get; private set; }

		public static ScriptOptions Default { get; private set; }

		static ScriptOptions()
		{
			Default = new ScriptOptions();
		}

		public ScriptOptions(SourceCodeKind sourceCodeKind = SourceCodeKind.Script, LanguageVersion languageVersion = LanguageVersion.Latest, OptimizationLevel optimizationLevel = OptimizationLevel.Debug, IEnumerable<AssemblyReference> references = null, IEnumerable<string> usings = null)
		{
			SourceCodeKind = sourceCodeKind;
			LanguageVersion = languageVersion;
			OptimizationLevel = optimizationLevel;
			References = ((references != null) ? ImmutableArray.CreateRange(references) : ImmutableArray<AssemblyReference>.Empty);
			Usings = ((usings != null) ? ImmutableArray.CreateRange(usings) : ImmutableArray<string>.Empty);
		}
	}
}
