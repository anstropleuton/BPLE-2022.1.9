using System;

namespace Innovation.Script
{
	public class RuntimeException : ScriptException
	{
		private Exception m_error;

		public Exception Error => m_error;

		public override string Message
		{
			get
			{
				if (m_error == null)
				{
					return base.Message;
				}
				return m_error.GetType().ToString() + ": " + m_error.Message;
			}
		}

		internal RuntimeException()
		{
		}

		internal RuntimeException(string message)
			: base(message)
		{
		}

		internal RuntimeException(Exception error)
		{
			m_error = error;
		}
	}
}
