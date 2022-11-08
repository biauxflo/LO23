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
			this.thread = new Thread(this.receiveMessage);
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
					int bytesRead = nwStream.Read(bytesToRead, 0, this.client.ReceiveBufferSize);
					string data = Encoding.ASCII.GetString(bytesToRead, 0, bytesRead);
					MessageToServer msg = JsonConvert.DeserializeObject<MessageToServer>(data, new JsonSerializerSettings
					{
						TypeNameHandling = TypeNameHandling.All
					});
					msg.handle();
					//test code
					//this.sendMessage(new UserAnnoucedMessage());
				}
				catch (Exception e)
				{
					//TODO : handle exception

					Console.WriteLine("error reception");
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
			byte[] bytesToSend = ASCIIEncoding.ASCII.GetBytes(data);
			nwStream.Write(bytesToSend, 0, bytesToSend.Length);
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
