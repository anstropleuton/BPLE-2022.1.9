using System;
using Newtonsoft.Json;
using UnityEngine;

namespace Innovation
{
	[Serializable]
	[JsonConverter(typeof(JsonAliasConverter<ColliderTemplate>))]
	public class ColliderTemplate : ComponentTemplate
	{
		[JsonAlias("类型")]
		public ColliderTypeCode Type { get; set; }

		[JsonAlias("中心")]
		public Vector3 Center { get; set; }

		[JsonAlias("大小")]
		public Vector3 Size { get; set; }

		[JsonAlias("半径")]
		public float Radius { get; set; }

		[JsonAlias("高度")]
		public float Height { get; set; }

		[JsonAlias("弹性系数")]
		public float Bounciness { get; set; }

		[JsonAlias("动摩擦系数")]
		public float DynamicFriction { get; set; }

		[JsonAlias("静摩擦系数")]
		public float StaticFriction { get; set; }

		[JsonAlias("弹性组合模式")]
		public PhysicMaterialCombine BounceCombine { get; set; }

		[JsonAlias("摩擦组合模式")]
		public PhysicMaterialCombine FrictionCombine { get; set; }
	}
}
