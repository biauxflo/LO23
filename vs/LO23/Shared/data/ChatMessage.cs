using System;

namespace Shared.data
{
	/// <summary>
	/// Classe <c>ChatMessage</c> Classe modélisant les messages qui sont envoyés dans le chat
	/// </summary>
	public class ChatMessage
    {
        public DateTime date { get; private set; }
        public string sender { get; private set; } // username sera passé en paramètre
        public string text { get; private set; }
        public int idGame { get; private set; }
		/// <summary>
		/// Constructeur d'un ChatMessage
		/// </summary>
		/// <param name="date"></param>
		/// <param name="sender"></param>
		/// <param name="text"></param>
		/// <param name="idGame"></param>
        public ChatMessage(
            DateTime date,
            string sender,
            string text,
            int idGame
        ) {
            this.date = date;
            this.sender = sender;
            this.text = text;
            this.idGame = idGame;
        }
    }
}
