using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
	public static class Config
	{
		public static string GetServerIp()
		{
			return Properties.Resources.ServerIp;
		}
		public static int GetServerPort()
		{
			int port = int.Parse(Properties.Resources.ServerPort);
			return port;
		}
	}
}
