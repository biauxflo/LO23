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
	public class SendChatMessage : MessageToServer
	{
		public string message;
		public Game game;

		public SendChatMessage(Game game, string message)
		{
			this.game = game;
			this.message = message;
		}

		public override void Handle(
			string id, 
			ICommToDataServer commToDataServer, 
			Action<MessageToClient, string> sendTo, 
			Action<MessageToClient, string> broadcastExceptTo,
			Action<MessageToClient, Game, string> broadcastOnGame
		)
		{
			List<int> players = commToDataServer.getPlayersOfGame(this.game);
/*			for (int player in players)
			{
				sendTo(new NotifyNewChatMessage(this.message, player.username, this.game.id), player.id);
			}*/
			throw new NotImplementedException();
		}
	}
}
