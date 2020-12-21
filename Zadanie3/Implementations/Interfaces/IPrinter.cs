namespace Zadanie3.Implementations.Interfaces
{
	public interface IPrinter
	{
		/// <summary>
		/// Drukowany dokument - jeśli urządzenie jest uruchomione.
		/// </summary>
		/// <param name="document">IDocument - nie może być 'null'</param>
		void Print(in IDocument document);
	}
}