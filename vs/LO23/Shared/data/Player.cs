﻿using System;
using System.Collections.Generic;

namespace Shared.data
{
    public enum PlayerRole
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
        public List<Card> hand { get; set; } //Doit referencer les objets cartes contenus dans l'objet Deck

        public Player(int tokens)
        {
            this.role = PlayerRole.nothing.ToString();
            this.isFolded = false;
            this.tokens = tokens;
            this.tokensBet = 0;
            this.hand = new List<Card>();
        }

		public void removeCardFromHand(Card card)
		{   
			int remove = 0;//index of the card to remove in player's hand
			bool flag =false;

			for(int i = 0; i < this.hand.Count; i++)
			{   
				if(this.CompareCard(this.hand[i], card) == true)
				{
					flag = true;
				}
				
			}

			if(remove == 6)
			{
				Console.WriteLine("pas bien la vérification dans player pour la carte");
			}
			this.hand.RemoveAt(remove);

		}
		public void AddCardToHand(Card card)
		{

			if(this.hand.Count < 5)
			{
				this.hand.Add(card);
			}

		}
		public bool CompareCard(Card card1, Card card2)
		{
			return (card1.color == card2.color) && (card1.index == card2.index);
		}
	}
}
