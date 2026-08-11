using System;
using Newtonsoft.Json;

namespace Innovation
{
	[Serializable]
	[JsonConverter(typeof(JsonAliasConverter<CustomPartTemplate>))]
	public class CustomPartTemplate : GameObjectTemplate
	{
		[JsonAlias("Unity类型")]
		public string UnityType { get; set; }

		[JsonAlias("部件类型")]
		public PartTypeCode PartType { get; set; }

		[JsonAlias("部件材质类型")]
		public PartTierCode PartTier { get; set; }

		[JsonAlias("部件材质序号")]
		public int PartIndex { get; set; }

		[JsonAlias("基础部件类型")]
		public PartTypeCode UnderlyingPartType { get; set; }

		[JsonAlias("基础部件材质序号")]
		public int UnderlyingPartIndex { get; set; }

		[JsonAlias("质量")]
		public float Mass { get; set; }

		[JsonAlias("动力消耗值")]
		public float PowerConsumption { get; set; }

		[JsonAlias("动力值")]
		public float EnginePower { get; set; }

		[JsonAlias("图标渲染器模板")]
		public RendererTemplate IconRendererTemplate { get; set; }
	}
}
