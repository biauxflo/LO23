using Server.comm;
using Server.Data;
using Shared.data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    class MainServer
    {
        static void Main(string[] args)
        {
            CommServer commServer = new CommServer();
			DataServerCore dataServer = new DataServerCore();

			commServer.CommToDataServer = dataServer.implInterfaceForComm;

			/*********** Start code needed for V1 - to delete later****************/
			//dataServer.addGameToList(new Game(Guid.NewGuid(), new GameOptions("Game1", 2000, true, true, 100, 8, 0, 10,10)));
			/*********** End code needed for V1 - to delete later****************/


			commServer.Run("127.0.0.1", 10000);
            _ = Console.ReadLine(); // wait until press Return to close console
        }	
    }
}
