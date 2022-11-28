using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.data
{
	// Nina added inheritance from LightGame
    public class Game : LightGame
    {
        public List<Round> rounds { get; set; }
        public int turn { set; get; }
        public int smallBlind { set; get; }
        public int bingBLind { set; get; }
        public int currentPLayerIndex { set; get; }
        public Phase currentPhase { set; get; }
        public int pot { set; get; }
        public int highestBet { set; get; }
        public int nbNoRise { set; get; }
        public List<ChatMessage> chat { set; get; }
		public bool CanSpecJoin { get; set; }
		public bool CanSpecChat { get; set; }
		public string Name { get;set; }
		public int NbPlayers { get;set; }
		public int NbTokens { get;set; }

		// For test purpose
		// TODO : Delete later
		public Game()
		{
		}

		public Game(int id, int smallBlind, int bingBLind) : base(id)
		{
			this.rounds = new List<Round>();
            this.turn = 0;
            this.smallBlind = smallBlind;
            this.bingBLind = bingBLind;
            this.currentPLayerIndex = 0;
            this.currentPhase = new Phase();
            this.pot = 0;
            this.highestBet = 0;
            this.nbNoRise = 0;
            this.chat = new List<ChatMessage>();
        }

        // For test purpose
		// TODO : Delete later
		public Game(string name, int nbPlayers, int nbTokens, bool canSpecJoin, bool canSpecChat )
		{
			this.Name = name;
			this.NbPlayers = nbPlayers;
			this.NbTokens = nbTokens;
			this.CanSpecJoin = canSpecJoin;
			this.CanSpecChat = canSpecChat;
		}

		// For test purpose
		// TODO : Delete later
		public Game(string name, int nbPlayers)
		{
			this.Name = name;
			this.NbPlayers = nbPlayers;
		}

		public int GoToNextPlayer()
		{
			int nextPlayerIndex = this.currentPLayerIndex + 1 <= this.players.Count ? this.currentPLayerIndex + 1 : 0;
			this.currentPLayerIndex = nextPlayerIndex;

			return nextPlayerIndex;
		}
    }
}
