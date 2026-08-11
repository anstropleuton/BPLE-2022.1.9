using UnityEngine;

namespace Innovation
{
	public class AddonComponent : MonoBehaviour
	{
		protected Collider m_collider;

		protected Renderer m_renderer;

		public Collider collider
		{
			get
			{
				UpdateComponent(ref m_collider, force: false);
				return m_collider;
			}
		}

		public Renderer renderer
		{
			get
			{
				UpdateComponent(ref m_renderer, force: false);
				return m_renderer;
			}
		}

		public void UpdateComponents()
		{
			UpdateComponent(ref m_collider, force: true);
			UpdateComponent(ref m_renderer, force: true);
		}

		protected void UpdateComponent<T>(ref T component, bool force) where T : Component
		{
			if (force || component == null)
			{
				component = GetComponent<T>();
			}
		}
	}
}
