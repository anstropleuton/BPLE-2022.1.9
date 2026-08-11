using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Innovation
{
	[Serializable]
	[JsonConverter(typeof(JsonAliasConverter<GameObjectTemplate>))]
	public class GameObjectTemplate : TemplateBase
	{
		[JsonAlias("名称")]
		public string Name { get; set; }

		[JsonAlias("层级")]
		public int Layer { get; set; }

		[JsonAlias("是否启用")]
		public bool Active { get; set; }

		[JsonAlias("子物体")]
		public List<GameObjectTemplate> Children { get; set; }

		[JsonAlias("变换模板")]
		public TransformTemplate TransformTemplate { get; set; }

		[JsonAlias("碰撞器模板")]
		public ColliderTemplate ColliderTemplate { get; set; }

		[JsonAlias("渲染器模板")]
		public RendererTemplate RendererTemplate { get; set; }

		[JsonAlias("刚体模板")]
		public RigidbodyTemplate RigidbodyTemplate { get; set; }
	}
}
