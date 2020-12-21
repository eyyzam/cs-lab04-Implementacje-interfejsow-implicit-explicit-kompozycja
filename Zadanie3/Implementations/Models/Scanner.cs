using System;
using Zadanie3.Implementations.Interfaces;

namespace Zadanie3.Implementations.Models
{
	public class Scanner : IScanner
	{
		public IDevice.State State { get; private set; } = IDevice.State.Off;

		public int Counter { get; private set; }

		public int ScanCounter { get; private set; }

		public IDevice.State GetState() => State;

		public void PowerOff()
		{
			if (State != IDevice.State.On) return;

			State = IDevice.State.Off;
			Console.WriteLine("Scanner turned Off");
		}

		public void PowerOn()
		{
			if (State != IDevice.State.Off) return;

			State = IDevice.State.On;
			Console.WriteLine("Scanner turned On");
			Counter++;
		}

		public void Scan(out IDocument document, IDocument.FormatType formatType = IDocument.FormatType.JPG)
		{
			document = null;
			if (State != IDevice.State.On) return;

			string fileName;
			switch (formatType)
			{
				case IDocument.FormatType.JPG:
					fileName = $@"ImageScan{++ScanCounter}.jpg";
					document = new ImageDocument(fileName);
					break;
				case IDocument.FormatType.PDF:
					fileName = $@"PDFScan{++ScanCounter}.pdf";
					document = new PDFDocument(fileName);
					break;
				case IDocument.FormatType.TXT:
					fileName = $@"TextScan{++ScanCounter}.txt";
					document = new TextDocument(fileName);
					break;
				default:
					throw new ArgumentOutOfRangeException(nameof(formatType), formatType, null);
			}
			Console.WriteLine($@"{DateTime.Now} Scan: {fileName}");
		}
	}
}
