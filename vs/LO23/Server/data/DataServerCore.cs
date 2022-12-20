using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Server.Data;
using Shared.data;

namespace Server.Data
{
    public class DataServerCore
    {
		internal CommToDataServer implInterfaceForComm { get; private set; }

		internal List<LightUser> lightUsers = new List<LightUser>();
		internal List<Game> games = new List<Game>();

		public DataServerCore()
        {
            this.implInterfaceForComm = new CommToDataServer(this);
        }


		internal void addGameToList(Game game)
		{
			games.Add(game);
		}

		internal Game findGameWithId(Guid gameId)
		{
			return games.Find(g => g.id == gameId);
		}

		internal Game applyGameAction(GameAction action)
		{
			Game game = findGameWithId(action.gameId);
			game.handleGameAction(action);

			return game;
		}
    }
}
