using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.comm;
using Shared.comm.messages;
using Shared.interfaces;
using Shared.data;

namespace Shared.comm.messages
{
	public class RequestPlayGame : MessageToServer
	{
		public Guid gameId;
		public LightUser user;

		public RequestPlayGame(LightUser user, Guid id)
		{
			this.user = user;
			this.gameId = id;
		}
		public override void Handle(
			string id, 
			ICommToDataServer commToDataServer, 
			Action<MessageToClient, string> sendTo,
			Action<MessageToClient, string> broadcastExceptTo,
			Action<MessageToClient, Game, string> broadcastOnGame
			)
		{
			Game game = commToDataServer.addUserToGame(this.user, this.gameId);
			sendTo(new AddPlayerToGameReturn(game), id);
			broadcastOnGame(new NotifyGameChangeMessage(game), game, id);
		}
	}
}
