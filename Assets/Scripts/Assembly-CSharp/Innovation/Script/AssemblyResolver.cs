using System;
using System.Reflection;

namespace Innovation.Script
{
	internal abstract class AssemblyResolver : IDisposable
	{
		~AssemblyResolver()
		{
			Dispose();
		}

		internal abstract Assembly LoadFromFile(string path);

		internal abstract Assembly LoadFromBytes(byte[] bytes);

		internal Assembly LoadFromReference(AssemblyReference reference)
		{
			try
			{
				if (reference is AssemblyByteReference assemblyByteReference)
				{
					return LoadFromBytes(assemblyByteReference.Bytes);
				}
				if (reference is AssemblyFileReference assemblyFileReference)
				{
					return LoadFromFile(assemblyFileReference.Path);
				}
				throw new ArgumentException("reference");
			}
			catch
			{
				return null;
			}
		}

		internal abstract void RegisterDependency(Assembly assembly);

		public abstract void Dispose();

		internal static AssemblyResolver Create()
		{
			return new AssemblyFrameworkResolver();
		}
	}
}
