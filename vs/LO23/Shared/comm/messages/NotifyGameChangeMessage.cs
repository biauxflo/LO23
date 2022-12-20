using Shared.interfaces;
using Shared.data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.comm.messages
{
	public class NotifyGameChangeMessage : MessageToClient
	{

		public Game game;
		public NotifyGameChangeMessage(Game game)
		{
			this.game = game;
		}
		/// <summary>
		/// Operates the process of the message.
		/// </summary>
		public override void Handle(ICommToDataClient commToData)
		{
			commToData.updateGame(this.game);

		}
	}
}
