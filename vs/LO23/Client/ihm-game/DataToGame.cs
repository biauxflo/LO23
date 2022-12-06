using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.interfaces;
using Shared.data;

namespace Client.ihm_game
{
	class DataToGame : IDataToGame
	{
		private readonly IhmGameCore core;

		public DataToGame(IhmGameCore core)
		{
			this.core = core;
		}
		public void updateMessageDisplay(ChatMessage message)
		{
			core.updateMessageDisplay(message);
		}

		public void updateGameDisplay(Game game)
		{
			core.updateGameDisplay(game);
		}

		public void startReplayDisplay(Game game)
		{
			core.startReplayDisplay(game);
		}

		public void displayGame(Game game)
		{
			core.displayGame(game);
		}
	}
}
