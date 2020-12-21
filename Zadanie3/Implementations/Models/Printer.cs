using System;
using Zadanie3.Implementations.Interfaces;

namespace Zadanie3.Implementations.Models
{
	public class Printer : IPrinter
	{
		public IDevice.State State { get; private set; } = IDevice.State.Off;

		public int Counter { get; private set; }

		public int PrintCounter { get; private set; }

		public IDevice.State GetState() => State;

		public void PowerOff()
		{
			if (State != IDevice.State.On) return;

			State = IDevice.State.Off;
			Console.WriteLine("Printer turned Off");
		}

		public void PowerOn()
		{
			if (State != IDevice.State.Off) return;

			State = IDevice.State.On;
			Console.WriteLine("Printer turned On");
			Counter++;
		}

		public void Print(in IDocument document)
		{
			if (State != IDevice.State.On) return;

			Console.WriteLine($@"{DateTime.Now} Print: {document.GetFileName()}");
			PrintCounter++;
		}
	}
}
