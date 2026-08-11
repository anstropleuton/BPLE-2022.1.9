using System.Collections.Generic;

namespace Innovation
{
	public interface IPartService
	{
		IReadOnlyList<IBasePart> GetAllParts();

		IReadOnlyList<IBasePart> GetAllRuntimeParts();

		IBasePart SelectPart(int x, int y, PartTypeCode partType, int partIndex);

		IReadOnlyList<IBasePart> SelectParts(int x, int y, int width, int height, PartTypeCode partType, int partIndex);

		IReadOnlyList<IBasePart> InvertSelection(IReadOnlyList<IBasePart> parts);

		IBasePart SetPart(int x, int y, PartTypeCode partType, int partIndex);

		IReadOnlyList<IBasePart> SetParts(int x, int y, int width, int height, PartTypeCode partType, int partIndex);

		IReadOnlyList<IBasePart> SetPartsInterval(int x, int y, int width, int height, int deltaX, int deltaY, PartTypeCode partType, int partIndex);

		void MoveParts(IReadOnlyList<IBasePart> parts, int x, int y);

		void RotateParts(IReadOnlyList<IBasePart> parts, int times);

		IReadOnlyList<IBasePart> CopyParts(IReadOnlyList<IBasePart> parts, int x, int y);

		IReadOnlyList<IBasePart> ReplaceParts(IReadOnlyList<IBasePart> parts, PartTypeCode partType, int partIndex);

		void RemoveParts(IReadOnlyList<IBasePart> parts);

		IBasePart SetRuntimePart(int x, int y, int rotation, bool flipped, PartTypeCode partType, int partIndex);

		string GetContraptionName();

		void SaveContraption();

		void MoveContraption(int x, int y);

		IContraptionData CopyContraption();

		void PasteContraption(IContraptionData data, int x, int y, bool absolute);
	}
}
