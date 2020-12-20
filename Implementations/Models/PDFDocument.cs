using CopierProject.Implementations.Interfaces;

namespace CopierProject.Implementations.Models
{
	public class PDFDocument : AbstractDocument
	{
		public PDFDocument(string fileName) : base(fileName) {}

		public override IDocument.FormatType GetFormatType() => IDocument.FormatType.PDF;
	}
}
