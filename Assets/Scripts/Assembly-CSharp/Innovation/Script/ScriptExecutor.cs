using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Emit;

namespace Innovation.Script
{
	internal class ScriptExecutor : IDisposable
	{
		private int m_submissionCount;

		private object[] m_submissionStates;

		private CSharpCompilation m_compilation;

		private CSharpCompilationOptions m_compilationOptions;

		private ScriptOptions m_options;

		private AssemblyResolver m_assemblyResolver;

		private static int s_executionIndex;

		public ScriptExecutor(ScriptOptions options)
		{
			s_executionIndex++;
			m_options = options;
			m_compilationOptions = new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary).WithOptimizationLevel(m_options.OptimizationLevel).WithUsings(m_options.Usings);
			m_assemblyResolver = AssemblyResolver.Create();
			InitializeAssemblyTable();
		}

		static ScriptExecutor()
		{
			s_executionIndex = -1;
		}

		~ScriptExecutor()
		{
			Dispose();
		}

		public void UpdateSubmissionStates()
		{
			if (m_submissionCount == 0)
			{
				m_submissionCount = 1;
				m_submissionStates = new object[4];
				return;
			}
			m_submissionCount++;
			if (m_submissionCount >= m_submissionStates.Length)
			{
				Array.Resize(ref m_submissionStates, m_submissionStates.Length * 2);
			}
		}

		public ExecutionResult Execute(CSharpSyntaxTree syntaxTree)
		{
			ImmutableArray<MetadataReference> immutableArray = ImmutableArray.CreateRange(GetReferences());
			CSharpCompilationOptions cSharpCompilationOptions = m_compilationOptions.WithScriptClassName("Innovation_Submission_" + s_executionIndex + "_" + m_submissionCount);
			if (m_compilation != null)
			{
				immutableArray = ImmutableArray<MetadataReference>.Empty;
				cSharpCompilationOptions = cSharpCompilationOptions.WithUsings(ImmutableArray<string>.Empty);
			}
			CSharpCompilation cSharpCompilation = CSharpCompilation.CreateScriptCompilation("Innovation_Assembly_" + s_executionIndex + "_" + m_submissionCount, syntaxTree, immutableArray, cSharpCompilationOptions, m_compilation, typeof(object));
			UpdateSubmissionStates();
			using MemoryStream memoryStream = new MemoryStream();
			EmitResult emitResult = cSharpCompilation.Emit(memoryStream);
			if (!emitResult.Success)
			{
				throw new EmitException(emitResult.Diagnostics);
			}
			byte[] array = memoryStream.ToArray();
			Assembly assembly = m_assemblyResolver.LoadFromBytes(array);
			m_compilation = cSharpCompilation;
			m_assemblyResolver.RegisterDependency(assembly);
			IMethodSymbol? entryPoint = cSharpCompilation.GetEntryPoint(CancellationToken.None);
			string metadataName = entryPoint.ContainingNamespace.MetadataName;
			string metadataName2 = entryPoint.ContainingType.MetadataName;
			string name = (string.IsNullOrEmpty(metadataName) ? metadataName2 : (metadataName + "." + metadataName2));
			string metadataName3 = entryPoint.MetadataName;
			Func<object[], Task<object>> func = (Func<object[], Task<object>>)assembly.GetType(name, throwOnError: true, ignoreCase: false).GetTypeInfo().GetDeclaredMethod(metadataName3)
				.CreateDelegate(typeof(Func<object[], Task<object>>));
			object result;
			try
			{
				result = func(m_submissionStates).GetAwaiter().GetResult();
			}
			catch (Exception error)
			{
				return new ExecutionResult(null, assembly, array, new RuntimeException(error));
			}
			return new ExecutionResult(result, assembly, array, null);
		}

		private void InitializeAssemblyTable()
		{
			ImmutableArray<AssemblyReference>.Enumerator enumerator = m_options.References.GetEnumerator();
			while (enumerator.MoveNext())
			{
				AssemblyReference current = enumerator.Current;
				Assembly assembly = m_assemblyResolver.LoadFromReference(current);
				if (assembly != null)
				{
					m_assemblyResolver.RegisterDependency(assembly);
				}
			}
		}

		private IEnumerable<MetadataReference> GetReferences()
		{
			ImmutableArray<AssemblyReference>.Enumerator enumerator = m_options.References.GetEnumerator();
			while (enumerator.MoveNext())
			{
				AssemblyReference current = enumerator.Current;
				yield return current.MetadataReference;
			}
		}

		public void Dispose()
		{
			m_assemblyResolver.Dispose();
		}
	}
}
