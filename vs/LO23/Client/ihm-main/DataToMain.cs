using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.interfaces;
using System.Threading;

namespace Client.ihm_main
{
    class DataToMain : IDataToMain
    {
        /// <summary>
        /// Controleur de l'IHM-Main
        /// </summary>
        private IhmMainCore core;

        public DataToMain(IhmMainCore core)
        {
            this.core = core;
        }

        public void ConnectionFailed()
        {
            core.ConnectionFailed();
        }

        public void ConnectionSucceed(string username)
        {
            core.ConnectionSucceed(username);
        }
    }
}
