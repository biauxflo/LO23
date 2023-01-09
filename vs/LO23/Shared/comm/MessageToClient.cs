using Shared.interfaces;

namespace Shared.comm
{
	/// <summary>
	/// Message envoy� du serveur au client. Le message est cr�� sur le serveur.
	/// </summary>
	public abstract class MessageToClient
	{
		/// <summary>
		/// G�re le message.
		/// Cette m�thode est ex�cut�e sur la machine du client.
		/// Les classes filles doivent surcharger cette m�thode.
		/// </summary>
		/// <param name="commToData">Interface de data</param>
		public abstract void Handle(ICommToDataClient commToData);
	}
}
