using Microsoft.CodeAnalysis;

namespace Innovation.Script
{
	public abstract class AssemblyReference
	{
		internal abstract MetadataReference MetadataReference { get; }
	}
}
