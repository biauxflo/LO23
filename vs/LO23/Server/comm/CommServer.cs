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
	/// <summary>
	/// Server that handle several clients and manages communication.
	/// </summary>
	public class CommServer
	{
		/// <summary>Interface fournie par DataServer</summary>
		public ICommToDataServer CommToDataServer { private get; set; }

		/// <summary>Recepteur de nouveaux cliens</summary>
		private TcpListener newClientListener = default;

		/// <summary>Ensemble des clients connectés.</summary>
		private readonly Dictionary<string, TcpClientHandler<MessageToClient, MessageToServer>>
			tcpClientHandlers = new Dictionary<string, TcpClientHandler<MessageToClient, MessageToServer>>();

		/// <summary>Ensemble des clients identifiés.</summary>
		private readonly Dictionary<Guid, string> tcpClientIds = new Dictionary<Guid, string>();


		/// <summary>
		/// Starts to listen to new clients.
		/// </summary>
		/// <param name="ip">Ip</param>
		/// <param name="port">Port</param>
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
					new TcpClientHandler<MessageToClient, MessageToServer>(client, action, id)
				);
			}
		}

		/// <summary>
		/// Handles a received message.
		/// </summary>
		/// <param name="msg">Message received</param>
		/// <param name="id">Client id</param>
		private void OnReceiveFrom(MessageToServer msg, string id)
		{
			if(typeof(AnnounceUserMessage) == msg.GetType())
			{

				this.tcpClientIds[((AnnounceUserMessage)msg).user.id] = id;
			}
			msg.Handle(
				id, 
				this.CommToDataServer, 
				this.SendTo,
				this.BroadcastExceptTo
			);
		}

		/// <summary>
		/// Sends a message to a client.
		/// </summary>
		/// <param name="msg">Message to send</param>
		/// <param name="id">Client id</param>
		public void SendTo(MessageToClient msg, string id)
		{
			this.tcpClientHandlers[id].Send(msg);
		}

		/// <summary>
		/// Sends a message to all clients except one.
		/// </summary>
		/// <param name="msg">Message to send</param>
		/// <param name="exceptId">Id of one client who won't receive the message</param>
		public void BroadcastExceptTo(MessageToClient msg, string exceptId)
		{
			foreach(KeyValuePair<string, TcpClientHandler<MessageToClient, MessageToServer>> client in this.tcpClientHandlers)
			{
				if(client.Key == exceptId) break;
				client.Value.Send(msg);
			}
		}
		
		// TODO : clean stop or destroy
	}
}
