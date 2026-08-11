using System;
using System.Reflection;

namespace Innovation.Script
{
	public class ExecutionResult
	{
		public object ReturnValue { get; private set; }

		public Assembly Assembly { get; private set; }

		public byte[] AssemblyBytes { get; private set; }

		public Exception Exception { get; private set; }

		public ExecutionResult(Exception exception)
			: this(null, null, null, exception)
		{
		}

		public ExecutionResult(object returnValue, Assembly assembly, byte[] assemblyBytes, Exception exception)
		{
			ReturnValue = returnValue;
			Assembly = assembly;
			AssemblyBytes = assemblyBytes;
			Exception = exception;
		}
	}
}
