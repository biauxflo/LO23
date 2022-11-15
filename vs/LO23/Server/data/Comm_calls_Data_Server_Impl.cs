using Shared.data;
using System;
using System.Collections.Generic;
using System.Data;


namespace Server.Data
{
    public class Comm_calls_Data_Server_Impl : Shared.interfaces.Interface_Comm_calls_Data_Server
    {
        private LightUser user;
        private static List<LightUser> users = new List<LightUser>();
        private static List<LightGame> games = new List<LightGame>();

        public Comm_calls_Data_Server_Impl()
        {
            this.user = new LightUser();
            registerUser(user);
        }
        
        public LightUser getUser()
        {
            return this.user;
       
        }

        public List<LightUser> registerUser(LightUser user)
        {
            users.Add(user);
            return users;
        }

        public void removeUser(int idJoueur)
        {
            foreach(LightUser user in users)
            {
                if(user.id == idJoueur)
                {
                    users.Remove(user);
                    break;
                }
            }
        
    }
        public Comm_calls_Data_Server_Impl getCommCallsDataServerImpl()
        {
            return this;
        }

    }

}

