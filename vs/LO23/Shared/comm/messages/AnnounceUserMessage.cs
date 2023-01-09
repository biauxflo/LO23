using Shared.comm.messages;
using Shared.data;
using Shared.interfaces;
using System;
using System.Collections.Generic;
using System.Configuration;

namespace Shared.comm
{
	public class AnnounceUserMessage : MessageToServer
	{
		public LightUser user;

		public AnnounceUserMessage(LightUser user)
		{
			this.user = user;
		}

		/// <summary>
		/// Operates the process of the message.
		/// </summary>
		public override void Handle(
			string id,
			ICommToDataServer commToDataServer,
			Action<MessageToClient, string> sendTo,
			Action<MessageToClient, string> broadcastExceptTo,
			Action<MessageToClient, Game, string> broadcastOnGame
		)
		{
			Console.WriteLine(this.user.username);
			(List<LightUser>, List<LightGame>) usersAndGames = commToDataServer.registerUser(this.user);
			sendTo(new RegisterUserReturnMessage(usersAndGames.Item1, usersAndGames.Item2), id);
			broadcastExceptTo(new NotifyUserChangeMessage(usersAndGames.Item1, usersAndGames.Item2), id);
		}
	}
}
;
