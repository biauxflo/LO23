using Shared.data;
using Shared.interfaces;
using System;
using System.Collections.Generic;
using System.Configuration;

namespace Shared.comm
{
	public class AnnounceUserMessage : MessageToServer
	{
		public User user;

		public AnnounceUserMessage(User user)
		{
			this.user = user;
		}

		/// <summary>
		/// Operates the process of the message.
		/// </summary>
		public override void Handle(
			string id,
			ICommToDataServer commToDataServer,
			Action<MessageToClient, string> sendTo
		)
		{
			Console.WriteLine(this.user.firstname);
			// commToDataServer.$
			sendTo(new UserAnnouncedMessage(), id);
		}
	}
}
;
