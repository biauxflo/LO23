using System;
using System.IO;
using System.Net.Sockets;
using Shared.data;
using Shared.comm;
using Shared.interfaces;
using System.Text;
using System.Threading;
using Newtonsoft.Json;
using System.Windows;

namespace Client.comm
{
	/// <summary>
	/// Client that connects to a server and manage messages.
	/// </summary>
	public class CommClient
	{
		/// <summary>Interface fournie par DataClient</summary>
		public ICommToDataClient CommToData { private get; set; }
		/// <summary>Interface implémentée pour DataClient</summary>
		public IDataToComm DataToComm { get; private set; }

		/// <summary>Tcp client handler</summary>
		private TcpClientHandler<MessageToServer, MessageToClient> handler;

		/// <summary>
		/// Constructeur : instancie l'interface implémentée pour DataClient.
		/// </summary>
		public CommClient()
		{
			this.DataToComm = new IDataToCommImpl(this.Send);
		}

		/// <summary>
		/// Starts the connexion to the server. Then, starts to listen to new messages.
		/// </summary>
		/// <param name="ip">Server ip</param>
		/// <param name="port">Server port</param>
		public void Start(string ip, int port)
		{
			TcpClient tcpClient = new TcpClient();
			//try to connect
			do
			{
				try
				{
					tcpClient.Connect(ip, port);
				}
				catch(Exception e)
				{
					//TODO : Connection fail
				}
			} while(!tcpClient.Connected);

			this.handler = new TcpClientHandler<MessageToServer, MessageToClient>
				(tcpClient, this.OnReceive);
		}

		/// <summary>
		/// Handles a received message.
		/// </summary>
		/// <param name="msg">Recevied message</param>
		private void OnReceive(MessageToClient msg)
		{
			Application.Current.Dispatcher.Invoke(() => msg.Handle(this.CommToData));
		}

		/// <summary>
		/// Sends a message to the server.
		/// </summary>
		/// <param name="msg">Message to send</param>
		public void Send(MessageToServer msg)
		{
			this.handler.Send(msg);
		}

	}
}
