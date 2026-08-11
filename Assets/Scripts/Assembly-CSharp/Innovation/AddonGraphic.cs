using UnityEngine;

namespace Innovation
{
	public class AddonGraphic : AddonComponent
	{
		public LocationMode LocationMode { get; set; }

		private void LateUpdate()
		{
			GameObject gameObject = GameObject.FindGameObjectWithTag("MainCamera");
			if (gameObject == null)
			{
				return;
			}
			if (LocationMode == LocationMode.Camera || LocationMode == LocationMode.CameraAndScreen)
			{
				Vector3 position = gameObject.transform.position;
				base.transform.position = new Vector3(position.x, position.y, base.transform.position.z);
			}
			if (LocationMode == LocationMode.Screen || LocationMode == LocationMode.CameraAndScreen)
			{
				float num = (float)Screen.width / (float)Screen.height;
				float num2 = gameObject.GetComponent<Camera>().orthographicSize * 2f;
				Renderer renderer = base.renderer;
				Texture texture = ((renderer != null) ? renderer.material.mainTexture : null);
				if (texture != null)
				{
					float num3 = (float)texture.width / (float)texture.height;
					float num4 = Mathf.Max(num / num3, 1f);
					base.transform.localScale = new Vector3(num2 * num3 * num4, num2 * num4, 1f);
				}
				else
				{
					base.transform.localScale = new Vector3(num2 * num, num2, 1f);
				}
			}
		}
	}
}
