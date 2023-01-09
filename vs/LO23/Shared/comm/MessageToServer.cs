using Shared.interfaces;
using System;
using Shared.data;

namespace Shared.comm
{
	/// <summary>
	///  Message envoy� du client au serveur. Le message est cr�� sur le client.
	/// </summary>
	public abstract class MessageToServer
	{
		/// <summary>
		/// G�re le message.
		/// Cette m�thode est ex�cut�e sur le serveur.
		/// Les classes filles doivent surcharger cette m�thode.
		/// </summary>
		/// <param name="id">identifiant de l'exp�diteurt</param>
		/// <param name="commToDataServer">Interface de data</param>
		/// <param name="sendTo">M�thode pour envoyer un message � un utilisateur</param>
		/// <param name="broadcastExceptTo">M�thode pour envoyer un message � un tous les utilisateurs sauf un</param>
		/// <param name="broadcastOnGame">M�thode pour envoyer un message � un tous les utilisateurs d'une partie sauf un</param>
		public abstract void Handle(
			string id,
			ICommToDataServer commToDataServer,
			Action<MessageToClient, string> sendTo,
			Action<MessageToClient, string> broadcastExceptTo,
			Action<MessageToClient, Game, string> broadcastOnGame
		);
	}
}
