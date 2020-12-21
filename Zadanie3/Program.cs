using System;
using Zadanie3.Implementations.Interfaces;
using Zadanie3.Implementations.Models;

namespace Zadanie3
{
	public class Program
	{
		private static void Main()
		{
			Console.WriteLine("==========================================");
			Console.WriteLine("================ COPIER  =================");
			Console.WriteLine("==========================================");

			var device = new Copier();
			Console.WriteLine("\n" + $@"Copier default counter: {device.Counter}");
			Console.WriteLine($@"Copier default print counter: {device.PrintCounter}");
			Console.WriteLine($@"Copier default state: {device.GetState()}");
			
			device.PowerOn();
			Console.WriteLine("\n" + $@"Copier state after turning power on: {device.GetState()}");

			Console.WriteLine("\nPDF Print (doc1)");
			IDocument doc1 = new PDFDocument("doc1");
			device.Print(doc1);

			Console.WriteLine("\nPDF Scan (doc1)");
			device.Scan(doc1.GetFormatType());

			Console.WriteLine("\n" + $@"Copier counter: {device.Counter}");
			Console.WriteLine($@"Copier print counter: {device.PrintCounter}");
			Console.WriteLine($@"Copier scan counter: {device.ScanCounter}");

			Console.WriteLine("\nCopier Scanning and printing (doc2)");
			IDocument doc2 = new TextDocument("doc2");
			device.ScanAndPrint(doc2.GetFormatType());

			Console.WriteLine("\n" + $@"Copier print counter: {device.PrintCounter}");
			Console.WriteLine($@"Copier scan counter: {device.ScanCounter}");

			Console.WriteLine("\n==========================================");
			Console.WriteLine("=========== MULTI DIMENSIONAL ============");
			Console.WriteLine("==========================================");

			var multiDimensionalDevice = new MultiDimensionalDevice();
			Console.WriteLine("\n" + $@"Default state: {multiDimensionalDevice.GetState()}");

			multiDimensionalDevice.PowerOn();
			Console.WriteLine("\n" + $@"State after turning on: {multiDimensionalDevice.GetState()}");

			Console.WriteLine("\n" + $@"Default print counter: {multiDimensionalDevice.PrintCounter}");
			Console.WriteLine($@"Default scan counter: {multiDimensionalDevice.ScanCounter}");

			Console.WriteLine("\n" + $@"PDF Print (doc1)");
			multiDimensionalDevice.Print(doc1);

			Console.WriteLine($@"Text Document Scan (doc2)");
			multiDimensionalDevice.Scan(doc2.GetFormatType());

			Console.WriteLine($@"PDF Scan (doc1)");
			multiDimensionalDevice.Scan(doc1.GetFormatType());

			Console.WriteLine("\n" + $@"Print Counter: {multiDimensionalDevice.PrintCounter}");
			Console.WriteLine($@"Scan Counter: {multiDimensionalDevice.ScanCounter}");

			Console.WriteLine("\nFax (doc1) to +48515266034");
			multiDimensionalDevice.Fax(in doc1, "+48515266034");

			Console.WriteLine("\nFax (doc2) to +48502669143");
			multiDimensionalDevice.Fax(in doc2, "+48502669143");

			Console.WriteLine("\n" + $@"Counter: {multiDimensionalDevice.Counter}");
			Console.WriteLine($@"Print Counter: {multiDimensionalDevice.PrintCounter}");
			Console.WriteLine($@"Scan Counter: {multiDimensionalDevice.ScanCounter}");
			Console.WriteLine($@"Fax Counter: {multiDimensionalDevice.FaxCounter}");

			Console.ReadLine();
		}
	}
}
