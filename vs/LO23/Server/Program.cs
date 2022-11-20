using Server.comm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    class Program
    {
        static void Main(string[] args)
        {
            CommServer s = new CommServer();
			s.Run("127.0.0.1", 10000);
            _ = Console.ReadLine(); // wait until press Return to close console
        }
    }
}
