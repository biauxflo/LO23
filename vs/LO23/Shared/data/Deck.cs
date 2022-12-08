using System;
using System.Collections.Generic;

namespace Shared.data
{
    public class Deck
    {
        public int index { get; set; }
        public List<Card> cards { get; set; }

        public Deck()
		{
			generate52Cards();
		}

		private void generate52Cards() //Can also just be the reset() function
		{
			//TODO
			//Implemente
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
		public void giveBackCards(List<Card> listOfCards)
		{
			for(int i = 0; i < listOfCards.Count; i++)
			{
				Card card = listOfCards[i];
				card.isInHand = false;
			}

		}
	}
}
