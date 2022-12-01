using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Server.Data;
using Shared.data;

namespace Server.Data
{
    public class Data_Server_ctrl
    {
        private Comm_calls_Data_Server_Impl implInterfaceForComm;

		internal static List<LightUser> lightUsers = new List<LightUser>();
		internal static List<Game> games = new List<Game>();

		public Data_Server_ctrl()
        {
            this.implInterfaceForComm = new Comm_calls_Data_Server_Impl(this);
        }

        public Comm_calls_Data_Server_Impl getImplInterfaceForComm()
        {
            return this.implInterfaceForComm;
        }
    }
}
