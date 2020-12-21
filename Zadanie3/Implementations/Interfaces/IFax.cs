using Zadanie3.Implementations.Interfaces;

namespace Zadanie3.Implementations.Interfaces
{
	public interface IFax : IDevice
	{
		void Fax(in IDocument document, string faxNumber);
	}
}
