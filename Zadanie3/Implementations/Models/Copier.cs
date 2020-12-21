using Zadanie3.Implementations.Interfaces;

namespace Zadanie3.Implementations.Models
{
	public class Copier : BaseDevice
	{
		public int PrintCounter { get; protected set; }

		public int ScanCounter { get; protected set; }

		public new int Counter { get; protected set; }

		protected readonly Printer _printer;
		protected readonly Scanner _scanner;

		public Copier()
		{
			_printer = new Printer();
			_scanner = new Scanner();
		}

		public new void PowerOn()
		{
			if (State != IDevice.State.Off) return;

			base.PowerOn();
			Counter++;
		}

		public new void PowerOff()
		{
			if (State != IDevice.State.On) return;

			base.PowerOff();
		}

		public void Scan(IDocument.FormatType formatType)
		{
			if (State != IDevice.State.On) return;

			_scanner.PowerOn();
			_scanner.Scan(out _, formatType);
			ScanCounter += _scanner.ScanCounter;
			_scanner.PowerOff();
		}

		public void Print(IDocument document)
		{
			if (State != IDevice.State.On) return;

			_printer.PowerOn();
			_printer.Print(document);
			PrintCounter += _printer.PrintCounter;
			_printer.PowerOff();
		}

		public void ScanAndPrint(IDocument.FormatType formatType)
		{
			if (State != IDevice.State.On) return;

			_scanner.PowerOn();
			_scanner.Scan(out var document, formatType);
			ScanCounter += _scanner.ScanCounter;

			_printer.PowerOn();
			_printer.Print(document);
			PrintCounter += _printer.PrintCounter;

			_scanner.PowerOff();
			_printer.PowerOff();
		}
	}
}
