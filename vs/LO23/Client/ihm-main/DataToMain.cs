﻿using Shared.interfaces;

namespace Client.ihm_main
{
    internal class DataToMain : IDataToMain
    {
        /// <summary>
        /// Controleur de l'IHM-Main.
        /// </summary>
        private readonly IhmMainCore core;

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
