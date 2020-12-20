using CopierProject.Implementations.Interfaces;

namespace CopierProject.Implementations.Models
{
	public abstract class AbstractDocument : IDocument
	{
		private string FileName;

		protected AbstractDocument(string fileName) => FileName = fileName;

		public string GetFileName() => FileName;

		public void ChangeFileName(string newFileName) => FileName = newFileName;

		public abstract IDocument.FormatType GetFormatType();
	}
}
