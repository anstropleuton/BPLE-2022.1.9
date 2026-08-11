using UnityEngine;

namespace Innovation
{
	public interface IFramePart : IBasePart
	{
		Color Color { get; set; }

		bool IsColoredFrame();
	}
}
