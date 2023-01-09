using Shared.interfaces;
using System;
using Shared.data;

namespace Shared.comm
{
	/// <summary>
	///  Message envoyé du client au serveur. Le message est créé sur le client.
	/// </summary>
	public abstract class MessageToServer
	{
		/// <summary>
		/// Gère le message.
		/// Cette méthode est exécutée sur le serveur.
		/// Les classes filles doivent surcharger cette méthode.
		/// </summary>
		/// <param name="id">identifiant de l'expéditeurt</param>
		/// <param name="commToDataServer">Interface de data</param>
		/// <param name="sendTo">Méthode pour envoyer un message à un utilisateur</param>
		/// <param name="broadcastExceptTo">Méthode pour envoyer un message à un tous les utilisateurs sauf un</param>
		/// <param name="broadcastOnGame">Méthode pour envoyer un message à un tous les utilisateurs d'une partie sauf un</param>
		public abstract void Handle(
			string id,
			ICommToDataServer commToDataServer,
			Action<MessageToClient, string> sendTo,
			Action<MessageToClient, string> broadcastExceptTo,
			Action<MessageToClient, Game, string> broadcastOnGame
		);
	}
}
