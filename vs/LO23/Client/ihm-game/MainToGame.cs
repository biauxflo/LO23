using Shared.data;
using Shared.interfaces;

namespace Client.ihm_game
{
	class MainToGame : IMainToGame
	{
		private readonly IhmGameCore core;

		public MainToGame(IhmGameCore core)
		{
			this.core = core;
		}

		public void LaunchGame(Game game)
		{
			core.LaunchGame(game);
		}
	}
}
