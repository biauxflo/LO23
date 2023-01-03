using Shared.data;
using System;
using System.Collections.Generic;
using System.Data;
using Shared.interfaces;

namespace Client.data
{
    public class CommToDataClient : Shared.interfaces.ICommToDataClient
    {
        public DataClientCore dataClientCore { get; private set; }
        
        public CommToDataClient(DataClientCore data_Client_Ctrl){
			this.dataClientCore = data_Client_Ctrl;
        }

		// @pirousse: setGame creating/joining game
        public void setGame(Game game)
        {
            dataClientCore.joinedGame = game;
            dataClientCore.request_displayGameToIHMMain(game);
        }

		public void setGamesAndUsers(List<LightUser> listUsers, List<LightGame> listGame)
		{
			dataClientCore.Games = listGame;
			dataClientCore.Users = listUsers;
		}

		public void setLoggedOut()
		{
		}

		public void updateMessages(ChatMessage message)
		{
		}

		public void updateGame(Game game)
		{
			dataClientCore.interfaceFromGame.UpdateGameDisplay(game);
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
