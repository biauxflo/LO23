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
	public class CreateNewGame : MessageToServer
	{
		public GameOptions gameOptions;

		public CreateNewGame(GameOptions gameOptions)
		{
			this.gameOptions = gameOptions;
		}
		public override void Handle(
			string id, 
			ICommToDataServer commToDataServer, 
			Action<MessageToClient, string> sendTo, 
			Action<MessageToClient, string> broadcastExceptTo,
			Action<MessageToClient, Game, string> broadcastOnGame
			)
		{
			Game game = commToDataServer.createNewGame(this.gameOptions);
			(List<LightUser>, List<LightGame>) usersAndGames = commToDataServer.getUsersAndGames();
			//sendTo(new CreateNewGameReturn(game), id);
			broadcastExceptTo(new NewGameMessage(usersAndGames.Item1, usersAndGames.Item2), "");
		}
	}
}
