using Shared.interfaces;

namespace Shared.comm
{
	/// <summary>
	/// Message envoyé du serveur au client. Le message est créé sur le serveur.
	/// </summary>
	public abstract class MessageToClient
	{
		/// <summary>
		/// Gère le message.
		/// Cette méthode est exécutée sur la machine du client.
		/// Les classes filles doivent surcharger cette méthode.
		/// </summary>
		/// <param name="commToData">Interface de data</param>
		public abstract void Handle(ICommToDataClient commToData);
	}
}
