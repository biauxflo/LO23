using Shared.interfaces;
using Shared.data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.comm.messages
{
	class CreateNewGameReturn : MessageToClient
	{
		public Game game;
		public CreateNewGameReturn(Game game)
		{
			this.game = game;
		}
		public override void Handle(ICommToDataClient commToData)
		{
			commToData.setGame(this.game);
		}
	}
}
