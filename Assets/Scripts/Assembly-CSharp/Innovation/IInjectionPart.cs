using UnityEngine;

namespace Innovation
{
	public interface IInjectionPart
	{
		IBasePart BasePart { get; set; }

		void Awake()
		{
		}

		void Start()
		{
		}

		void FixedUpdate()
		{
		}

		void Update()
		{
		}

		void PrePlaced()
		{
		}

		void EnsureRigidbody()
		{
		}

		void Initialize()
		{
		}

		void InitializeEngine()
		{
		}

		void PostInitialize()
		{
		}

		void MoveTo(int x, int y)
		{
		}

		void RotateTo(int rotation, bool flipped)
		{
		}

		bool CanBeEnabled()
		{
			return false;
		}

		bool CanEncloseParts()
		{
			return false;
		}

		bool CanBeEnclosed()
		{
			return false;
		}

		bool HasOnOffToggle()
		{
			return false;
		}

		bool IsEnabled()
		{
			return false;
		}

		void SetEnabled(bool enabled)
		{
		}

		int EffectDirection()
		{
			return 0;
		}

		Joint CustomConnectToPart(IBasePart part)
		{
			return null;
		}

		void ProcessTouch()
		{
		}
	}
}
