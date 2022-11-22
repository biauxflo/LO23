using System;

namespace Shared.data
{
    public class ChatMessage
    {
        public DateTime date { get; private set; }
        public string sender { get; private set; } // username sera passé en paramètre
        public string text { get; private set; }
        public int idGame { get; private set; }

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
