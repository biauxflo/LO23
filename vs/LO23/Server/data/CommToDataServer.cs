using Shared.constants;
using Shared.data;
using System;
using System.Collections.Generic;
using System.Data;

namespace Server.Data
{
    public class CommToDataServer : Shared.interfaces.ICommToDataServer
    {
		DataServerCore data_Server_Ctrl;
        public CommToDataServer(DataServerCore data_Server_Ctrl)
        {
			this.data_Server_Ctrl = data_Server_Ctrl;
		}

        public (List<LightUser>, List<LightGame>) registerUser(LightUser lightUser)
        {
			data_Server_Ctrl.lightUsers.Add(lightUser);
			List<LightGame> lgames = new List<LightGame>();

			foreach(Game game in data_Server_Ctrl.games)
			{
				lgames.Add(Game.ToLightGame(game));
			}

			return (data_Server_Ctrl.lightUsers, lgames);
        }

        public void removeUser(Guid idUser)
        {
            foreach(LightUser lightUser in data_Server_Ctrl.lightUsers)
            {
                if(lightUser.id == idUser)
                {
					data_Server_Ctrl.lightUsers.Remove(lightUser);
                    break;
                }
            }
        }

		public void printLightUserList()
		{
			foreach(LightUser lightUser in data_Server_Ctrl.lightUsers)
			{
				Console.WriteLine(lightUser.id + " " + lightUser.username);
			}
		}

        public Game addUserToGame(LightUser user, Guid gameId)
        {
			Game game = data_Server_Ctrl.games.Find(x => x.id == gameId);

			if(game.lobby.Count < Constants.NB_PLAYERS_MAX)
			{
				game.addUser(user);
				return game;
			}
			else
				return null;
        }

        public CommToDataServer getCommCallsDataServerImpl()
        {
            return this;
        }



		public Game createNewGame(GameOptions options)
		{
			Game game = new Game(Guid.NewGuid(), options);
			data_Server_Ctrl.addGameToList(game);

			return game;
		}

		public Game addPlayerToGame(Guid playerId)
		{
			throw new NotImplementedException();
		}

		public List<int> getPlayersOfGame(Game game)
		{
			throw new NotImplementedException();
		}

		public void uneregisterUser(Guid playerId)
		{
			throw new NotImplementedException();
		}

		public Game applyActionToPlayer(GameAction gameAction)
		{
			return data_Server_Ctrl.applyGameAction(gameAction);
		}
		public Game removePlayerToGame(Guid playerId, Guid gameId)
		{
			throw new NotImplementedException();
		}

		public (List<LightUser>, List<LightGame>) getUsersAndGames()
		{
			List<LightGame> lgames = new List<LightGame>();

			foreach(Game game in data_Server_Ctrl.games)
			{
				lgames.Add(Game.ToLightGame(game));
			}

			return (data_Server_Ctrl.lightUsers, lgames);
		}
		public List<LightUser> getListConnectedUsers()
		{
			return data_Server_Ctrl.getListConnectedUsers();
		}

		public List<LightGame> getListGames()
		{
			return data_Server_Ctrl.getListLightGames();
		}

		/*public (Game,List<Guid>) applyPlayTurn(Guid playerId, GameAction action)
		{
			Game game = DataServerCore.games.Find(x => x.players.Find((y)=> y.id == playerId) != null);// ask denis it is his pb <3
			game.handleGameAction(playerId, action);
			List<Guid> playersId = game.players.ConvertAll(new Converter<Player, Guid>(x => x.id));// ask denis it is his pb <3

			return (game,playersId);
		}
*/
		public (Game, List<Guid>) removePlayerToGame(Guid playerId)
		{
			Game game = data_Server_Ctrl.games.Find(x => x.players.Find((y) => y.id == playerId) != null);
			Player player = game.players.Find(x => x.id == playerId);
			player.tokens = 0;
			game.fold(player);
			game.players.Remove(player);
			if(game.nbPlayers >= 1)
			{
				game.nbPlayers -= 1;
			}
			List<Guid> playersId = game.players.ConvertAll(new Converter<Player, Guid>(x => x.id));

			return (game, playersId);

		}

	}
}
