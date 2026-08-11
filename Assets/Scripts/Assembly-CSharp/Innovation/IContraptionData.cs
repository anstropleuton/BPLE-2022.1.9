using System.Collections.Generic;

namespace Innovation
{
	public interface IContraptionData
	{
		IReadOnlyList<IContraptionDataUnit> Units { get; }
	}
}
