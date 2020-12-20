using CopierProject.Implementations.Interfaces;

namespace CopierProject.Implementations.Models
{
	public class ImageDocument : AbstractDocument
	{
		public ImageDocument(string fileName) : base(fileName) {}

		public override IDocument.FormatType GetFormatType() => IDocument.FormatType.JPG;
	}
}
