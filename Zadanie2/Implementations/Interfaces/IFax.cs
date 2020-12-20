using CopierProject.Implementations.Interfaces;

namespace Zadanie2.Implementations.Interfaces
{
	public interface IFax : IDevice
	{
		void Fax(in IDocument document, string faxNumber);
	}
}
