using Client.ihm_main.DTO;
using Shared.interfaces;

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

        public void GameListUpdated(string game)
        {
            core.GameListUpdated(game);
        }

        public void GameLaunched(string game)
        {
            core.GameLaunched(game);
        }

        
        public void GameCreationFailed(string error)
        {
            core.GameCreationFailed(error);
        }
    }
}
