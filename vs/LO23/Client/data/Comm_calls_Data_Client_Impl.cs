using Shared.data;
using System;
using System.Collections.Generic;
using System.Data;
using Shared.interfaces;

namespace Client.data
{
    public class Comm_calls_Data_Client_impl : Shared.interfaces.Interface_Comm_calls_Data_Client
    {
        public Data_Client_ctrl data_Client_Ctrl { get; private set; }
        
        public Comm_calls_Data_Client_impl(Data_Client_ctrl data_Client_Ctrl){
			this.data_Client_Ctrl = data_Client_Ctrl;
        }

        public void setGame(Game game)
        {
            data_Client_Ctrl.joinedGame = game;
            data_Client_Ctrl.request_displayGameToIHMMain(game);
        }

		public void setGamesAndUsers(List<LightUser> listUsers, List<LightGame> listGame)
		{
			data_Client_Ctrl.games = listGame;
			data_Client_Ctrl.users = listUsers;
		}

		public void setLoggedOut()
		{
		}

		public void updateMessages(ChatMessage message)
		{
		}

		public void updateGame(Game game)
		{
		}

		public LightUser getProfile()
		{
			return new LightUser();
		}

		public void getProfileReturn(LightUser user)
		{
		}

		public void removeUserFromListUsers(LightUser user)
		{
		}

		public void addUserToListUsers(LightUser user)
		{
		}

		public void removeGameFromListGames(LightGame game)
		{
		}

		public void addGameToListGames(LightGame game)
		{
		}
	}
}
