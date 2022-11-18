using System;
using System.Collections.Generic;

namespace Shared.data
{
    public class Deck
    {
        public int index { get; set; }
        public List<Card> cards { get; set; }

        public Deck(int index, List<Card> cards)
        {
            this.index = index;
            this.cards = cards;
        }
    }
}
