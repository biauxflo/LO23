using Shared.interfaces;
using Shared.data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.comm
{
	public class RegisterUserReturnMessage : MessageToClient
	{
		public List<LightUser> lightUsers;
		public List<LightGame> lightGames;

		public RegisterUserReturnMessage(List<LightUser> users, List<LightGame> games)
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
