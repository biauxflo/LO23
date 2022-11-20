using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.data
{
    public class Game
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

        public Game(int smallBlind, int bingBLind)
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

        public Game(
            List<Round> rounds,
            int turn, int smallBlind,
            int bingBLind,
            int currentPLayerIndex,
            Phase currentPhase,
            int pot,
            int highestBet,
            int nbNoRise,
            List<ChatMessage> chat
        ) {
            // Not sure if we really need that but it is present in case
            // Prefer using the constructor above
            this.rounds = rounds;
            this.turn = turn;
            this.smallBlind = smallBlind;
            this.bingBLind = bingBLind;
            this.currentPLayerIndex = currentPLayerIndex;
            this.currentPhase = currentPhase;
            this.pot = pot;
            this.highestBet = highestBet;
            this.nbNoRise = nbNoRise;
            this.chat = chat;
        }
    }
}
