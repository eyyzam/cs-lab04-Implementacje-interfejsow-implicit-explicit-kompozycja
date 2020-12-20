using System;
using System.IO;
using CopierProject.Implementations.Interfaces;
using CopierProject.Implementations.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Zadanie2.Implementations.Models;

namespace Zadanie2UnitTests
{
	public class ConsoleRedirectionToStringWriter : IDisposable
	{
		private readonly StringWriter stringWriter;
		private readonly TextWriter originalOutput;

		public ConsoleRedirectionToStringWriter()
		{
			stringWriter = new StringWriter();
			originalOutput = Console.Out;
			Console.SetOut(stringWriter);
		}

		public string GetOutput()
		{
			return stringWriter.ToString();
		}

		public void Dispose()
		{
			Console.SetOut(originalOutput);
			stringWriter.Dispose();
		}
	}

	[TestClass]
	public class UnitTestMultiFunctionalDevice
	{
		[TestMethod]
		public void MultiFunctional_GetState_Default()
		{
			var device = new MultiFunctionalDevice();

			Assert.AreEqual(IDevice.State.Off, device.GetState());
		}

		[TestMethod]
		public void MultiFunctional_GetState_StateOff()
		{
			var device = new MultiFunctionalDevice();
			device.PowerOff();

			Assert.AreEqual(IDevice.State.Off, device.GetState());
		}

		[TestMethod]
		public void MultiFunctional_GetState_StateOn()
		{
			var device = new MultiFunctionalDevice();
			device.PowerOn();

			Assert.AreEqual(IDevice.State.On, device.GetState());
		}

		[TestMethod]
		public void MultiFunctional_Print_DeviceOn()
		{
			var device = new MultiFunctionalDevice();
			device.PowerOn();

			var currentConsoleOut = Console.Out;
			currentConsoleOut.Flush();

			using (var consoleOutput = new ConsoleRedirectionToStringWriter())
			{
				IDocument doc1 = new PDFDocument("aaa.pdf");
				device.Print(in doc1);
				Assert.IsTrue(consoleOutput.GetOutput().Contains("Print"));
			}
			Assert.AreEqual(currentConsoleOut, Console.Out);
		}

		[TestMethod]
		public void MultiFunctional_Print_DeviceOff()
		{
			var device = new MultiFunctionalDevice();
			device.PowerOff();

			var currentConsoleOut = Console.Out;
			currentConsoleOut.Flush();

			using (var consoleOutput = new ConsoleRedirectionToStringWriter())
			{
				IDocument doc1 = new PDFDocument("aaa.pdf");
				device.Print(in doc1);
				Assert.IsFalse(consoleOutput.GetOutput().Contains("Print"));
			}
			Assert.AreEqual(currentConsoleOut, Console.Out);
		}

        [TestMethod]
        public void MultiFunctional_Scan_DeviceOff()
        {
            var device = new MultiFunctionalDevice();
            device.PowerOff();

            var currentConsoleOut = Console.Out;
            currentConsoleOut.Flush();

            using (var consoleOutput = new ConsoleRedirectionToStringWriter())
            {
                device.Scan(out _);
                Assert.IsFalse(consoleOutput.GetOutput().Contains("Scan"));
            }
            Assert.AreEqual(currentConsoleOut, Console.Out);
        }

        [TestMethod]
        public void MultiFunctional_Scan_DeviceOn()
        {
            var device = new MultiFunctionalDevice();
            device.PowerOn();

            var currentConsoleOut = Console.Out;
            currentConsoleOut.Flush();

            using (var consoleOutput = new ConsoleRedirectionToStringWriter())
            {
                device.Scan(out _);
                Assert.IsTrue(consoleOutput.GetOutput().Contains("Scan"));
            }
            Assert.AreEqual(currentConsoleOut, Console.Out);
        }

        [TestMethod]
        public void MultiFunctional_Scan_FormatTypeDocument()
        {
            var device = new MultiFunctionalDevice();
            device.PowerOn();

            var currentConsoleOut = Console.Out;
            currentConsoleOut.Flush();

            using (var consoleOutput = new ConsoleRedirectionToStringWriter())
            {
                device.Scan(out _);
                Assert.IsTrue(consoleOutput.GetOutput().Contains("Scan"));
                Assert.IsTrue(consoleOutput.GetOutput().Contains(".jpg"));

                device.Scan(out _, IDocument.FormatType.TXT);
                Assert.IsTrue(consoleOutput.GetOutput().Contains("Scan"));
                Assert.IsTrue(consoleOutput.GetOutput().Contains(".txt"));

                device.Scan(out _, IDocument.FormatType.PDF);
                Assert.IsTrue(consoleOutput.GetOutput().Contains("Scan"));
                Assert.IsTrue(consoleOutput.GetOutput().Contains(".pdf"));
            }
            Assert.AreEqual(currentConsoleOut, Console.Out);
        }

        [TestMethod]
        public void MultiFunctional_ScanAndPrint_DeviceOn()
        {
            var device = new MultiFunctionalDevice();
            device.PowerOn();

            var currentConsoleOut = Console.Out;
            currentConsoleOut.Flush();

            using (var consoleOutput = new ConsoleRedirectionToStringWriter())
            {
                device.ScanAndPrint();
                Assert.IsTrue(consoleOutput.GetOutput().Contains("Scan"));
                Assert.IsTrue(consoleOutput.GetOutput().Contains("Print"));
            }
            Assert.AreEqual(currentConsoleOut, Console.Out);
        }

