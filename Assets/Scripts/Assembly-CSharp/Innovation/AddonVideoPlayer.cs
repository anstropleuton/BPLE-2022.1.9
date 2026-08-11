using UnityEngine.Video;

namespace Innovation
{
	public class AddonVideoPlayer : AddonGraphic
	{
		private VideoPlayer m_player;

		public VideoPlayer Player
		{
			get
			{
				UpdateComponent(ref m_player, force: false);
				return m_player;
			}
		}
	}
}
