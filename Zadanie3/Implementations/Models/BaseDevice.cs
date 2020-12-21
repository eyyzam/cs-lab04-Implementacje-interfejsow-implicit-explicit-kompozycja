using System;
using Zadanie3.Implementations.Interfaces;

namespace Zadanie3.Implementations.Models
{
	public abstract class BaseDevice : IDevice
	{
		protected IDevice.State State = IDevice.State.Off;

		public IDevice.State GetState() => State;

		public int Counter { get; } = 0;

		public void PowerOff()
		{
			State = IDevice.State.Off;
			Console.WriteLine("... Device is off!");
		}

		public void PowerOn()
		{
			State = IDevice.State.On;
			Console.WriteLine("Device is on ...");
		}
	}
}
