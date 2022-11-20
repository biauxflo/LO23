using System;
using System.IO;
using System.Net.Sockets;
using Shared.data;
using Shared.comm;
using Shared.interfaces;
using System.Text;
using System.Threading;
using Newtonsoft.Json;

namespace Client.comm
{
	public class CommClient
	{
		/// <summary>Interface fournie par DataClient</summary>
		public ICommToData CommToData { get; set; }
		/// <summary>Interface implémentée pour DataClient</summary>
		public IDataToComm DataToComm { get; }

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
		/// Initie la connexion et l'écoute.
		/// </summary>
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

		private void OnReceive(MessageToClient msg)
		{
			msg.Handle(this.CommToData);
		}

		public void Send(MessageToServer msg)
		{
			this.handler.Send(msg);
		}

	}
}
