using Shared.interfaces;

namespace Shared.comm
{
	/// <summary>
	/// Message send from the client to the server.
	/// </summary>
	public abstract class MessageToClient
	{
		/// <summary>
		/// Handles the message. This has to be overridden by children classes.
		/// </summary>
		/// <param name="commToData">Interface which has to be used to process</param>
		public abstract void Handle(ICommToDataClient commToData);
	}
}
