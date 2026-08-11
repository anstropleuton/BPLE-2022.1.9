using System;
using Newtonsoft.Json;

namespace Innovation
{
	[Serializable]
	[JsonConverter(typeof(JsonAliasConverter<RendererTemplate>))]
	public class RendererTemplate : ComponentTemplate
	{
		[JsonAlias("着色器")]
		public string Shader { get; set; }

		[JsonAlias("贴图")]
		public string Texture { get; set; }

		[JsonAlias("颜色")]
		public HexColor Color { get; set; }

		public RendererTemplate()
		{
			Color = HexColor.White;
		}
	}
}
