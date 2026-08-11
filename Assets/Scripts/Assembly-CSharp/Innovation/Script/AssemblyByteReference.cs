using Microsoft.CodeAnalysis;

namespace Innovation.Script
{
	public class AssemblyByteReference : AssemblyReference
	{
		private byte[] m_bytes;

		private MetadataReference m_metadataReference;

		public byte[] Bytes => m_bytes;

		internal override MetadataReference MetadataReference => m_metadataReference;

		public AssemblyByteReference(byte[] bytes)
		{
			m_bytes = bytes;
			m_metadataReference = MetadataReference.CreateFromImage(bytes);
		}
	}
}
