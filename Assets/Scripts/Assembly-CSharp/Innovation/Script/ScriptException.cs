using System;

namespace Innovation.Script
{
	public abstract class ScriptException : Exception
	{
		internal ScriptException()
		{
		}

		internal ScriptException(string message)
			: base(message)
		{
		}
	}
}
