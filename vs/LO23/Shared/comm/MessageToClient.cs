namespace Shared.comm
{
	abstract class MessageToClient
	{
		/// <summary>
		/// Operates the process of the message.
		/// </summary>
		public abstract void handle();
	}
}
