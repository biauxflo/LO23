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
		public void UpdateMessageDisplay(ChatMessage message)
		{
			core.UpdateMessageDisplay(message);
		}

		public void UpdateGameDisplay(Game game)
		{
			core.UpdateGameDisplay(game);
		}

		public void StartReplayDisplay(Game game)
		{
			core.StartReplayDisplay(game);
		}

	}
}
