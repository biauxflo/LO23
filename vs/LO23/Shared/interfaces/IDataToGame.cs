using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.data;

namespace Shared.interfaces
{
	public interface IDataToGame
	{
		void updateMessageDisplay(ChatMessage message);

		void updateGameDisplay(Game game);

		void startReplayDisplay(Game game);

		void displayGame(Game game);
	}
}
