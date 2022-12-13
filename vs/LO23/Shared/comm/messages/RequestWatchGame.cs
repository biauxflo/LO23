using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.comm.messages;
using Shared.interfaces;

namespace Shared.comm
{
	public class RequestWatchGame : MessageToServer
	{
		public Guid gameId;
		public Guid playerId;

		public RequestWatchGame(Guid gameId, Guid playerId)
		{
			this.gameId = gameId;
			this.playerId = playerId;
		}

		public override void Handle(
			string id, 
			ICommToDataServer commToDataServer, 
			Action<MessageToClient, string> sendTo, 
			Action<MessageToClient, string> broadcastExceptTo
		)
		{
			//Game game = commToDataServer.addSpectatorToGame(this.gameId, this.playerId);
			//sendTo(new AddSpectatorToGameReturn(game), id);
			//broadcastExceptTo(new NotifyGameChangeMessage(game), id);
			throw new NotImplementedException();
		}
	}
}
