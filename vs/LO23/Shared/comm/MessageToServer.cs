using Shared.interfaces;
using System;
using Shared.data;

namespace Shared.comm
{
	/// <summary>
	/// Message send from the server to the client.
	/// </summary>
	public abstract class MessageToServer
	{
		/// <summary>
		/// Handles the message. This has to be overridden by children classes.
		/// </summary>
		/// <param name="id">Id of the client</param>
		/// <param name="commToDataServer">Interface which has to be used to process</param>
		/// <param name="sendTo">Action used to send response</param>
		/// <param name="broadcastExceptTo">Action used to notify other clients</param>
		public abstract void Handle(
			string id,
			ICommToDataServer commToDataServer,
			Action<MessageToClient, string> sendTo,
			Action<MessageToClient, string> broadcastExceptTo,
			Action<MessageToClient, Game, string> broadcastOnGame
		);
	}
}
