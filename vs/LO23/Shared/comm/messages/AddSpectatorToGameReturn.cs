using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.comm.messages;
using Shared.data;
using Shared.interfaces;

namespace Shared.comm
{
	public class AddSpectatorToGameReturn : MessageToClient
	{
		public Game game;

		public AddSpectatorToGameReturn(Game game)
		{
			this.game = game;
		}

		public override void Handle(ICommToDataClient commToData)
		{
			commToData.setGame(this.game);
		}
	}
}
