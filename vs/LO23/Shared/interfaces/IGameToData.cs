using System;
using Shared.data;

namespace Shared.interfaces
{
	public interface IGameToData
	{
		void SendMessage(ChatMessage message) ;

		void StopGame();

		void SaveGame();

		void LeaveGame();
		
		LightUser whoAmi();

		void PlayRound(GameAction action);
	}
}
