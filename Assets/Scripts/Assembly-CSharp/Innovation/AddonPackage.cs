using System;
using System.Collections.Generic;
using UnityEngine;

namespace Innovation
{
	public class AddonPackage : IResourceResolver
	{
		public string ID { get; }

		public string Name { get; }

		public string Developer { get; }

		public Version Version { get; }

		public AddonPackageKind Kind { get; }

		public string EntryPoint { get; }

		public Dictionary<string, string> Scripts { get; }

		public Dictionary<string, byte[]> BinaryAssets { get; }

		public Dictionary<string, string> TextAssets { get; }

		public Dictionary<string, Texture2D> Textures { get; }

		public Dictionary<string, AudioClip> AudioClips { get; }

		public IAddonPackageRunner Runner { get; set; }

		public AddonPackageSettings Settings { get; set; }

		public AddonPackage(string id, string name, string developer, Version version, AddonPackageKind kind, string entryPoint)
		{
			ID = id;
			Name = name;
			Developer = developer;
			Version = version;
			Kind = kind;
			EntryPoint = entryPoint;
			Scripts = new Dictionary<string, string>();
			BinaryAssets = new Dictionary<string, byte[]>();
			TextAssets = new Dictionary<string, string>();
			Textures = new Dictionary<string, Texture2D>();
			AudioClips = new Dictionary<string, AudioClip>();
		}

		public string LoadScript(string name)
		{
			return Scripts[name];
		}

		public byte[] LoadBinaryAsset(string name)
		{
			return BinaryAssets[name];
		}

		public string LoadTextAsset(string name)
		{
			return TextAssets[name];
		}

		public Texture2D LoadTexture(string name)
		{
			return Textures[name];
		}

		public AudioClip LoadAudio(string name)
		{
			return AudioClips[name];
		}

		public GameObject CreateGameObject(GameObjectTemplate template)
		{
			return BP.CreateGameObject(template, this);
		}

		public IBasePart CreateCustomPart(CustomPartTemplate template)
		{
			return BP.CreateCustomPart(template, this);
		}

		public string 加载脚本(string name)
		{
			return LoadScript(name);
		}

		public byte[] 加载二进制资源(string name)
		{
			return LoadBinaryAsset(name);
		}

		public string 加载文本资源(string name)
		{
			return LoadTextAsset(name);
		}

		public Texture2D 加载贴图(string name)
		{
			return LoadTexture(name);
		}

		public AudioClip 加载音频(string name)
		{
			return LoadAudio(name);
		}

		public GameObject 创建游戏物体(GameObjectTemplate template)
		{
			return CreateGameObject(template);
		}

		public IBasePart 创建自定义部件(CustomPartTemplate template)
		{
			return CreateCustomPart(template);
		}

		Texture2D IResourceResolver.ResolveTexture(string path)
		{
			return LoadTexture(path);
		}

		AudioClip IResourceResolver.ResolveAudio(string path)
		{
			return LoadAudio(path);
		}

		Shader IResourceResolver.ResolveShader(string path)
		{
			return Shader.Find(path);
		}
	}
}
