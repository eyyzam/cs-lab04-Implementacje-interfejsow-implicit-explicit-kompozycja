using System;
using Zadanie3.Implementations.Interfaces;
using Zadanie3.Implementations.Models;

namespace Zadanie3
{
	public class Program
	{
		private static void Main()
		{
			var device = new Copier();
			Console.WriteLine($@"Copier default counter: {device.Counter}");
			Console.WriteLine($@"Copier default print counter: {device.PrintCounter}");
			Console.WriteLine($@"Copier default state: {device.GetState()}");
			
			device.PowerOn();
			Console.WriteLine($@"Copier state after turning power on: {device.GetState()}");

			Console.WriteLine("PDF Print (doc1)");
			IDocument doc1 = new PDFDocument("doc1");
			device.Print(doc1);

			Console.WriteLine("PDF Scan (doc1)");
			device.Scan(doc1.GetFormatType());

			Console.WriteLine($@"Copier counter: {device.Counter}");
			Console.WriteLine($@"Copier print counter: {device.PrintCounter}");
			Console.WriteLine($@"Copier scan counter: {device.ScanCounter}");

			Console.WriteLine($@"Copier Scanning and printing (doc2)");
			IDocument doc2 = new TextDocument("doc2");
			device.ScanAndPrint(doc2.GetFormatType());

			Console.WriteLine($@"Copier print counter: {device.PrintCounter}");
			Console.WriteLine($@"Copier scan counter: {device.ScanCounter}");

			Console.ReadLine();
		}
	}
}
