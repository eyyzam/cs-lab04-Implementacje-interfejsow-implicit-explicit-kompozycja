using Zadanie3.Implementations.Interfaces;

namespace Zadanie3.Implementations.Models
{
	public class TextDocument : AbstractDocument
	{
		public TextDocument(string fileName) : base(fileName) {}

		public override IDocument.FormatType GetFormatType() => IDocument.FormatType.TXT;
	}
}
