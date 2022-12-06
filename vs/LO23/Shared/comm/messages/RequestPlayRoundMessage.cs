﻿using System;
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
	public class RequestPlayRoundMessage : MessageToServer
	{
		public GameAction gameAction;
		public Guid playerId;

		public RequestPlayRoundMessage(GameAction gameAction, Guid playerId)
		{
			this.gameAction = gameAction;
			this.playerId = playerId;
		}
		public override void Handle(
			string id,
			ICommToDataServer commToDataServer,
			Action<MessageToClient, string> sendTo,
			Action<MessageToClient, string> broadcastExceptTo
			)
		{
			Game game = commToDataServer.applyActionToPlayer(this.gameAction, this.playerId);
			broadcastExceptTo(new NotifyGameChangeMessage(game), id);
		}
	}
}
