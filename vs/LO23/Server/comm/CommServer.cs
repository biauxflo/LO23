using Shared.comm;
using Shared.interfaces;
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
		/// <summary>Interface fournie par DataServer</summary>
		public ICommToDataServer CommToDataServer {get; set; }

		/// <summary>Recepteur de nouveaux cliens</summary>
		private TcpListener newClientListener = default;

		/// <summary>Ensemble des clients connectés.</summary>
		private readonly Dictionary<string, TcpClientHandler<MessageToClient, MessageToServer>> 
			tcpClientHandlers = new Dictionary<string, TcpClientHandler<MessageToClient, MessageToServer>>();


		/// <summary>
		/// Initie l'écoute.
		/// </summary>
		public void Run(string ip, int port)
		{
			//setup server
			this.newClientListener = new TcpListener(IPAddress.Parse(ip), port);
			this.newClientListener.Start();

			//listen to new clients
			while(true)
			{
				TcpClient client = this.newClientListener.AcceptTcpClient();
				string id = Guid.NewGuid().ToString();

				void action(MessageToServer msg)
				{
					this.OnReceiveFrom(msg, id);
				}

				this.tcpClientHandlers.Add(
					id,
					new TcpClientHandler<MessageToClient, MessageToServer>(client, action)
				);
			}
		}

		private void OnReceiveFrom(MessageToServer msg, string id)
		{
			msg.Handle(id, this.CommToDataServer, this.SendTo);
		}

		public void SendTo(MessageToClient msg, string id)
		{
			this.tcpClientHandlers[id].Send(msg);
		}
		
		// TODO : clean stop or destroy
	}
}
