using Shared.data;
using System;
using System.Collections.Generic;
using System.Data;

namespace Server.Data
{
    public class Comm_calls_Data_Server_Impl : Shared.interfaces.ICommToDataServer
    {
        private LightUser lightUser;
        private static List<LightUser> lightUsers = new List<LightUser>();
        private static List<LightGame> games = new List<LightGame>();

		public Comm_calls_Data_Server_Impl()
		{
		}

		public Comm_calls_Data_Server_Impl(LightUser lightUser)
        {
			this.lightUser = lightUser;
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

		public void printLightUserList()
		{
			foreach(LightUser lightUser in lightUsers)
			{
				Console.WriteLine(lightUser.id + " " + lightUser.username);
			}
		}

		public Comm_calls_Data_Server_Impl getCommCallsDataServerImpl()
        {
            return this;
        }
    }
}
