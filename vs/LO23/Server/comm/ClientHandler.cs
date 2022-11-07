using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Server.comm
{
	class ClientHandler
	{
		/// <summary>
		/// Socket de connexion avec le client.
		/// </summary>
		private TcpClient client;

		/// <summary>
		/// Serveur auquel le ClientHandler apparitent.
		/// </summary>
		private Server server;

		/// <summary>
		/// Écoute le client.
		/// </summary>
		private void handling();

		/// <summary>
		/// Traite un message.
		/// </summary>
		private void receiveMessage();

		/// <summary>
		/// Envoie un message au client.
		/// </summary>
		public void sendMessage(string ip);
	}
}
