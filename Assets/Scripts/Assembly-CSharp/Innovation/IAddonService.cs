using System.Collections.Generic;
using UnityEngine;

namespace Innovation
{
	public interface IAddonService
	{
		string GetAddonDataPath();

		string LoadFileAsString(string path);

		byte[] LoadFileAsBytes(string path);

		void RunScript(string code, bool printCode);

		void RunScriptFromFile(string path, bool printCode);

		Texture2D LoadTexture(byte[] data, string name);

		Texture2D LoadTextureFromFile(string path, string name);

		AudioClip LoadAudio(byte[] data, string name);

		AudioClip LoadAudioFromFile(string path, string name);

		IContraptionData LoadContraptionData(string text);

		IContraptionData LoadContraptionDataFromFile(string path);

		void SetTexturePack(Texture2D texture);

		void SetAudioPack(AudioClip audioClip, string name);

		AddonPackage FindPackage(string id);

		AddonPackage FindCurrentPackage();

		AddonComponent FindAddonComponent(string name);

		IReadOnlyList<AddonComponent> FindAddonComponents(string name);

		AddonBackground CreateBackground(Texture2D texture, Color? color, LocationMode locationMode);

		AddonVideoPlayer CreateVideoPlayer(string path, LocationMode locationMode);

		GameObject CreateGameObject(GameObjectTemplate template, IResourceResolver resolver);

		IBasePart CreateCustomPart(CustomPartTemplate template, IResourceResolver resolver);
	}
}
