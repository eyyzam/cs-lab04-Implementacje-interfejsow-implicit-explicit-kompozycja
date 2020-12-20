namespace CopierProject.Implementations.Interfaces
{
	public interface IDevice
	{
		enum State
		{
			On,
			Off
		}

		void PowerOn();

		void PowerOff();

		State GetState();

		int Counter { get; }
	}
}
