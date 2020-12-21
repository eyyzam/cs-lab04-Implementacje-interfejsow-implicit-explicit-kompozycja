using System;
using System.Collections.Generic;
using CopierProject.Implementations.Interfaces;
using CopierProject.Implementations.Models;
using Zadanie2.Implementations.Interfaces;

namespace Zadanie2.Implementations.Models
{
	public class MultiFunctionalDevice : Copier, IFax
	{
		public int FaxCounter { get; private set; }

		public List<string> RecipientList { get; }

		public MultiFunctionalDevice() => RecipientList = new List<string>();

		public void Fax(in IDocument document, string faxNumber)
		{
			if (State != IDevice.State.On) return;

			if (string.IsNullOrEmpty(faxNumber))
				throw new ArgumentNullException();

			if (!RecipientList.Contains(faxNumber))
				RecipientList.Add(faxNumber);

			Console.WriteLine($@"{DateTime.Now} Fax: {document.GetFileName()} sent to: {faxNumber}");
			FaxCounter++;
		}

		public void ScanAndSendFax(string faxNumber)
		{
			if (string.IsNullOrEmpty(faxNumber))
				throw new ArgumentNullException();

			Scan(out var document);
			Fax(document, faxNumber);
		}
	}
}
