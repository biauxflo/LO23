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
	public class RequestLeaveGameMessage : MessageToServer
	{
		public Guid userId;
		public Guid gameId;

		public RequestLeaveGameMessage(Guid userId, Guid gameId)
		{
			this.userId = userId;
			this.gameId = gameId;
		}
		public override void Handle(
			string id,
			ICommToDataServer commToDataServer,
			Action<MessageToClient, string> sendTo,
			Action<MessageToClient, string> broadcastExceptTo
			)
		{
			Game game = commToDataServer.removePlayerToGame(this.userId, this.gameId);
			broadcastExceptTo(new NotifyGameChangeMessage(game), id);
		}
	}
}
