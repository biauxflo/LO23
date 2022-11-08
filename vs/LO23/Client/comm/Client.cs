using System;
using System.IO;
using System.Net.Sockets;
using Shared.comm;
using System.Text;

namespace Client.comm
{
    class Client : IDataToComm
	{
		/// <summary>
		/// Socket de connexion avec le serveur.
		/// </summary>
		private TcpClient clientSocket = new TcpClient();

		/// <summary>
		/// Socket de connexion avec le serveur.
		/// </summary>
		private Thread tcpListenerThread = new Thread();

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
			StreamWriter sw = new StreamWriter(nwStream, Encoding.UTF8);
			sw.Write(data);
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
					StreamReader sr = new StreamReader(nwStream, Encoding.UTF8);
					MessageToClient msg = JsonConvert.DeserializeObject<MessageToClient>(sr.ReadToEnd(), new JsonSerializerSettings
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
		private void start(string ip, int port)
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
			this.tcpListenerThread = new Thread(receiveMessage);
			this.tcpListenerThread.Start();
		}

		/// <summary>
        /// Termine la connexion et l'écoute.
        /// </summary>
		private void end()
        {
			tcpListenerThread.Abort();
			this.clientSocket.Close();
		}
	}
}
