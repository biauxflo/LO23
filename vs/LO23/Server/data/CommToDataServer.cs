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
			DataServerCore.lightUsers.Add(lightUser);
			List<LightGame> lgames = new List<LightGame>();

			foreach(Game game in DataServerCore.games)
			{
				lgames.Add(Game.ToLightGame(game));
			}

			return (DataServerCore.lightUsers, lgames);
        }

        public void removeUser(Guid idUser)
        {
            foreach(LightUser lightUser in DataServerCore.lightUsers)
            {
                if(lightUser.id == idUser)
                {
					DataServerCore.lightUsers.Remove(lightUser);
                    break;
                }
            }
        }

		public void printLightUserList()
		{
			foreach(LightUser lightUser in DataServerCore.lightUsers)
			{
				Console.WriteLine(lightUser.id + " " + lightUser.username);
			}
		}

        public Game addUserToGame(LightUser user, Guid gameId)
        {
            //TODO
			//WARNING
			// Verifier que �a fonctionne => L'utilisateur ajoute � la Game devrait se retrouver dans l'objet Game qui fait partie de listGames dans Data_Server_ctrl.
			//Or ici on fait (je pense) une copie de cet objet la, et on ajoute le user � la copie de la game. 
			//A verifier
			Game game = DataServerCore.games.Find(x => x.id == gameId);
			
            game.addUser(user);
			if(game.lobby.Count == game.gameOptions.NbPlayersMin)
			{
				game.initializeGame();
			}
            return game;
        }

        public CommToDataServer getCommCallsDataServerImpl()
        {
            return this;
        }



		public Game createNewGame(GameOptions options)
		{
			Game game = new Game(new Guid(), options);
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
		public (Game,List<Guid>) applyPlayTurn(Guid playerId, GameAction action)
		{
			Game game = DataServerCore.games.Find(x => x.players.Find((y)=> y.id == playerId) != null);// ask denis it is his pb <3
			game.handleGameAction(playerId, action);
			List<Guid> playersId = game.players.ConvertAll(new Converter<Player, Guid>(x => x.id));// ask denis it is his pb <3

			return (game,playersId);
		}
	}
}
