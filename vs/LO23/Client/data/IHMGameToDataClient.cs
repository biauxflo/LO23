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
	public class IHMGameToDataClient : IGameToData
	{
		DataClientCore data_client_ctrl;
		public IHMGameToDataClient(DataClientCore data_client_ctrl)
		{
			this.data_client_ctrl = data_client_ctrl;
		}

		public void LeaveGame()
		{
			throw new NotImplementedException();
		}

		public void PlayRound(GameAction action)
		{
			throw new NotImplementedException();
		}

		public void SaveGame()
		{
			throw new NotImplementedException();
		}

		public void SendMessage(ChatMessage message)
		{
			throw new NotImplementedException();
		}

		public void StopGame()
		{
			throw new NotImplementedException();
		}

		public LightUser whoAmi()
		{
			return User.ToLightUser(data_client_ctrl.CurrentUser);
		}
	}
}
