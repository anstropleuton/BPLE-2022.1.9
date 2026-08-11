using UnityEngine;

namespace Innovation
{
	public interface IBasePart
	{
		MonoBehaviour UnityObject { get; }

		PartTypeCode PartType { get; }

		PartTierCode PartTier { get; }

		int PartIndex { get; }

		int CoordX { get; }

		int CoordY { get; }

		int GridRotation { get; }

		bool Flipped { get; }

		int ConnectedComponent { get; }

		float PowerConsumption { get; set; }

		float EnginePower { get; set; }

		IBasePart EnclosedPart { get; }

		IBasePart EnclosedInto { get; }

		bool IsEnabled();

		void SetEnabled(bool enabled);

		void MoveTo(int x, int y);

		void RotateTo(int rotation, bool flipped);

		void ProcessTouch();

		void Inject(IInjectionPart part);
	}
}
