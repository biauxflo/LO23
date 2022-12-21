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
		}

		public void generate52Cards() //Can also just be the reset() function
		{
			//TODO
			//Implemente
			Card c1 = new Card(0, 'C', 5, true, false);
			Card c2 = new Card(1, 'P', 4, true, false);
			Card c3 = new Card(2, 'C', 2, true, false);
			Card c4 = new Card(3, 'C', 7, true, false);
			Card c5 = new Card(4, 'C', 10, true, false);


			this.cards.Add(c1);
			this.cards.Add(c2);
			this.cards.Add(c3);
			this.cards.Add(c4);
			this.cards.Add(c5);


			for(int i = 6; i <= 10; i++)
			{
				Card c6 = new Card(i, 'C', 10, true, false);

				this.cards.Add(c6);
		
			}

			for(int i = 10; i <= 20; i++)
			{
				Card c7 = new Card(i, 'P', 10, false, false);

				this.cards.Add(c7);

			}


		}
		public Card getNextCardAvailable()
		{
	
				while(this.cards[this.index].isInHand==true )
			{
					this.index = (this.index + 1) % 52;
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
