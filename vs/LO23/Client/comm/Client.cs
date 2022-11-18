using System;
using System.Net.Sockets;
using Shared.interfaces;

namespace Client.comm
{
    class Client : ICommToData
    {
        /// <summary>
        /// Socket de connexion avec le serveur.
        /// </summary>
        private TcpClient clientSocket = new TcpClient();

		/// <summary>
        /// Interface avec les données du client.
        /// </summary>
		/// private IDataToComm

		/// <summary>
        /// Envoie un message au serveur.
        /// </summary>
		private void sendMessage(string text);

		/// <summary>
        /// Réceptionne un message du serveur.
        /// </summary>
		private string receiveMessage();

		/// <summary>
        /// Initie la connexion et l'écoute.
        /// </summary>
		public void run(string ip, int port);
    }
}
