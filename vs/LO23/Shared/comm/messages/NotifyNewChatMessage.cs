using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.comm.messages;
using Shared.interfaces;

namespace Shared.comm
{
	public class NotifyNewChatMessage : MessageToClient
	{
		public string message;
		public string sender;
		public Guid gameId;

		public NotifyNewChatMessage(string message, string sender, Guid gameId)
		{
			this.message = message;
			this.sender = sender;
			this.gameId = gameId;
		}

		public override void Handle(ICommToDataClient commToData)
		{
			//commToData.updateMessages(new data.ChatMessage(DateTime.Now, this.sender, this.message, this.gameId));
			throw new NotImplementedException();
		}
	}
}
