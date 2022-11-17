﻿using Shared.data;
using System;
using System.Collections.Generic;
using System.Data;

namespace Client.Data
{
    public class Comm_calls_Data_Client_impl : Shared.interfaces.Interface_Comm_calls_Data_Client
    {
        public List<LightUser> users { get; set; }
        public List<LightGame> games { get; set; }
        
        public Comm_calls_Data_Client_impl(){
            this.users = new List<LightUser>();
            this.games = new List<LightGame>();
        }

        public Comm_calls_Data_Client_impl(List<LightUser> users, List<LightGame> games){
            this.users = users;
            this.games = games;
        }

		public void setGamesAndUsers(List<LightUser> listUsers, List<LightGame> listGame)
		{
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

		public void setGame(Game game)
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

