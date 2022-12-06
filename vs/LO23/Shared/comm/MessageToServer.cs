using Shared.interfaces;
using System;

namespace Shared.comm
{
	public abstract class MessageToServer
	{
		/// <summary>
		/// Operates the process of the message.
		/// </summary>
		public abstract void Handle(
			string id,
			ICommToDataServer commToDataServer,
			Action<MessageToClient, string> sendTo,
			Action<MessageToClient, string> broadcastExceptTo
		);
	}
}
