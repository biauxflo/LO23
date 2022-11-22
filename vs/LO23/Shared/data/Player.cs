using System;
using System.Collections.Generic;

namespace Shared.data
{
    enum PlayerRole
    {
        smallBlind,
        bigBlind,
        nothing,
    }

    public class Player
    {
        public string role { get; set; }
        public bool isFolded { get; set; }
        public int tokens { get; set; }
        public int tokensBet { get; set; }
        public List<Card> hand { get; set; }

        public Player(int tokens)
        {
            this.role = PlayerRole.nothing.ToString();
            this.isFolded = false;
            this.tokens = tokens;
            this.tokensBet = 0;
            this.hand = new List<Card>();
        }
    }
}
