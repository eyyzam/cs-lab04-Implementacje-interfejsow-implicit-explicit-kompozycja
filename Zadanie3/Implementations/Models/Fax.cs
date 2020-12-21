using System;
using System.Collections.Generic;
using Zadanie3.Implementations.Interfaces;

namespace Zadanie3.Implementations.Models
{
	public class FaxDevice : IFax
	{
		public IDevice.State State { get; private set; } = IDevice.State.Off;

		public int Counter { get; private set; }

		public int FaxCounter { get; private set; }

		public IDevice.State GetState() => State;

		public List<string> RecipientList { get; }

		public FaxDevice() => RecipientList = new List<string>();

		public void PowerOff()
		{
			if (State != IDevice.State.On) return;

			State = IDevice.State.Off;
			Console.WriteLine("Fax turned Off");
		}

		public void PowerOn()
		{
			if (State != IDevice.State.Off) return;

			State = IDevice.State.On;
			Console.WriteLine("Fax turned On");
			Counter++;
		}

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
	}
}
