using System;
using Zadanie3.Implementations.Interfaces;

namespace Zadanie3.Implementations.Models
{
	public class MultiDimensionalDevice : Copier
	{
		private readonly FaxDevice _fax;

		public int FaxCounter => _fax.FaxCounter;

		public MultiDimensionalDevice()
		{
			_fax = new FaxDevice();
		}

		public void Fax(in IDocument document, string faxNumber)
		{
			if (string.IsNullOrEmpty(faxNumber))
				throw new ArgumentNullException();

			if (State != IDevice.State.On) return;

			_scanner.PowerOn();
			_scanner.Scan(out _, document.GetFormatType());
			ScanCounter += _scanner.ScanCounter;
			_scanner.PowerOff();

			_fax.PowerOn();
			_fax.Fax(document, faxNumber);
			_fax.PowerOff();
		}
	}
}
