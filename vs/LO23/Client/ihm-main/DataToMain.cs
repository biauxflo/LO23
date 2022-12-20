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

        public void ConnectionFailed(string error)
        {
            core.ConnectionFailed(error);
        }

        public void ConnectionSucceed(LightUser user)
        {
            core.ConnectionSucceed(user);
        }

        public void GameCreationFailed(string error)
        {
            core.GameCreationFailed(error);
        }

		public void GameListUpdated(List<LightGame> games)
        {
			core.GameListUpdated(games);
        }

		public void GameLaunched(Game game)
        {
            core.GameLaunched(game);
        }

		public void ProfileCreationSuceed()
		{
			core.ProfileCreationSuceed();
		}

		public void ProfileCreatioFailed(string error)
		{
			core.ProfileCreationFailed(error);
		}
	}
}
