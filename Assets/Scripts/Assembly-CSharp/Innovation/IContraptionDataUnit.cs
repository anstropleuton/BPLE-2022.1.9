namespace Innovation
{
	public interface IContraptionDataUnit
	{
		int Type { get; }

		int Index { get; }

		int X { get; }

		int Y { get; }

		int Rotation { get; }

		bool Flipped { get; }
	}
}
