using Shared.comm;
using Shared.comm.messages;
using Shared.data;
using Shared.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.comm
{
	internal class IDataToCommImpl : IDataToComm
	{
		readonly Action<MessageToServer> send;
		public IDataToCommImpl(Action<MessageToServer> send)
		{
			this.send = send;
		}

		void IDataToComm.announceUser(LightUser user)
		{
			AnnounceUserMessage msg = new AnnounceUserMessage(user);
			this.send(msg);
		}
		void IDataToComm.unannounce(Guid userId)
		{
			throw new NotImplementedException();
		}

		void IDataToComm.sendMessage(ChatMessage chatMsg)
		{
			throw new NotImplementedException();
		}

		void IDataToComm.requestStopGame(Guid gameId, Guid playerId)
		{
			throw new NotImplementedException();
		}

		void IDataToComm.requestLeaveGame(Guid gameId, Guid playerId)
		{
			RequestLeaveGameMessage msg = new RequestLeaveGameMessage(playerId, gameId);
			this.send(msg);
		}

		void IDataToComm.requestPlayRound(GameAction gameAction)
		{
			RequestPlayRoundMessage msg = new RequestPlayRoundMessage(gameAction);
			this.send(msg);
		}

		void IDataToComm.requestWatchGame(Guid gameId, Guid playerId)
		{
			throw new NotImplementedException();
		}

		void IDataToComm.requestPlayGame(Guid gameId, LightUser lightUser)
		{
			RequestPlayGame msg = new RequestPlayGame(lightUser, gameId);
			this.send(msg);
		}

		void IDataToComm.createNewGame(GameOptions gameOptions)
		{
			CreateNewGame msg = new CreateNewGame(gameOptions); 
			this.send(msg);
		}

		void IDataToComm.getProfile(Guid playerId)
		{
			throw new NotImplementedException();
		}
	}
}
