namespace CopierProject.Implementations.Interfaces
{
	public interface IScanner
	{
		/// <summary>
		/// Skanowany dokument - jeśli urządzenie jest uruchomione.
		/// </summary>
		void Scan(out IDocument document, IDocument.FormatType formatType);
	}
}