using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Zadanie3.Implementations.Interfaces;
using Zadanie3.Implementations.Models;

namespace Zadanie3UnitTests
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
    public class UnitTestMultiDimensionalDevice
    {
        [TestMethod]
        public void MultiDimensional_GetState_Default()
        {
            var device = new MultiDimensionalDevice();

            Assert.AreEqual(IDevice.State.Off, device.GetState());
        }

        [TestMethod]
        public void MultiDimensional_GetState_StateOff()
        {
            var device = new MultiDimensionalDevice();
            device.PowerOff();

            Assert.AreEqual(IDevice.State.Off, device.GetState());
        }

        [TestMethod]
        public void MultiDimensional_GetState_StateOn()
        {
            var device = new MultiDimensionalDevice();
            device.PowerOn();

            Assert.AreEqual(IDevice.State.On, device.GetState());
        }

        [TestMethod]
        public void MultiDimensional_Print_DeviceOn()
        {
            var device = new MultiDimensionalDevice();
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
        public void MultiDimensional_Print_DeviceOff()
        {
            var device = new MultiDimensionalDevice();
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
        public void MultiDimensional_Scan_DeviceOff()
        {
            var device = new MultiDimensionalDevice();
            device.PowerOff();

            var currentConsoleOut = Console.Out;
            currentConsoleOut.Flush();

            IDocument doc = new PDFDocument("aaa.pdf");

            using (var consoleOutput = new ConsoleRedirectionToStringWriter())
            {
                device.Scan(doc.GetFormatType());
                Assert.IsFalse(consoleOutput.GetOutput().Contains("Scan"));
            }
            Assert.AreEqual(currentConsoleOut, Console.Out);
        }

        [TestMethod]
        public void MultiDimensional_Scan_DeviceOn()
        {
            var device = new MultiDimensionalDevice();
            device.PowerOn();

            var currentConsoleOut = Console.Out;
            currentConsoleOut.Flush();

            IDocument doc = new PDFDocument("aaa.pdf");

            using (var consoleOutput = new ConsoleRedirectionToStringWriter())
            {
                device.Scan(doc.GetFormatType());
                Assert.IsTrue(consoleOutput.GetOutput().Contains("Scan"));
            }
            Assert.AreEqual(currentConsoleOut, Console.Out);
        }

        [TestMethod]
        public void MultiDimensional_Scan_FormatTypeDocument()
        {
            var device = new MultiDimensionalDevice();
            device.PowerOn();

            var currentConsoleOut = Console.Out;
            currentConsoleOut.Flush();

            IDocument JPG_doc = new ImageDocument("image");
            IDocument TXT_doc = new TextDocument("text");
            IDocument PDF_doc = new PDFDocument("pdf");

            using (var consoleOutput = new ConsoleRedirectionToStringWriter())
            {
                device.Scan(JPG_doc.GetFormatType());
                Assert.IsTrue(consoleOutput.GetOutput().Contains("Scan"));
                Assert.IsTrue(consoleOutput.GetOutput().Contains(".jpg"));

                device.Scan(TXT_doc.GetFormatType());
                Assert.IsTrue(consoleOutput.GetOutput().Contains("Scan"));
                Assert.IsTrue(consoleOutput.GetOutput().Contains(".txt"));

                device.Scan(PDF_doc.GetFormatType());
                Assert.IsTrue(consoleOutput.GetOutput().Contains("Scan"));
                Assert.IsTrue(consoleOutput.GetOutput().Contains(".pdf"));
            }
            Assert.AreEqual(currentConsoleOut, Console.Out);
        }

        [TestMethod]
        public void MultiDimensional_ScanAndPrint_DeviceOn()
        {
            var device = new MultiDimensionalDevice();
            device.PowerOn();

            var currentConsoleOut = Console.Out;
            currentConsoleOut.Flush();

            IDocument JPG_doc = new ImageDocument("image");

            using (var consoleOutput = new ConsoleRedirectionToStringWriter())
            {
                device.ScanAndPrint(JPG_doc.GetFormatType());
                Assert.IsTrue(consoleOutput.GetOutput().Contains("Scan"));
                Assert.IsTrue(consoleOutput.GetOutput().Contains("Print"));
            }
            Assert.AreEqual(currentConsoleOut, Console.Out);
        }

        [TestMethod]
        public void MultiDimensional_ScanAndPrint_DeviceOff()
        {
            var device = new MultiDimensionalDevice();
            device.PowerOff();

            var currentConsoleOut = Console.Out;
            currentConsoleOut.Flush();

            IDocument PDF_doc = new PDFDocument("pdf");

            using (var consoleOutput = new ConsoleRedirectionToStringWriter())
            {
                device.ScanAndPrint(PDF_doc.GetFormatType());
                Assert.IsFalse(consoleOutput.GetOutput().Contains("Scan"));
                Assert.IsFalse(consoleOutput.GetOutput().Contains("Print"));
            }
            Assert.AreEqual(currentConsoleOut, Console.Out);
        }

        [TestMethod]
        public void MultiDimensional_PrintCounter()
        {
            var device = new MultiDimensionalDevice();
            device.PowerOn();

            IDocument doc1 = new PDFDocument("aaa.pdf");
            device.Print(in doc1);
            IDocument doc2 = new TextDocument("aaa.txt");
            device.Print(in doc2);
            IDocument doc3 = new ImageDocument("aaa.jpg");
            device.Print(in doc3);

            device.PowerOff();
            device.Print(in doc3);
            device.Scan(doc1.GetFormatType());
            device.PowerOn();

            device.ScanAndPrint(doc2.GetFormatType());
            device.ScanAndPrint(doc3.GetFormatType());

            Assert.AreEqual(15, device.PrintCounter);
        }

        [TestMethod]
        public void MultiDimensional_ScanCounter()
        {
            var device = new MultiDimensionalDevice();
            device.PowerOn();

            device.Scan(IDocument.FormatType.PDF);
            device.Scan(IDocument.FormatType.JPG);

            IDocument doc3 = new ImageDocument("aaa.jpg");
            device.Print(in doc3);

            device.PowerOff();
            device.Print(in doc3);
            device.Scan(IDocument.FormatType.TXT);
            device.PowerOn();

            device.ScanAndPrint(IDocument.FormatType.PDF);
            device.ScanAndPrint(IDocument.FormatType.TXT);

            Assert.AreEqual(10, device.ScanCounter);
        }

        [TestMethod]
        public void MultiDimensional_PowerOnCounter()
        {
            var device = new MultiDimensionalDevice();
            device.PowerOn();
            device.PowerOn();
            device.PowerOn();

            device.Scan(IDocument.FormatType.JPG);
            device.Scan(IDocument.FormatType.PDF);

            device.PowerOff();
            device.PowerOff();
            device.PowerOff();
            device.PowerOn();

            IDocument doc3 = new ImageDocument("aaa.jpg");
            device.Print(in doc3);

            device.PowerOff();
            device.Print(in doc3);
            device.Scan(IDocument.FormatType.PDF);
            device.PowerOn();

            device.ScanAndPrint(IDocument.FormatType.JPG);
            device.ScanAndPrint(IDocument.FormatType.PDF);

            Assert.AreEqual(3, device.Counter);
        }

        [TestMethod]
        public void MultiDimensional_Fax_DeviceOn()
        {
            var device = new MultiDimensionalDevice();
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
        public void MultiDimensional_Fax_DeviceOff()
        {
            var device = new MultiDimensionalDevice();
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
    }
}