        [TestMethod]
        public void MultiFunctional_ScanAndPrint_DeviceOff()
        {
            var device = new MultiFunctionalDevice();
            device.PowerOff();

            var currentConsoleOut = Console.Out;
            currentConsoleOut.Flush();

            using (var consoleOutput = new ConsoleRedirectionToStringWriter())
            {
	            device.ScanAndPrint();
                Assert.IsFalse(consoleOutput.GetOutput().Contains("Scan"));
                Assert.IsFalse(consoleOutput.GetOutput().Contains("Print"));
            }
            Assert.AreEqual(currentConsoleOut, Console.Out);
        }

        [TestMethod]
        public void MultiFunctional_PrintCounter()
        {
            var device = new MultiFunctionalDevice();
            device.PowerOn();

            IDocument doc1 = new PDFDocument("aaa.pdf");
            device.Print(in doc1);
            IDocument doc2 = new TextDocument("aaa.txt");
            device.Print(in doc2);
            IDocument doc3 = new ImageDocument("aaa.jpg");
            device.Print(in doc3);

            device.PowerOff();
            device.Print(in doc3);
            device.Scan(out doc1);
            device.PowerOn();

            device.ScanAndPrint();
            device.ScanAndPrint();

            Assert.AreEqual(5, device.PrintCounter);
        }

        [TestMethod]
        public void MultiFunctional_ScanCounter()
        {
            var device = new MultiFunctionalDevice();
            device.PowerOn();

            device.Scan(out _);
            device.Scan(out _);

            IDocument doc3 = new ImageDocument("aaa.jpg");
            device.Print(in doc3);

            device.PowerOff();
            device.Print(in doc3);
            device.Scan(out _);
            device.PowerOn();

            device.ScanAndPrint();
            device.ScanAndPrint();

            Assert.AreEqual(4, device.ScanCounter);
        }

        [TestMethod]
        public void MultiFunctional_PowerOnCounter()
        {
            var device = new MultiFunctionalDevice();
            device.PowerOn();
            device.PowerOn();
            device.PowerOn();

            device.Scan(out _);
            device.Scan(out _);

            device.PowerOff();
            device.PowerOff();
            device.PowerOff();
            device.PowerOn();

            IDocument doc3 = new ImageDocument("aaa.jpg");
            device.Print(in doc3);

            device.PowerOff();
            device.Print(in doc3);
            device.Scan(out _);
            device.PowerOn();

            device.ScanAndPrint();
            device.ScanAndPrint();

            Assert.AreEqual(3, device.Counter);
        }

        [TestMethod]
        public void MultiFunctional_Fax_DeviceOn()
        {
            var device = new MultiFunctionalDevice();
            device.PowerOn();

            var currentConsoleOut = Console.Out;
            currentConsoleOut.Flush();

            IDocument document = new ImageDocument("obrazek");

            using (var consoleOutput = new ConsoleRedirectionToStringWriter())
            {
                device.Fax(document, "+48515266034");
                Assert.IsTrue(consoleOutput.GetOutput().Contains("Fax:"));
            }

            Assert.AreEqual(currentConsoleOut, Console.Out);
        }

        [TestMethod]
        public void MultiFunctional_Fax_DeviceOff()
        {
	        var device = new MultiFunctionalDevice();
	        device.PowerOff();

	        var currentConsoleOut = Console.Out;
	        currentConsoleOut.Flush();

	        IDocument document = new TextDocument("essej");

            using (var consoleOutput = new ConsoleRedirectionToStringWriter())
	        {
		        device.Fax(document, "+48515266034");
		        Assert.IsFalse(consoleOutput.GetOutput().Contains("Fax:"));
	        }

	        Assert.AreEqual(currentConsoleOut, Console.Out);
        }

        [TestMethod]
        public void MultiFunctional_ScanAndSendFax_DeviceOn()
        {
	        var device = new MultiFunctionalDevice();
	        device.PowerOn();

	        var currentConsoleOut = Console.Out;
	        currentConsoleOut.Flush();

	        using (var consoleOutput = new ConsoleRedirectionToStringWriter())
	        {
		        device.ScanAndSendFax("+48515266034");
		        Assert.IsTrue(consoleOutput.GetOutput().Contains("Scan"));
		        Assert.IsTrue(consoleOutput.GetOutput().Contains("Fax:"));
	        }

	        Assert.AreEqual(currentConsoleOut, Console.Out);
        }

        [TestMethod]
        public void MultiFunctional_ScanAndSendFax_DeviceOff()
        {
	        var device = new MultiFunctionalDevice();
	        device.PowerOff();

	        var currentConsoleOut = Console.Out;
	        currentConsoleOut.Flush();

	        using (var consoleOutput = new ConsoleRedirectionToStringWriter())
	        {
		        device.ScanAndSendFax("+48515266034");
		        Assert.IsFalse(consoleOutput.GetOutput().Contains("Scan"));
		        Assert.IsFalse(consoleOutput.GetOutput().Contains("Fax:"));
	        }

	        Assert.AreEqual(currentConsoleOut, Console.Out);
        }
    }
}
