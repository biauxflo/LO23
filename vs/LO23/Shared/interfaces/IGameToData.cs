using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.data;

namespace Shared.interfaces
{
	public interface IGameToData
	{
		void sendMessage(ChatMessage message) ;

		void stopGame();

		void saveGame();

		void leaveGame();

		void playRound(Action action);
		
		LightUser whoAmi();
	}
}
