using Shared.data;
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
        public Game joinedGame { get; set; }

        public Data_Client_ctrl()
        {
            this.implInterfaceForComm = new Comm_calls_Data_Client_impl(this);
        }

        public Comm_calls_Data_Client_impl getImplInterfaceForComm()
        {
            return this.implInterfaceForComm;
        }

        public void request_PlayGameToComm(int gameId)
        {
            //En attendant l'ajout des interfaces de comm pour envoi requestPLayGame
        }

        public void request_displayGameToIHMMain(Game game)
        {
            //EN attendant l'ajout des interfaces de IHM Game pour DisplayGame
        }
    }
}
