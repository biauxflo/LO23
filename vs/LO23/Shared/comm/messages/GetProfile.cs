using Shared.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.data;
using Shared.comm.messages;

namespace Shared.comm
{
	public class GetProfile : MessageToServer
	{
		public Guid playerId;

		public GetProfile(Guid playerId)
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
			//LightUser user = commToDataServer.getProfile(this.playerId);
			//sendTo(new GetProfileReturn(user), id);
			throw new NotImplementedException();
		}
	}
}
