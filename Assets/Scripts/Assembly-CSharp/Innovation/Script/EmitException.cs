using System;
using System.Collections.Immutable;
using System.Text;
using Microsoft.CodeAnalysis;

namespace Innovation.Script
{
	public class EmitException : ScriptException
	{
		internal EmitException()
		{
		}

		internal EmitException(string message)
			: base(message)
		{
		}

		internal EmitException(ImmutableArray<Diagnostic> diagnostics)
			: base(GetMessage(diagnostics))
		{
		}

		private static string GetMessage(ImmutableArray<Diagnostic> diagnostics)
		{
			StringBuilder stringBuilder = new StringBuilder();
			ImmutableArray<Diagnostic>.Enumerator enumerator = diagnostics.GetEnumerator();
			while (enumerator.MoveNext())
			{
				Diagnostic current = enumerator.Current;
				if (current.WarningLevel == 0)
				{
					try
					{
						stringBuilder.AppendLine(current.ToString());
					}
					catch (Exception ex)
					{
						stringBuilder.AppendLine(ex.ToString());
					}
				}
			}
			return stringBuilder.ToString();
		}
	}
}
