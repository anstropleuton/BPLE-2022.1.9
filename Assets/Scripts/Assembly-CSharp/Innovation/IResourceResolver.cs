using UnityEngine;

namespace Innovation
{
	public interface IResourceResolver
	{
		Texture2D ResolveTexture(string path);

		AudioClip ResolveAudio(string path);

		Shader ResolveShader(string path);
	}
}
