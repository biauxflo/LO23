using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.comm
{
	class Server
	{
        /// <summary>
        /// Ensemble des clients connectés.
        /// </summary>
		private Dictionary<int, ClientHandler>

		/// <summary>
        /// Interface avec les données du serveur.
        /// </summary>
		/// private IDataToComm

		/// <summary>
        /// Initie l'écoute.
        /// </summary>
		public void run(string ip, int port);

		/// <summary>
        /// Envoie un message à tous les clients.
        /// </summary>
		public void broadcast(string msg);
	}
}
