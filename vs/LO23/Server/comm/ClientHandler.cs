using Newtonsoft.Json;
using Shared.comm;
using System;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Server.comm
{
	class ClientHandler
	{
		/// <summary>
		/// Identifiant du client géré.
		/// </summary>
		private string id;

		/// <summary>
		/// Socket de connexion avec le client.
		/// </summary>
		private TcpClient client;

		/// <summary>
		/// Serveur auquel le ClientHandler apparitent.
		/// </summary>
		private CommServer server;

		/// <summary>
		/// Thread d'écoute du client.
		/// </summary>
		private Thread thread;

		public ClientHandler(string id, TcpClient client, CommServer server)
        {
			this.id = id;
			this.client = client;
			this.server = server;
			this.thread = new Thread(receiveMessage);
			this.thread.Start();
		}

		/// <summary>
		/// Traite un message.
		/// </summary>
		private void receiveMessage()
        {
			while (true)
			{
				try
				{
					byte[] bytesToRead = new byte[this.client.ReceiveBufferSize];
					NetworkStream nwStream = this.client.GetStream();
					StreamReader sr = new StreamReader(nwStream, Encoding.UTF8);
					MessageToServer msg = JsonConvert.DeserializeObject<MessageToServer>(sr.ReadToEnd(), new JsonSerializerSettings
					{
						TypeNameHandling = TypeNameHandling.All
					});
					msg.handle();
				}
				catch (Exception e)
				{
					//TODO : handle exception
				}
			}
		}

		/// <summary>
		/// Envoie un message au client.
		/// </summary>
		public void sendMessage(MessageToClient msg)
        {
			string data = JsonConvert.SerializeObject(msg, Formatting.Indented, new JsonSerializerSettings
			{
				TypeNameHandling = TypeNameHandling.All
			});
			NetworkStream nwStream = this.client.GetStream();
			StreamWriter sw = new StreamWriter(nwStream, Encoding.UTF8);
			sw.Write(data);
		}

		/// <summary>
        /// Ferme l'écoute.
        /// </summary>
		public void end()
        {
			this.thread.Abort();
			this.client.Close();
		}
	}
}
