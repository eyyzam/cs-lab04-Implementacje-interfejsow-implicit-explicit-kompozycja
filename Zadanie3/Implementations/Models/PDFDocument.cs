using Zadanie3.Implementations.Interfaces;

namespace Zadanie3.Implementations.Models
{
	public class PDFDocument : AbstractDocument
	{
		public PDFDocument(string fileName) : base(fileName) {}

		public override IDocument.FormatType GetFormatType() => IDocument.FormatType.PDF;
	}
}
