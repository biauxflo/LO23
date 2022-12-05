using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.data
{
    public class Game : LightGame
    {
        public List<Round> rounds { get; set; } //Un round est un enchainement de phases de jeu => Il va de la distribution des cartes jusqu'au reveal des cartes et répartition des gains
        public int turn { set; get; } //Un turn correspond au tour de jeu d'un joueur
		public GameOptions options { set; get; }
        public int smallBlind { set; get; }
        public int bingBlind { set; get; } 
        public int currentPlayerIndex { set; get; } //Le joueur qui doit jouer actuellement
        public Phase currentPhase { set; get; } //La phase actuelle (phase de mise, de pari, ou de révélation des cartes)
        public int pot { set; get; } //L'ensemble des jetons misés
        public int highestBet { set; get; } //Le nombre de jetons misés par un joueur le plus important 
        public int nbNoRise { set; get; } //Nombre de turn depuis le début de la phase courante qui n'ont pas rise (augmenter le pari minimum nécessaire) => Sert à déduire la fin d'une phase
        public List<ChatMessage> chat { set; get; }
		public string name { get;set; }
		public int nbPlayers { get;set; }
		public Deck deck {get; private set;} //L'ensemble des cartes dans le jeu, qu'elles soient en main, dans la pioche ou la défausse

		public Game(int id, GameOptions options) : base(id, options)
		{
			this.rounds = new List<Round>();
            this.turn = 0;
            this.smallBlind = options.StartingBigBlind/2;
            this.bingBlind = options.StartingBigBlind;
            this.currentPlayerIndex = 0;
            this.currentPhase = new Phase();
            this.pot = 0;
            this.highestBet = 0;
            this.nbNoRise = 0;
            this.chat = new List<ChatMessage>();
			this.deck = new Deck();
        }

		public int goToNextPlayer()
		{
			int nextPlayerIndex = this.currentPlayerIndex + 1 <= this.players.Count ? this.currentPlayerIndex + 1 : 0;
			this.currentPlayerIndex = nextPlayerIndex;

			return nextPlayerIndex;
		}
    }
}
