using UnityEngine;

namespace Innovation
{
	public abstract class InjectionPart : IInjectionPart
	{
		public IBasePart BasePart { get; set; }

		public virtual void Awake()
		{
		}

		public virtual void Start()
		{
		}

		public virtual void FixedUpdate()
		{
		}

		public virtual void Update()
		{
		}

		public virtual void PrePlaced()
		{
		}

		public virtual void EnsureRigidbody()
		{
		}

		public virtual void Initialize()
		{
		}

		public virtual void InitializeEngine()
		{
		}

		public virtual void PostInitialize()
		{
		}

		public virtual void MoveTo(int x, int y)
		{
		}

		public virtual void RotateTo(int rotation, bool flipped)
		{
		}

		public virtual bool CanBeEnabled()
		{
			return false;
		}

		public virtual bool CanEncloseParts()
		{
			return false;
		}

		public virtual bool CanBeEnclosed()
		{
			return false;
		}

		public virtual bool HasOnOffToggle()
		{
			return false;
		}

		public virtual bool IsEnabled()
		{
			return false;
		}

		public virtual void SetEnabled(bool enabled)
		{
		}

		public virtual int EffectDirection()
		{
			return 0;
		}

		public virtual Joint CustomConnectToPart(IBasePart part)
		{
			return null;
		}

		public virtual void ProcessTouch()
		{
		}
	}
}
