using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.comm.messages;
using Shared.interfaces;
using Shared.data;

namespace Shared.comm
{
	public class RequestStopGame : MessageToServer
	{
		public Guid playerId;

		public RequestStopGame(Guid playerId)
		{
			this.playerId = playerId;
		}

		public override void Handle(
			string id, 
			ICommToDataServer commToDataServer, 
			Action<MessageToClient, string> sendTo, 
			Action<MessageToClient, string> broadcastExceptTo
		)
		{
			//GameAction gameAction = commToDataServer.terminateGame(this.playerId);
			//sendTo(new NotifyGameChangeMessage(gameAction), id);
			throw new NotImplementedException();
		}
	}
}
