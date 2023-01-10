using System;
using System.Collections.Generic;
using System.Linq;

namespace Shared.data
{
	/// <summary>
	/// Classe <c>Deck</c> Classe modélisant le deck d'une partie
	/// </summary>
	public class Deck
    {
        public int index { get; set; }
        public List<Card> cards { get; set; }
		
		/// <summary>
		/// Constructeur d'une instance de Deck
		/// </summary>
        public Deck()
		{
			this.index = 0;
			this.cards = new List<Card>();
			this.generate52Cards();
		}

		/// <summary>
		/// Méthode permettant de mélanger les cartes de manière aléatoire
		/// </summary>
		public void shuffleCards()
		{
			Random rand = new Random();
			cards = cards.OrderBy(a => rand.Next()).ToList();
			index = 0;
		}
		/// <summary>
		/// Méthode permettant de générer un jeu de 52 cartes 
		/// </summary>
		public void generate52Cards() 
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
		/// <summary>
		/// Récuperer la prochaine carte disponible dans le deck
		/// </summary>
		/// <returns></returns>
		public Card getNextCardAvailable()
		{
	
			while(this.cards[this.index].isInHand)
			{
				this.index = (this.index + 1) % this.cards.Count;
			}

			Card cardTmp = this.cards[this.index];
			this.index++; // points on the next card potentially available

			return cardTmp;	
		}
		/// <summary>
		/// Donne une nouvelle carte
		/// </summary>
		/// <returns></returns>
		public Card giveNewCard()
		{
			Card card = this.getNextCardAvailable();
			card.isInHand = true;
		
			return card;
		}
		/// <summary>
		/// Permet de changer le statut associé à une carte lorsqu'elle n'est plus disponible
		/// </summary>
		/// <param name="listOfCards"></param>
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
