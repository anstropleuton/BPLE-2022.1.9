using UnityEngine;

namespace Innovation
{
	internal static class UnityExtensions
	{
		internal static T AddOrGetComponent<T>(this GameObject gameObject) where T : Component
		{
			T val = gameObject.GetComponent<T>();
			if (val == null)
			{
				val = gameObject.AddComponent<T>();
			}
			return val;
		}
	}
}
