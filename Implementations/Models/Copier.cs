using System;
using CopierProject.Implementations.Interfaces;

namespace CopierProject.Implementations.Models
{
	public class Copier : BaseDevice, IPrinter, IScanner
	{
		public int PrintCounter { get; private set; }

		public int ScanCounter { get; private set; }

		public new int Counter { get; private set; }

		public new void PowerOn()
		{
			if (State != IDevice.State.Off) return;

			Counter++;
			base.PowerOn();
		}

		public new void PowerOff()
		{
			if (State == IDevice.State.On)
				base.PowerOff();
		}

		public void Print(in IDocument document)
		{
			if (State != IDevice.State.On) return;

			Console.WriteLine($@"{DateTime.Now} Print: {document.GetFileName()}");
			PrintCounter++;
		}

		public void Scan(out IDocument document, IDocument.FormatType formatType = IDocument.FormatType.JPG)
		{
			document = null;

			if (State != IDevice.State.On) return;
			
			string fileName;
			switch (formatType)
			{
				case IDocument.FormatType.JPG:
					fileName = $@"ImageScan{ScanCounter}.jpg";
					document = new ImageDocument(fileName);
					break;
				case IDocument.FormatType.PDF:
					fileName = $@"PDFScan{ScanCounter}.pdf";
					document = new PDFDocument(fileName);
					break;
				case IDocument.FormatType.TXT:
					fileName = $@"TextScan{ScanCounter}.txt";
					document = new TextDocument(fileName);
					break;
				default:
					throw new ArgumentOutOfRangeException(nameof(formatType), formatType, null);
			}

			ScanCounter++;
			Console.WriteLine($@"{DateTime.Now} Scan: {fileName}");
			
		}

		public void ScanAndPrint()
		{
			if (State != IDevice.State.On) return;

			Scan(out var document);
			Print(in document);
		}
	}
}
