using System;
using System.Collections.Generic;
using Client.data;
using Shared.data;
using Shared.interfaces;
using Shared.helpers;
using Shared.constants;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace Client.data
{
	public class IHMGameToDataClient : Shared.interfaces.IGameToData
	{
		DataClientCore data_client_ctrl;
		public IHMGameToDataClient(DataClientCore data_client_ctrl)
		{
			this.data_client_ctrl = data_client_ctrl;
		}

		void IGameToData.leaveGame(Guid gameId, Guid lightUser)
		{
			data_client_ctrl.request_LeaveGame(gameId, lightUser);
		}

		void IGameToData.playRound(GameAction action)
		{
			data_client_ctrl.request_PlayRoundToComm(action);
		}

		void IGameToData.saveGame()
		{
			throw new NotImplementedException();
		}

		void IGameToData.sendMessage(ChatMessage message)
		{
			throw new NotImplementedException();
		}

		void IGameToData.stopGame()
		{
			throw new NotImplementedException();
		}

		LightUser IGameToData.whoAmi()
		{
			return User.ToLightUser(data_client_ctrl.CurrentUser);
		}
	}
}
