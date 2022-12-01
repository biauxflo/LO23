﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.data
{
    public class Game : LightGame
    {
		public string Name  { set; get; } // TODO : Supprimer duplicata (info déjà dans GameOptions)
		public int NbPlayers { set; get; } // TODO : Supprimer duplicata (info déjà dans GameOptions) 
		public int NbTokens  { set; get; } // TODO : Supprimer duplicata (info déjà dans GameOptions)
		public bool CanSpecJoin  { set; get; } // TODO : Supprimer duplicata (info déjà dans GameOptions)
		public bool CanSpecChat  { set; get; } // TODO : Supprimer duplicata (info déjà dans GameOptions)
		public GameOptions options { set; get; }
		public List<Round> rounds { set; get; }
        public int turn { set; get; }
        public int smallBlind { set; get; }
        public int bingBlind { set; get; }
        public int currentPLayerIndex { set; get; }
        public Phase currentPhase { set; get; }
        public int pot { set; get; }
        public int highestBet { set; get; }
        public int nbNoRise { set; get; }
        public List<ChatMessage> chat { set; get; }

		// For test purpose
		// TODO : Delete later
		public Game()
		{
		}

		public Game(int id, int smallBlind, int bingBlind) : base(id)
		{
			this.rounds = new List<Round>();
            this.turn = 0;
            this.smallBlind = smallBlind;
            this.bingBlind = bingBlind;
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
