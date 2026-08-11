using System;
using System.Collections.Generic;
using System.Reflection;

namespace Innovation.Script
{
	internal class AssemblyFrameworkResolver : AssemblyResolver
	{
		private Dictionary<string, Assembly> m_assemblyTable;

		internal AssemblyFrameworkResolver()
		{
			m_assemblyTable = new Dictionary<string, Assembly>();
			AppDomain.CurrentDomain.AssemblyResolve += ResolveAssembly;
		}

		internal override Assembly LoadFromBytes(byte[] bytes)
		{
			return Assembly.Load(bytes);
		}

		internal override Assembly LoadFromFile(string path)
		{
			return Assembly.LoadFile(path);
		}

		internal override void RegisterDependency(Assembly assembly)
		{
			m_assemblyTable.Add(assembly.FullName, assembly);
		}

		internal Assembly ResolveAssembly(object sender, ResolveEventArgs args)
		{
			if (m_assemblyTable.TryGetValue(args.Name, out var value))
			{
				return value;
			}
			return null;
		}

		public override void Dispose()
		{
			AppDomain.CurrentDomain.AssemblyResolve -= ResolveAssembly;
		}
	}
}
