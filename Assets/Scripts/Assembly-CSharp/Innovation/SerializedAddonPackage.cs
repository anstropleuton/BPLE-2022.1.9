using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Innovation
{
	[Serializable]
	[JsonConverter(typeof(JsonAliasConverter<SerializedAddonPackage>))]
	public class SerializedAddonPackage
	{
		public enum ResourceKind
		{
			Script,
			BinaryAsset,
			TextAsset,
			Texture,
			AudioClip
		}

		[Serializable]
		public class ResourceData
		{
			[JsonAlias("类型")]
			public ResourceKind Kind { get; set; }

			[JsonAlias("名称")]
			public string Name { get; set; }

			[JsonAlias("路径")]
			public string Path { get; set; }
		}

		[JsonAlias("ID")]
		public string ID { get; set; }

		[JsonAlias("名称")]
		public string Name { get; set; }

		[JsonAlias("开发者")]
		public string Developer { get; set; }

		[JsonAlias("版本")]
		public Version Version { get; set; }

		[JsonAlias("类型")]
		public AddonPackageKind Kind { get; set; }

		[JsonAlias("脚本入口点")]
		public string EntryPoint { get; set; }

		[JsonAlias("资源列表")]
		public List<ResourceData> Resources { get; set; }
	}
}
