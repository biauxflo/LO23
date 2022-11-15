using Shared.data;
using System;
using System.Collections.Generic;
using System.Data;


namespace Client.Data
{   
    public class Comm_calls_Data_Client_impl : Shared.interfaces.Interface_Comm_calls_Data_Client
    {
        private List<LightUser> users = new List<LightUser>();
        private List<LightGame> games = new List<LightGame>();
        public Comm_calls_Data_Client_impl(){}

        public void setGamesAndUsersList(List<LightUser> users, List<LightGame> games)
        {
            this.users = users;
            this.games = games;
        }

    }
}

