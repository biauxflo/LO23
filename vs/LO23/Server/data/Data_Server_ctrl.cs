using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Server.Data;

namespace Server.Data
{
    public class Data_Server_ctrl
    {
        private Comm_calls_Data_Server_Impl implInterfaceForComm;

        public Data_Server_ctrl()
        {
            this.implInterfaceForComm = new Comm_calls_Data_Server_Impl();
        }

        public Comm_calls_Data_Server_Impl getImplInterfaceForComm()
        {
            return this.implInterfaceForComm;
        }
    }
}
