using Shared.comm;
using Shared.data;
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
	/// Server gérant la communication avec les clients.
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
		/// Débute l'écoute de nouveaux clients afin qu'ils puissent se connecter.
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
		/// Gère la réception des messages
		/// </summary>
		/// <param name="msg">Message reçu</param>
		/// <param name="id">Identifiant du client expéditeur</param>
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
				this.BroadcastExceptTo,
				this.BroadcastOnGame
			);
		}

		/// <summary>
		///Envoie un message à un client.
		/// </summary>
		/// <param name="msg">Message à envoyer</param>
		/// <param name="id">Identifiant du destinataire</param>
		public void SendTo(MessageToClient msg, string id)
		{
			this.tcpClientHandlers[id].Send(msg);
		}

		/// <summary>
		/// Envoie un message à tous les clients sauf un.
		/// </summary>
		/// <param name="msg">Message à envoyer</param>
		/// <param name="exceptId">Identifiant du client à éviter (laisser "" pour envoyer à tous les clients)</param>
		public void BroadcastExceptTo(MessageToClient msg, string exceptId)
		{
			foreach(KeyValuePair<string, TcpClientHandler<MessageToClient, MessageToServer>> client in this.tcpClientHandlers)
			{
				if(client.Key == exceptId)
					break;
				client.Value.Send(msg);
			}
		}

		/// <summary>
		/// Envoi un message à tous les utilisateurs connectés à la partie donnée sauf un.
		/// Envoie le message à tous les utilisateurs présents dans la propriété loby du jeu.
		/// </summary>
		/// <param name="msg">Message à envoyer</param>
		/// <param name="game">Jeu contenant les utilisateurs à qui envoyer le message.</param>
		/// <param name="exceptId">Identifiant du client à éviter (laisser "" pour envoyer à tous les clients)</param>
		public void BroadcastOnGame(MessageToClient msg, Game game, string exceptId)
		{
			foreach(LightUser user in game.lobby)
			{
				string clientId = this.tcpClientIds[user.id];
				if(clientId != exceptId)
				{
					this.tcpClientHandlers[clientId].Send(msg);
				}
			}
		}

		// TODO : clean stop or destroy
	}
}
