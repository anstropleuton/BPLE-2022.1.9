using Newtonsoft.Json;
using UnityEngine;

namespace Innovation
{
	[JsonConverter(typeof(JsonAliasConverter<TransformTemplate>))]
	public class TransformTemplate : ComponentTemplate
	{
		[JsonAlias("位置")]
		public Vector3 LocalPosition { get; set; }

		[JsonAlias("旋转")]
		public Quaternion LocalRotation { get; set; }

		[JsonAlias("缩放")]
		public Vector3 LocalScale { get; set; }

		public TransformTemplate()
		{
			LocalPosition = Vector3.zero;
			LocalRotation = Quaternion.identity;
			LocalScale = Vector3.one;
		}
	}
}
