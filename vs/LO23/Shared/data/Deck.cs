using System;
using System.Collections.Generic;
using System.Linq;

namespace Shared.data
{
    public class Deck
    {
        public int index { get; set; }
        public List<Card> cards { get; set; }

        public Deck()
		{
			this.index = 0;
			this.cards = new List<Card>();
			this.generate52Cards();
		}


		public void shuffleCards()
		{
			Random rand = new Random();
			cards = cards.OrderBy(a => rand.Next()).ToList();
			index = 0;
		}

		public void generate52Cards() //Can also just be the reset() function
		{
			int i = 0;
			foreach(char color in Card.colors)
			{
				foreach(int value in Card.values)
				{
					this.cards.Add(new Card(i, color, value, false, true));
					i++;
				}
			}

			Console.WriteLine("Number of cards in deck: ", cards.Count);
		}
		public Card getNextCardAvailable()
		{
	
			while(this.cards[this.index].isInHand)
			{
				this.index = (this.index + 1) % cards.Count;
			}

			Card cardTmp = this.cards[this.index];
			this.index++;// ptr on the next card potentially available

			return cardTmp;	
		}

		public Card giveNewCard()
		{
			Card card = this.getNextCardAvailable();
			card.isInHand = true;
		
			return card;
		}
		public void changeStatusOfCards(List<Card> listOfCards)
		{	
			for(int i = 0; i < listOfCards.Count; i++)
			{

				Card card = listOfCards[i];

				card.isInHand = false;

			}

		}
	}
}
