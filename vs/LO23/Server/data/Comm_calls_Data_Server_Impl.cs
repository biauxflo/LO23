using Shared.data;
using System;
using System.Collections.Generic;
using System.Data;

namespace Server.Data
{
    public class Comm_calls_Data_Server_Impl : Shared.interfaces.Interface_Comm_calls_Data_Server
    {
        private LightUser lightUser;
        private static List<LightUser> lightUsers = new List<LightUser>();
        private static List<LightGame> games = new List<LightGame>();

        public Comm_calls_Data_Server_Impl()
        {
            this.lightUser = new LightUser();
            this.registerUser(this.lightUser);
        }
        
        public LightUser getUser()
        {
            return this.lightUser;
       
        }

        public List<LightUser> registerUser(LightUser lightUser)
        {
            lightUsers.Add(lightUser);
            return lightUsers;
        }

        public void removeUser(int idJoueur)
        {
            foreach(LightUser lightUser in lightUsers)
            {
                if(lightUser.id == idJoueur)
                {
                    lightUsers.Remove(lightUser);
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

