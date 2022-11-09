using System;

namespace Client.Data
{
    public class Data_Client
    {
        private Comm_calls_Data_Client_impl implInterfaceForComm;

        public Data_Client()
        {
            this.implInterfaceForComm = new Comm_calls_Data_Client_impl();
        }
        
        public Comm_calls_Data_Client_impl getImplInterfaceForComm()
        {
            return implInterfaceForComm;
        }
    }
}
