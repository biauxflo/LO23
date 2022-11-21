using Client.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.data
{
    public class Data_Client_ctrl
    {
        private Comm_calls_Data_Client_impl implInterfaceForComm;

        public Data_Client_ctrl()
        {
            this.implInterfaceForComm = new Comm_calls_Data_Client_impl();
        }

        public Comm_calls_Data_Client_impl getImplInterfaceForComm()
        {
            return this.implInterfaceForComm;
        }
    }
}
