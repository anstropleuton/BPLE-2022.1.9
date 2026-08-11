using Newtonsoft.Json;
using UnityEngine;

namespace Innovation
{
	[JsonConverter(typeof(JsonAliasConverter<RigidbodyTemplate>))]
	public class RigidbodyTemplate : ComponentTemplate
	{
		[JsonAlias("质量")]
		public float Mass { get; set; }

		[JsonAlias("阻力系数")]
		public float Drag { get; set; }

		[JsonAlias("角阻力系数")]
		public float AngularDrag { get; set; }

		[JsonAlias("启用重力")]
		public bool UseGravity { get; set; }

		[JsonAlias("关闭动力学模拟")]
		public bool IsKinematic { get; set; }

		[JsonAlias("插值")]
		public RigidbodyInterpolation Interpolation { get; set; }

		[JsonAlias("碰撞检测模式")]
		public CollisionDetectionMode CollisionDetectionMode { get; set; }

		[JsonAlias("约束")]
		public RigidbodyConstraints Constraints { get; set; }
	}
}
