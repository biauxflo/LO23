using System;
using System.Collections.Generic;
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

		internal List<LightUser> getListConnectedUsers()
		{
			return lightUsers;
		}

		internal List<LightGame> getListLightGames()
		{
			List<LightGame> lgs = new List<LightGame>();
			foreach(Game g in games)
			{
				lgs.Add(Game.ToLightGame(g));
			}

			return lgs;
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
