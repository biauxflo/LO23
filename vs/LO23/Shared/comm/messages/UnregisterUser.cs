using Shared.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.comm.messages;

namespace Shared.comm
{
	public class UnregisterUser : MessageToServer
	{
		public Guid playerId;

		public UnregisterUser(Guid playerId)
		{
			this.playerId = playerId;
		}

		public override void Handle(
			string id, 
			ICommToDataServer commToDataServer, 
			Action<MessageToClient, string> sendTo, 
			Action<MessageToClient, string> broadcastExceptTo
		)
		{
			commToDataServer.uneregisterUser(this.playerId);
			sendTo(new UnregisterUserReturn(), id);
			throw new NotImplementedException();
			//broadcastExceptTo(new NotifyUserChangeMessage());
		}
	}
}
