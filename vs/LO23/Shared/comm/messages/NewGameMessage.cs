using Shared.data;
using Shared.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.comm.messages
{
	public class NewGameMessage : MessageToClient
	{

		public List<LightUser> lightUsers;
		public List<LightGame> lightGames;
		public NewGameMessage(List<LightUser> users, List<LightGame> games)
		{
			this.lightUsers = users;
			this.lightGames = games;
		}
		/// <summary>
		/// Operates the process of the message.
		/// </summary>
		public override void Handle(ICommToDataClient commToData)
		{
			commToData.setGamesAndUsers(this.lightUsers, this.lightGames);
		}
	}
}
