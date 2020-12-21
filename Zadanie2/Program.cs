using System;
using CopierProject.Implementations.Interfaces;
using CopierProject.Implementations.Models;
using Zadanie2.Implementations.Models;

namespace Zadanie2
{
	public class Program
	{
		private static void Main()
		{
			var device = new MultiFunctionalDevice();
			device.PowerOn();
			IDocument document = new PDFDocument("jakis_plik.pdf");
			device.Print(in document);

			device.Scan(out _);
			device.ScanAndPrint();

			device.Fax(document, "+48515266034");
			device.ScanAndSendFax("+48515266034");

			Console.WriteLine($@"{device.Counter}");
			Console.WriteLine($@"{device.FaxCounter}");
			Console.WriteLine($@"{device.PrintCounter}");
			Console.WriteLine($@"{device.ScanCounter}");

			Console.ReadLine();
		}
	}
}
