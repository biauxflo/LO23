using Shared.data;
using System;
using System.Collections.Generic;
using System.Data;

namespace Server.Data
{
    public class Comm_calls_Data_Server_Impl : Shared.interfaces.ICommToDataServer
    {
		Data_Server_ctrl data_Server_Ctrl;
        public Comm_calls_Data_Server_Impl(Data_Server_ctrl data_Server_Ctrl)
        {
			this.data_Server_Ctrl = data_Server_Ctrl;
		}

        public List<LightUser> registerUser(LightUser lightUser)
        {
			Data_Server_ctrl.lightUsers.Add(lightUser);
            return Data_Server_ctrl.lightUsers;
        }

        public void removeUser(int idJoueur)
        {
            foreach(LightUser lightUser in Data_Server_ctrl.lightUsers)
            {
                if(lightUser.id == idJoueur)
                {
					Data_Server_ctrl.lightUsers.Remove(lightUser);
                    break;
                }
            }
        }

		public void printLightUserList()
		{
			foreach(LightUser lightUser in Data_Server_ctrl.lightUsers)
			{
				Console.WriteLine(lightUser.id + " " + lightUser.username);
			}
		}

        public Game addUserToGame(LightUser user, int gameId)
        {
            //TODO
			//WARNING
			// Verifier que �a fonctionne => L'utilisateur ajoute � la Game devrait se retrouver dans l'objet Game qui fait partie de listGames dans Data_Server_ctrl.
			//Or ici on fait (je pense) une copie de cet objet la, et on ajoute le user � la copie de la game. 
			//A verifier
			Game game = Data_Server_ctrl.games.Find(x => x.id == gameId);
			
            game.addUser(user); 
            return game;
        }

        public Comm_calls_Data_Server_Impl getCommCallsDataServerImpl()
        {
            return this;
        }



		public Game createNewGame(GameOptions options)
		{
			Random rnd = new Random();
			Game game = new Game(rnd.Next(1, int.MaxValue), options);
			data_Server_Ctrl.addGameToList(game);

			return game;
		}
    }
}
