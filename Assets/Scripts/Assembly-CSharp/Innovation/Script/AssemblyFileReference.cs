using Microsoft.CodeAnalysis;

namespace Innovation.Script
{
	public class AssemblyFileReference : AssemblyReference
	{
		private string m_path;

		private MetadataReference m_metadataReference;

		public string Path => m_path;

		internal override MetadataReference MetadataReference => m_metadataReference;

		public AssemblyFileReference(string path)
		{
			m_path = path;
			m_metadataReference = MetadataReference.CreateFromFile(path);
		}
	}
}
