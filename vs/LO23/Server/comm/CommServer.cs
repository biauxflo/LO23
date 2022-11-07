using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Server.comm
{
	public class CommServer
	{
		/// <summary>
		/// Listen to new client
		/// </summary>
		private TcpListener newClientListener = default;

		/// <summary>
		/// Ensemble des clients connectés.
		/// </summary>
		private readonly Dictionary<string, ClientHandler> clientHandlers = new Dictionary<string, ClientHandler>();

		/// <summary>
		/// Interface avec les données du serveur.
		/// </summary>
		/// private IDataToComm

		/// <summary>
		/// Initie l'écoute.
		/// </summary>
		public void run(string ip, int port)
		{
			//setup server
			this.newClientListener = new TcpListener(IPAddress.Parse(ip), port);
			TcpClient client;
			this.newClientListener.Start();

			//listen to new clients
			while(true)
			{
				client = this.newClientListener.AcceptTcpClient();
				//start new Thread
				string id = Guid.NewGuid().ToString();
				this.clientHandlers.Add(id, new ClientHandler(id, client, this));

			}
		}

		private void stop()
		{
			this.newClientListener.Stop();
		}
	}
}
