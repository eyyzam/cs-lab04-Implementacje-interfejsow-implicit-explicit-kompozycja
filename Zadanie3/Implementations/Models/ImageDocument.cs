using Zadanie3.Implementations.Interfaces;

namespace Zadanie3.Implementations.Models
{
	public class ImageDocument : AbstractDocument
	{
		public ImageDocument(string fileName) : base(fileName) {}

		public override IDocument.FormatType GetFormatType() => IDocument.FormatType.JPG;
	}
}
