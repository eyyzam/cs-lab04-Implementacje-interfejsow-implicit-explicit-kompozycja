namespace Zadanie3.Implementations.Interfaces
{
	public interface IDocument
	{
		enum FormatType
		{
			TXT,
			PDF,
			JPG
		}

		/// <summary>
		///  Zwraca typ formatu dokumentu
		/// </summary>
		FormatType GetFormatType();

		/// <summary>
		/// Zwraca nazwę pliku dokmentu - nie może być 'null' i empty string
		/// </summary>
		string GetFileName();
	}
}
