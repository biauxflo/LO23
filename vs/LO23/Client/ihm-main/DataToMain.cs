using Shared.data;
using Shared.interfaces;
using System.Collections.Generic;

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

        public void ConnectionSucceed(User user)
        {
            core.ConnectionSucceed(user);
        }

        public void GameCreationFailed(string error)
        {
            core.GameCreationFailed(error);
        }

		public void GameListUpdated(List<Game> games)
		{
			core.GameListUpdated(games);
		}

		public void GameLaunched(Game game)
		{
			core.GameLaunched(game);
		}
	}
}
