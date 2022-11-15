using System;
using System.IO;
using System.Net.Sockets;
using Shared.comm;
using Shared.interfaces;
using System.Text;
using System.Threading;
using Newtonsoft.Json;

namespace Client.comm
{
    public class CommClient : IDataToComm
	{
		/// <summary>
		/// Socket de connexion avec le serveur.
		/// </summary>
		private readonly TcpClient clientSocket = new TcpClient();

		/// <summary>
		/// Socket de connexion avec le serveur.
		/// </summary>
		private Thread tcpListenerThread = default;

		/// <summary>
		/// Interface avec les données du client.
		/// </summary>
		/// private ICommToData

		/// <summary>
		/// Envoie un message au serveur.
		/// </summary>
		private void sendMessage(MessageToServer msg)
        {
			string data = JsonConvert.SerializeObject(msg, Formatting.Indented, new JsonSerializerSettings
			{
				TypeNameHandling = TypeNameHandling.All
			});
			NetworkStream nwStream = this.clientSocket.GetStream();
			byte[] bytesToSend = ASCIIEncoding.ASCII.GetBytes(data);
			nwStream.Write(bytesToSend, 0, bytesToSend.Length);
		}

		/// <summary>
		/// Réceptionne un message du serveur.
		/// </summary>
		private void receiveMessage()
        {
			while (true)
			{
				try
				{
					byte[] bytesToRead = new byte[this.clientSocket.ReceiveBufferSize];
					NetworkStream nwStream = this.clientSocket.GetStream();
					int bytesRead = nwStream.Read(bytesToRead, 0, this.clientSocket.ReceiveBufferSize);
					string data = Encoding.ASCII.GetString(bytesToRead, 0, bytesRead);
					MessageToClient msg = JsonConvert.DeserializeObject<MessageToClient>(data, new JsonSerializerSettings
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
		/// Initie la connexion et l'écoute.
		/// </summary>
		public void start(string ip, int port)
        {
			//try to connect
			do
			{
				try
				{
					this.clientSocket.Connect(ip, port);
				}
				catch (SocketException e)
				{
					//TODO : Connection fail
				}
			} while (!this.clientSocket.Connected);

			//listen to server
			this.tcpListenerThread = new Thread(this.receiveMessage);
			this.tcpListenerThread.Start();
		}

		/// <summary>
        /// Termine la connexion et l'écoute.
        /// </summary>
		private void end()
        {
			this.tcpListenerThread.Abort();
			this.clientSocket.Close();
		}

		/// <summary>
        /// 
        /// </summary>
        /// <param name="username"></param>
		public void announceUser(string username)
        {
			AnnounceUserMessage msg = new AnnounceUserMessage();
			this.sendMessage(msg);
		}
	}
}
