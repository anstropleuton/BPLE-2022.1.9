using UnityEngine;

namespace Innovation
{
	public class AddonMusicPlayer : AddonGraphic
	{
		private AudioSource m_source;

		public AudioSource Source
		{
			get
			{
				UpdateComponent(ref m_source, force: false);
				return m_source;
			}
		}
	}
}
