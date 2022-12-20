﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.data;

namespace Shared.interfaces
{
	public interface IGameToData
	{
		void SendMessage(ChatMessage message) ;

		void StopGame();

		void SaveGame();

		void LeaveGame();

		void PlayRound(Action action);

	}
}
