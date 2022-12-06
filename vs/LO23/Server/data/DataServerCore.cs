using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Server.Data;
using Shared.data;

namespace Server.Data
{
    public class DataServerCore
    {
		internal CommToDataServer implInterfaceForComm { get; private set; }

		internal static List<LightUser> lightUsers = new List<LightUser>();
		internal static List<Game> games = new List<Game>();

		public DataServerCore()
        {
            this.implInterfaceForComm = new CommToDataServer(this);
        }


		internal void addGameToList(Game game)
		{
			games.Add(game);
		}
    }
}
